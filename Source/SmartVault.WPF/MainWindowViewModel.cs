using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SmartVault.WPF
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields 
        private readonly SmartVaultModel _smartVaultModel;
        #endregion

        #region Public Properties 
        public RelayCommand CheckSmartVaultEnableCommand { get; private set; }
        #endregion

        #region Constructor 
        public MainWindowViewModel()
        {
            this._smartVaultModel = new SmartVaultModel();      // TODO: this need to be injected in the future

            this.CheckSmartVaultEnableCommand = new RelayCommand(this.HandleCheckSmartVaultEnable);
        }
        #endregion

        #region Private Methods
        private async void HandleCheckSmartVaultEnable()
        {
            var isContractEnable = await this._smartVaultModel.CheckContractEnabled();
        }
        #endregion
    }
}
