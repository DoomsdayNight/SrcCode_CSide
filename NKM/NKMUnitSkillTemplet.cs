using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020004B6 RID: 1206
	public sealed class NKMUnitSkillTemplet : INKMTemplet
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x000AB947 File Offset: 0x000A9B47
		public int Key
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000AB950 File Offset: 0x000A9B50
		public static NKMUnitSkillTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMUnitSkillTemplet nkmunitSkillTemplet = new NKMUnitSkillTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("m_UnitSkillID", ref nkmunitSkillTemplet.m_ID);
			flag &= cNKMLua.GetData("m_UnitSkillStrID", ref nkmunitSkillTemplet.m_strID);
			flag &= cNKMLua.GetData("m_Level", ref nkmunitSkillTemplet.m_Level);
			flag &= cNKMLua.GetData("m_UnitSkillIcon", ref nkmunitSkillTemplet.m_UnitSkillIcon);
			flag &= cNKMLua.GetData("m_SkillName", ref nkmunitSkillTemplet.m_SkillName);
			flag &= cNKMLua.GetData("m_SkillDesc", ref nkmunitSkillTemplet.m_SkillDesc);
			cNKMLua.GetData("m_AttackCount", ref nkmunitSkillTemplet.m_AttackCount);
			flag &= cNKMLua.GetData<NKM_SKILL_TYPE>("m_NKM_SKILL_TYPE", ref nkmunitSkillTemplet.m_NKM_SKILL_TYPE);
			if (nkmunitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_INVALID)
			{
				Log.Error("Skill Templet with INVALID skill type, table error!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitSkillManager.cs", 135);
				flag = false;
			}
			cNKMLua.GetData("m_UnlockReqUpgrade", ref nkmunitSkillTemplet.m_UnlockReqUpgrade);
			if (!flag)
			{
				Log.Error("UnitSkillTemplet parsing failed, id = " + nkmunitSkillTemplet.m_strID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitSkillManager.cs", 143);
				return null;
			}
			for (int i = 0; i < 4; i++)
			{
				int num = 0;
				int num2 = 0;
				cNKMLua.GetData("m_UpgradeReqtemID_" + (i + 1).ToString(), ref num);
				cNKMLua.GetData("m_UpgradeReqtemValue_" + (i + 1).ToString(), ref num2);
				if (num != 0 && num2 != 0)
				{
					nkmunitSkillTemplet.m_lstUpgradeReqItem.Add(new NKMUnitSkillTemplet.NKMUpgradeReqItem
					{
						ItemCount = num2,
						ItemID = num
					});
				}
			}
			cNKMLua.GetData("m_fCooltimeSecond", ref nkmunitSkillTemplet.m_fCooltimeSecond);
			cNKMLua.GetData("m_fEmpowerFactor", ref nkmunitSkillTemplet.m_fEmpowerFactor);
			nkmunitSkillTemplet.m_lstSkillStatData.Clear();
			for (int j = 0; j < 5; j++)
			{
				string str = (j + 1).ToString();
				NKM_STAT_TYPE nkm_STAT_TYPE = NKM_STAT_TYPE.NST_END;
				if (!cNKMLua.GetData<NKM_STAT_TYPE>("m_NKM_STAT_TYPE" + str, ref nkm_STAT_TYPE) || nkm_STAT_TYPE == NKM_STAT_TYPE.NST_END)
				{
					break;
				}
				float value = 0f;
				float rate = 0f;
				cNKMLua.GetData("m_fStatValue" + str, ref value);
				cNKMLua.GetData("m_fStatRate" + str, ref rate);
				nkmunitSkillTemplet.m_lstSkillStatData.Add(new SkillStatData(nkm_STAT_TYPE, value, rate));
			}
			return nkmunitSkillTemplet;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000ABB96 File Offset: 0x000A9D96
		public void Join()
		{
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000ABB98 File Offset: 0x000A9D98
		public void Validate()
		{
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000ABB9A File Offset: 0x000A9D9A
		public string GetSkillDesc()
		{
			return NKCStringTable.GetString(this.m_SkillDesc, false);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000ABBA8 File Offset: 0x000A9DA8
		public string GetSkillName()
		{
			return NKCStringTable.GetString(this.m_SkillName, false);
		}

		// Token: 0x04002254 RID: 8788
		public int m_ID;

		// Token: 0x04002255 RID: 8789
		public string m_strID = "";

		// Token: 0x04002256 RID: 8790
		public int m_Level = 1;

		// Token: 0x04002257 RID: 8791
		public string m_UnitSkillIcon = "";

		// Token: 0x04002258 RID: 8792
		public string m_SkillName = "";

		// Token: 0x04002259 RID: 8793
		public string m_SkillDesc = "";

		// Token: 0x0400225A RID: 8794
		public int m_AttackCount;

		// Token: 0x0400225B RID: 8795
		public NKM_SKILL_TYPE m_NKM_SKILL_TYPE;

		// Token: 0x0400225C RID: 8796
		public int m_UnlockReqUpgrade;

		// Token: 0x0400225D RID: 8797
		public List<NKMUnitSkillTemplet.NKMUpgradeReqItem> m_lstUpgradeReqItem = new List<NKMUnitSkillTemplet.NKMUpgradeReqItem>(4);

		// Token: 0x0400225E RID: 8798
		public float m_fCooltimeSecond;

		// Token: 0x0400225F RID: 8799
		public float m_fEmpowerFactor = 1f;

		// Token: 0x04002260 RID: 8800
		public List<SkillStatData> m_lstSkillStatData = new List<SkillStatData>(5);

		// Token: 0x04002261 RID: 8801
		private const int MAX_REQ_ITEM_TYPE_COUNT = 4;

		// Token: 0x0200121B RID: 4635
		public struct NKMUpgradeReqItem
		{
			// Token: 0x04009481 RID: 38017
			public int ItemID;

			// Token: 0x04009482 RID: 38018
			public int ItemCount;
		}
	}
}
