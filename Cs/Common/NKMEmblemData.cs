using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200102D RID: 4141
	public sealed class NKMEmblemData : ISerializable
	{
		// Token: 0x06009B2A RID: 39722 RVA: 0x00332478 File Offset: 0x00330678
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.id);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008E7A RID: 36474
		public int id;

		// Token: 0x04008E7B RID: 36475
		public long count;
	}
}
