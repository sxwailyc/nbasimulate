using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using Web.DBData;
using Web.VMatchEngine;
using Client.XBA.Common;

namespace Client.XBA.Client
{
    class Club5MatchHandler:BaseClient
    {
        protected override void run()
        {

            DataTable matchs = this.GetFriMatch();
            if(matchs!=null)
            {
                Console.WriteLine(String.Format("has {0} new matchs", matchs.Rows.Count));  
                foreach(DataRow row in matchs.Rows)
                {
                    int category = Convert.ToInt32(row["Category"]);
                    if (category != Constant.MATCH_CATEGORY_FRIEND && category != Constant.MATCH_CATEGORY_ONLY_ONE && category != Constant.MATCH_CATEGORY_TRAIN)
                    {
                        if (category != Constant.MATCH_CATEGORY_TRAIN_A && category != Constant.MATCH_CATEGORY_UNION_FIELD)
                        {
                            Console.WriteLine(String.Format("not need handle category:{0}", category));
                            continue;
                        }
                    }
                    this.HandleMatch(row);
                }
            }
            else
            {
                Console.WriteLine("not matchs now, sleep 10s");
            }
            Thread.Sleep(10 * 1000);
            
        }

        private DataTable GetFriMatch()
        {
            while(true)
            {
                try
                {
                    return BTPFriMatchManager.GetFriMatch(Constant.MATCH_TYPE_5);
                }
                catch(Exception e)
                {
                   Console.WriteLine(e.Message);
                }
                Thread.Sleep(10*1000);
            }
        }

        private void HandleMatch(DataRow row)
        {
              int matchId =(int)row["FMatchID"];
              int clubIDA = (int)row["ClubIDA"];
              int clubIDB = (int)row["ClubIDB"];
              int category = Convert.ToInt32(row["Category"]);
              Console.WriteLine(matchId);
              Console.WriteLine(clubIDA);
              Console.WriteLine(clubIDB);
              bool blnPower = false;
              if (category == Constant.MATCH_CATEGORY_TRAIN || category == Constant.MATCH_CATEGORY_TRAIN_A)
              {
                  blnPower = true;
              }
              Match match = new Match(clubIDA, clubIDB, blnPower, category, matchId, false, false, 0, 0);
              match.Run();
              String reportUrl = match.sbRepURL.ToString();
              String statUrl = match.sbStasURL.ToString();
              int homeScore = match.GetHomeScore();
              int awayScore = match.GetAwayScore();
              Console.WriteLine("home score:" + homeScore + ":away score" + awayScore);
              BTPFriMatchManager.SetMatchEnd(matchId, Constant.MATCH_STATUS_FINISH, homeScore, awayScore, reportUrl, statUrl);
              if (category == Constant.MATCH_CATEGORY_ONLY_ONE)
              {
                  BTPOnlyOneCenterReg.OnlyOneMatchEnd(matchId);
              }
              else if (category == Constant.MATCH_CATEGORY_UNION_FIELD)
              {
                  BTPUnionFieldManager.UpdateUnionFieldGame(matchId, homeScore, awayScore);
              }
             
        }

    }
}
