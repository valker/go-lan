using System;
using Valker.PlayOnLan.Api.Game;

namespace MyGoban
{
	public class GoRules : IGameType
    {
		
		public string Name {
			get {
				return "Го";
			}
		}
		
		public override string ToString() {
			return Name + ',' + ID;
		}
		
		public IGameServer CreateServer()
		{
			throw new NotImplementedException();
		}
		
		public IGameClient CreateClient()
		{
			throw new NotImplementedException();
		}
		
		public string ID {
			get {
				return "BB7AAEB7-626A-4bcd-BED5-EC3DEB201F32";
			}
		}
    }
}