using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB5 RID: 3509
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_SELECT_UNIT_ACK)]
	public sealed class NKMPacket_DRAFT_PVP_SELECT_UNIT_ACK : ISerializable
	{
		// Token: 0x06009663 RID: 38499 RVA: 0x0032B829 File Offset: 0x00329A29
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x0400885E RID: 34910
		public NKM_ERROR_CODE errorCode;
	}
}
