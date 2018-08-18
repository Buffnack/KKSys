using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using KKSysForms_DataTypes;
using KKSysForms_Filter;

//TODO: Composite pattern
//TODO: Fill this class
namespace KKSysForms_CardModel
{
    //Serialisierung der anderen Objekte als Byte[] objekt - sprich erst umwandlung in byte, dann
    //Speicherung in SerializazionInfo
    abstract class Card : KKSysForms_Interfaces.IKKSysDatabaseInterface, ISerializable
    {
        public long IDatabaseID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ICreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IModified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }


    [Serializable]
    class ContentCard : Card
    {
        public ContentCard()
        {

        }

        public ContentCard(SerializationInfo info, StreamingContext context)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    class QACard : Card
    {
        
        private String questionHeader;


        private String answerHeader;

        private CompositeDatatype questionContent;

        private CompositeDatatype answerContent;

        [NonSerialized]
        private List<Tag> tags;

        public QACard(CompositeDatatype question, CompositeDatatype answer)
        {

            this.questionContent = question;
            this.answerContent = answer;
            this.tags = new List<Tag>();
        }

        public QACard(SerializationInfo info, StreamingContext context)
        {

        }

        public void SetQuestionContent(Datatype Content)
        {
            this.questionContent.AddComponent(Content);
        }

        public void SetBackContent(Datatype Content)
        {
            this.answerContent.AddComponent(Content);
        }

        public List<Tag> getTags()
        {
            return this.tags;
        }

        public void addTag(Tag tag)
        {
            this.tags.Add(tag);
        }

        public void removeTag(Tag tag)
        {
            this.tags.Remove(tag);
        }

        //this method might be redundant
        public bool hasTag(Tag tag)
        {
            return this.tags.Contains(tag);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
