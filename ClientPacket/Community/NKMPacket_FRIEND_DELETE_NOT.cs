using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD9 RID: 4057
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_DELETE_NOT)]
	public sealed class NKMPacket_FRIEND_DELETE_NOT : ISerializable
	{
		// Token: 0x06009A82 RID: 39554 RVA: 0x00331879 File Offset: 0x0032FA79
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008DDA RID: 36314
		public long userUid;

		// Token: 0x04008DDB RID: 36315
		public long friendCode;
	}
}
