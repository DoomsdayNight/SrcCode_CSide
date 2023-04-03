using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FEB RID: 4075
	[PacketId(ClientPacketId.kNKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ)]
	public sealed class NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ : ISerializable
	{
		// Token: 0x06009AA6 RID: 39590 RVA: 0x00331B0C File Offset: 0x0032FD0C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008E01 RID: 36353
		public long friendCode;
	}
}
