using System;
//Using for Unicode for Mathmode
using System.Text;
//Using for Graphics
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;

//Rebuild this into Composite
//TODO: Overloaed berechnung
//Backpack
//TODO: Geneuere Implementierung
namespace KKSysForms_DataTypes
{
    //Abstract Class for the COmposite
    abstract class Datatype
    {

        public Datatype()
        {

        }

        //What kind of operation they need?
        public abstract String ToTex();


    }
    //Elemntary Text Datatype equals to String
    class Text : Datatype
    {
        private String content;

        public Text(String content)
        {
            this.content = content;
        }
        public override String ToTex()
        {
            return content;
        }
    }

    //Not implemented
    class Graphics : Datatype
    {
        private Image content;

        public Graphics(Image content)
        {
            this.content = content;

        }

        //This method should save the Image into a specific folder with the tex
        public override String ToTex()
        {
            throw new NotImplementedException();
        }
    }

    //Not implemented, because its not sure how we gonna store that
    class MathMode : Datatype
    {
        public override String ToTex()
        {
            throw new NotImplementedException();
        }
    }

    class CompositeDatatype : Datatype
    {
        private List<Datatype> components;

        public CompositeDatatype()
        {
            components = new List<Datatype>();
        }

        public void AddComponent(Datatype elem)
        {
            this.components.Add(elem);
        }

        public override String ToTex()
        {
            String tmp = "";
            foreach (Datatype data in components)
            {
                tmp = tmp +data.ToTex();
            }

            return tmp;
        }

        public void RemoveComponent(Datatype elem)
        {
            this.components.Remove(elem);
        }

        public void SetComponents(List<Datatype> components)
        {
            this.components = components;
        }



    }

    

  

}
