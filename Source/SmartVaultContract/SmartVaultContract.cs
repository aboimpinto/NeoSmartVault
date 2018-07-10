using Neo.SmartContract.Framework;
using SmartWallet.Administration.ContractEnabled;
using SmartWallet.Administration.Ownership;

namespace SmartWallet
{
    public class SmartVaultContract : SmartContract
    {
        public static string Main(string method, params object[] args)
        {
            if (CheckContractEnabled.Execute(args) == "TRUE")
            {
                if (CheckCallOwnership.Execute(args) == "YES")
                {
                    // Execute administrative methods on the smart contract.
                    if (DisableContract.IsMethod(method))
                    {
                        return DisableContract.Execute(args);
                    }
                }
                else
                {
                    // Execute user Vault methods. 
                }
            }
            else
            {
                // Available methods when the contract is disable
                if (CheckContractEnabled.IsMethod(method))
                {
                    return CheckContractEnabled.Execute(args);
                }

                if (CheckContractOwnership.Execute(args) == "OWNED")
                {
                    if (EnableContract.IsMethod(method))
                    {
                        var result = EnableContract.Execute(args);
                        return result;
                    }
                }
                else
                {
                    if (TakeContractOwnership.IsMethod(method))
                    {
                        var result = TakeContractOwnership.Execute(args);
                        return result;
                    }
                }
            }

            return "ERROR 001: Unknown method " + method;
        }
    }
}
