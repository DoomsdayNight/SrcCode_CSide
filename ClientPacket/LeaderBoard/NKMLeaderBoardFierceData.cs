using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E70 RID: 3696
	public sealed class NKMLeaderBoardFierceData : ISerializable
	{
		// Token: 0x060097CE RID: 38862 RVA: 0x0032D7AF File Offset: 0x0032B9AF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMFierceData>(ref this.fierceData);
		}

		// Token: 0x04008A00 RID: 35328
		public List<NKMFierceData> fierceData = new List<NKMFierceData>();
	}
}
