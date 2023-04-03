using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F42 RID: 3906
	[PacketId(ClientPacketId.kNKMPacket_GAME_AUTO_RESPAWN_ACK)]
	public sealed class NKMPacket_GAME_AUTO_RESPAWN_ACK : ISerializable
	{
		// Token: 0x06009964 RID: 39268 RVA: 0x0032FFC7 File Offset: 0x0032E1C7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isAutoRespawn);
		}

		// Token: 0x04008C6A RID: 35946
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C6B RID: 35947
		public bool isAutoRespawn = true;
	}
}
