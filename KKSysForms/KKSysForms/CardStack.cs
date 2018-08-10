using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKSysForms_CardResultTable
{
    class CardStack
    {
        private KKSysDatabase.DatabaseConnector databaseConnector;
        private LinkedList<KKSysForms_CardModel.ContentCard> contentList;

        private LinkedList<KKSysForms_CardModel.QACard> qaList;

        private String sqlQuery;

        private bool notFullyInDatabase;

        private bool created;

        public CardStack(String sqlQuere)
        {
            //Handle DB implementation requiered
            try
            {

                this.sqlQuery = sqlQuere;
            }
            catch (Exception e)
            {
                
            }
            
        }

        public CardStack()
        {
            this.created = true;
        }

        public void addCard(KKSysForms_CardModel.Card card)
        {
            
        }

        public void removeCard(KKSysForms_CardModel.Card remove)
        {

        }

        public void replaceCard(KKSysForms_CardModel.Card target, KKSysForms_CardModel.Card replacement)
        {

        }

        //False meanns, the transfer failed
        public bool TransferIntoDataBase()
        {
            foreach (KKSysForms_CardModel.ContentCard card in this.contentList)
            {

            }
            foreach (KKSysForms_CardModel.QACard card in this.qaList)
            {

            }
            return false;
        }

        
    }
}
