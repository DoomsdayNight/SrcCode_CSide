using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D72 RID: 3442
	[PacketId(ClientPacketId.kNKMPacket_PVP_GAME_MATCH_ACK)]
	public sealed class NKMPacket_PVP_GAME_MATCH_ACK : ISerializable
	{
		// Token: 0x060095DF RID: 38367 RVA: 0x0032AF1A File Offset: 0x0032911A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x040087DE RID: 34782
		public NKM_ERROR_CODE errorCode;
	}
}
