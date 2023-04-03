using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F54 RID: 3924
	[PacketId(ClientPacketId.kNKMPacket_GAME_UNIT_RETREAT_ACK)]
	public sealed class NKMPacket_GAME_UNIT_RETREAT_ACK : ISerializable
	{
		// Token: 0x06009988 RID: 39304 RVA: 0x0033023C File Offset: 0x0032E43C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
		}

		// Token: 0x04008C8D RID: 35981
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C8E RID: 35982
		public long unitUID;
	}
}
