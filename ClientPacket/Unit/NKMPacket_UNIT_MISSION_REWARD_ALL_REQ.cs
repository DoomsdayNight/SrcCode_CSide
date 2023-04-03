using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D0B RID: 3339
	[PacketId(ClientPacketId.kNKMPacket_UNIT_MISSION_REWARD_ALL_REQ)]
	public sealed class NKMPacket_UNIT_MISSION_REWARD_ALL_REQ : ISerializable
	{
		// Token: 0x06009513 RID: 38163 RVA: 0x00329A26 File Offset: 0x00327C26
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
		}

		// Token: 0x040086A9 RID: 34473
		public int unitId;
	}
}
