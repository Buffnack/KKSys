using System;
using System.Collections.Generic;


namespace KKSysForms_CardResultTable
{
  
    //Rework
    class CardStack
    {
        public LinkedList<KKSysForms_CardModel.ContentCard> ContentList
        { get
            {
                return ContentList;
            }
          set
            {
                if (ContentList == null)
                {
                    ContentList = value;
                }
                else
                {
                    foreach (KKSysForms_CardModel.ContentCard card in value)
                    {
                        ContentList.AddLast(card);
                    }
                }
            }
        }

        public LinkedList<KKSysForms_CardModel.QACard> QAList
        {
            get { return QAList; }
            set
            {
                if (QAList == null)
                {
                    QAList = value;
                }
                else
                {
                    foreach (KKSysForms_CardModel.QACard card in value)
                    {
                        QAList.AddLast(card);
                    }
                }
            }
        }

        

        public CardStack()
        {
           
        }



        public List<KKSysForms_CardModel.Card> GetCardPack()
        {
            List<KKSysForms_CardModel.Card> returnList = new List<KKSysForms_CardModel.Card>();
            foreach (KKSysForms_CardModel.ContentCard card in this.ContentList)
            {
                returnList.Add(card);
            }
            foreach (KKSysForms_CardModel.QACard card in this.QAList)
            {
                returnList.Add(card);
            }

            return returnList;

        }
        

        
    }
}
