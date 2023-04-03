using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC4 RID: 3524
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_TARGET_SEARCH_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_TARGET_SEARCH_ACK : ISerializable
	{
		// Token: 0x06009681 RID: 38529 RVA: 0x0032BA07 File Offset: 0x00329C07
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.list);
		}

		// Token: 0x04008872 RID: 34930
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008873 RID: 34931
		public List<FriendListData> list = new List<FriendListData>();
	}
}
