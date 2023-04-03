using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC6 RID: 3526
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_EXIT_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_EXIT_ACK : ISerializable
	{
		// Token: 0x06009685 RID: 38533 RVA: 0x0032BA3E File Offset: 0x00329C3E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008874 RID: 34932
		public NKM_ERROR_CODE errorCode;
	}
}
