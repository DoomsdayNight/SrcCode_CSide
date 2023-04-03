using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000455 RID: 1109
	public class NpcPvpData : ISerializable
	{
		// Token: 0x06001E1B RID: 7707 RVA: 0x0008EF44 File Offset: 0x0008D144
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.MaxTierCount);
			stream.PutOrGet(ref this.MaxOpenedTier);
		}

		// Token: 0x04001ECF RID: 7887
		public int MaxTierCount;

		// Token: 0x04001ED0 RID: 7888
		public int MaxOpenedTier;
	}
}
