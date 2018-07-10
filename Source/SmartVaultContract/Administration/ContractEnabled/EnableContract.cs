using Neo.SmartContract.Framework.Services.Neo;

namespace SmartWallet.Administration.ContractEnabled
{
    public static class EnableContract
    {
        public static bool IsMethod(string method)
        {
            return method == "EnableContract";
        }

        public static string Execute(object[] args)
        {
            // TODO: Only if some address own the Contract this can be executed.

            Storage.Put(Storage.CurrentContext, "ContractEnabled", "1");
            return "Contract enabled.";
        }
    }
}
