using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE1 RID: 4065
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_ACCEPT_NOT)]
	public sealed class NKMPacket_FRIEND_ACCEPT_NOT : ISerializable
	{
		// Token: 0x06009A92 RID: 39570 RVA: 0x00331995 File Offset: 0x0032FB95
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isAllow);
			stream.PutOrGet<FriendListData>(ref this.friendProfileData);
			stream.PutOrGet(ref this.regDate);
		}

		// Token: 0x04008DEB RID: 36331
		public bool isAllow;

		// Token: 0x04008DEC RID: 36332
		public FriendListData friendProfileData = new FriendListData();

		// Token: 0x04008DED RID: 36333
		public DateTime regDate;
	}
}
