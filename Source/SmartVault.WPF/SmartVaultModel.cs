using System;
using System.Text;
using System.Threading.Tasks;
using NeoModules.Core;
using NeoModules.JsonRpc.Client;
using NeoModules.NEP6;
using NeoModules.NEP6.Models;
using NeoModules.Rest.Services;
using NeoModules.RPC;
using Newtonsoft.Json;
using Helper = NeoModules.KeyPairs.Helper;

namespace SmartVault.WPF
{
    public class SmartVaultModel
    {
        #region Private Fields 
        private const string ContractHash = "0x4b49a30c537c5c005ca968a872b3c950897b07b1";
        private const string BlockchainUrl = "http://localhost:30333";

        private readonly NeoApiService _neoApiService;
        private readonly Account _importedAccount;

        private const string AdministratorAddress = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y";
        #endregion

        #region Constructor 
        public SmartVaultModel()
        {
            var rpcClient = new RpcClient(new Uri(BlockchainUrl));
            this._neoApiService = new NeoApiService(rpcClient);
            var restService = new NeoScanRestService("http://localhost:4000/api/main_net/v1/");

            var walletManager = new WalletManager(restService, rpcClient);
            this._importedAccount = walletManager.ImportAccount("KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr", "PrivNetKey");
        }
        #endregion

        #region Public Methods 
        public async Task<bool> CheckContractEnabled()
        {
            var script = Utils.GenerateScript(UInt160.Parse(ContractHash).ToArray(), "CheckContractEnabled", new object[] { });

            var result = await this._neoApiService.Contracts.InvokeScript.SendRequestAsync(script.ToHexString());
            var content = Encoding.UTF8.GetString(result.Stack[0].Value.ToString().HexToBytes());

            if (content.StartsWith("ERROR"))
            {
                throw new ApplicationException(content);
            }

            var resultBytes = result.Stack[0].Value.ToString().HexToBytes();
            return Encoding.UTF8.GetString(resultBytes) == "TRUE";
        }

        public async Task<bool> CheckIfContractHasOwner()
        {
            var script = Utils.GenerateScript(UInt160.Parse(ContractHash).ToArray(), "CheckContractOwnership", new object[] { });

            var result = await this._neoApiService.Contracts.InvokeScript.SendRequestAsync(script.ToHexString());
            var content = Encoding.UTF8.GetString(result.Stack[0].Value.ToString().HexToBytes());

            if (content.StartsWith("ERROR"))
            {
                throw new ApplicationException(content);
            }

            var resultBytes = result.Stack[0].Value.ToString().HexToBytes();
            return Encoding.UTF8.GetString(resultBytes) == "OWNED";
        }

        public async Task EnableContract()
        {
            if (this._importedAccount.TransactionManager is AccountSignerTransactionManager accountSignerTransactionManager)
            {
                // Call contract
                var scriptHash = UInt160.Parse(ContractHash).ToArray();
                const string operation = "EnableContract";
                var arguments = new object[] { Helper.ToScriptHash(AdministratorAddress).ToArray() };

                var contractCallTx = await accountSignerTransactionManager.CallContract(scriptHash, operation, arguments);
            }

            //var arguments = new object[] { Helper.ToScriptHash(AdministratorAddress).ToArray() };

            //var x = JsonConvert.SerializeObject(arguments);

            //var script = Utils.GenerateScript(UInt160.Parse(ContractHash).ToArray(), "EnableContract", new object[] { });

            //var result = await this._neoApiService.Contracts.InvokeScript.SendRequestAsync(script.ToHexString());
            //var content = Encoding.UTF8.GetString(result.Stack[0].Value.ToString().HexToBytes());

            //if (content != "Contract enabled.")
            //{
            //    throw new ApplicationException($"There was an error enabling the contract ({content})");
            //}
        }

        public async Task TakeOwnship()
        {
            if (this._importedAccount.TransactionManager is AccountSignerTransactionManager accountSignerTransactionManager)
            {
                // Call contract
                var scriptHash = UInt160.Parse(ContractHash).ToArray();
                const string operation = "TakeContractOwnership";
                var arguments = new object[] { Helper.ToScriptHash(AdministratorAddress).ToArray() };

                var contractCallTx = await accountSignerTransactionManager.CallContract(scriptHash, operation, arguments);
            }
        }
        #endregion
    }
}
