using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD2 RID: 3538
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_INVITE_REQ)]
	public sealed class NKMPacket_PVP_ROOM_INVITE_REQ : ISerializable
	{
		// Token: 0x0600969B RID: 38555 RVA: 0x0032BB8B File Offset: 0x00329D8B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGetEnum<PvpPlayerRole>(ref this.preferRole);
		}

		// Token: 0x04008885 RID: 34949
		public long friendCode;

		// Token: 0x04008886 RID: 34950
		public PvpPlayerRole preferRole;
	}
}
