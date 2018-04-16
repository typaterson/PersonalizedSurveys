using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Survey
{
    int SurveyID;
    int LayoutID;
    int UserID;
    string tittle;
    bool active;
    LinkedList<Question> questions;
    LinkedList<SurveyTaken> takensurveys;

    public Survey(int SurveyID, int LayoutID, int UserID, string tittle, bool active)
    {
        this.SurveyID = SurveyID;
        this.LayoutID = LayoutID;
        this.UserID = UserID;
        this.tittle = tittle;
        this.active = active;
        questions = new LinkedList<Question>();
    }

    public Survey(int SurveyID, int LayoutID, int UserID, string tittle, bool active, LinkedList<Question> questions)
    {
        this.SurveyID = SurveyID;
        this.LayoutID = LayoutID;
        this.UserID = UserID;
        this.tittle = tittle;
        this.active = active;
        this.questions = questions;
    }

    public bool addQuestion(int QuestionID, string text, string type, string target, int number, string commentbox)
    {
        if(questions == null)
        {
            questions = new LinkedList<Question>();
        }
        questions.AddLast(new Question(QuestionID, text, type, target, number, commentbox));
        return true;
    }

    public bool setTitle(string title)
    {
        tittle = title;
        return true;
    }

    public bool setLayoutID(int layoutid)
    {
        LayoutID = layoutid;
        return true;
    }

    public bool setActive(bool active)
    {
        this.active = active;
        return true;
    }

    public int getSurveyID()
    {
        return SurveyID;
    }

    public int getLayoutID()
    {
        return LayoutID;
    }

    public int getUserID()
    {
        return UserID;
    }

    public string getTittle()
    {
        return tittle;
    }

    public bool getActive()
    {
        return active;
    }

    public LinkedList<Question> getQuestions()
    {
        return questions;
    }

    public Question getQuestion(int number)
    {
        foreach(Question q in questions)
        {
            if(q.getQuestionNumber() == number)
            {
                return q;
            }
        }
        return null;
    }

    public Question removeQuestion(int number)
    {
        Question result = null;
        foreach(Question q in questions)
        {
            if (result != null)
            {
                q.setQuestionNumber(q.getQuestionNumber() - 1);
            }
            if (q.getQuestionNumber() == number)
            {
                result =  q;
            }
        }
        questions.Remove(result);
        return result;
    }

    public bool addSurveyTaken(SurveyTaken takensurvey)
    {
        if(takensurveys == null)
        {
            takensurveys = new LinkedList<SurveyTaken>();
        }
        takensurveys.AddLast(takensurvey);
        return true;
    }

    public LinkedList<SurveyTaken> getSurveysTaken()
    {
        return takensurveys;
    }

    public bool clearSurveysTaken()
    {
        takensurveys = null;
        return true;
    }

    public DataTable getSurveyResponsesDT()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SurveyTakenID");
        dt.Columns.Add("Yes/No Target Hit");
        dt.Columns.Add("CommentEntered");
        dt.Columns.Add("Scale 1-10 Target Hit");
        if (takensurveys != null)
        {
            foreach (SurveyTaken st in takensurveys)
            {
                DataRow row = dt.NewRow();
                row["SurveyTakenID"] = st.getSurveyTakenID();

                foreach (Question q in questions)
                {
                    Answer a = st.getAnswer(q.getQuestionID());
                    if (a != null)
                    {
                        string answer = st.getAnswer(q.getQuestionID()).getAnswer();
                        string target = q.getTarget();
                        string type = q.getType();


                        switch (type)
                        {
                            case "YN":
                                if (answer.Equals(target))
                                {
                                    row["Yes/No Target Hit"] = "Yes";
                                }
                                else { row["Yes/No Target Hit"] = "No"; }
                                break;

                            case "SC":
                                if (answer.Equals(target))
                                {
                                    row["Scale 1-10 Target Hit"] = "Yes";
                                }
                                else { row["Scale 1-10 Target Hit"] = "No"; }
                                break;
                        }
                        if (!st.getAnswer(q.getQuestionID()).getCommentText().Equals(""))
                        {
                            row["CommentEntered"] = "Yes";
                        }
                    }
                }

                dt.Rows.Add(row);
            }
            return dt;
        }
        return null;
    }
}