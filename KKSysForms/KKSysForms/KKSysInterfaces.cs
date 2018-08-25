using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Klasse fuer die Interfaces: Sollte INterface anlegen, wenn mehrere Klassen die gleichen Eigenschaft
//Besitzen sollte, siehe Events, Cards fuer die Datenbank
namespace KKSysForms_Interfaces
{

    abstract class DatabaseMark
    {

        protected Int64 databaseID;

        protected bool created;

        protected bool modified;

        public long IDatabaseID
        {
            get => this.databaseID;
            set => this.databaseID = value;
        }

        public bool ICreated
        {
            get
            {
                return this.created;
            }
            set { this.created = value; }
        }
        public bool IModified
        {
            get => modified;
            set => modified = value;
        }

    }
}
