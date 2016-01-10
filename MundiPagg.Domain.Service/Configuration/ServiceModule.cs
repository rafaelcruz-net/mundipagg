using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Conventions;
using Ninject.Web;
using Ninject.Web.Mvc;
using Ninject.Web.Common;


namespace MundiPagg.Domain.Service.Configuration
{
    public class ServiceModule : NinjectModule
    {
        bool isInHttpContext = false;

        public ServiceModule(bool _isInHttpContext)
        {
            this.isInHttpContext = _isInHttpContext;
        }

        public override void Load()
        {
            this.Bind(x => x.FromThisAssembly()
                            .SelectAllClasses()
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
