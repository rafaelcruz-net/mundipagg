using log4net;
using MundiPagg.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MundiPagg.Infra.Data.Context
{
    public class BaseContext : DbContext, IUnitOfWork
    {
        ILog Logger = log4net.LogManager.GetLogger("Database.Context");
        
        public string ExecutionLog
        {
            get;
            set;
        }

             
        public BaseContext(DbConnection connection, bool contextOwnsConnection) : base(connection,contextOwnsConnection)
        {
            this.StartUpContext();
        }

        public BaseContext(string nameOrConnectionString) 
            :base(nameOrConnectionString)
        {
            this.StartUpContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            return this.Database.BeginTransaction();
        }


        private void StartUpContext()
        {
            var enableLog = false;

            if (ConfigurationManager.AppSettings["EntityFramework.Log.Enable"] != null)
            {
                bool enable;

                if (bool.TryParse(ConfigurationManager.AppSettings["EntityFramework.Log.Enable"].ToString(), out enable))
                    enableLog = enable;
            }

            if (enableLog)
            {
                this.Database.Log = (message =>
                {
                    Logger.Info(message);
                    this.ExecutionLog += message;
                });
            }
        }

    }
}
