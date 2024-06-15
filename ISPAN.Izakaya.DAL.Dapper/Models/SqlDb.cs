using System;
using System.Configuration;

namespace ISPAN.Izakaya.DAL.Dapper.Models
{
    public class SqlDb
    {
        public static string GetConnectionString(string keyOfConn)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings[keyOfConn].ToString();
                return conn;
            }
            catch (Exception)
            {
                throw new Exception($"找不到名稱為{keyOfConn}的連線字串,請檢查是否正確");
            }
        }
    }
}
