using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD3 RID: 3539
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_INVITE_ACK)]
	public sealed class NKMPacket_PVP_ROOM_INVITE_ACK : ISerializable
	{
		// Token: 0x0600969D RID: 38557 RVA: 0x0032BBAD File Offset: 0x00329DAD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008887 RID: 34951
		public NKM_ERROR_CODE errorCode;
	}
}
