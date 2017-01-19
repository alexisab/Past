/*
Navicat MySQL Data Transfer

Source Server         : dev
Source Server Version : 50714
Source Host           : localhost:3306
Source Database       : past

Target Server Type    : MYSQL
Target Server Version : 50714
File Encoding         : 65001

Date: 2017-01-19 05:14:20
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Login` mediumtext NOT NULL,
  `Password` mediumtext NOT NULL,
  `Ticket` mediumtext,
  `Nickname` mediumtext,
  `Role` tinyint(3) NOT NULL DEFAULT '20',
  `SecretQuestion` mediumtext,
  `SecretAnswer` mediumtext,
  `BannedUntil` datetime DEFAULT NULL,
  `LastConnection` datetime DEFAULT NULL,
  `LastIp` mediumtext,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('1', 'Test', '098f6bcd4621d373cade4e832627b4f6', 'FGLABAOWASSYCELSRPWGUZYUBTUKCBZX', 'admin', '4', 'Delete ?', 'Yes', '2016-10-04 11:42:16', '2017-01-19 04:12:15', '127.0.0.1');
INSERT INTO `accounts` VALUES ('2', 'Test2', '098f6bcd4621d373cade4e832627b4f6', 'THRSUEVYPFGMMIIQJIUOYWRSMMFOWOJH', 'admin2', '1', 'Delete ?', 'Yes', '2016-10-04 11:42:16', '2017-01-13 04:28:29', '127.0.0.1');
INSERT INTO `accounts` VALUES ('3', 'Test3', '098f6bcd4621d373cade4e832627b4f6', 'EBQXYBISBNCLLLJFMVSLKAUBDNLNDAZK', 'admin3', '4', 'Delete ?', 'Yes', '2016-10-04 11:42:16', '2016-12-04 21:58:27', '127.0.0.1');

-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OwnerId` int(11) NOT NULL,
  `Name` mediumtext NOT NULL,
  `Level` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `Experience` bigint(20) NOT NULL DEFAULT '0',
  `Breed` tinyint(3) NOT NULL,
  `EntityLookString` mediumtext NOT NULL,
  `Sex` tinyint(1) NOT NULL,
  `MapId` int(11) NOT NULL,
  `CellId` smallint(6) NOT NULL,
  `Direction` tinyint(3) NOT NULL,
  `SpawnMapId` int(11) DEFAULT NULL,
  `Health` int(11) DEFAULT '55',
  `Energy` smallint(6) DEFAULT '10000',
  `AP` tinyint(3) unsigned DEFAULT '6',
  `MP` tinyint(3) unsigned DEFAULT '3',
  `Strength` int(11) DEFAULT '0',
  `Vitality` int(11) DEFAULT '0',
  `Wisdom` int(11) DEFAULT '0',
  `Chance` int(11) DEFAULT '0',
  `Agility` int(11) DEFAULT '0',
  `Intelligence` int(11) DEFAULT '0',
  `AlignementSide` tinyint(3) NOT NULL DEFAULT '0',
  `Honor` smallint(5) unsigned DEFAULT '0',
  `Dishonor` smallint(5) unsigned DEFAULT '0',
  `PvPEnabled` tinyint(1) DEFAULT '0',
  `Kamas` int(11) DEFAULT '0',
  `StatsPoints` smallint(6) DEFAULT '0',
  `SpellsPoints` smallint(6) DEFAULT '0',
  `LastUsage` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=82 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of characters
-- ----------------------------
INSERT INTO `characters` VALUES ('31', '1', 'Skeezr', '1', '110', '1', '{1|11,420,197,353|1=8089936,2=14036310,3=770001,4=1476050,5=15483569,6=15483569|125|1@0={264|||80},6@0={170|||125}}', '1', '131883', '396', '1', null, '55', '10000', '6', '3', '0', '0', '30', '20', '0', '0', '2', '1754', '0', '0', '0', '1287', '3', '2016-12-11 17:40:55');
INSERT INTO `characters` VALUES ('56', '2', 'Asinaki', '1', '0', '2', '{1048|||100}', '0', '21757956', '156', '3', null, '55', '10000', '6', '3', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2017-01-13 04:28:29');
INSERT INTO `characters` VALUES ('76', '1', 'Ogetanifow', '1', '0', '2', '{1|21||125|6@0={170|||125}}', '1', '21761028', '159', '2', '2323', '55', '10000', '6', '3', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '300', '0', '0', '2017-01-19 04:12:15');
INSERT INTO `characters` VALUES ('77', '2', 'Emoqak', '1', '0', '7', '{1|71||125}', '1', '21757955', '312', '1', null, '55', '10000', '6', '3', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2016-11-24 02:54:01');
INSERT INTO `characters` VALUES ('78', '3', 'Ebowage', '1', '0', '3', '{1|31||95}', '1', '21757955', '428', '4', null, '55', '10000', '6', '3', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2016-12-04 21:58:27');

-- ----------------------------
-- Table structure for characters_spell
-- ----------------------------
DROP TABLE IF EXISTS `characters_spell`;
CREATE TABLE `characters_spell` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OwnerId` int(11) NOT NULL,
  `Position` tinyint(4) unsigned NOT NULL,
  `SpellId` int(11) NOT NULL,
  `Level` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=121 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of characters_spell
-- ----------------------------
INSERT INTO `characters_spell` VALUES ('1', '31', '64', '0', '1');
INSERT INTO `characters_spell` VALUES ('2', '31', '65', '17', '2');
INSERT INTO `characters_spell` VALUES ('3', '31', '66', '6', '1');
INSERT INTO `characters_spell` VALUES ('4', '31', '67', '3', '1');
INSERT INTO `characters_spell` VALUES ('20', '56', '67', '34', '1');
INSERT INTO `characters_spell` VALUES ('19', '56', '66', '23', '1');
INSERT INTO `characters_spell` VALUES ('18', '56', '65', '21', '1');
INSERT INTO `characters_spell` VALUES ('17', '56', '64', '0', '1');
INSERT INTO `characters_spell` VALUES ('100', '76', '67', '34', '1');
INSERT INTO `characters_spell` VALUES ('99', '76', '66', '23', '1');
INSERT INTO `characters_spell` VALUES ('98', '76', '65', '21', '1');
INSERT INTO `characters_spell` VALUES ('97', '76', '64', '0', '1');
INSERT INTO `characters_spell` VALUES ('104', '77', '67', '128', '1');
INSERT INTO `characters_spell` VALUES ('103', '77', '66', '121', '1');
INSERT INTO `characters_spell` VALUES ('102', '77', '65', '125', '1');
INSERT INTO `characters_spell` VALUES ('101', '77', '64', '0', '1');
INSERT INTO `characters_spell` VALUES ('105', '78', '64', '0', '1');
INSERT INTO `characters_spell` VALUES ('106', '78', '65', '51', '1');
INSERT INTO `characters_spell` VALUES ('107', '78', '66', '41', '1');
INSERT INTO `characters_spell` VALUES ('108', '78', '67', '43', '1');
