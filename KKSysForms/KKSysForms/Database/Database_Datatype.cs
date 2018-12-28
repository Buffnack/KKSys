using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKSysForms.Database
{
    public class Parse_Datatype
    {
        //Get the Value 
        protected ValueType Value
        {
            get {
                if (this.asParameter)
                {
                    return _serialized_object;
                }
                else
                {
                    return this._var;
                }
            }
        }

        //Get the type of the data
        protected TypeCode VarType
        {
            get
            {
                return this._varType;
            }
        }

        protected String GetColName
        {
            get
            {
                return this._colName;
            }
        }
        
        protected Boolean asParameter;
        //If something should be stored as blob
        protected byte[] _serialized_object;

        //If there is no byte[] object, this should be here
        protected TypeCode _varType;
        //Contains the value
        protected ValueType _var;

        //Name of Col
        protected String _colName;

        /**
         * Call this Const if fixed value is used
         * 
         * 
         */ 
        public Datatype(String colName, ValueType var)
        {
            this._colName = colName;
            this._var = var;
            _varType = var.GetType();
            this.asParameter = false;
        }

        /**
        * Call this Const if blob value is used
        * 
        * 
        */
        public Pars_Datatype(String colName, byte[] serializedObject)
        {
            this._colName = colName;
            this._serialized_object = serializedObject;
            this.asParameter = true;
           // this._varType = serializedObject.GetType();
        }

        public String GenString()
        {

            if (this.VarType == TypeCode.String)
            {
                return this._colName + " = '" + this.Value + "'";
            }
            else
            {
                return this._colName + " = " + this.Value;
            }
        }

        public byte[] GetParam()
        {
            return _serialized_object;
        }

    }


    
}
