using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA5 RID: 3493
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_RANK_LIST_ACK)]
	public sealed class NKMPacket_LEAGUE_PVP_RANK_LIST_ACK : ISerializable
	{
		// Token: 0x06009643 RID: 38467 RVA: 0x0032B690 File Offset: 0x00329890
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<RANK_TYPE>(ref this.rankType);
			stream.PutOrGet<NKMUserSimpleProfileData>(ref this.list);
		}

		// Token: 0x0400884C RID: 34892
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400884D RID: 34893
		public RANK_TYPE rankType;

		// Token: 0x0400884E RID: 34894
		public List<NKMUserSimpleProfileData> list = new List<NKMUserSimpleProfileData>();
	}
}
