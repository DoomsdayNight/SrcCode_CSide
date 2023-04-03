using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F88 RID: 3976
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_DOT_NOT)]
	public sealed class NKMPacket_EVENT_PASS_DOT_NOT : ISerializable
	{
		// Token: 0x060099EC RID: 39404 RVA: 0x00330AE7 File Offset: 0x0032ECE7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.passLevelDot);
			stream.PutOrGet(ref this.dailyMissionDot);
			stream.PutOrGet(ref this.weeklyMissionDot);
		}

		// Token: 0x04008D08 RID: 36104
		public bool passLevelDot;

		// Token: 0x04008D09 RID: 36105
		public bool dailyMissionDot;

		// Token: 0x04008D0A RID: 36106
		public bool weeklyMissionDot;
	}
}
