using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KKSysForms_CardModel;
using KKSysForms_CardResultTable;
using KKSysForms_Filter;
using KKSysForms_Event;
using KKSysDatabase;
using KKSysForms_DataTypes;

namespace KKSysForms
{
    class KKSys
    {

        private DatabaseConnector database;

        private EventLabel currentTarget;

        //Need to be static
        public static List<EventLabel> loadedLabel;

        public DateTime today;

        //Nicht brauchbar
        private Filter Eventfilter;

        public static bool Stored
        {
            get { return Stored; }
            set { if (value)
                {
                    DatabaseConnector.getInstance().InsertData(loadedLabel);
                    Stored = value;
                }
                else
                {
                    Stored = value;
                }
            }
        }
        

        public KKSys(Form1 test)
        {
            database = DatabaseConnector.getInstance();
            today = DateTime.Now;
            loadedLabel = database.InitialCallEventLabel_Repeat();


            String tmp = "";
            String whatIsIn = "";
            foreach (EventLabel tust in loadedLabel)
            {
                
                tmp = tmp + tust.Name+ " hat folgende Events:\n";
                if (tust.getEventList() == null)
                {
                    tmp = tmp + " keine \n";
                }
                else
                {
                    if (tust.getEventList().Count == 0)
                    {
                        tmp = tmp + " keine \n";
                    }
                    else
                    {
                        List<Event> tump = tust.getEventList();
                        foreach (Event testo in tump)
                        {
                            tmp = tmp + testo.Name + " mit ID "+testo.IDatabaseID +"\n";
                        }

                    }
                }

            }

            test.setRt(tmp);
        }

        public void CreateEventLabel(String name)
        {
            if (currentTarget != null)
            {
                EventLabel tmpLabel = new EventLabel(name, false);
                if (loadedLabel.Contains(tmpLabel))
                {
                    throw new Exception("Dieses Label existiert schon");
                }
                else
                {
                    currentTarget = tmpLabel;
                }
            }
            else
            {
                currentTarget = new EventLabel(name,false);
            }
        }

        public void ModifyLabelToEvent(EventLabel label, Event @event, bool remove)
        {
            if (remove)
            {
                label.removeEvent(@event);
            }
            else
            {
                label.addEvent(@event);
            }
           
        }

        //This Method needs a high lvl parser in GUI
        public List<Event> showEvents(bool fromTarget)
        {
            if (fromTarget)
            {
                return this.currentTarget.getEventList();
            }
            else
            {
                List<Event> unsortedList = new List<Event>();
                List<Event> tmp = new List<Event>();
                foreach (EventLabel el in loadedLabel)
                {
                    tmp = el.getEventList();
                    foreach (Event ev in tmp)
                    {
                        unsortedList.Add(ev);
                    }
                }
                return unsortedList;

            }
        }

        public void SetFilter(Filter filter)
        {
            this.Eventfilter = filter;
        }

        //TODO
        private void GenerateDaysLeftForOneTime()
        {

        }

        private void getCardsFromDatabase()
        {

        }

    }
}
