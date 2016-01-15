using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.ServiceBus.Messaging;
using Microsoft.Azure;
using Microsoft.ServiceBus;
using System.Reflection;
using Ninject;
using System.IO;
using Ninject.Modules;
using MundiPagg.Domain.Service.Interfaces;

namespace MundiPagg.ProcessQueue
{

    public class WorkerRole : RoleEntryPoint
    {
        const string QueueName = "mundipagg-queue";

        // QueueClient is thread-safe. Recommended that you cache rather than recreating it on every request
        QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        IQueueProcessorService queueProcessorService;

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = false; 
            Client.OnMessage((receivedMessage) =>
            {
                try
                {
                    queueProcessorService.ProcessMessage(receivedMessage);
                    receivedMessage.Complete();
                }
                catch
                {

                }
            }, options);

            CompletedEvent.WaitOne();

            Trace.WriteLine("Ending processing of messages");
        }


        public override bool OnStart()
        {
            //Starting Kernel
            Trace.TraceInformation("Starting Kernel");
            IKernel kernel = new StandardKernel();
            this.RegisterServices(kernel);
            Infra.IoC.Kernel.StartKernel(kernel);
            Trace.TraceInformation("Kernel Started");

            Trace.TraceInformation("Starting Logging");
            log4net.Config.XmlConfigurator.Configure();
            Trace.TraceInformation("Log Started");

            //Starting Listening Queue
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);

            Trace.TraceInformation("Starting Process Queue Service");
            queueProcessorService = Infra.IoC.Kernel.ResolveService<IQueueProcessorService>();
            Trace.TraceInformation("Process Queue Service Started");
            return base.OnStart();
        }

        public override void OnStop()
        {
            Trace.TraceInformation("Stoping Kernel");
            Infra.IoC.Kernel.StopKernel();
            CompletedEvent.Set();
            Trace.TraceInformation("Kernel Stopped");
            base.OnStop();
        }

        public void RegisterServices(IKernel kernel)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            foreach (string pathAssembly in Directory.GetFiles(path, "MundiPagg.*.dll"))
            {
                var assembly = Assembly.LoadFrom(pathAssembly);

                assembly.GetTypes()
                        .Where(x => x.IsSubclassOf(typeof(NinjectModule))).ToList().ForEach(x =>
                        {
                            var t = (NinjectModule)Activator.CreateInstance(x, new object[] { false });
                            kernel.Load(t);
                        });
            }
        }
    }
}
