using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F72 RID: 3954
	public sealed class NKMEventCollectionInfo : ISerializable
	{
		// Token: 0x060099C0 RID: 39360 RVA: 0x00330788 File Offset: 0x0032E988
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGet(ref this.goodsCollection);
		}

		// Token: 0x04008CD7 RID: 36055
		public int eventId;

		// Token: 0x04008CD8 RID: 36056
		public HashSet<int> goodsCollection = new HashSet<int>();
	}
}
