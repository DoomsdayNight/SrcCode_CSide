using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E13 RID: 3603
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_CHAT_REQ)]
	public sealed class NKMPacket_OFFICE_CHAT_REQ : ISerializable
	{
		// Token: 0x0600971A RID: 38682 RVA: 0x0032C856 File Offset: 0x0032AA56
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet(ref this.emotionId);
		}

		// Token: 0x0400892F RID: 35119
		public long userUid;

		// Token: 0x04008930 RID: 35120
		public int emotionId;
	}
}
