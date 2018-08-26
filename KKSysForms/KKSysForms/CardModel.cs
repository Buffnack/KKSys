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
    //TODO: Implement ContentCard
    class Tag : KKSysForms_Interfaces.DatabaseMark
    {
       



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


    class Theme : KKSysForms_Interfaces.DatabaseMark
    {

        private String name;
      

        public String ThemeName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (!ICreated && IDatabaseID != 0)
                {
                    this.IModified = true;
                }
            }
        }

        private List<ContentCard> contentList;

        private List<QACard> qAList;

        

       

        public Theme(String name, bool fromDatabase)
        {
            ICreated = !fromDatabase;
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

        public override string ToString()
        {
            return this.name;
        }
    }

    //Serialisierung der anderen Objekte als Byte[] objekt - sprich erst umwandlung in byte, dann
    //Speicherung in SerializazionInfo
    abstract class Card : KKSysForms_Interfaces.DatabaseMark, ISerializable
    {
       

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }


    [Serializable]
    class ContentCard : Card
    {
        public String header;

        public CompositeDatatype content { get; set; }

        //Currently not implemented
        private List<Tag> tags { get; }

        public ContentCard(String header,  CompositeDatatype content)
        {
            this.header = header;
            this.content = content;
            
        }

        [Obsolete]
        public void addTag(Tag tag)
        {
            this.tags.Add(tag);
        }
        [Obsolete]
        public void removeTag(Tag tag)
        {
            this.tags.Remove(tag);
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
        private String qHead, aHead;

        private CompositeDatatype qContent, aContent;

        //Do We need this?
        public String QuestionHeader
        {
            get
            {
                return this.qHead;
            }
            set
            {
                this.qHead = value;
            }
        }

        //DO we need this?
        public String AnswerHeader
        {
            get
            {
                return this.aHead;
            }
            set
            {
                this.aHead = value;
            }
        }

        public CompositeDatatype QuestionContent
        {
            get
            {
                return this.qContent;
            }
            set
            {
                this.qContent = value;
            }
        }

        public CompositeDatatype AnswerContent
        {
            get
            {
                return this.aContent;
            }
            set
            {
                this.aContent = value;
            }
        }

        private List<Tag> tags { get; }

        public QACard(String questHead, String ansHead, CompositeDatatype question, CompositeDatatype answer)
        {
            this.ICreated = true;
            this.QuestionHeader = questHead;
            this.AnswerHeader = ansHead;
            this.QuestionContent = question;
            this.AnswerContent = answer;
            this.tags = new List<Tag>();
        }

        public QACard(SerializationInfo info, StreamingContext context)
        {
            
            this.QuestionHeader = info.GetString("qHead");
            this.QuestionContent =(CompositeDatatype) info.GetValue("qCont", typeof(Datatype));
            this.AnswerHeader = info.GetString("aHead");
            this.AnswerContent = (CompositeDatatype)info.GetValue("aCont", typeof(Datatype));
            
        }

        public void SetQuestionContent(Datatype Content)
        {
            this.QuestionContent.AddComponent(Content);
        }

        public void SetAnswerContent(Datatype Content)
        {
            this.AnswerContent.AddComponent(Content);
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
            info.AddValue("qHead", this.QuestionHeader);
            info.AddValue("qCont", this.QuestionContent);
            info.AddValue("aHead", this.AnswerHeader);
            info.AddValue("aCont", this.AnswerContent);
        }
    }
}
