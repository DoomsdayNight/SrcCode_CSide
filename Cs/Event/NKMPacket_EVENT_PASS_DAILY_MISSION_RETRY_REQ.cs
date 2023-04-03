using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F82 RID: 3970
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ)]
	public sealed class NKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ : ISerializable
	{
		// Token: 0x060099E0 RID: 39392 RVA: 0x00330A13 File Offset: 0x0032EC13
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.missionId);
		}

		// Token: 0x04008CFF RID: 36095
		public int missionId;
	}
}
