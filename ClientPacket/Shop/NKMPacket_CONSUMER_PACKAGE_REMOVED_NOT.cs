using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D39 RID: 3385
	[PacketId(ClientPacketId.kNKMPacket_CONSUMER_PACKAGE_REMOVED_NOT)]
	public sealed class NKMPacket_CONSUMER_PACKAGE_REMOVED_NOT : ISerializable
	{
		// Token: 0x0600956F RID: 38255 RVA: 0x0032A2EC File Offset: 0x003284EC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.productIds);
		}

		// Token: 0x04008725 RID: 34597
		public List<int> productIds = new List<int>();
	}
}
