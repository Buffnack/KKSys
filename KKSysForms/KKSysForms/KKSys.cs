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

        private Theme currentThemeTarget;

        //Need to be static
        public List<EventLabel> loadedLabel;

       

        public DateTime today;

        //Nicht brauchbar
        private Filter Eventfilter;

        private bool datastored;

        public bool Stored
        {
            get { return datastored; }
            set {
                if (value)
                {
                    DatabaseConnector.getInstance().InsertData(this.loadedLabel);
                    datastored = value;
                    if (loadedLabel.Contains(currentTarget))
                    {
                        if (currentTarget.getThemeList().Contains(currentThemeTarget))
                        {

                        }
                        else
                        {
                            throw new Exception("Damn it");
                        }
                    }
                    else
                    {
                        throw new Exception("Damn it 2");
                    }
                }
                else
                {
                    datastored = value;
                }
            }
        }
        

        public KKSys()
        {
            database = DatabaseConnector.getInstance();
            loadedLabel = database.InitialCallDatabase();
            currentTarget = loadedLabel.ElementAt(0);
            
          
        }

        public ref List<EventLabel> GetLoadedReference()
        {
            return ref loadedLabel;
        }

        public ref EventLabel GetCurrentEventLabelTargetReference()
        {
            return ref currentTarget;
        }

        public ref Theme GetCurrentThemeReference()
        {
            return ref currentThemeTarget;
        }

        public void CreatePDF(Theme name)
        {
            
            GeneratePDF.GeneratePDFFile(name.GetQA(), name.ThemeName);
        }

        public void CreateEventLabel(String name)
        {
            EventLabel tmp = new EventLabel(name, false);
            loadedLabel.Add(tmp);
            currentTarget = tmp;
            Stored = false;
        }

        public void SetCurrentEventLabelTarget(EventLabel el)
        {
            foreach (EventLabel find in loadedLabel)
            {
                if (el.Name == find.Name)
                {
                    currentTarget = find;
                }
            }
            currentThemeTarget = null;
        }

        public void SetCurrentThemeTarget(Theme th)
        {
            foreach (Theme find in currentTarget.getThemeList())
            {
                if (th.ThemeName == find.ThemeName)
                {
                    currentThemeTarget = find;
                }
            }
        }

        public void CreateTheme(String name)
        {
            Theme them = new Theme(name, false);
            currentTarget.getThemeList().Add(them);
            Stored = false;

        }

        //TODO: Rework that shit
        public void CreateCards(Theme theme,String qhead, String ahead, String qcontent, String acontent)
        {
            
            CompositeDatatype test, tmpo;
            test = new CompositeDatatype();
            test.AddComponent(new Text(qcontent));
            tmpo = new CompositeDatatype();
            tmpo.AddComponent(new Text(acontent));
           
            currentThemeTarget.AddCard(new QACard(qhead, ahead, test, tmpo));
            Stored = false;
                    
           
          

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
