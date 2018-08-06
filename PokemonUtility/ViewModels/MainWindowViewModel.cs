using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
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

            // 世代コンボボックスのアイテム設定
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

        // 世代コンボボックス
        private ObservableCollection<PersonViewModel> _Persons;

        public class generation 
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
