using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000472 RID: 1138
	public class NKMShopRandomData : ISerializable
	{
		// Token: 0x06001EFA RID: 7930 RVA: 0x00093471 File Offset: 0x00091671
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMShopRandomListData>(ref this.datas);
			stream.PutOrGet(ref this.nextRefreshDate);
			stream.PutOrGet(ref this.refreshCount);
		}

		// Token: 0x04001F68 RID: 8040
		public Dictionary<int, NKMShopRandomListData> datas = new Dictionary<int, NKMShopRandomListData>();

		// Token: 0x04001F69 RID: 8041
		public long nextRefreshDate;

		// Token: 0x04001F6A RID: 8042
		public int refreshCount;
	}
}
