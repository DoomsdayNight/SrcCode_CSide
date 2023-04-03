using System;
using Cs.Logging;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000985 RID: 2437
	public class NKCUIEpisodeActSlot : MonoBehaviour
	{
		// Token: 0x060063F0 RID: 25584 RVA: 0x001FAC36 File Offset: 0x001F8E36
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x001FAC44 File Offset: 0x001F8E44
		public static NKCUIEpisodeActSlot GetNewInstance(Transform parent, NKCUIEpisodeActSlot.OnSelectedItemSlot selectedSlot = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_OPERATION", "NKM_UI_OPERATION_EPISODE_MENU_BUTTON", false, null);
			NKCUIEpisodeActSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIEpisodeActSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIEpisodeActSlot Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			component.SetOnSelectedItemSlot(selectedSlot);
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x001FACBA File Offset: 0x001F8EBA
		public NKMEpisodeTempletV2 GetNKMEpisodeTemplet()
		{
			return this.m_cNKMEpisodeTemplet;
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x001FACC2 File Offset: 0x001F8EC2
		public int GetActIndex()
		{
			return this.m_ActID;
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x001FACCC File Offset: 0x001F8ECC
		public void SetOnSelectedItemSlot(NKCUIEpisodeActSlot.OnSelectedItemSlot selectedSlot)
		{
			if (selectedSlot != null)
			{
				this.m_ToggleButton.m_bGetCallbackWhileLocked = true;
				this.m_ToggleButton.OnValueChanged.RemoveAllListeners();
				this.m_OnSelectedSlot = selectedSlot;
				this.m_ToggleButton.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelectedItemSlotImpl));
			}
		}

		// Token: 0x060063F5 RID: 25589 RVA: 0x001FAD1C File Offset: 0x001F8F1C
		private void OnSelectedItemSlotImpl(bool bSet)
		{
			if (!this.CheckContentsUnlocked())
			{
				this.ShowLockedMessage();
				this.m_ToggleButton.Select(false, true, true);
				return;
			}
			if (this.m_OnSelectedSlot != null && this.m_ToggleButton.m_ToggleGroup != null)
			{
				this.m_OnSelectedSlot(bSet, this.m_ActID, this.m_cNKMEpisodeTemplet, this.m_diffculty);
			}
		}

		// Token: 0x060063F6 RID: 25590 RVA: 0x001FAD80 File Offset: 0x001F8F80
		public void SetData(NKMEpisodeTempletV2 cNKMEpisodeTemplet, int actID, EPISODE_DIFFICULTY difficulty, NKCUIComToggleGroup toggleGroup, bool bLock = false, bool bEPSlot = false)
		{
			this.m_ActID = actID;
			this.m_cNKMEpisodeTemplet = cNKMEpisodeTemplet;
			this.m_diffculty = difficulty;
			this.m_ToggleGroup = toggleGroup;
			NKMStageTempletV2 firstStageTemplet = NKCContentManager.GetFirstStageTemplet(this.m_cNKMEpisodeTemplet, this.m_ActID, this.m_diffculty);
			if (firstStageTemplet != null)
			{
				bLock = (bLock || !NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), firstStageTemplet.m_UnlockInfo, false) || !firstStageTemplet.EnableByTag);
			}
			if (!bLock)
			{
				NKCUIComToggle toggleButton = this.m_ToggleButton;
				if (toggleButton != null)
				{
					toggleButton.SetToggleGroup(toggleGroup);
				}
				NKCUtil.SetGameobjectActive(this.m_goEmblemOff, bEPSlot);
				NKCUtil.SetGameobjectActive(this.m_goEmblemOn, bEPSlot);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_LOPERATION_ACT_NUMBER_ACT_ON, !bEPSlot);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_LOPERATION_ACT_NUMBER_ACT_OFF, !bEPSlot);
				if (!bEPSlot)
				{
					this.m_lbActNameOn.text = actID.ToString();
					this.m_lbActNameOff.text = this.m_lbActNameOn.text;
					this.m_lbTitleOn.text = "";
					this.m_lbTitleOff.text = "";
					this.m_lbTitleLock.text = "";
					this.m_lbSubTitleOn.text = "";
					this.m_lbSubTitleOff.text = "";
					this.m_lbSubTitleLock.text = "";
					switch (this.m_cNKMEpisodeTemplet.m_EPCategory)
					{
					case EPISODE_CATEGORY.EC_SIDESTORY:
						this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_ON.text = NKCUtilString.GET_STRING_SIDE_STORY;
						this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_OFF.text = NKCUtilString.GET_STRING_SIDE_STORY;
						goto IL_47F;
					case EPISODE_CATEGORY.EC_FIELD:
						this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_ON.text = NKCUtilString.GET_STRING_FREE_ORDER;
						this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_OFF.text = NKCUtilString.GET_STRING_FREE_ORDER;
						goto IL_47F;
					case EPISODE_CATEGORY.EC_EVENT:
						this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_ON.text = NKCUtilString.GET_STRING_EVENT;
						this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_OFF.text = NKCUtilString.GET_STRING_EVENT;
						goto IL_47F;
					case EPISODE_CATEGORY.EC_CHALLENGE:
						this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_ON.text = NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_CHALLENGE", false);
						this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_OFF.text = NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_CHALLENGE", false);
						goto IL_47F;
					}
					this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_ON.text = NKCUtilString.GET_STRING_MAIN_STREAM;
					this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_OFF.text = NKCUtilString.GET_STRING_MAIN_STREAM;
				}
				else
				{
					this.m_lbActNameOn.text = "";
					this.m_lbActNameOff.text = this.m_lbActNameOn.text;
					this.m_lbTitleOn.text = cNKMEpisodeTemplet.GetEpisodeName();
					this.m_lbTitleOff.text = cNKMEpisodeTemplet.GetEpisodeName();
					this.m_lbTitleLock.text = cNKMEpisodeTemplet.GetEpisodeName();
					this.m_lbSubTitleOn.text = cNKMEpisodeTemplet.GetEpisodeTitle();
					this.m_lbSubTitleOff.text = cNKMEpisodeTemplet.GetEpisodeTitle();
					this.m_lbSubTitleLock.text = cNKMEpisodeTemplet.GetEpisodeTitle();
					this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_ON.text = "";
					this.m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_OFF.text = "";
					switch (cNKMEpisodeTemplet.m_EpisodeID)
					{
					case 101:
						if (!NKCContentManager.IsContentsUnlocked(ContentsType.DAILY_ATTACK, 0, 0))
						{
							this.m_ToggleButton.Lock(false);
						}
						else
						{
							this.m_ToggleButton.UnLock(false);
						}
						this.m_imgEmblemOff.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_DAILYMISSION_EMBLEM_01", false);
						this.m_imgEmblemOn.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_DAILYMISSION_EMBLEM_01_SELECT", false);
						break;
					case 102:
						if (!NKCContentManager.IsContentsUnlocked(ContentsType.DAILY_SEARCH, 0, 0))
						{
							this.m_ToggleButton.Lock(false);
						}
						else
						{
							this.m_ToggleButton.UnLock(false);
						}
						this.m_imgEmblemOff.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_DAILYMISSION_EMBLEM_04", false);
						this.m_imgEmblemOn.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_DAILYMISSION_EMBLEM_04_SELECT", false);
						break;
					case 103:
						if (!NKCContentManager.IsContentsUnlocked(ContentsType.DAILY_DEFENCE, 0, 0))
						{
							this.m_ToggleButton.Lock(false);
						}
						else
						{
							this.m_ToggleButton.UnLock(false);
						}
						this.m_imgEmblemOff.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_DAILYMISSION_EMBLEM_03", false);
						this.m_imgEmblemOn.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_DAILYMISSION_EMBLEM_03_SELECT", false);
						break;
					default:
						if (cNKMEpisodeTemplet.m_EPCategory != EPISODE_CATEGORY.EC_SUPPLY)
						{
							Log.Error("에피소드슬롯 쓰는게 맞는지 카테고리/아이디 확인 필요함", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIEpisodeActSlot.cs", 246);
						}
						else
						{
							NKCUtil.SetImageSprite(this.m_imgEmblemOff, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", cNKMEpisodeTemplet.m_DefaultSubTabIcon, false), false);
							NKCUtil.SetImageSprite(this.m_imgEmblemOn, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", cNKMEpisodeTemplet.m_SelectSubTabIcon, false), false);
						}
						break;
					}
				}
				IL_47F:
				if (!bEPSlot)
				{
					bool flag = this.CheckExistClearMission(cNKMEpisodeTemplet, actID);
					if (this.m_objNew.activeSelf == flag)
					{
						this.m_objNew.SetActive(!flag);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objNew, false);
				}
			}
			else
			{
				if (this.m_ToggleButton != null)
				{
					this.m_ToggleButton.SetToggleGroup(null);
					this.m_ToggleButton.Select(false, true, false);
				}
				if (this.m_objNew.activeSelf)
				{
					this.m_objNew.SetActive(false);
				}
			}
			if (this.m_objLock.activeSelf == !bLock)
			{
				this.m_objLock.SetActive(bLock);
			}
			this.m_bUseLockEndTime = false;
			NKCUtil.SetGameobjectActive(this.m_lbLock, true);
			NKCUtil.SetGameobjectActive(this.m_lbLockWithTime, false);
			if (firstStageTemplet != null && !NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), firstStageTemplet.m_UnlockInfo, false))
			{
				if (!NKMContentUnlockManager.IsStarted(firstStageTemplet.m_UnlockInfo))
				{
					this.m_lockEndTimeUtc = NKMContentUnlockManager.GetConditionStartTime(firstStageTemplet.m_UnlockInfo);
					NKCUtil.SetGameobjectActive(this.m_lbLock, false);
					NKCUtil.SetGameobjectActive(this.m_lbLockWithTime, true);
					this.m_bUseLockEndTime = true;
					this.SetLockText(this.m_lockEndTimeUtc);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lbLock, true);
					NKCUtil.SetGameobjectActive(this.m_lbLockWithTime, false);
					this.m_bUseLockEndTime = false;
				}
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x001FB354 File Offset: 0x001F9554
		private void Update()
		{
			if (this.m_bUseLockEndTime)
			{
				this.m_deltaTime += Time.deltaTime;
				if (this.m_deltaTime > 1f)
				{
					this.m_deltaTime = 0f;
					this.SetLockText(this.m_lockEndTimeUtc);
				}
			}
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x001FB394 File Offset: 0x001F9594
		public void SetLockText(DateTime lockEndTimeUtc)
		{
			if (lockEndTimeUtc < NKCSynchronizedTime.GetServerUTCTime(0.0))
			{
				this.SetData(this.m_cNKMEpisodeTemplet, this.m_ActID, this.m_diffculty, this.m_ToggleGroup, false, false);
				return;
			}
			if (NKCSynchronizedTime.GetTimeLeft(lockEndTimeUtc).TotalSeconds < 1.0)
			{
				NKCUtil.SetLabelText(this.m_lbLockWithTime, NKCUtilString.GET_STRING_QUIT);
				return;
			}
			string remainTimeString = NKCUtilString.GetRemainTimeString(lockEndTimeUtc, 2);
			NKCUtil.SetLabelText(this.m_lbLockWithTime, string.Format(NKCUtilString.GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM_CLOSE, remainTimeString));
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x001FB420 File Offset: 0x001F9620
		private bool CheckExistClearMission(NKMEpisodeTempletV2 cNKMEpisodeTemplet, int actID)
		{
			if (!cNKMEpisodeTemplet.m_DicStage.ContainsKey(actID))
			{
				return false;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			for (int i = 0; i < cNKMEpisodeTemplet.m_DicStage[actID].Count; i++)
			{
				NKMStageTempletV2 nkmstageTempletV = cNKMEpisodeTemplet.m_DicStage[actID][i];
				if (nkmstageTempletV != null)
				{
					switch (nkmstageTempletV.m_STAGE_TYPE)
					{
					case STAGE_TYPE.ST_WARFARE:
						if (myUserData.CheckWarfareClear(nkmstageTempletV.m_StageBattleStrID))
						{
							return true;
						}
						break;
					case STAGE_TYPE.ST_DUNGEON:
						if (myUserData.CheckDungeonClear(nkmstageTempletV.m_StageBattleStrID))
						{
							return true;
						}
						break;
					case STAGE_TYPE.ST_PHASE:
						if (NKCPhaseManager.CheckPhaseStageClear(nkmstageTempletV))
						{
							return true;
						}
						break;
					}
				}
			}
			return false;
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x001FB4C4 File Offset: 0x001F96C4
		private bool CheckContentsUnlocked()
		{
			if (this.m_cNKMEpisodeTemplet.m_EpisodeID == 101)
			{
				return NKCContentManager.IsContentsUnlocked(ContentsType.DAILY_ATTACK, 0, 0);
			}
			if (this.m_cNKMEpisodeTemplet.m_EpisodeID == 103)
			{
				return NKCContentManager.IsContentsUnlocked(ContentsType.DAILY_DEFENCE, 0, 0);
			}
			if (this.m_cNKMEpisodeTemplet.m_EpisodeID == 102)
			{
				return NKCContentManager.IsContentsUnlocked(ContentsType.DAILY_SEARCH, 0, 0);
			}
			NKMStageTempletV2 firstStageTemplet = NKCContentManager.GetFirstStageTemplet(this.m_cNKMEpisodeTemplet, this.m_ActID, this.m_diffculty);
			if (firstStageTemplet != null)
			{
				if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), firstStageTemplet.m_UnlockInfo, false))
				{
					return false;
				}
				if (!firstStageTemplet.EnableByTag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x001FB558 File Offset: 0x001F9758
		private void ShowLockedMessage()
		{
			if (this.m_cNKMEpisodeTemplet.m_EpisodeID == 101)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.DAILY_ATTACK, 0);
			}
			if (this.m_cNKMEpisodeTemplet.m_EpisodeID == 103)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.DAILY_DEFENCE, 0);
			}
			if (this.m_cNKMEpisodeTemplet.m_EpisodeID == 102)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.DAILY_SEARCH, 0);
			}
			NKMStageTempletV2 firstStageTemplet = NKCContentManager.GetFirstStageTemplet(this.m_cNKMEpisodeTemplet, this.m_ActID, this.m_diffculty);
			if (firstStageTemplet != null)
			{
				if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), firstStageTemplet.m_UnlockInfo, false))
				{
					NKCPopupMessageManager.AddPopupMessage(NKCContentManager.MakeUnlockConditionString(firstStageTemplet.m_UnlockInfo, true), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				if (!firstStageTemplet.EnableByTag)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_ALWAYS_LOCKED", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
			}
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x001FB617 File Offset: 0x001F9817
		public void SetActive(bool bSet)
		{
			if (base.gameObject.activeSelf == !bSet)
			{
				base.gameObject.SetActive(bSet);
			}
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x001FB636 File Offset: 0x001F9836
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x001FB643 File Offset: 0x001F9843
		public void Close()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
			this.m_instance = null;
		}

		// Token: 0x04004F80 RID: 20352
		[Header("Act")]
		public Text m_lbActNameOn;

		// Token: 0x04004F81 RID: 20353
		public Text m_lbActNameOff;

		// Token: 0x04004F82 RID: 20354
		public GameObject m_NKM_UI_LOPERATION_ACT_NUMBER_ACT_ON;

		// Token: 0x04004F83 RID: 20355
		public GameObject m_NKM_UI_LOPERATION_ACT_NUMBER_ACT_OFF;

		// Token: 0x04004F84 RID: 20356
		public Text m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_ON;

		// Token: 0x04004F85 RID: 20357
		public Text m_NKM_UI_LOPERATION_ACT_NUMBER_SUB_TEXT_OFF;

		// Token: 0x04004F86 RID: 20358
		public Text m_lbTitleOn;

		// Token: 0x04004F87 RID: 20359
		public Text m_lbTitleOff;

		// Token: 0x04004F88 RID: 20360
		public Text m_lbTitleLock;

		// Token: 0x04004F89 RID: 20361
		public Text m_lbSubTitleOn;

		// Token: 0x04004F8A RID: 20362
		public Text m_lbSubTitleOff;

		// Token: 0x04004F8B RID: 20363
		public Text m_lbSubTitleLock;

		// Token: 0x04004F8C RID: 20364
		public GameObject m_goEmblemOn;

		// Token: 0x04004F8D RID: 20365
		public GameObject m_goEmblemOff;

		// Token: 0x04004F8E RID: 20366
		public Image m_imgEmblemOn;

		// Token: 0x04004F8F RID: 20367
		public Image m_imgEmblemOff;

		// Token: 0x04004F90 RID: 20368
		public GameObject m_objLock;

		// Token: 0x04004F91 RID: 20369
		public Text m_lbLock;

		// Token: 0x04004F92 RID: 20370
		public Text m_lbLockWithTime;

		// Token: 0x04004F93 RID: 20371
		public NKCUIComToggle m_ToggleButton;

		// Token: 0x04004F94 RID: 20372
		public GameObject m_objNew;

		// Token: 0x04004F95 RID: 20373
		public GameObject m_objEventBadge;

		// Token: 0x04004F96 RID: 20374
		private NKCUIComToggleGroup m_ToggleGroup;

		// Token: 0x04004F97 RID: 20375
		private NKCUIEpisodeActSlot.OnSelectedItemSlot m_OnSelectedSlot;

		// Token: 0x04004F98 RID: 20376
		private int m_ActID;

		// Token: 0x04004F99 RID: 20377
		private NKMEpisodeTempletV2 m_cNKMEpisodeTemplet;

		// Token: 0x04004F9A RID: 20378
		private EPISODE_DIFFICULTY m_diffculty;

		// Token: 0x04004F9B RID: 20379
		private bool m_bUseLockEndTime;

		// Token: 0x04004F9C RID: 20380
		private DateTime m_lockEndTimeUtc = DateTime.MinValue;

		// Token: 0x04004F9D RID: 20381
		private NKCAssetInstanceData m_instance;

		// Token: 0x04004F9E RID: 20382
		private float m_deltaTime;

		// Token: 0x02001638 RID: 5688
		// (Invoke) Token: 0x0600AF85 RID: 44933
		public delegate void OnSelectedItemSlot(bool bSet, int actID, NKMEpisodeTempletV2 cNKMEpisodeTemplet, EPISODE_DIFFICULTY difficulty);
	}
}
