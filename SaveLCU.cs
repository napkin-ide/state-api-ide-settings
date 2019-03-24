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
using System.Collections.Generic;
using LCU.Graphs.Registry.Enterprises;
using Fathym;
using System.Linq;
using System.Net.Http;
using Gremlin.Net.Process.Traversal;
using LCU.Manager;

namespace LCU.State.API.IDESettings
{
    [Serializable]
    [DataContract]
    public class SaveLCURequest
    {
        [DataMember]
        public virtual LowCodeUnitConfig LCU { get; set; }
    }

    public static class SaveLCU
    {
        [FunctionName("SaveLCU")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            return await req.Manage<SaveLCURequest, IdeSettingsState, IDESettingsStateHarness>(log, async (mgr, reqData) =>
            {
                return await mgr.SaveLCU(reqData.LCU);
            });
        }
    }
}
