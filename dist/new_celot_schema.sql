CREATE DATABASE  IF NOT EXISTS `celot_db` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `celot_db`;
-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: celot_db
-- ------------------------------------------------------
-- Server version	5.5.46

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
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `admin` (
  `AdminNo` int(11) NOT NULL AUTO_INCREMENT,
  `AdminGroupNo` int(11) DEFAULT NULL,
  `Id` varchar(45) DEFAULT NULL,
  `Password` varchar(45) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `AdminRegDate` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`AdminNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `admin_group`
--

DROP TABLE IF EXISTS `admin_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `admin_group` (
  `AdminGroupNo` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(45) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Des` text,
  PRIMARY KEY (`AdminGroupNo`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `device`
--

DROP TABLE IF EXISTS `device`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `device` (
  `DeviceNo` int(11) NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(100) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `SerialNo` int(11) DEFAULT NULL,
  `SecuCode` varchar(9) DEFAULT NULL,
  `RouterIp` varchar(45) DEFAULT NULL,
  `Latitude` varchar(50) DEFAULT NULL,
  `Longitude` varchar(50) DEFAULT NULL,
  `Des` text,
  `PhoneNumber` int(11) NOT NULL,
  `SmsSupport` int(11) DEFAULT NULL COMMENT '0 : supported \n1 : unsupported',
  `BatterySupport` int(11) DEFAULT NULL COMMENT '0 : supported \n1 : unsupported',
  `WifiSupport` int(11) DEFAULT NULL COMMENT '0 : supported \n1 : unsupported',
  `VpnSupport` int(11) DEFAULT NULL COMMENT '0 : supported \n1 : unsupported',
  `ResetTime` int(11) DEFAULT '0',
  `AlertStatus` int(11) DEFAULT '0' COMMENT '1 : normal\n2 : alert\n3 : noting\n',
  `AlertOccurentTime` varchar(50) DEFAULT 'NA' COMMENT '??? ??? ?? ???',
  `DeviceRegDate` int(11) DEFAULT '0',
  PRIMARY KEY (`DeviceNo`,`PhoneNumber`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `packet`
--

DROP TABLE IF EXISTS `packet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `packet` (
  `PacketNo` int(11) NOT NULL AUTO_INCREMENT,
  `SessionId` int(11) DEFAULT NULL COMMENT '010, 012 ? ??? 8?? ?? phone_number',
  `Identifier` int(11) DEFAULT NULL COMMENT '???? ???',
  `RawPacket` varbinary(2000) DEFAULT NULL COMMENT '???? ?? raw packet ???? \n',
  `PacketRegDate` int(11) DEFAULT NULL,
  PRIMARY KEY (`PacketNo`)
) ENGINE=InnoDB AUTO_INCREMENT=72 DEFAULT CHARSET=utf8 COMMENT='?? ???? ??? ?? ?? ???';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `packet_log`
--

DROP TABLE IF EXISTS `packet_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `packet_log` (
  `PacketLogNo` int(11) NOT NULL AUTO_INCREMENT,
  `SessionId` int(11) DEFAULT NULL COMMENT '010, 012 ? ??? 8?? ?? phone_number',
  `Identifier` int(11) DEFAULT NULL COMMENT '???? ???\n',
  `RawPacket` varbinary(2000) DEFAULT NULL COMMENT '???? ?? raw packet ???? \n',
  `PacketLogRegDate` int(11) DEFAULT NULL,
  PRIMARY KEY (`PacketLogNo`)
) ENGINE=InnoDB AUTO_INCREMENT=7158 DEFAULT CHARSET=utf8 COMMENT='?? ???? ??? ?? ?? ???';
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-05-28 22:04:26
