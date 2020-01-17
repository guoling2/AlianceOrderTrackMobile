using System;
using System.Collections.Generic;
using System.Text;

namespace TmsBuinessCommonLibrary.Model
{
    public class AppUpdateModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NowVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NotifyType { get; set; }
        /// <summary>
        /// 更新了内容
        /// </summary>
        public string UpdateMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsNeedUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IdNoMapping { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UpdateModelType { get; set; }


        public string DownloadFileName
        {


            get { return UpdateUrl.Substring(UpdateUrl.LastIndexOf("/", StringComparison.Ordinal) + 1); }
        }
    }
}
