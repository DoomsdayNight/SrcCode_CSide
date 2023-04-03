using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020004F7 RID: 1271
	public class NKMUserDateData : ISerializable
	{
		// Token: 0x0600240A RID: 9226 RVA: 0x000BB6F6 File Offset: 0x000B98F6
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_RegisterTime);
			stream.PutOrGet(ref this.m_LastLogInTime);
			stream.PutOrGet(ref this.m_LastLogOutTime);
		}

		// Token: 0x040025BD RID: 9661
		public DateTime m_RegisterTime;

		// Token: 0x040025BE RID: 9662
		public DateTime m_LastLogInTime;

		// Token: 0x040025BF RID: 9663
		public DateTime m_LastLogOutTime;
	}
}
