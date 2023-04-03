using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FCE RID: 4046
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_LIST_REQ)]
	public sealed class NKMPacket_FRIEND_LIST_REQ : ISerializable
	{
		// Token: 0x06009A6C RID: 39532 RVA: 0x0033171F File Offset: 0x0032F91F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_FRIEND_LIST_TYPE>(ref this.friendListType);
		}

		// Token: 0x04008DCA RID: 36298
		public NKM_FRIEND_LIST_TYPE friendListType;
	}
}
