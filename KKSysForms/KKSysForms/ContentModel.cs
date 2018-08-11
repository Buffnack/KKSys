using System;
//Using for Unicode for Mathmode
using System.Text;
//Using for Graphics
using System.Drawing;
using System.IO;

using System.Runtime.Serialization;

namespace KKSysForms_DataTypes
{
    
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

        abstract public void DecodeFromByte(byte[] data);
        
    }
    [Serializable]
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

        public void SetContent(String set)
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

        public override void DecodeFromByte(byte[] data)
        {

        }

        
    }
    
    class Graphic : DataType
    {
        
        private Image content;

       
        private bool currentFromFileSystem;

        public Graphic(String pathToGraphic)
        {
            try
            {

                this.content = Image.FromFile(pathToGraphic);
                currentFromFileSystem = true;
            }
            catch (IOException e)  
            {
                content = null;
                throw e;
            }
            
       
        }

       

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
   

            return returnVar;
        }

        public override void DecodeFromByte(byte[] data)
        {
            
             MemoryStream testBack = new MemoryStream(data);
             this.content = Image.FromStream(testBack);
        }
        
        public override String ToString()
        {
            return null;
        }
    }

    //How we gonna do that?(XML file maybe)?
    
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
        public override void DecodeFromByte(byte[] data)
        {
            throw new NotImplementedException();
        }
    }

    

  

}
