using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA0 RID: 3488
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_MATCH_ACK)]
	public sealed class NKMPacket_LEAGUE_PVP_MATCH_ACK : ISerializable
	{
		// Token: 0x06009639 RID: 38457 RVA: 0x0032B622 File Offset: 0x00329822
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008847 RID: 34887
		public NKM_ERROR_CODE errorCode;
	}
}
