-- MySqlBackup.NET 2.6.5.0
-- Dump Time: 2569-03-07 12:01:27
-- --------------------------------------
-- Server version 8.0.45 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb3 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of __efmigrationshistory
-- 

DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table __efmigrationshistory
-- 

/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory`(`MigrationId`,`ProductVersion`) VALUES('20260118172304_AddFacebookPageConfigs','9.0.0');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

-- 
-- Definition of affiliatevisits
-- 

DROP TABLE IF EXISTS `affiliatevisits`;
CREATE TABLE IF NOT EXISTS `affiliatevisits` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AffiliateId` varchar(50) NOT NULL,
  `VisitDate` date NOT NULL,
  `VisitCount` int DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `AffiliateId` (`AffiliateId`,`VisitDate`)
) ENGINE=InnoDB AUTO_INCREMENT=938 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table affiliatevisits
-- 

/*!40000 ALTER TABLE `affiliatevisits` DISABLE KEYS */;
INSERT INTO `affiliatevisits`(`Id`,`AffiliateId`,`VisitDate`,`VisitCount`) VALUES(1,'Boss','2026-01-15 00:00:00',52),(18,'PakRob','2026-01-15 00:00:00',11),(64,'PakRob','2026-01-16 00:00:00',15),(65,'Boss','2026-01-16 00:00:00',16),(95,'PakRob','2026-01-17 00:00:00',6),(97,'Boss','2026-01-17 00:00:00',5),(106,'Boss','2026-01-18 00:00:00',26),(107,'PakRob','2026-01-18 00:00:00',3),(135,'Boss','2026-01-19 00:00:00',15),(140,'PakRob','2026-01-19 00:00:00',2),(152,'Boss','2026-01-20 00:00:00',21),(173,'Boss','2026-01-21 00:00:00',22),(176,'PakRob','2026-01-21 00:00:00',2),(197,'Boss','2026-01-22 00:00:00',40),(212,'PakRob','2026-01-22 00:00:00',4),(241,'PakRob','2026-01-23 00:00:00',3),(244,'Boss','2026-01-23 00:00:00',1),(245,'PakRob','2026-01-24 00:00:00',2),(246,'Boss','2026-01-24 00:00:00',73),(296,'FAII','2026-01-24 00:00:00',3),(324,'Boss','2026-01-25 00:00:00',68),(336,'FAII','2026-01-25 00:00:00',3),(357,'PakRob','2026-01-25 00:00:00',3),(399,'Boss','2026-01-26 00:00:00',55),(413,'FAII','2026-01-26 00:00:00',1),(455,'Boss','2026-01-27 00:00:00',23),(460,'PakRob','2026-01-27 00:00:00',4),(474,'FAII','2026-01-27 00:00:00',1),(483,'Boss','2026-01-28 00:00:00',7),(486,'PakRob','2026-01-28 00:00:00',2),(492,'PakRob','2026-01-29 00:00:00',4),(493,'Boss','2026-01-29 00:00:00',7),(503,'Boss','2026-01-30 00:00:00',18),(510,'PakRob','2026-01-30 00:00:00',1),(523,'Boss','2026-01-31 00:00:00',15),(529,'PakRob','2026-01-31 00:00:00',4),(542,'Boss','2026-02-01 00:00:00',5),(546,'PakRob','2026-02-01 00:00:00',3),(550,'Boss','2026-02-02 00:00:00',9),(553,'PakRob','2026-02-02 00:00:00',6),(566,'Boss','2026-02-03 00:00:00',3),(570,'Boss','2026-02-04 00:00:00',4),(575,'Boss','2026-02-05 00:00:00',17),(577,'PakRob','2026-02-05 00:00:00',5),(598,'Boss','2026-02-06 00:00:00',40),(638,'PakRob','2026-02-06 00:00:00',1),(639,'Boss','2026-02-07 00:00:00',14),(641,'F','2026-02-07 00:00:00',3),(656,'Boss','2026-02-08 00:00:00',2),(658,'PakRob','2026-02-09 00:00:00',6),(664,'Boss','2026-02-09 00:00:00',15),(679,'Boss','2026-02-10 00:00:00',7),(681,'F','2026-02-10 00:00:00',2),(688,'PakRob','2026-02-11 00:00:00',3),(689,'Boss','2026-02-11 00:00:00',4),(692,'F','2026-02-11 00:00:00',3),(698,'Boss','2026-02-12 00:00:00',6),(700,'F','2026-02-12 00:00:00',1),(705,'Boss','2026-02-13 00:00:00',14),(719,'Boss','2026-02-14 00:00:00',3),(721,'F','2026-02-14 00:00:00',1),(723,'PakRob','2026-02-14 00:00:00',1),(724,'Boss','2026-02-15 00:00:00',5),(725,'F','2026-02-15 00:00:00',2),(731,'Boss','2026-02-16 00:00:00',2),(732,'F','2026-02-16 00:00:00',3),(733,'PakRob','2026-02-16 00:00:00',1),(737,'F','2026-02-17 00:00:00',2),(739,'Boss','2026-02-17 00:00:00',2),(741,'Boss','2026-02-18 00:00:00',23),(764,'Boss','2026-02-19 00:00:00',6),(765,'F','2026-02-19 00:00:00',2),(772,'Boss','2026-02-20 00:00:00',4),(776,'F','2026-02-20 00:00:00',1),(777,'FAII','2026-02-21 00:00:00',3),(779,'PakRob','2026-02-21 00:00:00',1),(781,'Boss','2026-02-21 00:00:00',2),(783,'FAII','2026-02-22 00:00:00',7),(784,'Boss','2026-02-22 00:00:00',4),(794,'FAII','2026-02-23 00:00:00',10),(796,'PakRob','2026-02-23 00:00:00',3),(802,'Boss','2026-02-23 00:00:00',3),(810,'Boss','2026-02-24 00:00:00',5),(811,'FAII','2026-02-24 00:00:00',21),(824,'F','2026-02-24 00:00:00',3),(839,'FAII','2026-02-25 00:00:00',13),(843,'Boss','2026-02-25 00:00:00',3),(855,'FAII','2026-02-26 00:00:00',7),(856,'Boss','2026-02-26 00:00:00',3),(860,'F','2026-02-26 00:00:00',1),(866,'Boss','2026-02-27 00:00:00',1),(867,'FAII','2026-02-27 00:00:00',2),(869,'FAII','2026-02-28 00:00:00',5),(874,'FAII','2026-03-01 00:00:00',6),(878,'Boss','2026-03-01 00:00:00',1),(881,'F','2026-03-01 00:00:00',1),(882,'FAII','2026-03-02 00:00:00',5),(883,'Boss','2026-03-02 00:00:00',3),(886,'F','2026-03-02 00:00:00',2),(892,'F','2026-03-03 00:00:00',2),(893,'FAII','2026-03-03 00:00:00',4),(898,'FAII','2026-03-04 00:00:00',4),(902,'FAII','2026-03-05 00:00:00',3),(903,'Boss','2026-03-05 00:00:00',8),(906,'F','2026-03-05 00:00:00',1),(914,'F','2026-03-06 00:00:00',1),(915,'Boss','2026-03-06 00:00:00',4),(917,'FAII','2026-03-06 00:00:00',1),(920,'Boss','2026-03-07 00:00:00',18);
/*!40000 ALTER TABLE `affiliatevisits` ENABLE KEYS */;

