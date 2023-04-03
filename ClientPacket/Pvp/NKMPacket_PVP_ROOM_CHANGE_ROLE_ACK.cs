using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD1 RID: 3537
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_CHANGE_ROLE_ACK)]
	public sealed class NKMPacket_PVP_ROOM_CHANGE_ROLE_ACK : ISerializable
	{
		// Token: 0x06009699 RID: 38553 RVA: 0x0032BB75 File Offset: 0x00329D75
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008884 RID: 34948
		public NKM_ERROR_CODE errorCode;
	}
}
