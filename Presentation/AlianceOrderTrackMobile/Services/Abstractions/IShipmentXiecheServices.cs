using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Model.Xieche;

namespace AlianceOrderTrackMobile.Services.Abstractions
{
    public interface IShipmentXiecheServices
    {
        Task<TmsResponseEvolution<XiecheScanResult>> Query(string taskId, string logisticStoreId);

        Task<TmsResponse> XieChe(XiecheRequestModel xiecheRequest);
    }
}
