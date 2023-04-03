using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200103E RID: 4158
	public sealed class MiscItemData : ISerializable
	{
		// Token: 0x06009B3E RID: 39742 RVA: 0x003327B4 File Offset: 0x003309B4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.itemId);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008EC6 RID: 36550
		public int itemId;

		// Token: 0x04008EC7 RID: 36551
		public int count;
	}
}
