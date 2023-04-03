using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F6F RID: 3951
	public sealed class EventInfo : ISerializable
	{
		// Token: 0x060099BA RID: 39354 RVA: 0x00330717 File Offset: 0x0032E917
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<BingoInfo>(ref this.bingoInfo);
		}

		// Token: 0x04008CD1 RID: 36049
		public List<BingoInfo> bingoInfo = new List<BingoInfo>();
	}
}
