using System;

namespace NKM
{
	// Token: 0x020004A8 RID: 1192
	public class NKMPhaseChangeCoolTime
	{
		// Token: 0x06002135 RID: 8501 RVA: 0x000A96D5 File Offset: 0x000A78D5
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_StateName", ref this.m_StateName);
			cNKMLua.GetData("m_fCoolTime", ref this.m_fCoolTime);
			return true;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000A96FC File Offset: 0x000A78FC
		public void DeepCopyFromSource(NKMPhaseChangeCoolTime source)
		{
			this.m_StateName = source.m_StateName;
			this.m_fCoolTime = source.m_fCoolTime;
		}

		// Token: 0x04002202 RID: 8706
		public string m_StateName = "";

		// Token: 0x04002203 RID: 8707
		public float m_fCoolTime;
	}
}
