using Neo.SmartContract.Framework.Services.Neo;

namespace SmartWallet.Administration.ContractEnabled
{
    public static class DisableContract
    {
        public static bool IsMethod(string method)
        {
            return method == "DisableContract";
        }

        public static string Execute(object[] args)
        {
            // TODO: Only if some address own the Contract this can be executed.

            Storage.Put(Storage.CurrentContext, "ContractEnabled", "0");
            return "Contract disable.";
        }
    }
}
