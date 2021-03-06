﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KKSysForms_CardModel;


//Std value for start end
//TODO: ANstehende Events sollen eine View bekommen in der Database
//TODO: Nutze IDatabase
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
    //Sortieralgorithmus -> Alphabetische Abfolge
    class EventLabel : KKSysForms_Interfaces.DatabaseMark
    {

        private String _name;

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (ICreated)
                {
                    _name = value;
                }
                else
                {
                    IModified = true;
                    _name = value;
                }

            }
        }

        private List<Event> eventsUnderLabel;
        private List<Theme> themeUnderLabel;

        public EventLabel(String label, bool fromDatabase)
        {
            
            this.eventsUnderLabel = new List<Event>();
            this.themeUnderLabel = new List<Theme>();
            this.ICreated = !fromDatabase;
            Name = label;

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

        public List<Theme> getThemeList()
        {
            return this.themeUnderLabel;
        }

        public override String ToString()
        {
            return this._name;
        }

        
    }

    [Serializable]
    abstract class Event : KKSysForms_Interfaces.DatabaseMark, ISerializable
    {
        //Required to update specific Event if modfied is set
        

        public TimeStamp Start {
            get
            {
                return Start;
            }
            set
            {
                Start = value;
                if (!ICreated && IDatabaseID != 0)
                {
                    IModified = true;
                }
            } }

        public TimeStamp End
        {
            get
            {
                return End;
            }
            set
            {
                End = value;
                if (!ICreated && IDatabaseID != 0)
                {
                    IModified = true;
                }
            }
        }

        public String Name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
                if (!ICreated && IDatabaseID != 0)
                {
                    IModified = true;
                }
            }
        }

        [NonSerialized]
        protected bool DeadLine;

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

            
        }
       

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);

    }

   

    abstract class NonRepeatingEvents : Event
    {
        //Filter
        [NonSerialized]
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
        //Reference wird aus der Datenbank geladen
        [NonSerialized]
        private RepeatEvent reference;

        public ReferencedOneTimeEvent(RepeatEvent reference, TimeStamp start, TimeStamp end, DateTime date) : base(reference.Name, start, end, date)
        {
            this.reference = reference;
            this.ICreated = true;
        }

        public ReferencedOneTimeEvent(SerializationInfo info, StreamingContext context)
            : base((String)info.GetValue("Name", typeof(string)),
            new TimeStamp(
                (int)info.GetValue("StartHour",
                typeof(int)), (int)info.GetValue("StartMin", typeof(int))),
            new TimeStamp((int)info.GetValue("EndHour", typeof(int)),
                (int)info.GetValue("EndMin", typeof(int))),
            (DateTime)info.GetDateTime("Date")
             )
        {
            this.ICreated = false;

        }

        

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
            info.AddValue("StartMin", this.Start.m);
            info.AddValue("StartHour", this.Start.h);
            info.AddValue("EndMin", this.End.m);
            info.AddValue("EndHour", this.End.h);
            info.AddValue("Date", this.date);
        }
    }
    //Fuer Termine, welche nicht einen Termin verschieben (wie KLausuren, Extra Vorlesung etc)
    class NonReferencedOneTimeEvent : NonRepeatingEvents
    {
        public NonReferencedOneTimeEvent(String name, TimeStamp start, TimeStamp end, DateTime date) : base(name, start, end, date)
        {
            this.ICreated = true;
        }

        public NonReferencedOneTimeEvent(SerializationInfo info, StreamingContext streamingContext)
            :base((String)info.GetValue("Name", typeof(string)),
            new TimeStamp(
                (int)info.GetValue("StartHour",
                typeof(int)), (int)info.GetValue("StartMin", typeof(int))),
            new TimeStamp((int)info.GetValue("EndHour", typeof(int)),
                (int)info.GetValue("EndMin", typeof(int))),
            (DateTime)info.GetDateTime("Date"))

        {
            this.ICreated = false;
        }
        


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("StartMin", this.Start.m);
            info.AddValue("StartHour", this.Start.h);
            info.AddValue("EndMin", this.End.m);
            info.AddValue("EndHour", this.End.h);
            info.AddValue("Date", this.date);
        }
    }

    [Serializable]
    class RepeatEvent : Event
    {
        //Filter-Option
        [NonSerialized]
        public List<DayOfWeek> dayCode;


        private String location { get; set; }

        private String additionalInformation { get; set; }

        public RepeatEvent(String name, TimeStamp start, TimeStamp end, List<DayOfWeek> dayCode, String location, String additonalInformation) : base(name, start, end)
        {

            this.dayCode = dayCode;
            
            this.location = location;
            this.additionalInformation = additionalInformation;
            this.ICreated = true;
            
            
        }

        public RepeatEvent(SerializationInfo info, StreamingContext streamingContext)
            :base((String)info.GetValue("Name",typeof(string)),
            new TimeStamp(
                (int)info.GetValue("StartHour",
                typeof(int)),(int)info.GetValue("StartMin", typeof(int))),
            new TimeStamp((int)info.GetValue("EndHour",typeof(int)),
                (int)info.GetValue("EndMin",typeof(int))))
        {
            this.location = (String)info.GetValue("LocationString", typeof(string));
            this.additionalInformation = (String)info.GetString("AdditionNal");
            this.ICreated = false;
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
