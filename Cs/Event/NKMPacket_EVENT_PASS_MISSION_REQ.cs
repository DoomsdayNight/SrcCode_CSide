using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F7D RID: 3965
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_MISSION_REQ)]
	public sealed class NKMPacket_EVENT_PASS_MISSION_REQ : ISerializable
	{
		// Token: 0x060099D6 RID: 39382 RVA: 0x0033095D File Offset: 0x0032EB5D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<EventPassMissionType>(ref this.missionType);
		}

		// Token: 0x04008CF4 RID: 36084
		public EventPassMissionType missionType;
	}
}
