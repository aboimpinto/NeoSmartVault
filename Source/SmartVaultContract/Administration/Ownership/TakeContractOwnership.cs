using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWallet.Administration.Ownership
{
    public static class TakeContractOwnership
    {
        public static bool IsMethod(string method)
        {
            return method == "TakeOwnewship";
        }

        public static string Execute(object[] args)
        {
            return "";
        }
    }
}
