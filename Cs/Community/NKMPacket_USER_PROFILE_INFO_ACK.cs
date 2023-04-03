using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FEC RID: 4076
	[PacketId(ClientPacketId.kNKMPacket_USER_PROFILE_INFO_ACK)]
	public sealed class NKMPacket_USER_PROFILE_INFO_ACK : ISerializable
	{
		// Token: 0x06009AA8 RID: 39592 RVA: 0x00331B22 File Offset: 0x0032FD22
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUserProfileData>(ref this.userProfileData);
		}

		// Token: 0x04008E02 RID: 36354
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E03 RID: 36355
		public NKMUserProfileData userProfileData = new NKMUserProfileData();
	}
}
