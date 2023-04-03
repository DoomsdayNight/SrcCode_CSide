using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200052A RID: 1322
	public class NKCRandomMoldTabTemplet : INKMTemplet
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x000C22A4 File Offset: 0x000C04A4
		public int Key
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000C22AC File Offset: 0x000C04AC
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000C22BC File Offset: 0x000C04BC
		public static NKCRandomMoldTabTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKCRandomMoldTabTemplet nkcrandomMoldTabTemplet = new NKCRandomMoldTabTemplet();
			bool flag = true & cNKMLua.GetData("IDX", ref nkcrandomMoldTabTemplet.index) & cNKMLua.GetData("m_TabOrder", ref nkcrandomMoldTabTemplet.m_TabOrder) & cNKMLua.GetData<NKM_CRAFT_TAB_TYPE>("m_MoldTabID", ref nkcrandomMoldTabTemplet.m_MoldTabID) & cNKMLua.GetData("m_MoldTabName", ref nkcrandomMoldTabTemplet.m_MoldTabName) & cNKMLua.GetData("m_MoldTabIconName", ref nkcrandomMoldTabTemplet.m_MoldTabIconName);
			cNKMLua.GetData("m_OpenTag", ref nkcrandomMoldTabTemplet.m_OpenTag);
			if (!flag)
			{
				Log.ErrorAndExit(string.Format("NKCRandomMoldTabTemplet 정보를 읽어오지 못하였습니다. index : {0}", nkcrandomMoldTabTemplet.index), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 248);
			}
			if (cNKMLua.OpenTable("m_MoldTab_Filter"))
			{
				bool flag2 = true;
				int num = 1;
				string item = "";
				while (flag2)
				{
					flag2 = cNKMLua.GetData(num, ref item);
					if (flag2)
					{
						nkcrandomMoldTabTemplet.m_MoldTab_Filter.Add(item);
					}
					num++;
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_MoldTab_Sort"))
			{
				bool flag3 = true;
				int num2 = 1;
				string item2 = "";
				while (flag3)
				{
					flag3 = cNKMLua.GetData(num2, ref item2);
					if (flag3)
					{
						nkcrandomMoldTabTemplet.m_MoldTab_Sort.Add(item2);
					}
					num2++;
				}
				cNKMLua.CloseTable();
			}
			nkcrandomMoldTabTemplet.CheckValidation();
			return nkcrandomMoldTabTemplet;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000C23F3 File Offset: 0x000C05F3
		public void Join()
		{
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000C23F5 File Offset: 0x000C05F5
		public void Validate()
		{
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000C23F8 File Offset: 0x000C05F8
		private void CheckValidation()
		{
			for (int i = 0; i < this.m_MoldTab_Sort.Count; i++)
			{
				if (!NKCMoldSortSystem.MoldSortData.ContainsKey(this.m_MoldTab_Sort[i]))
				{
					Log.ErrorAndExit(string.Format("[NKCRandomMoldTabTemplet] Mold Sorting 정보가 없습니다. sortCnt : {0}, sort name : {1}", i, this.m_MoldTab_Sort[i]), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 306);
				}
			}
			for (int j = 0; j < this.m_MoldTab_Filter.Count; j++)
			{
				if (!NKCMoldSortSystem.MoldFilterData.Contains(this.m_MoldTab_Filter[j]))
				{
					Log.ErrorAndExit(string.Format("[NKCRandomMoldTabTemplet] Mold Filter 정보가 없습니다. filterCnt : {0}, sort name : {1}", j, this.m_MoldTab_Filter[j]), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMItemManagerEx.cs", 312);
				}
			}
		}

		// Token: 0x04002731 RID: 10033
		public int index;

		// Token: 0x04002732 RID: 10034
		public int m_TabOrder;

		// Token: 0x04002733 RID: 10035
		public NKM_CRAFT_TAB_TYPE m_MoldTabID;

		// Token: 0x04002734 RID: 10036
		public string m_MoldTabName;

		// Token: 0x04002735 RID: 10037
		public string m_MoldTabIconName;

		// Token: 0x04002736 RID: 10038
		public List<string> m_MoldTab_Filter = new List<string>();

		// Token: 0x04002737 RID: 10039
		public List<string> m_MoldTab_Sort = new List<string>();

		// Token: 0x04002738 RID: 10040
		private string m_OpenTag;
	}
}
