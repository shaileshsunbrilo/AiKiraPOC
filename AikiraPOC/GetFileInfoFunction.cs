using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AikiraPOC_Core.Interfaces.Persistence;
using AikiraPOC_Core.Entities;
using AikiraPOC_ServiceFactory.File;

namespace AikiraPOC
{

    public class GetFileInfoFunction
    {
        private readonly IFileInfoRepository _fileinfo;
        public GetFileInfoFunction(IFileInfoRepository fileinfo)
        {
            this._fileinfo = fileinfo;
        }

        [FunctionName("GetFileInfo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("AiKira GetFileInfo function processed a request.");
                string PSId = req.Query["PSId"];
                string ProjectId = req.Query["ProjectId"];
                string BatchId = req.Query["BatchId"];
                string FileId = req.Query["FileId"];
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                PSId = PSId ?? data?.PSId;
                ProjectId = ProjectId ?? data?.ProjectId;
                BatchId = BatchId ?? data?.BatchId;
                FileId = FileId ?? data?.FileId;

                FileInfoRequestModel fileInfoRequestModel = new FileInfoRequestModel();
                fileInfoRequestModel.intPSId = Convert.ToInt16(PSId);
                fileInfoRequestModel.intProjectId = Convert.ToInt16(ProjectId);
                fileInfoRequestModel.intBatchId = Convert.ToInt16(BatchId);
                fileInfoRequestModel.intFileId = Convert.ToInt16(FileId);

                int intPSId = Convert.ToInt16(PSId);
                var result = _fileinfo.GetFileInfo(fileInfoRequestModel);

                //var result = _fileinfomgr.GetFileData()
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                dynamic jsonToReturn = null;
                string strMsg = ex.Message.ToString();
                jsonToReturn = JsonConvert.SerializeObject(strMsg);
                return new OkObjectResult(jsonToReturn);
            }
           
        }
    }
}
