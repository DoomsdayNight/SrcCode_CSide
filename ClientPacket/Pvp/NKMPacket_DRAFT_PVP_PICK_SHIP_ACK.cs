using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DAF RID: 3503
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_PICK_SHIP_ACK)]
	public sealed class NKMPacket_DRAFT_PVP_PICK_SHIP_ACK : ISerializable
	{
		// Token: 0x06009657 RID: 38487 RVA: 0x0032B7A5 File Offset: 0x003299A5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008858 RID: 34904
		public NKM_ERROR_CODE errorCode;
	}
}
