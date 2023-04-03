using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F47 RID: 3911
	[PacketId(ClientPacketId.kNKMPacket_GAME_SPEED_2X_ACK)]
	public sealed class NKMPacket_GAME_SPEED_2X_ACK : ISerializable
	{
		// Token: 0x0600996E RID: 39278 RVA: 0x00330054 File Offset: 0x0032E254
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NKM_GAME_SPEED_TYPE>(ref this.gameSpeedType);
		}

		// Token: 0x04008C71 RID: 35953
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C72 RID: 35954
		public NKM_GAME_SPEED_TYPE gameSpeedType;
	}
}
