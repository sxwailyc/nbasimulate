﻿namespace Web.MyControls
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class JumpBoardListInfoNew : UserControl
    {
        public int intUserID;
        public string strBoardID;
        private string strBoardInfo;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetBoardInfo();
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(this.strBoardInfo);
        }

        private void SetBoardInfo()
        {
            bool flag;
            DataRow boardRowByBoardID = ROOTBoardManager.GetBoardRowByBoardID(this.strBoardID);
            string strMaster = boardRowByBoardID["Master"].ToString().Trim();
            int num = 0;
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            if (userInfoByID != null)
            {
                num = (int) userInfoByID["Wealth"];
            }
            if ((this.intUserID < 0) || (num < 0))
            {
                flag = false;
            }
            else
            {
                flag = true;
            }
            if (boardRowByBoardID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                string str2;
                switch (((byte) boardRowByBoardID["Category"]))
                {
                    case 1:
                        if ((this.intUserID <= -1) || (num < 0))
                        {
                            flag = false;
                            break;
                        }
                        if (!BoardItem.IsBoardMaster(this.intUserID, strMaster))
                        {
                            flag = false;
                        }
                        break;

                    case 2:
                        if ((this.intUserID > -1) && (num >= 0))
                        {
                            if (!BoardItem.CanView(this.intUserID, this.strBoardID))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=4004");
                                return;
                            }
                            break;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=4004");
                        return;

                    case 3:
                        base.Response.Redirect("Report.aspx?Parameter=4003");
                        return;
                }
                BoardItem.GetMasterNickName(strMaster);
                DataRow newsByBoardID = ROOTBoardManager.GetNewsByBoardID();
                if (newsByBoardID == null)
                {
                    str2 = "暂时没有新贴。";
                }
                else
                {
                    int num2 = (int) newsByBoardID["TopicID"];
                    string str3 = newsByBoardID["Title"].ToString().Trim();
                    str2 = string.Concat(new object[] { "<a href='Topic.aspx?TopicID=", num2, "&BoardID=001001&Page=1'>", str3, "</a>" });
                }
                string strLogo = "face1.gif";
                this.strBoardInfo = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td width='70%'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td height='35'>版主：<font class='ForumTime'>" + BoardItem.GetMasterNickName(strMaster) + "</font></td></tr><tr><td>" + BoardItem.GetForumLogo(strLogo) + str2 + "</td></tr></table></td><td width='30%'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td height='50'>";
                if (flag)
                {
                    string strBoardInfo = this.strBoardInfo;
                    this.strBoardInfo = strBoardInfo + "<a href='AddTopic.aspx?BoardID=" + this.strBoardID + "'><img src='" + SessionItem.GetImageURL() + "Forum/NewTitle.gif' border=0 align=absmiddle></a>";
                }
                this.strBoardInfo = this.strBoardInfo + "</td></tr><tr><td height='35'>论坛跳转：&nbsp;&nbsp;" + BoardItem.GetBoardJump(this.strBoardID) + "</td></tr></table></td></tr></table>";
            }
        }
    }
}

