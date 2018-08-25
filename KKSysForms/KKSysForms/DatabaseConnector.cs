using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
using KKSysForms_Event;
using KKSysForms_CardModel;
using KKSysForms_Filter;
//Webserver database (SQL Server 2000 +)
//using System.Data.SqlClient;
//Connection driver for legacy Databases (possible unused) 
//using System.Data.Odbc;
//Connection to SQL 6.5 or higher
//using System.Data.OleDb;

using System.Data.SQLite;
using KKSysForms_SerializeBoundModul;

//using Npgsql;

//We gonna use PostgreSQL because its free -> need extern reference!
//Postgre für WEbserveranbindung
//Was mir nicht gefällt: Installation der Datenbank nötig..... SQLite?
//Geht klar, Licensierung darf aber nicht auf SQLite erfolgen - sprich copyright is nicht anmeldbar solange
//SQLite mit lizensiert wird
//Teste IF not exists anweisung
namespace KKSysDatabase
{
   //Was funktioniert: Wiederholende Events einfuegen und updaten
   //Anlegen der Datenbank
   //Auslesen der Datenbank (wiederholende nur
   //Fehlt: Onetime events
   //Befuerchtung: Synchroner Aufruf wuerde kollidieren mit der GUI: Sprich keine Reaktion solange gewartet wird
   //Sollte daher in einem Thread ablaufen
    class DatabaseConnector
    {
        public static List<KKSysForms_CardModel.Card> AddToDatabase = new List<KKSysForms_CardModel.Card>();
       
        //The one and only instance
        private static DatabaseConnector instance;

        private SQLiteConnection connection;

        private static SQLiteCommand command;



        private static SQLiteDataReader resultTable;

        //Sollten wir auf Dateisystemebene verstecken
        private const String databaseName = "KKSys.db";
        //maybe not needed
        private static String pathToDb;

        public static DatabaseConnector getInstance()
        {
            if (instance == null)
            {
               
                instance = new DatabaseConnector();
                
            }
            return instance;
        }


        public void InsertData(List<EventLabel> insert)
        {
            //TagList woher?
            InsertReferences(insert);
            foreach (EventLabel el in insert)
            {
                InsertEvents(el);
                InsertCard(el);
            }
           
            //Wie bekommen wir die Tags hierhin?
            
        }
        //TODO Highly change this method
        //This method should load all EventLabel from database and should load all weekly and incomming events
        //Was ist, wenn die Datenbank leer ist? vorher anfragen ob eine Tabelle mit Settings existiert! TODO
        //Weniger Variablen waeren schön
        //TODO: NonRepeat and Replace
        //Change to private -> public List<KKSysForms_Event.EventLabel> InitialCallDatabase(){}
        //TODO: Auskapseln des EventLabel insert update etc...
        public List<KKSysForms_Event.EventLabel> InitialCallEventLabel_Repeat()
        {
            //Befehl zuruecksetzen
            ResetCommand();
            //Liste die zurueckgegeben wird - leer
            List<EventLabel> returnList = new List<EventLabel>();
            //Warteschlange fuer die einzelnen Labels - leer
            Queue<EventLabel> queue = new Queue<EventLabel>();
            //Liste mit den IDs -> sollte querey sein
            
            command.CommandText = "SELECT * FROM EventLabel WHERE ID > 0";

            resultTable = command.ExecuteReader();
           
            //Hier werden alle EventLabels ermittelt
            while (resultTable.Read())
            {

                
                EventLabel eventLabel = new EventLabel(resultTable[1].ToString(), true);
                eventLabel.IDatabaseID = resultTable.GetInt64(0);
                queue.Enqueue(eventLabel);
            }
            //Ergebnistablle schlieszen
            resultTable.Close();

            
            //Aktuell zu betrachtendes EventLabel
            EventLabel el;
            //Speicher fuer das Blob object
            byte[] serialized;
            //Inaktive Events beachten! TODO
            //TODO: Maybe try to reduce this to one loop except for 3 Loops for all kinds of Events
            while (queue.Count != 0)
            {
                el = queue.Dequeue();
                //This Command gets the serial of the label
                //Waehle shit from Database
                command.CommandText = "SELECT ID,serialized FROM RepeatEvents WHERE LabelID =" + el.IDatabaseID + ";";

                //Eventlabel wird herausgenommen
                
                //Die Ergebnis tabelle
                resultTable = command.ExecuteReader();




                //Schleife, erste Zeile wurde bereitsgeladen
                while (resultTable.Read())
                {

                   
                    //Event Object 
                    serialized = (byte[])resultTable.GetValue(1);

                    RepeatEvent tempEvent = (RepeatEvent)Serialize.GetDeserializeObject(serialized);
                  
                    //Setting EventID
                    tempEvent.IDatabaseID = resultTable.GetInt64(0);
                  
                    //Haengen das Event an das Eventlaebel an
                    el.addEvent(tempEvent);
                    tempEvent = null;




                }
                returnList.Add(el);
                resultTable.Close();


             
               

            }
            
            return returnList;
        }

