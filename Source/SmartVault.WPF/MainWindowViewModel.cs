using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SmartVault.WPF
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields 
        private readonly SmartVaultModel _smartVaultModel;
        private bool _isContractEnable;
        private bool _canEnableContract;
        private bool _isContractWithoutOwner;

        #endregion

        #region Public Properties 

        public bool IsContractEnable
        {
            get => this._isContractEnable;
            set
            {
                this._isContractEnable = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsContractWithoutOwner
        {
            get => this._isContractWithoutOwner;
            set
            {
                this._isContractWithoutOwner = value;
                this.RaisePropertyChanged();
            }
        }

        public bool CanEnableContract
        {
            get => this._canEnableContract;
            set
            {
                this._canEnableContract = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand EnableSmartVaultCommand { get; private set; }

        public RelayCommand TakeOwnershipCommand { get; private set; }

        public RelayCommand RefreshCommand { get; private set; }
        #endregion

        #region Constructor 
        public MainWindowViewModel()
        {
            this._smartVaultModel = new SmartVaultModel();      // TODO: this need to be injected in the future

            this.EnableSmartVaultCommand = new RelayCommand(this.HandleEnableSmartVault);
            this.TakeOwnershipCommand = new RelayCommand(this.HandleTakeOwnership);
            this.RefreshCommand = new RelayCommand(this.HandleRefresh);

            this.CheckIfContractHasOwner();
        }
        #endregion

        #region Private Methods
        private void HandleRefresh()
        {
            this.CheckIfContractHasOwner();
        }

        private async void HandleEnableSmartVault()
        {
            await this._smartVaultModel.EnableContract();

            this.CheckContractEnable();
        }

        private async void HandleTakeOwnership()
        {
            await this._smartVaultModel.TakeOwnship();
            this.IsContractWithoutOwner = false;
        }

        private async void CheckIfContractHasOwner()
        {
            this.IsContractWithoutOwner = !await this._smartVaultModel.CheckIfContractHasOwner();

            if (this.IsContractWithoutOwner)
            {
                this.IsContractEnable = false;
            }
            else
            {
                this.CheckContractEnable();
            }
        }

        private async void CheckContractEnable()
        {
            this.IsContractEnable = await this._smartVaultModel.CheckContractEnabled();
            this.CanEnableContract = !this.IsContractEnable;
        }
        #endregion
    }
}
