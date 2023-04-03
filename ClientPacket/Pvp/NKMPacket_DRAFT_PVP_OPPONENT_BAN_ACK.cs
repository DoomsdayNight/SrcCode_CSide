using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DAD RID: 3501
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_OPPONENT_BAN_ACK)]
	public sealed class NKMPacket_DRAFT_PVP_OPPONENT_BAN_ACK : ISerializable
	{
		// Token: 0x06009653 RID: 38483 RVA: 0x0032B779 File Offset: 0x00329979
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008856 RID: 34902
		public NKM_ERROR_CODE errorCode;
	}
}
