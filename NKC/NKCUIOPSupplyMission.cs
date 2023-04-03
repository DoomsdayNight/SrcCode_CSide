using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007D0 RID: 2000
	public class NKCUIOPSupplyMission : MonoBehaviour
	{
		// Token: 0x06004EF2 RID: 20210 RVA: 0x0017D33A File Offset: 0x0017B53A
		public void InitUI()
		{
			NKCUtil.SetScrollHotKey(this.m_srList, null);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x0017D354 File Offset: 0x0017B554
		private void BuildEPTemplets(EPISODE_CATEGORY category)
		{
			this.m_dicTemplet.Clear();
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(category, true, EPISODE_DIFFICULTY.NORMAL);
			for (int i = 0; i < listNKMEpisodeTempletByCategory.Count; i++)
			{
				if (!this.m_dicTemplet.ContainsKey(listNKMEpisodeTempletByCategory[i].m_EpisodeID))
				{
					this.m_dicTemplet.Add(listNKMEpisodeTempletByCategory[i].m_EpisodeID, new List<NKMEpisodeTempletV2>
					{
						listNKMEpisodeTempletByCategory[i]
					});
				}
				else
				{
					this.m_dicTemplet[listNKMEpisodeTempletByCategory[i].m_EpisodeID].Add(listNKMEpisodeTempletByCategory[i]);
				}
			}
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x0017D3F0 File Offset: 0x0017B5F0
		private void BuildEPSlot(EPISODE_CATEGORY category)
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstSlot[i].gameObject);
			}
			this.m_lstSlot.Clear();
			NKCUIOPSupplyMissionSlot original;
			if (category == EPISODE_CATEGORY.EC_SUPPLY || category != EPISODE_CATEGORY.EC_CHALLENGE)
			{
				original = this.m_pfbSlot;
			}
			else
			{
				original = this.m_pfbChallengeSlot;
			}
			for (int j = 0; j < this.m_dicTemplet.Count; j++)
			{
				NKCUIOPSupplyMissionSlot nkcuiopsupplyMissionSlot = UnityEngine.Object.Instantiate<NKCUIOPSupplyMissionSlot>(original, this.m_rtSlotParent);
				nkcuiopsupplyMissionSlot.Init();
				this.m_lstSlot.Add(nkcuiopsupplyMissionSlot);
			}
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x0017D484 File Offset: 0x0017B684
		public void Open(EPISODE_CATEGORY category)
		{
			this.BuildEPTemplets(category);
			if (this.m_CurrentCategory != category || this.m_dicTemplet.Count != this.m_lstSlot.Count)
			{
				this.BuildEPSlot(category);
			}
			this.m_CurrentCategory = category;
			if (category == EPISODE_CATEGORY.EC_SUPPLY || category != EPISODE_CATEGORY.EC_CHALLENGE)
			{
				this.m_rtSlotParent.pivot = new Vector2(0f, 0.5f);
				this.m_rtSlotParent.GetComponent<LayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
			}
			else
			{
				this.m_rtSlotParent.pivot = new Vector2(0.5f, 1f);
				this.m_rtSlotParent.GetComponent<LayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
			}
			int num = 0;
			foreach (KeyValuePair<int, List<NKMEpisodeTempletV2>> keyValuePair in this.m_dicTemplet)
			{
				if (this.m_lstSlot.Count <= num)
				{
					break;
				}
				int key = keyValuePair.Key;
				NKMEpisodeTempletV2 nkmepisodeTempletV = keyValuePair.Value[0];
				this.m_lstSlot[num].SetData(this.m_CurrentCategory, key, new NKCUIOPSupplyMissionSlot.OnClickSM(this.OnClickSMSlot));
				num++;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x0017D5C0 File Offset: 0x0017B7C0
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x0017D5CE File Offset: 0x0017B7CE
		public void OnClickSMSlot(int episodeId)
		{
			this.SelectEP(episodeId);
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x0017D5D8 File Offset: 0x0017B7D8
		private void SelectEP(int episodeId)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeId, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV == null)
			{
				return;
			}
			if (!NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.GetScenManager().GetMyUserData(), nkmepisodeTempletV))
			{
				NKCContentManager.ShowLockedMessagePopup(NKCContentManager.GetContentsType(this.m_CurrentCategory), 0);
				return;
			}
			if (!nkmepisodeTempletV.IsOpenedDayOfWeek())
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_DAILY_CHECK_DAY, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetEpisodeID(nkmepisodeTempletV.m_EpisodeID);
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetFirstOpen();
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetReservedActID(-1);
		}

		// Token: 0x04003ED7 RID: 16087
		public NKCUIOPSupplyMissionSlot m_pfbSlot;

		// Token: 0x04003ED8 RID: 16088
		public NKCUIOPSupplyMissionSlot m_pfbChallengeSlot;

		// Token: 0x04003ED9 RID: 16089
		public ScrollRect m_srList;

		// Token: 0x04003EDA RID: 16090
		public RectTransform m_rtSlotParent;

		// Token: 0x04003EDB RID: 16091
		private List<NKCUIOPSupplyMissionSlot> m_lstSlot = new List<NKCUIOPSupplyMissionSlot>();

		// Token: 0x04003EDC RID: 16092
		private Dictionary<int, List<NKMEpisodeTempletV2>> m_dicTemplet = new Dictionary<int, List<NKMEpisodeTempletV2>>();

		// Token: 0x04003EDD RID: 16093
		private EPISODE_CATEGORY m_CurrentCategory = EPISODE_CATEGORY.EC_COUNT;
	}
}
