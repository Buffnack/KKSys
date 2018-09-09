using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KKSysForms_Event;
using KKSysForms_CardModel;
using KKSysForms_PDFCreate;

namespace KKSysForms
{
    //This class should load the plan
    class EventManager
    {
        private List<EventLabel> eventLabelList;

        private List<Event> currentLoaded;

        private List<NonRepeatingEvents> nonRepeatings;
        private List<RepeatEvent> repeatEvents;

        public EventManager(ref List<EventLabel> evList)
        {
            eventLabelList = evList;
            String day = GetTodayDayCode();
            //Sort Lists;
            
        }

        private String GetTodayDayCode()
        {
            DayOfWeek date = DateTime.Now.DayOfWeek;
            switch (date)
            {
                case DayOfWeek.Monday:
                    return "Mo";
                case DayOfWeek.Tuesday:
                    return "Di";
                case DayOfWeek.Wednesday:
                    return "Mi";
                case DayOfWeek.Thursday:
                    return "Do";
                case DayOfWeek.Friday:
                    return "Fr";
                case DayOfWeek.Saturday:
                    return "Sa";
                case DayOfWeek.Sunday:
                    return "So";
                default:
                    throw new Exception("Ups");
            }
        }
        

        

    }

    class CardManager
    {
        private List<Card> allCards;

        private List<Card> currentCards;

        private List<QACard> currentTestCards; 
    
    }
}
