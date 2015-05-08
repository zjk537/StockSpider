/*
Navicat SQLite Data Transfer

Source Server         : Stock
Source Server Version : 30808
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30808
File Encoding         : 65001

Date: 2015-05-08 17:22:22
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for BigDealRecord
-- ----------------------------
DROP TABLE IF EXISTS "BigDealRecord";
CREATE TABLE "BigDealRecord" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"StockCode"  TEXT,
"Name"  TEXT,
"Price"  TEXT,
"Volume"  INTEGER,
"PrevPrice"  TEXT,
"DealType"  TEXT,
"TickTime"  TEXT,
"DealDate"  TEXT,
"CreatedDate"  TEXT
);

-- ----------------------------
-- Table structure for BigDealSum
-- ----------------------------
DROP TABLE IF EXISTS "BigDealSum";
CREATE TABLE "BigDealSum" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL DEFAULT 1,
"StockCode"  TEXT,
"Name"  TEXT,
"TotalVol"  INTEGER,
"TotalVolPCT"  TEXT,
"TotalAmt"  INTEGER,
"TotalAmtPCT"  TEXT,
"AvgPrice"  TEXT,
"KuVolume"  INTEGER,
"KuAmount"  INTEGER,
"KeVolume"  INTEGER,
"KeAmount"  INTEGER,
"KdVolume"  INTEGER,
"KdAmount"  INTEGER,
"StockVol"  INTEGER,
"StockAmt"  INTEGER,
"DealDate"  TEXT,
"CreatedDate"  TEXT
);

-- ----------------------------
-- Table structure for ChangePercent
-- ----------------------------
DROP TABLE IF EXISTS "ChangePercent";
CREATE TABLE "ChangePercent" (
"Id"  INTEGER NOT NULL,
"StockCode"  TEXT NOT NULL,
"StockName"  TEXT NOT NULL,
"LimitUp"  TEXT NOT NULL,
"IncreasePercent"  TEXT,
"StockBourse"  INTEGER,
"PE"  TEXT,
"PB"  TEXT,
PRIMARY KEY ("Id" ASC)
);

-- ----------------------------
-- Table structure for DailyRecord
-- ----------------------------
DROP TABLE IF EXISTS "DailyRecord";
CREATE TABLE "DailyRecord" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL DEFAULT 1,
"StockCode"  TEXT,
"Name"  TEXT,
"Now"  TEXT,
"Open"  TEXT,
"High"  TEXT,
"Low"  TEXT,
"PreClose"  TEXT,
"Change"  TEXT,
"ChangeP"  TEXT,
"Volume"  INTEGER,
"Amount"  INTEGER,
"DealDate"  TEXT,
"CreatedDate"  TEXT
);

-- ----------------------------
-- Table structure for Notice
-- ----------------------------
DROP TABLE IF EXISTS "Notice";
CREATE TABLE "Notice" (
"NoticeId"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL DEFAULT 1,
"Id"  INTEGER NOT NULL,
"StockCode"  TEXT(20) NOT NULL,
"Title"  TEXT(500),
"Date"  TEXT(20)
);

-- ----------------------------
-- Table structure for SourceUrl
-- ----------------------------
DROP TABLE IF EXISTS "SourceUrl";
CREATE TABLE "SourceUrl" (
"UrlId"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL DEFAULT 1,
"Url"  TEXT NOT NULL,
"Type"  INTEGER NOT NULL DEFAULT 1,
"State"  INTEGER,
"Remark"  TEXT,
"Handle"  TEXT,
"CreatedDate"  TEXT
);


-- ----------------------------
-- Table structure for StockCompany
-- ----------------------------
DROP TABLE IF EXISTS "StockCompany";
CREATE TABLE "StockCompany" (
"CompanyId"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL DEFAULT 1,
"StockCode"  TEXT NOT NULL,
"StockBourse"  INTEGER NOT NULL,
"CompanyName"  TEXT NOT NULL,
"CompanyNature"  TEXT,
"CreatedDate"  TEXT NOT NULL
);


INSERT INTO "SourceUrl" ("UrlId", "Url", "Type", "State", "Remark", "Handle", "CreatedDate") VALUES ('1', 'http://hqdigi2.eastmoney.com/EM_Quote2010NumericApplication/index.aspx?type=s&sortType=C&sortRule=-1&pageSize=50&page=1&jsName=quote_123&style=33&_g=0.109458738180513', '1', '4', '获取所有入市的公司及股票代码', 'StockCompanyBusiness', '2015-05-07');
INSERT INTO "SourceUrl" ("UrlId", "Url", "Type", "State", "Remark", "Handle", "CreatedDate") VALUES ('3', 'http://vip.stock.finance.sina.com.cn/api/jsonp.php/var%20noticeData=/CB_AllService.getMemordlistbysymbol?num=8&PaperCode={0}', '3', '4', '获取上市公司最新公布消息', 'NoticeBusiness', '2015-05-07');
INSERT INTO "SourceUrl" ("UrlId", "Url", "Type", "State", "Remark", "Handle", "CreatedDate") VALUES ('4', 'http://vip.stock.finance.sina.com.cn/quotes_service/api/json_v2.php/CN_Bill.GetBillList?symbol={0}&num=60&page=1&sort=ticktime&asc=1&volume={1}&type=0&day={2}', '4', '4', '获取个股的大单交易名细：有三个参数param1=sh/sz+code;param2=大单我交易数量; param3="yyyy-MM-dd"', 'BigDealBusiness', '2015-05-07');
INSERT INTO "SourceUrl" ("UrlId", "Url", "Type", "State", "Remark", "Handle", "CreatedDate") VALUES ('5', 'http://hq.sinajs.cn/list={0}', '2', '4', '获取股票实时交易数据
param1=(sh|sz)+code：交易数据详情
param1=s_(sh|sz)+code:交易简易信息', 'DailyRecordBusiness', '2015-05-07');
INSERT INTO "SourceUrl" ("UrlId", "Url", "Type", "State", "Remark", "Handle", "CreatedDate") VALUES ('6', 'http://vip.stock.finance.sina.com.cn/quotes_service/api/json_v2.php/CN_Bill.GetBillSum?symbol={0}&volume={1}&day={2}', '4', '3', '大单交易汇总信息
param1=sz|sh+code
param2=大单的标准默认40000
param3=yyyy-MM-dd', 'BigDealSumBusiness', '2015-05-07');