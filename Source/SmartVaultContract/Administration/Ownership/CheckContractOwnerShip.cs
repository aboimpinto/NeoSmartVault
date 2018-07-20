using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace SmartWallet.Administration.Ownership
{
    public static class CheckContractOwnership
    {
        public static bool IsMethod(string method)
        {
            return method == "CheckContractOwnership";
        }

        public static string Execute(object[] args)
        {
            var isContractOwned = Storage.Get(Storage.CurrentContext, "ContractOwner").AsString();

            if (isContractOwned == "")
            {
                return "NOT_OWNED";
            }

            return "OWNED";
        }
    }
}
