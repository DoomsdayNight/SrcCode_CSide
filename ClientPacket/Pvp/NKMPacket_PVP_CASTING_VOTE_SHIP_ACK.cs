using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC2 RID: 3522
	[PacketId(ClientPacketId.kNKMPacket_PVP_CASTING_VOTE_SHIP_ACK)]
	public sealed class NKMPacket_PVP_CASTING_VOTE_SHIP_ACK : ISerializable
	{
		// Token: 0x0600967D RID: 38525 RVA: 0x0032B9C4 File Offset: 0x00329BC4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<PvpCastingVoteData>(ref this.pvpCastingVoteData);
		}

		// Token: 0x0400886F RID: 34927
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008870 RID: 34928
		public PvpCastingVoteData pvpCastingVoteData = new PvpCastingVoteData();
	}
}