-- 
-- Definition of appsettings
-- 

DROP TABLE IF EXISTS `appsettings`;
CREATE TABLE IF NOT EXISTS `appsettings` (
  `SettingKey` varchar(255) NOT NULL,
  `SettingValue` longtext,
  PRIMARY KEY (`SettingKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table appsettings
-- 

/*!40000 ALTER TABLE `appsettings` DISABLE KEYS */;
INSERT INTO `appsettings`(`SettingKey`,`SettingValue`) VALUES('MaintenanceMode','true');
/*!40000 ALTER TABLE `appsettings` ENABLE KEYS */;

-- 
-- Definition of customers
-- 

DROP TABLE IF EXISTS `customers`;
CREATE TABLE IF NOT EXISTS `customers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `PhoneNumber` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `LineId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `FacebookId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 
-- Dumping data for table customers
-- 

/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers`(`Id`,`Name`,`PhoneNumber`,`LineId`,`FacebookId`) VALUES(1,'ลูกค้าใหม่ (Voice)','089-999-8888',NULL,NULL);
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;

-- 
-- Definition of facebook_page_configs
-- 

DROP TABLE IF EXISTS `facebook_page_configs`;
CREATE TABLE IF NOT EXISTS `facebook_page_configs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AdminNote` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PageId` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PageAccessToken` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `LastUpdated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table facebook_page_configs
-- 

/*!40000 ALTER TABLE `facebook_page_configs` DISABLE KEYS */;
INSERT INTO `facebook_page_configs`(`Id`,`AdminNote`,`PageId`,`PageAccessToken`,`IsActive`,`LastUpdated`) VALUES(1,'น้ำปลาร้าสมหวัง','312937111912926','EAAa2HZAWsuaEBQlO78ayEUB3epAtTtReBgNZAvzhjOobLhr3qECr7M9EO4reIqPUsEDp0LvFMj0KyLjOVyO07jG6b1aKwLw1CxB499ZAptfuc6hZC2mG6nAild1Jmlb55bZBEmVSN7sWVz1PvL6Nv9ict0VZB2dSzfTs1kcoWh2aRKZAXJGZCmfnAXVZCZCCNQ7dGBgZCowqhKUeQZDZD',1,'2026-01-30 10:04:20.820137'),(2,'TestMypage','896951066842556','EAAVZCagQnxUwBQkli7deC5vGiHwnVkXwIv3YsZCYHFrPrlCb5eGrCxQkC8uwC3skcd36oZCZCAuFi5MMTbjhKZBAFZAgMbYXNkdiwzpeJXla0f6fwiHWShpp3SpCff63LrZB0ibG8K1mvA6XEZCLGkT8tulIvNGIxMg5vyYEzy6tPWotTC34gwrZBH5vIZBcNiwBZCV1ZAxiQKLeWwZDZD',0,'2026-01-25 21:53:34.212065'),(4,'สมหวังลูกชิ้นเนื้อแท้','108997935149830','333',0,'2026-01-20 16:57:54.202075');
/*!40000 ALTER TABLE `facebook_page_configs` ENABLE KEYS */;

-- 
-- Definition of auto_reply_items
-- 

DROP TABLE IF EXISTS `auto_reply_items`;
CREATE TABLE IF NOT EXISTS `auto_reply_items` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FacebookPageConfigId` int NOT NULL,
  `Keywords` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ReplyMessage` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ReplyType` varchar(50) DEFAULT 'text',
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `IX_auto_reply_items_FacebookPageConfigId` (`FacebookPageConfigId`),
  CONSTRAINT `FK_auto_reply_items_facebook_page_configs_FacebookPageConfigId` FOREIGN KEY (`FacebookPageConfigId`) REFERENCES `facebook_page_configs` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table auto_reply_items
-- 

/*!40000 ALTER TABLE `auto_reply_items` DISABLE KEYS */;
INSERT INTO `auto_reply_items`(`Id`,`FacebookPageConfigId`,`Keywords`,`ReplyMessage`,`ReplyType`,`IsActive`) VALUES(4,1,'ราคา','น้ำปลาร้าสมหวัง ขวดละ 40 บาทครับ 3 ขวด ขึ้นไปส่งฟรี\n....................................................................\nสั่งซื้อง่ายๆ กับ Somwhangshop ใน 4 ขั้นตอนครับ! ?\n\n1. เลือกสินค้า ที่ถูกใจลงตะกร้า ?\n2. ระบุที่อยู่ และเบอร์โทรให้ครบถ้วน ?\n3. โอนเงิน & แนบสลิป เพื่อยืนยันออเดอร์ ?\n4. รอรับสินค้า ส่งไวถึงหน้าบ้าน ?\n\n? สั่งซื้อน้ำปลาร้า: https://somwhangshop.com/shop/fish_sauce\n? เช็คสถานะคำสั่งซื้อด้วยเบอร์โทร:\n? https://somwhangshop.com/tracking','text',1),(5,4,'สนใจ','? วิธีสั่งซื้อง่ายๆ ด้วยตัวเอง:\n1. กดลิงก์เว็บไซต์ด้านล่าง\n2. เลือกสินค้าและระบุที่อยู่\n3. โอนเงินและแนบสลิปในเว็บได้ทันที\n✨ (ระบบจะตรวจสอบและแจ้งเลขพัสดุให้อัตโนมัติค่ะ)\n\n? สั่งซื้อคลิกเลย: https://somwhangshop.com','text',1),(6,2,'ทดสอบ','test','text',1),(7,1,'สนใจ','น้ำปลาร้าสมหวัง ขวดละ 40 บาทครับ 3 ขวด ขึ้นไปส่งฟรี\n....................................................................\nสั่งซื้อง่ายๆ กับ Somwhangshop ใน 4 ขั้นตอนครับ! ?\n\n1. เลือกสินค้า ที่ถูกใจลงตะกร้า ?\n2. ระบุที่อยู่ และเบอร์โทรให้ครบถ้วน ?\n3. โอนเงิน & แนบสลิป เพื่อยืนยันออเดอร์ ?\n4. รอรับสินค้า ส่งไวถึงหน้าบ้าน ?\n\n? สั่งซื้อน้ำปลาร้า: https://somwhangshop.com/shop/fish_sauce\n? เช็คสถานะคำสั่งซื้อด้วยเบอร์โทร:\n? https://somwhangshop.com/tracking\n','text',1),(8,2,'สนใจ','https://somwhangshop.com/shop/meatball','text',1),(9,1,'โปร','ของดีราคาโดน! 3 ขวด 100 เท่านั้น ใครยังไม่มี รีบคอมเม้นเลย!\nใต้โพสนี้เท่านั้น https://www.facebook.com/share/v/1E1xyAsDsY/','text',1),(10,1,'ตัวแทน','หากสนใจเป็นตัวแทน แจ้งรายละเอียดพื้นที่ขาย และเบอร์ติดต่อได้เลยค่ะ','text',1),(11,1,'แนะนำ','ปรกติ ขวดละ 40 บาท 3 ขวดส่งฟรี คละสูตรได้ค่ะ\n.............................................................................................................................\nโปรพิเศษ สำหรับลูกค้าที่กดไลท์กดแชร์คอมเมนต์ใต้โพสนี้\nของดีราคาโดน! 3 ขวด 100  ส่งฟรี  ใครยังไม่มี รีบคอมเม้นเลย!\nใต้โพสนี้เท่านั้น https://www.facebook.com/share/v/1E1xyAsDsY/','text',1);
/*!40000 ALTER TABLE `auto_reply_items` ENABLE KEYS */;

-- 
-- Definition of linebotconfigs
-- 

DROP TABLE IF EXISTS `linebotconfigs`;
CREATE TABLE IF NOT EXISTS `linebotconfigs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AdminNote` longtext,
  `ChannelAccessToken` longtext,
  `ChannelSecret` longtext,
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `LastUpdated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table linebotconfigs
-- 

/*!40000 ALTER TABLE `linebotconfigs` DISABLE KEYS */;
/*!40000 ALTER TABLE `linebotconfigs` ENABLE KEYS */;

-- 
-- Definition of lineautoreplyitems
-- 

DROP TABLE IF EXISTS `lineautoreplyitems`;
CREATE TABLE IF NOT EXISTS `lineautoreplyitems` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `LineBotConfigId` int NOT NULL,
  `Keywords` longtext,
  `ReplyMessage` longtext,
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `FK_LineAutoReplyItems_LineBotConfigs_LineBotConfigId` (`LineBotConfigId`),
  CONSTRAINT `FK_LineAutoReplyItems_LineBotConfigs_LineBotConfigId` FOREIGN KEY (`LineBotConfigId`) REFERENCES `linebotconfigs` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table lineautoreplyitems
-- 

/*!40000 ALTER TABLE `lineautoreplyitems` DISABLE KEYS */;
/*!40000 ALTER TABLE `lineautoreplyitems` ENABLE KEYS */;

-- 
-- Definition of orderitems
-- 

DROP TABLE IF EXISTS `orderitems`;
CREATE TABLE IF NOT EXISTS `orderitems` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OrderId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(65,2) NOT NULL,
  `Quantity` int NOT NULL,
  `TotalPrice` decimal(65,2) NOT NULL,
  `ProductId` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=105 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table orderitems
-- 

/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
INSERT INTO `orderitems`(`Id`,`OrderId`,`ProductName`,`Price`,`Quantity`,`TotalPrice`,`ProductId`) VALUES(6,'ORD-20251230110936','ลูกชิ้นเอ็นหมู Kg.',150.00,1,150.00,6),(7,'ORD-20260109181728','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(21,'ORD-260113170447','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(22,'ORD-260113170447','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(23,'ORD-260113194726','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(24,'ORD-260113194726','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(25,'ORD-260113201615','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(26,'ORD-260114152944','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(30,'ORD-260114152945','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(31,'ORD-260114152945','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(32,'ORD-260114152945','น้ำจิ้มมะขามแท้ 280 ml.',50.00,1,50.00,5),(33,'ORD-260116095710','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(34,'ORD-260116112958','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(35,'ORD-260116112958','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(40,'ORD-260122092500','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(41,'ORD-260122092500','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(42,'ORD-260122092500','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(43,'ORD-260124094348','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(44,'ORD-260124094348','ลูกชิ้นเอ็นหมู Kg.',180.00,3,540.00,6),(45,'ORD-260124183847','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(46,'ORD-260124183847','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(47,'ORD-260124183847','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(48,'ORD-260124214857','น้ำปลาร้า สูตรผสมน้ำปู',40.00,3,120.00,11),(49,'ORD-260124214857','น้ำปลาร้า สูตรออริจินอล',40.00,3,120.00,12),(50,'ORD-260124224624','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(51,'ORD-260124224624','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(52,'ORD-260125003855','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(53,'ORD-260125003855','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(54,'ORD-260125003855','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(55,'ORD-260125211208','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(56,'ORD-260125211208','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(57,'ORD-260125211208','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(58,'ORD-260126190626','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(60,'ORD-260203161637','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(61,'ORD-260203161637','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(62,'ORD-260203161637','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(63,'ORD-260203205148','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,3,540.00,7),(64,'ORD-260204131331','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(65,'ORD-260204131331','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(66,'ORD-260206090422','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(67,'ORD-260206090422','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(68,'ORD-260206234315','ลูกชิ้นเอ็นหมู Kg.',180.00,2,360.00,6),(69,'ORD-260206234315','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,3,540.00,7),(70,'ORD-260206235107','ลูกชิ้นเอ็นหมู Kg.',180.00,6,1080.00,6),(72,'ORD-260207094001','ลูกชิ้นเอ็นหมู Kg.',180.00,4,720.00,6),(73,'ORD-260213171931','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(74,'ORD-260213171931','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(75,'ORD-260214095950','ลูกชิ้นเอ็นหมู Kg.',180.00,2,360.00,6),(76,'ORD-260214095950','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(77,'ORD-260217105904','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(78,'ORD-260217105904','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(79,'ORD-260219115634','ลูกชิ้นเอ็นหมู Kg.',180.00,2,360.00,6),(80,'ORD-260219115634','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(81,'ORD-690222121637','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(82,'ORD-690224165304','ลูกชิ้นเอ็นหมู Kg.',180.00,2,360.00,6),(83,'ORD-690224165304','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(84,'ORD-690226090601','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(85,'ORD-690228181543','ลูกชิ้นเอ็นหมู Kg.',180.00,2,360.00,6),(86,'ORD-690228181543','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(87,'ORD-690301221028','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(88,'ORD-690301221028','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(89,'ORD-690301222027','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(90,'ORD-690301222027','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(91,'ORD-690302085757','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(92,'ORD-690302085757','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(93,'ORD-690302085757','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(94,'ORD-690302190002','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(95,'ORD-690302190002','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(96,'ORD-690302191041','น้ำจิ้มมะขามแท้ 280 ml.',50.00,2,100.00,5),(97,'ORD-690303135829','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(98,'ORD-690303135829','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(99,'ORD-690305113346','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8),(100,'ORD-690305113346','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,2,360.00,7),(101,'ORD-690306112911','ลูกชิ้นเอ็นหมู Kg.',180.00,3,540.00,6),(102,'ORD-690307113914','ลูกชิ้นเอ็นหมู Kg.',180.00,1,180.00,6),(103,'ORD-690307113914','ลูกชิ้นเอ็นเนื้อ Kg.',180.00,1,180.00,7),(104,'ORD-690307113914','ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,1,180.00,8);
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;

-- 
-- Definition of orders
-- 

DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `OrderId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrderDate` datetime(2) NOT NULL,
  `TotalAmount` decimal(65,2) NOT NULL,
  `CustomerName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ShippingAddress` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SlipImageUrl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext,
  `TransRef` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AffiliateId` varchar(255) DEFAULT NULL,
  `TrackingNumber` longtext CHARACTER SET big5 COLLATE big5_chinese_ci,
  `ShippingCost` decimal(10,2) NOT NULL DEFAULT '0.00',
  `CourierName` varchar(50) DEFAULT NULL,
  `Channel` varchar(20) DEFAULT 'Web',
  `ActualShippingCost` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`OrderId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table orders
-- 

/*!40000 ALTER TABLE `orders` DISABLE KEYS */;

/*!40000 ALTER TABLE `orders` ENABLE KEYS */;

-- 
-- Definition of productviews
-- 

DROP TABLE IF EXISTS `productviews`;
CREATE TABLE IF NOT EXISTS `productviews` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProductId` int NOT NULL,
  `ViewCount` int DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `ProductId` (`ProductId`)
) ENGINE=InnoDB AUTO_INCREMENT=8411 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table productviews
-- 

/*!40000 ALTER TABLE `productviews` DISABLE KEYS */;
INSERT INTO `productviews`(`Id`,`ProductId`,`ViewCount`) VALUES(1,7,1959),(3,6,1965),(4,5,1944),(9,8,1970),(171,11,282),(172,12,282),(5323,13,2),(5324,14,2),(5325,15,2),(5326,16,2);
/*!40000 ALTER TABLE `productviews` ENABLE KEYS */;

-- 
-- Definition of shippingconfigs
-- 

DROP TABLE IF EXISTS `shippingconfigs`;
CREATE TABLE IF NOT EXISTS `shippingconfigs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ShopId` int NOT NULL,
  `CourierName` varchar(50) NOT NULL,
  `BaseRate` decimal(10,2) NOT NULL DEFAULT '40.00',
  `FreeShippingThreshold` decimal(10,2) NOT NULL DEFAULT '500.00',
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table shippingconfigs
-- 

/*!40000 ALTER TABLE `shippingconfigs` DISABLE KEYS */;
INSERT INTO `shippingconfigs`(`Id`,`ShopId`,`CourierName`,`BaseRate`,`FreeShippingThreshold`,`IsActive`) VALUES(1,1,'Flash',50.00,5000.00,1),(2,1,'SPX',50.00,5000.00,1),(3,1,'DHL',60.00,5000.00,1),(4,3,'Flash',40.00,3000.00,1),(5,3,'SPX',40.00,3000.00,1),(6,3,'DHL',60.00,5000.00,1);
/*!40000 ALTER TABLE `shippingconfigs` ENABLE KEYS */;

-- 
-- Definition of shippingtiers
-- 

DROP TABLE IF EXISTS `shippingtiers`;
CREATE TABLE IF NOT EXISTS `shippingtiers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ShopId` int NOT NULL,
  `CourierName` varchar(50) NOT NULL,
  `MinWeight` decimal(10,2) NOT NULL,
  `MaxWeight` decimal(10,2) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=163 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table shippingtiers
-- 

/*!40000 ALTER TABLE `shippingtiers` DISABLE KEYS */;
INSERT INTO `shippingtiers`(`Id`,`ShopId`,`CourierName`,`MinWeight`,`MaxWeight`,`Price`) VALUES(145,1,'Flash',0.00,1.20,50.00),(146,1,'SPX',0.00,1.20,50.00),(147,1,'DHL',0.00,1.20,60.00),(148,1,'Flash',1.20,2.40,70.00),(149,1,'SPX',1.20,2.40,70.00),(150,1,'DHL',1.20,2.40,70.00),(151,1,'Flash',2.40,999.00,0.00),(152,1,'SPX',2.40,999.00,0.00),(153,1,'DHL',2.40,999.00,0.00),(154,3,'Flash',1.00,2.00,50.00),(155,3,'SPX',1.00,2.00,50.00),(156,3,'DHL',1.00,2.00,50.00),(157,3,'Flash',2.00,3.00,50.00),(158,3,'SPX',2.00,3.00,50.00),(159,3,'DHL',2.00,3.00,50.00),(160,3,'Flash',3.00,999.00,0.00),(161,3,'SPX',3.00,999.00,0.00),(162,3,'DHL',3.00,999.00,0.00);
/*!40000 ALTER TABLE `shippingtiers` ENABLE KEYS */;

-- 
-- Definition of shops
-- 

DROP TABLE IF EXISTS `shops`;
CREATE TABLE IF NOT EXISTS `shops` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Description` text,
  `ImageUrl` text,
  `LinkUrl` varchar(255) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `DisplayOrder` int DEFAULT '0',
  `PromptPayId` varchar(50) DEFAULT NULL,
  `PromptPayName` varchar(255) DEFAULT NULL,
  `QrCodeUrl` longtext,
  `CalcType` varchar(20) DEFAULT 'Weight',
  `FreeThreshold` decimal(10,2) DEFAULT '3.00',
  `BaseShippingFee` decimal(10,2) DEFAULT '50.00',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table shops
-- 

/*!40000 ALTER TABLE `shops` DISABLE KEYS */;
INSERT INTO `shops`(`Id`,`Name`,`Description`,`ImageUrl`,`LinkUrl`,`IsActive`,`DisplayOrder`,`PromptPayId`,`PromptPayName`,`QrCodeUrl`,`CalcType`,`FreeThreshold`,`BaseShippingFee`) VALUES(1,'ลูกชิ้นสมหวัง','เอ็นหมู เอ็นเนื้อ เนื้อล้วน','/images/shops/shops_20260128_105742_c9884.png','meatball',1,1,'','สมหวัง ลูกชิ้นเนื้อแท้','/images//shops/shop-meatball-qr.png','Weight',3.00,50.00),(3,'น้ำปลาร้าสมหวัง','อร่อยแซ่บนัวแน่นอนทุกเมนู','/images/shops/shops_20260128_105754_26b04.png','fish_sauce',1,2,'','บจก. สมหวัง น้ำปลาร้า','/images/shops/shops_20260124_171222_dc094.jpg','Quantity',3.00,50.00);
/*!40000 ALTER TABLE `shops` ENABLE KEYS */;

-- 
-- Definition of products
-- 

DROP TABLE IF EXISTS `products`;
CREATE TABLE IF NOT EXISTS `products` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ShopId` int NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Description` text,
  `ImageUrl` longtext,
  `Category` varchar(100) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `StockQuantity` int NOT NULL DEFAULT '100',
  `Weight` decimal(10,2) NOT NULL DEFAULT '1.00',
  `CostPrice` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`Id`),
  KEY `ShopId` (`ShopId`),
  CONSTRAINT `products_ibfk_1` FOREIGN KEY (`ShopId`) REFERENCES `shops` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table products
-- 

/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products`(`Id`,`ShopId`,`Name`,`Price`,`Description`,`ImageUrl`,`Category`,`IsActive`,`StockQuantity`,`Weight`,`CostPrice`) VALUES(5,1,'น้ำจิ้มมะขามแท้ 280 ml.',50.00,'น้ำจิ้มลูกชิ้นขนาด 280 ml','/images/products/products_20260126_115805_c261e.png','น้ำจิ้ม',1,48,0.30,0.00),(6,1,'ลูกชิ้นเอ็นหมู Kg.',180.00,'ลูกชิ้นเอ็นหมู KG.\n','/images/products/products_20260126_171859_4a223.png','ลูกชิ้น',1,94,1.00,0.00),(7,1,'ลูกชิ้นเอ็นเนื้อ Kg.',180.00,'ลูกชิ้นเอ็นเนื้อ KG.\n','/images/products/products_20260126_172552_ae72b.png','ลูกชิ้น',1,90,1.00,0.00),(8,1,'ลูกชิ้นเนื้อล้วน Kg. (เนื้อวัว)',180.00,'ลูกชิ้นเนื้อล้วน KG','/images/products/products_20260126_161637_c80d9.png','ลูกชิ้น',1,101,1.00,0.00),(11,3,'น้ำปลาร้า สูตรผสมน้ำปู',40.00,'','/images/products/products_20260125_222758_afe05.png','เครื่องปรุงรส',1,97,0.40,0.00),(12,3,'น้ำปลาร้า สูตรออริจินอล',40.00,'','/images/products/products_20260125_222900_5ef3e.png','เครื่องปรุงรส',1,97,0.40,0.00),(13,1,'เจลเย็น (Cooling Gel)',0.00,'ขนาดมาตรฐาน 500g','','Packaging',1,-31,0.50,1.00),(14,1,'กล่อง B (เล็ก)',0.00,'บรรจุ 1-2 กก.','','Packaging',1,13,0.20,3.30),(15,1,'กล่อง C (กลาง)',0.00,'บรรจุ 3-5 กก.','','Packaging',1,16,0.30,4.60),(16,1,'กล่อง C+9 (ใหญ่)',0.00,'บรรจุ 6 กก. ขึ้นไป','','Packaging',1,13,0.50,6.40);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;

-- 
-- Definition of sitevisits
-- 

DROP TABLE IF EXISTS `sitevisits`;
CREATE TABLE IF NOT EXISTS `sitevisits` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `VisitDate` date NOT NULL,
  `VisitCount` int DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `VisitDate` (`VisitDate`)
) ENGINE=InnoDB AUTO_INCREMENT=2635 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table sitevisits
-- 

/*!40000 ALTER TABLE `sitevisits` DISABLE KEYS */;
INSERT INTO `sitevisits`(`Id`,`VisitDate`,`VisitCount`) VALUES(1,'2026-01-14 00:00:00',2),(3,'2026-01-15 00:00:00',102),(105,'2026-01-16 00:00:00',52),(157,'2026-01-17 00:00:00',28),(185,'2026-01-18 00:00:00',45),(230,'2026-01-19 00:00:00',29),(259,'2026-01-20 00:00:00',46),(305,'2026-01-21 00:00:00',48),(353,'2026-01-22 00:00:00',85),(438,'2026-01-23 00:00:00',21),(459,'2026-01-24 00:00:00',274),(733,'2026-01-25 00:00:00',208),(941,'2026-01-26 00:00:00',143),(1084,'2026-01-27 00:00:00',76),(1160,'2026-01-28 00:00:00',68),(1228,'2026-01-29 00:00:00',41),(1269,'2026-01-30 00:00:00',73),(1342,'2026-01-31 00:00:00',56),(1398,'2026-02-01 00:00:00',35),(1433,'2026-02-02 00:00:00',53),(1486,'2026-02-03 00:00:00',35),(1521,'2026-02-04 00:00:00',27),(1548,'2026-02-05 00:00:00',72),(1620,'2026-02-06 00:00:00',117),(1737,'2026-02-07 00:00:00',59),(1796,'2026-02-08 00:00:00',17),(1813,'2026-02-09 00:00:00',67),(1880,'2026-02-10 00:00:00',30),(1910,'2026-02-11 00:00:00',32),(1942,'2026-02-12 00:00:00',37),(1979,'2026-02-13 00:00:00',41),(2020,'2026-02-14 00:00:00',20),(2040,'2026-02-15 00:00:00',23),(2063,'2026-02-16 00:00:00',23),(2086,'2026-02-17 00:00:00',17),(2103,'2026-02-18 00:00:00',58),(2161,'2026-02-19 00:00:00',26),(2187,'2026-02-20 00:00:00',15),(2202,'2026-02-21 00:00:00',16),(2218,'2026-02-22 00:00:00',28),(2246,'2026-02-23 00:00:00',41),(2287,'2026-02-24 00:00:00',65),(2352,'2026-02-25 00:00:00',44),(2396,'2026-02-26 00:00:00',28),(2424,'2026-02-27 00:00:00',11),(2435,'2026-02-28 00:00:00',12),(2447,'2026-03-01 00:00:00',23),(2470,'2026-03-02 00:00:00',29),(2499,'2026-03-03 00:00:00',20),(2519,'2026-03-04 00:00:00',12),(2531,'2026-03-05 00:00:00',39),(2570,'2026-03-06 00:00:00',26),(2596,'2026-03-07 00:00:00',39);
/*!40000 ALTER TABLE `sitevisits` ENABLE KEYS */;

-- 
-- Definition of slip_audit_logs
-- 

DROP TABLE IF EXISTS `slip_audit_logs`;
CREATE TABLE IF NOT EXISTS `slip_audit_logs` (
  `LogId` int NOT NULL AUTO_INCREMENT,
  `ShopId` int NOT NULL DEFAULT '1',
  `AttemptDate` datetime DEFAULT CURRENT_TIMESTAMP COMMENT 'เวลาที่ทำรายการ',
  `CustomerName` varchar(255) DEFAULT NULL COMMENT 'ชื่อลูกค้าที่กรอกมา',
  `PhoneNumber` varchar(50) DEFAULT NULL COMMENT 'เบอร์โทรศัพท์',
  `Amount` decimal(10,2) DEFAULT NULL COMMENT 'ยอดเงินที่พยายามสั่งซื้อ',
  `Status` varchar(50) DEFAULT NULL COMMENT 'สถานะ เช่น Rejected, Duplicate',
  `Reason` text COMMENT 'สาเหตุที่ปฏิเสธ เช่น สลิปซ้ำ, ยอดไม่ตรง',
  `SlipImageUrl` text COMMENT 'ลิงก์รูปสลิปที่แนบมา',
  PRIMARY KEY (`LogId`)
) ENGINE=InnoDB AUTO_INCREMENT=85 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table slip_audit_logs
-- 

/*!40000 ALTER TABLE `slip_audit_logs` DISABLE KEYS */;
/*!40000 ALTER TABLE `slip_audit_logs` ENABLE KEYS */;

-- 
-- Definition of slipconfigs
-- 

DROP TABLE IF EXISTS `slipconfigs`;
CREATE TABLE IF NOT EXISTS `slipconfigs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ShopId` int NOT NULL,
  `ApiUrl` longtext,
  `ApiToken` longtext,
  `ShopNameKey` longtext,
  `ShopPromptPay` longtext,
  `IsActive` tinyint(1) DEFAULT '1',
  `LastUpdated` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `idx_shop_id` (`ShopId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table slipconfigs
-- 

/*!40000 ALTER TABLE `slipconfigs` DISABLE KEYS */;
INSERT INTO `slipconfigs`(`Id`,`ShopId`,`ApiUrl`,`ApiToken`,`ShopNameKey`,`ShopPromptPay`,`IsActive`,`LastUpdated`) VALUES(2,1,'https://api.thunder.in.th/v2/verify/bank','c0c36e9c-8960-4aea-9229-14c888a9ef86','สมหวัง ลูกชิ้นเนื้อแท้,SOMWANG LOOKCHIN NUATAIR,บจก. สมหวัง ล','',1,'2026-02-22 11:34:10.284780'),(3,3,'https://api.thunder.in.th/v1/verify','c0c36e9c-8960-4aea-9229-14c888a9ef86','บจก. สมหวัง น,SOMWANG N','',1,'2026-01-24 21:48:47.970309');
/*!40000 ALTER TABLE `slipconfigs` ENABLE KEYS */;

-- 
-- Definition of sliptransactions
-- 

DROP TABLE IF EXISTS `sliptransactions`;
CREATE TABLE IF NOT EXISTS `sliptransactions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TransRef` varchar(100) NOT NULL,
  `BankName` varchar(50) DEFAULT NULL,
  `SenderName` varchar(255) DEFAULT NULL,
  `Amount` decimal(10,2) NOT NULL,
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `TransRef` (`TransRef`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table sliptransactions
-- 

/*!40000 ALTER TABLE `sliptransactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `sliptransactions` ENABLE KEYS */;

-- 
-- Definition of stocktransactions
-- 

DROP TABLE IF EXISTS `stocktransactions`;
CREATE TABLE IF NOT EXISTS `stocktransactions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProductId` int NOT NULL,
  `Quantity` int NOT NULL,
  `Type` varchar(50) NOT NULL,
  `Note` text,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `ProductId` (`ProductId`),
  CONSTRAINT `stocktransactions_ibfk_1` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table stocktransactions
-- 

/*!40000 ALTER TABLE `stocktransactions` DISABLE KEYS */;
INSERT INTO `stocktransactions`(`Id`,`ProductId`,`Quantity`,`Type`,`Note`,`CreatedAt`) VALUES(16,6,-1,'Sale','Order: ORD-260213171931 (Manual)','2026-02-13 17:19:32'),(17,7,-1,'Sale','Order: ORD-260213171931 (Manual)','2026-02-13 17:19:32'),(18,16,-1,'Packing','Order: ORD-260213171931','2026-02-13 17:40:28'),(19,13,-2,'Packing','Order: ORD-260213171931','2026-02-13 17:40:28'),(20,16,-1,'Packing','Order: ORD-260214095950','2026-02-14 10:37:01'),(21,13,-3,'Packing','Order: ORD-260214095950','2026-02-14 10:37:01'),(22,13,4,'Import','','2026-02-14 18:09:19'),(23,13,-3,'Packing','Order: ORD-260217105904','2026-02-17 11:48:59'),(24,16,-1,'Packing','Order: ORD-260217105904','2026-02-17 11:48:59'),(25,16,-1,'Packing','Order: ORD-260219115634','2026-02-19 13:07:46'),(26,13,-3,'Packing','Order: ORD-260219115634','2026-02-19 13:07:46'),(27,6,-1,'Sale','Order: ORD-690222121637 (Manual)','2026-02-22 12:16:37'),(28,14,-1,'Packing','Order: ORD-690222121637','2026-02-22 12:49:54'),(29,13,-2,'Packing','Order: ORD-690222121637','2026-02-22 12:49:54'),(30,13,-4,'Packing','Order: ORD-690224165304','2026-02-25 11:18:58'),(31,16,-1,'Packing','Order: ORD-690224165304','2026-02-25 11:18:58'),(32,7,-2,'Sale','Order: ORD-690226090601 (Manual)','2026-02-26 09:06:01'),(33,15,-1,'Packing','Order: ORD-690226090601','2026-02-26 11:14:15'),(34,13,-4,'Packing','Order: ORD-690226090601','2026-02-26 11:14:15'),(35,13,-4,'Packing','Order: ORD-690228181543','2026-03-01 10:35:28'),(36,16,-1,'Packing','Order: ORD-690228181543','2026-03-01 10:35:28'),(37,16,-1,'Packing','Order: ORD-690228181543','2026-03-02 13:24:47'),(38,13,-4,'Packing','Order: ORD-690228181543','2026-03-02 13:24:47'),(39,16,-1,'Packing','Order: ORD-690301221028','2026-03-02 13:25:16'),(40,13,-4,'Packing','Order: ORD-690301221028','2026-03-02 13:25:16'),(41,16,-1,'Packing','Order: ORD-690301222027','2026-03-03 14:27:54'),(42,13,-3,'Packing','Order: ORD-690301222027','2026-03-03 14:27:54'),(43,13,-3,'Packing','Order: ORD-690302085757','2026-03-03 14:28:23'),(44,16,-1,'Packing','Order: ORD-690302085757','2026-03-03 14:28:23'),(45,13,-3,'Packing','Order: ORD-690302190002','2026-03-03 14:28:44'),(46,16,-1,'Packing','Order: ORD-690302190002','2026-03-03 14:28:44'),(47,14,-1,'Packing','Order: ORD-690302191041','2026-03-03 14:29:22'),(48,13,-3,'Packing','Order: ORD-690303135829','2026-03-03 14:29:47'),(49,16,-1,'Packing','Order: ORD-690303135829','2026-03-03 14:29:47'),(50,13,-4,'Packing','Order: ORD-690305113346','2026-03-05 15:43:35'),(51,16,-1,'Packing','Order: ORD-690305113346','2026-03-05 15:43:35'),(52,13,-4,'Packing','Order: ORD-690306112911','2026-03-06 11:36:14'),(53,16,-1,'Packing','Order: ORD-690306112911','2026-03-06 11:36:14'),(54,13,-6,'Packing','Order: ORD-690307113914','2026-03-07 11:45:04'),(55,16,-1,'Packing','Order: ORD-690307113914','2026-03-07 11:45:04');
/*!40000 ALTER TABLE `stocktransactions` ENABLE KEYS */;

-- 
-- Definition of voice_qnas
-- 

DROP TABLE IF EXISTS `voice_qnas`;
CREATE TABLE IF NOT EXISTS `voice_qnas` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Keywords` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Answer` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `Category` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT 'General',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table voice_qnas
-- 

/*!40000 ALTER TABLE `voice_qnas` DISABLE KEYS */;
INSERT INTO `voice_qnas`(`Id`,`Keywords`,`Answer`,`IsActive`,`Category`) VALUES(1,'ร้านอยู่ไหน,ที่อยู่,ร้านอยู่ที่ไหน','ข้านเลขที่ 355 พุดซา ตำบล พุดซา อำเภอเมืองนครราชสีมา นครราชสีมา 30000 ค่ะ',1,'ShopInfo'),(3,'ราคา,กี่บาท','ลูกชิ้นกิโลละ 180 บาท น้ำปลาร้าขวดละ 40 บาท',1,'ProductInfo'),(4,'เนื้อล้วน,เอ็นหมู,เอ็นเนื้อ','กิโลกรัมละ180บาทค่ะ ไม่ทราบว่าลูกค้าต้องการรับ จำนวนกี่กิโลกรัมดีค่ะ',1,'ProductInfo'),(9,'1กิโล,2กิโล,3กิโล,4กิโล,5กิโล,6กิโล,7กิโล,8กิโล,9กิโล,10กิโล,11กิโล,12กิโล,13กิโล,14กิโล,15กิโล,16กิโล,17กิโล,18กิโล,19กิโล,20กิโล','ขอบคุณคะ คุณลูกค้าต้องการสินค้ารายการอื่นอีกไหมค่ะ ถ้าต้องการคุณลูกค้าสามารถแจ้งรายการได้เลยค่ะ หรือคุณลูกค้าพอใจแล้วโปรดกล่าวว่า ยืนยันออเดอร์ ค่ะ',1,'ProductInfo'),(13,'Welcome','สวัสดีค่ะ น้องสมหวัง ยินดีให้บริการค่ะ วันนี้รับลูกชิ้นหรือน้ำปลาร้าดีคะ?',1,'General'),(14,'อยากจะสั่งลูกชิ้น,ต้องการจะสั่งลูกชิ้น,จะสั่งลูกชิ้น,ต้องการลูกชิ้น,อยากได้ลูกชิ้น','คุณ ลูกค้าต้องการลูกชิ้น เอ็นเนื้อ เอ็นหมู หรือ เนื้อล้วน ดีค่ะ',1,'ProductInfo'),(15,'ฝาแดงสูตรออริจินอล,ฝาเหลืองสูตรน้ำปู,ฝาแดง,ฝาเหลือง,สูตรออริจินอล,สูตรน้ำปู,สูตรปู','ขวดละ40บาทค่ะ ไม่ทราบว่าคุณลูกค้าต้องการรับ จำนวนกี่ขวดดีค่ะ',1,'ProductInfo'),(16,'ยืนยันออเดอร์','ระบบได้บันทึกออเดอร์เรียบร้อยแล้ว ไม่เกิน30นาทีจะมีพนักงานโทรกลับเพื่อขอที่อยู่ของคุณลูกค้าเพื่อจัดส่งสินค้านะค่ะ ขอบคุณค่ะที่ซื้อสินค้ากับ สมหวัง ลูกชิ้นเนื้อแท้',1,'ProductInfo'),(17,'1ขวด,2ขวด,3ขวด,4ขวด,5ขวด,6ขวด,7ขวด,8ขวด,9ขวด,10ขวด,11ขวด,12ขวด,13ขวด,14ขวด,15ขวด,16ขวด,17ขวด,18ขวด,19ขวด,20ขวด','ขอบคุณคะ คุณลูกค้าต้องการสินค้ารายการอื่นอีกไหมค่ะ ถ้าต้องการคุณลูกค้าสามารถแจ้งรายการได้เลยค่ะ หรือคุณลูกค้าพอใจแล้วโปรดกล่าวว่า ยืนยันออเดอร์ ค่ะ',1,'ProductInfo'),(18,'1แพ็ค,2แพ็ค,3แพ็ค,4แพ็ค,5แพ็ค,6แพ็ค,7แพ็ค,8แพ็ค,9แพ็ค,10แพ็ค,11แพ็ค,12แพ็ค,13แพ็ค,14แพ็ค,15แพ็ค,16แพ็ค,17แพ็ค,18แพ็ค,19แพ็ค,20แพ็ค','ขอบคุณคะ คุณลูกค้าต้องการสินค้ารายการอื่นอีกไหมค่ะ ถ้าต้องการคุณลูกค้าสามารถแจ้งรายการได้เลยค่ะ หรือคุณลูกค้าพอใจแล้วโปรดกล่าวว่า ยืนยันออเดอร์ ค่ะ',1,'ProductInfo'),(19,'อยากจะสั่งน้ำปลาร้า,ต้องการจะสั่งน้ำปลาร้า,จะสั่งน้ำปลาร้า,ต้องการน้ำปลาร้า,อยากได้น้ำปลาร้า','คุณลูกค้าต้องการสั่งน้ำปลาร้า ฝาแดงสูตรออริจินอล หรือ ฝาเหลืองสูตรน้ำปู ดีค่ะ',1,'ProductInfo'),(20,'น้ำจิ้ม, มะขาม, น้ำจิ้มมะขาม','น้ำจิ้มมะขามแท้ รสเด็ด ขวดละ 50 บาทค่ะ รับกี่ขวดดีคะ?',1,'ProductInfo');
/*!40000 ALTER TABLE `voice_qnas` ENABLE KEYS */;

-- 
-- Definition of voicelogs
-- 

DROP TABLE IF EXISTS `voicelogs`;
CREATE TABLE IF NOT EXISTS `voicelogs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CallId` varchar(100) NOT NULL,
  `CallerPhone` varchar(20) NOT NULL,
  `Transcript` text,
  `AudioUrl` varchar(255) DEFAULT NULL,
  `Intent` varchar(50) DEFAULT NULL,
  `Parameters` text,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=389 DEFAULT CHARSET=utf8mb3;

-- 
-- Dumping data for table voicelogs
-- 

/*!40000 ALTER TABLE `voicelogs` DISABLE KEYS */;
/*!40000 ALTER TABLE `voicelogs` ENABLE KEYS */;


/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2569-03-07 12:01:27
-- Total time: 0:0:0:0:36 (d:h:m:s:ms)
