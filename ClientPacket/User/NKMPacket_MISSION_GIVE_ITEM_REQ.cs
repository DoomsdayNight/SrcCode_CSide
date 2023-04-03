using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CDC RID: 3292
	[PacketId(ClientPacketId.kNKMPacket_MISSION_GIVE_ITEM_REQ)]
	public sealed class NKMPacket_MISSION_GIVE_ITEM_REQ : ISerializable
	{
		// Token: 0x060094B5 RID: 38069 RVA: 0x0032919E File Offset: 0x0032739E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.missionId);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008632 RID: 34354
		public int missionId;

		// Token: 0x04008633 RID: 34355
		public int count;
	}
}
