using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FAB RID: 4011
	[PacketId(ClientPacketId.kNKMPacket_AD_ITEM_REWARD_REQ)]
	public sealed class NKMPacket_AD_ITEM_REWARD_REQ : ISerializable
	{
		// Token: 0x06009A2A RID: 39466 RVA: 0x00331033 File Offset: 0x0032F233
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.aditemId);
		}

		// Token: 0x04008D65 RID: 36197
		public int aditemId;
	}
}
