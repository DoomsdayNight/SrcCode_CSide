using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020004FF RID: 1279
	public class NKMBanOperatorData : ISerializable
	{
		// Token: 0x0600241D RID: 9245 RVA: 0x000BB94B File Offset: 0x000B9B4B
		public void DeepCopyFromSource(NKMBanOperatorData source)
		{
			this.m_OperatorID = source.m_OperatorID;
			this.m_BanLevel = source.m_BanLevel;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000BB965 File Offset: 0x000B9B65
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_OperatorID);
			stream.PutOrGet(ref this.m_BanLevel);
		}

		// Token: 0x040025DC RID: 9692
		public int m_OperatorID;

		// Token: 0x040025DD RID: 9693
		public byte m_BanLevel;
	}
}
