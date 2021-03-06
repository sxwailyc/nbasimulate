-- MySQL dump 10.13  Distrib 5.1.36, for unknown-linux-gnu (x86_64)
--
-- Host: localhost    Database: gba
-- ------------------------------------------------------
-- Server version	5.1.36-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `action_desc`
--

DROP TABLE IF EXISTS `action_desc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `action_desc` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `result` varchar(255) DEFAULT NULL,
  `action_desc` varchar(255) DEFAULT NULL,
  `action_name` varchar(255) DEFAULT NULL,
  `flg` varchar(255) DEFAULT NULL,
  `percent` int(11) NOT NULL,
  `is_assist` tinyint(3) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `all_finance`
--

DROP TABLE IF EXISTS `all_finance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `all_finance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `season` smallint(6) DEFAULT NULL,
  `income` int(11) NOT NULL DEFAULT '0',
  `outlay` int(11) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id` (`team_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `attention_player`
--

DROP TABLE IF EXISTS `attention_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attention_player` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `no` char(32) NOT NULL DEFAULT '',
  `type` tinyint(3) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id_no` (`no`,`team_id`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `client_running_log`
--

DROP TABLE IF EXISTS `client_running_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `client_running_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `client_name` varchar(255) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `log_time` datetime DEFAULT NULL,
  `status` varchar(15) DEFAULT NULL,
  `log` varchar(2000) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `client_ip_time` (`client_name`,`ip`,`log_time`)
) ENGINE=InnoDB AUTO_INCREMENT=34467 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `error_log`
--

DROP TABLE IF EXISTS `error_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `error_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` varchar(20) DEFAULT NULL,
  `log` varchar(2500) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `free_player`
--

DROP TABLE IF EXISTS `free_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `free_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` tinyint(3) DEFAULT NULL,
  `picture` varchar(255) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `current_team_id` bigint(20) DEFAULT NULL,
  `current_price` int(11) DEFAULT '0',
  `ability` double DEFAULT NULL,
  `shooting` float DEFAULT NULL,
  `speed` float DEFAULT NULL,
  `strength` float DEFAULT NULL,
  `bounce` float DEFAULT NULL,
  `stamina` float DEFAULT NULL,
  `trisection` float DEFAULT NULL,
  `dribble` float DEFAULT NULL,
  `pass` float DEFAULT NULL,
  `backboard` float DEFAULT NULL,
  `steal` float DEFAULT NULL,
  `blocked` float DEFAULT NULL,
  `shooting_max` float DEFAULT NULL,
  `speed_max` float DEFAULT NULL,
  `strength_max` float DEFAULT NULL,
  `bounce_max` float DEFAULT NULL,
  `stamina_max` float DEFAULT NULL,
  `trisection_max` float DEFAULT NULL,
  `dribble_max` float DEFAULT NULL,
  `pass_max` float DEFAULT NULL,
  `backboard_max` float DEFAULT NULL,
  `steal_max` float DEFAULT NULL,
  `blocked_max` float DEFAULT NULL,
  `expired_time` datetime DEFAULT NULL,
  `betch_no` varchar(100) DEFAULT NULL,
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `bid_count` smallint(6) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `worth` int(11) DEFAULT '0',
  `wage` int(11) DEFAULT '0',
  `power` smallint(6) DEFAULT '100',
  `auction_status` tinyint(1) DEFAULT '0',
  `delete_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`),
  KEY `delete_time` (`delete_time`),
  KEY `expired_time` (`expired_time`)
) ENGINE=InnoDB AUTO_INCREMENT=597 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league`
--

DROP TABLE IF EXISTS `league`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `degree` smallint(6) DEFAULT NULL,
  `no` smallint(6) DEFAULT NULL,
  `team_count` smallint(6) DEFAULT '0',
  `status` tinyint(3) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `degree_no` (`degree`,`no`)
) ENGINE=InnoDB AUTO_INCREMENT=2508 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_config`
--

DROP TABLE IF EXISTS `league_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_config` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` smallint(6) DEFAULT NULL,
  `round` smallint(6) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_dis_task`
--

