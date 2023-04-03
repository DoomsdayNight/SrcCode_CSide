using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F3C RID: 3900
	[PacketId(ClientPacketId.kNKMPacket_GAME_CHECK_DIE_UNIT_ACK)]
	public sealed class NKMPacket_GAME_CHECK_DIE_UNIT_ACK : ISerializable
	{
		// Token: 0x06009958 RID: 39256 RVA: 0x0032FECB File Offset: 0x0032E0CB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008C5A RID: 35930
		public NKM_ERROR_CODE errorCode;
	}
}
