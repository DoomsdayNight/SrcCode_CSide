using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F73 RID: 3955
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BINGO_RANDOM_MARK_REQ)]
	public sealed class NKMPacket_EVENT_BINGO_RANDOM_MARK_REQ : ISerializable
	{
		// Token: 0x060099C2 RID: 39362 RVA: 0x003307B5 File Offset: 0x0032E9B5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventId);
		}

		// Token: 0x04008CD9 RID: 36057
		public int eventId;
	}
}
