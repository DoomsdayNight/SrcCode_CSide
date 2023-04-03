using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC4 RID: 3268
	[PacketId(ClientPacketId.kNKMPacket_RANDOM_MISSION_CHANGE_REQ)]
	public sealed class NKMPacket_RANDOM_MISSION_CHANGE_REQ : ISerializable
	{
		// Token: 0x06009485 RID: 38021 RVA: 0x00328D6F File Offset: 0x00326F6F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tabId);
			stream.PutOrGet(ref this.missionId);
		}

		// Token: 0x040085F7 RID: 34295
		public int tabId;

		// Token: 0x040085F8 RID: 34296
		public int missionId;
	}
}
