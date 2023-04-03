using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA5 RID: 4005
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BAR_CREATE_COCKTAIL_REQ)]
	public sealed class NKMPacket_EVENT_BAR_CREATE_COCKTAIL_REQ : ISerializable
	{
		// Token: 0x06009A1E RID: 39454 RVA: 0x00330F0B File Offset: 0x0032F10B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cocktailItemId);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008D55 RID: 36181
		public int cocktailItemId;

		// Token: 0x04008D56 RID: 36182
		public int count;
	}
}
