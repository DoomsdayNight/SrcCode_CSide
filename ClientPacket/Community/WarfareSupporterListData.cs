using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Community
{
	// Token: 0x02000FCC RID: 4044
	public sealed class WarfareSupporterListData : ISerializable
	{
		// Token: 0x06009A68 RID: 39528 RVA: 0x00331674 File Offset: 0x0032F874
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet<NKMDummyDeckData>(ref this.deckData);
			stream.PutOrGet(ref this.lastLoginDate);
			stream.PutOrGet(ref this.lastUsedDate);
			stream.PutOrGet(ref this.message);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
		}

		// Token: 0x04008DC2 RID: 36290
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008DC3 RID: 36291
		public NKMDummyDeckData deckData;

		// Token: 0x04008DC4 RID: 36292
		public DateTime lastLoginDate;

		// Token: 0x04008DC5 RID: 36293
		public DateTime lastUsedDate;

		// Token: 0x04008DC6 RID: 36294
		public string message;

		// Token: 0x04008DC7 RID: 36295
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();
	}
}
