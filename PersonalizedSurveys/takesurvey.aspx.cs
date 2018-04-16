using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class takesurvey : System.Web.UI.Page
{
    SQLConnector sqlconnector = null;
    Survey survey;
    Layout layout;
    LinkedList<Answer> answers;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if(sqlconnector == null)
        {
            sqlconnector = new SQLConnector();
        }
        int SurveyID = 0;
        if (Request.QueryString["SurveyID"] != null)
        {
            SurveyID = Convert.ToInt32(Request.QueryString["SurveyID"]);
        }
        if(SurveyID != 0)
        {
            survey = sqlconnector.getSurvey(SurveyID);
            layout = sqlconnector.getLayout(survey.getLayoutID());
        }
        
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SurveyPanel.BackColor = ColorTranslator.FromHtml(layout.getBackgroundColor());
        TitleLbl.Text = survey.getTittle();
        byte[] bytes = layout.getLogo().getLogo();
        string strBase64 = Convert.ToBase64String(bytes);
        SurveyLogo.ImageUrl = "data:Image/png;base64," + strBase64;
        if (Session["Message"] != null && Session["Message"].Equals("SurveySubmited"))
        {
            Session["Message"] = null;
            Messagelbl.Visible = true;
            Messagelbl.Text = "Survey Submited Successfully!<br/>Thank you for taking the survey. your responses are greatly appreciated.";
        }
        else if (survey.getActive())
        {
            
            displayQuestions();
        }
        else
        {

        }
    }

    private void displayQuestions()
    {
        foreach(Question q in survey.getQuestions())
        {
            QuestionsPlaceholder.Controls.Add(new Literal()
            {
                Text = "<div class='container col-auto' style='border:2px solid "
                + layout.getBackgroundColor() + ";border-radius:5px;margin:10px 10px 10px 10px;'>"
            });
            QuestionsPlaceholder.Controls.Add(new Literal() { Text = "<hr/>" });

            QuestionsPlaceholder.Controls.Add(new Literal() { Text = "Question " + q.getQuestionNumber() + "<hr/>"});
            QuestionsPlaceholder.Controls.Add(new Literal() { Text = q.getQuestionText() + "<br/>"});

            if (q.getType().Equals("YN"))
            {
                RadioButtonList buttonlist = new RadioButtonList();
                buttonlist.ID = "Question" + q.getQuestionNumber() + "Answer";
                buttonlist.RepeatDirection = RepeatDirection.Horizontal;
                buttonlist.Attributes.Add("style", "padding-top:10px");

                ListItem yesbtn = new ListItem()
                {
                    Text = "Yes",
                    Value = "1"
                };
                buttonlist.Items.Add(yesbtn);

                ListItem nobtn = new ListItem()
                {
                    Text = "No",
                    Value = "0"
                };
                buttonlist.Items.Add(nobtn);
                QuestionsPlaceholder.Controls.Add(buttonlist);
                QuestionsPlaceholder.Controls.Add(new Literal() { Text = "<br/>" });
            }
            else if (q.getType().Equals("SC"))
            {
                RadioButtonList buttonlist = new RadioButtonList();
                buttonlist.ID = "Question" + q.getQuestionNumber() + "Answer";
                buttonlist.RepeatDirection = RepeatDirection.Horizontal;
                buttonlist.RepeatLayout = RepeatLayout.Flow;
                buttonlist.TextAlign = TextAlign.Left;

                for (int i = 1; i <= 10; i++)
                {
                    ListItem radiobtn = new ListItem()
                    {
                        Text = i.ToString(),
                        Value = i.ToString()
                    };
                    radiobtn.Attributes.Add("style", "margin-left:10px");
                    buttonlist.Items.Add(radiobtn);
                }
                QuestionsPlaceholder.Controls.Add(buttonlist);
                QuestionsPlaceholder.Controls.Add(new Literal() { Text = "<br/>" });
            }

            if (q.getCommentBox().Equals("1")){
                TextBox tb = new TextBox()
                {
                    ID = "Question" + q.getQuestionNumber() + "CommentBox",
                    TextMode = TextBoxMode.MultiLine,
                    
                };
                tb.Attributes.Add("style", "width:100%");
                QuestionsPlaceholder.Controls.Add(tb);
            }


            QuestionsPlaceholder.Controls.Add(new Literal() { Text = "</div>" });
        }
    }

    protected void NextBtn_Click(object sender, EventArgs e)
    {

    }

    protected void SumbitBtn_OnClick(object sender, EventArgs e)
    {
        SurveyTaken takensurvey = new SurveyTaken(survey.getSurveyID());
        foreach (Question q in survey.getQuestions())
        {
            string answer = Request.Form["Question" + q.getQuestionNumber() + "Answer"];
            string comment = Request.Form["Question" + q.getQuestionNumber() + "CommentBox"];
            
            takensurvey.addAnswer(new Answer(q.getQuestionID(), comment, answer));
        }
        if (sqlconnector.createSurveyResponse(takensurvey))
        {
            Session["Message"] = "SurveySubmited";
            Response.Redirect(Request.Path + "?SurveyID=" + survey.getSurveyID());
        }
        else
        {
            Messagelbl2.Text = "Something went wrong";
            Messagelbl2.Visible = true;
        }
    }
}