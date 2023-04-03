using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004AA RID: 1194
	public class NKMStaticBuffData : IEventConditionOwner
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x000A9913 File Offset: 0x000A7B13
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000A9952 File Offset: 0x000A7B52
		public bool Validate()
		{
			return string.IsNullOrEmpty(this.m_BuffStrID) || NKMBuffManager.GetBuffTempletByStrID(this.m_BuffStrID) != null;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000A9974 File Offset: 0x000A7B74
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_BuffStrID", ref this.m_BuffStrID);
			byte b = 0;
			if (cNKMLua.GetData("m_BuffLevel", ref b))
			{
				this.m_BuffStatLevel = b;
				this.m_BuffTimeLevel = b;
			}
			cNKMLua.GetData("m_BuffStatLevel", ref this.m_BuffStatLevel);
			cNKMLua.GetData("m_BuffTimeLevel", ref this.m_BuffTimeLevel);
			cNKMLua.GetData("m_fRebuffTime", ref this.m_fRebuffTime);
			cNKMLua.GetData("m_fRange", ref this.m_fRange);
			cNKMLua.GetData("m_bMyTeam", ref this.m_bMyTeam);
			cNKMLua.GetData("m_bEnemy", ref this.m_bEnemy);
			return true;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000A9A40 File Offset: 0x000A7C40
		public void DeepCopyFromSource(NKMStaticBuffData source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_BuffStrID = source.m_BuffStrID;
			this.m_BuffStatLevel = source.m_BuffStatLevel;
			this.m_BuffTimeLevel = source.m_BuffTimeLevel;
			this.m_fRebuffTime = source.m_fRebuffTime;
			this.m_fRange = source.m_fRange;
			this.m_bMyTeam = source.m_bMyTeam;
			this.m_bEnemy = source.m_bEnemy;
		}

		// Token: 0x0400220D RID: 8717
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400220E RID: 8718
		public string m_BuffStrID = "";

		// Token: 0x0400220F RID: 8719
		public byte m_BuffStatLevel = 1;

		// Token: 0x04002210 RID: 8720
		public byte m_BuffTimeLevel = 1;

		// Token: 0x04002211 RID: 8721
		public float m_fRebuffTime = -1f;

		// Token: 0x04002212 RID: 8722
		public float m_fRange;

		// Token: 0x04002213 RID: 8723
		public bool m_bMyTeam;

		// Token: 0x04002214 RID: 8724
		public bool m_bEnemy;
	}
}
