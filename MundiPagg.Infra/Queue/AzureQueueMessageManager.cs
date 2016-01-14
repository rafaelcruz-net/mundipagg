using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MundiPagg.Infra.Queue
{
    public class AzureQueueMessageManager
    {
        private static QueueClient _queueClient;
        private static readonly string QUEUE_NAME = "mundipagg-queue";
        private static TimeSpan TIMEOUT = new TimeSpan(hours: 0, minutes: 1, seconds: 0);

        static AzureQueueMessageManager()
        {
            Setup();
        }

        private static void Setup()
        {
            if (ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"] == null)
                throw new ConfigurationErrorsException("Configuração da Fila de Notificação não encontrada");

            string configurationSettings = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"].ToString();

            _queueClient = QueueClient.CreateFromConnectionString(configurationSettings, QUEUE_NAME);
        }

        private static QueueClient QueueNotification
        {
            get
            {
                return _queueClient;
            }
        }

        /// <summary>
        /// Cria a instancia do AzureQueueNotification
        /// </summary>
        /// <param name="senderUserId">Usuario que gerou a Notificação</param>
        /// <param name="senderUsername">Nome do Usuario que gerou a Notificação</param>
        /// <param name="objectRef">Referencia do objeto que gerou a notificação</param>
        /// <param name="notificationTypeEnum">Tipo de Notificação </param>
        /// <param name="extraData">Dados Extras da notificações</param>
        /// <returns>AzureQueueNotification</returns>
        public static void Enqueue(string senderUserId, string senderUsername, string objectRef, AzureMessageType messageTypeEnum, Dictionary<string, object> extraData = null)
        {
            AzureMessage notification = Infra.Queue.AzureMessage.CreateAzureMessage(senderUserId, senderUsername, objectRef, messageTypeEnum, extraData);
            Enqueue(notification);
        }
        private static void Enqueue(AzureMessage broker)
        {
            Enqueue(broker, retry: false);
        }

        public static AzureMessage Dequeue()
        {
            return Dequeue(retry: false);
        }

        #region Private Methods

        private static void Enqueue(AzureMessage broker, bool retry = false)
        {
            try
            {
                BrokeredMessage message = new BrokeredMessage(broker);
                //Criando um identificador para a mensage
                message.MessageId = Guid.NewGuid().ToString();

                QueueNotification.Send(message);
            }
            catch (MessagingException e)
            {
                if (retry)
                    throw new Exception(e.Message, e);

                if (!e.IsTransient)
                    throw new Exception(e.Message, e);
                else
                {
                    //If transient error/exception, let's back-off for 1 seconds and retry
                    Enqueue(broker, true);
                    Thread.Sleep(1000);
                }
            }
        }

        private static AzureMessage Dequeue(bool retry = false)
        {
            try
            {
                BrokeredMessage message = null;

                message = QueueNotification.Receive(TIMEOUT);

                if (message == null)
                    return null;

                AzureMessage notification = message.GetBody<AzureMessage>();
                message.Complete();

                return notification;
            }
            catch (MessagingException e)
            {
                if (retry)
                    throw new Exception(e.Message, e);

                if (!e.IsTransient)
                    throw new Exception(e.Message, e);
                else
                {
                    //If transient error/exception, let's back-off for 1 seconds and retry
                    Thread.Sleep(1000);
                    return Dequeue(true);
                }
            }

        }
        #endregion

    }
}
