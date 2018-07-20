using Neo.SmartContract.Framework.Services.Neo;

namespace SmartWallet.Administration.ContractEnabled
{
    public static class EnableContract
    {
        public static bool IsMethod(string method)
        {
            return method == "EnableContract";
        }

        public static bool Execute(object[] args)
        {
            var ownerAddress = Storage.Get(Storage.CurrentContext, "ContractOwner");
            if (!Runtime.CheckWitness(ownerAddress)) return false;

            Storage.Put(Storage.CurrentContext, "ContractEnabled", "1");
            return true;
        }
    }
}
