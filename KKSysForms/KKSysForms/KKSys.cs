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
using KKSysForms_PDFCreate;

namespace KKSysForms
{
    //First Iteration: Implement Adding and Database insert for exceptions
    //Second Iteration: Implement Remove and Modify for exceptions
    //Third Iteration: Implement Filter
    class KKSys
    {

        private DatabaseConnector database;

        private EventLabel currentTarget;

        //Need to be static
        public List<EventLabel> loadedLabel;

        public List<Theme> currentTargetThemes;

        public DateTime today;

        //Nicht brauchbar
        private Filter Eventfilter;

        private bool datastored;

        public bool Stored
        {
            get { return datastored; }
            set { if (value)
                {
                    DatabaseConnector.getInstance().InsertData(this.loadedLabel);
                    datastored = value;
                }
                else
                {
                    Stored = value;
                }
            }
        }
        

        public KKSys()
        {
            database = DatabaseConnector.getInstance();
            loadedLabel = database.InitialCallEventLabel_Repeat();
            
          
        }

        public void CreatePDF()
        {
            GeneratePDF.GeneratePDFFile(currentTarget.getThemeList().First().GetQA(), "TestTex");
        }

        public void CreateEventLabel(String name)
        {
            EventLabel tmp = new EventLabel(name, false);
            loadedLabel.Add(tmp);
        }

        public void SetCurrentEventLabelTarget(String name)
        {
            foreach (EventLabel label in loadedLabel)
            {
                if (label.Name == name)
                {
                    currentTarget = label;
                    break;
                }
            }
        }

        public void CreateTheme(String name)
        {
            Theme them = new Theme(name);
            currentTarget.getThemeList().Add(them);

        }

        //TODO: Rework that shit
        public void CreateCards(String theme,String qhead, String ahead, String qcontent, String acontent)
        {
            Theme tmp = new Theme("None");
            tmp.IDatabaseID = 0;
            CompositeDatatype test, tmpo;
            test = new CompositeDatatype();
            test.AddComponent(new Text(qcontent));
            tmpo = new CompositeDatatype();
            tmpo.AddComponent(new Text(acontent));
            tmp.AddCard(new QACard(qhead, ahead, test, tmpo));

        }

        //Parameter List
        public void CreateEvent()
        {

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

        private void getCardsFromDatabase()
        {

        }

    }
}
