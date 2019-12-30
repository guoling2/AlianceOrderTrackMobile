using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;

namespace AlianceOrderTrackMobile.Services.Abstractions
{
   public interface IShipmentPlanServices
   {

       /// <summary>
       /// 给司机下达任务
       /// </summary>
       /// <param name="shipmentPlanRequest"></param>
       /// <returns></returns>
       Task<TmsResponse> Shipmentcreateplan(ShipmentPlanRequest shipmentPlanRequest);
   }
}
