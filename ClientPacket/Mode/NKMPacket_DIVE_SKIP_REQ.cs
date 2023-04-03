using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E66 RID: 3686
	[PacketId(ClientPacketId.kNKMPacket_DIVE_SKIP_REQ)]
	public sealed class NKMPacket_DIVE_SKIP_REQ : ISerializable
	{
		// Token: 0x060097BC RID: 38844 RVA: 0x0032D5D5 File Offset: 0x0032B7D5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
			stream.PutOrGet(ref this.skipCount);
		}

		// Token: 0x040089E8 RID: 35304
		public int stageId;

		// Token: 0x040089E9 RID: 35305
		public int skipCount;
	}
}
