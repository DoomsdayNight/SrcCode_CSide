using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD1 RID: 4049
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_RECOMMEND_ACK)]
	public sealed class NKMPacket_FRIEND_RECOMMEND_ACK : ISerializable
	{
		// Token: 0x06009A72 RID: 39538 RVA: 0x00331778 File Offset: 0x0032F978
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.list);
		}

		// Token: 0x04008DCE RID: 36302
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DCF RID: 36303
		public List<FriendListData> list = new List<FriendListData>();
	}
}
