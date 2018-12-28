using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KKSysForms_CardModel;
using KKSysForms_Event;



namespace KKSysForms_Filter
{
    abstract class Filter 
    {
        protected bool noFilter;

        //How the const should be?

        public String generateSQLRequest()
        {
            return generateSQLSelect() + generateSQLFrom() + generateSQLWhere();
        }
        protected abstract String generateSQLWhere();
        protected abstract String generateSQLFrom();
        protected abstract String generateSQLSelect();
        protected abstract String generateSQLCreateView();
    }
    //Definetly check it with Franz
    //REwork THIS!
    class CardFilter : Filter
    {
        private bool isContent { get; set; }
        //Highly need Coding for combs
        private String tagName { get; set; }
        

        //COntains the name of the Content - not a specific Event
        private EventLabel label { get; set; }

        

        //weitere filter ?

        

        protected override string generateSQLCreateView()
        {
            return "";
        }
        //THis Method should generate
        //Requires KarteiCard declaration in Database
        protected override string generateSQLSelect()
        {
            String returnVar;
            if (noFilter)
            {
                returnVar = "SELECT serialized FROM KarteiCard";
            }
            else
            {
                returnVar = "SELECT kc.serialized FROM KarteiCard k";
            }
           
            
            return returnVar;
        }
        //Return extra SQL FROM declaration
        protected override string generateSQLFrom()
        {
            String returnVar = "";
            if (noFilter)
            {
                returnVar = "";
            }
            else
            {
                if (this.tagName.Length > 0)
                {
                    returnVar = ", Tag t";
                }
                if (this.label != null)
                {
                    returnVar = returnVar + ", EventLabel el";
                }
            
            }
            return returnVar;
        }

        //This Method should generate the WHERE in the SELECT statement
        protected override string generateSQLWhere()
        {
            String returnVar = "";
            if (noFilter)
            {
                return returnVar;
            }
            else
            {
                returnVar = "WHERE";
                if (this.tagName.Length > 0)
                {
                    returnVar = "(t.nameOf="+tagName+" AND t.ID = k.tagID) AND";
                }
                if (this.label != null)
                {
                    returnVar = returnVar + ", EventLabel el";
                    returnVar = returnVar + "(el.ID = k.belongTo AND el.nameOf = " + label.Name + ") AND";
                }


                if (returnVar.EndsWith("AND"))
                {
                    returnVar = returnVar.Substring(0, returnVar.Length - 4);
                }
                return returnVar;

            }     

        }     
    }

    class EventFilter : Filter
    {

        //Hirachy: If EventLabel is null, search for all Events with DayCode or Date in all Tables
        // If Date is set, and DayCode too - throw Error or make two SQL calls
        

        //If null, show all for day
        //If not null, ignore DayOfWeek and show all with eventLabel
        public String EventLabelName { get; set; }

        //If null, set it to "No Day"
        public String DayCode { get; set; }

        //What about Date later than exspected?
        public DateTime Date { get; set; }

        


        public EventFilter()
        {
            this.noFilter = false;
            
            
            this.EventLabelName = null;
        }

        //SHould be used for holding the last Statement
        protected override string generateSQLCreateView()
        {
            throw new NotImplementedException();
        }
        //Should be used to select the filter
        protected override string generateSQLFrom()
        {
            throw new NotImplementedException();
        }

        protected override string generateSQLSelect()
        {
            throw new NotImplementedException();
        }

        protected override string generateSQLWhere()
        {
            throw new NotImplementedException();
        }
    }
}
