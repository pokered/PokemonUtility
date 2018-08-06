using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using PokemonUtility.Models;
using System.Collections.Generic;

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
            var aaa = new SoftGeneration();
            aaa.ID = 0;
            aaa.Name = "aaa";

            var list = new List<SoftGeneration>();
            list.Add(aaa);

            SoftGenerations = list;

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

        private IList<SoftGeneration> _softGenerations;
        public IList<SoftGeneration> SoftGenerations
        {
            get { return _softGenerations; }
            // privateにする
            set { _softGenerations = value; }
        }
    }
}