DROP TABLE IF EXISTS `league_dis_task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_dis_task` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `league_id` int(11) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_info`
--

DROP TABLE IF EXISTS `league_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `新字段` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_matchs`
--

DROP TABLE IF EXISTS `league_matchs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_matchs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `match_team_home_id` int(11) NOT NULL DEFAULT '0',
  `match_team_guest_id` int(11) NOT NULL DEFAULT '0',
  `round` smallint(6) DEFAULT NULL,
  `point` varchar(50) DEFAULT NULL,
  `status` tinyint(1) DEFAULT '0',
  `league_id` int(11) NOT NULL DEFAULT '0',
  `match_id` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `match_id` (`match_id`)
) ENGINE=InnoDB AUTO_INCREMENT=456093 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_tasks`
--

DROP TABLE IF EXISTS `league_tasks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_tasks` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `league_id` int(11) DEFAULT NULL,
  `status` tinyint(1) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_teams`
--

DROP TABLE IF EXISTS `league_teams`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_teams` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `league_id` int(11) DEFAULT NULL,
  `team_id` int(11) DEFAULT '-1',
  `rank` varchar(255) DEFAULT NULL,
  `win` smallint(6) DEFAULT '0',
  `lose` smallint(6) DEFAULT '0',
  `net_points` smallint(6) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `status` tinyint(3) DEFAULT '0',
  `seq` tinyint(3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `league_id` (`league_id`),
  KEY `status` (`status`),
  KEY `team_id` (`team_id`),
  KEY `rank` (`rank`)
) ENGINE=InnoDB AUTO_INCREMENT=35090 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_nodosity_detail`
--

DROP TABLE IF EXISTS `match_nodosity_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_nodosity_detail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `match_nodosity_main_id` bigint(20) DEFAULT NULL,
  `match_id` bigint(20) DEFAULT NULL,
  `seq` smallint(6) DEFAULT NULL,
  `description` varchar(1024) DEFAULT NULL,
  `time_msg` varchar(40) DEFAULT NULL,
  `point_msg` varchar(40) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `main_seq` (`match_nodosity_main_id`,`seq`),
  KEY `match_nodosity_main_id` (`match_nodosity_main_id`)
) ENGINE=InnoDB AUTO_INCREMENT=26468 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_nodosity_main`
--

DROP TABLE IF EXISTS `match_nodosity_main`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_nodosity_main` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `seq` smallint(6) DEFAULT NULL,
  `match_id` bigint(20) DEFAULT NULL,
  `home_tactic_id` bigint(20) DEFAULT NULL,
  `visiting_tactic_id` bigint(20) DEFAULT NULL,
  `point` varchar(255) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=282 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_nodosity_tactical_detail`
--

DROP TABLE IF EXISTS `match_nodosity_tactical_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_nodosity_tactical_detail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `match_nodosity_main_id` bigint(20) DEFAULT NULL,
  `position` varchar(3) DEFAULT NULL,
  `player_id` bigint(20) DEFAULT NULL,
  `player_name` varchar(255) DEFAULT NULL,
  `colligate` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2811 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_not_in_player`
--

DROP TABLE IF EXISTS `match_not_in_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_not_in_player` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `match_id` int(11) NOT NULL,
  `player_no` char(32) NOT NULL,
  `ability` int(11) NOT NULL,
  `created_time` datetime NOT NULL,
  `updated_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_match_player` (`team_id`,`match_id`,`player_no`),
  KEY `team_match` (`match_id`,`team_id`)
) ENGINE=MyISAM AUTO_INCREMENT=381 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_stat`
--

DROP TABLE IF EXISTS `match_stat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_stat` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `team_id` bigint(20) DEFAULT NULL,
  `player_no` char(32) DEFAULT NULL,
  `match_id` bigint(20) DEFAULT NULL,
  `point2_shoot_times` smallint(6) DEFAULT '0',
  `point2_doom_times` smallint(6) DEFAULT '0',
  `point3_shoot_times` smallint(6) DEFAULT '0',
  `point3_doom_times` smallint(6) DEFAULT '0',
  `point1_shoot_times` smallint(6) DEFAULT '0',
  `point1_doom_times` smallint(6) DEFAULT '0',
  `offensive_rebound` smallint(6) DEFAULT '0',
  `defensive_rebound` smallint(6) DEFAULT '0',
  `foul` smallint(6) DEFAULT '0',
  `lapsus` smallint(6) DEFAULT '0',
  `assist` smallint(6) DEFAULT '0',
  `block` smallint(6) DEFAULT '0',
  `steals` smallint(6) DEFAULT '0',
  `times` smallint(6) DEFAULT NULL,
  `is_main` tinyint(3) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `player_match` (`player_no`,`match_id`),
  KEY `match_id` (`match_id`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=701 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `matchs`
--

DROP TABLE IF EXISTS `matchs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `matchs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `home_team_id` int(11) DEFAULT NULL,
  `guest_team_id` int(11) DEFAULT NULL,
  `type` smallint(6) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `sub_status` tinyint(1) DEFAULT '0',
  `point` varchar(20) DEFAULT NULL,
  `send_time` datetime DEFAULT NULL,
  `start_time` datetime DEFAULT NULL,
  `is_youth` tinyint(1) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `home_team_id` (`home_team_id`),
  KEY `guest_team_id` (`guest_team_id`),
  KEY `type` (`type`)
) ENGINE=InnoDB AUTO_INCREMENT=128 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `message`
--

DROP TABLE IF EXISTS `message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `message` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `to_team_id` int(11) DEFAULT NULL,
  `from_team_id` int(11) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `content` text,
  `is_new` tinyint(1) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=322 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `names`
