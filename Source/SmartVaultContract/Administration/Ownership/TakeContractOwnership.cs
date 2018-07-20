using Neo.SmartContract.Framework.Services.Neo;

namespace SmartWallet.Administration.Ownership
{
    public static class TakeContractOwnership
    {
        public static bool IsMethod(string method)
        {
            return method == "TakeContractOwnership";
        }

        public static object Execute(object[] args)
        {
            var ownerAddress = (byte[])args[0];
            if (!Runtime.CheckWitness(ownerAddress)) return false;

            Storage.Put(Storage.CurrentContext, "ContractOwner", ownerAddress);

            return true;
        }
    }
}
