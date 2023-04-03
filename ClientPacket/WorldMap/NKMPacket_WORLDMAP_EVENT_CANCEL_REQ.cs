using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C8D RID: 3213
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_EVENT_CANCEL_REQ)]
	public sealed class NKMPacket_WORLDMAP_EVENT_CANCEL_REQ : ISerializable
	{
		// Token: 0x06009419 RID: 37913 RVA: 0x003280A3 File Offset: 0x003262A3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
		}

		// Token: 0x04008530 RID: 34096
		public int cityID;
	}
}
