using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKSysForms_Exceptions
{
    abstract class KKSysExceptions : Exception
    {
        public KKSysExceptions(String msg) : base(msg)
        {

        }
    }

    class SQL_DatabaseExistsException : KKSysExceptions
    {
        private const String message = "Database-Elements already exists";

        public SQL_DatabaseExistsException() : base(message)
        {

        }
    }

    


}
