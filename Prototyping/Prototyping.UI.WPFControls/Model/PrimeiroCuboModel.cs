using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace Prototyping.UI.WPFControls.Model
{
    internal class PrimeiroCuboModel
    {
        private IDbConnection _dbconnection;
        private DbProviderFactory _connectionFactory;

        public PrimeiroCuboModel(string connectionString)
        {
            ConnectionStringSettings  connectionStringSettings = null;
            
            if (string.IsNullOrEmpty (connectionString)) {throw new ArgumentNullException ("connectionString")}
            connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionString];
            _connectionFactory = DbProviderFactories.GetFactory (connectionStringSettings.ProviderName);
            _dbconnection = _connectionFactory.CreateConnection();
            _dbconnection.ConnectionString = connectionStringSettings.ConnectionString;
        }

        public string ConnectionString { get { return _dbconnection.ConnectionString; } }
    }
}
