using System;
using Cs.Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D08 RID: 3336
	public sealed class NKMUnitMissionData : ISerializable
	{
		// Token: 0x0600950D RID: 38157 RVA: 0x00329991 File Offset: 0x00327B91
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
			stream.PutOrGet(ref this.missionId);
			stream.PutOrGet(ref this.stepId);
		}

		// Token: 0x040086A0 RID: 34464
		public int unitId;

		// Token: 0x040086A1 RID: 34465
		public int missionId;

		// Token: 0x040086A2 RID: 34466
		public int stepId;
	}
}
