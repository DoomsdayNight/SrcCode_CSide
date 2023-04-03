using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E07 RID: 3591
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_POST_LIST_REQ)]
	public sealed class NKMPacket_OFFICE_POST_LIST_REQ : ISerializable
	{
		// Token: 0x06009702 RID: 38658 RVA: 0x0032C67E File Offset: 0x0032A87E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.lastPostUid);
		}

		// Token: 0x04008919 RID: 35097
		public long lastPostUid;
	}
}
