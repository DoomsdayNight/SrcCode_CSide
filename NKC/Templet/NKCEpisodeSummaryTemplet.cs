using System;
using Cs.Logging;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000847 RID: 2119
	public class NKCEpisodeSummaryTemplet : INKMTempletEx, INKMTemplet
	{
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x0019BC22 File Offset: 0x00199E22
		public int Key
		{
			get
			{
				return this.INDEX;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06005458 RID: 21592 RVA: 0x0019BC2A File Offset: 0x00199E2A
		internal bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06005459 RID: 21593 RVA: 0x0019BC37 File Offset: 0x00199E37
		// (set) Token: 0x0600545A RID: 21594 RVA: 0x0019BC3F File Offset: 0x00199E3F
		public NKMIntervalTemplet IntervalTemplet { get; private set; } = NKMIntervalTemplet.Invalid;

		// Token: 0x0600545B RID: 21595 RVA: 0x0019BC48 File Offset: 0x00199E48
		public static NKCEpisodeSummaryTemplet LoadFromLua(NKMLua lua)
		{
			NKCEpisodeSummaryTemplet nkcepisodeSummaryTemplet = new NKCEpisodeSummaryTemplet();
			bool flag = true & lua.GetData("INDEX", ref nkcepisodeSummaryTemplet.INDEX);
			lua.GetData("EpisodeID", ref nkcepisodeSummaryTemplet.m_EpisodeID);
			bool flag2 = flag & lua.GetData<EPISODE_CATEGORY>("m_EPCategory", ref nkcepisodeSummaryTemplet.m_EPCategory) & lua.GetData("m_SortIndex", ref nkcepisodeSummaryTemplet.m_SortIndex);
			lua.GetData("LobbyResourceID", ref nkcepisodeSummaryTemplet.m_LobbyResourceID);
			lua.GetData("BigResourceID", ref nkcepisodeSummaryTemplet.m_BigResourceID);
			lua.GetData("SubResourceID", ref nkcepisodeSummaryTemplet.m_SubResourceID);
			lua.GetData<NKM_SHORTCUT_TYPE>("m_ShortcutType", ref nkcepisodeSummaryTemplet.m_ShortcutType);
			lua.GetData("m_Shortcut", ref nkcepisodeSummaryTemplet.m_ShortcutParam);
			lua.GetData("DateStrID", ref nkcepisodeSummaryTemplet.dateStrId);
			lua.GetData("OpenTag", ref nkcepisodeSummaryTemplet.m_OpenTag);
			if (!flag2)
			{
				Log.ErrorAndExit(string.Format("INDEX : {0} - data loading failed.", nkcepisodeSummaryTemplet.INDEX), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCEpisodeSummaryTemplet.cs", 75);
			}
			return nkcepisodeSummaryTemplet;
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0019BD46 File Offset: 0x00199F46
		public static NKCEpisodeSummaryTemplet Find(EPISODE_CATEGORY category, int episodeID)
		{
			return NKMTempletContainer<NKCEpisodeSummaryTemplet>.Find((NKCEpisodeSummaryTemplet x) => x.m_EPCategory == category && x.m_EpisodeID == episodeID);
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0019BD6C File Offset: 0x00199F6C
		public void JoinIntervalTemplet()
		{
			if (!string.IsNullOrEmpty(this.dateStrId))
			{
				this.IntervalTemplet = NKMIntervalTemplet.Find(this.dateStrId);
				if (this.IntervalTemplet == null)
				{
					this.IntervalTemplet = NKMIntervalTemplet.Unuseable;
					Log.ErrorAndExit("잘못된 interval id :" + this.dateStrId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCEpisodeSummaryTemplet.cs", 91);
					return;
				}
				if (this.IntervalTemplet.IsRepeatDate)
				{
					Log.ErrorAndExit(string.Format("[NKCEpisodeSummaryTemplet:{0}] 반복 기간설정 사용 불가. id:{1}", this.Key, this.dateStrId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCEpisodeSummaryTemplet.cs", 97);
				}
			}
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0019BDFB File Offset: 0x00199FFB
		public void PostJoin()
		{
			this.JoinIntervalTemplet();
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x0019BE03 File Offset: 0x0019A003
		public void Join()
		{
			this.EpisodeTemplet = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x0019BE17 File Offset: 0x0019A017
		public void Validate()
		{
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x0019BE1C File Offset: 0x0019A01C
		public bool CheckEnable(DateTime current)
		{
			bool result = this.EnableByTag && this.IntervalTemplet.IsValidTime(current);
			if (this.m_EPCategory == EPISODE_CATEGORY.EC_FIERCE)
			{
				NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
				if (nkcfierceBattleSupportDataMgr.FierceTemplet == null || !nkcfierceBattleSupportDataMgr.IsCanAccessFierce())
				{
					return false;
				}
			}
			return result;
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x0019BE6C File Offset: 0x0019A06C
		public bool CheckEpisodeEnable(DateTime current)
		{
			bool flag = this.EpisodeTemplet != null && this.EpisodeTemplet.EnableByTag && this.EpisodeTemplet.IsOpen && this.EpisodeTemplet.IntervalTemplet.IsValidTime(current);
			bool flag2 = this.EpisodeTemplet != null && this.EpisodeTemplet.m_DicStage.Count > 0 && this.EpisodeTemplet.GetFirstStage(1) != null && this.EpisodeTemplet.GetFirstStage(1).IsOpenedDayOfWeek();
			return flag && flag2;
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x0019BEEF File Offset: 0x0019A0EF
		public bool HasDateLimit()
		{
			return this.EpisodeTemplet != null && this.EpisodeTemplet.HasEventTimeLimit;
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x0019BF08 File Offset: 0x0019A108
		public bool ShowInPVE_01()
		{
			EPISODE_CATEGORY epcategory = this.m_EPCategory;
			if (epcategory <= EPISODE_CATEGORY.EC_SIDESTORY)
			{
				if (epcategory != EPISODE_CATEGORY.EC_MAINSTREAM && epcategory != EPISODE_CATEGORY.EC_SIDESTORY)
				{
					return false;
				}
			}
			else if (epcategory != EPISODE_CATEGORY.EC_EVENT && epcategory != EPISODE_CATEGORY.EC_FIERCE)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x0019BF38 File Offset: 0x0019A138
		public bool ShowInPVE_02()
		{
			EPISODE_CATEGORY epcategory = this.m_EPCategory;
			return epcategory - EPISODE_CATEGORY.EC_SUPPLY <= 1;
		}

		// Token: 0x04004353 RID: 17235
		public string m_OpenTag = "";

		// Token: 0x04004354 RID: 17236
		public int INDEX;

		// Token: 0x04004355 RID: 17237
		public int m_EpisodeID;

		// Token: 0x04004356 RID: 17238
		public EPISODE_CATEGORY m_EPCategory = EPISODE_CATEGORY.EC_COUNT;

		// Token: 0x04004357 RID: 17239
		public int m_SortIndex;

		// Token: 0x04004358 RID: 17240
		public string m_LobbyResourceID;

		// Token: 0x04004359 RID: 17241
		public string m_BigResourceID;

		// Token: 0x0400435A RID: 17242
		public string m_SubResourceID;

		// Token: 0x0400435B RID: 17243
		public NKM_SHORTCUT_TYPE m_ShortcutType;

		// Token: 0x0400435C RID: 17244
		public string m_ShortcutParam;

		// Token: 0x0400435E RID: 17246
		private string dateStrId = "";

		// Token: 0x0400435F RID: 17247
		public NKMEpisodeTempletV2 EpisodeTemplet;
	}
}