        //Threads....
        public List<Tag> InitialAsyncCallTagList()
        {

            ResetCommand();
            
            return null;

        }

        //Threads.... oder synchron
        public List<Theme> InitialAsyncCallThemeList()
        {
            return null;
        }

        //TODO
        //This Method executes a Filter
        public List<KKSysForms_Event.EventLabel> FilterCallEvent(KKSysForms_Filter.Filter filter)
        {
            return null;
        }

        //Tags still missing
        private void InsertReferences(List<EventLabel> labels)
        {
            ResetCommand();
            foreach (EventLabel el in labels)
            {
                if (el.ICreated)
                {
                    command.CommandText = "INSERT INTO EventLabel (nameOf) VALUES ('" + el.Name + "');";
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT ID From EventLabel where nameOf = '" + el.Name + "';";
                    resultTable = command.ExecuteReader();
                    resultTable.Read();
                    el.IDatabaseID = resultTable.GetInt64(0);
                    resultTable.Close();
                    el.ICreated = false;
                    el.IModified = false;
                }
                else if (el.IModified)
                {
                    command.CommandText = "UPDATE EventLabel SET nameOf = '" + el.Name + "' where ID = " + el.IDatabaseID + ";";
                    command.ExecuteNonQuery();
                    el.ICreated = false;
                    el.IModified = false;
                }

                List<Theme> themeList = el.getThemeList();
                foreach (Theme th in themeList)
                {
                    if (th.ICreated)
                    {
                        command.CommandText = "INSERT INTO Thema (nameOf, belongsTo) VALUES ('" + th.ThemeName + "', " + el.IDatabaseID + ");";
                        command.ExecuteNonQuery();
                        command.CommandText = "SELECT ID From EventLabel where nameOf = '" + th.ThemeName + "' and belongsTo = " + el.IDatabaseID + ";";
                        resultTable = command.ExecuteReader();
                        resultTable.Read();
                        th.IDatabaseID = resultTable.GetInt64(0);
                        resultTable.Close();
                        th.ICreated = false;
                        th.IModified = false;

                    }
                    else if (th.IModified)
                    {
                        command.CommandText = "UPDATE Thema SET nameOf = '" + th.ThemeName + "', belongsTo = "+el.IDatabaseID+" where ID = " + th.IDatabaseID + ";";
                        command.ExecuteNonQuery();
                        el.ICreated = false;
                        el.IModified = false;
                    }
                }

            }
        }
        //TODO: Change to private
        //Auskapsel von EventLabel insert!
        private void InsertEvents(EventLabel el)
        {
            ResetCommand();
            List<Event> eventList;
          
          
            
            eventList = el.getEventList();
               
            foreach (Event ev in eventList)
            {
                if (ev is RepeatEvent)
                {
                    RepeatEvent re = (RepeatEvent)ev;
                    //Wenn es erstellt ist, wird es einfach eingefügt
                    if (re.ICreated)
                    {
                        String dayCode = generateDayCodeKurz(re.dayCode);
                        command.CommandText = "INSERT INTO RepeatEvents (LabelId, NameOf, serialized,DayCode) VALUES (" + ev.IDatabaseID + ",'" + ev.Name + "',?,'" + dayCode + "');";

                        byte[] data = Serialize.GetSerializeByte(re);
                        

                        SQLiteParameter param = new SQLiteParameter();
                        command.CreateParameter();
                        command.Parameters.Add(param);
                        param.Value = data;

                        command.ExecuteNonQuery();
                    }
                    //WEnn es modifiziert wurde, muss der alte Datenbank eintrag gelöscht werden bzw ersetzt werden
                    //Hierfür ersetzen wir alles: Wenn sich das Label geändert hat, gehört es nicht mehr zu genau diesem Label

                    else if (re.IModified)
                    {
                        bool updateDay = false;
                        bool updateName = false;
                        bool updateLabel = false;

                         
                        command.CommandText = "SELECT LabelID,nameOf, DayCode FROM RepeatEvents WHERE ID =" + re.IDatabaseID + ";";

                        resultTable = command.ExecuteReader();
                        resultTable.Read();
                        Int64 databaseLabelID = (Int64)resultTable.GetInt64(0);
                        List<DayOfWeek> databaseDays = this.parseDatabaseDayCodeToList(resultTable.GetString(2));
                        String databaseName = resultTable.GetString(1);
                        resultTable.Close();
                        if (databaseDays.Count != re.dayCode.Count)
                        {
                            updateDay = true;
                        }
                        else
                        {
                            foreach (DayOfWeek d in databaseDays)
                            {
                                if (!re.dayCode.Contains(d))
                                {
                                    updateDay = true;
                                }
                            }
                        }

                        //Checking Name Update
                        if (!databaseName.Equals(re.Name))
                        {
                            updateName = true;
                        }


                        //Checkin Label
                            
                        if (el.ICreated)
                        {
                            updateLabel = true;
                        }
                        else
                        {
  
                            if (el.IDatabaseID != databaseLabelID)
                            {
                                updateLabel = true;
                            }
                        }

                        //Anfrage generieren
                        String cmdGen = "UPDATE RepeatEvents SET ";
                        if (updateLabel)
                        {

                            if (el.ICreated)
                            {
                                //Muss so sein, da EL noch keine ID hat
                                command.CommandText = "INSERT INTO EventLabel (nameOf) VALUES ('" + el.Name + "');";
                                command.ExecuteNonQuery();
                                command.CommandText = "SELECT ID FROM EventLabel WHERE nameOf = '" + el.Name + "';";
                                resultTable = command.ExecuteReader();
                                el.IDatabaseID = resultTable.GetInt64(0);
                                resultTable.Close();
                                //Got The new Created ID

                            }
                            cmdGen = cmdGen + "LabelID = " + el.IDatabaseID + ",";
                        }

                        if (updateName)
                        {
                            cmdGen = cmdGen + "nameOf = '" + re.Name + "',";
                        }
                        if (updateDay)
                        {
                            cmdGen = cmdGen + " dayCode = '" + generateDayCodeKurz(re.dayCode) + "',";
                        }
                        //Ofc update serialized Object
                        command.CreateParameter();
                      
                        byte[] data = Serialize.GetSerializeByte(re);
                        
                        cmdGen = cmdGen + "serialized = ? WHERE ID = " + re.IDatabaseID+";";
                        SQLiteParameter param = new SQLiteParameter();
                        command.CommandText = cmdGen;
                        command.Parameters.Add(param);
                        param.Value = data;

                        command.ExecuteNonQuery();

                    }
                    ResetCommand();

                }
                else if (ev is NonRepeatingEvents)
                {
                    //Schwierig - was wenn ein gleicher Name existiert? Definitiv unique id rausladen
                    if (ev is ReferencedOneTimeEvent)
                    {

                        throw new NotImplementedException("Not implementd");
                    }
                    else if (ev is NonReferencedOneTimeEvent)
                    {
                        throw new NotImplementedException("Not implementd");
                    }
                    else
                    {
                        throw new Exception("Devs haben wieder scheise gebaut");
                    }

                }
                
            }
        }
        
