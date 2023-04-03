using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DBD RID: 3517
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_GIVEUP_ACK)]
	public sealed class NKMPacket_LEAGUE_PVP_GIVEUP_ACK : ISerializable
	{
		// Token: 0x06009673 RID: 38515 RVA: 0x0032B8F0 File Offset: 0x00329AF0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008867 RID: 34919
		public NKM_ERROR_CODE errorCode;
	}
}
