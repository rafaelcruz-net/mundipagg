using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Infra.Queue
{
    public enum AzureMessageType
    {
        NEW_TRANSACTION = 1,
        NEW_TRANSACTION_INSTANT_BUY = 2,
    }
}
