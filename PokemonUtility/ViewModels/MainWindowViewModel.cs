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
            var softGenerationList = new List<SoftGeneration>();
            softGenerationList.Add(new SoftGeneration(){ID = 0, Name = "赤緑"});
            softGenerationList.Add(new SoftGeneration(){ID = 1, Name = "金銀"});
            softGenerationList.Add(new SoftGeneration(){ID = 2, Name = "ＲＳ"});
            softGenerationList.Add(new SoftGeneration(){ID = 3, Name = "ＤＰ"});
            softGenerationList.Add(new SoftGeneration(){ID = 4, Name = "黒白"});
            softGenerationList.Add(new SoftGeneration(){ID = 5, Name = "ＸＹ"});
            softGenerationList.Add(new SoftGeneration(){ID = 6, Name = "ＳＭ"});
            // 先にselecteditemに値を設定しないとダメ
            this.SelectedSoftGeneration = softGenerationList[0];
            this.SoftGenerations = softGenerationList;

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

        public SoftGeneration SelectedSoftGeneration { get; set; }	// 変更通知
        private List<SoftGeneration> _softGenerations;
        public List<SoftGeneration> SoftGenerations
        {
            get { return _softGenerations; }
            // privateにする
            set { _softGenerations = value; }
        }

        public enum battle_result
        {
            win = 0,
            lose = 1,
            draw = 2
        }
    }
}
