using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FCF RID: 4047
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_LIST_ACK)]
	public sealed class NKMPacket_FRIEND_LIST_ACK : ISerializable
	{
		// Token: 0x06009A6E RID: 39534 RVA: 0x00331735 File Offset: 0x0032F935
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NKM_FRIEND_LIST_TYPE>(ref this.friendListType);
			stream.PutOrGet<FriendListData>(ref this.list);
		}

		// Token: 0x04008DCB RID: 36299
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DCC RID: 36300
		public NKM_FRIEND_LIST_TYPE friendListType;

		// Token: 0x04008DCD RID: 36301
		public List<FriendListData> list = new List<FriendListData>();
	}
}
