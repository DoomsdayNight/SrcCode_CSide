using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC.Templet
{
	// Token: 0x02000848 RID: 2120
	public class NKCEventMissionTemplet : INKMTemplet
	{
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06005467 RID: 21607 RVA: 0x0019BF86 File Offset: 0x0019A186
		public int Key
		{
			get
			{
				return this.m_EventID;
			}
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x0019BF8E File Offset: 0x0019A18E
		public static NKCEventMissionTemplet Find(int id)
		{
			return NKMTempletContainer<NKCEventMissionTemplet>.Find(id);
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x0019BF98 File Offset: 0x0019A198
		public static NKCEventMissionTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCEventMissionTemplet.cs", 26))
			{
				return null;
			}
			NKCEventMissionTemplet nkceventMissionTemplet = new NKCEventMissionTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("m_EventID", ref nkceventMissionTemplet.m_EventID);
			cNKMLua.GetData("m_SpecialMissionTab", ref nkceventMissionTemplet.m_SpecialMissionTab);
			cNKMLua.GetData<NKM_SHORTCUT_TYPE>("m_ShortCutType", ref nkceventMissionTemplet.m_ShortcutType);
			cNKMLua.GetData("m_ShortCut", ref nkceventMissionTemplet.m_ShortcutParam);
			if (cNKMLua.OpenTable("m_UseMissionTab"))
			{
				int num = 1;
				int item = 0;
				while (cNKMLua.GetData(num, ref item))
				{
					nkceventMissionTemplet.m_lstMissionTab.Add(item);
					num++;
				}
				cNKMLua.CloseTable();
			}
			if (!(flag & nkceventMissionTemplet.m_lstMissionTab.Count > 0))
			{
				return null;
			}
			return nkceventMissionTemplet;
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x0019C054 File Offset: 0x0019A254
		public void Join()
		{
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x0019C058 File Offset: 0x0019A258
		public void Validate()
		{
			foreach (int num in this.m_lstMissionTab)
			{
				NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(num);
				if (missionTabTemplet == null)
				{
					Debug.LogError(string.Format("Event {0} : MissionTab {1} does not Exist!", this.m_EventID, num));
				}
				else if (missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.EVENT)
				{
					Debug.LogError(string.Format("Event {0} : MissionTab {1} is not a EVENT tab!", this.m_EventID, num));
				}
			}
		}

		// Token: 0x04004360 RID: 17248
		public int m_EventID;

		// Token: 0x04004361 RID: 17249
		public NKM_SHORTCUT_TYPE m_ShortcutType;

		// Token: 0x04004362 RID: 17250
		public string m_ShortcutParam = "";

		// Token: 0x04004363 RID: 17251
		public List<int> m_lstMissionTab = new List<int>();

		// Token: 0x04004364 RID: 17252
		public int m_SpecialMissionTab;
	}
}
