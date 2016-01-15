﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web;
using Ninject.Web.Mvc;
using Ninject.Web.Common;
using Ninject.Modules;
using MundiPagg.Repository.Context.Interfaces;
using System.Data.Entity;
using MundiPagg.Repository.Context;
using MundiPagg.Infra.Data.Interfaces;
using MundiPagg.Infra.Data.Context;


namespace MundiPagg.Repository.Configuration
{
    public class RepositoryModule : NinjectModule
    {
        bool isInHttpContext = false;

        public RepositoryModule(bool _isInHttpContext)
        {
            this.isInHttpContext = _isInHttpContext;
        }

        public override void Load()
        {

            if (isInHttpContext)
            {
                Bind<DbContext>().ToSelf().InRequestScope();
                Bind(typeof(IDbContext)).To(typeof(MundiPaggContext)).InRequestScope();
                Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>)).InRequestScope();
            }
            else
            {
                Bind<DbContext>().ToSelf().InSingletonScope();
                Bind(typeof(IDbContext)).To(typeof(MundiPaggContext)).InSingletonScope();
                Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>)).InThreadScope();
            }
            

            this.Bind(x => x.FromThisAssembly()
                            .SelectAllClasses()
                            .Excluding<DbContext>()
                            .Excluding(typeof(MundiPaggContext))
                            .Excluding(typeof(RepositoryBase<>))
                            .Excluding<IMapping>()
                            .BindDefaultInterface()
                            .Configure((Ninject.Syntax.IBindingWhenInNamedWithOrOnSyntax<object> c) =>
                            {
                                if (isInHttpContext)
                                    c.InRequestScope();
                                else
                                    c.InTransientScope();
                            })
            );

        }
    }
}
