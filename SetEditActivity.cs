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
using System.Linq;
using LCU.Graphs.Registry.Enterprises.IDE;
using LCU.State.API.IDESettings.Harness;
using LCU.State.API.IDESettings.Models;

namespace LCU.State.API.IDESettings
{
	[Serializable]
	[DataContract]
	public class SetEditActivityRequest
	{
		[DataMember]
		public virtual string Activity { get; set; }
	}

	public static class SetEditActivity
    {
        [FunctionName("SetEditActivity")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
		{
            return await req.Manage<SetEditActivityRequest, IdeSettingsState, IDESettingsStateHarness>(log, async (mgr, reqData) =>
            {
				log.LogInformation($"Setting Edit Activity: {reqData.Activity}");

				return await mgr.SetEditActivity(reqData.Activity);
            });
		}
    }
}
