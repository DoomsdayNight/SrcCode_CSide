using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F80 RID: 3968
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ)]
	public sealed class NKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ : ISerializable
	{
		// Token: 0x060099DC RID: 39388 RVA: 0x003309CF File Offset: 0x0032EBCF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<EventPassMissionType>(ref this.missionType);
		}

		// Token: 0x04008CFB RID: 36091
		public EventPassMissionType missionType;
	}
}
