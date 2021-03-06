﻿namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class XBACup : Page
    {
        protected ImageButton btnSearch;
        protected ImageButton btnXBAOK;
        protected ImageButton btnXResult;
        protected ImageButton btnXSchedule;
        private int intClubID;
        private int intClubID5;
        private int intGameID;
        private int intGroupCategory;
        public int intGroupCount;
        private int intGroupIndex;
        private int intPage;
        private int intPayType;
        private int intPerPage;
        private int intTotal;
        private int intUnionCategory;
        private int intUnionIDS;
        private int intUserID;
        public string strGroup;
        public string strGroupTeam;
        public string strKempList;
        private string strKind;
        public string strList;
        public string strPageIntro;
        public string strPageIntro1;
        public string strScript;
        public string strTeamList;
        private string strURL = "";
        public string strXClubLogo;
        public string strXClubName;
        public string strXGameLogo;
        public string strXIntro;
        public string strXMsg;
        public string strXResultList;
        public string strXRound;
        public string strXRoundA;
        protected TextBox tbGroup;
        protected HtmlTable tblKemp;
        protected HtmlTable tblRegXBACup;
        protected HtmlTable tblTryout;
        protected HtmlTable tblXBAGame;
        protected HtmlTable tblXCupIntro;
        protected HtmlTable tblXResult;
        protected TextBox tbPass;
        protected HtmlTableRow trXCupReg;

        private void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            int num;
            try
            {
                num = Convert.ToInt32(this.tbGroup.Text);
            }
            catch
            {
                num = 1;
            }
            base.Response.Redirect(string.Concat(new object[] { "XBACup.aspx?Kind=", this.strKind, "&Index=", num }));
        }

        private void btnXBAOK_Click(object sender, ImageClickEventArgs e)
        {
            switch (BTPXGroupTeamManager.RegXBACup(this.intClubID5))
            {
                case -6:
                    base.Response.Redirect("Report.aspx?Parameter=440");
                    return;

                case -5:
                    base.Response.Redirect("Report.aspx?Parameter=439");
                    return;

                case -4:
                    base.Response.Redirect("Report.aspx?Parameter=444");
                    return;

                case -3:
                    base.Response.Redirect("Report.aspx?Parameter=438c");
                    return;

                case -2:
                    base.Response.Redirect("Report.aspx?Parameter=438b");
                    return;

                case -1:
                    base.Response.Redirect("Report.aspx?Parameter=438");
                    return;

                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=442");
                    break;
            }
        }

        private void btnXResult_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect(string.Concat(new object[] { Config.GetDomain(), "XCupLadder/XResult", this.intGameID, ".htm" }));
        }

        private void btnXSchedule_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect(string.Concat(new object[] { Config.GetDomain(), "XCupLadder/XSchedule", this.intGameID, ".htm" }));
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            return BTPXGameManager.GetXGameCount();
        }

        private void GetTryoutList()
        {
            string str;
            string str2;
            if (this.strKind == "TRYOUT")
            {
                this.intGroupCategory = 1;
                this.strGroupTeam = "A";
            }
            else
            {
                this.intGroupCategory = 2;
                this.strGroupTeam = "B";
            }
            this.intGroupIndex = (int) SessionItem.GetRequest("Index", 0);
            if (this.intGroupIndex <= 0)
            {
                this.intGroupIndex = BTPXGroupTeamManager.GetGroupIndexByClubID(this.intClubID5, this.intGroupCategory);
                if (this.intGroupIndex == 0)
                {
                    this.intGroupIndex = 1;
                }
            }
            this.intGroupCount = BTPXGroupTeamManager.GetGroupCountByCategory(this.intGroupCategory);
            if (this.intGroupIndex == 1)
            {
                str = "&lt;&lt;";
            }
            else
            {
                str = string.Concat(new object[] { "<a href='XBACup.aspx?Kind=", this.strKind, "&Index=", this.intGroupIndex - 1, "'>&lt;&lt;</a>" });
            }
            if (this.intGroupIndex >= this.intGroupCount)
            {
                str2 = "&gt;&gt;";
            }
            else
            {
                str2 = string.Concat(new object[] { "<a href='XBACup.aspx?Kind=", this.strKind, "&Index=", this.intGroupIndex + 1, "'>&gt;&gt;</a>" });
            }
            this.strGroup = str + this.strGroupTeam + Convert.ToString(this.intGroupIndex) + "小组" + str2;
            DataTable groupTeamByCG = BTPXGroupTeamManager.GetGroupTeamByCG(this.intGroupCategory, this.intGroupIndex);
            this.GetXBATeamList(groupTeamByCG);
            DataTable dt = BTPXGroupMatchManager.GetGroupMatchByCategoryGroupIndex(this.intClubID5, this.intGroupCategory, this.intGroupIndex);
            this.GetXBAMatchList(dt, 3);
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            object obj2;
            int total = this.GetTotal();
            int num2 = (total / this.intPerPage) + 1;
            if ((total % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str2 = "";
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                strArray = new string[5];
                strArray[0] = "<a href='";
                strArray[1] = strCurrentURL;
                strArray[2] = "Page=";
                int num4 = this.intPage - 1;
                strArray[3] = num4.ToString();
                strArray[4] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == num2)
            {
                str3 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str4 = str4 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str4 = str4 + " selected";
                }
                obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            obj2 = str2 + " " + str3 + " ";
            return string.Concat(new object[] { obj2, "总数:", total, "跳转", str4 });
        }

        private void GetXBAMatchList(DataTable dt, int intC)
        {
            string str = "";
            string str2 = "";
            string str5 = "";
            if (dt == null)
            {
                this.strList = "<tr><td height='25' align='center'>暂时没有赛程</td></tr>";
            }
            else
            {
                int count = dt.Rows.Count;
                int num10 = count % intC;
                int num11 = count / intC;
                if (num10 > 0)
                {
                    num11++;
                }
                for (int i = 0; i < num11; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.strList = this.strList + "<tr><td height='25' width='50'　align='center'>第一轮</td>";
                            break;

                        case 1:
                            this.strList = this.strList + "<tr><td height='25' width='50'　align='center'>第二轮</td>";
                            break;

                        case 2:
                            this.strList = this.strList + "<tr><td height='25' width='50'　align='center'>第三轮</td>";
                            break;

                        case 3:
                            this.strList = this.strList + "<tr><td height='25' width='50'　align='center'>第四轮</td>";
                            break;

                        case 4:
                            this.strList = this.strList + "<tr><td height='25' width='50'　align='center'>第五轮</td>";
                            break;
                    }
                    for (int j = 1; j <= intC; j++)
                    {
                        int num;
                        int num2;
                        int num3;
                        int num4;
                        int num5;
                        int num6;
                        int num7;
                        int num8;
                        string str3;
                        string str4;
                        DataRow row;
                        object obj2;
                        int num12 = (i * intC) + (j - 1);
                        if ((j % 3) != 0)
                        {
                            if (num12 < count)
                            {
                                row = dt.Rows[num12];
                                num2 = (int) row["XGroupMatchID"];
                                num = (byte) row["Category"];
                                int num1 = (int) row["GroupIndex"];
                                num3 = (int) row["ClubAID"];
                                num4 = (int) row["ClubBID"];
                                num5 = (int) row["ClubAScore"];
                                num6 = (int) row["ClubBScore"];
                                num7 = (byte) row["TeamAIndex"];
                                num8 = (byte) row["TeamBIndex"];
                                str3 = row["RepURL"].ToString().Trim();
                                str4 = row["StasURL"].ToString().Trim();
                                byte num15 = (byte) row["Round"];
                                switch (num)
                                {
                                    case 1:
                                        str = "A" + Convert.ToString(num7);
                                        str2 = "A" + Convert.ToString(num8);
                                        if (num3 == 0)
                                        {
                                            str = "轮空";
                                        }
                                        if (num4 == 0)
                                        {
                                            str2 = "轮空";
                                        }
                                        if ((num3 == 0) || (num4 == 0))
                                        {
                                            str5 = "--";
                                        }
                                        else
                                        {
                                            str5 = num5 + ":" + num6;
                                        }
                                        break;

                                    case 2:
                                        str = "B" + Convert.ToString(num7);
                                        str2 = "B" + Convert.ToString(num8);
                                        if (num3 == 0)
                                        {
                                            str = "轮空";
                                        }
                                        if (num4 == 0)
                                        {
                                            str2 = "轮空";
                                        }
                                        if ((num3 == 0) || (num4 == 0))
                                        {
                                            str5 = "--";
                                        }
                                        else
                                        {
                                            str5 = num5 + ":" + num6;
                                        }
                                        break;
                                }
                                string strList = this.strList;
                                this.strList = strList + "<td width='140' align='center'><strong>" + str + "</strong> " + str5 + " <strong>" + str2 + "</strong>";
                                if ((str3 != "") && (str4 != ""))
                                {
                                    obj2 = this.strList;
                                    this.strList = string.Concat(new object[] { 
                                        obj2, " <a href='", Config.GetDomain(), "VRep.aspx?Tag=", num2, "&Type=4&A=", num3, "&B=", num4, "' target='_blank'><img src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a> <a href='", Config.GetDomain(), "VStas.aspx?Tag=", num2, "&Type=4&A=", 
                                        num3, "&B=", num4, "' target='_blank'><img src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>"
                                     });
                                }
                                this.strList = this.strList + "</td>";
                            }
                            else
                            {
                                this.strList = this.strList + "<td width='140'></td>";
                            }
                        }
                        else if (num12 < count)
                        {
                            row = dt.Rows[num12];
                            num2 = (int) row["XGroupMatchID"];
                            num = (byte) row["Category"];
                            int num16 = (int) row["GroupIndex"];
                            num3 = (int) row["ClubAID"];
                            num4 = (int) row["ClubBID"];
                            num5 = (int) row["ClubAScore"];
                            num6 = (int) row["ClubBScore"];
                            num7 = (byte) row["TeamAIndex"];
                            num8 = (byte) row["TeamBIndex"];
                            str3 = row["RepURL"].ToString().Trim();
                            str4 = row["StasURL"].ToString().Trim();
                            byte num17 = (byte) row["Round"];
                            DateTime datIn = (DateTime) row["MatchTime"];
                            switch (num)
                            {
                                case 1:
                                    str = "A" + Convert.ToString(num7);
                                    str2 = "A" + Convert.ToString(num8);
                                    if (num3 == 0)
                                    {
                                        str = "轮空";
                                    }
                                    if (num4 == 0)
                                    {
                                        str2 = "轮空";
                                    }
                                    if ((num3 == 0) || (num4 == 0))
                                    {
                                        str5 = "--";
                                    }
                                    else
                                    {
                                        str5 = num5 + ":" + num6;
                                    }
                                    break;

                                case 2:
                                    str = "B" + Convert.ToString(num7);
                                    str2 = "B" + Convert.ToString(num8);
                                    if (num3 == 0)
                                    {
                                        str = "轮空";
                                    }
                                    if (num4 == 0)
                                    {
                                        str2 = "轮空";
                                    }
                                    if ((num3 == 0) || (num4 == 0))
                                    {
                                        str5 = "--";
                                    }
                                    else
                                    {
                                        str5 = num5 + ":" + num6;
                                    }
                                    break;
                            }
                            obj2 = this.strList;
                            this.strList = string.Concat(new object[] { obj2, "<td width='140' align='center'><strong>", str, "</strong> ", num5, ":", num6, " <strong>", str2, "</strong>" });
                            if ((str3 != "") && (str4 != ""))
                            {
                                obj2 = this.strList;
                                this.strList = string.Concat(new object[] { 
                                    obj2, " <a href='", Config.GetDomain(), "VRep.aspx?Tag=", num2, "&Type=4&A=", num3, "&B=", num4, "' target='_blank'><img src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a> <a href='", Config.GetDomain(), "VStas.aspx?Tag=", num2, "&Type=4&A=", 
                                    num3, "&B=", num4, "' target='_blank'><img src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>"
                                 });
                            }
                            this.strList = this.strList + "</td><td width='140' align='center'><a title='比赛时间' style='cursor:hand;'>" + StringItem.FormatDate(datIn, "MM-dd hh:mm") + "</a></td></tr>";
                            this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                        }
                        else
                        {
                            this.strList = this.strList + "<td width='140'></td>";
                        }
                    }
                    this.strList = this.strList + "</tr>";
                }
            }
        }

        private void GetXBATeamList(DataTable dt)
        {
            string str = "";
            if (dt == null)
            {
                this.strTeamList = "<tr height='100%'><td colspan='5' align='center' height='25'>暂时没有球队参赛</td></tr>";
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    string str2;
                    int num = (byte) row["Category"];
                    int num2 = (byte) row["TeamIndex"];
                    int num4 = (int) row["Win"];
                    int num5 = (int) row["Lose"];
                    int num6 = (int) row["Score"];
                    int intClubID = (int) row["ClubID"];
                    if (intClubID == 0)
                    {
                        str2 = "轮空";
                    }
                    else
                    {
                        str2 = BTPClubManager.GetClubNameByClubID(intClubID, 5, "Right", 10);
                    }
                    switch (num)
                    {
                        case 1:
                            str = "A" + Convert.ToString(num2);
                            break;

                        case 2:
                            str = "B" + Convert.ToString(num2);
                            break;
                    }
                    object strTeamList = this.strTeamList;
                    this.strTeamList = string.Concat(new object[] { strTeamList, "<tr align='center' onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'>", str, "</td><td align='left'>", str2, "</td><td>", num4, "</td><td>", num5, "</td><td>", num6, "</td></tr>" });
                }
            }
        }

        private void GetXGameIntro()
        {
            int num;
            if (this.strKind == "XJOIN")
            {
                num = 3;
                this.strXGameLogo = "JoinCup.gif";
            }
            else if (this.strKind == "XWIN")
            {
                num = 4;
                this.strXGameLogo = "WinCup.GIF";
            }
            else if (this.strKind == "XCHAMPION")
            {
                num = 5;
                this.strXGameLogo = "ChampionCup.GIF";
            }
            else
            {
                base.Response.Redirect("");
                return;
            }
            DataRow xGameRowByCategory = BTPXGameManager.GetXGameRowByCategory(num);
            if (xGameRowByCategory != null)
            {
                int num2 = (byte) xGameRowByCategory["Round"];
                if (num2 == 0)
                {
                    this.btnXResult.Visible = false;
                    this.btnXSchedule.Visible = false;
                }
                int num3 = (byte) xGameRowByCategory["Status"];
                this.intGameID = (int) xGameRowByCategory["XGameID"];
                if (num3 != 3)
                {
                    this.strXClubName = "暂无";
                    this.strXClubLogo = "0.gif";
                }
                else
                {
                    this.strXClubName = xGameRowByCategory["ChampionClubName"].ToString().Trim();
                    int intUserID = (int) xGameRowByCategory["ChampionUserID"];
                    int clubIDByUserID = BTPClubManager.GetClubIDByUserID(intUserID);
                    this.strXClubLogo = BTPClubManager.GetClubLogo(clubIDByUserID);
                }
                this.strXIntro = xGameRowByCategory["Introduction"].ToString().Trim();
                this.strXRound = "<td height='25' bgcolor='#fcc6a4' align='right' style='PADDING-RIGHT:5px'><font color='#af1f30'>比赛进行到第" + num2 + "轮</font></td>";
            }
            else
            {
                this.strXRound = "<td height='25' bgcolor='#fcc6a4' align='center' s><font color='#af1f30'>暂时没有比赛在进行</font></td>";
                this.strXClubName = "暂无";
                this.strXClubLogo = "0.gif";
                this.strXIntro = "暂无比赛";
            }
        }

        public void GetXResultList()
        {
            int num2;
            int request = (int) SessionItem.GetRequest("XGameID", 0);
            DataRow gameRowByGameID = BTPXGameManager.GetGameRowByGameID(request);
            if (gameRowByGameID != null)
            {
                num2 = (byte) gameRowByGameID["Round"];
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
                return;
            }
            if (num2 == 1)
            {
                this.strXRoundA = "本轮结果 比赛尚未开始";
            }
            else
            {
                this.strXRoundA = "本轮结果 第" + (num2 - 1) + "轮";
            }
            DataTable regTableByCupID = BTPXCupRegManager.GetRegTableByCupID(request);
            if ((regTableByCupID != null) && (num2 > 1))
            {
                DataRow row2 = regTableByCupID.Rows[0];
                int length = row2["BaseCode"].ToString().Length - (num2 - 1);
                int num4 = (int) Math.Pow(2.0, (double) length);
                for (int i = 0; i < num4; i++)
                {
                    string strGainCode = Binary.FrontFillUp(Binary.DecToBinary(i), length, "0");
                    DataRow xResultRow = BTPXCupMatchManager.GetXResultRow(request, strGainCode);
                    int num1 = (int) xResultRow["XCupMatchID"];
                    int num10 = (int) xResultRow["XGameID"];
                    int intClubID = (int) xResultRow["ClubAID"];
                    int num6 = (int) xResultRow["ClubBID"];
                    int num7 = (int) xResultRow["ScoreA"];
                    int num8 = (int) xResultRow["ScoreB"];
                    string str2 = xResultRow["RepURL"].ToString().Trim();
                    string str3 = xResultRow["StasURL"].ToString().Trim();
                    xResultRow["GainCode"].ToString().Trim();
                    DateTime datIn = (DateTime) xResultRow["CreateTime"];
                    string str4 = BTPClubManager.GetClubNameByClubID(intClubID, 5, "Right", 10);
                    string str5 = BTPClubManager.GetClubNameByClubID(num6, 5, "Right", 10);
                    object strXResultList = this.strXResultList;
                    this.strXResultList = string.Concat(new object[] { strXResultList, "<tr><td height='25' align='center'>", str4, "</td><td align='center'>", num7, ":", num8, "</td><td align='center'>", str5, "</td><td align='center'>", StringItem.FormatDate(datIn, "MM-dd hh:mm"), "</td><td align='center'><a href='", str2, "'>战报</a> | <a href='", str3, "'>统计</a></td></tr>" });
                }
            }
            else
            {
                this.strXResultList = "<tr><td height='25' align='center'>暂时没有比赛结果</td></tr>";
            }
        }

        public void GetXScheduleList()
        {
            int num2;
            int num3;
            int num4;
            int request = (int) SessionItem.GetRequest("XGameID", 0);
            string str = "";
            string str2 = "";
            int num6 = 0;
            DataRow gameRowByGameID = BTPXGameManager.GetGameRowByGameID(request);
            if (gameRowByGameID != null)
            {
                num2 = (byte) gameRowByGameID["Round"];
                switch (((int) gameRowByGameID["Capacity"]))
                {
                    case 0x100:
                        num6 = 9;
                        goto Label_00A6;

                    case 0x200:
                        num6 = 10;
                        goto Label_00A6;

                    case 0x400:
                        num6 = 11;
                        goto Label_00A6;

                    case 0x800:
                        num6 = 12;
                        break;
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
                return;
            }
        Label_00A6:
            if (num2 == num6)
            {
                this.strXRoundA = "下轮对阵 比赛已结束";
            }
            else
            {
                this.strXRoundA = "下轮对阵 第" + num2 + "轮";
            }
            DataTable regTableByCupID = BTPXCupRegManager.GetRegTableByCupID(request);
            if (regTableByCupID != null)
            {
                DataRow row2 = regTableByCupID.Rows[0];
                num3 = row2["BaseCode"].ToString().Length - num2;
                num4 = (int) Math.Pow(2.0, (double) num3);
            }
            else
            {
                num3 = 0;
                num3 = 0;
                num4 = 0;
            }
            DataTable aliveRegTableByCupID = BTPXCupRegManager.GetAliveRegTableByCupID(request);
            if ((aliveRegTableByCupID != null) && (num2 < num6))
            {
                string strXResultList;
                if ((aliveRegTableByCupID.Rows.Count == 2) || (aliveRegTableByCupID.Rows.Count == 1))
                {
                    if (aliveRegTableByCupID.Rows.Count == 2)
                    {
                        int intClubID = (int) aliveRegTableByCupID.Rows[0]["ClubID"];
                        int num8 = (int) aliveRegTableByCupID.Rows[1]["ClubID"];
                        str = BTPClubManager.GetClubNameByClubID(intClubID, 5, "Right", 10);
                        str2 = BTPClubManager.GetClubNameByClubID(num8, 5, "Right", 10);
                        strXResultList = this.strXResultList;
                        this.strXResultList = strXResultList + "<tr><td height='25' align='center'>" + str + "</td><td align='center'>--:--</td><td align='center'>" + str2 + "</td><td align='center'>-- --</td><td align='center'>战报 | 统计</td></tr>";
                    }
                    else
                    {
                        this.strXResultList = "<tr><td height='25' align='center'>没有下场比赛</td></tr>";
                    }
                }
                else
                {
                    for (int i = 0; i < num4; i++)
                    {
                        string str4 = Binary.FrontFillUp(Binary.DecToBinary(i), num3, "0");
                        int num10 = 0;
                        int num11 = 0;
                        foreach (DataRow row3 in aliveRegTableByCupID.Rows)
                        {
                            int num12 = (int) row3["ClubID"];
                            string str5 = row3["BaseCode"].ToString();
                            if ((str5.Length == (str4.Length + num2)) && (str5.IndexOf(str4) == 0))
                            {
                                if (num10 == 0)
                                {
                                    num10 = num12;
                                    str = BTPClubManager.GetClubNameByClubID(num10, 5, "Right", 10);
                                }
                                else
                                {
                                    num11 = num12;
                                    str2 = BTPClubManager.GetClubNameByClubID(num11, 5, "Right", 10);
                                }
                                if ((str != "") && (str2 != ""))
                                {
                                    strXResultList = this.strXResultList;
                                    this.strXResultList = strXResultList + "<tr><td height='25' align='center'>" + str + "</td><td align='center'>--:--</td><td align='center'>" + str2 + "</td><td align='center'>-- --</td><td align='center'>战报 | 统计</td></tr>";
                                    str = "";
                                    str2 = "";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.strXResultList = "<tr><td height='25' align='center'>比赛已结束</td></tr>";
            }
        }

        private void InitializeComponent()
        {
            this.btnXBAOK.Click += new ImageClickEventHandler(this.btnXBAOK_Click);
            this.btnXResult.Click += new ImageClickEventHandler(this.btnXResult_Click);
            this.btnXSchedule.Click += new ImageClickEventHandler(this.btnXSchedule_Click);
            this.btnSearch.Click += new ImageClickEventHandler(this.btnSearch_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.tblRegXBACup.Visible = false;
                this.tblTryout.Visible = false;
                this.tblKemp.Visible = false;
                this.tblXCupIntro.Visible = false;
                this.tblXResult.Visible = false;
                this.tblXBAGame.Visible = false;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionIDS = (int) accountRowByUserID["UnionID"];
            this.intUnionCategory = (byte) accountRowByUserID["UnionCategory"];
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.intClubID = (int) onlineRowByUserID["ClubID3"];
            this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
            this.intPayType = Convert.ToInt16(onlineRowByUserID["PayType"]);
            this.strKind = SessionItem.GetRequest("Kind", 1).ToString().Trim();
            bool flag = false;
            DataRow parameterRow = BTPParameterManager.GetParameterRow();
            if (parameterRow != null)
            {
                flag = (bool) parameterRow["CanXBAGame"];
            }
            if (flag)
            {
                this.tblXBAGame.Visible = true;
                this.SetIntro();
                this.SetIntro1();
                this.strURL = DBLogin.URLString(ServerParameter.intGameCategory);
                this.btnXBAOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.btnSearch.ImageUrl = SessionItem.GetImageURL() + "button_34.gif";
                this.btnXResult.ImageUrl = SessionItem.GetImageURL() + "button_35.gif";
                this.btnXSchedule.ImageUrl = SessionItem.GetImageURL() + "button_36.gif";
            }
            else
            {
                base.Response.Redirect("SecretaryPage.aspx?Type=NOXBACUP");
            }
        }

        private void SetIntro()
        {
            this.strPageIntro = "<img src='" + SessionItem.GetImageURL() + "MenuCard/Union/Union_04.GIF' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
        }

        private void SetIntro1()
        {
            switch (this.strKind)
            {
                case "REGXBA":
                    this.strPageIntro1 = "<img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_01.gif' border='0' height='24' width='76' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblRegXBACup.Visible = true;
                    this.SetXMsg();
                    return;

                case "TRYOUT":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_02.gif' border='0' height='24' width='75' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblTryout.Visible = true;
                    this.GetTryoutList();
                    return;

                case "FINAL":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_03.gif' border='0' height='24' width='75' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblTryout.Visible = true;
                    this.GetTryoutList();
                    return;

                case "XJOINSCHEDULE":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_04.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXResult.Visible = true;
                    this.GetXScheduleList();
                    return;

                case "XWINSCHEDULE":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_05.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXResult.Visible = true;
                    this.GetXScheduleList();
                    return;

                case "XCHAMPIONSCHEDULE":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_06.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXResult.Visible = true;
                    this.GetXScheduleList();
                    return;

                case "XJOINRESULT":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_04.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXResult.Visible = true;
                    this.GetXScheduleList();
                    return;

                case "XWINRESULT":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_05.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXResult.Visible = true;
                    this.GetXScheduleList();
                    return;

                case "XCHAMPIONRESULT":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_06.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXResult.Visible = true;
                    this.GetXScheduleList();
                    return;

                case "XWIN":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_05.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXCupIntro.Visible = true;
                    this.GetXGameIntro();
                    return;

                case "XCHAMPION":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_06.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXCupIntro.Visible = true;
                    this.GetXGameIntro();
                    return;

                case "XJOIN":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_04.gif' border='0' height='24' width='62' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblXCupIntro.Visible = true;
                    this.GetXGameIntro();
                    return;

                case "KEMP":
                    this.strPageIntro1 = "<a href='" + this.strURL + "XBACup.aspx?Kind=REGXBA'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_01.gif' border='0' height='24' width='76' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_07.gif' border='0' height='24' width='62' border='0'><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblKemp.Visible = true;
                    this.XBAChampion();
                    return;
            }
            this.strPageIntro1 = "<img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_01.gif' border='0' height='24' width='76' border='0'><a href='" + this.strURL + "XBACup.aspx?Kind=TRYOUT'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_02.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=FINAL'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_03.gif' border='0' height='24' width='75' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XJOIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_04.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XWIN'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_05.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=XCHAMPION'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_06.gif' border='0' height='24' width='62' border='0'></a><a href='" + this.strURL + "XBACup.aspx?Kind=KEMP'><img src='" + SessionItem.GetImageURL() + "Cup/XBACup/XBACup_C_07.gif' border='0' height='24' width='62' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
            this.tblRegXBACup.Visible = true;
            this.SetXMsg();
        }

        private void SetXMsg()
        {
            if (BTPXGroupTeamManager.GetGroupTeamRowByCID(this.intClubID5) != null)
            {
                this.trXCupReg.Visible = false;
                this.strXMsg = "<font color='#660066'>您已成功报名，比赛会在联赛第四轮当天的下午4点正式开始。<font>";
            }
        }

        private void XBAChampion()
        {
            string strCurrentURL = "XBACup.aspx?Kind=KEMP&";
            this.intPerPage = 5;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            int intCount = this.intPerPage * this.intPage;
            this.intTotal = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            DataTable table = BTPXGameManager.GetXGameTable(this.intPage, this.intPerPage, this.intTotal, intCount);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2 = row["BigLogo"].ToString().Trim();
                    row["Introduction"].ToString().Trim();
                    string str3 = row["Name"].ToString().Trim();
                    string str4 = row["RewardXML"].ToString().Trim();
                    string str5 = row["ChampionClubName"].ToString().Trim();
                    byte num1 = (byte) row["Category"];
                    byte num4 = (byte) row["Status"];
                    byte num5 = (byte) row["Round"];
                    int num6 = (int) row["Capacity"];
                    int intUserID = (int) row["ChampionUserID"];
                    DateTime datIn = (DateTime) row["CreateTime"];
                    str5 = BTPClubManager.GetClubNameByClubID(BTPClubManager.GetClubIDByUserID(intUserID), 5, "Right", 0x11);
                    string strKempList = this.strKempList;
                    this.strKempList = strKempList + "<tr align='center'><td height='50'><Img src='" + SessionItem.GetImageURL() + "Cup/XBACup/" + str2 + "' height='50' width='40' border='0'></td><td>" + str3 + "</td><td><a href='" + str4 + "'>赛程</a></td><td>" + str5 + "</td><td>" + StringItem.FormatDate(datIn, "yy-MM-dd<br>hh:mm:ss") + "</td></tr>";
                }
                this.strList = this.strList + "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
            else
            {
                this.strKempList = "<tr><td height='25' align='center' colspan='5'>暂时没有杯赛冠军</td></tr>";
            }
        }
    }
}

