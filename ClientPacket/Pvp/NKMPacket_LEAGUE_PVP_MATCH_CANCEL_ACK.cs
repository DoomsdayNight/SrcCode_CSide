using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA2 RID: 3490
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_MATCH_CANCEL_ACK)]
	public sealed class NKMPacket_LEAGUE_PVP_MATCH_CANCEL_ACK : ISerializable
	{
		// Token: 0x0600963D RID: 38461 RVA: 0x0032B642 File Offset: 0x00329842
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008848 RID: 34888
		public NKM_ERROR_CODE errorCode;
	}
}
