using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000405 RID: 1029
	public class NKMGameSyncData_Unit : ISerializable
	{
		// Token: 0x06001B17 RID: 6935 RVA: 0x00077048 File Offset: 0x00075248
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUnitSyncData>(ref this.m_NKMGameUnitSyncData);
		}

		// Token: 0x04001AC3 RID: 6851
		public NKMUnitSyncData m_NKMGameUnitSyncData;
	}
}