--

DROP TABLE IF EXISTS `names`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `names` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_type` (`name`,`type`)
) ENGINE=InnoDB AUTO_INCREMENT=844 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `player_auction_log`
--

DROP TABLE IF EXISTS `player_auction_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `player_auction_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` tinyint(1) DEFAULT NULL,
  `player_no` char(32) DEFAULT NULL,
  `content` varchar(2000) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `player_betch_log`
--

DROP TABLE IF EXISTS `player_betch_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `player_betch_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `betch_no` varchar(100) DEFAULT NULL,
  `is_success` tinyint(1) DEFAULT NULL,
  `info` text,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `betch_no` (`betch_no`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pro_player_career_stat_total`
--

DROP TABLE IF EXISTS `pro_player_career_stat_total`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pro_player_career_stat_total` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `player_no` char(32) DEFAULT NULL,
  `point2_shoot_times` smallint(6) DEFAULT '0',
  `point2_doom_times` smallint(6) DEFAULT '0',
  `point3_shoot_times` smallint(6) DEFAULT '0',
  `point3_doom_times` smallint(6) DEFAULT '0',
  `point1_shoot_times` smallint(6) DEFAULT '0',
  `point1_doom_times` smallint(6) DEFAULT '0',
  `point_total` int(11) DEFAULT '0',
  `offensive_rebound` smallint(6) DEFAULT '0',
  `defensive_rebound` smallint(6) DEFAULT '0',
  `rebound_total` smallint(6) DEFAULT '0',
  `foul` smallint(6) DEFAULT '0',
  `lapsus` smallint(6) DEFAULT '0',
  `assist` smallint(6) DEFAULT '0',
  `block` smallint(6) DEFAULT '0',
  `steals` smallint(6) DEFAULT '0',
  `times` smallint(6) DEFAULT NULL,
  `match_total` int(11) DEFAULT '0',
  `main_total` int(11) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `player_no` (`player_no`)
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pro_player_season_stat_total`
--

DROP TABLE IF EXISTS `pro_player_season_stat_total`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pro_player_season_stat_total` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `player_no` char(32) NOT NULL DEFAULT '',
  `point2_shoot_times` smallint(6) DEFAULT '0',
  `point2_doom_times` smallint(6) DEFAULT '0',
  `point3_shoot_times` smallint(6) DEFAULT '0',
  `point3_doom_times` smallint(6) DEFAULT '0',
  `point1_shoot_times` smallint(6) DEFAULT '0',
  `point1_doom_times` smallint(6) DEFAULT '0',
  `point_total` int(11) DEFAULT '0',
  `offensive_rebound` smallint(6) DEFAULT '0',
  `defensive_rebound` smallint(6) DEFAULT '0',
  `rebound_total` smallint(6) DEFAULT '0',
  `foul` smallint(6) DEFAULT '0',
  `lapsus` smallint(6) DEFAULT '0',
  `assist` smallint(6) DEFAULT '0',
  `block` smallint(6) DEFAULT '0',
  `steals` smallint(6) DEFAULT '0',
  `times` smallint(6) DEFAULT NULL,
  `match_total` int(11) DEFAULT '0',
  `main_total` int(11) DEFAULT '0',
  `point_agv` float NOT NULL DEFAULT '0',
  `rebound_agv` float NOT NULL DEFAULT '0',
  `steals_agv` float NOT NULL DEFAULT '0',
  `assist_agv` float NOT NULL DEFAULT '0',
  `block_agv` float NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `player_no` (`player_no`),
  KEY `point_agv` (`point_agv`),
  KEY `rebound_agv` (`rebound_agv`),
  KEY `steals_agv` (`steals_agv`),
  KEY `assist_agv` (`assist_agv`),
  KEY `block_agv` (`block_agv`)
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `profession_player`
--

DROP TABLE IF EXISTS `profession_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `profession_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` smallint(6) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `wage` int(11) DEFAULT NULL,
  `contract` smallint(6) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `team_id` bigint(20) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `power` tinyint(3) DEFAULT NULL,
  `ability` double DEFAULT NULL,
  `shooting` float DEFAULT NULL,
  `speed` float DEFAULT NULL,
  `strength` float DEFAULT NULL,
  `bounce` float DEFAULT NULL,
  `stamina` float DEFAULT NULL,
  `trisection` float DEFAULT NULL,
  `dribble` float DEFAULT NULL,
  `pass` float DEFAULT NULL,
  `backboard` float DEFAULT NULL,
  `steal` float DEFAULT NULL,
  `blocked` float DEFAULT NULL,
  `shooting_max` float DEFAULT NULL,
  `speed_max` float DEFAULT NULL,
  `strength_max` float DEFAULT NULL,
  `bounce_max` float DEFAULT NULL,
  `stamina_max` float DEFAULT NULL,
  `trisection_max` float DEFAULT NULL,
  `dribble_max` float DEFAULT NULL,
  `pass_max` float DEFAULT NULL,
  `backboard_max` float DEFAULT NULL,
  `steal_max` float DEFAULT NULL,
  `blocked_max` float DEFAULT NULL,
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  `picture` smallint(6) DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `training` varchar(30) DEFAULT 'speed',
  `last_inc` float DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  UNIQUE KEY `team_player_no` (`player_no`,`team_id`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`)
) ENGINE=InnoDB AUTO_INCREMENT=112 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `runtime_data`
--

