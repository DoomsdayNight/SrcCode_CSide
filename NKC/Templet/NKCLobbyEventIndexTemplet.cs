using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000851 RID: 2129
	public class NKCLobbyEventIndexTemplet : INKMTemplet
	{
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x0019C822 File Offset: 0x0019AA22
		public NKMIntervalTemplet IntervalTemplet
		{
			get
			{
				return NKMIntervalTemplet.Find(this.IntervalTag);
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x0019C82F File Offset: 0x0019AA2F
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.OpenTag);
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x0019C83C File Offset: 0x0019AA3C
		public int Key
		{
			get
			{
				return this.EventLobbyID;
			}
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x0019C844 File Offset: 0x0019AA44
		public static NKCLobbyEventIndexTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCLobbyEventIndexTemplet.cs", 29))
			{
				return null;
			}
			NKCLobbyEventIndexTemplet nkclobbyEventIndexTemplet = new NKCLobbyEventIndexTemplet();
			bool flag = true & cNKMLua.GetData("EventLobbyID", ref nkclobbyEventIndexTemplet.EventLobbyID) & cNKMLua.GetData("OpenTag", ref nkclobbyEventIndexTemplet.OpenTag) & cNKMLua.GetData("IntervalTag", ref nkclobbyEventIndexTemplet.IntervalTag);
			nkclobbyEventIndexTemplet.UnlockInfo = UnlockInfo.LoadFromLua(cNKMLua, true);
			bool flag2 = flag & cNKMLua.GetData("SortIndex", ref nkclobbyEventIndexTemplet.SortIndex) & cNKMLua.GetData("BannerID", ref nkclobbyEventIndexTemplet.BannerID) & cNKMLua.GetData<NKM_SHORTCUT_TYPE>("ShortCutType", ref nkclobbyEventIndexTemplet.ShortCutType);
			cNKMLua.GetData("ShortCutParam", ref nkclobbyEventIndexTemplet.ShortCutParam);
			cNKMLua.GetDataList("m_lstAlarmMissionTab", out nkclobbyEventIndexTemplet.m_lstAlarmMissionTab);
			if (!flag2)
			{
				return null;
			}
			return nkclobbyEventIndexTemplet;
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0019C90C File Offset: 0x0019AB0C
		public static List<NKCLobbyEventIndexTemplet> GetCurrentLobbyEvents()
		{
			List<NKCLobbyEventIndexTemplet> list = new List<NKCLobbyEventIndexTemplet>();
			foreach (NKCLobbyEventIndexTemplet nkclobbyEventIndexTemplet in NKMTempletContainer<NKCLobbyEventIndexTemplet>.Values)
			{
				if (nkclobbyEventIndexTemplet.EnableByTag && NKCSynchronizedTime.IsEventTime(nkclobbyEventIndexTemplet.IntervalTemplet) && NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkclobbyEventIndexTemplet.UnlockInfo, false))
				{
					list.Add(nkclobbyEventIndexTemplet);
				}
			}
			list.Sort((NKCLobbyEventIndexTemplet x, NKCLobbyEventIndexTemplet y) => x.SortIndex.CompareTo(y.SortIndex));
			return list;
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0019C9AC File Offset: 0x0019ABAC
		public void Join()
		{
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x0019C9AE File Offset: 0x0019ABAE
		public void Validate()
		{
		}

		// Token: 0x04004395 RID: 17301
		public int EventLobbyID;

		// Token: 0x04004396 RID: 17302
		public string OpenTag;

		// Token: 0x04004397 RID: 17303
		public string IntervalTag;

		// Token: 0x04004398 RID: 17304
		public UnlockInfo UnlockInfo;

		// Token: 0x04004399 RID: 17305
		public int SortIndex;

		// Token: 0x0400439A RID: 17306
		public string BannerID;

		// Token: 0x0400439B RID: 17307
		public NKM_SHORTCUT_TYPE ShortCutType;

		// Token: 0x0400439C RID: 17308
		public string ShortCutParam;

		// Token: 0x0400439D RID: 17309
		public List<int> m_lstAlarmMissionTab;
	}
}
