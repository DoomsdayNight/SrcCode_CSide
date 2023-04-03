using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E6F RID: 3695
	public sealed class NKMFierceData : ISerializable
	{
		// Token: 0x060097CC RID: 38860 RVA: 0x0032D76B File Offset: 0x0032B96B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.fiercePoint);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
		}

		// Token: 0x040089FD RID: 35325
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x040089FE RID: 35326
		public long fiercePoint;

		// Token: 0x040089FF RID: 35327
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();
	}
}
