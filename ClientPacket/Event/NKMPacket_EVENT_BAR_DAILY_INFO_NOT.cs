using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA4 RID: 4004
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BAR_DAILY_INFO_NOT)]
	public sealed class NKMPacket_EVENT_BAR_DAILY_INFO_NOT : ISerializable
	{
		// Token: 0x06009A1C RID: 39452 RVA: 0x00330EE9 File Offset: 0x0032F0E9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.dailyCocktailItemId);
			stream.PutOrGet(ref this.remainDeliveryLimitValue);
		}

		// Token: 0x04008D53 RID: 36179
		public int dailyCocktailItemId;

		// Token: 0x04008D54 RID: 36180
		public int remainDeliveryLimitValue;
	}
}
