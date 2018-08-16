using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KKSysForms_DataTypes;

//TODO: Composite pattern
//TODO: Fill this class
namespace KKSysForms_CardModel
{
    abstract class Card
    {
        
    }

    

    class ContentCard : Card
    {
        public ContentCard()
        {

        }
    }

    class QACard : Card
    {
        private String questionHeader;

        private String answerHeader;

        //Maybe Composite?
        private List<DataType> questionContent;

        private List<DataType> answerContent;

        private List<String> tags;

        public QACard(DataType question, DataType answer)
        {
            this.questionContent = new List<DataType>();
            this.questionContent.Add(question);

            this.answerContent = new List<DataType>();
            this.answerContent.Add(answer);

            this.tags = new List<String>();
        }

        public void SetQuestionContent(DataType Content)
        {
            this.questionContent.Add(Content);
        }

        public void SetBackContent(DataType Content)
        {
            this.answerContent.Add(Content);
        }

        public List<String> getTags()
        {
            return this.tags;
        }

        public void addTag(String tag)
        {
            this.tags.Add(tag);
        }

        public void removeTag(String tag)
        {
            this.tags.Remove(tag);
        }

        //this method might be redundant
        public bool hasTag(String tag)
        {
            return this.tags.Contains(tag);
        }

    }
}
