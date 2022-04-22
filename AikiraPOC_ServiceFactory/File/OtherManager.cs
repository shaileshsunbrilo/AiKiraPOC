using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AikiraPOC_Core.Models.ResponseModel;
using Dapper;
using Newtonsoft.Json;

namespace AikiraPOC_ServiceFactory.File
{
    internal class OtherManager : IFileManager
    {
        public dynamic GetFileData(string Connstr, string sqlQuery, DynamicParameters parameters)
        {
            List<FileInfoOtherResponseModel> objFileInfoOtherResponseModel = new List<FileInfoOtherResponseModel>();
            using (IDbConnection db = new SqlConnection(Connstr))
            {
                var result = db.Query(sqlQuery, parameters, commandType: CommandType.StoredProcedure);

                foreach (var item in result.AsList())
                {
                    FileInfoOtherResponseModel objLocalFileInfoOtherResponseModel = new FileInfoOtherResponseModel();
                    foreach (var localVar in item)
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        dict.Add(localVar.Key, localVar.Value);
                        objLocalFileInfoOtherResponseModel.DynamicProperties.Add(dict);
                    }
                    objFileInfoOtherResponseModel.Add(objLocalFileInfoOtherResponseModel);
                }
                return JsonConvert.SerializeObject(objFileInfoOtherResponseModel);
            }
        }
    }
}
