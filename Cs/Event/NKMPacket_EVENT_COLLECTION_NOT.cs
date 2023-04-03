using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FB2 RID: 4018
	[PacketId(ClientPacketId.kNKMPacket_EVENT_COLLECTION_NOT)]
	public sealed class NKMPacket_EVENT_COLLECTION_NOT : ISerializable
	{
		// Token: 0x06009A38 RID: 39480 RVA: 0x00331114 File Offset: 0x0032F314
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMEventCollectionInfo>(ref this.eventCollectionInfo);
		}

		// Token: 0x04008D71 RID: 36209
		public NKMEventCollectionInfo eventCollectionInfo = new NKMEventCollectionInfo();
	}
}
