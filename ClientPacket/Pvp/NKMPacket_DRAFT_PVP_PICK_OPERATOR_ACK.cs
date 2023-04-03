using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB1 RID: 3505
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_PICK_OPERATOR_ACK)]
	public sealed class NKMPacket_DRAFT_PVP_PICK_OPERATOR_ACK : ISerializable
	{
		// Token: 0x0600965B RID: 38491 RVA: 0x0032B7D1 File Offset: 0x003299D1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x0400885A RID: 34906
		public NKM_ERROR_CODE errorCode;
	}
}
