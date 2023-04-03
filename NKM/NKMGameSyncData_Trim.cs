using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200040A RID: 1034
	public class NKMGameSyncData_Trim : ISerializable
	{
		// Token: 0x06001B20 RID: 6944 RVA: 0x0007717F File Offset: 0x0007537F
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_fTrimPoint);
		}

		// Token: 0x04001AD3 RID: 6867
		public int m_fTrimPoint;
	}
}
