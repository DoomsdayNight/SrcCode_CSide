using System;
using Cs.Protocol;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FB1 RID: 4017
	[PacketId(ClientPacketId.kNKMPacket_EVENT_POINT_NOT)]
	public sealed class NKMPacket_EVENT_POINT_NOT : ISerializable
	{
		// Token: 0x06009A36 RID: 39478 RVA: 0x003310F2 File Offset: 0x0032F2F2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.totalEventPoint);
			stream.PutOrGet<NKMRewardData>(ref this.additionalReward);
		}

		// Token: 0x04008D6F RID: 36207
		public long totalEventPoint;

		// Token: 0x04008D70 RID: 36208
		public NKMRewardData additionalReward;
	}
}
