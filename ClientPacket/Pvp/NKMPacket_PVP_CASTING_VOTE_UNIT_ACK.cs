using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC0 RID: 3520
	[PacketId(ClientPacketId.kNKMPacket_PVP_CASTING_VOTE_UNIT_ACK)]
	public sealed class NKMPacket_PVP_CASTING_VOTE_UNIT_ACK : ISerializable
	{
		// Token: 0x06009679 RID: 38521 RVA: 0x0032B976 File Offset: 0x00329B76
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<PvpCastingVoteData>(ref this.pvpCastingVoteData);
		}

		// Token: 0x0400886C RID: 34924
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400886D RID: 34925
		public PvpCastingVoteData pvpCastingVoteData = new PvpCastingVoteData();
	}
}
