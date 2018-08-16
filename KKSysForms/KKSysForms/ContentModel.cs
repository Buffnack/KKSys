using System;
//Using for Unicode for Mathmode
using System.Text;
//Using for Graphics
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;

//Rebuild this into Composite
namespace KKSysForms_DataTypes
{

    abstract class Datatype
    {

        public Datatype()
        {

        }

        //What kind of operation they need?
        public abstract void Draw();


    }

    class Text : Datatype
    {
        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }

    class Graphics : Datatype
    {
        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }

    class MathMode : Datatype
    {
        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }

    class CompositeDatatype : Datatype
    {
        private List<Datatype> components;

        public CompositeDatatype()
        {

        }

        public void AddComponent(Datatype elem)
        {
            this.components.Add(elem);
        }

        public override void Draw()
        {
            throw new NotImplementedException();
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
