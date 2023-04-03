using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB2 RID: 3506
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_PICK_LEADER_REQ)]
	public sealed class NKMPacket_DRAFT_PVP_PICK_LEADER_REQ : ISerializable
	{
		// Token: 0x0600965D RID: 38493 RVA: 0x0032B7E7 File Offset: 0x003299E7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.leaderIndex);
		}

		// Token: 0x0400885B RID: 34907
		public int leaderIndex;
	}
}
