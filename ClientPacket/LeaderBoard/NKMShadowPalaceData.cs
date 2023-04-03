using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E6D RID: 3693
	public sealed class NKMShadowPalaceData : ISerializable
	{
		// Token: 0x060097C8 RID: 38856 RVA: 0x0032D706 File Offset: 0x0032B906
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.bestTime);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
		}

		// Token: 0x040089F9 RID: 35321
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x040089FA RID: 35322
		public int bestTime;

		// Token: 0x040089FB RID: 35323
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();
	}
}
