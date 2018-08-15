using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


//Std value for start end
//TODO: ANstehende Events sollen eine View bekommen in der Database
namespace KKSysForms_Event
{
    internal enum DayCode { Mon, Di, Mi, Do, Fr, Sa, So }

    class TimeStamp
    {
        private int h { get; }

        private int m { get; }

        public TimeStamp(int h, int m)
        {
            if (h < 0 || h > 23 || m < 0 || m > 59)
            {
                throw new Exception("Das sind keine gueltigen Werte");
            }
            else
            {
                this.h = h;
                this.m = m;
            }
        }

        public void generateTime(TimeStamp time, int durationInMinutes)
        {

        }

    }


    //Enthaelt Information ueber das Label mehrere Veranstaltungen
    class EventLabel
    {
        public String Name { get; }

        private List<Event> eventsUnderLabel;
        

        public EventLabel(String label)
        {
            Name = label;
            this.eventsUnderLabel = new List<Event>();
            
        }

        public void addEvent(Event @event)
        {
            this.eventsUnderLabel.Add(@event);
        }

        public void removeEvent(Event @event)
        {
            this.eventsUnderLabel.Remove(@event);
        }

        public List<Event> getEventList()
        {
            return this.eventsUnderLabel;
        }

        
    }
    [Serializable]
    abstract class Event : ISerializable
    {

        public int Start { get; set; }

        public int End { get; set; }

        public String Name { get; set; }

        [NonSerialized]
        public EventLabel EventLabel;

        [NonSerialized]
        protected bool modified;

        [NonSerialized]
        protected bool created;

        //Constructor for Creation
        public Event(EventLabel label, String name, int start, int end)
        {

            this.EventLabel = label;
            this.Name = name;
            this.Start = start;
            this.End = end;
            this.created = true;
        }
        //For Deserialisation
        public Event(String name, int start, int end)
        {
            this.Name = name;
            this.Start = start;
            this.End = end;
        }

        public void setModified()
        {
            if (created)
            {
                return;
            }
            else
            {
                this.modified = true;
            }
        }

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);

    }

   

    abstract class NonRepeatingEvents : Event
    {
        
        protected int numOfReplace { get; set; }

        //With Reference
        public NonRepeatingEvents(EventLabel lab, String name, int start, int end, DateTime date) : base(lab, name, start, end)
        {

        }

        public NonRepeatingEvents(String name, int start, int end, DateTime date) : base(name, start, end)
        {

        }

    }
    //Fuer Termine, die mal verschoben werden (mit Ausfall)
    class ReferencedOneTimeEvent : NonRepeatingEvents
    {
        //TODO: Datatype
        private Event reference;

        public ReferencedOneTimeEvent(Event reference, int start, int end, DateTime date) : base(reference.EventLabel, reference.Name, start, end, date)
        {

        }

        

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
    //Fuer Termine, welche nicht einen Termin verschieben (wie KLausuren, Extra Vorlesung etc)
    class NonReferencedOneTimeEvent : NonRepeatingEvents
    {
        public NonReferencedOneTimeEvent(EventLabel lab, String name, int start, int end, DateTime date) : base(lab, name, start, end, date)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }


    class Lecture : Event
    {
        public DayOfWeek dayCode;
        private int Start; //1030
        // Nutzer: Start = 10, dauer 45min --> Ende wurde nicht angegeben, aber es ist eine Lecture, -> Ende = Start ^Dauer
        private String location { get; set; }
        private String additionalInformation { get; set; }

        public Lecture(EventLabel lab,String name, int start, int end, DayOfWeek dayCode, String location, String additonalInformation) : base(lab,name, start, end)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

   

    

    
}
