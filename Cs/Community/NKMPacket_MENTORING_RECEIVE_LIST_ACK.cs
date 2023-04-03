using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001018 RID: 4120
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_RECEIVE_LIST_ACK)]
	public sealed class NKMPacket_MENTORING_RECEIVE_LIST_ACK : ISerializable
	{
		// Token: 0x06009B00 RID: 39680 RVA: 0x003321D9 File Offset: 0x003303D9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.invitedList);
			stream.PutOrGet<FriendListData>(ref this.recommendList);
		}

		// Token: 0x04008E5A RID: 36442
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E5B RID: 36443
		public List<FriendListData> invitedList = new List<FriendListData>();

		// Token: 0x04008E5C RID: 36444
		public List<FriendListData> recommendList = new List<FriendListData>();
	}
}
