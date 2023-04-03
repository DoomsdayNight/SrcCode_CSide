using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200101A RID: 4122
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_SEARCH_LIST_ACK)]
	public sealed class NKMPacket_MENTORING_SEARCH_LIST_ACK : ISerializable
	{
		// Token: 0x06009B04 RID: 39684 RVA: 0x0033223F File Offset: 0x0033043F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.searchList);
		}

		// Token: 0x04008E5F RID: 36447
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E60 RID: 36448
		public List<FriendListData> searchList = new List<FriendListData>();
	}
}
