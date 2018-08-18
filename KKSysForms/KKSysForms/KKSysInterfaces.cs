using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Klasse fuer die Interfaces: Sollte INterface anlegen, wenn mehrere Klassen die gleichen Eigenschaft
//Besitzen sollte, siehe Events, Cards fuer die Datenbank
namespace KKSysForms_Interfaces
{
    //Sollten wir implementieren, das alles in der Datanbank unique sein sollte
    interface IKKSysDatabaseInterface
    {
        Int64 IDatabaseID { get; set; }
        bool ICreated { get; set; }
        bool IModified { get; set; }
    }
}
