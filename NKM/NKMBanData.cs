using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020004FD RID: 1277
	public class NKMBanData : ISerializable
	{
		// Token: 0x06002417 RID: 9239 RVA: 0x000BB8D3 File Offset: 0x000B9AD3
		public void DeepCopyFromSource(NKMBanData source)
		{
			this.m_UnitID = source.m_UnitID;
			this.m_BanLevel = source.m_BanLevel;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000BB8ED File Offset: 0x000B9AED
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_UnitID);
			stream.PutOrGet(ref this.m_BanLevel);
		}

		// Token: 0x040025D8 RID: 9688
		public int m_UnitID;

		// Token: 0x040025D9 RID: 9689
		public byte m_BanLevel;
	}
}
