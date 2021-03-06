﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using Oracle.DataAccess.Client;

namespace DataService
{
    /// <summary>
    /// 调拨入库单同步
    /// </summary>
    [WebService(Namespace = "http://www.kingdee.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SyncMoveInWarehsBill : System.Web.Services.WebService
    {
        
        private MoveInWarehsBill mibill=new MoveInWarehsBill();
        private MoveInWarehsBillEntry mibillEntry = new MoveInWarehsBillEntry();
        private string _storageUnit;

        #region 写入调拨入库主表语句
        private string BillCmdStr = "insert into T_IM_MoveInWarehsBill( " +
                                    "FID, " +
                                    "FSTORAGEORGUNITID, " +
                                    "FTRANSACTIONTYPEID, " +
                                    "FBIZTYPEID, " +
                                    "FNUMBER, " +
                                    "FBIZDATE, " +
                                    "FCREATORID, " +
                                    "FCREATETIME, " +
                                    "FISSUECOMPANYORGUNITID, " +
                                    "FRECEIPTCOMPANYORGUNITID, " +
                                    "FISSUESTORAGEORGUNITID, " +
                                    "FISSYSBILL, " +
                                    "FADMINORGUNITID, " +
                                    "FSTOCKERID, " +
                                    "FVOUCHERID, " +
                                    "FTOTALQTY, " +
                                    "FTOTALAMOUNT, " +
                                    "FFIVOUCHERED, " +
                                    "FTOTALSTANDARDCOST, " +
                                    "FTOTALACTUALCOST, " +
                                    "FISREVERSED, " +
                                    "FISINITBILL, " +
                                    "FMONTH, " +
                                    "FDAY, " +
                                    "FCOSTCENTERORGUNITID, " +
                                    "FAUDITTIME, " +
                                    "FBASESTATUS, " +
                                    "FSOURCEBILLTYPEID, " +
                                    "FBILLTYPEID, " +
                                    "FYEAR, " +
                                    "FPERIOD, " +
                                    "FMODIFIERID, " +
                                    "FMODIFICATIONTIME, " +
                                    "FHANDLERID, " +
                                    "FDESCRIPTION, " +
                                    "FHASEFFECTED, " +
                                    "FAUDITORID, " +
                                    "FSOURCEBILLID, " +
                                    "FSOURCEFUNCTION, " +
                                    "FLASTUPDATEUSERID, " +
                                    "FLASTUPDATETIME, " +
                                    "FCONTROLUNITID) " +
                                    "Values( " +
                                    ":FID, " +
                                    ":FSTORAGEORGUNITID, " +
                                    ":FTRANSACTIONTYPEID, " +
                                    ":FBIZTYPEID, " +
                                    ":FNUMBER, " +
                                    ":FBIZDATE, " +
                                    ":FCREATORID, " +
                                    ":FCREATETIME, " +
                                    ":FISSUECOMPANYORGUNITID, " +
                                    ":FRECEIPTCOMPANYORGUNITID, " +
                                    ":FISSUESTORAGEORGUNITID, " +
                                    ":FISSYSBILL, " +
                                    ":FADMINORGUNITID, " +
                                    ":FSTOCKERID, " +
                                    ":FVOUCHERID, " +
                                    ":FTOTALQTY, " +
                                    ":FTOTALAMOUNT, " +
                                    ":FFIVOUCHERED, " +
                                    ":FTOTALSTANDARDCOST, " +
                                    ":FTOTALACTUALCOST, " +
                                    ":FISREVERSED, " +
                                    ":FISINITBILL, " +
                                    ":FMONTH, " +
                                    ":FDAY, " +
                                    ":FCOSTCENTERORGUNITID, " +
                                    ":FAUDITTIME, " +
                                    ":FBASESTATUS, " +
                                    ":FSOURCEBILLTYPEID, " +
                                    ":FBILLTYPEID, " +
                                    ":FYEAR, " +
                                    ":FPERIOD, " +
                                    ":FMODIFIERID, " +
                                    ":FMODIFICATIONTIME, " +
                                    ":FHANDLERID, " +
                                    ":FDESCRIPTION, " +
                                    ":FHASEFFECTED, " +
                                    ":FAUDITORID, " +
                                    ":FSOURCEBILLID, " +
                                    ":FSOURCEFUNCTION, " +
                                    ":FLASTUPDATEUSERID, " +
                                    ":FLASTUPDATETIME, " +
                                    ":FCONTROLUNITID) ";
#endregion

        #region 写入调拨入库子表语句

        /// <summary>
        /// 写入调拨入库子表语句
        /// </summary>
        private string BillEntryCmdStr = "insert into T_IM_MoveInWarehsBillEntry( " +
                                    "FPARENTID, " +                        
                                    "FSTORAGEORGUNITID, " +
                                    "FCOMPANYORGUNITID, " +
                                    "FWAREHOUSEID, " +
                                    "FQTY, " +
                                    "FBASEQTY, " +
                                    "FMATERIALID, " +
                                    "FBASEUNITID, " +
                                    "FSEQ, " +
                                    "FID, " +
                                    "FSTORETYPEID, " +
                                    "FSTOCKTRANSFERBILLID, " +
                                    "FSTOCKTRANSBILLENTRYID, " +
                                    "FSTOCKTRANSFERNUM, " +
                                    "FSTOCKTRANSFERBILLENTRYSEQ, " +
                                    "FBASEUNITACTUALCOST, " +
                                    "FCUSTOMERID, " +
                                    "FSUPPLIERID, " +
                                    "FPRICE, " +
                                    "FAMOUNT, " +
                                    "FBIZDATE, " +
                                    "FLOCATIONID, " +
                                    "FSTOCKERID, " +
                                    "FLOT, " +
                                    "FASSISTQTY, " +
                                    "FREVERSEQTY, " +
                                    "FRETURNSQTY, " +
                                    "FUNITSTANDARDCOST, " +
                                    "FSTANDARDCOST, " +
                                    "FUNITACTUALCOST, " +
                                    "FACTUALCOST, " +
                                    "FISPRESENT, " +
                                    "FMFG, " +
                                    "FEXP, " +
                                    "FREVERSEBASEQTY, " +
                                    "FRETURNBASEQTY, " +
                                    "FPROJECTID, " +
                                    "FTRACKNUMBERID, " +
                                    "FASSISTPROPERTYID, " +
                                    "FUNITID, " +
                                    "FSOURCEBILLID, " +
                                    "FSOURCEBILLNUMBER, " +
                                    "FSOURCEBILLENTRYID, " +
                                    "FSOURCEBILLENTRYSEQ, " +
                                    "FASSCOEFFICIENT, " +
                                    "FBASESTATUS, " +
                                    "FASSOCIATEQTY," +
                                    "FSOURCEBILLTYPEID) " +
                                    "Values( " +
                                    ":FPARENTID, "+
                                    ":FSTORAGEORGUNITID, " +
                                    ":FCOMPANYORGUNITID, " +
                                    ":FWAREHOUSEID, " +
                                    ":FQTY, " +
                                    ":FBASEQTY, " +
                                    ":FMATERIALID, " +
                                    ":FBASEUNITID, " +
                                    ":FSEQ, " +
                                    ":FID, " +
                                    ":FSTORETYPEID, " +
                                    ":FSTOCKTRANSFERBILLID, " +
                                    ":FSTOCKTRANSBILLENTRYID, " +
                                    ":FSTOCKTRANSFERNUM, " +
                                    ":FSTOCKTRANSFERBILLENTRYSEQ, " +
                                    ":FBASEUNITACTUALCOST, " +
                                    ":FCUSTOMERID, " +
                                    ":FSUPPLIERID, " +
                                    ":FPRICE, " +
                                    ":FAMOUNT, " +
                                    ":FBIZDATE, " +
                                    ":FLOCATIONID, " +
                                    ":FSTOCKERID, " +
                                    ":FLOT, " +
                                    ":FASSISTQTY, " +
                                    ":FREVERSEQTY, " +
                                    ":FRETURNSQTY, " +
                                    ":FUNITSTANDARDCOST, " +
                                    ":FSTANDARDCOST, " +
                                    ":FUNITACTUALCOST, " +
                                    ":FACTUALCOST, " +
                                    ":FISPRESENT, " +
                                    ":FMFG, " +
                                    ":FEXP, " +
                                    ":FREVERSEBASEQTY, " +
                                    ":FRETURNBASEQTY, " +
                                    ":FPROJECTID, " +
                                    ":FTRACKNUMBERID, " +
                                    ":FASSISTPROPERTYID, " +
                                    ":FUNITID, " +
                                    ":FSOURCEBILLID, " +
                                    ":FSOURCEBILLNUMBER, " +
                                    ":FSOURCEBILLENTRYID, " +
                                    ":FSOURCEBILLENTRYSEQ, " +
                                    ":FASSCOEFFICIENT, " +
                                    ":FBASESTATUS, " +
                                    ":FASSOCIATEQTY," +
                                    ":FSOURCEBILLTYPEID) ";

        #endregion


        /// <summary>
        /// 写上下游关系表
        /// </summary>
        private string RelationCmdstr = "insert into t_bot_relation(" +
                                        "FID," +
                                        "FSRCENTITYID," +
                                        "FDESTENTITYID," +
                                        "FSRCOBJECTID," +
                                        "FDESTOBJECTID," +
                                        "FDATE," +
                                        "FOPERATORID," +
                                        "FISEFFECTED," +
                                        "FBOTMAPPINGID," +
                                        "FTYPE) " +
                                        "Values( " +
                                        ":FID," +
                                        ":FSRCENTITYID," +
                                        ":FDESTENTITYID," +
                                        ":FSRCOBJECTID," +
                                        ":FDESTOBJECTID," +
                                        ":FDATE," +
                                        ":FOPERATORID," +
                                        ":FISEFFECTED," +
                                        ":FBOTMAPPINGID," +
                                        ":FTYPE) ";
        /// <summary>
        /// 填充主表数据
        /// </summary>
        /// <returns></returns>
        private void FillBill(string cOrderNumber, string cNewEasOrder,string cUserName)
        {
            var iof = new InterfaceOracleFunction(Properties.Settings.Default.EasCon);
            mibill.FID = iof.GetFID("E3DAFF63");

            _storageUnit = iof.GetReceiptStorageByOrderNumber(cOrderNumber, "T_IM_StockTransferBill");

            mibill.FSTORAGEORGUNITID = _storageUnit;
            mibill.FTRANSACTIONTYPEID = "DawAAAAPoAuwCNyn";
            mibill.FBIZTYPEID = "d8e80652-011b-1000-e000-04c5c0a812202407435C";
            mibill.FNUMBER = cNewEasOrder;

            var dDate = iof.ReturnBizDate();

            mibill.FBIZDATE = dDate;

            //制单人
            var cUserId = iof.GetUserIDbyUserName(cUserName);
            mibill.FCREATORID = string.IsNullOrEmpty(cUserId) ? "K7Li625bRC6r8uAH5mlIDRO33n8=" : cUserId;
            
            mibill.FCREATETIME = DateTime.Now;
            mibill.FISSUECOMPANYORGUNITID = _storageUnit;
            mibill.FRECEIPTCOMPANYORGUNITID = _storageUnit;
            mibill.FISSUESTORAGEORGUNITID = _storageUnit;
            mibill.FISSYSBILL = 0;
            mibill.FADMINORGUNITID = ""; ;
            //mibill.FSTOCKERID = "0";
            //mibill.FVOUCHERID = "0";
            mibill.FTOTALQTY = 0;
            mibill.FTOTALAMOUNT = 0;
            mibill.FFIVOUCHERED = 0;
            mibill.FTOTALSTANDARDCOST = 0;
            mibill.FTOTALACTUALCOST = 0;
            mibill.FISREVERSED = 0;
            mibill.FISINITBILL = 0;
            mibill.FMONTH = int.Parse(dDate.ToString("yyyyMM"));
            mibill.FDAY = int.Parse(dDate.ToString("yyyyMMdd"));
            mibill.FCOSTCENTERORGUNITID ="";
            //审核时间
            //mibill.FAUDITTIME = ;
            mibill.FBASESTATUS = 1;
            mibill.FSOURCEBILLTYPEID = "50957179-0105-1000-e003-3152c0a812fd463ED552";
            mibill.FBILLTYPEID = "50957179-0105-1000-e000-0172c0a812fd463ED552";
            mibill.FYEAR = dDate.Year;
            mibill.FPERIOD = dDate.Month;
            //修改信息
            //mibill.FMODIFIERID = "aL66lpH5SP+CdxD9O7bQmhO33n8=";
            //mibill.FMODIFICATIONTIME = "15-1月 -15 06.42.30.478000000 下午";
            //mibill.FHANDLERID = "0";
            //mibill.FDESCRIPTION = "0";
            mibill.FHASEFFECTED = 0;
            //mibill.FAUDITORID = "aL66lpH5SP+CdxD9O7bQmhO33n8=";
            //mibill.FSOURCEBILLID = "0";
            //mibill.FSOURCEFUNCTION = "0";
            //mibill.FLASTUPDATEUSERID = "aL66lpH5SP+CdxD9O7bQmhO33n8=";
            //mibill.FLASTUPDATETIME = "15-1月 -15 06.42.30.000000000 下午";
            mibill.FCONTROLUNITID = _storageUnit;

        }


        /// <summary>
        /// 填充子表数据
        /// </summary>
        /// <returns></returns>
        private void FillBillEntry(string cOrderNumber, string cInvCode, string iQuantity, string cInvName, string cLotNo, int iRowIndex)
        {
            

            var iof = new InterfaceOracleFunction(Properties.Settings.Default.EasCon);
            mibillEntry.FSTORAGEORGUNITID = _storageUnit;
            mibillEntry.FCOMPANYORGUNITID = _storageUnit;

            var dDate = iof.ReturnBizDate();
            //数量
            var cQty = iQuantity;
            decimal iQty;
            if (!decimal.TryParse(cQty, out iQty))
            {
                iQty = 0;
            }
            mibillEntry.FQTY = iQty;
            mibillEntry.FBASEQTY = iQty;
            mibillEntry.FMATERIALID = iof.GetInvCode(cInvCode);
            mibillEntry.FBASEUNITID = iof.GetInvUnit(mibillEntry.FMATERIALID);
            mibillEntry.FSEQ = iRowIndex;
            mibillEntry.FID = iof.GetFID("451C3ECF");
            mibillEntry.FPARENTID = mibill.FID;
            mibillEntry.FSTORETYPEID = "181875d5-0105-1000-e000-0111c0a812fd97D461A6";
            mibillEntry.FSTOCKTRANSFERBILLID = iof.GetSourIDByOrderNumber(cOrderNumber, "T_IM_StockTransferBill");
            //mibillEntry.FSTOCKTRANSBILLENTRYID = iof.GetEntrySourIDByOrderNumber(mibillEntry.FSTOCKTRANSFERBILLID, mibillEntry.FMATERIALID, "T_IM_StockTransferBillEntry"); ;
            //获取调拨单分录ID
            if (string.IsNullOrEmpty(cLotNo))
            {
                mibillEntry.FSTOCKTRANSBILLENTRYID = iof.GetEntrySourIDByOrderNumber(mibillEntry.FSTOCKTRANSFERBILLID, mibillEntry.FMATERIALID, "T_IM_StockTransferBillEntry");
            }
            else
            {
                mibillEntry.FSTOCKTRANSBILLENTRYID = iof.GetEntrySourIDByOrderNumberAndLotNo(mibillEntry.FSTOCKTRANSFERBILLID, mibillEntry.FMATERIALID, "T_IM_StockTransferBillEntry", cLotNo);
            }

            if (string.IsNullOrEmpty(mibillEntry.FSTOCKTRANSBILLENTRYID))
            {
                mibillEntry.FSTOCKTRANSBILLENTRYID = iof.GetEntrySourIDByOrderNumber(mibillEntry.FSTOCKTRANSFERBILLID, mibillEntry.FMATERIALID, "T_IM_StockTransferBillEntry");
            }

            mibillEntry.FWAREHOUSEID = iof.GetFReceiptWarehouseIDByfID(mibillEntry.FSTOCKTRANSBILLENTRYID, "T_IM_StockTransferBillEntry");
            mibillEntry.FSTOCKTRANSFERNUM = cOrderNumber;

            var cSeq = iof.GetEntrySeqByEntryFid(mibillEntry.FSTOCKTRANSBILLENTRYID, "T_IM_StockTransferBillEntry");
            int iSeq;
            if (!int.TryParse(cSeq, out iSeq))
            {
                iSeq = 1;
            }
            mibillEntry.FSTOCKTRANSFERBILLENTRYSEQ = iSeq;
            mibillEntry.FBASEUNITACTUALCOST = 0;
            //mibillEntry.FCUSTOMERID = "0";
            //mibillEntry.FSUPPLIERID = "0";
            mibillEntry.FPRICE = 0;
            mibillEntry.FAMOUNT = 0;


            mibillEntry.FBIZDATE = dDate;
            //mibillEntry.FLOCATIONID = "0";
            //mibillEntry.FSTOCKERID = "0";
            //是否批次管理
            //var bLot = iof.GetBLotById(mibillEntry.FMATERIALID);
            var bLot = iof.GetBLotById(mibillEntry.FMATERIALID, mibillEntry.FSTORAGEORGUNITID);
            if (bLot.Equals("1"))
            {
                mibillEntry.FLOT = cLotNo;
            }
            else
            {
                mibillEntry.FLOT = "";
            }
            mibillEntry.FASSISTQTY = 0;
            mibillEntry.FREVERSEQTY = 0;
            mibillEntry.FRETURNSQTY = 0;
            mibillEntry.FUNITSTANDARDCOST = 0;
            mibillEntry.FSTANDARDCOST = 0;
            mibillEntry.FUNITACTUALCOST = 0;
            mibillEntry.FACTUALCOST = 0;
            mibillEntry.FISPRESENT = 0;
            //mibillEntry.FMFG = 0;
            //mibillEntry.FEXP = 0;
            mibillEntry.FREVERSEBASEQTY = 0;
            mibillEntry.FRETURNBASEQTY = 0;
            //mibillEntry.FPROJECTID = "0";
            //mibillEntry.FTRACKNUMBERID = "0";
            //mibillEntry.FASSISTPROPERTYID = "0";
            mibillEntry.FUNITID = iof.GetInvUnit(mibillEntry.FMATERIALID);
            mibillEntry.FSOURCEBILLID = mibillEntry.FSTOCKTRANSFERBILLID;
            mibillEntry.FSOURCEBILLNUMBER = cOrderNumber;
            mibillEntry.FSOURCEBILLENTRYID = mibillEntry.FSTOCKTRANSBILLENTRYID;
            mibillEntry.FSOURCEBILLENTRYSEQ = iSeq;
            mibillEntry.FASSCOEFFICIENT = 0;
            mibillEntry.FBASESTATUS = 2;
            mibillEntry.FASSOCIATEQTY = iQty;
            mibillEntry.FSOURCEBILLTYPEID = "50957179-0105-1000-e003-3152c0a812fd463ED552";
        }

        

        /// <summary>
        /// 给写入主表的SQL参数传值
        /// </summary>
        /// <returns></returns>
        private void GenBillPara(OracleCommand ocmd)
        {
            ocmd.Parameters.Add(":FID", mibill.FID);
            ocmd.Parameters.Add(":FSTORAGEORGUNITID", mibill.FSTORAGEORGUNITID);
            ocmd.Parameters.Add(":FTRANSACTIONTYPEID", mibill.FTRANSACTIONTYPEID);
            ocmd.Parameters.Add(":FBIZTYPEID", mibill.FBIZTYPEID);
            ocmd.Parameters.Add(":FNUMBER", mibill.FNUMBER);
            ocmd.Parameters.Add(":FBIZDATE", mibill.FBIZDATE);
            ocmd.Parameters.Add(":FCREATORID", mibill.FCREATORID);
            ocmd.Parameters.Add(":FCREATETIME", mibill.FCREATETIME);
            ocmd.Parameters.Add(":FISSUECOMPANYORGUNITID", mibill.FISSUECOMPANYORGUNITID);
            ocmd.Parameters.Add(":FRECEIPTCOMPANYORGUNITID", mibill.FRECEIPTCOMPANYORGUNITID);
            ocmd.Parameters.Add(":FISSUESTORAGEORGUNITID", mibill.FISSUESTORAGEORGUNITID);
            ocmd.Parameters.Add(":FISSYSBILL", mibill.FISSYSBILL);
            ocmd.Parameters.Add(":FADMINORGUNITID", mibill.FADMINORGUNITID);
            ocmd.Parameters.Add(":FSTOCKERID", mibill.FSTOCKERID);
            ocmd.Parameters.Add(":FVOUCHERID", mibill.FVOUCHERID);
            ocmd.Parameters.Add(":FTOTALQTY", mibill.FTOTALQTY);
            ocmd.Parameters.Add(":FTOTALAMOUNT", mibill.FTOTALAMOUNT);
            ocmd.Parameters.Add(":FFIVOUCHERED", mibill.FFIVOUCHERED);
            ocmd.Parameters.Add(":FTOTALSTANDARDCOST", mibill.FTOTALSTANDARDCOST);
            ocmd.Parameters.Add(":FTOTALACTUALCOST", mibill.FTOTALACTUALCOST);
            ocmd.Parameters.Add(":FISREVERSED", mibill.FISREVERSED);
            ocmd.Parameters.Add(":FISINITBILL", mibill.FISINITBILL);
            ocmd.Parameters.Add(":FMONTH", mibill.FMONTH);
            ocmd.Parameters.Add(":FDAY", mibill.FDAY);
            ocmd.Parameters.Add(":FCOSTCENTERORGUNITID", mibill.FCOSTCENTERORGUNITID);
            ocmd.Parameters.Add(":FAUDITTIME", mibill.FAUDITTIME);
            ocmd.Parameters.Add(":FBASESTATUS", mibill.FBASESTATUS);
            ocmd.Parameters.Add(":FSOURCEBILLTYPEID", mibill.FSOURCEBILLTYPEID);
            ocmd.Parameters.Add(":FBILLTYPEID", mibill.FBILLTYPEID);
            ocmd.Parameters.Add(":FYEAR", mibill.FYEAR);
            ocmd.Parameters.Add(":FPERIOD", mibill.FPERIOD);
            ocmd.Parameters.Add(":FMODIFIERID", mibill.FMODIFIERID);
            ocmd.Parameters.Add(":FMODIFICATIONTIME", mibill.FMODIFICATIONTIME);
            ocmd.Parameters.Add(":FHANDLERID", mibill.FHANDLERID);
            ocmd.Parameters.Add(":FDESCRIPTION", mibill.FDESCRIPTION);
            ocmd.Parameters.Add(":FHASEFFECTED", mibill.FHASEFFECTED);
            ocmd.Parameters.Add(":FAUDITORID", mibill.FAUDITORID);
            ocmd.Parameters.Add(":FSOURCEBILLID", mibill.FSOURCEBILLID);
            ocmd.Parameters.Add(":FSOURCEFUNCTION", mibill.FSOURCEFUNCTION);
            ocmd.Parameters.Add(":FLASTUPDATEUSERID", mibill.FLASTUPDATEUSERID);
            ocmd.Parameters.Add(":FLASTUPDATETIME", mibill.FLASTUPDATETIME);
            ocmd.Parameters.Add(":FCONTROLUNITID", mibill.FCONTROLUNITID);

        }


        /// <summary>
        /// 给写入子表的SQL参数传值
        /// </summary>
        /// <returns></returns>
        private void GenBillEntryPara(OracleCommand ocmd)
        {
            ocmd.Parameters.Add(":FPARENTID", mibillEntry.FPARENTID);
            ocmd.Parameters.Add(":FSTORAGEORGUNITID", mibillEntry.FSTORAGEORGUNITID);
            ocmd.Parameters.Add(":FCOMPANYORGUNITID", mibillEntry.FCOMPANYORGUNITID);
            ocmd.Parameters.Add(":FWAREHOUSEID", mibillEntry.FWAREHOUSEID);
            ocmd.Parameters.Add(":FQTY", mibillEntry.FQTY);
            ocmd.Parameters.Add(":FBASEQTY", mibillEntry.FBASEQTY);
            ocmd.Parameters.Add(":FMATERIALID", mibillEntry.FMATERIALID);
            ocmd.Parameters.Add(":FBASEUNITID", mibillEntry.FBASEUNITID);
            ocmd.Parameters.Add(":FSEQ", mibillEntry.FSEQ);
            ocmd.Parameters.Add(":FID", mibillEntry.FID);
            ocmd.Parameters.Add(":FSTORETYPEID", mibillEntry.FSTORETYPEID);
            ocmd.Parameters.Add(":FSTOCKTRANSFERBILLID", mibillEntry.FSTOCKTRANSFERBILLID);
            ocmd.Parameters.Add(":FSTOCKTRANSBILLENTRYID", mibillEntry.FSTOCKTRANSBILLENTRYID);
            ocmd.Parameters.Add(":FSTOCKTRANSFERNUM", mibillEntry.FSTOCKTRANSFERNUM);
            ocmd.Parameters.Add(":FSTOCKTRANSFERBILLENTRYSEQ", mibillEntry.FSTOCKTRANSFERBILLENTRYSEQ);
            ocmd.Parameters.Add(":FBASEUNITACTUALCOST", mibillEntry.FBASEUNITACTUALCOST);
            ocmd.Parameters.Add(":FCUSTOMERID", mibillEntry.FCUSTOMERID);
            ocmd.Parameters.Add(":FSUPPLIERID", mibillEntry.FSUPPLIERID);
            ocmd.Parameters.Add(":FPRICE", mibillEntry.FPRICE);
            ocmd.Parameters.Add(":FAMOUNT", mibillEntry.FAMOUNT);
            ocmd.Parameters.Add(":FBIZDATE", mibillEntry.FBIZDATE);
            ocmd.Parameters.Add(":FLOCATIONID", mibillEntry.FLOCATIONID);
            ocmd.Parameters.Add(":FSTOCKERID", mibillEntry.FSTOCKERID);
            ocmd.Parameters.Add(":FLOT", mibillEntry.FLOT);
            ocmd.Parameters.Add(":FASSISTQTY", mibillEntry.FASSISTQTY);
            ocmd.Parameters.Add(":FREVERSEQTY", mibillEntry.FREVERSEQTY);
            ocmd.Parameters.Add(":FRETURNSQTY", mibillEntry.FRETURNSQTY);
            ocmd.Parameters.Add(":FUNITSTANDARDCOST", mibillEntry.FUNITSTANDARDCOST);
            ocmd.Parameters.Add(":FSTANDARDCOST", mibillEntry.FSTANDARDCOST);
            ocmd.Parameters.Add(":FUNITACTUALCOST", mibillEntry.FUNITACTUALCOST);
            ocmd.Parameters.Add(":FACTUALCOST", mibillEntry.FACTUALCOST);
            ocmd.Parameters.Add(":FISPRESENT", mibillEntry.FISPRESENT);
            ///生产日期，失效日期
            ocmd.Parameters.Add(":FMFG", DBNull.Value);
            ocmd.Parameters.Add(":FEXP", DBNull.Value);
            ocmd.Parameters.Add(":FREVERSEBASEQTY", mibillEntry.FREVERSEBASEQTY);
            ocmd.Parameters.Add(":FRETURNBASEQTY", mibillEntry.FRETURNBASEQTY);
            ocmd.Parameters.Add(":FPROJECTID", mibillEntry.FPROJECTID);
            ocmd.Parameters.Add(":FTRACKNUMBERID", mibillEntry.FTRACKNUMBERID);
            ocmd.Parameters.Add(":FASSISTPROPERTYID", mibillEntry.FASSISTPROPERTYID);
            ocmd.Parameters.Add(":FUNITID", mibillEntry.FUNITID);
            ocmd.Parameters.Add(":FSOURCEBILLID", mibillEntry.FSOURCEBILLID);
            ocmd.Parameters.Add(":FSOURCEBILLNUMBER", mibillEntry.FSOURCEBILLNUMBER);
            ocmd.Parameters.Add(":FSOURCEBILLENTRYID", mibillEntry.FSOURCEBILLENTRYID);
            ocmd.Parameters.Add(":FSOURCEBILLENTRYSEQ", mibillEntry.FSOURCEBILLENTRYSEQ);
            ocmd.Parameters.Add(":FASSCOEFFICIENT", mibillEntry.FASSCOEFFICIENT);
            ocmd.Parameters.Add(":FBASESTATUS", mibillEntry.FBASESTATUS);
            ocmd.Parameters.Add(":FASSOCIATEQTY", mibillEntry.FASSOCIATEQTY);
            ocmd.Parameters.Add(":FSOURCEBILLTYPEID", mibillEntry.FSOURCEBILLTYPEID);

        }

        /// <summary>
        /// 给写入子表的SQL参数传值
        /// </summary>
        /// <returns></returns>
        private void GenRelationPara(OracleCommand ocmd)
        {
            var iof = new InterfaceOracleFunction(Properties.Settings.Default.EasCon);
            ocmd.Parameters.Add(":FID", iof.GetFID("59302EC6"));
            ocmd.Parameters.Add(":FSRCENTITYID", "2239F30A");
            ocmd.Parameters.Add(":FDESTENTITYID", "E3DAFF63");
            ocmd.Parameters.Add(":FSRCOBJECTID", mibillEntry.FSOURCEBILLID);
            ocmd.Parameters.Add(":FDESTOBJECTID", mibill.FID);
            ocmd.Parameters.Add(":FDATE", DateTime.Now);
            ocmd.Parameters.Add(":FOPERATORID", "yofoto");
            ocmd.Parameters.Add(":FISEFFECTED", "1");
            ocmd.Parameters.Add(":FBOTMAPPINGID", "709becee-0108-1000-e000-2980c0a812fd045122C4");
            ocmd.Parameters.Add(":FTYPE", "1");

        }
       

        /// <summary>
        /// 传递WMS要与EAS同步的单据
        /// </summary>
        /// <param name="cOrderNumber">单据号</param>
        /// <param name="cGuid">单据GUID</param>
        /// <param name="iCount">行数</param>
        /// <returns></returns>
        [WebMethod]
        public string SyncOrder(string cOrderNumber, string cEasNewOrder, string cGuid, int iCount)
        {

            var dtMoveMoveIn = GetImportDataTable(cGuid);
            if (dtMoveMoveIn.Rows.Count < 1)
                return "无内容";
            var cUser = dtMoveMoveIn.Rows[0]["cUser"].ToString();
            using (var ocon = new OracleConnection(Properties.Settings.Default.EasCon))
            {
                ocon.Open();
                {
                    using (var otran = ocon.BeginTransaction())
                    {
                        OracleCommand ocmd;
                        using (ocmd = new OracleCommand())
                        {
                            ocmd.Connection = ocon;
                            try
                            {

                                ocmd.CommandText = "select FNUMBER from T_IM_MoveInWarehsBill where FNUMBER=:FNUMBER";
                                ocmd.Parameters.Add(":FNUMBER", cEasNewOrder);
                                if (ocmd.ExecuteReader().Read())
                                    return "OK";
                                ocmd.Parameters.Clear();


                                //执行主表写入
                                ocmd.CommandText = BillCmdStr;
                                FillBill(cOrderNumber, cEasNewOrder, cUser);
                                GenBillPara(ocmd);
                                ocmd.ExecuteNonQuery();

                                //执行子表写入，先填充，再写入
                                ocmd.CommandText = BillEntryCmdStr;
                                for (var i = 0; i < dtMoveMoveIn.Rows.Count; i++)
                                {
                                    ocmd.Parameters.Clear();
                                    var cInvCode = dtMoveMoveIn.Rows[i]["cInvCode"].ToString();
                                    var cInvName = dtMoveMoveIn.Rows[i]["cInvName"].ToString();
                                    var iQuantity = dtMoveMoveIn.Rows[i]["iQuantity"].ToString();
                                    var cLotNo = dtMoveMoveIn.Rows[i]["cLotNo"].ToString();
                                    FillBillEntry(cOrderNumber, cInvCode, iQuantity, cInvName, cLotNo, i + 1);
                                    GenBillEntryPara(ocmd);
                                    ocmd.ExecuteNonQuery();
                                }
                                ocmd.CommandText = RelationCmdstr;
                                ocmd.Parameters.Clear();
                                GenRelationPara(ocmd);
                                ocmd.ExecuteNonQuery();
                                otran.Commit();
                                return "OK";
                            }

                            catch (OracleException ex)
                            {

                                otran.Rollback();
                                return ex.Message;
                            }
                        }
                    }
                }
            }
        }

        private DataTable GetImportDataTable(string cGuid)
        {
            using (var con = new SqlConnection(Properties.Settings.Default.WmsCon))
            {
                //using (var cmd = new SqlCommand("select * from View_Compare_Transfer_Receive where cGuid=@cGuid", con))

                using (var cmd = new SqlCommand("select cUser,cInvCode,max(cInvName) [cInvName],sum(iQuantity) [iQuantity],cLotNo from View_Compare_Transfer_Receive where cGuid=@cGuid group by cInvCode,cLotNo,cUser", con))
                {
                    cmd.Parameters.AddWithValue("@cGuid", cGuid);
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable("MoveIn");
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
