using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


//Std value for start end
//TODO: ANstehende Events sollen eine View bekommen in der Database
namespace KKSysForms_Event
{
    internal enum DayCode { Mon, Di, Mi, Do, Fr, Sa, So }

    [Serializable]
    class TimeStamp 
    {
        public int h { get; }

        public int m { get; }

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

        public static TimeStamp generateTime(TimeStamp time, int durationInMinutes)
        {
            int newMinutes = time.m + durationInMinutes;
            int newHours = time.h;

            while(newMinutes >= 60)
            {
                newHours++;
                newMinutes = newMinutes - 60;
            }

            while(newMinutes < 0)
            {
                newHours--;
                newMinutes = newMinutes + 60;
            }

            if(newHours < 0 || newHours > 23)
            {
                throw new Exception("Neue Zeit liegt an einem anderen Tag.");
            }

            TimeStamp newTime = new TimeStamp(newHours, newMinutes);

            return newTime;

        }

        public bool Equals(TimeStamp timeStamp)
        {
            if (this.h == timeStamp.h)
            {
                if (this.m == timeStamp.m)
                {
                    return true;
                }
                return false;
            }
            return false;
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
        [NonSerialized]
        public int serialID;

        public TimeStamp Start { get; set; }

        public TimeStamp End { get; set; }

        public String Name { get; set; }

        [NonSerialized]
        protected bool DeadLine;

        [NonSerialized]
        protected bool modified;

        [NonSerialized]
        protected bool created;

        //Constructor for Creation and Deserialisation
        public Event(String name, TimeStamp start, TimeStamp end)
        {

            if (start.Equals(end))
            {
                this.DeadLine = true;
            }

            this.Name = name;
            this.Start = start;
            this.End = end;
            this.created = true;
        }
       

        //Datenbank schutz
        public void SetModified()
        {
            if (!created)
            {
                this.modified = true;
            }
            else
            {
                this.modified = false;
            }
        }

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);

    }

   

    abstract class NonRepeatingEvents : Event
    {

        protected DateTime date;

        //Creator and Deserialization
        public NonRepeatingEvents(String name, TimeStamp start, TimeStamp end, DateTime date) : base(name, start, end)
        {
            this.date = date;
        }

    }
    //Fuer Termine, die mal verschoben werden (mit Ausfall)
    [Serializable]
    class ReferencedOneTimeEvent : NonRepeatingEvents
    {
        //TODO: Datatype
        private RepeatEvent reference;

        public ReferencedOneTimeEvent(RepeatEvent reference, TimeStamp start, TimeStamp end, DateTime date) : base(reference.Name, start, end, date)
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
        public NonReferencedOneTimeEvent(EventLabel lab, String name, TimeStamp start, TimeStamp end, DateTime date) : base(name, start, end, date)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    class RepeatEvent : Event
    {
        //Filter-Option
        [NonSerialized]
        public DayOfWeek dayCode;


        private String location { get; set; }

        private String additionalInformation { get; set; }

        public RepeatEvent(String name, TimeStamp start, TimeStamp end, DayOfWeek dayCode, String location, String additonalInformation) : base(name, start, end)
        {
            
            this.dayCode = dayCode;
            
            this.location = location;
            this.additionalInformation = additionalInformation;
            
            
        }

        public RepeatEvent(SerializationInfo info, StreamingContext streamingContext):base((String)info.GetValue("Name",typeof(string)),
            new TimeStamp(
                (int)info.GetValue("StartHour",
                typeof(int)),(int)info.GetValue("StartMin", typeof(int))),
            new TimeStamp((int)info.GetValue("EndHour",typeof(int)),
                (int)info.GetValue("EndMin",typeof(int))))
        {
            this.location = (String)info.GetValue("LocationString", typeof(string));
            this.additionalInformation = (String)info.GetString("AddtionNal");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("StartMin", this.Start.m);
            info.AddValue("StartHour", this.Start.h);
            info.AddValue("EndMin", this.End.m);
            info.AddValue("EndHour", this.End.h);
            //This class based shit

            info.AddValue("LocationString", this.location);
            info.AddValue("AdditionNal", this.additionalInformation);



        }
    }

   

    

    
}
