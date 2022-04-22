using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace AikiraPOC_ServiceFactory.File
{
    public interface IFileManager
    {
        dynamic GetFileData(string Connstr, string sqlQuery, DynamicParameters parameters);
    }
}
