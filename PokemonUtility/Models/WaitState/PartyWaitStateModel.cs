using PokemonUtility.Const;
using System.Collections.Generic;

namespace PokemonUtility.Models.WaitState
{
    class PartyWaiStatetModel
    {
        private List<WaitStateModel> _waitStateList = new List<WaitStateModel>();

        public WaitStateModel WaitState1 { get { return _waitStateList[PartyConst.PARTY_INDEX1]; } }
        public WaitStateModel WaitState2 { get { return _waitStateList[PartyConst.PARTY_INDEX2]; } }
        public WaitStateModel WaitState3 { get { return _waitStateList[PartyConst.PARTY_INDEX3]; } }
        public WaitStateModel WaitState4 { get { return _waitStateList[PartyConst.PARTY_INDEX4]; } }
        public WaitStateModel WaitState5 { get { return _waitStateList[PartyConst.PARTY_INDEX5]; } }
        public WaitStateModel WaitState6 { get { return _waitStateList[PartyConst.PARTY_INDEX6]; } }

        public PartyWaiStatetModel()
        {
            for (int i = 0; i < 6; i++)
            {
                _waitStateList.Add(new WaitStateModel());
            }
        }
    }
}
