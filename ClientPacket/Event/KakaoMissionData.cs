using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F95 RID: 3989
	public sealed class KakaoMissionData : ISerializable
	{
		// Token: 0x06009A02 RID: 39426 RVA: 0x00330D10 File Offset: 0x0032EF10
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGetEnum<KakaoMissionState>(ref this.state);
		}

		// Token: 0x04008D31 RID: 36145
		public int eventId;

		// Token: 0x04008D32 RID: 36146
		public KakaoMissionState state;
	}
}
