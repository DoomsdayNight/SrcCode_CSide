using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200049A RID: 1178
	public class NKMLimitBreakItemTemplet : INKMTemplet
	{
		// Token: 0x060020F6 RID: 8438 RVA: 0x000A7E1C File Offset: 0x000A601C
		public static NKMLimitBreakItemTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitLimitBreakManager.cs", 72))
			{
				return null;
			}
			NKMLimitBreakItemTemplet nkmlimitBreakItemTemplet = new NKMLimitBreakItemTemplet();
			bool flag = true;
			flag &= lua.GetData<NKM_UNIT_STYLE_TYPE>("m_NKM_UNIT_STYLE_TYPE", ref nkmlimitBreakItemTemplet.m_NKM_UNIT_STYLE_TYPE);
			flag &= lua.GetData<NKM_UNIT_GRADE>("m_NKM_UNIT_GRADE", ref nkmlimitBreakItemTemplet.m_NKM_UNIT_GRADE);
			flag &= lua.GetData("m_TargetLimitbreakLevel", ref nkmlimitBreakItemTemplet.m_TargetLimitbreakLevel);
			flag &= lua.GetData("m_CreditReq", ref nkmlimitBreakItemTemplet.m_CreditReq);
			for (int i = 0; i < 2; i++)
			{
				int num = 0;
				lua.GetData("m_ItemID_" + (i + 1).ToString(), ref num);
				if (num != 0)
				{
					int count = 0;
					lua.GetData("m_ItemCount_" + (i + 1).ToString(), ref count);
					nkmlimitBreakItemTemplet.m_lstRequiredItem.Add(new NKMLimitBreakItemTemplet.ItemRequirement
					{
						itemID = num,
						count = count
					});
				}
			}
			if (!flag)
			{
				Log.Error("NKMLimitBreakItemTemplet Load Fail", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitLimitBreakManager.cs", 97);
				return null;
			}
			return nkmlimitBreakItemTemplet;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x000A7F21 File Offset: 0x000A6121
		public int Key
		{
			get
			{
				return NKMUnitLimitBreakManager.MakeKey(this.m_NKM_UNIT_STYLE_TYPE, this.m_NKM_UNIT_GRADE, this.m_TargetLimitbreakLevel);
			}
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000A7F3A File Offset: 0x000A613A
		public void Join()
		{
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000A7F3C File Offset: 0x000A613C
		public void Validate()
		{
		}

		// Token: 0x0400217B RID: 8571
		private const int ITEM_MAX_COUNT = 2;

		// Token: 0x0400217C RID: 8572
		public NKM_UNIT_STYLE_TYPE m_NKM_UNIT_STYLE_TYPE;

		// Token: 0x0400217D RID: 8573
		public NKM_UNIT_GRADE m_NKM_UNIT_GRADE;

		// Token: 0x0400217E RID: 8574
		public int m_TargetLimitbreakLevel;

		// Token: 0x0400217F RID: 8575
		public int m_CreditReq;

		// Token: 0x04002180 RID: 8576
		public List<NKMLimitBreakItemTemplet.ItemRequirement> m_lstRequiredItem = new List<NKMLimitBreakItemTemplet.ItemRequirement>(2);

		// Token: 0x02001213 RID: 4627
		public struct ItemRequirement
		{
			// Token: 0x04009466 RID: 37990
			public int itemID;

			// Token: 0x04009467 RID: 37991
			public int count;
		}
	}
}
