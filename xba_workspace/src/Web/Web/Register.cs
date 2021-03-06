﻿namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Register : Page
    {
        protected ImageButton btnNext;
        protected DropDownList ddlMonth;
        protected DropDownList ddlProvince;
        protected DropDownList ddlYear;
        public int intRecomUserID;
        protected RadioButton rbFemale;
        protected RadioButton rbMale;
        public string strErrCity;
        public string strErrEmail;
        public string strErrIntroNickName;
        public string strErrInviteCode;
        public string strErrNickName;
        public string strErrPassword;
        public string strErrSay;
        public string strErrUserName;
        public string strMsg;
        public string strPageIntro;
        protected TextBox tbCity;
        protected TextBox tbEmail;
        protected TextBox tbIntroNickName;
        protected TextBox tbInviteCode;
        protected TextBox tbNickName;
        protected TextBox tbPassword;
        protected TextBox tbRePassword;
        protected TextBox tbSay;
        protected TextBox tbUserName;

        private void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            string text = this.tbUserName.Text;
            string strIn = this.tbPassword.Text;
            string str3 = strIn;
            string htmlEncode = StringItem.SetValidWord(this.tbNickName.Text.Trim());
            string str5 = "北京";
            string strEmail = this.tbEmail.Text;
            string str7 = this.tbIntroNickName.Text;
            bool blnSex = this.rbFemale.Checked;
            string selectedValue = this.ddlYear.SelectedValue;
            string text2 = this.ddlMonth.SelectedValue;
            string strProvince = this.ddlProvince.SelectedValue;
            string str9 = "无道篮球";
            StringItem.SetValidWord(this.tbSay.Text);
            if (str7 != null)
            {
                str7 = str7.Trim();
            }
            bool flag2 = false;
            if (!StringItem.IsValidLogin(text))
            {
                this.strErrUserName = "<font color='#FF0000'>*用户名填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidLogin(strIn) || (strIn != str3))
            {
                this.strErrPassword = "<font color='#FF0000'>*密码填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidName(htmlEncode, 2, 0x10))
            {
                this.strErrNickName = "<font color='#FF0000'>*昵称填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidName(str5, 2, 0x10))
            {
                this.strErrCity = "<font color='#FF0000'>*城市填写错误！</font>";
                flag2 = true;
            }
            else if ((str7.Length > 0) && !StringItem.IsValidName(str7, 2, 0x10))
            {
                this.strErrIntroNickName = "<font color='#FF0000'>*介绍人填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidName(str9, 2, 200))
            {
                this.strErrSay = "<font color='#FF0000'>*宣言填写错误！</font>";
                flag2 = true;
            }
            text = StringItem.GetHtmlEncode(text);
            htmlEncode = StringItem.GetHtmlEncode(htmlEncode);
            str5 = StringItem.GetHtmlEncode(str5);
            str7 = StringItem.GetHtmlEncode(str7);
            str9 = StringItem.GetHtmlEncode(str9);
            if (!flag2)
            {
                switch (BTPAccountManager.CheckRegisterInfo(text, htmlEncode, "11@11.com"))
                {
                    case 1:
                        this.strErrUserName = "<font color='#FF0000'>*用户名已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 2:
                        this.strErrNickName = "<font color='#FF0000'>*昵称已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 3:
                        this.strErrEmail = "<font color='#FF0000'>*Email已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 4:
                        this.strErrUserName = "<font color='#FF0000'>*用户名已存在，请重新输入！</font>";
                        this.strErrNickName = "<font color='#FF0000'>*昵称已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 5:
                        this.strErrUserName = "<font color='#FF0000'>*用户名已存在，请重新输入！</font>";
                        this.strErrEmail = "<font color='#FF0000'>*Email已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 6:
                        this.strErrNickName = "<font color='#FF0000'>*昵称已存在，请重新输入！</font>";
                        this.strErrEmail = "<font color='#FF0000'>*Email已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 7:
                        this.strErrUserName = "<font color='#FF0000'>*用户名已存在，请重新输入！</font>";
                        this.strErrNickName = "<font color='#FF0000'>*昵称已存在，请重新输入！</font>";
                        this.strErrEmail = "<font color='#FF0000'>*Email已存在，请重新输入！</font>";
                        flag2 = true;
                        break;
                }
                if (str7 != "")
                {
                    DataRow accountRowByNickName = BTPAccountManager.GetAccountRowByNickName(str7.Trim());
                    if (accountRowByNickName == null)
                    {
                        this.strErrIntroNickName = "<font color='#FF0000'>*您输入的介绍人并不存在，请重新输入或留空！</font>";
                        flag2 = true;
                    }
                    else
                    {
                        this.intRecomUserID = Convert.ToInt32(accountRowByNickName["UserID"]);
                    }
                }
                if (!flag2)
                {
                    bool flag3;
                    string strDiskURL = "NetDisk/" + StringItem.FormatDate(DateTime.Now, "yyyyMM") + "/";
                    if (base.Request.Cookies["FromURL"] != null)
                    {
                        base.Request.Cookies["FromURL"].Value.ToString();
                    }
                    try
                    {
                        BTPAccountManager.AddFullAccount(BTPAccountManager.GetMaxUserID(), text, htmlEncode, strIn, blnSex, 0, strDiskURL, "", strProvince, str5, "", this.intRecomUserID, strEmail);
                        flag3 = true;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.ToString());
                        flag3 = false;
                    }
                    if (!flag3)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=10114");
                    }
                    string strUserName = StringItem.MD5Encrypt(text, Global.strMD5Key);
                    string strPassword = StringItem.MD5Encrypt(strIn, Global.strMD5Key);
                    if (SessionItem.SetSelfLogin(strUserName, strPassword, true) > 0)
                    {
                        base.Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=10");
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnNext.Click += new ImageClickEventHandler(this.btnNext_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
            if (!base.IsPostBack)
            {
                this.ddlYear.DataSource = DDLItem.GetYear();
                this.ddlMonth.DataSource = DDLItem.GetMonth();
                this.ddlProvince.DataSource = DDLItem.GetProvince();
                this.ddlYear.DataBind();
                this.ddlMonth.DataBind();
                this.ddlProvince.DataBind();
                this.ddlYear.SelectedValue = "1980";
                this.ddlMonth.SelectedValue = "1";
                this.ddlProvince.SelectedValue = "北京市";
                this.strMsg = "请严格按照注释填写下列各项。";
            }
            this.btnNext.ImageUrl = SessionItem.GetImageURL() + "button_11.GIF";
            int request = SessionItem.GetRequest("u", 0);
            if (request > 0)
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(request);
                if (accountRowByUserID != null)
                {
                    this.tbIntroNickName.Text = Convert.ToString(accountRowByUserID["NickName"]);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(-1, "", "", "");
        }
    }
}

