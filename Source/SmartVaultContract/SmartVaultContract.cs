using System;
using Neo.SmartContract.Framework;
using SmartWallet.Administration.ContractEnabled;
using SmartWallet.Administration.Ownership;

namespace SmartWallet
{
    public class SmartVaultContract : SmartContract
    {
        // INPUT:   0710
        // OUTPUT:  07

        public static object Main(string method, params object[] args)
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

                if (CheckContractOwnership.IsMethod(method))
                {
                    return CheckContractOwnership.Execute(args);
                }

                if (CheckContractOwnership.Execute(args) == "OWNED")
                {
                    if (EnableContract.IsMethod(method))
                    {
                        return EnableContract.Execute(args);
                    }
                }
                else
                {
                    if (TakeContractOwnership.IsMethod(method))
                    {
                        return TakeContractOwnership.Execute(args);
                    }
                }
            }

            return "ERROR 001: Unknown method " + method;
        }
    }
}
