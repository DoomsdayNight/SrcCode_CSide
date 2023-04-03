using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA7 RID: 3495
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_UPDATED_NOT)]
	public sealed class NKMPacket_LEAGUE_PVP_UPDATED_NOT : ISerializable
	{
		// Token: 0x06009647 RID: 38471 RVA: 0x0032B6EA File Offset: 0x003298EA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<DraftPvpRoomData>(ref this.roomData);
		}

		// Token: 0x04008850 RID: 34896
		public DraftPvpRoomData roomData = new DraftPvpRoomData();
	}
}
