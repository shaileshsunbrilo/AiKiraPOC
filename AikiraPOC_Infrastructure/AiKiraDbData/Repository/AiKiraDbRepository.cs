using AikiraPOC_Core.Entities;
using AikiraPOC_Core.Entities.Base;
using AikiraPOC_Core.Interfaces.Persistence;
using AikiraPOC_Core.Models.ResponseModel;
using AikiraPOC_ServiceFactory.Factory;
using AikiraPOC_ServiceFactory.File;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AikiraPOC_Infrastructure.AiKiraDbData.Repository
{
    public class AiKiraDbRepository<T> : IRepository
    {      

        public dynamic GetFileInfo(FileInfoRequestModel fileInfoRequestModel)
        {
            dynamic jsonToReturn = null;
            try
            {
                string Connstr = Environment.GetEnvironmentVariable("sqldb_connection");
                var parameters = new DynamicParameters();              
                parameters.Add("@PsId", fileInfoRequestModel.intPSId, DbType.Int32, ParameterDirection.Input);            
                parameters.Add("@ProjectId", fileInfoRequestModel.intProjectId, DbType.Int32, ParameterDirection.Input);             
                parameters.Add("@BatchId", fileInfoRequestModel.intBatchId, DbType.Int32, ParameterDirection.Input);             
                parameters.Add("@FileId", fileInfoRequestModel.intFileId, DbType.Int32, ParameterDirection.Input);               
                string sqlQuery = "[dbo].[GetFileInfo]";
                FileManagerFactory fileFactory = new FileManagerFactory();
                IFileManager fileManager = fileFactory.GetFileManager(fileInfoRequestModel.intPSId);
                jsonToReturn = fileManager.GetFileData(Connstr, sqlQuery, parameters);
              
                return jsonToReturn;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message.ToString();
                jsonToReturn = JsonConvert.SerializeObject(strMsg);
                return jsonToReturn;
            }
          
        }
    }


}
