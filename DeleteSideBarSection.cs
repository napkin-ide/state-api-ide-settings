using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using LCU.Graphs;
using LCU.Graphs.Registry.Enterprises.IDE;
using LCU.Manager;

namespace LCU.State.API.IDESettings
{
    [Serializable]
    [DataContract]
    public class DeleteSideBarSectionRequest
    {
        [DataMember]
        public virtual string Section { get; set; }
    }

    public static class DeleteSideBarSection
    {
        [FunctionName("DeleteSideBarSection")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            return await req.Manage<DeleteSideBarSectionRequest, IdeSettingsState, IDESettingsStateHarness>(log, async (mgr, reqData) =>
            {
                return await mgr.DeleteSideBarSection(reqData.Section);
            });
        }
    }
}
