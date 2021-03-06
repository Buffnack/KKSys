﻿using System;
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
  
   
   //Fehlt: Onetime events
   //Befuerchtung: Synchroner Aufruf wuerde kollidieren mit der GUI: Sprich keine Reaktion solange gewartet wird
   //Sollte daher in einem Thread ablaufen
   //TODO: Filter-Anfrage
   //TODO: Events werden noch nicht rangeholt... bzw vollständig rangeholt
   //TODO: Create hide folder with Database!
   //TODO: Async database call for cards and themes
    class DatabaseConnector
    {
        //Maybe remove this
        public static List<KKSysForms_CardModel.Card> AddToDatabase = new List<KKSysForms_CardModel.Card>();
       
        //The one and only instance
        private static DatabaseConnector instance;

        //fixed memory var for database connection
        private SQLiteConnection connection;

        //FIxed memory var for sql command
        private static SQLiteCommand command;



        private static SQLiteDataReader resultTable;

        //Sollten wir auf Dateisystemebene verstecken
        private const String databaseName = "KKSys.db";
        
        //Singleton pattern: Getter of this instance
        public static DatabaseConnector getInstance()
        {
            if (instance == null)
            {
               
                instance = new DatabaseConnector();
                
            }
            return instance;
        }

        //Main interface
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

        //InitialCall to Database - returns all EventLabels, Events and Themes 
        public List<KKSysForms_Event.EventLabel> InitialCallDatabase()
        {
            List<KKSysForms_Event.EventLabel> eventLabels = GetEventLabels();
            foreach (EventLabel el in eventLabels)
            {
                el.getEventList().AddRange(GetRepeatEvents(el));
                el.getThemeList().AddRange(GetThemes(el));
            }
            return eventLabels;
        }

        //Main interface - return all eventlabels
        private List<KKSysForms_Event.EventLabel> GetEventLabels()
        {
            ResetCommand();
            List<EventLabel> returnList = new List<EventLabel>();
            command.CommandText = "SELECT * FROM EventLabel WHERE ID > 0";

            resultTable = command.ExecuteReader();

            //Hier werden alle EventLabels ermittelt
            while (resultTable.Read())
            {
                EventLabel eventLabel = new EventLabel(resultTable[1].ToString(), true);
                eventLabel.IDatabaseID = resultTable.GetInt64(0);
                returnList.Add(eventLabel);
            }
            resultTable.Close();
            return returnList;


        }

        //First we Try to get all Repeatevents
        //Restruct EventLabel.Events
        private List<Event> GetRepeatEvents(EventLabel el)
        {
            List<Event> returnList = new List<Event>();
            command.CommandText = "SELECT ID,serialized FROM RepeatEvents WHERE LabelID =" + el.IDatabaseID + ";";
            byte[] serialized;
            RepeatEvent re;
            resultTable = command.ExecuteReader();
            while (resultTable.Read())
            {
                serialized = (byte[])resultTable.GetValue(1);
                re = (RepeatEvent)Serialize.GetDeserializeObject(serialized);
                re.IDatabaseID = resultTable.GetInt64(0);
                returnList.Add(re);
            }
            resultTable.Close();
            return returnList;
        }

        //Implement
        private List<NonRepeatingEvents> GetNonRepeatEvents(EventLabel el)
        {

            return null;
        }
        //Implement
        private List<NonReferencedOneTimeEvent> GetNonReferencedOneTimeEvents(EventLabel el)
        {
            return null;
        }
        //Implement
        private List<ReferencedOneTimeEvent> GetReferencedOneTimeEvents(EventLabel el)
        {
            return null;
        }

        //Gets the themes from database
        private List<KKSysForms_CardModel.Theme> GetThemes(EventLabel el)
        {
            List<Theme> returnList = new List<Theme>();
            Theme thm;
            command.CommandText = "SELECT ID, nameOf FROM Thema WHERE belongsTO = " + el.IDatabaseID + ";";
            resultTable = command.ExecuteReader();
            while (resultTable.Read())
            {
                thm = new Theme(resultTable.GetString(1), true);
                thm.IDatabaseID = resultTable.GetInt64(0);
                thm.GetQA().AddRange(GetCards(thm));
                GetCards(thm);
                returnList.Add(thm);
            }
            resultTable.Close();
            return returnList;
        }

        //Sets cards of theme
        private List<QACard> GetCards(Theme th)
        {
            ResetCommand();
            command.CommandText = "SELECT ID,serialized from QACard where ThemeID = " + th.IDatabaseID + ";";
            SQLiteDataReader tmpTable;
            List<QACard> returnList = new List<QACard>();
            tmpTable = command.ExecuteReader();
            while (tmpTable.Read())
            {
                QACard tmpCard = (QACard)Serialize.GetDeserializeObject((byte[])tmpTable.GetValue(1));
                tmpCard.IDatabaseID = tmpTable.GetInt64(0);
                returnList.Add(tmpCard);
                
            }
            tmpTable.Close();
            return returnList;
            
            
        }
        //TODO
        //This Method executes a Filter
        //THis method should find cards with tags
        public List<KKSysForms_Event.EventLabel> FilterCallEvent(KKSysForms_Filter.Filter filter)
        {
            return null;
        }

        //This method insert the labels and themes into the database
        //needed to insert cards 
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
                        command.CommandText = "SELECT ID From Thema where nameOf = '" + th.ThemeName + "' and belongsTo = " + el.IDatabaseID + ";";
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
        //TODO: Implement NonRepeatEvents
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

                        //Anfrage
                        String cmdGen= "UPDATE RepeatEvents SET LabelID = "+ el.IDatabaseID + ", nameOf =" + re.Name + "',dayCode = '" + generateDayCodeKurz(re.dayCode) + "',serialized = ? WHERE ID = " + re.IDatabaseID + ";";
                        
                        SQLiteParameter param = new SQLiteParameter();
                        command.CommandText = cmdGen;
                        command.Parameters.Add(param);
                        param.Value = Serialize.GetSerializeByte(re);
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
        
        //TODO: Maybe change param
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
                tmpRe = new System.IO.StreamReader("SQL\\SQL_Init\\InitializeDatabase.sql");
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