DROP TABLE IF EXISTS `runtime_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `runtime_data` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `programe` varchar(255) DEFAULT NULL,
  `value_key` varchar(50) DEFAULT NULL,
  `value` text,
  `created_at` datetime DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `NewIndex` (`programe`,`value_key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `season_finance`
--

DROP TABLE IF EXISTS `season_finance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `season_finance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` varchar(255) DEFAULT NULL,
  `season` smallint(6) DEFAULT NULL,
  `round` tinyint(3) DEFAULT NULL,
  `income` int(11) DEFAULT NULL,
  `outlay` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_season_round` (`team_id`,`round`,`season`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `season_finance_detail`
--

DROP TABLE IF EXISTS `season_finance_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `season_finance_detail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` smallint(6) DEFAULT NULL,
  `round` tinyint(3) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `info` varchar(255) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `table`
--

DROP TABLE IF EXISTS `table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `table` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `value` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team`
--

DROP TABLE IF EXISTS `team`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(30) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `youth_league` int(11) DEFAULT NULL,
  `profession_league_evel` smallint(6) DEFAULT NULL,
  `profession_league_class` smallint(6) DEFAULT NULL,
  `funds` int(11) DEFAULT '0',
  `hold_funds` int(11) DEFAULT '0',
  `last_active_time` datetime DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`name`),
  UNIQUE KEY `name` (`name`),
  KEY `last_active_time` (`last_active_time`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_arena`
--

DROP TABLE IF EXISTS `team_arena`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_arena` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `level` tinyint(3) DEFAULT '1',
  `fare` tinyint(3) NOT NULL DEFAULT '0',
  `fan_count` varchar(255) NOT NULL DEFAULT '',
  `status` tinyint(3) DEFAULT '0',
  `next_level_time` datetime DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_staff`
--

DROP TABLE IF EXISTS `team_staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_staff` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `is_youth` tinyint(3) DEFAULT '0',
  `current_team_id` int(11) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `wave` smallint(6) DEFAULT NULL,
  `round` tinyint(3) DEFAULT NULL,
  `level_round` tinyint(3) DEFAULT NULL,
  `level` tinyint(3) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_tactical`
--

DROP TABLE IF EXISTS `team_tactical`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_tactical` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `team_id` int(11) NOT NULL,
  `tactical_detail_1_id` int(11) DEFAULT NULL,
  `tactical_detail_2_id` int(11) DEFAULT NULL,
  `tactical_detail_3_id` int(11) DEFAULT NULL,
  `tactical_detail_4_id` int(11) DEFAULT NULL,
  `tactical_detail_5_id` int(11) DEFAULT NULL,
  `tactical_detail_6_id` int(11) DEFAULT NULL,
  `tactical_detail_7_id` int(11) DEFAULT NULL,
  `tactical_detail_8_id` int(11) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `is_youth` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id_type` (`team_id`,`type`,`is_youth`)
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_tactical_detail`
--

DROP TABLE IF EXISTS `team_tactical_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_tactical_detail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `offensive_tactical_type` tinyint(3) DEFAULT '1',
  `defend_tactical_type` tinyint(3) DEFAULT '7',
  `seq` char(1) DEFAULT NULL,
  `cid` char(32) DEFAULT NULL,
  `pfid` char(32) DEFAULT NULL,
  `sfid` char(32) DEFAULT NULL,
  `sgid` char(32) DEFAULT NULL,
  `pgid` char(32) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_youth` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id` (`team_id`,`seq`,`is_youth`)
) ENGINE=InnoDB AUTO_INCREMENT=113 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `test`
--

DROP TABLE IF EXISTS `test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `test` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `value` varchar(255) DEFAULT NULL,
  `b` tinyint(3) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `training_center`
--

DROP TABLE IF EXISTS `training_center`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `training_center` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `in_match` tinyint(1) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id` (`team_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user_info`
--

DROP TABLE IF EXISTS `user_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `password` char(32) DEFAULT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `session_id` varchar(255) DEFAULT NULL,
  `last_active_time` datetime DEFAULT NULL,
  `last_login_time` datetime DEFAULT NULL,
  `login_times` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `youth_free_player`
--

DROP TABLE IF EXISTS `youth_free_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `youth_free_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` tinyint(3) DEFAULT NULL,
  `picture` varchar(255) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `ability` double DEFAULT NULL,
  `shooting` float DEFAULT NULL,
  `speed` float DEFAULT NULL,
  `strength` float DEFAULT NULL,
  `bounce` float DEFAULT NULL,
  `stamina` float DEFAULT NULL,
  `trisection` float DEFAULT NULL,
  `dribble` float DEFAULT NULL,
  `pass` float DEFAULT NULL,
  `backboard` float DEFAULT NULL,
  `steal` float DEFAULT NULL,
  `blocked` float DEFAULT NULL,
  `shooting_max` float DEFAULT NULL,
  `speed_max` float DEFAULT NULL,
  `strength_max` float DEFAULT NULL,
  `bounce_max` float DEFAULT NULL,
  `stamina_max` float DEFAULT NULL,
  `trisection_max` float DEFAULT NULL,
  `dribble_max` float DEFAULT NULL,
  `pass_max` float DEFAULT NULL,
  `backboard_max` float DEFAULT NULL,
  `steal_max` float DEFAULT NULL,
  `blocked_max` float DEFAULT NULL,
  `betch_no` varchar(100) DEFAULT NULL,
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `bid_count` smallint(6) DEFAULT '0',
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `delete_time` datetime DEFAULT NULL,
  `expired_time` datetime DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `price` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`),
  KEY `delete_time` (`delete_time`),
  KEY `expired_time` (`expired_time`)
) ENGINE=InnoDB AUTO_INCREMENT=597 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `youth_freeplayer_auction_log`
--

