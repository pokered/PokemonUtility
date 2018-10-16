﻿using PokemonUtility.Const;
using PokemonUtility.Models.Abstract;
using PokemonUtility.Struct;
using System;
using System.Collections.Generic;

namespace PokemonUtility.Models.Main
{
    class MainWindowModel : WindowModel
    {
        #region Singleton

        static MainWindowModel Instance;
        public static MainWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new MainWindowModel();
            return Instance;
        }

        #endregion

        // 対戦結果
        private int _battleResult = BattleResultConst.WIN;
        public int BattleResult
        {
            get { return _battleResult; }
            set { SetProperty(ref _battleResult, value); }
        }

        // 世代リスト
        public List<SoftGeneration> SoftGenerationList { get; set; }

        // 世代ID
        private int _softGenerationId = 0;
        public int SoftGenerationId
        {
            get { return _softGenerationId; }
            set { SetProperty(ref _softGenerationId, value); }
        }

        // ログ
        private string _log = "";
        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }

        public MainWindowModel()
        {
            // 世代リスト作成
            SoftGenerationList = new List<SoftGeneration>();
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.SUN_MOON, Name = SoftGeneration.SUN_MOON_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.X_Y, Name = SoftGeneration.X_Y_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.BLACK_WHITE, Name = SoftGeneration.BLACK_WHITE_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.DIAMOND_PEARL, Name = SoftGeneration.DIAMOND_PEARL_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.RUBY_SAPPHIRE, Name = SoftGeneration.RUBY_SAPPHIRE_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.GOLD_SILVER, Name = SoftGeneration.GOLD_SILVER_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.RED_GREEN, Name = SoftGeneration.RED_GREEN_NAME });
        }

        public void AddLog(string message)
        {
            // メッセージに時間を付加
            message += " " + DateTime.Now.ToString("HH:mm");

            // メッセージを繋げる
            string log = message + "\n" + Log;

            Log = log.Trim();
        }
    }
}
