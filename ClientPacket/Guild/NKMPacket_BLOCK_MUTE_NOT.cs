using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F15 RID: 3861
	[PacketId(ClientPacketId.kNKMPacket_BLOCK_MUTE_NOT)]
	public sealed class NKMPacket_BLOCK_MUTE_NOT : ISerializable
	{
		// Token: 0x0600990A RID: 39178 RVA: 0x0032F60F File Offset: 0x0032D80F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet(ref this.endDate);
		}

		// Token: 0x04008BD5 RID: 35797
		public long userUid;

		// Token: 0x04008BD6 RID: 35798
		public DateTime endDate;
	}
}
