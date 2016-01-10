using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Common;
using System.Reflection;
using System.Data.Entity.Infrastructure;
using MundiPagg.Infra.Data.Context;
using MundiPagg.Repository.Context.Interfaces;
using MundiPagg.Infra.Data.Interfaces;

namespace MundiPagg.Repository.Context
{
    public class MundiPaggContext : BaseContext, IDbContext
    {

        #region Constructor
        public MundiPaggContext()
            : base("MundiPagg.Connection")
        {
            Database.SetInitializer<MundiPaggContext>(null);
        }

        public MundiPaggContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Database.SetInitializer<MundiPaggContext>(new CreateDatabaseIfNotExists<MundiPaggContext>());
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //Fazendo o mapeamento com o banco de dados
            var typesToMapping = (from x in Assembly.GetExecutingAssembly().GetTypes()
                                  where x.IsClass && typeof(IMapping).IsAssignableFrom(x)
                                  select x).ToList();


            //Adicionando os outros relacionamentos
            foreach (var mapping in typesToMapping)
            {
                dynamic mappingClass = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(mappingClass);
            }



            base.OnModelCreating(modelBuilder);
        }


        public new string ExecutionLog
        {
            get
            {
                return base.ExecutionLog;
            }
            set
            {
                base.ExecutionLog = value;
            }
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new DbEntityEntry<T> Entry<T>(T entity) where T : class
        {
            return base.Entry<T>(entity);
        }

        public new DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public virtual void SetModified(object model, EntityState state)
        {
            ((IObjectContextAdapter)this)
                            .ObjectContext
                            .ObjectStateManager
                            .ChangeObjectState(model,
                            state);
        }

        public virtual void Detach(object model)
        {

            ((IObjectContextAdapter)this).ObjectContext.Detach(model);

        }

    }
}
