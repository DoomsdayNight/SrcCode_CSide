using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E74 RID: 3700
	public sealed class NKMLeaderBoardTimeAttackData : ISerializable
	{
		// Token: 0x060097D6 RID: 38870 RVA: 0x0032D89E File Offset: 0x0032BA9E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMTimeAttackData>(ref this.timeAttackData);
		}

		// Token: 0x04008A0C RID: 35340
		public List<NKMTimeAttackData> timeAttackData = new List<NKMTimeAttackData>();
	}
}
