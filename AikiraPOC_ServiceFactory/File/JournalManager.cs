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
    internal class JournalManager : IFileManager
    {
        public dynamic GetFileData(string Connstr, string sqlQuery, DynamicParameters parameters)
        {
            using (IDbConnection db = new SqlConnection(Connstr))
            {
                IList<FileInfoJournalResponseModel> result = db.Query<FileInfoJournalResponseModel>(sqlQuery, parameters, commandType: CommandType.StoredProcedure).AsList<FileInfoJournalResponseModel>();
                return JsonConvert.SerializeObject(result);
            }   
        }
    }
}