DROP TABLE IF EXISTS `youth_freeplayer_auction_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `youth_freeplayer_auction_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `player_no` char(32) DEFAULT NULL,
  `price` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `youth_player`
--

DROP TABLE IF EXISTS `youth_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `youth_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` smallint(6) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `wage` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `power` tinyint(3) DEFAULT NULL,
  `ability` double DEFAULT NULL,
  `shooting` float DEFAULT NULL,
  `speed` float DEFAULT NULL,
  `strength` float DEFAULT NULL,
  `bounce` float DEFAULT NULL,
  `stamina` float DEFAULT NULL,
  `trisection` float DEFAULT NULL,
  `dribble` float DEFAULT NULL,
  `pass` float DEFAULT NULL,
  `backboard` float DEFAULT NULL,
  `steal` float DEFAULT NULL,
  `blocked` float DEFAULT NULL,
  `shooting_max` float DEFAULT NULL,
  `speed_max` float DEFAULT NULL,
  `strength_max` float DEFAULT NULL,
  `bounce_max` float DEFAULT NULL,
  `stamina_max` float DEFAULT NULL,
  `trisection_max` float DEFAULT NULL,
  `dribble_max` float DEFAULT NULL,
  `pass_max` float DEFAULT NULL,
  `backboard_max` float DEFAULT NULL,
  `steal_max` float DEFAULT NULL,
  `blocked_max` float DEFAULT NULL,
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  `picture` smallint(6) DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  UNIQUE KEY `team_player_no` (`player_no`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`)
) ENGINE=InnoDB AUTO_INCREMENT=102 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2010-04-08 23:08:26
