using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FB3 RID: 4019
	[PacketId(ClientPacketId.kNKMPacket_EVENT_COLLECTION_MERGE_REQ)]
	public sealed class NKMPacket_EVENT_COLLECTION_MERGE_REQ : ISerializable
	{
		// Token: 0x06009A3A RID: 39482 RVA: 0x00331135 File Offset: 0x0032F335
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.collectionMergeId);
			stream.PutOrGet(ref this.mergeRecipeGroupId);
			stream.PutOrGet(ref this.consumeTrophyUids);
		}

		// Token: 0x04008D72 RID: 36210
		public int collectionMergeId;

		// Token: 0x04008D73 RID: 36211
		public int mergeRecipeGroupId;

		// Token: 0x04008D74 RID: 36212
		public List<long> consumeTrophyUids = new List<long>();
	}
}
