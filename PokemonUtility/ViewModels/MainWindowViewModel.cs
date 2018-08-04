using Prism.Mvvm;
using Prism.Commands;
using PokemonUtility.Models;

namespace PokemonUtility.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private MainModel model;

        public MainWindowViewModel()
        {
            model = new MainModel();
        }

        private DelegateCommand calcComamnd;
        public DelegateCommand calcCommand
        {
            get { return calcComamnd = calcComamnd ?? new DelegateCommand(CHangeTitle); }
        }

        private void CHangeTitle()
        {
            Title = model.CHangeTitle();
        }
    }
}
