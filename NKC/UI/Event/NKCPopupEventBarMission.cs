using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.User;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BC2 RID: 3010
	public class NKCPopupEventBarMission : NKCUIBase
	{
		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06008B2C RID: 35628 RVA: 0x002F5610 File Offset: 0x002F3810
		public static NKCPopupEventBarMission Instance
		{
			get
			{
				if (NKCPopupEventBarMission.m_Instance == null)
				{
					NKCPopupEventBarMission.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventBarMission>("event_gremory_bar", "POPUP_EVENT_GREMORY_BAR_MOMO", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventBarMission.CleanupInstance)).GetInstance<NKCPopupEventBarMission>();
					NKCPopupEventBarMission.m_Instance.InitUI();
				}
				return NKCPopupEventBarMission.m_Instance;
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06008B2D RID: 35629 RVA: 0x002F565F File Offset: 0x002F385F
		public static bool HasInstance
		{
			get
			{
				return NKCPopupEventBarMission.m_Instance != null;
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06008B2E RID: 35630 RVA: 0x002F566C File Offset: 0x002F386C
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventBarMission.m_Instance != null && NKCPopupEventBarMission.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008B2F RID: 35631 RVA: 0x002F5687 File Offset: 0x002F3887
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventBarMission.m_Instance != null && NKCPopupEventBarMission.m_Instance.IsOpen)
			{
				NKCPopupEventBarMission.m_Instance.Close();
			}
		}

		// Token: 0x06008B30 RID: 35632 RVA: 0x002F56AC File Offset: 0x002F38AC
		private static void CleanupInstance()
		{
			NKCPopupEventBarMission.m_Instance = null;
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06008B31 RID: 35633 RVA: 0x002F56B4 File Offset: 0x002F38B4
		public override string MenuName
		{
			get
			{
				return "Momo Mission";
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06008B32 RID: 35634 RVA: 0x002F56BB File Offset: 0x002F38BB
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06008B33 RID: 35635 RVA: 0x002F56BE File Offset: 0x002F38BE
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06008B34 RID: 35636 RVA: 0x002F56C1 File Offset: 0x002F38C1
		public override bool WillCloseUnderPopupOnOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008B35 RID: 35637 RVA: 0x002F56C4 File Offset: 0x002F38C4
		private void InitUI()
		{
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetPresetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnPresetSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvidePresetData;
				this.m_LoopScrollRect.ContentConstraintCount = 1;
				this.m_LoopScrollRect.PrepareCells(0);
				this.m_LoopScrollRect.TotalCount = 0;
				this.m_LoopScrollRect.RefreshCells(false);
			}
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.OnClickClose));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCompleteAll, new UnityAction(this.OnClickCompleteAll));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnErrandRawardList, new UnityAction(this.OnClickShowErrandRward));
			NKCUtil.SetButtonClickDelegate(this.m_scriptButton, new UnityAction(this.OnClickScriptPanel));
			NKCUIComMissionGroup comMissionGroup = this.m_comMissionGroup;
			if (comMissionGroup != null)
			{
				comMissionGroup.Init();
			}
			NKCUIEventBarMissionGroupList comMissionGroupList = this.m_comMissionGroupList;
			if (comMissionGroupList != null)
			{
				comMissionGroupList.Init("event_gremory_bar", "POPUP_EVENT_GREMORY_BAR_MOMO_REWARD_SLOT");
			}
			if (this.m_SpineIllust != null)
			{
				this.m_SpineIllust.m_dOnTouch = new NKCUINPCSpineIllust.OnTouch(this.OnCharacterTouch);
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008B36 RID: 35638 RVA: 0x002F581C File Offset: 0x002F3A1C
		public override void CloseInternal()
		{
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			if (this.m_goErrandCoroutine != null)
			{
				base.StopCoroutine(this.m_goErrandCoroutine);
				this.m_goErrandCoroutine = null;
			}
			if (this.m_dOnClose != null)
			{
				this.m_dOnClose();
			}
			this.HideScript();
			NKCUIVoiceManager.StopVoice();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008B37 RID: 35639 RVA: 0x002F588C File Offset: 0x002F3A8C
		public void Open(int errandRewardMissionTabId, int cocktailRewardMissionGroupId, NKCPopupEventBarMission.OnClose onClose = null)
		{
			this.m_errandRewardMissionTabId = errandRewardMissionTabId;
			this.m_cocktailRewardMissionGroupId = cocktailRewardMissionGroupId;
			this.m_dOnClose = onClose;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool flag = nkmuserData == null || this.m_userUId != nkmuserData.m_UserUID;
			this.m_userUId = ((nkmuserData != null) ? nkmuserData.m_UserUID : 0L);
			if (this.missionTempletList == null || flag)
			{
				this.missionTempletList = this.BuildAllMissionTempletListByTab(this.m_errandRewardMissionTabId);
			}
			NKCUtil.SetGameobjectActive(this.m_objErrandPanel, false);
			base.gameObject.SetActive(true);
			NKCUIComMissionGroup comMissionGroup = this.m_comMissionGroup;
			if (comMissionGroup != null)
			{
				comMissionGroup.SetData(this.m_cocktailRewardMissionGroupId, new NKCUIComMissionGroup.OnRewardGet(this.OnMomoGoErrand), new NKCUIComMissionGroup.OnRewardLocked(this.OnMomoCannotGoErrand));
			}
			NKCUIEventBarMissionGroupList comMissionGroupList = this.m_comMissionGroupList;
			if (comMissionGroupList != null)
			{
				comMissionGroupList.CloseImmediately();
			}
			this.RefreshScrollRect();
			int num = this.PlayRandomAnimation(this.m_aniStandBy);
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_LOBBY_CONNECT, this.m_unitId, 0, false, false);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_unitId);
			NKCUtil.SetLabelText(this.m_lbUnitName, (unitTempletBase != null) ? unitTempletBase.GetUnitName() : "");
			this.ShowScript((num == 0) ? NKCUtilString.GET_STRING_GREMORY_MOMO_HELLO_1 : NKCUtilString.GET_STRING_GREMORY_MOMO_HELLO_2);
			base.UIOpened(true);
		}

		// Token: 0x06008B38 RID: 35640 RVA: 0x002F59BC File Offset: 0x002F3BBC
		public override void Hide()
		{
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			if (this.m_goErrandCoroutine != null)
			{
				base.StopCoroutine(this.m_goErrandCoroutine);
				this.m_goErrandCoroutine = null;
			}
			this.HideScript();
			NKCUIVoiceManager.StopVoice();
			NKCUtil.SetGameobjectActive(this.m_objErrandPanel, false);
			base.Hide();
		}

		// Token: 0x06008B39 RID: 35641 RVA: 0x002F5A1C File Offset: 0x002F3C1C
		private void RefreshScrollRect()
		{
			if (this.m_LoopScrollRect == null || this.missionTempletList == null)
			{
				return;
			}
			this.m_LoopScrollRect.TotalCount = this.missionTempletList.Count;
			this.m_LoopScrollRect.SetIndexPosition(0);
			this.RefreshCompleteAllState();
		}

		// Token: 0x06008B3A RID: 35642 RVA: 0x002F5A68 File Offset: 0x002F3C68
		private void RefreshCompleteAllState()
		{
			bool flag = false;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				flag = NKCScenManager.CurrentUserData().m_MissionData.CheckCompletableMission(nkmuserData, this.m_errandRewardMissionTabId, false);
			}
			NKCUIComStateButton csbtnCompleteAll = this.m_csbtnCompleteAll;
			if (csbtnCompleteAll == null)
			{
				return;
			}
			csbtnCompleteAll.SetLock(!flag, false);
		}

		// Token: 0x06008B3B RID: 35643 RVA: 0x002F5AB0 File Offset: 0x002F3CB0
		public void RefreshMission()
		{
			this.missionTempletList = this.BuildAllMissionTempletListByTab(this.m_errandRewardMissionTabId);
			this.RefreshScrollRect();
			NKCUIComMissionGroup comMissionGroup = this.m_comMissionGroup;
			if (comMissionGroup != null)
			{
				comMissionGroup.SetData(this.m_cocktailRewardMissionGroupId, new NKCUIComMissionGroup.OnRewardGet(this.OnMomoGoErrand), new NKCUIComMissionGroup.OnRewardLocked(this.OnMomoCannotGoErrand));
			}
			if (this.m_comMissionGroupList != null && this.m_comMissionGroupList.IsOpened())
			{
				this.m_comMissionGroupList.Refresh();
			}
		}

		// Token: 0x06008B3C RID: 35644 RVA: 0x002F5B2A File Offset: 0x002F3D2A
		public override void OnBackButton()
		{
			if (!this.CanClose())
			{
				return;
			}
			base.Close();
		}

		// Token: 0x06008B3D RID: 35645 RVA: 0x002F5B3C File Offset: 0x002F3D3C
		private void Update()
		{
			this.m_typeWriter.Update();
			if (!this.m_objScriptRoot.activeSelf && (this.m_SpineIllust == null || this.m_SpineIllust.GetCurrentAnimationName() == "IDLE"))
			{
				this.m_fScriptStanbyTimer += Time.deltaTime;
				if (this.m_fScriptStanbyTimer >= this.m_scriptIdleTimer)
				{
					int num = this.PlayRandomAnimation(this.m_aniStandBy);
					this.ShowScript((num == 0) ? NKCUtilString.GET_STRING_GREMORY_MOMO_HELLO_1 : NKCUtilString.GET_STRING_GREMORY_MOMO_HELLO_2);
				}
			}
			if (Input.anyKeyDown)
			{
				this.m_fScriptStanbyTimer = 0f;
			}
		}

		// Token: 0x06008B3E RID: 35646 RVA: 0x002F5BDA File Offset: 0x002F3DDA
		private List<NKMMissionTemplet> BuildAllMissionTempletListByTab(int tabID)
		{
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(tabID);
			missionTempletListByType.Sort(new Comparison<NKMMissionTemplet>(NKMMissionManager.Comparer));
			return missionTempletListByType;
		}

		// Token: 0x06008B3F RID: 35647 RVA: 0x002F5BF4 File Offset: 0x002F3DF4
		private void ShowScript(string message)
		{
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
				this.m_aniScript.Play("EVNET_GREMORY_BAR_GREMORY_SCRIPT_IDLE", -1, 0f);
			}
			NKCUtil.SetGameobjectActive(this.m_objScriptRoot, true);
			this.m_typeWriter.Start(this.m_lbScriptMsg, message, 0f, false);
			this.m_fScriptTimer = this.m_showScriptTime;
			if (this.m_scriptCoroutine == null)
			{
				this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
			}
		}

		// Token: 0x06008B40 RID: 35648 RVA: 0x002F5C7C File Offset: 0x002F3E7C
		private void HideScript()
		{
			this.m_fScriptStanbyTimer = 0f;
			NKCUtil.SetGameobjectActive(this.m_objScriptRoot, false);
			NKCUtil.SetLabelText(this.m_lbScriptMsg, "");
		}

		// Token: 0x06008B41 RID: 35649 RVA: 0x002F5CA5 File Offset: 0x002F3EA5
		private IEnumerator IOnShowRequestScript()
		{
			while (this.m_fScriptTimer > 0f)
			{
				this.m_fScriptTimer -= Time.deltaTime;
				yield return null;
			}
			yield return new WaitWhile(() => this.m_typeWriter.IsTyping());
			if (this.m_SpineIllust != null)
			{
				yield return new WaitWhile(() => NKCUIVoiceManager.IsPlayingVoice(-1));
			}
			Animator aniScript = this.m_aniScript;
			if (aniScript != null)
			{
				aniScript.SetTrigger("OUTRO");
			}
			yield return new WaitWhile(() => this.m_scriptCanvasGroup.alpha > 0f);
			this.HideScript();
			yield break;
		}

		// Token: 0x06008B42 RID: 35650 RVA: 0x002F5CB4 File Offset: 0x002F3EB4
		private int PlayRandomAnimation(NKCASUIUnitIllust.eAnimation[] animations)
		{
			int num = animations.Length;
			int num2 = -1;
			if (animations != null && num > 0 && this.m_SpineIllust != null)
			{
				num2 = this.GetRandomAnimation(num);
				if (num2 >= 0)
				{
					string animationName = NKCASUIUnitIllust.GetAnimationName(animations[num2]);
					this.m_SpineIllust.SetAnimation(animationName);
				}
			}
			return num2;
		}

		// Token: 0x06008B43 RID: 35651 RVA: 0x002F5D00 File Offset: 0x002F3F00
		private int GetRandomAnimation(int aniCount)
		{
			if (aniCount <= 0)
			{
				return -1;
			}
			int num = -1;
			if (this.allowRepeat)
			{
				int num2 = UnityEngine.Random.Range(0, aniCount);
				if (num2 < aniCount)
				{
					this.m_prevAniIndex = num2;
					num = num2;
				}
			}
			else
			{
				List<int> list = new List<int>();
				for (int i = 0; i < aniCount; i++)
				{
					if (i != this.m_prevAniIndex)
					{
						list.Add(i);
					}
				}
				int num3 = UnityEngine.Random.Range(0, list.Count);
				if (num3 < list.Count)
				{
					num = list[num3];
				}
				if (aniCount == 1)
				{
					num = 0;
				}
				if (num >= 0 && num < aniCount)
				{
					this.m_prevAniIndex = num;
				}
			}
			return num;
		}

		// Token: 0x06008B44 RID: 35652 RVA: 0x002F5D90 File Offset: 0x002F3F90
		private void OnCharacterTouch()
		{
			this.PlayTouchVoice();
		}

		// Token: 0x06008B45 RID: 35653 RVA: 0x002F5D98 File Offset: 0x002F3F98
		private void PlayTouchVoice()
		{
			List<NKCVoiceTemplet> voiceTempletByType = NKCUIVoiceManager.GetVoiceTempletByType(VOICE_TYPE.VT_TOUCH);
			List<NKCVoiceTemplet> list = voiceTempletByType.FindAll((NKCVoiceTemplet v) => v.Condition == VOICE_CONDITION.VC_NONE);
			if (list.Count <= 0)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_unitId);
			if (unitTempletBase == null)
			{
				return;
			}
			int skinID = 0;
			List<NKCVoiceTemplet> list2 = new List<NKCVoiceTemplet>();
			VOICE_BUNDLE flag = VOICE_BUNDLE.UNIT | VOICE_BUNDLE.COMMON;
			string unitStrID = unitTempletBase.m_UnitStrID;
			string baseUnitStrID = "";
			if (unitTempletBase.BaseUnit != null)
			{
				baseUnitStrID = unitTempletBase.BaseUnit.m_UnitStrID;
			}
			list2 = list.FindAll((NKCVoiceTemplet v) => NKCUIVoiceManager.CheckAsset(unitStrID, 0, v.FileName, flag));
			if (list2.Count == 0 && !string.IsNullOrEmpty(baseUnitStrID))
			{
				list2 = list.FindAll((NKCVoiceTemplet v) => NKCUIVoiceManager.CheckAsset(baseUnitStrID, 0, v.FileName, flag));
				if (list2.Count > 0)
				{
					unitStrID = baseUnitStrID;
				}
			}
			if (list2.Count <= 0)
			{
				return;
			}
			if (list2.Exists((NKCVoiceTemplet v) => v.Condition > VOICE_CONDITION.VC_NONE))
			{
				list2 = list2.FindAll((NKCVoiceTemplet v) => v.Condition > VOICE_CONDITION.VC_NONE);
			}
			int num = NKMRandom.Range(0, list2.Count);
			if (num < list2.Count)
			{
				string voiceCaption = NKCUtilString.GetVoiceCaption(NKCUIVoiceManager.PlayOnUI(unitStrID, skinID, voiceTempletByType[num].FileName, 100f, flag, false));
				if (!string.IsNullOrEmpty(voiceCaption))
				{
					this.ShowScript(voiceCaption);
				}
			}
		}

		// Token: 0x06008B46 RID: 35654 RVA: 0x002F5F3A File Offset: 0x002F413A
		private RectTransform GetPresetSlot(int index)
		{
			NKCUIMissionAchieveSlot newInstance = NKCUIMissionAchieveSlot.GetNewInstance(null, "EVENT_GREMORY_BAR", "POPUP_EVENT_GREMORY_BAR_MOMO_MISSION_SLOT");
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06008B47 RID: 35655 RVA: 0x002F5F58 File Offset: 0x002F4158
		private void ReturnPresetSlot(Transform tr)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06008B48 RID: 35656 RVA: 0x002F5F90 File Offset: 0x002F4190
		private void ProvidePresetData(Transform tr, int index)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(this.missionTempletList[index], new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), null, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickLocked));
		}

		// Token: 0x06008B49 RID: 35657 RVA: 0x002F5FE5 File Offset: 0x002F41E5
		private bool CanClose()
		{
			if (this.m_objErrandPanel.activeSelf)
			{
				return false;
			}
			if (!this.m_comMissionGroupList.IsClosed())
			{
				this.m_comMissionGroupList.Close();
				return false;
			}
			return true;
		}

		// Token: 0x06008B4A RID: 35658 RVA: 0x002F6014 File Offset: 0x002F4214
		private void OnClickMove(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.RefreshMission), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCContentManager.MoveToShortCut(nkmmissionTemplet.m_ShortCutType, nkmmissionTemplet.m_ShortCut, false);
		}

		// Token: 0x06008B4B RID: 35659 RVA: 0x002F6088 File Offset: 0x002F4288
		private void OnClickComplete(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.RefreshMission), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionTabId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_GroupId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionID);
		}

		// Token: 0x06008B4C RID: 35660 RVA: 0x002F610E File Offset: 0x002F430E
		private void OnClickLocked(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKCUIVoiceManager.StopVoice();
			this.ShowScript(NKCUtilString.GET_STRING_GREMORY_MOMO_IGNORE_MISSION);
			this.PlayRandomAnimation(this.m_aniMissionNotCompleted);
		}

		// Token: 0x06008B4D RID: 35661 RVA: 0x002F6138 File Offset: 0x002F4338
		private void OnClickCompleteAll()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMUserMissionData missionData = NKCScenManager.GetScenManager().GetMyUserData().m_MissionData;
			if (missionData == null)
			{
				return;
			}
			if (!missionData.CheckCompletableMission(myUserData, this.m_errandRewardMissionTabId, false))
			{
				return;
			}
			NKMPacket_MISSION_COMPLETE_ALL_REQ nkmpacket_MISSION_COMPLETE_ALL_REQ = new NKMPacket_MISSION_COMPLETE_ALL_REQ();
			nkmpacket_MISSION_COMPLETE_ALL_REQ.tabId = this.m_errandRewardMissionTabId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MISSION_COMPLETE_ALL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008B4E RID: 35662 RVA: 0x002F619E File Offset: 0x002F439E
		private void OnClickShowErrandRward()
		{
			if (this.m_comMissionGroupList.IsOpened())
			{
				this.m_comMissionGroupList.Close();
				return;
			}
			this.m_comMissionGroupList.Open(NKCUIEventBarMissionGroupList.MissionType.MissionGroupId, this.m_cocktailRewardMissionGroupId);
		}

		// Token: 0x06008B4F RID: 35663 RVA: 0x002F61CB File Offset: 0x002F43CB
		private void OnMomoGoErrand(NKMMissionTemplet missionTemplet)
		{
			if (missionTemplet == null)
			{
				return;
			}
			this.m_goErrandCoroutine = base.StartCoroutine(this.IMomoGoErrand(missionTemplet));
		}

		// Token: 0x06008B50 RID: 35664 RVA: 0x002F61E4 File Offset: 0x002F43E4
		private IEnumerator IMomoGoErrand(NKMMissionTemplet missionTemplet)
		{
			NKCUtil.SetImageFillAmount(this.m_imgErrandGauge, 0f);
			NKCUtil.SetGameobjectActive(this.m_objErrandPanel, true);
			float startTime = this.m_aniGoErrandPopup.GetCurrentAnimatorStateInfo(0).normalizedTime;
			int num = UnityEngine.Random.Range(0, 2);
			SkeletonGraphic spineSD = this.m_SpineSD;
			if (spineSD != null)
			{
				spineSD.AnimationState.SetAnimation(0, (num == 1) ? "SD_ACT_RUN" : "SD_ACT_TUMBLE", num == 1);
			}
			while (this.m_imgErrandGauge.fillAmount < 1f)
			{
				yield return null;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(missionTemplet.m_MissionTabId, missionTemplet.m_GroupId, missionTemplet.m_MissionID);
			while (this.m_aniGoErrandPopup.GetCurrentAnimatorStateInfo(0).normalizedTime - startTime < 1f)
			{
				yield return null;
			}
			NKCUtil.SetGameobjectActive(this.m_objErrandPanel, false);
			yield break;
		}

		// Token: 0x06008B51 RID: 35665 RVA: 0x002F61FC File Offset: 0x002F43FC
		private void OnMomoCannotGoErrand(bool allMissionCompleted)
		{
			NKCUIVoiceManager.StopVoice();
			if (allMissionCompleted)
			{
				this.ShowScript(NKCUtilString.GET_STRING_GREMORY_MOMO_COMPLETE_ERRAND);
				this.PlayRandomAnimation(this.m_aniErrandCompleted);
			}
			else
			{
				this.ShowScript(NKCUtilString.GET_STRING_GREMORY_MOMO_IGNORE_ERRAND);
				this.PlayRandomAnimation(this.m_aniErrandNotEnabled);
			}
			this.m_fScriptTimer = this.m_showScriptTime;
			if (this.m_scriptCoroutine == null)
			{
				this.m_scriptCoroutine = base.StartCoroutine(this.IOnShowRequestScript());
			}
		}

		// Token: 0x06008B52 RID: 35666 RVA: 0x002F6269 File Offset: 0x002F4469
		private void OnClickClose()
		{
			if (!this.CanClose())
			{
				return;
			}
			base.Close();
		}

		// Token: 0x06008B53 RID: 35667 RVA: 0x002F627A File Offset: 0x002F447A
		private void OnClickScriptPanel()
		{
			if (this.m_typeWriter.IsTyping())
			{
				this.m_typeWriter.Finish();
				return;
			}
			if (this.m_scriptCoroutine != null)
			{
				base.StopCoroutine(this.m_scriptCoroutine);
				this.m_scriptCoroutine = null;
			}
			this.HideScript();
		}

		// Token: 0x06008B54 RID: 35668 RVA: 0x002F62B6 File Offset: 0x002F44B6
		private void OnDestroy()
		{
			if (this.missionTempletList != null)
			{
				this.missionTempletList.Clear();
				this.missionTempletList = null;
			}
		}

		// Token: 0x04007808 RID: 30728
		private const string ASSET_BUNDLE_NAME = "event_gremory_bar";

		// Token: 0x04007809 RID: 30729
		private const string UI_ASSET_NAME = "POPUP_EVENT_GREMORY_BAR_MOMO";

		// Token: 0x0400780A RID: 30730
		private static NKCPopupEventBarMission m_Instance;

		// Token: 0x0400780B RID: 30731
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x0400780C RID: 30732
		public EventTrigger m_eventTriggerBg;

		// Token: 0x0400780D RID: 30733
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400780E RID: 30734
		public NKCUIComStateButton m_csbtnCompleteAll;

		// Token: 0x0400780F RID: 30735
		public NKCUIComStateButton m_csbtnErrandRawardList;

		// Token: 0x04007810 RID: 30736
		public NKCUIComMissionGroup m_comMissionGroup;

		// Token: 0x04007811 RID: 30737
		public NKCUIEventBarMissionGroupList m_comMissionGroupList;

		// Token: 0x04007812 RID: 30738
		public GameObject m_objErrandPanel;

		// Token: 0x04007813 RID: 30739
		public Image m_imgErrandGauge;

		// Token: 0x04007814 RID: 30740
		public NKCUINPCSpineIllust m_SpineIllust;

		// Token: 0x04007815 RID: 30741
		public SkeletonGraphic m_SpineSD;

		// Token: 0x04007816 RID: 30742
		public Animator m_aniGoErrandPopup;

		// Token: 0x04007817 RID: 30743
		[Header("Spine UnitId")]
		public int m_unitId;

		// Token: 0x04007818 RID: 30744
		public NKCASUIUnitIllust.eAnimation[] m_aniStandBy;

		// Token: 0x04007819 RID: 30745
		public NKCASUIUnitIllust.eAnimation[] m_aniMissionNotCompleted;

		// Token: 0x0400781A RID: 30746
		public NKCASUIUnitIllust.eAnimation[] m_aniErrandNotEnabled;

		// Token: 0x0400781B RID: 30747
		public NKCASUIUnitIllust.eAnimation[] m_aniErrandCompleted;

		// Token: 0x0400781C RID: 30748
		public bool allowRepeat;

		// Token: 0x0400781D RID: 30749
		[Header("대사 창")]
		public Animator m_aniScript;

		// Token: 0x0400781E RID: 30750
		public CanvasGroup m_scriptCanvasGroup;

		// Token: 0x0400781F RID: 30751
		public GameObject m_objScriptRoot;

		// Token: 0x04007820 RID: 30752
		public Text m_lbUnitName;

		// Token: 0x04007821 RID: 30753
		public Text m_lbScriptMsg;

		// Token: 0x04007822 RID: 30754
		public float m_showScriptTime;

		// Token: 0x04007823 RID: 30755
		public float m_scriptIdleTimer;

		// Token: 0x04007824 RID: 30756
		public NKCUIComStateButton m_scriptButton;

		// Token: 0x04007825 RID: 30757
		private NKCUITypeWriter m_typeWriter = new NKCUITypeWriter();

		// Token: 0x04007826 RID: 30758
		private List<NKMMissionTemplet> missionTempletList;

		// Token: 0x04007827 RID: 30759
		private int m_errandRewardMissionTabId;

		// Token: 0x04007828 RID: 30760
		private int m_cocktailRewardMissionGroupId;

		// Token: 0x04007829 RID: 30761
		private int m_prevAniIndex;

		// Token: 0x0400782A RID: 30762
		private long m_userUId;

		// Token: 0x0400782B RID: 30763
		private float m_fScriptTimer;

		// Token: 0x0400782C RID: 30764
		private float m_fScriptStanbyTimer;

		// Token: 0x0400782D RID: 30765
		private Coroutine m_scriptCoroutine;

		// Token: 0x0400782E RID: 30766
		private Coroutine m_goErrandCoroutine;

		// Token: 0x0400782F RID: 30767
		private NKCPopupEventBarMission.OnClose m_dOnClose;

		// Token: 0x0200198D RID: 6541
		// (Invoke) Token: 0x0600B938 RID: 47416
		public delegate void OnClose();
	}
}
