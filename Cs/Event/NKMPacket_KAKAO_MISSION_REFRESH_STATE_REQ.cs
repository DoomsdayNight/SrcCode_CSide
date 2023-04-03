using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F96 RID: 3990
	[PacketId(ClientPacketId.kNKMPacket_KAKAO_MISSION_REFRESH_STATE_REQ)]
	public sealed class NKMPacket_KAKAO_MISSION_REFRESH_STATE_REQ : ISerializable
	{
		// Token: 0x06009A04 RID: 39428 RVA: 0x00330D32 File Offset: 0x0032EF32
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventId);
		}

		// Token: 0x04008D33 RID: 36147
		public int eventId;
	}
}
