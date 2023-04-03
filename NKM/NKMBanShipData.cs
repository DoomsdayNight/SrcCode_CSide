using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020004FE RID: 1278
	public class NKMBanShipData : ISerializable
	{
		// Token: 0x0600241A RID: 9242 RVA: 0x000BB90F File Offset: 0x000B9B0F
		public void DeepCopyFromSource(NKMBanShipData source)
		{
			this.m_ShipGroupID = source.m_ShipGroupID;
			this.m_BanLevel = source.m_BanLevel;
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000BB929 File Offset: 0x000B9B29
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_ShipGroupID);
			stream.PutOrGet(ref this.m_BanLevel);
		}

		// Token: 0x040025DA RID: 9690
		public int m_ShipGroupID;

		// Token: 0x040025DB RID: 9691
		public byte m_BanLevel;
	}
}
