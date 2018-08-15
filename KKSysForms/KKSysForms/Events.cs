using System;
using System.Collections.Generic;


//Serializeability is missing
//Std value for start end
namespace KKSysForms_Event
{
    internal enum DayCode { Mon, Di, Mi, Do, Fr, Sa, So }
    [Serializable]
    class EventLabel
    {
        public String Name { get; }
        private List<Event> eventsUnderLabel;
        

        public EventLabel(String label)
        {
            Name = label;
            this.eventsUnderLabel = new List<Event>();
            
        }

        public void addEvent()
        {

        }

        public void removeEvent()
        {

        }

        public List<Event> getEventList()
        {
            return this.eventsUnderLabel;
        }

        
    }
    
    abstract class Event
    {
        public int Start { get; set; }

        public int End { get; set; }

        public String Name { get; set; }

        public EventLabel EventLabel;

        protected bool modified;

        protected bool created;

        public Event(EventLabel label,String name, int start, int end)
        {
            
            this.EventLabel = label;
            this.Name = name;
            this.Start = start;
            this.End = end;
        }


    }

  

    abstract class RepeatingEvents : Event
    {
        protected DayOfWeek dayCode;

        public RepeatingEvents(EventLabel lab,String name, int start, int end, DayOfWeek dayCode) : base(lab,name, start, end)
        {

        }

        public void createUniqueChangeEvent()
        {

        }



    }

   

    abstract class NonRepeatingEvents : Event
    {
        protected RepeatingEvents replace { get; set; }
        protected int numOfReplace { get; set; }

        public NonRepeatingEvents(RepeatingEvents forEvent) : base(forEvent.EventLabel,forEvent.Name, forEvent.Start, forEvent.End)
        {

        }
        public NonRepeatingEvents(EventLabel lab, String name, int start, int end, DateTime date) : base(lab, name, start, end)
        {

        }

    }
    //Fuer Termine, die mal verschoben werden (mit Ausfall)
    class ReferencedOneTimeEvent : NonRepeatingEvents
    {
        public ReferencedOneTimeEvent(RepeatingEvents repeatingEvent) : base(repeatingEvent)
        {

        }

        
    }
    //Fuer Termine, welche nicht einen Termin verschieben (wie KLausuren, Extra Vorlesung etc)
    class NonReferencedOneTimeEvent : NonRepeatingEvents
    {
        public NonReferencedOneTimeEvent(EventLabel lab, String name, int start, int end, DateTime date) : base(lab, name, start, end, date)
        {

        }

    }


    class Lecture : RepeatingEvents
    {

        private String location { get; set; }
        private String additionalInformation { get; set; }

        public Lecture(EventLabel lab,String name, int start, int end, DayOfWeek dayCode, String location, String additonalInformation) : base(lab,name, start, end, dayCode)
        {

        }
    }

    class Task : RepeatingEvents
    {
        private DateTime deadLine;

        
        public Task(EventLabel lab,String name, int start, int end, DayOfWeek dayCode) : base (lab,name, start, end, dayCode)
        {
            
        }

        private void generateDeadLine()
        {
            //how?
        }

    }

    

    
}
