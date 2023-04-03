using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000409 RID: 1033
	public class NKMGameSyncData_Fierce : ISerializable
	{
		// Token: 0x06001B1E RID: 6942 RVA: 0x00077169 File Offset: 0x00075369
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_fFiercePoint);
		}

		// Token: 0x04001AD2 RID: 6866
		public int m_fFiercePoint;
	}
}
