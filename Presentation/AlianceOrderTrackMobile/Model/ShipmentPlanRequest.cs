using System;
using System.Collections.Generic;
using System.Text;

namespace AlianceOrderTrackMobile.Model
{
    public class ShipmentPlanRequest
    {
        public string[] FullShipmentId { get; set; }
        /// <summary>
        /// 安排的司机系统编号
        /// </summary>
        public string ShipmentUserId { get; set; }
        /// <summary>
        /// 任务类型  1 本地提货
        /// </summary>
        public ShipmentPlanTaskType TaskType { get; set; }

    }
    public enum ShipmentPlanTaskType
    {
        /// <summary>
        /// 本地提货
        /// </summary>
        LocalTihuo = 1,
        /// <summary>
        /// 转运提货
        /// </summary>
        TransferTihuo = 2,
        /// <summary>
        /// 配送
        /// </summary>
        SendItem = 3,
        /// <summary>
        /// 本地提+送
        /// </summary>
        CombimeLocalAndSend = 4,
        /// <summary>
        /// 中转提货+送货
        /// </summary>
        CombimeTransferAndSend = 5


    }
}
