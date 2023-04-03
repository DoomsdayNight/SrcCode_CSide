using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DAB RID: 3499
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_PICK_UNIT_ACK)]
	public sealed class NKMPacket_DRAFT_PVP_PICK_UNIT_ACK : ISerializable
	{
		// Token: 0x0600964F RID: 38479 RVA: 0x0032B74D File Offset: 0x0032994D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008854 RID: 34900
		public NKM_ERROR_CODE errorCode;
	}
}
