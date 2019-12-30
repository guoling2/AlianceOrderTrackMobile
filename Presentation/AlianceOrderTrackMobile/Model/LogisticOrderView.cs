using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AlianceOrderTrackMobile.Model
{
    public class LogisticTrackView
    {


        public string ShipmentId { get; set; }
        public string SystemOrderId { get; set; }
        public string TrackOrderId { get; set; }

        public string LogisticStoreName { get; set; }
        public int ShipmentStatuedId { get; set; }
        public decimal ReceivedTotalCount { get;  set; }

        public string DestCity { get; set; }
    }

    public enum ShipmentStatus
    {
        /// <summary>
        ///  未知
        /// </summary>
        [Description("未知")]
        UnKonw = -1,
        /// <summary>
        ///  未提货
        /// </summary>
        [Description("未提货")]
        Weitihuo = 10,
        /// <summary>
        ///  提货中
        /// </summary>
        [Description("提货中")]
        Tihuoing = 11,
        /// <summary>
        ///  已提货
        /// </summary>
        [Description("已提货")]
        Tihuoed = 19,
        /// <summary>
        /// 卸货中
        /// </summary>
        [Description("卸货中")]
        Xiehuoing = 20,
        /// <summary>
        /// 已卸货
        /// </summary>
        [Description("已卸货")]
        Xiehuoed = 29,
        /// <summary>
        /// 已入库
        /// </summary>
        [Description("已入库")]
        Rukued = 30,
        /// <summary>
        /// 已出库
        /// </summary>
        [Description("已出库")]
        Chukued = 40,
        ///// <summary>
        ///// 部分运输
        ///// </summary>
        //[Description("部分运输")]
        //PartiallyShipped=50,
        /// <summary>
        ///配送 
        /// </summary>
        [Description("配送中")]
        FullShipped = 51,
        /// <summary>
        ///  已完成
        /// </summary>
        [Description("已完成")]
        Finished = 100
    }
}
