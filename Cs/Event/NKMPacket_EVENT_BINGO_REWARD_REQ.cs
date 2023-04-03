using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F77 RID: 3959
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BINGO_REWARD_REQ)]
	public sealed class NKMPacket_EVENT_BINGO_REWARD_REQ : ISerializable
	{
		// Token: 0x060099CA RID: 39370 RVA: 0x0033086D File Offset: 0x0032EA6D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGet(ref this.rewardIndex);
		}

		// Token: 0x04008CE5 RID: 36069
		public int eventId;

		// Token: 0x04008CE6 RID: 36070
		public int rewardIndex;
	}
}
