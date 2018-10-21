using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KKSysForms
{
    
    class PaneDefinition
    {
       
    }

    abstract class CardManagePanel : Panel
    {

    }

    class CardCreatorPanel : CardManagePanel
    {



        //GUi attributes
        private Label currentCreatedCardsLabel;
        private Button createPdfBtn;
        private Button button6_bot_large;
        private Label currentLoadedCards;
        //Stores a value with the number of cards
        private int numberOfCards = 0;

        private Button killCreatedCardsBtn;
        private Button chooseExistingCardsBtn;
        private Button addCardBtn;
        private Label label6_left_southeast;
        private Label label5_left_northeast;
        private Label label4_left_southwest;
        private Label label_left_northwest;
        private Label answerContentLabel;
        private Label questionContentLabe;
        private RichTextBox answerContentInputField;
        private ListBox currentLoadedAnswerContentList;
        private RichTextBox questionContentInputField;
        private ListBox currentLoadedQuestionContentList;
        private TreeView ExistingLabelAndThemeDatatree;
        private Button addNewThemeBtn;
        private Button addNewLabelBtn;

        public CardCreatorPanel()
        {

          //  this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
          //  this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
          //  this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
          //  this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            


            //GUI Content
            this.currentCreatedCardsLabel = new System.Windows.Forms.Label();
            this.createPdfBtn = new System.Windows.Forms.Button();
            this.button6_bot_large = new System.Windows.Forms.Button();
            this.currentLoadedCards = new System.Windows.Forms.Label();
            this.killCreatedCardsBtn = new System.Windows.Forms.Button();
            this.chooseExistingCardsBtn = new System.Windows.Forms.Button();
            this.addCardBtn = new System.Windows.Forms.Button();
            this.label6_left_southeast = new System.Windows.Forms.Label();
            this.label5_left_northeast = new System.Windows.Forms.Label();
            this.label4_left_southwest = new System.Windows.Forms.Label();
            this.label_left_northwest = new System.Windows.Forms.Label();
            this.answerContentLabel = new System.Windows.Forms.Label();
            this.questionContentLabe = new System.Windows.Forms.Label();
            this.answerContentInputField = new System.Windows.Forms.RichTextBox();
            this.currentLoadedAnswerContentList = new System.Windows.Forms.ListBox();
            this.questionContentInputField = new System.Windows.Forms.RichTextBox();
            this.currentLoadedQuestionContentList = new System.Windows.Forms.ListBox();
            this.ExistingLabelAndThemeDatatree = new System.Windows.Forms.TreeView();
            this.addNewThemeBtn = new System.Windows.Forms.Button();
            this.addNewLabelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenuItem1
            // 
           // this.toolStripMenuItem1.Name = "toolStripMenuItem1";
           // this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem2
            // 
           // this.toolStripMenuItem2.Name = "toolStripMenuItem2";
           // this.toolStripMenuItem2.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem3
            // 
           // this.toolStripMenuItem3.Name = "toolStripMenuItem3";
           // this.toolStripMenuItem3.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem4
            // 
         //   this.toolStripMenuItem4.Name = "toolStripMenuItem4";
          //  this.toolStripMenuItem4.Size = new System.Drawing.Size(32, 19);
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.currentCreatedCardsLabel);
            this.Controls.Add(this.createPdfBtn);
            this.Controls.Add(this.button6_bot_large);
            this.Controls.Add(this.currentLoadedCards);
            this.Controls.Add(this.killCreatedCardsBtn);
            this.Controls.Add(this.chooseExistingCardsBtn);
            this.Controls.Add(this.addCardBtn);
            this.Controls.Add(this.label6_left_southeast);
            this.Controls.Add(this.label5_left_northeast);
            this.Controls.Add(this.label4_left_southwest);
            this.Controls.Add(this.label_left_northwest);
            this.Controls.Add(this.answerContentLabel);
            this.Controls.Add(this.questionContentLabe);
            this.Controls.Add(this.answerContentInputField);
            this.Controls.Add(this.currentLoadedAnswerContentList);
            this.Controls.Add(this.questionContentInputField);
            this.Controls.Add(this.currentLoadedQuestionContentList);
            this.Controls.Add(this.ExistingLabelAndThemeDatatree);
            this.Controls.Add(this.addNewThemeBtn);
            this.Controls.Add(this.addNewLabelBtn);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "CardCreatorPane";
            this.Size = new System.Drawing.Size(1489, 935);
            this.TabIndex = 1;
            // 
            // label8_belowFields
            // 
            this.currentCreatedCardsLabel.AutoSize = true;
            this.currentCreatedCardsLabel.Location = new System.Drawing.Point(809, 294);
            this.currentCreatedCardsLabel.Name = "label8_belowFields";
            this.currentCreatedCardsLabel.Size = new System.Drawing.Size(70, 25);
            this.currentCreatedCardsLabel.TabIndex = 22;
            this.currentCreatedCardsLabel.Text = "Current created cards";
            // 
            // createPdfBtn
            // 
            this.createPdfBtn.Location = new System.Drawing.Point(1267, 513);
            this.createPdfBtn.Name = "button7_left_midtop";
            this.createPdfBtn.Size = new System.Drawing.Size(210, 44);
            this.createPdfBtn.TabIndex = 21;
            this.createPdfBtn.Text = "Create PDF";
            this.createPdfBtn.Click += OnClickCreatePdf;
            this.createPdfBtn.UseVisualStyleBackColor = true;

           
            // 
            // button6_bot_large
            // 
            this.button6_bot_large.Location = new System.Drawing.Point(475, 871);
            this.button6_bot_large.Name = "button6_bot_large";
            this.button6_bot_large.Size = new System.Drawing.Size(767, 52);
            this.button6_bot_large.TabIndex = 20;
            this.button6_bot_large.Text = "button6";
            this.button6_bot_large.UseVisualStyleBackColor = true;
            // 
            // currentLoadedCards
            // 
            this.currentLoadedCards.AutoSize = true;
            this.currentLoadedCards.Location = new System.Drawing.Point(1335, 456);
            this.currentLoadedCards.Name = "label_below_butt3";
            this.currentLoadedCards.Size = new System.Drawing.Size(70, 25);
            this.currentLoadedCards.TabIndex = 19;
            this.currentLoadedCards.Text = "label7";
            // 
            // killCreatedCardsBtn
            // 
            this.killCreatedCardsBtn.Location = new System.Drawing.Point(1267, 783);
            this.killCreatedCardsBtn.Name = "button5_leftbot";
            this.killCreatedCardsBtn.Size = new System.Drawing.Size(210, 55);
            this.killCreatedCardsBtn.TabIndex = 18;
            this.killCreatedCardsBtn.Text = "button5";
            this.killCreatedCardsBtn.UseVisualStyleBackColor = true;
            this.killCreatedCardsBtn.Click += OnClickKillCards;
            // 
            // chooseExistingCardsBtn
            // 
            this.chooseExistingCardsBtn.Location = new System.Drawing.Point(1267, 706);
            this.chooseExistingCardsBtn.Name = "button_left_midbot";
            this.chooseExistingCardsBtn.Size = new System.Drawing.Size(210, 57);
            this.chooseExistingCardsBtn.TabIndex = 17;
            this.chooseExistingCardsBtn.Text = "button4";
            this.chooseExistingCardsBtn.UseVisualStyleBackColor = true;
            this.chooseExistingCardsBtn.Click += OnClickChooseExistingCards;
            // 
            // button3_left_top
            // 
            this.addCardBtn.Location = new System.Drawing.Point(1267, 379);
            this.addCardBtn.Name = "addCardBtn";
            this.addCardBtn.Size = new System.Drawing.Size(210, 53);
            this.addCardBtn.TabIndex = 16;
            this.addCardBtn.Text = "Add Card";
            this.addCardBtn.UseVisualStyleBackColor = true;
            this.addCardBtn.Click += OnClickAddCard;
            // 
            // label6_left_southeast
            // 
            this.label6_left_southeast.AutoSize = true;
            this.label6_left_southeast.Location = new System.Drawing.Point(1368, 169);
            this.label6_left_southeast.Name = "label6_left_southeast";
            this.label6_left_southeast.Size = new System.Drawing.Size(70, 25);
            this.label6_left_southeast.TabIndex = 15;
            this.label6_left_southeast.Text = "label6";
            // 
            // label5_left_northeast
            // 
            this.label5_left_northeast.AutoSize = true;
            this.label5_left_northeast.Location = new System.Drawing.Point(1368, 112);
            this.label5_left_northeast.Name = "label5_left_northeast";
            this.label5_left_northeast.Size = new System.Drawing.Size(70, 25);
            this.label5_left_northeast.TabIndex = 14;
            this.label5_left_northeast.Text = "label5";
            // 
            // label4_left_southwest
            // 
            this.label4_left_southwest.AutoSize = true;
            this.label4_left_southwest.Location = new System.Drawing.Point(1272, 169);
            this.label4_left_southwest.Name = "label4_left_southwest";
            this.label4_left_southwest.Size = new System.Drawing.Size(70, 25);
            this.label4_left_southwest.TabIndex = 13;
            this.label4_left_southwest.Text = "label4";
            // 
            // label_left_northwest
            // 
            this.label_left_northwest.AutoSize = true;
            this.label_left_northwest.Location = new System.Drawing.Point(1272, 112);
            this.label_left_northwest.Name = "label_left_northwest";
            this.label_left_northwest.Size = new System.Drawing.Size(70, 25);
            this.label_left_northwest.TabIndex = 12;
            this.label_left_northwest.Text = "label3";
            // 
            // answerContentLabel
            // 
            this.answerContentLabel.AutoSize = true;
            this.answerContentLabel.Location = new System.Drawing.Point(996, 51);
            this.answerContentLabel.Name = "label2_headeroffield_east";
            this.answerContentLabel.Size = new System.Drawing.Size(70, 25);
            this.answerContentLabel.TabIndex = 11;
            this.answerContentLabel.Text = "Backpage:";
            // 
            // label1_headerOfFieldWest
            // 
            this.questionContentLabe.AutoSize = true;
            this.questionContentLabe.Location = new System.Drawing.Point(489, 51);
            this.questionContentLabe.Name = "label1_headerOfFieldWest";
            this.questionContentLabe.Size = new System.Drawing.Size(70, 25);
            this.questionContentLabe.TabIndex = 10;
            this.questionContentLabe.Text = "Frontpage";
            // 
            // answerContentInputField
            // 
            this.answerContentInputField.Location = new System.Drawing.Point(855, 95);
            this.answerContentInputField.Name = "richTextBox_east";
            this.answerContentInputField.Size = new System.Drawing.Size(387, 170);
            this.answerContentInputField.TabIndex = 9;
            this.answerContentInputField.Text = "AnswerContent";
            // 
            // listBoxWest
            // 
            this.currentLoadedAnswerContentList.FormattingEnabled = true;
            this.currentLoadedAnswerContentList.ItemHeight = 25;
            this.currentLoadedAnswerContentList.Location = new System.Drawing.Point(855, 334);
            this.currentLoadedAnswerContentList.Name = "ListedAnswerContent";
            this.currentLoadedAnswerContentList.Size = new System.Drawing.Size(387, 504);
            this.currentLoadedAnswerContentList.TabIndex = 8;
            // 
            // richTextBox1_west
            // 
            this.questionContentInputField.Location = new System.Drawing.Point(475, 95);
            this.questionContentInputField.Name = "richTextBox1_west";
            this.questionContentInputField.Size = new System.Drawing.Size(353, 170);
            this.questionContentInputField.TabIndex = 7;
            this.questionContentInputField.Text = "QuestionContent";
            // 
            // listWest
            // 
            this.currentLoadedQuestionContentList.FormattingEnabled = true;
            this.currentLoadedQuestionContentList.ItemHeight = 25;
            this.currentLoadedQuestionContentList.Location = new System.Drawing.Point(475, 334);
            this.currentLoadedQuestionContentList.Name = "listWest";
            this.currentLoadedQuestionContentList.Size = new System.Drawing.Size(353, 504);
            this.currentLoadedQuestionContentList.TabIndex = 5;
            // 
            // evlaThemeTree
            //Guess i have to read something TODO
            // 
            this.ExistingLabelAndThemeDatatree.Location = new System.Drawing.Point(33, 23);
            this.ExistingLabelAndThemeDatatree.Name = "evlaThemeTree";
            this.ExistingLabelAndThemeDatatree.Size = new System.Drawing.Size(416, 815);
            this.ExistingLabelAndThemeDatatree.TabIndex = 4;
            
            // 
            // button2_bot
            // 
            this.addNewThemeBtn.Location = new System.Drawing.Point(265, 871);
            this.addNewThemeBtn.Name = "button2_bot";
            this.addNewThemeBtn.Size = new System.Drawing.Size(184, 52);
            this.addNewThemeBtn.TabIndex = 3;
            this.addNewThemeBtn.Text = "Add Theme";
            this.addNewThemeBtn.UseVisualStyleBackColor = true;
            this.addNewThemeBtn.Click += OnClickCreateNewTheme;
            // 
            // button1_bot
            // 
            this.addNewLabelBtn.Location = new System.Drawing.Point(33, 871);
            this.addNewLabelBtn.Name = "button1_bot";
            this.addNewLabelBtn.Size = new System.Drawing.Size(199, 52);
            this.addNewLabelBtn.TabIndex = 2;
            this.addNewLabelBtn.Text = "Create Label";
            this.addNewLabelBtn.UseVisualStyleBackColor = true;
            this.addNewLabelBtn.Click += OnClickCreateNewLabel;

            

        }

        private void OnClickCreateNewLabel(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnClickCreateNewTheme(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnClickChooseExistingCards(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnClickKillCards(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnClickCreatePdf(object sender, EventArgs e)
        {
            //TOdo!
            Controller.system.CreatePDF();
        }

        private void OnClickAddCard(object sender, EventArgs e)
        {
            Controller.system.CreateCards(this.questionContentInputField.Text, this.answerContentInputField.Text);
            
            this.numberOfCards++;
            this.currentLoadedCards.Text = numberOfCards.ToString();

            this.currentLoadedAnswerContentList.Items.Add(this.answerContentInputField.Text);
            this.currentLoadedQuestionContentList.Items.Add(this.questionContentInputField.Text);
            this.questionContentInputField.Text = "";
            this.answerContentInputField.Text = "";
        }
    }

    class CardEditPanel : CardManagePanel
    {

    }

    class CardListPanel : CardManagePanel
    {

    }

    abstract class EventManagePanel : Panel
    {

    }

    class EventListPanel : EventManagePanel
    {

    }

    class EventCreatorPanel : EventManagePanel
    {

    }

    class EventEditPanel : EventManagePanel
    {

    }

    class MainRootPanel : Panel
    {

        private Panel ListPane;

        private Panel MidPane;

        private Panel AdditonalPane;


    }





    
}
