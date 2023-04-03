using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003C2 RID: 962
	public class NKMMoldItemData : ISerializable
	{
		// Token: 0x0600193B RID: 6459 RVA: 0x00067F9E File Offset: 0x0006619E
		public NKMMoldItemData()
		{
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00067FA6 File Offset: 0x000661A6
		public NKMMoldItemData(int moldID, long Count)
		{
			this.m_MoldID = moldID;
			this.m_Count = Count;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00067FBC File Offset: 0x000661BC
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_MoldID);
			stream.PutOrGet(ref this.m_Count);
		}

		// Token: 0x040011B2 RID: 4530
		public int m_MoldID;

		// Token: 0x040011B3 RID: 4531
		public long m_Count;
	}
}
