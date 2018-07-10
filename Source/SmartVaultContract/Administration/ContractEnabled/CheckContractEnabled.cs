using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace SmartWallet.Administration.ContractEnabled
{
    public static class CheckContractEnabled
    {
        public static bool IsMethod(string method)
        {
            return method == "CheckContractEnabled";
        }

        public static string Execute(object[] args)
        {
            var isContractEnabled = Storage.Get(Storage.CurrentContext, "ContractEnabled").AsString();

            if (isContractEnabled == "1")
            {
                return "TRUE";
            }

            return "FALSE";
        }
    }
}
