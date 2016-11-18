/*
Navicat MySQL Data Transfer

Source Server         : dev
Source Server Version : 50709
Source Host           : localhost:3306
Source Database       : past

Target Server Type    : MYSQL
Target Server Version : 50709
File Encoding         : 65001

Date: 2016-11-18 18:44:22
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Login` mediumtext CHARACTER SET utf8 NOT NULL,
  `Password` mediumtext CHARACTER SET utf8 NOT NULL,
  `Ticket` mediumtext,
  `Nickname` mediumtext CHARACTER SET utf8,
  `HasRights` tinyint(1) DEFAULT NULL,
  `SecretQuestion` mediumtext CHARACTER SET utf8,
  `SecretAnswer` mediumtext CHARACTER SET utf8,
  `BannedUntil` datetime DEFAULT NULL,
  `LastConnection` datetime DEFAULT NULL,
  `LastIp` mediumtext CHARACTER SET utf8,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('1', 'Test', '098f6bcd4621d373cade4e832627b4f6', 'FXVWWKMIMLQYICJBJZBTGNYEHYUATNWW', 'admin', '1', 'Delete ?', 'Yes', '2016-10-04 11:42:16', '2016-11-17 03:14:23', '127.0.0.1');
INSERT INTO `accounts` VALUES ('2', 'Test2', '098f6bcd4621d373cade4e832627b4f6', 'YMGOOXPSQCRJPDYEQAAYFCSDWOXIGKQA', 'admin2', '1', 'Delete ?', 'Yes', '2016-10-04 11:42:16', '2016-11-17 03:13:10', '127.0.0.1');
INSERT INTO `accounts` VALUES ('3', 'Test3', '098f6bcd4621d373cade4e832627b4f6', 'VIOYHCDMHWBGVNVWOXBIOYIEAGIXNZES', 'admin3', '1', 'Delete ?', 'Yes', '2016-10-04 11:42:16', '2016-11-04 07:57:14', '127.0.0.1');

-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OwnerId` int(11) NOT NULL,
  `Name` mediumtext CHARACTER SET utf8 NOT NULL,
  `Level` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `Experience` bigint(20) NOT NULL DEFAULT '0',
  `Breed` tinyint(3) NOT NULL,
  `EntityLookString` mediumtext CHARACTER SET utf8 NOT NULL,
  `Sex` tinyint(1) NOT NULL,
  `MapId` int(11) NOT NULL,
  `CellId` smallint(6) NOT NULL,
  `Direction` tinyint(3) NOT NULL,
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
) ENGINE=MyISAM AUTO_INCREMENT=59 DEFAULT CHARSET=latin1 COLLATE=latin1_bin;

-- ----------------------------
-- Records of characters
-- ----------------------------
INSERT INTO `characters` VALUES ('31', '1', 'Skeezr', '1', '0', '1', '{1|11,420|1=8089936,2=14036310,3=770001,4=1476050,5=15483569|125}', '1', '131883', '172', '0', '55', '10000', '6', '3', '0', '0', '0', '0', '0', '0', '2', '1754', '0', '0', '0', '0', '2', '2016-11-17 03:49:59');
INSERT INTO `characters` VALUES ('56', '2', 'Asinaki', '1', '0', '2', '{1|20||130}', '0', '131883', '340', '0', '55', '10000', '6', '3', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2016-11-17 03:13:10');

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
) ENGINE=MyISAM AUTO_INCREMENT=29 DEFAULT CHARSET=latin1;

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
