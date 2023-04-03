using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000407 RID: 1031
	public class NKMGameSyncData_DieUnit : ISerializable
	{
		// Token: 0x06001B1B RID: 6939 RVA: 0x000770FC File Offset: 0x000752FC
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_DieGameUnitUID);
		}

		// Token: 0x04001ACB RID: 6859
		public HashSet<short> m_DieGameUnitUID = new HashSet<short>();
	}
}
