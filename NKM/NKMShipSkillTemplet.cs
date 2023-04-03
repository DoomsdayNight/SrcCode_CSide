using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200046F RID: 1135
	public class NKMShipSkillTemplet : INKMTemplet
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x00092FED File Offset: 0x000911ED
		public int Key
		{
			get
			{
				return this.m_ShipSkillID;
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00092FF8 File Offset: 0x000911F8
		public static NKMShipSkillTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMShipSkillTemplet nkmshipSkillTemplet = new NKMShipSkillTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("m_ShipSkillID", ref nkmshipSkillTemplet.m_ShipSkillID);
			flag &= cNKMLua.GetData("m_ShipSkillStrID", ref nkmshipSkillTemplet.m_ShipSkillStrID);
			flag &= cNKMLua.GetData("m_ShipSkillIcon", ref nkmshipSkillTemplet.m_ShipSkillIcon);
			flag &= cNKMLua.GetData("m_SkillName", ref nkmshipSkillTemplet.m_SkillName);
			cNKMLua.GetData("m_SkillDesc", ref nkmshipSkillTemplet.m_SkillDesc);
			cNKMLua.GetData("m_SkillBuildDesc", ref nkmshipSkillTemplet.m_SkillBuildDesc);
			flag &= cNKMLua.GetData<NKM_SKILL_TYPE>("m_NKM_SKILL_TYPE", ref nkmshipSkillTemplet.m_NKM_SKILL_TYPE);
			flag &= cNKMLua.GetData<NKM_SHIP_SKILL_USE_TYPE>("m_NKM_SHIP_SKILL_USE_TYPE", ref nkmshipSkillTemplet.m_NKM_SHIP_SKILL_USE_TYPE);
			NKM_SKILL_TYPE nkm_SKILL_TYPE = nkmshipSkillTemplet.m_NKM_SKILL_TYPE;
			if (nkm_SKILL_TYPE != NKM_SKILL_TYPE.NST_PASSIVE)
			{
				if (nkm_SKILL_TYPE == NKM_SKILL_TYPE.NST_SHIP_ACTIVE)
				{
					flag &= cNKMLua.GetData("m_UnitStateName", ref nkmshipSkillTemplet.m_UnitStateName);
					flag &= cNKMLua.GetData("m_bFullMap", ref nkmshipSkillTemplet.m_bFullMap);
					flag &= cNKMLua.GetData("m_fRange", ref nkmshipSkillTemplet.m_fRange);
					flag &= cNKMLua.GetData("m_bEnemy", ref nkmshipSkillTemplet.m_bEnemy);
					flag &= cNKMLua.GetData("m_bAir", ref nkmshipSkillTemplet.m_bAir);
					flag &= cNKMLua.GetData("m_fCooltimeSecond", ref nkmshipSkillTemplet.m_fCooltimeSecond);
					goto IL_209;
				}
				if (nkm_SKILL_TYPE != NKM_SKILL_TYPE.NST_LEADER)
				{
					Log.Error("ShipSkill Can't have skilltype " + nkmshipSkillTemplet.m_NKM_SKILL_TYPE.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipSkillManager.cs", 101);
					return null;
				}
			}
			nkmshipSkillTemplet.m_lstSkillStatData.Clear();
			for (int i = 0; i < 5; i++)
			{
				string str = (i + 1).ToString();
				NKM_STAT_TYPE nkm_STAT_TYPE = NKM_STAT_TYPE.NST_END;
				if (!cNKMLua.GetData<NKM_STAT_TYPE>("m_NKM_STAT_TYPE" + str, ref nkm_STAT_TYPE) || nkm_STAT_TYPE == NKM_STAT_TYPE.NST_END)
				{
					break;
				}
				float value = 0f;
				float rate = 0f;
				cNKMLua.GetData("m_fStatValue" + str, ref value);
				cNKMLua.GetData("m_fStatRate" + str, ref rate);
				nkmshipSkillTemplet.m_lstSkillStatData.Add(new SkillStatData(nkm_STAT_TYPE, value, rate));
			}
			IL_209:
			if (!flag)
			{
				Log.Error(string.Format("NKMShipSkillTemplet Load - {0}", nkmshipSkillTemplet.m_ShipSkillID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipSkillManager.cs", 107);
				return null;
			}
			return nkmshipSkillTemplet;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00093235 File Offset: 0x00091435
		public void Validate()
		{
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x00093237 File Offset: 0x00091437
		public void Join()
		{
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00093239 File Offset: 0x00091439
		public string GetName()
		{
			return NKCStringTable.GetString(this.m_SkillName, false);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00093247 File Offset: 0x00091447
		public string GetDesc()
		{
			return NKCStringTable.GetString(this.m_SkillDesc, false);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x00093255 File Offset: 0x00091455
		public string GetBuildDesc()
		{
			return NKCStringTable.GetString(this.m_SkillBuildDesc, false);
		}

		// Token: 0x04001F50 RID: 8016
		public int m_ShipSkillID;

		// Token: 0x04001F51 RID: 8017
		public string m_ShipSkillStrID = "";

		// Token: 0x04001F52 RID: 8018
		public string m_ShipSkillIcon = "";

		// Token: 0x04001F53 RID: 8019
		public string m_SkillName = "";

		// Token: 0x04001F54 RID: 8020
		public string m_SkillDesc = "";

		// Token: 0x04001F55 RID: 8021
		public string m_SkillBuildDesc = "";

		// Token: 0x04001F56 RID: 8022
		public NKM_SKILL_TYPE m_NKM_SKILL_TYPE;

		// Token: 0x04001F57 RID: 8023
		public NKM_SHIP_SKILL_USE_TYPE m_NKM_SHIP_SKILL_USE_TYPE = NKM_SHIP_SKILL_USE_TYPE.NSSUT_ENEMY;

		// Token: 0x04001F58 RID: 8024
		public string m_UnitStateName = "";

		// Token: 0x04001F59 RID: 8025
		public bool m_bFullMap;

		// Token: 0x04001F5A RID: 8026
		public float m_fRange;

		// Token: 0x04001F5B RID: 8027
		public bool m_bEnemy = true;

		// Token: 0x04001F5C RID: 8028
		public bool m_bAir = true;

		// Token: 0x04001F5D RID: 8029
		public float m_fCooltimeSecond = 1f;

		// Token: 0x04001F5E RID: 8030
		public List<SkillStatData> m_lstSkillStatData = new List<SkillStatData>(5);
	}
}
