using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA7 RID: 4007
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BAR_GET_REWARD_REQ)]
	public sealed class NKMPacket_EVENT_BAR_GET_REWARD_REQ : ISerializable
	{
		// Token: 0x06009A22 RID: 39458 RVA: 0x00330F66 File Offset: 0x0032F166
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cocktailItemId);
		}

		// Token: 0x04008D5A RID: 36186
		public int cocktailItemId;
	}
}
