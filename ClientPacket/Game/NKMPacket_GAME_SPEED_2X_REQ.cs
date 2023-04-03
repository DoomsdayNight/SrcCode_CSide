using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F46 RID: 3910
	[PacketId(ClientPacketId.kNKMPacket_GAME_SPEED_2X_REQ)]
	public sealed class NKMPacket_GAME_SPEED_2X_REQ : ISerializable
	{
		// Token: 0x0600996C RID: 39276 RVA: 0x0033003E File Offset: 0x0032E23E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_GAME_SPEED_TYPE>(ref this.gameSpeedType);
		}

		// Token: 0x04008C70 RID: 35952
		public NKM_GAME_SPEED_TYPE gameSpeedType;
	}
}
