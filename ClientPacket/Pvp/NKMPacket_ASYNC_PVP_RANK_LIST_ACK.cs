using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D85 RID: 3461
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_RANK_LIST_ACK)]
	public sealed class NKMPacket_ASYNC_PVP_RANK_LIST_ACK : ISerializable
	{
		// Token: 0x06009605 RID: 38405 RVA: 0x0032B1A7 File Offset: 0x003293A7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<RANK_TYPE>(ref this.rankType);
			stream.PutOrGet(ref this.isAll);
			stream.PutOrGet<NKMUserSimpleProfileData>(ref this.userProfileDataList);
		}

		// Token: 0x04008800 RID: 34816
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008801 RID: 34817
		public RANK_TYPE rankType;

		// Token: 0x04008802 RID: 34818
		public bool isAll;

		// Token: 0x04008803 RID: 34819
		public List<NKMUserSimpleProfileData> userProfileDataList = new List<NKMUserSimpleProfileData>();
	}
}
