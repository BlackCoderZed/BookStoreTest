using BookStoreDataAccess.Utils;
using BookStoreWebAPI.Models.Common;
using Microsoft.Data.SqlClient;
using System.Xml.Serialization;

namespace BookStoreWebAPI.Utils
{
    public class DataAccessHelper
    {
        /// <summary>
        /// Initialize Dataaccess
        /// </summary>
        public static void Initialize()
        {
            string xmlPath = GetFilePath(Constants.DB_CONFIG_PATH);
            DBConfiguration DBInfo = new DBConfiguration();

            using (var reader = new StreamReader(xmlPath))
            {
                DBInfo = (DBConfiguration)new XmlSerializer(typeof(DBConfiguration)).Deserialize(reader);
            }

            string connectionString = CreateConnectionString(DBInfo);

            DataAccessUtil dataAccessUtil= new DataAccessUtil(connectionString);
        }

        /// <summary>
        /// Create Connection String
        /// </summary>
        /// <param name="dbConfig"></param>
        /// <returns></returns>
        private static string CreateConnectionString(DBConfiguration dbConfig)
        {
            try
            {
                // Connection string builder
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

                // Connection Properties
                sqlBuilder.MultipleActiveResultSets = true;
                //sqlBuilder.ApplicationName = "EntityFramework";

                sqlBuilder.DataSource = dbConfig.Server;
                sqlBuilder.InitialCatalog = dbConfig.Database;
                sqlBuilder.UserID = dbConfig.Username;
                sqlBuilder.Password = dbConfig.Password;
                sqlBuilder.TrustServerCertificate = true;

                string providerString = sqlBuilder.ToString();

                return providerString;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Config FilePath
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns></returns>
        private static string GetFilePath(string sFile)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFile);

        }
    }
}
