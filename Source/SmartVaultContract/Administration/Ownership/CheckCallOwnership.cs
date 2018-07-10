using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace SmartWallet.Administration.Ownership
{
    public static class CheckCallOwnership
    {
        public static bool IsMethod(string method)
        {
            return method == "CheckCallOwnership";
        }

        public static string Execute(object[] args)
        {
            var address = args[0].ToString().ToScriptHash();

            if (Runtime.CheckWitness(address))
            {
                return "YES";
            }

            return "NO";
        }
    }
}
