using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F5C RID: 3932
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_PROFILE_ACK)]
	public sealed class NKMPacket_FIERCE_PROFILE_ACK : ISerializable
	{
		// Token: 0x06009998 RID: 39320 RVA: 0x00330379 File Offset: 0x0032E579
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
			stream.PutOrGet(ref this.friendIntro);
			stream.PutOrGet<NKMFierceProfileData>(ref this.profileData);
		}

		// Token: 0x04008C9E RID: 35998
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C9F RID: 35999
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008CA0 RID: 36000
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();

		// Token: 0x04008CA1 RID: 36001
		public string friendIntro;

		// Token: 0x04008CA2 RID: 36002
		public NKMFierceProfileData profileData = new NKMFierceProfileData();
	}
}
