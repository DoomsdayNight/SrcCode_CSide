using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001002 RID: 4098
	[PacketId(ClientPacketId.kNKMPacket_MY_USER_PROFILE_INFO_ACK)]
	public sealed class NKMPacket_MY_USER_PROFILE_INFO_ACK : ISerializable
	{
		// Token: 0x06009AD4 RID: 39636 RVA: 0x00331F0B File Offset: 0x0033010B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUserProfileData>(ref this.userProfileData);
		}

		// Token: 0x04008E36 RID: 36406
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E37 RID: 36407
		public NKMUserProfileData userProfileData = new NKMUserProfileData();
	}
}
