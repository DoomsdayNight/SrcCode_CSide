using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000413 RID: 1043
	public class NKMShipResultData : ISerializable
	{
		// Token: 0x06001B54 RID: 6996 RVA: 0x00077D81 File Offset: 0x00075F81
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_GameUnitUID);
			stream.PutOrGet(ref this.m_fHP);
		}

		// Token: 0x04001B1B RID: 6939
		public short m_GameUnitUID;

		// Token: 0x04001B1C RID: 6940
		public float m_fHP;
	}
}
