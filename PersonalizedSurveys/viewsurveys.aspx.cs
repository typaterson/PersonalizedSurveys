using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewsurveys : System.Web.UI.Page
{
    SQLConnector sqlconnector;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        sqlconnector = (SQLConnector)Session["SQLConnector"];

        if (Session["IsLogedIn"] == null || !(bool)Session["IsLogedIn"])
        {
            Response.Redirect("./Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        loadControls();
        if(Session["Message"] != null && Session["Message"].Equals("surveydeleted")){
            Messagelbl.Text = "Survey Deleted Successfully";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("green");
            Messagelbl.Visible = true;
        }
        else
        {
            Messagelbl.Visible = false;
        }
    }

    private void loadControls()
    {
        TitleLbl.Text = "View Surveys";
        ViewSurveyMessageLbl.Text = "Click the 'View' button to view survey results";
        ViewSurveyMessageLbl.Visible = true;
        displaySurveys();
    }

    private void displaySurveys()
    {

        LinkedList<Survey> surveys = sqlconnector.getUser().getSurveys();
        SurveysPlaceholder.Controls.Clear();
        bool isactive = false;
        foreach (Survey s in surveys)
        {
            if (s.getActive())
            {
                isactive = true;
            }
        }

        if (!isactive) { SurveysPlaceholder.Controls.Add(new Literal() { Text = "There are not active surveys." }); }
        else
        {

            foreach (Survey s in surveys)
            {
                if (s.getActive())
                {

                }

                this.SurveysPlaceholder.Controls.Add(new Literal
                {
                    Text = "<div class='container - fluid' style='border:solid 2px "
                    + "#0094ff;border-radius:5px;padding-top:5px;padding-bottom:10px;margin-top:10px;margin-bottom:10px;'>"
                });
                SurveysPlaceholder.Controls.Add(new Literal { Text = "<div class='row'>" });
                SurveysPlaceholder.Controls.Add(new Literal { Text = "<div class='col-12'" });

                Label l = new Label();
                l.Text = s.getTittle();
                SurveysPlaceholder.Controls.Add(l);

                SurveysPlaceholder.Controls.Add(new Literal { Text = "<br />" });

                // Delete Button
                Button deletebtn = new Button();
                deletebtn.ID = "DeleteBtn" + s.getSurveyID();
                deletebtn.Click += new EventHandler(DeleteSurveybtn_OnClick);
                deletebtn.CommandArgument = s.getSurveyID().ToString();
                deletebtn.Text = "Delete";
                deletebtn.CssClass = "btn btn-danger btn-sm btn-edit";
                SurveysPlaceholder.Controls.Add(deletebtn);

                // View Button
                Button viewbtn = new Button();
                viewbtn.ID = "ViewBtn" + s.getSurveyID();
                viewbtn.Click += new EventHandler(ViewSurveybtn_OnClick);
                viewbtn.CommandArgument = s.getSurveyID().ToString();
                viewbtn.Text = "View";
                viewbtn.CssClass = "btn btn-primary btn-sm";
                SurveysPlaceholder.Controls.Add(viewbtn);

                SurveysPlaceholder.Controls.Add(new Literal { Text = "</div></div></div>" });
            }
        }

        
    }

    protected void DeleteSurveybtn_OnClick(object sender, EventArgs e)
    {
        Button deletebtn = (Button)sender;

        if (sqlconnector.deleteSurvey(sqlconnector.getUser().getSurvey(Convert.ToInt32(deletebtn.CommandArgument))))
        {
            Session["message"] = "surveydeleted";
            Response.Redirect(Request.Path);
        }
        else
        {
            Messagelbl.Text = "Failed to delete layout.";
            Messagelbl.ForeColor = ColorTranslator.FromHtml("red");
            //MessagePlaceholder.Visible = true; ;
        }
    }


    protected void ViewSurveybtn_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int SurveyID = Convert.ToInt32(btn.CommandArgument);

        if (sqlconnector.getSurveyResponses(SurveyID))
        {
            TitleLbl.Controls.Add(new Literal() { Text = sqlconnector.getUser().getSurvey(SurveyID).getTittle() });
        }

        

        Survey survey = sqlconnector.getUser().getSurvey(SurveyID);
        DataTable surveydt = survey.getSurveyResponsesDT();
        
        if(surveydt != null)
        {
            ViewSurveyGridView.DataSource = surveydt;
            ViewSurveyGridView.DataBind();
            ViewSurveyMessageLbl.Visible = false;
        }
        else
        {
            ViewSurveyMessageLbl.Text = "No one has taken your survey yet. Please wait for responses.";
            ViewSurveyMessageLbl.Visible = true;
        }
        


    }

    protected void ViewResponse_OnClick(object sender, EventArgs e)
    {
    }
}