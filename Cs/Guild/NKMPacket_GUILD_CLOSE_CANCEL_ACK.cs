using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED4 RID: 3796
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CLOSE_CANCEL_ACK)]
	public sealed class NKMPacket_GUILD_CLOSE_CANCEL_ACK : ISerializable
	{
		// Token: 0x06009888 RID: 39048 RVA: 0x0032EAAB File Offset: 0x0032CCAB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008B2C RID: 35628
		public NKM_ERROR_CODE errorCode;
	}
}
