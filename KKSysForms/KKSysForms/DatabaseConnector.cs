using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
using KKSysForms_Event;
using KKSysForms_CardModel;
//Webserver database (SQL Server 2000 +)
//using System.Data.SqlClient;
//Connection driver for legacy Databases (possible unused) 
//using System.Data.Odbc;
//Connection to SQL 6.5 or higher
//using System.Data.OleDb;

using System.Data.SQLite;

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

        private static List<byte[]> readyToAdd;

        private static List<List<byte[]>> history;

       
        //The one and only instance
        private static DatabaseConnector instance;

        private SQLiteConnection connection;

        private static SQLiteCommand command;

        private static BinaryFormatter bf;

        private static MemoryStream ms = null;

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

        //TODO
        //This method should load all EventLabel from database and should load all weekly and incomming events
        //Was ist, wenn die Datenbank leer ist? vorher anfragen ob eine Tabelle mit Settings existiert! TODO
        //Weniger Variablen waeren schön
        //TODO: NonRepeat and Replace
        public List<KKSysForms_Event.EventLabel> InitialCallEventLabel_Repeat()
        {
            //Befehl zuruecksetzen
            ResetCommand();
            //Liste die zurueckgegeben wird - leer
            List<EventLabel> returnList = new List<EventLabel>();
            //Warteschlange fuer die einzelnen Labels - leer
            Queue<EventLabel> queue = new Queue<EventLabel>();
            //Liste mit den IDs -> sollte querey sein
            Queue<Int64> idOfEventAtPlace = new Queue<Int64>();
            command.CommandText = "SELECT * FROM EventLabel WHERE ID > 0";

            resultTable = command.ExecuteReader();
            String read = "";
            //Hier werden alle EventLabels ermittelt
            while (resultTable.Read())
            {
                idOfEventAtPlace.Enqueue((Int64)(resultTable[0]));
                EventLabel eventLabel = new EventLabel(resultTable[1].ToString(), true);
                queue.Enqueue(eventLabel);
            }
            //Ergebnistablle schlieszen
            resultTable.Close();

            //Search by ID
            Int64 searchId; 
            
            //Aktuell zu betrachtendes EventLabel
            EventLabel el;
            
          

            //Speicher fuer das Blob object
            byte[] serialized;
            //Inaktive Events beachten! TODO
            //TODO: Maybe try to reduce this to one loop except for 3 Loops for all kinds of Events
            while (idOfEventAtPlace.Count != 0)
            {
                //This Command gets the serial of the label
                searchId = idOfEventAtPlace.Dequeue();
                //Waehle shit from Database
                command.CommandText = "SELECT ID,serialized FROM RepeatEvents WHERE LabelID =" + searchId + ";";

                //Eventlabel wird herausgenommen
                el = queue.Dequeue();
                //Die Ergebnis tabelle
                resultTable = command.ExecuteReader();




                //Schleife, erste Zeile wurde bereitsgeladen
                while (resultTable.Read())
                {

                    //ID vom Event - 
                    Int64 idOfEvent = (Int64)resultTable.GetInt64(0);
                    //Event Object 
                    serialized = (byte[])resultTable.GetValue(1);
                    //Deserialisierung
                    ms = new MemoryStream(serialized);
                    RepeatEvent tempEvent = (RepeatEvent)bf.Deserialize(ms);
                    ms.Close();

                    //Setzen der serial fuer spaeter
                    tempEvent.IDatabaseID = idOfEvent;
                    //Haengen das Event an das Eventlaebel an
                    el.addEvent(tempEvent);
                    tempEvent = null;




                }
                returnList.Add(el);
                resultTable.Close();


                if (ms != null)
                {
                    ms.Dispose();
                }
               

            }
            //Ressourcen freigeben
           
           

            return returnList;
        }

        //TODO
        //This Method executes a Filter
        public List<KKSysForms_Event.EventLabel> FilterCallEvent(KKSysForms_Filter.Filter filter)
        {
            return null;
        }



        public void InsertEvents(List<EventLabel> labledEvents)
        {
            ResetCommand();
            List<Event> eventList;
            Int64 eventLabelId;
            foreach (EventLabel el in labledEvents)
            {
                eventList = el.getEventList();
                if (el.ICreated)
                {
                    command.CommandText = "INSERT INTO EventLabel (nameOf) VALUES ('" + el.Name + "');";
                    command.ExecuteNonQuery();
                    
                        
                }
                command.CommandText = "SELECT ID FROM EventLabel WHERE nameOf ='" + el.Name + "';";
                resultTable = command.ExecuteReader();
                resultTable.Read();
                eventLabelId = Int64.Parse(resultTable[0].ToString());
                resultTable.Close();
                foreach (Event ev in eventList)
                {
                    if (ev is RepeatEvent)
                    {
                        RepeatEvent re = (RepeatEvent)ev;
                        if (re.ICreated)
                        {
                            String dayCode = generateDayCodeKurz(re.dayCode);
                            command.CommandText = "INSERT INTO RepeatEvents (LabelId, NameOf, serialized,DayCode) VALUES (" + eventLabelId + ",'" + ev.Name + "',?,'" + dayCode + "');";
                            ms = new MemoryStream();
                            bf.Serialize(ms, re);
                            byte[] data = ms.ToArray();
                            ms.Close();
                            ms.Dispose();
                            SQLiteParameter param = new SQLiteParameter();
                            command.CreateParameter();
                            command.Parameters.Add(param);
                            param.Value = data;

                            command.ExecuteNonQuery();
                        }
                        else if (re.IModified)
                        {
                            bool updateDay = false;
                            bool updateName = false;
                            bool updateLabel = false;

                            Int64 serial = re.IDatabaseID;
                            command.CommandText = "SELECT LabelID,nameOf, DayCode FROM RepeatEvents WHERE ID =" + serial + ";";

                            resultTable = command.ExecuteReader();
                            resultTable.Read();
                            Int64 databaseLabelID = (Int64)resultTable.GetInt64(0);
                            List<DayOfWeek> databaseDays = this.parseDatabaseDayCodeToList(resultTable.GetString(2));
                            String databaseName = resultTable.GetString(1);

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


                            //Checking, Label has to be updated
                            resultTable.Close();
                            Int64 currentLabelID = 0;
                            if (el.ICreated)
                            {
                                updateLabel = true;
                            }
                            else
                            {
                                command.CommandText = "SELECT ID FROM EventLabel WHERE NameOf = '" + el.Name + "';";
                                resultTable = command.ExecuteReader();
                                resultTable.Read();
                                currentLabelID = resultTable.GetInt64(0);
                                resultTable.Close();
                                if (currentLabelID != databaseLabelID)
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
                                    command.CommandText = "INSERT INTO EventLabel (nameOf) VALUES ('" + el.Name + "');";
                                    command.ExecuteNonQuery();
                                    command.CommandText = "SELECT ID FROM EventLabel WHERE nameOf = '" + el.Name + "';";
                                    resultTable = command.ExecuteReader();
                                    currentLabelID = resultTable.GetInt64(0);
                                    resultTable.Close();
                                    //Got The new Created ID

                                }
                                cmdGen = cmdGen + "LabelID = " + currentLabelID + ",";
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
                            ms = new MemoryStream();
                            bf.Serialize(ms, re);
                            byte[] data = ms.ToArray();
                            ms.Close();
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
        }

        public void InsertCard(KKSysForms_CardResultTable.CardStack cardStack)
        {
            ResetCommand();
            List<Card> tmpList = cardStack.GetCardPack();
            
            foreach (Card card in tmpList)
            {
                if (card is ContentCard)
                {
                    ContentCard tmpCard = (ContentCard)card;
                    if (tmpCard.ICreated)
                    {
                        //Insert into Database
                        //Maybe Tag ID List before n* database access
                        

                    }
                    else if (tmpCard.IModiefied)
                    {
                        //Update into database using IDatabaseID
                    }
                    else
                    {
                        //Skip if not Created either modified
                        continue;
                    }

                }
                else if (card is QACard)
                {
                    QACard tmpCard = (QACard)card;
                    if (tmpCard.ICreated)
                    {
                        //Insert into Database
                    }
                    else if (tmpCard.IModiefied)
                    {
                        //Update into database using IDatabaseID
                    }
                    else
                    {
                        //Skip if not Created either modified
                        continue;
                    }
                }

            }
        }

        
        

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
           

            //if no exception, initialize all objects;
            bf = new BinaryFormatter();



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
