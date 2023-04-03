using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000853 RID: 2131
	public static class NKCLobbyIconManager
	{
		// Token: 0x060054B5 RID: 21685 RVA: 0x0019CB21 File Offset: 0x0019AD21
		public static bool LoadFromLUA(string filename, string tabFileName)
		{
			NKCLobbyIconManager.m_DicIDX = NKMTempletLoader.LoadDictionary<NKCLobbyIconTemplet>("AB_SCRIPT", filename, "m_dicNKMMissionTempletByID", new Func<NKMLua, NKCLobbyIconTemplet>(NKCLobbyIconTemplet.LoadFromLUA));
			return NKCLobbyIconManager.m_DicIDX != null;
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0019CB50 File Offset: 0x0019AD50
		public static List<NKCLobbyIconTemplet> GetAvailableLobbyIconTemplet()
		{
			List<NKCLobbyIconTemplet> list = new List<NKCLobbyIconTemplet>();
			using (IEnumerator<NKCLobbyIconTemplet> enumerator = NKMTempletContainer<NKCLobbyIconTemplet>.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKCLobbyIconTemplet templet = enumerator.Current;
					if (list.Find((NKCLobbyIconTemplet e) => e.m_ShortCutType == templet.m_ShortCutType) == null && NKCSynchronizedTime.IsEventTime(templet.m_StartTimeUTC, templet.m_EndTimeUTC))
					{
						list.Add(templet);
					}
				}
			}
			list.Sort(new Comparison<NKCLobbyIconTemplet>(NKCLobbyIconManager.CompIDX));
			return list;
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0019CBF8 File Offset: 0x0019ADF8
		private static int CompIDX(NKCLobbyIconTemplet lItem, NKCLobbyIconTemplet rItem)
		{
			return lItem.IDX.CompareTo(rItem.IDX);
		}

		// Token: 0x040043A8 RID: 17320
		private static Dictionary<int, NKCLobbyIconTemplet> m_DicIDX = new Dictionary<int, NKCLobbyIconTemplet>();
	}
}
