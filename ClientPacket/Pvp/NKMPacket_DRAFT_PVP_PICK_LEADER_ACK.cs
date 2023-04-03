using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB3 RID: 3507
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_PICK_LEADER_ACK)]
	public sealed class NKMPacket_DRAFT_PVP_PICK_LEADER_ACK : ISerializable
	{
		// Token: 0x0600965F RID: 38495 RVA: 0x0032B7FD File Offset: 0x003299FD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x0400885C RID: 34908
		public NKM_ERROR_CODE errorCode;
	}
}
