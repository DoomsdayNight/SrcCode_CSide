using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD0 RID: 3536
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_CHANGE_ROLE_REQ)]
	public sealed class NKMPacket_PVP_ROOM_CHANGE_ROLE_REQ : ISerializable
	{
		// Token: 0x06009697 RID: 38551 RVA: 0x0032BB5F File Offset: 0x00329D5F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<PvpPlayerRole>(ref this.role);
		}

		// Token: 0x04008883 RID: 34947
		public PvpPlayerRole role;
	}
}
