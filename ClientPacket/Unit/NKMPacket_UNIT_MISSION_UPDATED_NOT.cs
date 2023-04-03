using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D0D RID: 3341
	[PacketId(ClientPacketId.kNKMPacket_UNIT_MISSION_UPDATED_NOT)]
	public sealed class NKMPacket_UNIT_MISSION_UPDATED_NOT : ISerializable
	{
		// Token: 0x06009517 RID: 38167 RVA: 0x00329A75 File Offset: 0x00327C75
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUnitMissionData>(ref this.rewardEnableMissions);
		}

		// Token: 0x040086AD RID: 34477
		public List<NKMUnitMissionData> rewardEnableMissions = new List<NKMUnitMissionData>();
	}
}
