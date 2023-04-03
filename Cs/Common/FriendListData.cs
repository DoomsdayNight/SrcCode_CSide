using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200103B RID: 4155
	public sealed class FriendListData : ISerializable
	{
		// Token: 0x06009B38 RID: 39736 RVA: 0x00332661 File Offset: 0x00330861
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.lastLoginDate);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
			stream.PutOrGet(ref this.hasOffice);
		}

		// Token: 0x04008EB2 RID: 36530
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008EB3 RID: 36531
		public DateTime lastLoginDate;

		// Token: 0x04008EB4 RID: 36532
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();

		// Token: 0x04008EB5 RID: 36533
		public bool hasOffice;
	}
}
