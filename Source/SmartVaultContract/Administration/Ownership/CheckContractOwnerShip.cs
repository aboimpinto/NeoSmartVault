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
            return "";
        }
    }
}
