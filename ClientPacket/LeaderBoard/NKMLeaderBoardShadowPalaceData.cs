using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E6E RID: 3694
	public sealed class NKMLeaderBoardShadowPalaceData : ISerializable
	{
		// Token: 0x060097CA RID: 38858 RVA: 0x0032D74A File Offset: 0x0032B94A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMShadowPalaceData>(ref this.shadowPalaceData);
		}

		// Token: 0x040089FC RID: 35324
		public List<NKMShadowPalaceData> shadowPalaceData = new List<NKMShadowPalaceData>();
	}
}
