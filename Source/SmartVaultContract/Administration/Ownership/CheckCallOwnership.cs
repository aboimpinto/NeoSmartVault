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
            var address = (byte[])args[0];

            if (Runtime.CheckWitness(address))
            {
                return "YES";
            }

            return "NO";
        }
    }
}
