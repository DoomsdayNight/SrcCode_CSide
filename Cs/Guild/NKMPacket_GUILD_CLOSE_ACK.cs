using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED2 RID: 3794
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CLOSE_ACK)]
	public sealed class NKMPacket_GUILD_CLOSE_ACK : ISerializable
	{
		// Token: 0x06009884 RID: 39044 RVA: 0x0032EA73 File Offset: 0x0032CC73
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.closingTime);
		}

		// Token: 0x04008B29 RID: 35625
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B2A RID: 35626
		public DateTime closingTime;
	}
}
