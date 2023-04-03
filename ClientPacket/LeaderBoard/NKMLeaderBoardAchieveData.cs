using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E6C RID: 3692
	public sealed class NKMLeaderBoardAchieveData : ISerializable
	{
		// Token: 0x060097C6 RID: 38854 RVA: 0x0032D6E5 File Offset: 0x0032B8E5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMAchieveData>(ref this.achieveData);
		}

		// Token: 0x040089F8 RID: 35320
		public List<NKMAchieveData> achieveData = new List<NKMAchieveData>();
	}
}
