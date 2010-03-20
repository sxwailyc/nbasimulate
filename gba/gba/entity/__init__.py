

__all__ = ['Player', 'PlayerBetchLog', 'YouthFreePlayer', 'SysConfig'
           , 'YouthPlayer', 'Message', 'Team', 'ProfessionPlayer', 'Matchs',
           'League', 'LeagueTeams', 'Names', 'UserInfo']

from gba.common.bottle.persistable import Persistable

class ProfessionPlayer(Persistable):
    CACHE_KEY = 'profession_player:no'
     
class FreePlayer(Persistable):
    CACHE_KEY = 'free_player:no'
    
class YouthFreePlayer(Persistable):
    CACHE_KEY = 'youth_free_player:no'

class YouthPlayer(Persistable):
    CACHE_KEY = 'youth_player:no'
    
class PlayerBetchLog(Persistable):
    CACHE_KEY = 'player_betch_log:betch_no'
    
class SysConfig(Persistable):
    CACHE_KEY = 'sys_config:key'
    
class Message(Persistable):
    CACHE_KEY = 'message:id'
    
class Team(Persistable):
    CACHE_KEY = 'team:username'
    
class Matchs(Persistable):
    CACHE_KEY = 'matchs:id'
 
class League(Persistable):
    CACHE_KEY = 'league:id'
 
class LeagueTeams(Persistable):
    CACHE_KEY = 'league_teams:id'
    
class Names(Persistable):
    CACHE_KEY = 'names:id'
    
class UserInfo(Persistable):
    CACHE_KEY = 'user_info:username'