using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Infra.Queue
{
    public class AzureMessage
    {

        /// <summary>
        /// Cria a instancia do AzureQueueNotification
        /// </summary>
        /// <param name="senderUserId">Usuario que gerou a Notificação</param>
        /// <param name="value">Objeto que gerou a Notificação</param>
        /// <param name="notificationType">Tipo de Notificação </param>
        /// <returns>AzureQueueNotification</returns>
        public static AzureMessage CreateAzureMessage(string senderUserId, string senderUserName, string objectRef, AzureMessageType messageType , Dictionary<string, object> extraData = null)
        {
            AzureMessage notification = new AzureMessage();

            notification.SenderUserId = senderUserId;
            notification.ObjectRef = objectRef;
            notification.ExtraData = extraData;
            notification.CreationDate = DateTime.Now;
            notification.SenderUserName = senderUserName;
            notification.MessageType = (int)messageType;

            return notification;

        }

        /// <summary>
        /// Tipo de Notificação 
        /// </summary>
        public int MessageType
        {
            get;
            set;
        }

        /// <summary>
        /// Referencia do objeto que gerou a Notificação
        /// </summary>
        public string ObjectRef
        {
            get;
            set;
        }



        /// <summary>
        /// Usuario que gerou a Notificação
        /// </summary>
        public string SenderUserId
        {
            get;
            set;
        }

        /// <summary>
        /// Nome do Usuario que gerou a Notificação
        /// </summary>
        public string SenderUserName
        {
            get;
            set;
        }

        /// <summary>
        /// Data da Criação da Notificação
        /// </summary>
        public DateTime CreationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Informações extras a ser passada para a mensagem
        /// </summary>
        public Dictionary<string, object> ExtraData
        {
            get;
            set;
        }
    }
}
