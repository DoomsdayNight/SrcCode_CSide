using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA6 RID: 3494
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_ACCEPT_NOT)]
	public sealed class NKMPacket_LEAGUE_PVP_ACCEPT_NOT : ISerializable
	{
		// Token: 0x06009645 RID: 38469 RVA: 0x0032B6C9 File Offset: 0x003298C9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<DraftPvpRoomData>(ref this.roomData);
		}

		// Token: 0x0400884F RID: 34895
		public DraftPvpRoomData roomData = new DraftPvpRoomData();
	}
}
