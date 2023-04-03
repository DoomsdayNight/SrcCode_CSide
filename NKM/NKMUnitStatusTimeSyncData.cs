using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200048A RID: 1162
	public class NKMUnitStatusTimeSyncData : ISerializable
	{
		// Token: 0x06001F74 RID: 8052 RVA: 0x00095023 File Offset: 0x00093223
		public NKMUnitStatusTimeSyncData()
		{
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0009502B File Offset: 0x0009322B
		public NKMUnitStatusTimeSyncData(NKM_UNIT_STATUS_EFFECT type, float time)
		{
			this.m_eStatusType = type;
			this.m_fTime = time;
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00095041 File Offset: 0x00093241
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_UNIT_STATUS_EFFECT>(ref this.m_eStatusType);
			stream.PutOrGet(ref this.m_fTime);
		}

		// Token: 0x04002081 RID: 8321
		public NKM_UNIT_STATUS_EFFECT m_eStatusType;

		// Token: 0x04002082 RID: 8322
		public float m_fTime;
	}
}
