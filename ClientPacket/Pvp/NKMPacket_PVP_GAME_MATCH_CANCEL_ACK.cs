using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D74 RID: 3444
	[PacketId(ClientPacketId.kNKMPacket_PVP_GAME_MATCH_CANCEL_ACK)]
	public sealed class NKMPacket_PVP_GAME_MATCH_CANCEL_ACK : ISerializable
	{
		// Token: 0x060095E3 RID: 38371 RVA: 0x0032AF3A File Offset: 0x0032913A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x040087DF RID: 34783
		public NKM_ERROR_CODE errorCode;
	}
}
