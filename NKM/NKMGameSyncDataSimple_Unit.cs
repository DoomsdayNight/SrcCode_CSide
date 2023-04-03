using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000406 RID: 1030
	public class NKMGameSyncDataSimple_Unit : ISerializable
	{
		// Token: 0x06001B19 RID: 6937 RVA: 0x00077088 File Offset: 0x00075288
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_GameUnitUID);
			stream.PutOrGet(ref this.m_TargetUID);
			stream.PutOrGet(ref this.m_SubTargetUID);
			stream.PutOrGet(ref this.m_bRight);
			stream.PutOrGet<NKMBuffSyncData>(ref this.m_dicBuffData);
			stream.PutOrGet(ref this.m_listNKM_UNIT_EVENT_MARK);
			stream.PutOrGet<NKMUnitStatusTimeSyncData>(ref this.m_listStatusTimeData);
		}

		// Token: 0x04001AC4 RID: 6852
		public short m_GameUnitUID;

		// Token: 0x04001AC5 RID: 6853
		public short m_TargetUID;

		// Token: 0x04001AC6 RID: 6854
		public short m_SubTargetUID;

		// Token: 0x04001AC7 RID: 6855
		public bool m_bRight = true;

		// Token: 0x04001AC8 RID: 6856
		public Dictionary<short, NKMBuffSyncData> m_dicBuffData = new Dictionary<short, NKMBuffSyncData>();

		// Token: 0x04001AC9 RID: 6857
		public List<byte> m_listNKM_UNIT_EVENT_MARK = new List<byte>();

		// Token: 0x04001ACA RID: 6858
		public List<NKMUnitStatusTimeSyncData> m_listStatusTimeData = new List<NKMUnitStatusTimeSyncData>();
	}
}
