using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D09 RID: 3337
	[PacketId(ClientPacketId.kNKMPacket_UNIT_MISSION_REWARD_REQ)]
	public sealed class NKMPacket_UNIT_MISSION_REWARD_REQ : ISerializable
	{
		// Token: 0x0600950F RID: 38159 RVA: 0x003299BF File Offset: 0x00327BBF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
			stream.PutOrGet(ref this.missionId);
			stream.PutOrGet(ref this.stepId);
		}

		// Token: 0x040086A3 RID: 34467
		public int unitId;

		// Token: 0x040086A4 RID: 34468
		public int missionId;

		// Token: 0x040086A5 RID: 34469
		public int stepId;
	}
}
