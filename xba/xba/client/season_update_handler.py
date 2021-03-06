#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from xba.config import CLIENT_EXE_PATH

from xba.business import player5_manager, player3_manager, account_manager, tool_manager, xcup_manager, xbatop_manager,\
    xguess_manager, star_match_manager
from xba.business import dev_match_manager
from xba.business import game_manager
from xba.business import finance_manager
from xba.business import dev_manager
from base import BaseClient
from xba.common.decorators import ensure_success
from xba.client.dev_match_handler import DevMatchHandler


class SeasonUpdateHandler(BaseClient):
    
    CLIENT_NAME = "season_update_handler"
    
    def __init__(self):
        super(self.__class__, self).__init__(self.__class__.CLIENT_NAME)
        self.__dev_level_sum = -1
        self.__season = -1
    
    def work(self):
        self.log("season update start")
        
        #更新球队综合
        self.set_team_ability()
        
        #分配选秀卡
        self.assign_choose_car()
        
        #统计赛季财政
        self.total_season_finance()
        
        #删除每天财政
        self.delete_turn_finance()
        
        #球员球员龄增加
        self.add_played_year()
        
        #球员伤病恢复
        self.player5_holiday()
        
        #球员老化
        self.player5_aging()
        
        #联赛奖励
        self.reward_dev()
        
        #联赛排名奖励,已经包括了MVP的奖励
        self.reward_dev_two()
        
        #球员赛季数据清空
        self.clear_player5_season()
        self.clear_player3_season()
        
        #发放冠军杯邀请函
        self.assign_xgame_card_with_devsort()

        #联赛更新
        self.dev_update()
        
        #清除14天不上的球队
        self.delete_from_league_long_not_login()
        
        #清训练点
        self.clear_train_point()
        
        #清豆腐
        self.clear_xba_box()
        
        #刷选秀球员
        self.betch_create_player()
        
        #删除联赛留言
        self.delete_devmessage()
        
        #将球队往前面挤一挤
        self.reset_dev()
        
        #将高综合点的球队往前面补
        self.fill_not_full_dev()
        
        #初始化联赛
        self.assign_dev()
        
        #将球队往前面挤一挤
        self.reset_dev()
        
        #比赛安排
        self.dev_match_assign()
                
        #发放提拔卡
        self.assign_promotion_card()
        
        #更新结束,设置赛季开始
        self.season_update_finish()
        
        #冠军杯初始化 
        self.init_xgame()
        
        #赛季收税
        self.payment_all()
        
        #球退役处理
        self.player_retire()
        
        #删除联赛日志
        self.delete_log_dev_msg()
        
        #清空冠军杯竟猜
        self.clean_xguess()
        
        #初始化全明星赛
        self.init_star_match()

        return "exist"
    
    @ensure_success
    def player_retire(self):
        """球员退役处理"""
        player3_infos = player3_manager.get_player3_pre_retire()
        if player3_infos:
            for player3_info in player3_infos:
                player_id, club_id = player3_info["PlayerID"], player3_info["ClubID"]
                if club_id > 0:
                    self.change_player_from_arrange5(player_id, club_id, 3)
                player3_manager.delete_player3(player_id)
                
        player5_infos = player5_manager.get_player5_pre_retire()
        if player5_infos:
            for player5_info in player5_infos:
                player_id, club_id = player5_info["PlayerID"], player5_info["ClubID"]
                if club_id > 0:
                    self.change_player_from_arrange5(player_id, club_id, 5)
                player5_manager.delete_player5(player_id)
                
        player5_manager.player_pre_retire()
        """球员退役发消息"""
        self.call_cmd("%s %s %s " % (CLIENT_EXE_PATH, 'season_update_handler', 1))
        
    @ensure_success
    def change_player_from_arrange5(self, player_id, club_id, category):
        command = "%s %s %s %s %s" % (CLIENT_EXE_PATH, "change_player_from_arrange5_handler", player_id, club_id, category)
        self.call_cmd(command)
    
    @ensure_success        
    def init_star_match(self):
        """初始化全明星赛"""
        return star_match_manager.init_star_match(self.__season + 1)
    
    @ensure_success
    def delete_log_dev_msg(self):
        """删除联赛日志"""
        return dev_manager.delete_log_dev_msg()
        
    @ensure_success
    def before_run(self):
        self.__dev_level_sum = self.get_total_level()
        game_info = self.get_game_info()
        self.__season = game_info["Season"]
        self.log("total dev level sum is %s" % self.__dev_level_sum)
        
    @ensure_success
    def season_update_finish(self):
        """赛季更新结束"""
        game_manager.set_to_next_days()
        game_manager.set_season()
    
    @ensure_success
    def get_total_level(self):
        """获取联赛等级数"""
        return game_manager.get_game_info()["DevLevelSum"]
    
    @ensure_success
    def delete_all_matches(self):
        """删除所有联赛比赛"""
        return dev_match_manager.delete_all_matches()
    
    @ensure_success
    def set_team_ability(self):
        """更新球队综合"""
        return xbatop_manager.set_team_ability()
    
    @ensure_success
    def reset_dev(self):
        """将各级联赛的球队尽量集中在一起"""
        self.log("start reset dev")
        for level in range(1, self.__dev_level_sum + 1):
            self.log("start to reset level:%s" % level)
            self.reset_level_dev(level)
            
    @ensure_success
    def change_player_from_arrange(self, playerid, clubid, category):
        command = "%s %s %s %s %s" % (CLIENT_EXE_PATH, "change_player_from_arrange5_handler", playerid, clubid, category)
        self.call_cmd(command)
                
    @ensure_success
    def reset_level_dev(self, level):
        """将某一等级的俱乐部往前排"""
        club_infos = self.get_dev_table_by_level(level)
        self.log("reset_level_dev,club count[%s]" % len(club_infos))
        for i, club_info in enumerate(club_infos):
            club_id = club_info["ClubID"]
            dev_code = club_info["DevCode"]
            if club_id == 0:
                self.log("club is empty,devcode[%s]" % dev_code)
                #如果该俱乐部是空，则从最后面补一个上来
                index, new_club_info = self.get_last_club_info(dev_code, club_infos)
                if new_club_info and i < index:
                    #交换两支球队
                    self.exchange_two_dev(club_info, new_club_info)
                else:
                    self.log("can not get a club return.i[%s], index[%s]" % (i, index))
                    #从后面拿不到一支有俱乐部的dev，则退出
                    return
            else:
                self.log("has club[%s], i[%s]. dev_code[%s]" % (club_id, i, dev_code))
    
    @ensure_success            
    def init_xgame(self):
        """冠军杯初始化"""
        return xcup_manager.init_xgame()
    
    @ensure_success            
    def clean_xguess(self):
        """冠军杯竞猜清空"""
        return xguess_manager.clean_xguess()
    
    @ensure_success
    def delete_from_league_long_not_login(self):
        """删除14天不上的球队"""
        return dev_manager.delete_from_league_long_not_login()
                
    @ensure_success
    def betch_create_player(self):
        """创建球员"""
        return player5_manager.betch_create_player()
         
    @ensure_success
    def payment_all(self):
        """赛季收税"""
        return finance_manager.payment_all()
    
    @ensure_success
    def clear_train_point(self):
        """清空训练点"""
        return player5_manager.clear_train_point()
      
    @ensure_success  
    def clear_xba_box(self):
        return tool_manager.clear_xba_box()
    
    @ensure_success
    def total_season_finance(self):
        """赛季财政统计"""
        return finance_manager.total_season_finance(self.__season)
           
    @ensure_success
    def delete_turn_finance(self):
        """删除每天财政"""
        return finance_manager.delete_turn_finance(self.__season)
            
    @ensure_success
    def assign_dev(self):
        """初始化联赛"""
        self.log("start assign dev")
        return dev_manager.assign_dev()
    
    @ensure_success
    def dev_match_assign(self):
        """安排所有比赛"""
        #删除所有比赛
        self.delete_all_matches()
        self.log("start to dev match assign")
        for level in range(1, self.__dev_level_sum + 1):
            dev_count = self.get_dev_count(level)
            for sort in range(dev_count):
                dev_code = self.get_dev_code_by(level, sort)
                self.do_dev_match_assign(dev_code)
                
        handler = DevMatchHandler()
        handler.update_club_main_xml()
                
    @ensure_success
    def do_dev_match_assign(self, dev_code):
        """为某个联赛安排比赛"""
        handler = DevMatchHandler()
        handler.do_dev_assign(dev_code)
    
    @ensure_success
    def exchange_two_dev(self, club_info_one, club_info_two):
        """交换两支球队"""
        return dev_manager.exchange_two_dev(club_info_one, club_info_two)
        
    @ensure_success
    def get_last_club_info(self, dev_code, club_infos):
        """获取最后一支球队"""
        i = len(club_infos)
        while i > 0:
            club_info = club_infos[i - 1]
            if club_info["ClubID"] > 0:
                if club_info["DevCode"] != dev_code:
                    return i, club_info
                else:
                    return 0, None
            i -= 1
        
        return 0, None
    
    @ensure_success    
    def get_dev_table_by_level(self, level):
        """获取某一等级所有俱乐部"""
        return dev_manager.get_dev_table_by_level(level)
        
    @ensure_success
    def add_played_year(self):
        """增加球员球龄"""
        return player5_manager.add_played_year()
    
    @ensure_success    
    def player5_holiday(self):
        """所有球员伤病恢复，体力恢复，心情恢复"""
        return player5_manager.player5_holiday()
    
    @ensure_success
    def clear_player5_season(self):
        """清理球员赛季数据"""
        return player5_manager.clear_player5_season()
    
    @ensure_success
    def clear_player3_season(self):
        """清理球员赛季数据"""
        return player3_manager.clear_player3_season()
    
    @ensure_success
    def reward_dev(self):
        """联赛冠军奖励"""
        for level in range(1, self.__dev_level_sum + 1):
            dev_count = self.get_dev_count(level)
            for sort in range(dev_count):
                dev_code = self.get_dev_code_by(level, sort)
                dev_manager.reward_dev(dev_code, level, sort)
                
    @ensure_success
    def reward_dev_two(self):
        """联赛奖励， 已经包括了MVP的奖励"""
        for level in range(1, self.__dev_level_sum + 1):
            dev_count = self.get_dev_count(level)
            for sort in range(dev_count):
                dev_code = self.get_dev_code_by(level, sort)
                club_infos = dev_manager.get_dev_clubs(dev_code)
                if not club_infos:
                    continue
                #联赛MVP奖励
                self.reword_mvp_by_devcode(dev_code)
                for i, club_info in enumerate(club_infos):
                    club_id = club_info["ClubID"]
                    if club_id <= 0 or i >= 10:
                        continue
                    else:
                        self.log("start to dev sort send money for level:%s, club id:%s" % (level, club_id))
                        self.dev_sort_send_money(level, club_id, i)
                        self.dev_sort_send_reputation(level, club_id, i)
         
    @ensure_success
    def dev_sort_send_reputation(self, level, club_id, sort):
        return dev_manager.dev_sort_send_reputation(level, club_id, sort)                  
                   
    @ensure_success
    def reword_mvp_by_devcode(self, dev_code):
        return dev_manager.reword_mvp_by_devcode(dev_code)
                        
    @ensure_success                    
    def dev_sort_send_money(self, level, club_id, sort):
        """联赛排名奖励"""
        return dev_manager.dev_sort_send_money(level, club_id, sort)                
    
    def dev_update(self):
        """联赛更新"""
        for level in range(1, self.__dev_level_sum + 1):
            self.log("start to update dev levels %s" % level)
            
            dev_count = self.get_dev_count(level)
            self.log("level:%s, dev total:%s" % (level, dev_count))
            for sort in range(dev_count):
                dev_code = self.get_dev_code_by(level, sort)
                self.log("start to handle dev[%s]" % dev_code)
                self.handle_dev(dev_code)
                
    @ensure_success
    def handle_dev(self, dev_code):
        """处理联赛"""
        club_infos = dev_manager.get_dev_clubs(dev_code, True)
        club_count = 0
        for club_info in club_infos:
            if club_info["ClubID"] > 0:
                club_count += 1
         
        self.log("dev %s has %s club's" % (dev_code, club_count))
        
        for i, club_info in enumerate(club_infos):
            if not dev_code:#一级联赛,只降不升
                if i in (10, 11, 12, 13):
                    self.log("club [%s] start to degrade" % club_info["ClubID"])
                    self.degrade_dev(club_info, i)
            elif len(dev_code) == self.__dev_level_sum - 1:#最后一级,只升不降
                if i in (0, 1):
                    self.log("club [%s] start to up grade" % club_info["ClubID"])
                    self.upgrade_dev(club_info)
            else:#又升又降
                if i in (10, 11, 12, 13):
                    self.log("club [%s] start to down grade" % club_info["ClubID"])
                    self.degrade_dev(club_info, i)
                if i in (0, 1):
                    self.log("club [%s] start to up grade" % club_info["ClubID"])
                    self.upgrade_dev(club_info)

    @ensure_success
    def upgrade_dev(self, dev_info):
        """联赛升级"""
        return dev_manager.upgrade_dev(dev_info)
    
    @ensure_success
    def degrade_dev(self, dev_info, sort):
        """联赛降级"""
        return dev_manager.degrade_dev(dev_info, sort)
            
    @ensure_success
    def get_dev_code_by(self, level, sort):
        """根据联赛等 级和序号获取dev code"""
        if level == 1:
            return ""
        
        base = ""
        while sort > 0:
            m = sort % 2
            sort = sort / 2
            base = "%s%s" % (m, base) 
            
        if len(base) < level - 1:
            base = "%s%s" % ("0" * (level - len(base) - 1), base)
        return base
    
    def get_dev_count(self, level):
        """根据联赛等级获取联赛数"""
        return 2 ** (level - 1)
    
    @ensure_success
    def get_game_info(self):
        return game_manager.get_game_info()
    
    @ensure_success
    def player5_aging(self):
        player5_manager.player5_aging()
        player3_manager.player3_aging()
    
    @ensure_success
    def delete_devmessage(self):
        return dev_manager.delete_devmessage()
    
    @ensure_success
    def assign_choose_car(self):
        """发放选秀卡"""
        return account_manager.assign_devchoose_card_with_devsort()
    
    @ensure_success
    def assign_promotion_card(self):
        """发放选拔卡"""
        return account_manager.assign_promotion_card()
    
    @ensure_success
    def assign_xgame_card_with_devsort(self):
        """发放冠军杯邀请"""
        return account_manager.assign_xgame_card_with_devsort()
    
    @ensure_success
    def fill_not_full_dev(self):
        """把高级的缺人的联赛填满"""
        for level in range(1, self.__dev_level_sum):
            self.log("start to fill level:%s" % level)
            self.do_fill_one_not_full_dev(level)
          
    @ensure_success
    def do_fill_one_not_full_dev(self, level):
        """把高级的缺人的联赛填满"""
        dev_infos = dev_manager.get_dev_table_by_total(level)
        if not dev_infos:
            self.log("not dev infos, cehck has up level?")
            #如果有上一级联,则下一级最少有一个联赛
            dev_infos = dev_manager.get_dev_table_by_total(level-1)
            if dev_infos:
                self.file_one_dev(level, "0" * (level - 1))
            return
        for dev_info in dev_infos:
            devcode, total = dev_info["devcode"], dev_info["total"]
            if total != 14:
                self.file_one_dev(level, devcode)
     
    def file_one_dev(self, level, devcode):
        """填充一个联赛"""
        infos = dev_manager.get_dev_clubs(devcode)
        for info in infos:
            clubid = info["ClubID"]
            if clubid <= 0:
                self.log("has club id less 0")
                #从下一级拿一级最高综合的球队
                user_infos = account_manager.get_one_level_max_team(level+1)
                if not user_infos:
                    #如果还不够，从下下一级要
                    user_infos = account_manager.get_one_level_max_team(level+2)
                self.log("get %s user infos" % len(user_infos))
                random.shuffle(user_infos)
                for user_info in user_infos:
                    if user_info and user_info["UserID"]:
                        new_dev_info = dev_manager.get_dev_info_by_userid(user_info["UserID"])
                        self.log("start to change %s(%s), %s(%s)" % (info["DevCode"], info["DevID"], new_dev_info["DevCode"], new_dev_info["DevID"]))
                        self.exchange_two_dev(info, new_dev_info)
                        break
                    
    def up_all9_level(self):
        """升级所有的9级球队"""
        dev_count = self.get_dev_count(9)
        for sort in range(dev_count):
            dev_code = self.get_dev_code_by(9, sort)
            club_infos = dev_manager.get_dev_clubs(dev_code)
            if not club_infos:
                continue
           
            for club_info in club_infos:
                club_id = club_info["ClubID"]
                if club_id > 0:
                    #从级拿一支空的球队
                    up_dev_info = dev_manager.get_one_empty_dev(8)
                    self.exchange_two_dev(club_info, up_dev_info)
                              
def main():
    handler = SeasonUpdateHandler()
    handler.before_run()
    #handler.reset_level_dev(8)
    handler.change_player_from_arrange5(14394, 1160, 5)

def run():
    handler = SeasonUpdateHandler()
    handler.start()
                      
if __name__ == "__main__":
    run()
    #main()
