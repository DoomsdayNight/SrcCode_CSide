using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006AE RID: 1710
	public class NKCNewsManager
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x00124BE6 File Offset: 0x00122DE6
		public static Dictionary<int, NKCNewsTemplet> DicNewsTemplet
		{
			get
			{
				return NKCNewsManager.m_dicNewsTemplet;
			}
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x00124BF0 File Offset: 0x00122DF0
		public static bool LoadFromLua()
		{
			NKCNewsManager.m_dicNewsTemplet = NKMTempletLoader.LoadDictionary<NKCNewsTemplet>("AB_SCRIPT", "LUA_NEWS_TEMPLET", "NEWS_TEMPLET", new Func<NKMLua, NKCNewsTemplet>(NKCNewsTemplet.LoadFromLUA));
			if (NKCNewsManager.m_dicNewsTemplet == null)
			{
				return false;
			}
			foreach (KeyValuePair<int, NKCNewsTemplet> keyValuePair in NKCNewsManager.m_dicNewsTemplet)
			{
				if (keyValuePair.Value.m_DateStartUtc > keyValuePair.Value.m_DateEndUtc)
				{
					Log.Error(string.Format("IDX {0} 의 StartDate가 EndDate보다 늦음 - {1} ~ {2}", keyValuePair.Value.Idx, keyValuePair.Value.m_DateStart.ToShortDateString(), keyValuePair.Value.m_DateEnd.ToShortDateString()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCNewsManager.cs", 89);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x00124CD8 File Offset: 0x00122ED8
		public static NKCNewsTemplet GetNewsTemplet(int idx)
		{
			if (NKCNewsManager.m_dicNewsTemplet.ContainsKey(idx))
			{
				return NKCNewsManager.m_dicNewsTemplet[idx];
			}
			return null;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x00124CF4 File Offset: 0x00122EF4
		public static List<NKCNewsTemplet> GetActivatedNewsTempletList()
		{
			List<NKCNewsTemplet> list = new List<NKCNewsTemplet>();
			foreach (NKCNewsTemplet nkcnewsTemplet in NKCNewsManager.DicNewsTemplet.Values)
			{
				if (NKCSynchronizedTime.IsEventTime(nkcnewsTemplet.m_DateStartUtc, nkcnewsTemplet.m_DateEndUtc))
				{
					list.Add(nkcnewsTemplet);
				}
			}
			return list;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x00124D68 File Offset: 0x00122F68
		public static void SortByFilterType(DateTime now, out List<NKCNewsTemplet> newsList, out List<NKCNewsTemplet> noticeList)
		{
			newsList = new List<NKCNewsTemplet>();
			noticeList = new List<NKCNewsTemplet>();
			foreach (KeyValuePair<int, NKCNewsTemplet> keyValuePair in NKCNewsManager.m_dicNewsTemplet)
			{
				if (keyValuePair.Value.m_DateEndUtc > now && keyValuePair.Value.m_DateStartUtc <= now)
				{
					if (keyValuePair.Value.m_FilterType == eNewsFilterType.NEWS)
					{
						newsList.Add(keyValuePair.Value);
					}
					else if (keyValuePair.Value.m_FilterType == eNewsFilterType.NOTICE)
					{
						noticeList.Add(keyValuePair.Value);
					}
				}
			}
			newsList.Sort(new NKCNewsManager.CompTempletOrderAscending());
			noticeList.Sort(new NKCNewsManager.CompTempletOrderAscending());
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x00124E40 File Offset: 0x00123040
		public static bool CheckNeedNewsPopup(DateTime now)
		{
			return NKCNewsManager.GetActivatedNewsTempletList().Count != 0 && (!PlayerPrefs.HasKey(NKCNewsManager.GetPreferenceString(NKCNewsManager.NKM_LOCAL_SAVE_NEXT_NEWS_POPUP_SHOW_TIME)) || new DateTime(long.Parse(PlayerPrefs.GetString(NKCNewsManager.GetPreferenceString(NKCNewsManager.NKM_LOCAL_SAVE_NEXT_NEWS_POPUP_SHOW_TIME)))) < now);
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x00124E92 File Offset: 0x00123092
		public static string GetPreferenceString(string baseString)
		{
			return string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, baseString);
		}

		// Token: 0x040034D9 RID: 13529
		public static string NKM_LOCAL_SAVE_NEXT_NEWS_POPUP_SHOW_TIME = "NKM_LOCAL_SAVE_NEXT_NEWS_POPUP_SHOW_TIME";

		// Token: 0x040034DA RID: 13530
		private static Dictionary<int, NKCNewsTemplet> m_dicNewsTemplet = null;

		// Token: 0x0200137A RID: 4986
		public class CompTempletOrderAscending : IComparer<NKCNewsTemplet>
		{
			// Token: 0x0600A5FD RID: 42493 RVA: 0x0034630C File Offset: 0x0034450C
			public int Compare(NKCNewsTemplet x, NKCNewsTemplet y)
			{
				if (x.m_Order < y.m_Order)
				{
					return -1;
				}
				if (x.m_Order != y.m_Order)
				{
					return 1;
				}
				if (x.m_DateStartUtc < y.m_DateStartUtc)
				{
					return -1;
				}
				if (!(x.m_DateStartUtc == y.m_DateStartUtc))
				{
					return 1;
				}
				if (x.Idx < y.Idx)
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
