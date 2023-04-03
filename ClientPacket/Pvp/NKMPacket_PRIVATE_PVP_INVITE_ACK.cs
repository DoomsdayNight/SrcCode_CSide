using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D8F RID: 3471
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_INVITE_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_INVITE_ACK : ISerializable
	{
		// Token: 0x06009619 RID: 38425 RVA: 0x0032B3B6 File Offset: 0x003295B6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x0400881F RID: 34847
		public NKM_ERROR_CODE errorCode;
	}
}
