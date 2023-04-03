using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DCC RID: 3532
	[PacketId(ClientPacketId.kNKMPacket_PVP_CASTING_VOTE_OPERATOR_ACK)]
	public sealed class NKMPacket_PVP_CASTING_VOTE_OPERATOR_ACK : ISerializable
	{
		// Token: 0x06009691 RID: 38545 RVA: 0x0032BAE4 File Offset: 0x00329CE4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<PvpCastingVoteData>(ref this.pvpCastingVoteData);
		}

		// Token: 0x0400887B RID: 34939
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400887C RID: 34940
		public PvpCastingVoteData pvpCastingVoteData = new PvpCastingVoteData();
	}
}
