using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace S7App
{
    class MainViewModel : BindableBase
    {
        private bool _db1Dbx00;

        private S7PlcService _plcService;

        public MainViewModel()
        {
            _plcService = new S7PlcService();
            _plcService.ValuesRefreshed += OnPlcValuesRefreshed;
            _plcService.Connect("192.168.0.101", 0, 1);
            WriteDb1Dbx00Command = new DelegateCommand(async () => await ExecuteWriteDb1Dbx00Command());
        }

        public bool Db1Dbx00
        {
            get => _db1Dbx00;
            set => SetProperty(ref _db1Dbx00, value);
        }

        public ICommand WriteDb1Dbx00Command { get; }

        private async Task ExecuteWriteDb1Dbx00Command()
        {
            await _plcService.WriteDb1Dbx00(!Db1Dbx00);
        }

        private void OnPlcValuesRefreshed(object sender, EventArgs e)
        {
            Db1Dbx00 = _plcService.Db1Dbx00;
        }
    }
}
