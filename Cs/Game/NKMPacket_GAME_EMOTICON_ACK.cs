using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F51 RID: 3921
	[PacketId(ClientPacketId.kNKMPacket_GAME_EMOTICON_ACK)]
	public sealed class NKMPacket_GAME_EMOTICON_ACK : ISerializable
	{
		// Token: 0x06009982 RID: 39298 RVA: 0x003301EE File Offset: 0x0032E3EE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008C89 RID: 35977
		public NKM_ERROR_CODE errorCode;
	}
}
