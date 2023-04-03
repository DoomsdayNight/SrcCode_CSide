using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD6 RID: 4054
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_REQUEST_NOT)]
	public sealed class NKMPacket_FRIEND_REQUEST_NOT : ISerializable
	{
		// Token: 0x06009A7C RID: 39548 RVA: 0x00331820 File Offset: 0x0032FA20
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<FriendListData>(ref this.friendProfileData);
		}

		// Token: 0x04008DD6 RID: 36310
		public FriendListData friendProfileData = new FriendListData();
	}
}
