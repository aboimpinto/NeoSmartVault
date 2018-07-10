using System;
using System.Text;
using System.Threading.Tasks;
using NeoModules.Core;
using NeoModules.JsonRpc.Client;
using NeoModules.RPC;

namespace SmartVault.WPF
{
    public class SmartVaultModel
    {
        #region Private Fields 
        private const string ContractHash = "0x79f413551019bfda0b183cca9f4296b4425537fa";
        private const string BlockchainUrl = "http://localhost:30333";

        private readonly NeoApiService _neoApiService;
        #endregion

        #region Constructor 
        public SmartVaultModel()
        {
            var rpcClient = new RpcClient(new Uri(BlockchainUrl));
            this._neoApiService = new NeoApiService(rpcClient);
        }
        #endregion

        #region Public Methods 
        public async Task<bool> CheckContractEnabled()
        {
            var script = NeoModules.NEP6.Utils.GenerateScript(UInt160.Parse(ContractHash).ToArray(), "CheckContractEnabled", new object[] { });

            var result = await this._neoApiService.Contracts.InvokeScript.SendRequestAsync(script.ToHexString());
            var content = Encoding.UTF8.GetString(result.Stack[0].Value.ToString().HexToBytes());

            if (content.StartsWith("ERROR"))
            {
                throw new ApplicationException(content);
            }

            return Encoding.UTF8.GetString(result.Stack[0].Value.ToString().HexToBytes()) == "TRUE" ? true : false;
        }
        #endregion
    }
}
