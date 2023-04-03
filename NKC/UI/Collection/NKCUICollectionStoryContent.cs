using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C29 RID: 3113
	public class NKCUICollectionStoryContent : MonoBehaviour
	{
		// Token: 0x06009062 RID: 36962 RVA: 0x00312A53 File Offset: 0x00310C53
		public List<RectTransform> GetRentalList()
		{
			return this.m_lstRentalSlot;
		}

		// Token: 0x06009063 RID: 36963 RVA: 0x00312A5B File Offset: 0x00310C5B
		public List<RectTransform> GetSubRentalList()
		{
			return this.m_lstRentalSubTitle;
		}

		// Token: 0x06009064 RID: 36964 RVA: 0x00312A63 File Offset: 0x00310C63
		public void ClearRentalList()
		{
			this.m_lstRentalSlot.Clear();
			this.m_lstRentalSubTitle.Clear();
		}

		// Token: 0x06009065 RID: 36965 RVA: 0x00312A7B File Offset: 0x00310C7B
		public void Init()
		{
		}

		// Token: 0x06009066 RID: 36966 RVA: 0x00312A80 File Offset: 0x00310C80
		public void SetData(NKCUICollectionStory.StorySlotData SlotData, List<RectTransform> lstSlot, List<RectTransform> lstSubTitle)
		{
			if (SlotData == null)
			{
				return;
			}
			this.m_ActTitle.Clear();
			NKCCollectionManager.COLLECTION_STORY_CATEGORY eCategory = SlotData.m_eCategory;
			bool flag = false;
			switch (eCategory)
			{
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.MAINSTREAM:
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_MAIN_01, SlotData.m_EpisodeTitle);
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_MAIN_02, SlotData.m_EpisodeName);
				goto IL_C1;
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP:
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_SIDE_01, NKCUtilString.GET_STRING_DIVE);
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_SIDE_02, "");
				goto IL_C1;
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.ETC:
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_SIDE_01, "");
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_SIDE_02, SlotData.m_EpisodeName);
				flag = true;
				goto IL_C1;
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_SIDE_01, SlotData.m_EpisodeTitle);
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_SIDE_02, SlotData.m_EpisodeName);
			IL_C1:
			NKCUtil.SetGameobjectActive(this.m_objMainStream, eCategory == NKCCollectionManager.COLLECTION_STORY_CATEGORY.MAINSTREAM);
			NKCUtil.SetGameobjectActive(this.m_objSideStory, eCategory > NKCCollectionManager.COLLECTION_STORY_CATEGORY.MAINSTREAM);
			List<NKCUICollectionStory.StoryData> lstStoryData = SlotData.m_lstStoryData;
			int num = 0;
			for (int i = 0; i < lstStoryData.Count; i++)
			{
				NKCUICollectionStorySlot component = lstSlot[i].GetComponent<NKCUICollectionStorySlot>();
				if (null != component)
				{
					if (this.CheckActTitle(lstStoryData[i].m_ActID))
					{
						NKCUICollectionStorySubTitle component2 = lstSubTitle[num].GetComponent<NKCUICollectionStorySubTitle>();
						string title = string.Empty;
						if (eCategory == NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP)
						{
							NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(lstStoryData[i].m_UnlockInfo.reqValue);
							if (nkmdiveTemplet != null)
							{
								title = nkmdiveTemplet.Get_STAGE_NAME();
							}
						}
						else if (lstStoryData[i].m_ActID < 100)
						{
							title = string.Format(NKCUtilString.GET_STRING_COLLECTION_STORY_SUB_TITLE_ONE_PARAM, lstStoryData[i].m_ActID);
						}
						else
						{
							title = string.Format(NKCUtilString.GET_STRING_COLLECTION_STORY_EXTRA_TITLE_ONE_PARAM, lstStoryData[i].m_ActID % 100);
						}
						component2.SetTitle(title);
						lstSubTitle[num].SetParent(this.m_rtParent);
						lstSubTitle[num].GetComponent<RectTransform>().localScale = Vector3.one;
						NKCUtil.SetGameobjectActive(lstSubTitle[num].gameObject, !flag);
						this.m_lstRentalSubTitle.Add(lstSubTitle[num]);
						num++;
					}
					int actID = (lstStoryData[i].m_ActID < 100) ? lstStoryData[i].m_ActID : (lstStoryData[i].m_ActID % 100);
					this.SetSlotData(actID, lstStoryData[i].m_MissionIdx, component, lstStoryData[i].m_UnlockInfo, lstStoryData[i].m_bClear, lstStoryData[i].m_strBeforeCutScene, lstStoryData[i].m_strAfterCutScene);
					this.m_lstRentalSlot.Add(lstSlot[i]);
				}
			}
		}

		// Token: 0x06009067 RID: 36967 RVA: 0x00312D40 File Offset: 0x00310F40
		private void SetSlotData(int actID, int missionID, NKCUICollectionStorySlot slot, UnlockInfo unlockInfo, bool bClear, string ForceBeforeCutScene, string ForceAfterCutScene)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME();
			int reqValue = unlockInfo.reqValue;
			STAGE_UNLOCK_REQ_TYPE eReqType = unlockInfo.eReqType;
			if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE)
			{
				switch (eReqType)
				{
				case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON:
				{
					NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(reqValue);
					if (dungeonTempletBase != null)
					{
						slot.SetData(actID, missionID, dungeonTempletBase.StageTemplet.Key, dungeonTempletBase.GetDungeonName(), bClear, dungeonTempletBase.m_CutScenStrIDBefore, dungeonTempletBase.m_CutScenStrIDAfter, false);
						goto IL_1F9;
					}
					Debug.Log(string.Format("fail!!?!?!?{0}", eReqType));
					goto IL_1F9;
				}
				case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DIVE:
					break;
				case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE:
				{
					NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(reqValue);
					if (nkmphaseTemplet != null)
					{
						slot.SetData(actID, missionID, nkmphaseTemplet.StageTemplet.Key, nkmphaseTemplet.GetName(), bClear, nkmphaseTemplet.m_CutScenStrIDBefore, nkmphaseTemplet.m_CutScenStrIDAfter, false);
						goto IL_1F9;
					}
					Debug.Log(string.Format("fail!!?!?!?{0}", eReqType));
					goto IL_1F9;
				}
				case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_TRIM:
				{
					NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(reqValue);
					if (nkmtrimTemplet != null)
					{
						slot.SetData(actID, missionID, 0, string.Format("{0}-{1}", NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false), missionID), bClear, ForceBeforeCutScene, ForceAfterCutScene, true);
						goto IL_1F9;
					}
					Debug.Log(string.Format("fail!!?!?!?{0}", eReqType));
					goto IL_1F9;
				}
				default:
					if (eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_DIVE_HISTORY_CLEARED)
					{
						NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(reqValue);
						if (nkmdiveTemplet != null)
						{
							slot.SetData(actID, missionID, 0, string.Format("{0}-{1}", nkmdiveTemplet.Get_STAGE_NAME(), missionID), bClear, nkmdiveTemplet.GetCutsceneID(missionID), "", true);
							goto IL_1F9;
						}
						Debug.Log(string.Format("fail!!?!?!?{0}", eReqType));
						goto IL_1F9;
					}
					break;
				}
				Debug.LogError(string.Format("NKCUICollectionStoryContent::SetSlotData - Can not define unlock-reqType : {0}", eReqType));
			}
			else
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(reqValue);
				if (nkmwarfareTemplet != null)
				{
					slot.SetData(actID, missionID, nkmwarfareTemplet.StageTemplet.Key, nkmwarfareTemplet.GetWarfareName(), bClear, nkmwarfareTemplet.m_CutScenStrIDBefore, nkmwarfareTemplet.m_CutScenStrIDAfter, false);
				}
				else
				{
					Debug.Log(string.Format("fail!!?!?!?{0}", eReqType));
				}
			}
			IL_1F9:
			NKCUtil.SetGameobjectActive(slot.gameObject, true);
			slot.transform.SetParent(this.m_rtParent, false);
			slot.GetComponent<RectTransform>().localScale = Vector3.one;
		}

		// Token: 0x06009068 RID: 36968 RVA: 0x00312F74 File Offset: 0x00311174
		private bool CheckActTitle(int ActID)
		{
			return this.m_ActTitle.Add(ActID);
		}

		// Token: 0x04007D80 RID: 32128
		[Header("설정")]
		public GameObject m_objMainStream;

		// Token: 0x04007D81 RID: 32129
		public GameObject m_objSideStory;

		// Token: 0x04007D82 RID: 32130
		public Text m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_MAIN_01;

		// Token: 0x04007D83 RID: 32131
		public Text m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_MAIN_02;

		// Token: 0x04007D84 RID: 32132
		public Text m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_SIDE_01;

		// Token: 0x04007D85 RID: 32133
		public Text m_NKM_UI_COLLECTION_STORY_TITLE_TEXT_SIDE_02;

		// Token: 0x04007D86 RID: 32134
		public RectTransform m_rtParent;

		// Token: 0x04007D87 RID: 32135
		private const int ExtraActDivision = 100;

		// Token: 0x04007D88 RID: 32136
		private List<RectTransform> m_lstRentalSlot = new List<RectTransform>();

		// Token: 0x04007D89 RID: 32137
		private List<RectTransform> m_lstRentalSubTitle = new List<RectTransform>();

		// Token: 0x04007D8A RID: 32138
		private HashSet<int> m_ActTitle = new HashSet<int>();
	}
}
