using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;

namespace AlianceOrderTrackMobile.Services.Abstractions
{
    public interface IDriverUserServices
    {
        Task<List<DriverUserProfile>> GetItemsAsync(string drivername, string drivertel);
    }

    
}
