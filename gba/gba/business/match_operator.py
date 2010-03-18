#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.common.db import connection
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.common.constants import MatchStatus, MatchTypes, TacticalGroupTypeMap, TacticalSectionTypeMap
from gba.common import log_execption
from gba.common import playerutil
from gba.entity import Team, ProfessionPlayer
from gba.business import player_operator

_SELECT_MATCH = 'select * from matchs where %s order by id desc limit %s, %s'
                
_SELECT_MATCH_TOTAL = 'select count(*) as total from matchs where %s order by id desc limit %s, %s'

def get_match(team_id, type, page=1, pagesize=15):
    '''获取比赛记录'''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    
    where = 'home_team_id=%s or guest_team_id=%s and type=%s' %  (team_id, team_id, type)
    
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH % (where, index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_MATCH_TOTAL, (where, ))
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

def send_match_request(home_team_id, guest_team_id, type):
    '''发送比赛请求'''
    info = {}
    info['home_team_id'] = home_team_id
    info['guest_team_id'] = guest_team_id
    info['type'] = type
    info['status'] = MatchStatus.SEND
    info['created_time'] = ReserveLiteral('now()')
    info['send_time'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        cursor.insert(info, 'matchs')
    finally:
        cursor.close()
        
def save_tactical_detail(info):
    '''保存战术'''
    info['created_time'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        cursor.insert(info, 'team_tactical_detail', True, ['created_time'])
    except:
        log_execption()
        return False
    finally:
        cursor.close()
    return True

_LOAD_TACTICAL_DETAIL = 'select * from team_tactical_detail where seq=%s and team_id=%s limit 1'

def get_tactical_detail(team_id, seq):
    '''获取战术信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_TACTICAL_DETAIL, (seq, team_id))
        if rs:
            return rs.to_dict()
    finally:
        cursor.close()
        
_SELECT_TACTICAL_DETAILS = 'select * from team_tactical_detail where team_id=%s order by seq asc'

def get_tactical_details(team_id):
    '''获取战术信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_TACTICAL_DETAILS, (team_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()

_SELECT_TACTICAL_MAINS = 'select * from team_tactical where team_id=%s order by type asc'
        
def get_tactical_mains(team_id):
    '''获取不同比赛阵容信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_TACTICAL_MAINS, (team_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
def create_team_default_tactical(team_id):
    '''创建球队默认战术'''
    
    players = player_operator.get_profession_player(team_id)
    if len(players) < 5:
        raise '球队人数不足五个, 不能创建'
    
    tactical_infos = {'PG': None, 'SG': None, 'SF': None, 'PF': None, 'C': None}
    in_player_nos = []
    #先按位置排
    for player in players:
        position = player['position']
        if tactical_infos[position] == None:
            tactical_infos[position] = player['no']
            in_player_nos.append(player['no'])
    
    #如果按位置不够人数,则挑高综合的补上
    if len(in_player_nos) < 5:
        for k, v in tactical_infos.items():
            if not v:
                for player in players:
                    if player['no'] not in in_player_nos:
                        tactical_infos[k] = player['no']
                        in_player_nos.append(player['no'])
                        break
                                       
    tactical_details = []
    for seq in 'ABCD':
        tactical_detail = {'team_id': team_id, 'pgid': tactical_infos['PG'], 'sgid': tactical_infos['SG'], \
                       'sfid': tactical_infos['SF'], 'pfid': tactical_infos['PF'], 'cid': tactical_infos['C'], \
                       'created_time': ReserveLiteral('now()')}
        if seq == 'A':
            name = '第一阵容'
        elif seq == 'B':
            name = '第二阵容'
        elif seq == 'C':
            name = '第三阵容'
        else:
            name = '第四阵容'

        tactical_detail['name'] = name
        tactical_detail['seq'] = seq
        tactical_details.append(tactical_detail)
    
    cursor = connection.cursor()
    try:
        cursor.insert(tactical_details, 'team_tactical_detail')
    finally:
        cursor.close()
        
    tactical_details = get_tactical_details(team_id)
    default_tactical_id = tactical_details[0]['id']
    
    tactical_mains = []
    for type in TacticalGroupTypeMap.keys():
        tactical_main = {'team_id': team_id, 'type': type}
        tactical_main['created_time'] = ReserveLiteral('now()')
        tactical_main['tactical_detail_1_id'] = default_tactical_id
        tactical_main['tactical_detail_2_id'] = default_tactical_id
        tactical_main['tactical_detail_3_id'] = default_tactical_id
        tactical_main['tactical_detail_4_id'] = default_tactical_id
        tactical_main['tactical_detail_5_id'] = default_tactical_id
        tactical_main['tactical_detail_6_id'] = default_tactical_id
        tactical_main['tactical_detail_7_id'] = default_tactical_id
        tactical_main['tactical_detail_8_id'] = default_tactical_id
        tactical_mains.append(tactical_main)
    
    cursor = connection.cursor()
    try:
        cursor.insert(tactical_mains, 'team_tactical')
    finally:
        cursor.close()
        
def save_tactical_main(infos):
    '''保存战术设置'''
    for info in infos:
        info['created_time'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        cursor.insert(infos, 'team_tactical', True, ['created_time', ])
    except:
        log_execption()
        return False
    finally:
        cursor.close()
    return True

def init_team(team_info):
    '''初始化一支球队'''
    
    team = Team()
    team.username = team_info['username']

    #先创建球员,8名职业球员1 c , 1pf 2 sf 2 sg 2 pg
    locations = ['C', 'PF', 'SF', 'SF', 'SG', 'SG', 'PG', 'PG']
    ProfessionPlayer.transaction()
    try:
        team.persist()
        for location in locations:
            player = playerutil.create_profession_player(location)
            setattr(player, 'team_id', team.id)
            player.persist()
        ProfessionPlayer.commit()
    except:
        ProfessionPlayer.rollback()
       
    #创建默认阵容
    create_team_default_tactical(team.id)
    
_SELECT_MATCH = 'select * from matchs where type=%s and (home_team_id=%s or guest_team_id=%s ) order by id limit %s, %s '
                       
_SELECT_MATCH_TOTAL = 'select count(*) as count from matchs where type=%s and (home_team_id=%s or guest_team_id=%s ) '

def get_match(team_id, type, page=1, pagesize=30):
    '''获取比赛列表
    '''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH % (type, team_id, team_id, index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_MATCH_TOTAL, (type, team_id, team_id))
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

_SELECT_MATCH_STAT = 'select * from match_stat where team_id=%s and match_id=%s'

def get_match_stat(team_id, match_id):
    '''获取比赛统计'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH_STAT, (team_id, match_id))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
_SELECT_MATCH_NODOSITY_MAIN = 'select * from match_nodosity_main where match_id=%s order by seq asc'

def get_match_nodosity_main(match_id):
    '''获取比赛每节概要'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH_NODOSITY_MAIN, (match_id,))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
_SELECT_MATCH_NODOSITY_DETAIL = 'select * from match_nodosity_detail where match_nodosity_main_id=%s order by seq asc '

def get_match_nodosity_detail(match_nodosity_main_id):
    '''获取每节的比赛详细'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH_NODOSITY_DETAIL, (match_nodosity_main_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
if __name__ == '__main__':
    print get_match(1, 1)