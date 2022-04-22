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
    internal class BookManager : IFileManager
    {
        public dynamic GetFileData(string Connstr, string sqlQuery, DynamicParameters parameters)
        {
            using (IDbConnection db = new SqlConnection(Connstr))
            {
                IList<FileInfoBookResponseModel> result = db.Query<FileInfoBookResponseModel>(sqlQuery, parameters, commandType: CommandType.StoredProcedure).AsList<FileInfoBookResponseModel>();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
}
