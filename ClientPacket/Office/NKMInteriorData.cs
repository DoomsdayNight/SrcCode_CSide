using System;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DE6 RID: 3558
	public sealed class NKMInteriorData : ISerializable
	{
		// Token: 0x060096C0 RID: 38592 RVA: 0x0032BEB3 File Offset: 0x0032A0B3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.itemId);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x040088B5 RID: 34997
		public int itemId;

		// Token: 0x040088B6 RID: 34998
		public long count;
	}
}
