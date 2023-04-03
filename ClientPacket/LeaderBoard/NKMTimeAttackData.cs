using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E73 RID: 3699
	public sealed class NKMTimeAttackData : ISerializable
	{
		// Token: 0x060097D4 RID: 38868 RVA: 0x0032D85A File Offset: 0x0032BA5A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.bestTime);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
		}

		// Token: 0x04008A09 RID: 35337
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008A0A RID: 35338
		public int bestTime;

		// Token: 0x04008A0B RID: 35339
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();
	}
}
