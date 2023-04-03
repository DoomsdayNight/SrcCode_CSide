using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020003CE RID: 974
	public class NKMKillFeedBack : IEventConditionOwner
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0006DC11 File Offset: 0x0006BE11
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0006DC48 File Offset: 0x0006BE48
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_fSkillCoolTime", ref this.m_fSkillCoolTime);
			cNKMLua.GetData("m_fHyperSkillCoolTime", ref this.m_fHyperSkillCoolTime);
			cNKMLua.GetData("m_fSkillCoolTimeAdd", ref this.m_fSkillCoolTimeAdd);
			cNKMLua.GetData("m_fHyperSkillCoolTimeAdd", ref this.m_fHyperSkillCoolTimeAdd);
			cNKMLua.GetData("m_fHPRate", ref this.m_fHPRate);
			cNKMLua.GetData("m_BuffName", ref this.m_BuffName);
			byte b = 0;
			if (cNKMLua.GetData("m_BuffLevel", ref b))
			{
				this.m_BuffStatLevel = b;
				this.m_BuffTimeLevel = b;
			}
			cNKMLua.GetData("m_BuffStatLevel", ref this.m_BuffStatLevel);
			cNKMLua.GetData("m_BuffTimeLevel", ref this.m_BuffTimeLevel);
			return true;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0006DD28 File Offset: 0x0006BF28
		public void DeepCopyFromSource(NKMKillFeedBack source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_fSkillCoolTime = source.m_fSkillCoolTime;
			this.m_fHyperSkillCoolTime = source.m_fHyperSkillCoolTime;
			this.m_fSkillCoolTimeAdd = source.m_fSkillCoolTimeAdd;
			this.m_fHyperSkillCoolTimeAdd = source.m_fHyperSkillCoolTimeAdd;
			this.m_fHPRate = source.m_fHPRate;
			this.m_BuffName = source.m_BuffName;
			this.m_BuffStatLevel = source.m_BuffStatLevel;
			this.m_BuffTimeLevel = source.m_BuffTimeLevel;
		}

		// Token: 0x04001278 RID: 4728
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04001279 RID: 4729
		public float m_fSkillCoolTime;

		// Token: 0x0400127A RID: 4730
		public float m_fHyperSkillCoolTime;

		// Token: 0x0400127B RID: 4731
		public float m_fSkillCoolTimeAdd;

		// Token: 0x0400127C RID: 4732
		public float m_fHyperSkillCoolTimeAdd;

		// Token: 0x0400127D RID: 4733
		public float m_fHPRate;

		// Token: 0x0400127E RID: 4734
		public string m_BuffName = "";

		// Token: 0x0400127F RID: 4735
		public byte m_BuffStatLevel = 1;

		// Token: 0x04001280 RID: 4736
		public byte m_BuffTimeLevel = 1;
	}
}
