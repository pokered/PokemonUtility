using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonUtility.Models
{
    class SoftGeneration
    {
        /// <summary>
        /// IDを取得または設定します。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ソフトの世代名を取得または設定します。
        /// </summary>
        public string Name { get; set; }
    }
}
