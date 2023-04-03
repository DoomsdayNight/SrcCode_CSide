using System;
using System.Collections.Generic;
using ClientPacket.Warfare;
using Cs.Logging;
using DG.Tweening;
using NKC.UI.Shop;
using NKM;
using NKM.Event;
using NKM.Shop;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A03 RID: 2563
	public class NKCUIOperationNodeViewer : NKCUIBase
	{
		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06006FD2 RID: 28626 RVA: 0x0024F684 File Offset: 0x0024D884
		public static NKCUIOperationNodeViewer Instance
		{
			get
			{
				if (NKCUIOperationNodeViewer.m_Instance == null)
				{
					NKCUIOperationNodeViewer.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOperationNodeViewer>("AB_UI_OPERATION", "AB_UI_OPERATION_MAIN", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperationNodeViewer.CleanupInstance)).GetInstance<NKCUIOperationNodeViewer>();
					NKCUIOperationNodeViewer.m_Instance.InitUI();
				}
				return NKCUIOperationNodeViewer.m_Instance;
			}
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x0024F6D3 File Offset: 0x0024D8D3
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOperationNodeViewer.m_Instance != null && NKCUIOperationNodeViewer.m_Instance.IsOpen)
			{
				NKCUIOperationNodeViewer.m_Instance.Close();
			}
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x0024F6F8 File Offset: 0x0024D8F8
		private static void CleanupInstance()
		{
			NKCUIOperationNodeViewer.m_Instance = null;
		}

		// Token: 0x06006FD5 RID: 28629 RVA: 0x0024F700 File Offset: 0x0024D900
		public static bool isOpen()
		{
			return NKCUIOperationNodeViewer.m_Instance != null && NKCUIOperationNodeViewer.m_Instance.IsOpen;
		}

		// Token: 0x06006FD6 RID: 28630 RVA: 0x0024F71C File Offset: 0x0024D91C
		public void InitUI()
		{
			if (this.m_srAct != null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				this.m_srAct.dOnGetObject += this.GetObject;
				this.m_srAct.dOnReturnObject += this.ReturnObject;
				this.m_srAct.dOnProvideData += this.ProvideData;
				Canvas.ForceUpdateCanvases();
				this.m_srAct.PrepareCells(0);
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			if (this.m_btnMedal != null)
			{
				this.m_btnMedal.PointerClick.RemoveAllListeners();
				this.m_btnMedal.PointerClick.AddListener(new UnityAction(this.OnClickMedal));
			}
			if (this.m_btnEventMission != null)
			{
				this.m_btnEventMission.PointerClick.RemoveAllListeners();
				this.m_btnEventMission.PointerClick.AddListener(new UnityAction(this.OnClickEventMission));
			}
			if (this.m_btnDifficulty != null)
			{
				this.m_btnDifficulty.PointerClick.RemoveAllListeners();
				this.m_btnDifficulty.PointerClick.AddListener(new UnityAction(this.OnClickChangeDifficulty));
				this.m_btnDifficulty.m_bGetCallbackWhileLocked = true;
				this.m_btnDifficulty.m_HotkeyEventType = HotkeyEventType.NextTab;
			}
			if (this.m_btnShop != null)
			{
				this.m_btnShop.PointerClick.RemoveAllListeners();
				this.m_btnShop.PointerClick.AddListener(new UnityAction(this.OnClickShop));
			}
			if (this.m_StageInfo != null)
			{
				this.m_StageInfo.InitUI(new NKCUIStageInfo.OnButton(this.OnClickStart));
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnPointClick));
			EventTrigger eventTrigger = base.gameObject.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = base.gameObject.AddComponent<EventTrigger>();
			}
			eventTrigger.triggers.Add(entry);
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06006FD7 RID: 28631 RVA: 0x0024F926 File Offset: 0x0024DB26
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06006FD8 RID: 28632 RVA: 0x0024F929 File Offset: 0x0024DB29
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06006FD9 RID: 28633 RVA: 0x0024F930 File Offset: 0x0024DB30
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_EpisodeTemplet != null && this.m_EpisodeTemplet.ResourceIdList != null && this.m_EpisodeTemplet.ResourceIdList.Count > 0)
				{
					return this.m_EpisodeTemplet.ResourceIdList;
				}
				return base.UpsideMenuShowResourceList;
			}
		}

		// Token: 0x06006FDA RID: 28634 RVA: 0x0024F96C File Offset: 0x0024DB6C
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_CurStageViewer = null;
		}

		// Token: 0x06006FDB RID: 28635 RVA: 0x0024F984 File Offset: 0x0024DB84
		public override void OnBackButton()
		{
			if (this.m_StageInfo != null && this.m_StageInfo.gameObject.activeSelf)
			{
				this.m_StageInfo.Close();
				return;
			}
			NKCUIFadeInOut.FadeOut(this.m_FadeTime, delegate
			{
				base.OnBackButton();
			}, false, -1f);
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x0024F9DC File Offset: 0x0024DBDC
		public RectTransform GetObject(int idx)
		{
			NKCUIStageActSlot nkcuistageActSlot;
			if (this.m_stkActSlot.Count > 0)
			{
				nkcuistageActSlot = this.m_stkActSlot.Pop();
			}
			else
			{
				nkcuistageActSlot = UnityEngine.Object.Instantiate<NKCUIStageActSlot>(this.m_pfbActSlot, this.m_srAct.content);
			}
			return nkcuistageActSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x0024FA24 File Offset: 0x0024DC24
		public void ReturnObject(Transform tr)
		{
			NKCUIStageActSlot component = tr.GetComponent<NKCUIStageActSlot>();
			if (component == null)
			{
				return;
			}
			this.m_lstActSlot.Remove(component);
			component.ResetData();
			this.m_stkActSlot.Push(component);
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x06006FDE RID: 28638 RVA: 0x0024FA68 File Offset: 0x0024DC68
		public void ProvideData(Transform tr, int idx)
		{
			NKCUIStageActSlot slot = tr.GetComponent<NKCUIStageActSlot>();
			if (slot == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(slot, true);
			if (!this.m_bUseEpSlot)
			{
				slot.SetData(this.m_EpisodeTemplet.m_EpisodeID, this.m_EpisodeTemplet.m_Difficulty, this.m_lstStageViewerID[idx], this.GetActName(this.m_EpisodeTemplet, idx), this.m_tgAct, new NKCUIStageActSlot.OnSelectActSlot(this.OnSelectedActSlot));
			}
			else
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_lstStageViewerID[idx], EPISODE_DIFFICULTY.NORMAL);
				if (nkmepisodeTempletV != null)
				{
					slot.SetData(nkmepisodeTempletV.m_EpisodeID, nkmepisodeTempletV.m_Difficulty, this.m_lstStageViewerID[idx], nkmepisodeTempletV.GetEpisodeName(), this.m_tgAct, new NKCUIStageActSlot.OnSelectActSlot(this.OnSelectedActSlot));
				}
			}
			slot.SetSelected(slot.GetStageViewerID() == this.m_StageViewerNodeID);
			if (this.m_lstActSlot.Find((NKCUIStageActSlot x) => x.GetStageViewerID() == slot.GetStageViewerID()) == null)
			{
				this.m_lstActSlot.Add(slot);
			}
		}

		// Token: 0x06006FDF RID: 28639 RVA: 0x0024FB98 File Offset: 0x0024DD98
		public void Open(NKMEpisodeTempletV2 epTemplet)
		{
			if (epTemplet == null)
			{
				this.OnBackButton();
				return;
			}
			if (!string.IsNullOrEmpty(epTemplet.m_BG_Music))
			{
				NKCBGMInfoTemplet nkcbgminfoTemplet = NKCBGMInfoTemplet.Find(epTemplet.m_BG_Music);
				if (nkcbgminfoTemplet != null && !NKCSoundManager.IsSameMusic(nkcbgminfoTemplet.m_BgmAssetID))
				{
					NKCSoundManager.PlayMusic(nkcbgminfoTemplet.m_BgmAssetID, false, 1f, false, 0f, 0f);
				}
			}
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().PlayByFavorite = false;
			this.m_ReservedStageTemplet = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedStageTemplet();
			if (this.m_ReservedStageTemplet != null)
			{
				epTemplet = this.m_ReservedStageTemplet.EpisodeTemplet;
			}
			this.m_bUseEpSlot = epTemplet.UseEpSlot();
			this.m_lstActSlot.Clear();
			if (this.m_StageInfo.IsOpened())
			{
				this.m_StageInfo.Close();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetData(epTemplet.m_EpisodeID, epTemplet.m_Difficulty);
			base.UIOpened(true);
			NKCUIFadeInOut.FadeIn(0.3f, null, false);
			if (this.m_ReservedStageTemplet != null)
			{
				this.SetScrollToIndex(this.m_ReservedStageTemplet.m_StageIndex - 1);
				this.m_StageInfo.Open(this.m_ReservedStageTemplet);
				this.m_ReservedStageTemplet = null;
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedStage(null);
			}
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeTemplet(null);
			NKCContentManager.ShowContentUnlockPopup(delegate
			{
				this.TutorialCheck();
			}, Array.Empty<STAGE_UNLOCK_REQ_TYPE>());
		}

		// Token: 0x06006FE0 RID: 28640 RVA: 0x0024FCF8 File Offset: 0x0024DEF8
		public void SetData(int episodeID, EPISODE_DIFFICULTY difficulty)
		{
			this.m_EpisodeTemplet = NKMEpisodeTempletV2.Find(episodeID, difficulty);
			if (this.m_EpisodeTemplet == null)
			{
				Log.Error(string.Format("EpisodeTemplet is null - {0} : {1}", episodeID, difficulty), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIOperationNodeViewer.cs", 319);
				base.Close();
				return;
			}
			this.m_lstStageViewerID.Clear();
			if (!this.m_bUseEpSlot)
			{
				foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in this.m_EpisodeTemplet.m_DicStage)
				{
					if (keyValuePair.Value.Count > 0)
					{
						this.m_lstStageViewerID.Add(keyValuePair.Value[0].ActId);
					}
				}
				this.m_lstStageViewerID.Sort();
			}
			else
			{
				NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(this.m_EpisodeTemplet.m_GroupID);
				for (int i = 0; i < nkmepisodeGroupTemplet.lstEpisodeTemplet.Count; i++)
				{
					this.m_lstStageViewerID.Add(nkmepisodeGroupTemplet.lstEpisodeTemplet[i].m_EpisodeID);
				}
			}
			if (this.m_lstStageViewerID.Count <= 0)
			{
				Log.Error(string.Format("Ȱ��ȭ�� ���������� ���� - {0}", this.m_EpisodeTemplet.m_EpisodeID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIOperationNodeViewer.cs", 347);
				this.OnBackButton();
				return;
			}
			if (this.m_ReservedStageTemplet != null)
			{
				if (!this.m_bUseEpSlot)
				{
					this.m_StageViewerNodeID = this.m_ReservedStageTemplet.ActId;
				}
				else
				{
					this.m_StageViewerNodeID = this.m_ReservedStageTemplet.EpisodeId;
				}
			}
			else
			{
				this.m_StageViewerNodeID = this.GetLastNodeID();
			}
			this.m_srAct.TotalCount = this.m_lstStageViewerID.Count;
			if (this.m_srAct.TotalCount > 1)
			{
				NKCUtil.SetGameobjectActive(this.m_objBottom, true);
				NKCUtil.SetGameobjectActive(this.m_srAct, true);
				this.m_srAct.RefreshCells(false);
				int index = this.m_lstStageViewerID.FindIndex((int x) => x == this.m_StageViewerNodeID);
				this.m_srAct.ScrollToCell(index, 0.1f, LoopScrollRect.ScrollTarget.Top, null);
			}
			this.m_bFirstSetting = true;
			NKCUtil.SetGameobjectActive(this.m_objDifficultyNormal, this.m_EpisodeTemplet.m_Difficulty == EPISODE_DIFFICULTY.NORMAL);
			NKCUtil.SetGameobjectActive(this.m_objDifficultyHard, this.m_EpisodeTemplet.m_Difficulty == EPISODE_DIFFICULTY.HARD);
			this.OnSelectedActSlot(this.m_StageViewerNodeID);
		}

		// Token: 0x06006FE1 RID: 28641 RVA: 0x0024FF54 File Offset: 0x0024E154
		private int GetLastNodeID()
		{
			if (this.m_bUseEpSlot)
			{
				if (this.m_ReservedStageTemplet != null)
				{
					return this.m_ReservedStageTemplet.EpisodeId;
				}
				return this.m_EpisodeTemplet.m_EpisodeID;
			}
			else
			{
				if (this.m_ReservedStageTemplet != null)
				{
					return this.m_ReservedStageTemplet.ActId;
				}
				for (int i = this.m_lstStageViewerID.Count - 1; i >= 0; i--)
				{
					if (NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), this.m_EpisodeTemplet.GetFirstStage(this.m_lstStageViewerID[i]).m_UnlockInfo, false))
					{
						return this.m_lstStageViewerID[i];
					}
				}
				return this.m_lstStageViewerID[0];
			}
		}

		// Token: 0x06006FE2 RID: 28642 RVA: 0x0024FFF8 File Offset: 0x0024E1F8
		private void OnSelectedActSlot(int stageViewerNodeID)
		{
			this.m_StageViewerNodeID = stageViewerNodeID;
			if (this.m_bUseEpSlot)
			{
				this.m_EpisodeTemplet = NKMEpisodeTempletV2.Find(this.m_StageViewerNodeID, EPISODE_DIFFICULTY.NORMAL);
				int firstStageID = NKCContentManager.GetFirstStageID(this.m_EpisodeTemplet, 1, this.m_EpisodeTemplet.m_Difficulty);
				if (this.m_dicUnlockEffectGo.ContainsKey(firstStageID))
				{
					UnityEngine.Object.Destroy(this.m_dicUnlockEffectGo[firstStageID]);
					this.m_dicUnlockEffectGo.Remove(firstStageID);
				}
				NKCContentManager.RemoveUnlockedContent(ContentsType.ACT, firstStageID, true);
			}
			else
			{
				int firstStageID2 = NKCContentManager.GetFirstStageID(this.m_EpisodeTemplet, stageViewerNodeID, this.m_EpisodeTemplet.m_Difficulty);
				if (this.m_dicUnlockEffectGo.ContainsKey(firstStageID2))
				{
					UnityEngine.Object.Destroy(this.m_dicUnlockEffectGo[firstStageID2]);
					this.m_dicUnlockEffectGo.Remove(firstStageID2);
				}
				NKCContentManager.RemoveUnlockedContent(ContentsType.ACT, firstStageID2, true);
			}
			foreach (KeyValuePair<int, INKCUIStageViewer> keyValuePair in this.m_dicStageViewer)
			{
				if (keyValuePair.Value is NKCUIStageViewer)
				{
					NKCUtil.SetGameobjectActive(keyValuePair.Value as NKCUIStageViewer, false);
				}
				else if (keyValuePair.Value is NKCUIStageViewerV2)
				{
					NKCUtil.SetGameobjectActive(keyValuePair.Value as NKCUIStageViewerV2, false);
				}
			}
			if (this.m_StageInfo.IsOpened())
			{
				this.m_StageInfo.Close();
			}
			string stage_Viewer_Prefab = this.m_EpisodeTemplet.m_Stage_Viewer_Prefab;
			int num;
			int count;
			int actID;
			if (!this.m_bUseEpSlot)
			{
				num = this.m_EpisodeTemplet.m_EpisodeID;
				count = this.m_EpisodeTemplet.m_DicStage.Count;
				actID = stageViewerNodeID;
			}
			else
			{
				NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(this.m_EpisodeTemplet.m_GroupID);
				num = nkmepisodeGroupTemplet.EpisodeGroupID;
				count = nkmepisodeGroupTemplet.lstEpisodeTemplet.Count;
				actID = this.m_EpisodeTemplet.m_EpisodeID;
			}
			if (!this.m_dicStageViewer.ContainsKey(num))
			{
				GameObject orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<GameObject>(stage_Viewer_Prefab, stage_Viewer_Prefab, true);
				if (orLoadAssetResource == null)
				{
					Log.Error(string.Format("�̺�Ʈ ���� ����Ʈ �ε忡 ���� : {0}", num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIOperationNodeViewer.cs", 478);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
					return;
				}
				INKCUIStageViewer component = UnityEngine.Object.Instantiate<GameObject>(orLoadAssetResource).GetComponent<INKCUIStageViewer>();
				if (component != null)
				{
					component.ResetPosition(this.m_trNodeStageParent);
					this.m_dicStageViewer.Add(num, component);
				}
			}
			if (this.m_dicStageViewer[num].GetActCount(this.m_EpisodeTemplet.m_Difficulty) != count)
			{
				Log.Error(string.Format("ACt ���ڰ� �����հ� ���� ���� - EpisodeID : {0}", num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIOperationNodeViewer.cs", 495);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			foreach (KeyValuePair<int, INKCUIStageViewer> keyValuePair2 in this.m_dicStageViewer)
			{
				if (keyValuePair2.Key == num)
				{
					keyValuePair2.Value.SetActive(true);
					this.m_CurStageViewer = keyValuePair2.Value;
					Vector2 vector = Vector2.zero;
					if (this.m_bUseEpSlot)
					{
						vector = keyValuePair2.Value.SetData(this.m_bUseEpSlot, num, stageViewerNodeID, this.m_EpisodeTemplet.m_Difficulty, new IDungeonSlot.OnSelectedItemSlot(this.OnSelecteNode), this.m_EpisodeTemplet.m_ScrollType);
					}
					else
					{
						vector = keyValuePair2.Value.SetData(this.m_bUseEpSlot, num, actID, this.m_EpisodeTemplet.m_Difficulty, new IDungeonSlot.OnSelectedItemSlot(this.OnSelecteNode), this.m_EpisodeTemplet.m_ScrollType);
					}
					if (!this.m_EpisodeTemplet.m_bHideActTab && (this.m_EpisodeTemplet.m_EPCategory != EPISODE_CATEGORY.EC_CHALLENGE || this.m_EpisodeTemplet.m_DicStage.Count > 1))
					{
						NKCUtil.SetGameobjectActive(this.m_srAct, true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_srAct, false);
					}
					LayoutRebuilder.ForceRebuildLayoutImmediate(this.m_srContent.content);
					if (this.m_srContent.horizontal)
					{
						this.m_srContent.horizontalNormalizedPosition = vector.x;
					}
					if (this.m_srContent.vertical)
					{
						this.m_srContent.verticalNormalizedPosition = vector.y;
					}
					NKCUtil.SetGameobjectActive(this.m_btnDifficulty, NKMEpisodeMgr.HasHardDifficulty(this.m_EpisodeTemplet.m_EpisodeID));
					if (!this.m_btnDifficulty.gameObject.activeSelf)
					{
						break;
					}
					if (NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), this.m_EpisodeTemplet.m_EpisodeID, (this.m_EpisodeTemplet.m_Difficulty == EPISODE_DIFFICULTY.NORMAL) ? EPISODE_DIFFICULTY.HARD : EPISODE_DIFFICULTY.NORMAL))
					{
						this.m_btnDifficulty.UnLock(false);
						break;
					}
					this.m_btnDifficulty.Lock(false);
					break;
				}
			}
			this.SetActInfo();
			int indexPosition = 0;
			for (int i = 0; i < this.m_lstActSlot.Count; i++)
			{
				indexPosition = i;
			}
			if (this.m_bFirstSetting)
			{
				this.m_srAct.SetIndexPosition(indexPosition);
			}
			this.m_bFirstSetting = false;
		}

		// Token: 0x06006FE3 RID: 28643 RVA: 0x002504E4 File Offset: 0x0024E6E4
		private void SetActInfo()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMStageTempletV2 firstStage = this.m_EpisodeTemplet.GetFirstStage(1);
			if (firstStage != null && !string.IsNullOrEmpty(firstStage.m_ACT_BG_Image))
			{
				NKCUtil.SetImageSprite(this.m_imgBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Bg", firstStage.m_ACT_BG_Image, false), false);
			}
			NKCUtil.SetLabelText(this.m_lbEpTitle, this.m_EpisodeTemplet.GetEpisodeTitle());
			NKCUtil.SetLabelText(this.m_lbEpSubTitle, this.m_EpisodeTemplet.GetEpisodeName());
			if (this.m_lbEpTitle != null)
			{
				if (this.m_EpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
				{
					if (this.m_fontRajhani != null)
					{
						this.m_lbEpTitle.font = this.m_fontRajhani;
						this.m_lbEpTitle.fontSize = (float)this.m_fontRajhaniSize;
					}
				}
				else if (this.m_fontNormal != null)
				{
					this.m_lbEpTitle.font = this.m_fontNormal;
					this.m_lbEpTitle.fontSize = (float)this.m_fontNormalSize;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_btnMedal, this.m_EpisodeTemplet.HasCompletionReward);
			if (this.m_EpisodeTemplet.HasCompletionReward)
			{
				NKCUtil.SetLabelText(this.m_lbMedalCount, string.Format("{0}/{1}", NKMEpisodeMgr.GetEPProgressClearCount(NKCScenManager.CurrentUserData(), this.m_EpisodeTemplet), NKMEpisodeMgr.GetTotalMedalCount(this.m_EpisodeTemplet)));
				bool bValue = NKMEpisodeMgr.CanGetEpisodeCompleteReward(NKCScenManager.CurrentUserData(), this.m_EpisodeTemplet.m_EpisodeID) == NKM_ERROR_CODE.NEC_OK;
				NKCUtil.SetGameobjectActive(this.m_objMedalReddot, bValue);
			}
			bool flag = this.m_EpisodeTemplet.m_ButtonShortCutType > NKM_SHORTCUT_TYPE.SHORTCUT_NONE;
			if (flag)
			{
				NKMEventTabTemplet nkmeventTabTemplet = null;
				int eventId;
				if (int.TryParse(this.m_EpisodeTemplet.m_ButtonShortCutParam, out eventId))
				{
					nkmeventTabTemplet = NKMEventTabTemplet.Find(eventId);
				}
				flag &= (nkmeventTabTemplet != null && nkmeventTabTemplet.IsAvailable);
			}
			NKCUtil.SetGameobjectActive(this.m_btnEventMission, flag);
			if (this.m_btnEventMission != null && this.m_btnEventMission.gameObject.activeSelf)
			{
				int nkm_MISSION_TAB_ID;
				int.TryParse(this.m_EpisodeTemplet.m_ButtonShortCutParam, out nkm_MISSION_TAB_ID);
				NKCUtil.SetGameobjectActive(this.m_objEventMissionReddot, nkmuserData.m_MissionData.CheckCompletableMission(nkmuserData, nkm_MISSION_TAB_ID, false));
			}
			if (!this.m_bUseEpSlot)
			{
				NKMStageTempletV2 firstStage2 = this.m_EpisodeTemplet.GetFirstStage(this.m_StageViewerNodeID);
				NKCUtil.SetGameobjectActive(this.m_btnShop, firstStage2.m_ShopShortcut != "TAB_NONE");
				if (firstStage2.m_ShopShortcut != "TAB_NONE")
				{
					NKCUtil.SetLabelText(this.m_lbShopCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(firstStage2.m_ShopShortcutResourceID).ToString());
					NKCUtil.SetImageSprite(this.m_imgShopIcon, NKCResourceUtility.GetOrLoadMiscItemIcon(firstStage2.m_ShopShortcutResourceID), false);
					if (!string.IsNullOrEmpty(firstStage2.m_ShopShortcutBgName))
					{
						NKCUtil.SetImageSprite(this.m_imgShopBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("", firstStage2.m_ShopShortcutBgName, false), false);
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_btnShop, false);
			}
			if (this.m_btnShop != null && this.m_btnDifficulty != null && this.m_srAct != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objBottom, this.m_btnShop.gameObject.activeSelf || this.m_btnDifficulty.gameObject.activeSelf || this.m_srAct.gameObject.activeSelf);
			}
			base.UpdateUpsideMenu();
		}

		// Token: 0x06006FE4 RID: 28644 RVA: 0x00250835 File Offset: 0x0024EA35
		public void RefreshFavoriteInfo()
		{
			if (this.m_StageInfo.gameObject.activeSelf)
			{
				this.m_StageInfo.RefreshFavoriteState();
			}
		}

		// Token: 0x06006FE5 RID: 28645 RVA: 0x00250854 File Offset: 0x0024EA54
		public void Refresh()
		{
			this.RefreshFavoriteInfo();
			this.SetActInfo();
			this.m_srAct.RefreshCells(false);
			this.m_CurStageViewer.RefreshData();
			if (this.m_NKCPopupAchieveRateReward != null)
			{
				this.NKCPopupAchieveRateReward.ResetUI();
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06006FE6 RID: 28646 RVA: 0x00250894 File Offset: 0x0024EA94
		private NKCPopupAchieveRateReward NKCPopupAchieveRateReward
		{
			get
			{
				if (this.m_NKCPopupAchieveRateReward == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupAchieveRateReward>("AB_UI_NKM_UI_OPERATION", "NKM_UI_OPERATION_POPUP_MEDAL", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupAchieveRateReward = loadedUIData.GetInstance<NKCPopupAchieveRateReward>();
					NKCPopupAchieveRateReward nkcpopupAchieveRateReward = this.m_NKCPopupAchieveRateReward;
					if (nkcpopupAchieveRateReward != null)
					{
						nkcpopupAchieveRateReward.InitUI();
					}
				}
				return this.m_NKCPopupAchieveRateReward;
			}
		}

		// Token: 0x06006FE7 RID: 28647 RVA: 0x002508E9 File Offset: 0x0024EAE9
		private void OnClickMedal()
		{
			if (this.NKCPopupAchieveRateReward != null)
			{
				this.NKCPopupAchieveRateReward.Open(this.m_EpisodeTemplet);
			}
		}

		// Token: 0x06006FE8 RID: 28648 RVA: 0x0025090A File Offset: 0x0024EB0A
		private void OnClickEventMission()
		{
			NKCContentManager.MoveToShortCut(this.m_EpisodeTemplet.m_ButtonShortCutType, this.m_EpisodeTemplet.m_ButtonShortCutParam, false);
		}

		// Token: 0x06006FE9 RID: 28649 RVA: 0x00250928 File Offset: 0x0024EB28
		private void OnClickChangeDifficulty()
		{
			EPISODE_DIFFICULTY difficulty = (this.m_EpisodeTemplet.m_Difficulty == EPISODE_DIFFICULTY.NORMAL) ? EPISODE_DIFFICULTY.HARD : EPISODE_DIFFICULTY.NORMAL;
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeTemplet.m_EpisodeID, difficulty);
			if (this.m_btnDifficulty.m_bLock)
			{
				NKMStageTempletV2 firstStage = nkmepisodeTempletV.GetFirstStage(1);
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GetUnlockConditionRequireDesc(firstStage, false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (nkmepisodeTempletV != null)
			{
				this.SetData(nkmepisodeTempletV.m_EpisodeID, nkmepisodeTempletV.m_Difficulty);
			}
		}

		// Token: 0x06006FEA RID: 28650 RVA: 0x002509A4 File Offset: 0x0024EBA4
		private void OnClickShop()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.LOBBY_SUBMENU, 0);
				return;
			}
			int curActID = this.m_CurStageViewer.GetCurActID();
			NKMStageTempletV2 firstStage = this.m_EpisodeTemplet.GetFirstStage(curActID);
			if (firstStage == null)
			{
				return;
			}
			if (firstStage.m_ShopShortcut == "TAB_NONE")
			{
				return;
			}
			if (!string.IsNullOrEmpty(firstStage.m_ShopShortcut))
			{
				string[] array = firstStage.m_ShopShortcut.Split(new char[]
				{
					',',
					' ',
					'@'
				});
				if (array.Length != 0)
				{
					int subTabIndex = 0;
					int num = 0;
					string selectedTab = array[0];
					if (array.Length > 1)
					{
						int.TryParse(array[1], out subTabIndex);
					}
					if (array.Length > 2)
					{
						int.TryParse(array[2], out num);
					}
					NKMAssetName cNKMAssetName = new NKMAssetName("AB_UI_OPERATION_Bg", firstStage.m_ACT_BG_Image);
					this.OperationShop.Open(firstStage.EpisodeTemplet.GetEpisodeTitle(), firstStage.EpisodeTemplet.GetEpisodeName(), firstStage.m_ShopShortcutResourceID, cNKMAssetName, NKCShopManager.ShopTabCategory.NONE, selectedTab, subTabIndex);
				}
			}
		}

		// Token: 0x06006FEB RID: 28651 RVA: 0x00250A90 File Offset: 0x0024EC90
		private void OnSelecteNode(int dunIndex, string dunStrID, bool isPlaying)
		{
			NKMStageTempletV2 nkmstageTempletV;
			int key;
			if (!this.m_EpisodeTemplet.UseEpSlot())
			{
				if (!this.m_EpisodeTemplet.m_DicStage.ContainsKey(this.m_StageViewerNodeID))
				{
					return;
				}
				if (this.m_EpisodeTemplet.m_DicStage[this.m_StageViewerNodeID].Count <= dunIndex - 1)
				{
					return;
				}
				nkmstageTempletV = this.m_EpisodeTemplet.m_DicStage[this.m_StageViewerNodeID][dunIndex - 1];
				key = nkmstageTempletV.EpisodeId;
			}
			else
			{
				if (this.m_EpisodeTemplet.m_DicStage[1].Count <= dunIndex - 1)
				{
					return;
				}
				nkmstageTempletV = this.m_EpisodeTemplet.m_DicStage[1][dunIndex - 1];
				key = this.m_EpisodeTemplet.m_GroupID;
			}
			if (nkmstageTempletV == null)
			{
				return;
			}
			if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkmstageTempletV.m_UnlockInfo, false))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GetUnlockConditionRequireDesc(nkmstageTempletV, false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			this.m_dicStageViewer[key].SetSelectNode(nkmstageTempletV);
			this.m_StageInfo.Open(nkmstageTempletV);
			this.SetScrollToIndex(dunIndex - 1);
		}

		// Token: 0x06006FEC RID: 28652 RVA: 0x00250BB0 File Offset: 0x0024EDB0
		private void SetScrollToIndex(int targetIndex)
		{
			float targetNormalizedPos = this.m_CurStageViewer.GetTargetNormalizedPos(targetIndex);
			this.m_srContent.DOKill(false);
			this.m_srContent.DOHorizontalNormalizedPos(targetNormalizedPos, 0.2f, false).SetEase(Ease.OutQuint);
		}

		// Token: 0x06006FED RID: 28653 RVA: 0x00250BF4 File Offset: 0x0024EDF4
		private void OnClickStart(NKMStageTempletV2 stageTemplet, bool bSkip, int skipCount)
		{
			if (stageTemplet == null)
			{
				return;
			}
			NKMEpisodeTempletV2 episodeTemplet = stageTemplet.EpisodeTemplet;
			if (episodeTemplet == null)
			{
				return;
			}
			if (!episodeTemplet.IsOpen)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCUtil.CheckCommonStartCond(myUserData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCUtil.OnExpandInventoryPopup(nkm_ERROR_CODE);
				return;
			}
			if (bSkip)
			{
				if (stageTemplet.DungeonTempletBase == null)
				{
					return;
				}
				if (!myUserData.CheckStageCleared(stageTemplet))
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_STAGE", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					return;
				}
				List<long> list = new List<long>();
				if (stageTemplet.IsUsingEventDeck())
				{
					NKMEventDeckData nkmeventDeckData = NKMDungeonManager.LoadDungeonDeck(stageTemplet);
					if (nkmeventDeckData == null)
					{
						NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_DP_DIVE_NO_SELECT_DECK", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
						return;
					}
					list.AddRange(nkmeventDeckData.m_dicUnit.Values);
				}
				else
				{
					NKMDeckIndex selectDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DAILY, 0);
					NKM_ERROR_CODE nkm_ERROR_CODE2 = NKMMain.IsValidDeck(myUserData.m_ArmyData, selectDeckIndex);
					if (nkm_ERROR_CODE2 != NKM_ERROR_CODE.NEC_OK)
					{
						NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString(nkm_ERROR_CODE2), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
						return;
					}
					myUserData.m_ArmyData.GetDeckList(NKM_DECK_TYPE.NDT_DAILY, 0, ref list);
				}
				NKCPacketSender.Send_NKMPacket_DUNGEON_SKIP_REQ(stageTemplet.DungeonTempletBase.m_DungeonID, list, skipCount);
				return;
			}
			else
			{
				if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
				{
					bool flag = true;
					if (stageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_WARFARE)
					{
						WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
						if (warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP && warfareGameData.warfareTempletID == stageTemplet.WarfareTemplet.Key)
						{
							flag = false;
						}
					}
					if (flag && (long)stageTemplet.m_StageReqItemCount - myUserData.m_InventoryData.GetCountMiscItem(stageTemplet.m_StageReqItemID) > 0L)
					{
						int dailyMissionTicketShopID = NKCShopManager.GetDailyMissionTicketShopID(this.m_EpisodeTemplet.m_EpisodeID);
						if (NKCShopManager.GetBuyCountLeft(dailyMissionTicketShopID) > 0)
						{
							NKCShopManager.OnBtnProductBuy(ShopItemTemplet.Find(dailyMissionTicketShopID).Key, false);
							return;
						}
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
				}
				if (stageTemplet.EnterLimit > 0)
				{
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					if (nkmuserData.IsHaveStatePlayData(stageTemplet.Key) && nkmuserData.GetStatePlayCnt(stageTemplet.Key, false, false, false) >= stageTemplet.EnterLimit)
					{
						if (nkmuserData.GetStageRestoreCnt(stageTemplet.Key) >= stageTemplet.RestoreLimit)
						{
							NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, null, "");
							return;
						}
						NKCPopupResourceWithdraw.Instance.OpenForRestoreEnterLimit(stageTemplet, delegate
						{
							NKCPacketSender.Send_NKMPacket_RESET_STAGE_PLAY_COUNT_REQ(stageTemplet.Key);
						}, nkmuserData.GetStageRestoreCnt(stageTemplet.Key));
						return;
					}
				}
				if (!NKMEpisodeMgr.HasEnoughResource(stageTemplet, 1))
				{
					return;
				}
				this.m_StageInfo.Close();
				STAGE_TYPE stage_TYPE = stageTemplet.m_STAGE_TYPE;
				if (stage_TYPE != STAGE_TYPE.ST_DUNGEON)
				{
					if (stage_TYPE != STAGE_TYPE.ST_PHASE)
					{
						return;
					}
					NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(stageTemplet, DeckContents.PHASE);
					if (stageTemplet.PhaseTemplet == null)
					{
						return;
					}
					if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
					{
						NKCUIOperationIntro.Instance.Open(stageTemplet, delegate
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
						});
						return;
					}
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
					return;
				}
				else
				{
					NKMDungeonTempletBase cNKMDungeonTempletBase = stageTemplet.DungeonTempletBase;
					if (cNKMDungeonTempletBase == null)
					{
						return;
					}
					NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(stageTemplet, DeckContents.NORMAL);
					NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedDungeonType(cNKMDungeonTempletBase.m_DungeonID);
					if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
					{
						NKCUIOperationIntro.Instance.Open(stageTemplet, delegate
						{
							NKCScenManager.GetScenManager().ScenChangeFade(this.Get_Next_NKM_SCEN_ID_By_DT(cNKMDungeonTempletBase.m_DungeonType), true);
						});
						return;
					}
					NKCScenManager.GetScenManager().ScenChangeFade(this.Get_Next_NKM_SCEN_ID_By_DT(cNKMDungeonTempletBase.m_DungeonType), true);
					return;
				}
			}
		}

		// Token: 0x06006FEE RID: 28654 RVA: 0x00251053 File Offset: 0x0024F253
		public void OnPointDown(BaseEventData eventData)
		{
			this.m_vDownPos = ((PointerEventData)eventData).position;
		}

		// Token: 0x06006FEF RID: 28655 RVA: 0x00251068 File Offset: 0x0024F268
		private void OnPointUp(BaseEventData eventData)
		{
			if (Vector2.Distance(this.m_vDownPos, ((PointerEventData)eventData).position) < this.m_fDragCheckDistance)
			{
				if (this.m_CurStageViewer != null)
				{
					this.m_CurStageViewer.SetSelectNode(null);
				}
				if (this.m_StageInfo.gameObject.activeSelf)
				{
					this.m_StageInfo.Close();
				}
			}
		}

		// Token: 0x06006FF0 RID: 28656 RVA: 0x002510C4 File Offset: 0x0024F2C4
		private void OnPointClick(BaseEventData eventData)
		{
			if (this.m_CurStageViewer != null)
			{
				this.m_CurStageViewer.SetSelectNode(null);
			}
			if (this.m_StageInfo.gameObject.activeSelf)
			{
				this.m_StageInfo.Close();
			}
		}

		// Token: 0x06006FF1 RID: 28657 RVA: 0x002510F7 File Offset: 0x0024F2F7
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey != HotkeyEventType.Up)
			{
				return hotkey == HotkeyEventType.Down && this.MoveTab(1);
			}
			return this.MoveTab(-1);
		}

		// Token: 0x06006FF2 RID: 28658 RVA: 0x00251114 File Offset: 0x0024F314
		private bool MoveTab(int moveCount)
		{
			int num = this.m_lstStageViewerID.FindIndex((int x) => x == this.m_StageViewerNodeID);
			if (num >= 0)
			{
				int index = (num + moveCount + this.m_lstStageViewerID.Count) % this.m_lstStageViewerID.Count;
				for (int i = 0; i < this.m_lstActSlot.Count; i++)
				{
					if (this.m_lstActSlot[i] != null)
					{
						bool flag = this.m_lstActSlot[i].GetStageViewerID() == this.m_lstStageViewerID[index];
						if (flag && this.m_lstActSlot[i].IsLocked())
						{
							if (moveCount > 0)
							{
								this.MoveTab(moveCount + 1);
							}
							else
							{
								this.MoveTab(moveCount - 1);
							}
							return false;
						}
						this.m_lstActSlot[i].SetSelected(flag);
					}
				}
				this.OnSelectedActSlot(this.m_lstStageViewerID[index]);
				return true;
			}
			return false;
		}

		// Token: 0x06006FF3 RID: 28659 RVA: 0x00251202 File Offset: 0x0024F402
		private NKM_SCEN_ID Get_Next_NKM_SCEN_ID_By_DT(NKM_DUNGEON_TYPE eNKM_DUNGEON_TYPE)
		{
			if (eNKM_DUNGEON_TYPE == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
			{
				return NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON;
			}
			return NKM_SCEN_ID.NSI_DUNGEON_ATK_READY;
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06006FF4 RID: 28660 RVA: 0x0025120D File Offset: 0x0024F40D
		private NKCUIShopSingleTab OperationShop
		{
			get
			{
				if (this.m_OperationShop == null)
				{
					this.m_OperationShop = NKCUIShopSingleTab.GetInstance("AB_UI_NKM_UI_SHOP", "NKM_UI_OPERATION_SHOP");
				}
				return this.m_OperationShop;
			}
		}

		// Token: 0x06006FF5 RID: 28661 RVA: 0x00251238 File Offset: 0x0024F438
		private string GetActName(NKMEpisodeTempletV2 epTemplet, int idx)
		{
			if (epTemplet.UseEpSlot())
			{
				return epTemplet.GetEpisodeName();
			}
			if (this.m_lstStageViewerID.Count > idx && epTemplet.m_DicStage.ContainsKey(this.m_lstStageViewerID[idx]) && epTemplet.m_DicStage[this.m_lstStageViewerID[idx]].Count > 0)
			{
				return string.Format("{0} {1}", NKCStringTable.GetString("SI_PF_ACT", false), this.m_lstStageViewerID[idx]);
			}
			return "";
		}

		// Token: 0x06006FF6 RID: 28662 RVA: 0x002512C6 File Offset: 0x0024F4C6
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Episode, true);
		}

		// Token: 0x06006FF7 RID: 28663 RVA: 0x002512D1 File Offset: 0x0024F4D1
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			base.OnInventoryChange(itemData);
			if (this.m_StageInfo.IsOpened())
			{
				this.m_StageInfo.RefreshUI();
			}
		}

		// Token: 0x04005B69 RID: 23401
		private const string ASSET_BUNDLE_NAME = "AB_UI_OPERATION";

		// Token: 0x04005B6A RID: 23402
		private const string UI_ASSET_NAME = "AB_UI_OPERATION_MAIN";

		// Token: 0x04005B6B RID: 23403
		private static NKCUIOperationNodeViewer m_Instance;

		// Token: 0x04005B6C RID: 23404
		private Dictionary<int, INKCUIStageViewer> m_dicStageViewer = new Dictionary<int, INKCUIStageViewer>();

		// Token: 0x04005B6D RID: 23405
		private INKCUIStageViewer m_CurStageViewer;

		// Token: 0x04005B6E RID: 23406
		[Header("���")]
		public TMP_FontAsset m_fontRajhani;

		// Token: 0x04005B6F RID: 23407
		public int m_fontRajhaniSize;

		// Token: 0x04005B70 RID: 23408
		public TMP_FontAsset m_fontNormal;

		// Token: 0x04005B71 RID: 23409
		public int m_fontNormalSize;

		// Token: 0x04005B72 RID: 23410
		public TMP_Text m_lbEpTitle;

		// Token: 0x04005B73 RID: 23411
		public TMP_Text m_lbEpSubTitle;

		// Token: 0x04005B74 RID: 23412
		public NKCUIComStateButton m_btnMedal;

		// Token: 0x04005B75 RID: 23413
		public Text m_lbMedalCount;

		// Token: 0x04005B76 RID: 23414
		public GameObject m_objMedalReddot;

		// Token: 0x04005B77 RID: 23415
		public NKCUIComStateButton m_btnEventMission;

		// Token: 0x04005B78 RID: 23416
		public GameObject m_objEventMissionReddot;

		// Token: 0x04005B79 RID: 23417
		[Header("�߾� ����")]
		public Image m_imgBG;

		// Token: 0x04005B7A RID: 23418
		public ScrollRect m_srContent;

		// Token: 0x04005B7B RID: 23419
		public Transform m_trNodeStageParent;

		// Token: 0x04005B7C RID: 23420
		[Header("���� �������� ����")]
		public Animator m_AniRightSide;

		// Token: 0x04005B7D RID: 23421
		public NKCUIStageInfo m_StageInfo;

		// Token: 0x04005B7E RID: 23422
		[Header("�ϴ� �޴�")]
		public GameObject m_objBottom;

		// Token: 0x04005B7F RID: 23423
		public NKCUIComStateButton m_btnDifficulty;

		// Token: 0x04005B80 RID: 23424
		public GameObject m_objDifficultyNormal;

		// Token: 0x04005B81 RID: 23425
		public GameObject m_objDifficultyHard;

		// Token: 0x04005B82 RID: 23426
		public LoopScrollRect m_srAct;

		// Token: 0x04005B83 RID: 23427
		public NKCUIComToggleGroup m_tgAct;

		// Token: 0x04005B84 RID: 23428
		public NKCUIStageActSlot m_pfbActSlot;

		// Token: 0x04005B85 RID: 23429
		public NKCUIComStateButton m_btnShop;

		// Token: 0x04005B86 RID: 23430
		public Image m_imgShopIcon;

		// Token: 0x04005B87 RID: 23431
		public TMP_Text m_lbShopCount;

		// Token: 0x04005B88 RID: 23432
		public Image m_imgShopBG;

		// Token: 0x04005B89 RID: 23433
		[Header("���̵� �ð�")]
		public float m_FadeTime = 0.3f;

		// Token: 0x04005B8A RID: 23434
		private Stack<NKCUIStageActSlot> m_stkActSlot = new Stack<NKCUIStageActSlot>();

		// Token: 0x04005B8B RID: 23435
		private List<NKCUIStageActSlot> m_lstActSlot = new List<NKCUIStageActSlot>();

		// Token: 0x04005B8C RID: 23436
		private List<int> m_lstStageViewerID = new List<int>();

		// Token: 0x04005B8D RID: 23437
		private Dictionary<int, GameObject> m_dicUnlockEffectGo = new Dictionary<int, GameObject>();

		// Token: 0x04005B8E RID: 23438
		private NKMEpisodeTempletV2 m_EpisodeTemplet;

		// Token: 0x04005B8F RID: 23439
		private NKMStageTempletV2 m_ReservedStageTemplet;

		// Token: 0x04005B90 RID: 23440
		private int m_StageViewerNodeID;

		// Token: 0x04005B91 RID: 23441
		private bool m_bUseEpSlot;

		// Token: 0x04005B92 RID: 23442
		private bool m_bFirstSetting;

		// Token: 0x04005B93 RID: 23443
		private NKCPopupAchieveRateReward m_NKCPopupAchieveRateReward;

		// Token: 0x04005B94 RID: 23444
		private Vector2 m_vDownPos = Vector2.zero;

		// Token: 0x04005B95 RID: 23445
		private float m_fDragCheckDistance = 100f;

		// Token: 0x04005B96 RID: 23446
		private NKCUIShopSingleTab m_OperationShop;
	}
}
