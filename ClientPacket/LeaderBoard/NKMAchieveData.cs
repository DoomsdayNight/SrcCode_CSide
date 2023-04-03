using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E6B RID: 3691
	public sealed class NKMAchieveData : ISerializable
	{
		// Token: 0x060097C4 RID: 38852 RVA: 0x0032D6A1 File Offset: 0x0032B8A1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.achievePoint);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
		}

		// Token: 0x040089F5 RID: 35317
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x040089F6 RID: 35318
		public long achievePoint;

		// Token: 0x040089F7 RID: 35319
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();
	}
}