        //Kapsel Eventlabel update/Insert aus aus beiden FUnktionen
        //TODO: Tags besser definiert!
        private void InsertCard(EventLabel el)
        {
            ResetCommand();
            //Ausgehend davon, das die Labels schon eingelagert worden sind und geupdatet
            //Ebenfalls Themen
            List<Theme> themeList = el.getThemeList();
            List<Card> cardList = new List<Card>();
            ContentCard cc;
            QACard qa;
            foreach (Theme th in themeList)
            {
                cardList.AddRange(th.GetContent());
                cardList.AddRange(th.GetQA());
                foreach (Card ca in cardList)
                {
                    ResetCommand();
                    if (ca.ICreated)
                    {
                        //Find right tabular
                        if (ca is ContentCard)
                        {
                            cc = (ContentCard)ca;
                            //Insert into contentCard
                            command.CommandText = "INSERT INTO ContentCards (ThemeID,Tag, serialized) VALUES (" + th.IDatabaseID + ",0, ?);";
                            command.CreateParameter();
                            SQLiteParameter param = new SQLiteParameter();
                            command.Parameters.Add(param);
                            param.Value = Serialize.GetSerializeByte(cc);

                            command.ExecuteNonQuery();

                        }
                        else if (ca is QACard)
                        {
                            qa = (QACard)ca;
                            //InSert into QA
                            command.CommandText = "INSERT INTO QACard (ThemeID,Tag, serialized) VALUES (" + th.IDatabaseID + ",0, ?);";
                            command.CreateParameter();
                            SQLiteParameter param = new SQLiteParameter();
                            command.Parameters.Add(param);
                            param.Value = Serialize.GetSerializeByte(qa);

                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            throw new Exception("Developers haben wieder mistgebaut...");
                        }

                    }
                    else if (ca.IModified)
                    {
                        //Find right tabular and the entry
                        //Just Update all
                        if (ca is ContentCard)
                        {
                            cc = (ContentCard)ca;
                            //Insert into contentCard
                            command.CommandText = "UPDATE ContentCards SET ThemeID = " + th.IDatabaseID + ", Tag = 0, serialized = ? where ID = " + cc.IDatabaseID + ";";
                            command.CreateParameter();
                            SQLiteParameter param = new SQLiteParameter();
                            command.Parameters.Add(param);
                            param.Value = Serialize.GetSerializeByte(cc);
                            command.ExecuteNonQuery();
                        }
                        else if (ca is QACard)
                        {
                            //TODO: Tags are currently 0 evrytime
                            qa = (QACard)ca;
                            command.CommandText = "UPDATE QACard SET ThemeID = " + th.IDatabaseID + ", Tag = 0, serialized = ?  where ID = "+qa.IDatabaseID+";";
                            command.CreateParameter();
                            SQLiteParameter param = new SQLiteParameter();

                            command.Parameters.Add(param);
                            param.Value = Serialize.GetSerializeByte(qa);
                            command.ExecuteNonQuery();
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
            }        
        }

        //Guess i wanna have not much serialize shit in the insert
        //Class for it
       

        private DatabaseConnector()
        {

            //Create Connection
            connection = new SQLiteConnection();
            connection.ConnectionString = "Data Source=" + databaseName;
            connection.Open();

            //Init Command
            command = new SQLiteCommand(connection);
            try
            {
                initDatabase();
            }
            catch (KKSysForms_Exceptions.SQL_DatabaseExistsException e)
            {
                
            }
           




            //Benoetigt fuer die Serialisierung
            // System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // BinaryFormatter bf = new BinaryFormatter();
            //  bf.Serialize(ms, test);
            // byte[] testbyte = ms.ToArray();




            //Creates PArameter-List for command - requires atleast one (?) inside of sql statement  
            //com.CreateParameter();
            //Required to store datatypes inside of database (like byte[])
            // SQLiteParameter data = new SQLiteParameter();
            //com.Parameters.Add(data);
            //data.Value = testbyte;
            //Used for Executes without Result table (insert, update, delete)
            //  com.ExecuteNonQuery();

            // com.CommandText = "SELECT * FROM TESTTABLE";
            //Requires for Resulttable
            //    SQLiteDataReader sqlr = com.ExecuteReader();
            //sqlr.Read();
            //Blob datatype can be easy casted to byte[]
            // byte[] testbbyte = (byte[])sqlr[1];

            // Test second = (Test)bf.Deserialize(ms);




            //Give Ressource free, else there can not execute anything in sql
            // sqlr.Close();
            // sqlr.Dispose();
            //  com.CommandText = "INSERT INTO TESTWITHSTRING VALUES ('HUEN')";
            //  com.ExecuteNonQuery();
            //  com.CommandText = "INSERT INTO TESTWITHSTRING VALUES ('DU HUEN')";

            // com.ExecuteNonQuery();


            // com.CommandText = "SELECT * FROM TESTWITHSTRING WHERE substr(dude,1,2) = 'HU'";
            // sqlr = com.ExecuteReader();
            // sqlr.Read();

            //Testdaten einfuegen
            //-----------------------------------------------------------

            //command.CommandText = "INSERT INTO EventLabel (nameOf) VALUES ('Stochastik');";
            //command.ExecuteNonQuery();
            //command.CommandText = "INSERT INTO EventLabel (nameOf) VALUES ('Stochastik 2');";
            //command.ExecuteNonQuery();

            //KKSysForms_Event.RepeatEvent testEvent = new KKSysForms_Event.RepeatEvent("Vorlesung", new KKSysForms_Event.TimeStamp(10, 30), new KKSysForms_Event.TimeStamp(12, 0), null, "", "");
            //command.CommandText = "INSERT INTO RepeatEvents (LabelID, nameOf,serialized,DayCode) VALUES (1,?,?,'Mo');";
            //command.CreateParameter();
            //SQLiteParameter sQLiteParameter = new SQLiteParameter();
            //SQLiteParameter sQLiteParamete = new SQLiteParameter();
            //SQLiteParameter sQLiteParamet = new SQLiteParameter();
            //command.Parameters.Add(sQLiteParameter);
            //command.Parameters.Add(sQLiteParamete);
      
            //sQLiteParameter.Value = testEvent.Name;
            //ms = new MemoryStream();
            //bf.Serialize(ms, testEvent);
            //byte[] ser = ms.ToArray();
            //sQLiteParamete.Value = ser;
         
            //command.ExecuteNonQuery();

            //ResetCommand();

            //EventLabel ev = new EventLabel("Mathemathik", false);
            //List<DayOfWeek> dayList = new List<DayOfWeek>();
            //dayList.Add(DayOfWeek.Monday);
            //dayList.Add(DayOfWeek.Friday);
            //ev.addEvent(new RepeatEvent("Analysis 1", new TimeStamp(10, 30), new TimeStamp(12, 00),dayList,"",""));
            //dayList.Reverse();
            //ev.addEvent(new RepeatEvent("Analysis 2", new TimeStamp(10, 00), new TimeStamp(13, 00), dayList, "", ""));
            //ev.addEvent(new RepeatEvent("Analysis 3", new TimeStamp(12, 00), new TimeStamp(13, 00), dayList, "", ""));
            

            //command.CommandText = "Insert into RepeatEvents ( LabelID,nameOf, serialized, dayCode) VALUES (1,'This is a Name', ?,Mo);";
            //command.CreateParameter();
            //command.Parameters.Add(sQLiteParamet);
            //RepeatEvent update = new RepeatEvent("Analysis 4", new TimeStamp(12, 00), new TimeStamp(13, 00), dayList, "", "");
            

          

            //List<EventLabel> evList = new List<EventLabel>();
            //evList.Add(ev);
            //InsertEvents(evList);
            //ev.created = false;
            //update.created = false;
            //evList.Clear();
            //update.modified = true;
            //update.serialID = 3;
            //ev.getEventList().Clear();
            //ev.addEvent(update);
            //evList.Add(ev);
            //InsertEvents(evList);
            
        }


        private void initDatabase()
        {

            System.IO.StreamReader tmpRe; 
            String tmp;
            try
            {
                tmpRe = new System.IO.StreamReader("InitializeDatabase.sql");
                while ((tmp = tmpRe.ReadLine()) != null)
                {
                    command.CommandText = tmp;
                    command.ExecuteNonQuery();
                }

            }
            catch (System.IO.IOException e)
            {
                throw new Exception("Error occured by File" + e);
            }
            catch (SQLiteException e)
            {
                throw new KKSysForms_Exceptions.SQL_DatabaseExistsException();
            }

           
        }

        private String generateDayCodeKurz(List<System.DayOfWeek> list)
        {
            String returnVar = "";

            //Sollte Absteigend sortieren (mo als erstes) is sonst eig egal
            list.Sort();

            foreach (DayOfWeek dw in list)
            {
                switch (dw)
                {
                    case (DayOfWeek.Monday):
                        returnVar = returnVar + "Mo";
                        break;
                    case (DayOfWeek.Tuesday):
                        returnVar = returnVar + "Tu";
                        break;
                    case (DayOfWeek.Wednesday):
                        returnVar = returnVar + "We";
                        break;
                    case (DayOfWeek.Thursday):
                        returnVar = returnVar + "Th";
                        break;
                    case (DayOfWeek.Friday):
                        returnVar = returnVar + "Fr";
                        break;
                    case (DayOfWeek.Saturday):
                        returnVar = returnVar + "Sa";
                        break;
                    case (DayOfWeek.Sunday):
                        returnVar = returnVar + "Su";
                        break;
                    default:
                        throw new Exception("Da existiert etwas anderes als ein DayOfTheWeek...");
                }
            }

            return returnVar;
        }

        private List<System.DayOfWeek> parseDatabaseDayCodeToList(String dayCode)
        {
            List<DayOfWeek> dayList = new List<DayOfWeek>();
            // Mo Tu We Th Fr Sa Su
            if (dayCode.Contains("Mo"))
            {
                dayList.Add(DayOfWeek.Monday);
            }
            if (dayCode.Contains("Tu"))
            {
                dayList.Add(DayOfWeek.Tuesday);
            }
            if (dayCode.Contains("We"))
            {
                dayList.Add(DayOfWeek.Wednesday);
            }
            if (dayCode.Contains("Th"))
            {
                dayList.Add(DayOfWeek.Thursday);
            }
            if (dayCode.Contains("Fr"))
            {
                dayList.Add(DayOfWeek.Friday);
            }
            if (dayCode.Contains("Sa"))
            {
                dayList.Add(DayOfWeek.Saturday);
            }

            if (dayCode.Contains("So"))
            {
                dayList.Add(DayOfWeek.Sunday);
            }
            return dayList;
        }

        private void ResetCommand()
        {

            command.Dispose();
            command = new SQLiteCommand(connection);
            
        }
        //Requires FilterOption

    }


}
