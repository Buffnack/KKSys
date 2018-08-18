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

    class Tag : KKSysForms_Interfaces.IKKSysDatabaseInterface
    {
        public long IDatabaseID
        {
            get => IDatabaseID;
            set => IDatabaseID = value;
        }

        public bool ICreated
        {
            get => ICreated;
            set => ICreated = true;
        }
        public bool IModified
        {
            get => IModified;
            set => IModified = value;
        }



        public String TagName
        {
            get { return TagName; }
            set
            {
                TagName = value;
                if (!ICreated && IDatabaseID != 0)
                {
                    this.IModified = true;
                }
            }
        }

        public Tag(String name)
        {
            this.ICreated = true;
            this.TagName = name;

        }

        public Tag(String name, long id)
        {
            this.TagName = name;
            this.IDatabaseID = id;
        }
    }


    class Theme : KKSysForms_Interfaces.IKKSysDatabaseInterface
    {


        public long IDatabaseID
        {
            get => IDatabaseID;
            set => IDatabaseID = value;
        }

        public bool ICreated
        {
            get => ICreated;
            set => ICreated = true;
        }
        public bool IModified
        {
            get => IModified;
            set => IModified = value;
        }

        public String ThemeName
        {
            get
            {
                return ThemeName;
            }
            set
            {
                ThemeName = value;
                if (!ICreated && IDatabaseID != 0)
                {
                    this.IModified = true;
                }
            }
        }

        private List<ContentCard> contentList;

        private List<QACard> qAList;

        public bool hasLabel { get; set; }

        public Theme(String name)
        {
            ICreated = true;
            this.ThemeName = name;
            this.contentList = new List<ContentCard>();
            this.qAList = new List<QACard>();


        }


        public void AddCard(ContentCard card)
        {
            this.contentList.Add(card);
        }

        public void AddCard(QACard card)
        {
            this.qAList.Add(card);
        }

        public List<ContentCard> GetContent()
        {
            return this.contentList;
        }

        public List<QACard> GetQA()
        {
            return this.qAList;
        }
    }

    //Serialisierung der anderen Objekte als Byte[] objekt - sprich erst umwandlung in byte, dann
    //Speicherung in SerializazionInfo
    abstract class Card : KKSysForms_Interfaces.IKKSysDatabaseInterface, ISerializable
    {
        public long IDatabaseID
        {
            get => IDatabaseID;
            set => IDatabaseID = value;
        }

        public bool ICreated
        {
            get => ICreated;
            set => ICreated = true;
        }
        public bool IModified
        {
            get => IModified;
            set => IModified = value;
        }

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
