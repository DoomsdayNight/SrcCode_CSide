using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD6 RID: 3542
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_CANCEL_INVITE_ACK)]
	public sealed class NKMPacket_PVP_ROOM_CANCEL_INVITE_ACK : ISerializable
	{
		// Token: 0x060096A3 RID: 38563 RVA: 0x0032BC29 File Offset: 0x00329E29
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x0400888D RID: 34957
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400888E RID: 34958
		public long targetUserUid;
	}
}
