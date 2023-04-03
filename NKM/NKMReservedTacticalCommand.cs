using System;

namespace NKM
{
	// Token: 0x02000421 RID: 1057
	public class NKMReservedTacticalCommand
	{
		// Token: 0x06001C8C RID: 7308 RVA: 0x00084C72 File Offset: 0x00082E72
		public NKMTacticalCommandTemplet GetNKMTacticalCommandTemplet()
		{
			return this.m_cNKMTacticalCommandTemplet;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00084C7A File Offset: 0x00082E7A
		public NKMTacticalCommandData GetNKMTacticalCommandData()
		{
			return this.m_cNKMTacticalCommandData;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00084C82 File Offset: 0x00082E82
		public NKM_TEAM_TYPE Get_NKM_TEAM_TYPE()
		{
			return this.m_NKM_TEAM_TYPE;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00084C8A File Offset: 0x00082E8A
		public void Invalidate()
		{
			this.m_fReservedTime = 0f;
			this.m_cNKMTacticalCommandTemplet = null;
			this.m_cNKMTacticalCommandData = null;
			this.m_NKM_TEAM_TYPE = NKM_TEAM_TYPE.NTT_INVALID;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00084CAC File Offset: 0x00082EAC
		public void SetNewData(float fReservedTime, NKMTacticalCommandTemplet cNKMTacticalCommandTemplet, NKMTacticalCommandData cNKMTacticalCommandData, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			if (this.m_fReservedTime > 0f)
			{
				return;
			}
			this.m_fReservedTime = fReservedTime;
			this.m_cNKMTacticalCommandTemplet = cNKMTacticalCommandTemplet;
			this.m_cNKMTacticalCommandData = cNKMTacticalCommandData;
			this.m_NKM_TEAM_TYPE = eNKM_TEAM_TYPE;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00084CD9 File Offset: 0x00082ED9
		public void Update(float fDeltaTime)
		{
			this.m_fReservedTime -= fDeltaTime;
			if (this.m_fReservedTime <= 0f)
			{
				this.m_fReservedTime = 0f;
			}
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00084D01 File Offset: 0x00082F01
		public bool CheckApplyTiming()
		{
			return this.m_cNKMTacticalCommandData != null && this.m_cNKMTacticalCommandTemplet != null && this.m_NKM_TEAM_TYPE != NKM_TEAM_TYPE.NTT_INVALID && this.m_fReservedTime <= 0f;
		}

		// Token: 0x04001BC6 RID: 7110
		private float m_fReservedTime;

		// Token: 0x04001BC7 RID: 7111
		private NKMTacticalCommandTemplet m_cNKMTacticalCommandTemplet;

		// Token: 0x04001BC8 RID: 7112
		private NKMTacticalCommandData m_cNKMTacticalCommandData;

		// Token: 0x04001BC9 RID: 7113
		private NKM_TEAM_TYPE m_NKM_TEAM_TYPE;
	}
}
