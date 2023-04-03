using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F0B RID: 3851
	[PacketId(ClientPacketId.kNKMPacket_GUILD_RECOMMEND_INVITE_LIST_ACK)]
	public sealed class NKMPacket_GUILD_RECOMMEND_INVITE_LIST_ACK : ISerializable
	{
		// Token: 0x060098F6 RID: 39158 RVA: 0x0032F3DA File Offset: 0x0032D5DA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.list);
		}

		// Token: 0x04008BB5 RID: 35765
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BB6 RID: 35766
		public List<FriendListData> list = new List<FriendListData>();
	}
}
