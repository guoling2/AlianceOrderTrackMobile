using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;

namespace AlianceOrderTrackMobile.Services.Abstractions
{
    public interface IShipmentServices
    {


        Task<LogisticTrackView> Query(string taskId, string logisticStoreId);

        Task<TmsResponse> ShipmentUpdateStatued(ShipmentStatuedChangedModel statuedChangedModel);
    }
}
