using System;
//Using for Unicode for Mathmode
using System.Text;
//Using for Graphics
using System.Drawing;
using System.IO;

using System.Runtime.Serialization;

namespace KKSysForms_DataTypes
{
    [Serializable]
    abstract class DataType
    {

        
        //Std Const.
        public DataType()
        {

        }
        //General method declaration for String method
        abstract public override String ToString() ;
        //Object to Byte for database
        abstract public byte[] EncodeToByte();
        
    }

    class Text : DataType
    {
        private String content;

        public Text(String content)
        {     
            this.content = content;

        }

        public String GetContent()
        {
            return content;
        }

        public void SentContent(String set)
        {
            this.content = set;
        }
        public override String ToString()
        {
            return content;
        }

        public override byte[] EncodeToByte()
        {
            byte[] returnValue;
           
            UnicodeEncoding encoding = new UnicodeEncoding();
            returnValue = encoding.GetBytes(this.content);

            
            
            return returnValue;
        }
    }

    class Graphic : DataType
    {

        private Image content;

        private bool currentFromFileSystem;

        public override byte[] EncodeToByte()
        {
            byte[] returnVar;
            MemoryStream stream = new MemoryStream();
            try
            {
                this.content.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                returnVar = stream.ToArray();
                stream.Dispose();

            }
            catch (Exception e)
            {
                
                return null;

            }
            //Back to Image
           // MemoryStream testBack = new MemoryStream(returnVar);
           // Image test = Image.FromStream(testBack);

            return returnVar;
        }
        //This Method needs a temporary File to store the Image, create .Tex and compile this text
        //Means: This Methods return for Graphics a path
        public override String ToString()
        {
            return "";
        }
    }

    //Requires Numeric class with String in texFormats
    
    class Formula : DataType
    {
        public override String ToString()
        {
            return "";
        }
        //Not implemented
        public override byte[] EncodeToByte()
        {
            throw new NotImplementedException();
        }
    }

    

  

}
