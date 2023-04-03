using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D78 RID: 3448
	[PacketId(ClientPacketId.kNKMPacket_PVP_RANK_LIST_ACK)]
	public sealed class NKMPacket_PVP_RANK_LIST_ACK : ISerializable
	{
		// Token: 0x060095EB RID: 38379 RVA: 0x0032AF9E File Offset: 0x0032919E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<RANK_TYPE>(ref this.rankType);
			stream.PutOrGet<NKMUserSimpleProfileData>(ref this.userProfileDataList);
		}

		// Token: 0x040087E4 RID: 34788
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087E5 RID: 34789
		public RANK_TYPE rankType;

		// Token: 0x040087E6 RID: 34790
		public List<NKMUserSimpleProfileData> userProfileDataList = new List<NKMUserSimpleProfileData>();
	}
}
