using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200040E RID: 1038
	public class NKMGameSyncDataPack : ISerializable
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x000773AC File Offset: 0x000755AC
		public virtual void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMGameSyncData_Base>(ref this.m_listGameSyncData);
		}

		// Token: 0x04001AF4 RID: 6900
		public List<NKMGameSyncData_Base> m_listGameSyncData = new List<NKMGameSyncData_Base>();
	}
}
