using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DCA RID: 3530
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_SEARCH_USER_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_SEARCH_USER_ACK : ISerializable
	{
		// Token: 0x0600968D RID: 38541 RVA: 0x0032BA96 File Offset: 0x00329C96
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.list);
		}

		// Token: 0x04008878 RID: 34936
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008879 RID: 34937
		public List<FriendListData> list = new List<FriendListData>();
	}
}
