using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020004B5 RID: 1205
	public class NKMUnitSkillTempletContainer
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x000AB84D File Offset: 0x000A9A4D
		public IDictionary<int, NKMUnitSkillTemplet> dicTemplets
		{
			get
			{
				return this.m_dicSkillTemplet;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000AB855 File Offset: 0x000A9A55
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x000AB85D File Offset: 0x000A9A5D
		public int SkillID { get; private set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000AB866 File Offset: 0x000A9A66
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x000AB86E File Offset: 0x000A9A6E
		public string SkillStrID { get; private set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x000AB877 File Offset: 0x000A9A77
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x000AB87F File Offset: 0x000A9A7F
		public NKM_SKILL_TYPE SkillType { get; private set; }

		// Token: 0x0600218C RID: 8588 RVA: 0x000AB888 File Offset: 0x000A9A88
		public NKMUnitSkillTempletContainer(int skillID, string strID)
		{
			this.SkillID = skillID;
			this.SkillStrID = strID;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000AB8AC File Offset: 0x000A9AAC
		public NKMUnitSkillTemplet GetSkillTemplet(int level)
		{
			NKMUnitSkillTemplet result;
			if (this.m_dicSkillTemplet.TryGetValue(level, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000AB8CC File Offset: 0x000A9ACC
		public void AddSkillTemplet(NKMUnitSkillTemplet skillTemplet)
		{
			if (!this.m_dicSkillTemplet.Any<KeyValuePair<int, NKMUnitSkillTemplet>>())
			{
				this.SkillType = skillTemplet.m_NKM_SKILL_TYPE;
			}
			NKMUnitSkillTemplet nkmunitSkillTemplet;
			if (this.m_dicSkillTemplet.TryGetValue(skillTemplet.m_Level, out nkmunitSkillTemplet))
			{
				NKMTempletError.Add(string.Format("[UnitSkill] 동일 레벨의 스킬 템플릿이 이미 존재. level:{0} skillId:{1}/{2}", skillTemplet.m_Level, skillTemplet.m_strID, nkmunitSkillTemplet.m_strID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitSkillManager.cs", 77);
				return;
			}
			this.m_dicSkillTemplet.Add(skillTemplet.m_Level, skillTemplet);
		}

		// Token: 0x04002250 RID: 8784
		private SortedDictionary<int, NKMUnitSkillTemplet> m_dicSkillTemplet = new SortedDictionary<int, NKMUnitSkillTemplet>();
	}
}
