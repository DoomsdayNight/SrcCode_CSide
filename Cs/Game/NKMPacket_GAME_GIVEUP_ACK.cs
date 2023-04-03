using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F45 RID: 3909
	[PacketId(ClientPacketId.kNKMPacket_GAME_GIVEUP_ACK)]
	public sealed class NKMPacket_GAME_GIVEUP_ACK : ISerializable
	{
		// Token: 0x0600996A RID: 39274 RVA: 0x00330028 File Offset: 0x0032E228
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008C6F RID: 35951
		public NKM_ERROR_CODE errorCode;
	}
}
