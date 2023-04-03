using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x020009C0 RID: 2496
	public class NKCUIOPDailyMission : MonoBehaviour
	{
		// Token: 0x06006A2D RID: 27181 RVA: 0x002271C2 File Offset: 0x002253C2
		public static NKCUIOPDailyMission GetInstance()
		{
			return NKCUIOPDailyMission.m_scNKCUIOPDailyMission;
		}

		// Token: 0x06006A2E RID: 27182 RVA: 0x002271CC File Offset: 0x002253CC
		public void InitUI()
		{
			NKCUIOPDailyMission.m_scNKCUIOPDailyMission = base.gameObject.GetComponent<NKCUIOPDailyMission>();
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].Init();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x0022721C File Offset: 0x0022541C
		public void Open()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].SetData((NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE)i, new NKCUIOPDailyMissionSlot.OnClickDM(this.OnClickDM));
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x00227269 File Offset: 0x00225469
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x00227278 File Offset: 0x00225478
		private void SelectEP(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return;
			}
			if (!NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.GetScenManager().GetMyUserData(), cNKMEpisodeTemplet))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DAILY_CHECK_DAY, null, "");
				return;
			}
			if (!cNKMEpisodeTemplet.IsOpenedDayOfWeek())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DAILY_CHECK_DAY, null, "");
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetEpisodeID(cNKMEpisodeTemplet.m_EpisodeID);
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetFirstOpen();
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetReservedActID(-1);
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x00227304 File Offset: 0x00225504
		public void OnClickDM(NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE dailyMissionType)
		{
			ContentsType contentsType = NKCContentManager.GetContentsType(dailyMissionType);
			if (!NKCContentManager.IsContentsUnlocked(contentsType, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(contentsType, 0);
				return;
			}
			this.SelectEP(NKMEpisodeTempletV2.Find(NKMEpisodeMgr.GetDailyMissionEPId(dailyMissionType), EPISODE_DIFFICULTY.NORMAL));
		}

		// Token: 0x06006A33 RID: 27187 RVA: 0x00227340 File Offset: 0x00225540
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].SetTicketCount();
			}
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x00227374 File Offset: 0x00225574
		public RectTransform GetDailyRect()
		{
			for (int i = 0; i < 3; i++)
			{
				if (!this.m_lstSlot[i].m_objLock.activeSelf)
				{
					return this.m_lstSlot[i].GetComponent<RectTransform>();
				}
			}
			return null;
		}

		// Token: 0x040055E4 RID: 21988
		private static NKCUIOPDailyMission m_scNKCUIOPDailyMission;

		// Token: 0x040055E5 RID: 21989
		public List<NKCUIOPDailyMissionSlot> m_lstSlot;

		// Token: 0x020016C0 RID: 5824
		public enum NKC_DAILY_MISSION_TYPE
		{
			// Token: 0x0400A529 RID: 42281
			NDMT_ATTACK,
			// Token: 0x0400A52A RID: 42282
			NDMT_DEFENSE,
			// Token: 0x0400A52B RID: 42283
			NDMT_SEARCH,
			// Token: 0x0400A52C RID: 42284
			NDMT_COUNT
		}
	}
}
