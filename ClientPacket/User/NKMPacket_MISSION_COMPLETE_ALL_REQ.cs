using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC2 RID: 3266
	[PacketId(ClientPacketId.kNKMPacket_MISSION_COMPLETE_ALL_REQ)]
	public sealed class NKMPacket_MISSION_COMPLETE_ALL_REQ : ISerializable
	{
		// Token: 0x06009481 RID: 38017 RVA: 0x00328D09 File Offset: 0x00326F09
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tabId);
		}

		// Token: 0x040085F2 RID: 34290
		public int tabId;
	}
}
