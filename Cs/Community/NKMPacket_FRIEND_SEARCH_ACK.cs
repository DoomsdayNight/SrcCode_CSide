using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD3 RID: 4051
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_SEARCH_ACK)]
	public sealed class NKMPacket_FRIEND_SEARCH_ACK : ISerializable
	{
		// Token: 0x06009A76 RID: 39542 RVA: 0x003317BB File Offset: 0x0032F9BB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.list);
		}

		// Token: 0x04008DD1 RID: 36305
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DD2 RID: 36306
		public List<FriendListData> list = new List<FriendListData>();
	}
}
