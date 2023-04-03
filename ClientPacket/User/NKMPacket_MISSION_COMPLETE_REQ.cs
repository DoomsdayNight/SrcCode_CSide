using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC0 RID: 3264
	[PacketId(ClientPacketId.kNKMPacket_MISSION_COMPLETE_REQ)]
	public sealed class NKMPacket_MISSION_COMPLETE_REQ : ISerializable
	{
		// Token: 0x0600947D RID: 38013 RVA: 0x00328C96 File Offset: 0x00326E96
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tabId);
			stream.PutOrGet(ref this.groupId);
			stream.PutOrGet(ref this.missionID);
		}

		// Token: 0x040085EB RID: 34283
		public int tabId;

		// Token: 0x040085EC RID: 34284
		public int groupId;

		// Token: 0x040085ED RID: 34285
		public int missionID;
	}
}
