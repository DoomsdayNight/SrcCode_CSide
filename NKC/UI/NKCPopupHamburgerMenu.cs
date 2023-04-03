using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKC.Templet;
using NKC.UI.Office;
using NKC.UI.Option;
using NKC.UI.Result;
using NKC.UI.Shop;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A5E RID: 2654
	public class NKCPopupHamburgerMenu : NKCUIBase
	{
		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06007497 RID: 29847 RVA: 0x0026C2EC File Offset: 0x0026A4EC
		public static NKCPopupHamburgerMenu instance
		{
			get
			{
				if (NKCPopupHamburgerMenu.m_Instance == null)
				{
					NKCPopupHamburgerMenu.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupHamburgerMenu>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_HAMBURGER_MENU", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupHamburgerMenu.CleanupInstance)).GetInstance<NKCPopupHamburgerMenu>();
					NKCPopupHamburgerMenu.m_Instance.Init();
				}
				return NKCPopupHamburgerMenu.m_Instance;
			}
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06007498 RID: 29848 RVA: 0x0026C33B File Offset: 0x0026A53B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupHamburgerMenu.m_Instance != null && NKCPopupHamburgerMenu.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007499 RID: 29849 RVA: 0x0026C356 File Offset: 0x0026A556
		private static void CleanupInstance()
		{
			NKCPopupHamburgerMenu.m_Instance = null;
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x0600749A RID: 29850 RVA: 0x0026C35E File Offset: 0x0026A55E
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME;
			}
		}

		// Token: 0x0600749B RID: 29851 RVA: 0x0026C361 File Offset: 0x0026A561
		public override void OnCloseInstance()
		{
			NKCMailManager.dOnMailFlagChange = (NKCMailManager.OnMailFlagChange)Delegate.Remove(NKCMailManager.dOnMailFlagChange, new NKCMailManager.OnMailFlagChange(this.SetNewMail));
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x0600749C RID: 29852 RVA: 0x0026C383 File Offset: 0x0026A583
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x0600749D RID: 29853 RVA: 0x0026C386 File Offset: 0x0026A586
		public override string MenuName
		{
			get
			{
				return "Menu";
			}
		}

		// Token: 0x0600749E RID: 29854 RVA: 0x0026C38D File Offset: 0x0026A58D
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600749F RID: 29855 RVA: 0x0026C39C File Offset: 0x0026A59C
		private void Init()
		{
			this.m_dicButtons.Clear();
			if (this.m_btnBackground != null)
			{
				this.m_btnBackground.PointerClick.RemoveAllListeners();
				this.m_btnBackground.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_btnUserBuff != null)
			{
				this.m_btnUserBuff.PointerClick.RemoveAllListeners();
				this.m_btnUserBuff.PointerClick.AddListener(new UnityAction(this.OnBtnBuff));
			}
			if (this.m_btnMail != null)
			{
				this.m_btnMail.PointerClick.RemoveAllListeners();
				this.m_btnMail.PointerClick.AddListener(new UnityAction(this.OnBtnMail));
			}
			if (this.m_objNewMail != null)
			{
				NKCMailManager.dOnMailFlagChange = (NKCMailManager.OnMailFlagChange)Delegate.Combine(NKCMailManager.dOnMailFlagChange, new NKCMailManager.OnMailFlagChange(this.SetNewMail));
			}
			if (this.m_btnOption != null)
			{
				this.m_btnOption.PointerClick.RemoveAllListeners();
				this.m_btnOption.PointerClick.AddListener(new UnityAction(this.OnBtnOption));
			}
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(this.OnBtnClose));
				NKCUtil.SetHotkey(this.m_btnClose, HotkeyEventType.HamburgerMenu, null, false);
			}
			if (this.m_btnGuild != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Guild, this.m_btnGuild);
				this.m_btnGuild.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckGuildNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnGuild), ContentsType.GUILD);
			}
			if (this.m_btnChat != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Chat, this.m_btnChat);
				this.m_btnChat.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckChatNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnChat), ContentsType.FRIENDS);
			}
			if (this.m_btnCollection != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Collection, this.m_btnCollection);
				this.m_btnCollection.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckCollectionNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnCollection), ContentsType.None);
			}
			if (this.m_btnOfficeDorm != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.OfficeDorm, this.m_btnOfficeDorm);
				this.m_btnOfficeDorm.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckOfficeDormNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnOfficeDorm), ContentsType.OFFICE);
			}
			if (this.m_btnInventory != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Inventory, this.m_btnInventory);
				this.m_btnInventory.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckInventoryNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnInventory), ContentsType.None);
			}
			if (this.m_btnHangar != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Hangar, this.m_btnHangar);
				this.m_btnHangar.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckHangarNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnHangar), ContentsType.BASE_HANGAR);
			}
			if (this.m_btnLab != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Lab, this.m_btnLab);
				this.m_btnLab.Init(null, new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnLab), ContentsType.BASE_LAB);
			}
			if (this.m_btnUnitList != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.UnitList, this.m_btnUnitList);
				this.m_btnUnitList.Init(null, new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnUnitList), ContentsType.None);
			}
			if (this.m_btnPersonnel != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Personnel, this.m_btnPersonnel);
				this.m_btnPersonnel.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckScoutNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnPersonnel), ContentsType.BASE_PERSONNAL);
			}
			if (this.m_btnFactory != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Factory, this.m_btnFactory);
				this.m_btnFactory.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckFactoryNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnFactory), ContentsType.BASE_FACTORY);
				this.m_btnFactory.SetEnableEvent(NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT) || NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT));
			}
			if (this.m_btnDeckSetup != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.DeckSetup, this.m_btnDeckSetup);
				this.m_btnDeckSetup.Init(null, new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnDeckSetup), ContentsType.DECKVIEW);
			}
			if (this.m_btnPvp != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Pvp, this.m_btnPvp);
				this.m_btnPvp.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckPVPNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnPvp), ContentsType.PVP);
			}
			if (this.m_btnWorldmap != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Worldmap, this.m_btnWorldmap);
				this.m_btnWorldmap.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckWorldMapNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnWorldmap), ContentsType.WORLDMAP);
			}
			if (this.m_btnOperation != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Operation, this.m_btnOperation);
				this.m_btnOperation.Init(null, new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnOperation), ContentsType.None);
			}
			if (this.m_btnFriends != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Friends, this.m_btnFriends);
				this.m_btnFriends.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckFriendNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnFriends), ContentsType.FRIENDS);
			}
			if (this.m_btnOfficeFacility != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.OfficeFacility, this.m_btnOfficeFacility);
				this.m_btnOfficeFacility.Init(null, new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnOfficeFacility), ContentsType.BASE);
			}
			if (this.m_btnRanking != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Ranking, this.m_btnRanking);
				this.m_btnRanking.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckleaderBoardNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnRanking), ContentsType.LEADERBOARD);
			}
			if (this.m_btnMission != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Mission, this.m_btnMission);
				this.m_btnMission.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckMissionNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnMission), ContentsType.LOBBY_SUBMENU);
			}
			if (this.m_btnContract != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Contract, this.m_btnContract);
				this.m_btnContract.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckContractNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnContract), ContentsType.CONTRACT);
			}
			if (this.m_btnShop != null)
			{
				this.m_dicButtons.Add(NKCPopupHamburgerMenu.eButtonMenuType.Shop, this.m_btnShop);
				this.m_btnShop.Init(new NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction(NKCAlarmManager.CheckShopNotify), new NKCPopupHamburgerMenuSimpleButton.OnButton(this.OnBtnShop), ContentsType.LOBBY_SUBMENU);
			}
			if (this.m_btnQuickMove != null)
			{
				this.m_btnQuickMove.PointerClick.RemoveAllListeners();
				this.m_btnQuickMove.PointerClick.AddListener(new UnityAction(this.OnBtnQuickMove));
			}
			if (this.m_btnComplete != null)
			{
				this.m_btnComplete.PointerClick.RemoveAllListeners();
				this.m_btnComplete.PointerClick.AddListener(new UnityAction(this.OnBtnQuickMove));
			}
			if (NKMMissionManager.GetTrackingMissionTemplet() == null)
			{
				NKMMissionManager.SetDefaultTrackingMissionToGrowthMission();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060074A0 RID: 29856 RVA: 0x0026CB00 File Offset: 0x0026AD00
		public void OpenUI()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			foreach (KeyValuePair<NKCPopupHamburgerMenu.eButtonMenuType, NKCPopupHamburgerMenuSimpleButton> keyValuePair in this.m_dicButtons)
			{
				keyValuePair.Value.UpdateData(nkmuserData);
			}
			this.SetNewMail(NKCAlarmManager.CheckMailNotify(nkmuserData));
			this.m_lbUserName.text = NKCUtilString.GetUserNickname(nkmuserData.m_UserNickName, false);
			this.SetGuildData();
			this.SetUserBuffCount();
			this.SetMissionTab();
			base.UIOpened(true);
		}

		// Token: 0x060074A1 RID: 29857 RVA: 0x0026CBA0 File Offset: 0x0026ADA0
		private void SetMissionTab()
		{
			if (NKMMissionManager.GetTrackingMissionTemplet() == null)
			{
				NKMMissionManager.SetDefaultTrackingMissionToGrowthMission();
			}
			this.m_trackingMissionTemplet = NKMMissionManager.GetTrackingMissionTemplet();
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0) || this.m_trackingMissionTemplet == null || !NKCTutorialManager.TutorialCompleted(TutorialStep.Achieventment))
			{
				NKCUtil.SetGameobjectActive(this.m_objTrackMission, false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgMissionBG, NKCUtil.GetMissionThumbnailSprite(this.m_trackingMissionTemplet), true);
			NKCUtil.SetImageSprite(this.m_imgMissionIcon, NKCUtil.GetGrowthMissionHamburgerIconSprite(this.m_trackingMissionTemplet), true);
			NKCUtil.SetLabelText(this.m_lbMissionTitle, this.m_trackingMissionTemplet.GetTitle());
			NKCUtil.SetLabelText(this.m_lbMissionDesc, this.m_trackingMissionTemplet.GetDesc());
			long num = 0L;
			long num2 = 0L;
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(this.m_trackingMissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objTrackMission, false);
				return;
			}
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.GROWTH_COMPLETE)
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(this.m_trackingMissionTemplet.m_MissionRequire);
				if (missionTemplet != null)
				{
					num = (long)NKMMissionManager.GetMissionTempletListByType(missionTemplet.m_MissionTabId).Count;
				}
				num2 = num;
			}
			else
			{
				num = this.m_trackingMissionTemplet.m_Times;
				if (NKMMissionManager.IsCumulativeCondition(this.m_trackingMissionTemplet.m_MissionCond.mission_cond))
				{
					NKMMissionData missionData = NKCScenManager.CurrentUserData().m_MissionData.GetMissionData(this.m_trackingMissionTemplet);
					if (missionData != null)
					{
						num2 = Math.Min(missionData.times, num);
					}
				}
				else
				{
					num2 = NKMMissionManager.GetNonCumulativeMissionTimes(this.m_trackingMissionTemplet, NKCScenManager.GetScenManager().GetMyUserData(), false);
				}
			}
			NKCUtil.SetLabelText(this.m_lbProgress, string.Format("{0} / {1}", num2, num));
			this.m_sliderProgress.value = (float)num2 / (float)num;
			new List<NKMMissionTemplet>();
			int num3;
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.GROWTH_COMPLETE)
			{
				num3 = NKMMissionManager.GetMissionTempletListByType(NKMMissionManager.GetMissionTemplet(this.m_trackingMissionTemplet.m_MissionRequire).m_MissionTabId).Count;
			}
			else
			{
				num3 = NKMMissionManager.GetMissionTempletListByType(this.m_trackingMissionTemplet.m_MissionTabId).FindIndex((NKMMissionTemplet x) => x == this.m_trackingMissionTemplet);
			}
			NKCUtil.SetLabelText(this.m_lbMissionNum, string.Format(NKCUtilString.GET_STRING_POPUP_HAMBER_MENU_MISSION_TITLE_DESC_01, num3 + 1));
			for (int i = 0; i < this.m_trackingMissionTemplet.m_MissionReward.Count; i++)
			{
				MissionReward missionReward = this.m_trackingMissionTemplet.m_MissionReward[i];
				this.m_lstRewardSlot[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(missionReward.reward_type, missionReward.reward_id, missionReward.reward_value, 0), true, null);
				this.m_lstRewardSlot[i].SetActive(true);
			}
			for (int j = this.m_trackingMissionTemplet.m_MissionReward.Count; j < this.m_lstRewardSlot.Count; j++)
			{
				this.m_lstRewardSlot[j].SetActive(false);
			}
			NKCUtil.SetGameobjectActive(this.m_btnQuickMove.gameObject, num2 < num && this.m_trackingMissionTemplet.m_ShortCutType > NKM_SHORTCUT_TYPE.SHORTCUT_NONE);
			NKCUtil.SetGameobjectActive(this.m_btnComplete.gameObject, num2 >= num);
			NKCUtil.SetGameobjectActive(this.m_objTrackMission, true);
		}

		// Token: 0x060074A2 RID: 29858 RVA: 0x0026CEA4 File Offset: 0x0026B0A4
		private void SetGuildData()
		{
			NKCPopupHamburgerMenuGuildButton btnGuild = this.m_btnGuild;
			if (btnGuild == null)
			{
				return;
			}
			btnGuild.SetGuildData();
		}

		// Token: 0x060074A3 RID: 29859 RVA: 0x0026CEB8 File Offset: 0x0026B0B8
		private void SetUserBuffCount()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				NKCCompanyBuff.RemoveExpiredBuffs(nkmuserData.m_companyBuffDataList);
				int count = nkmuserData.m_companyBuffDataList.Count;
				NKCUtil.SetGameobjectActive(this.m_btnUserBuff, count > 0);
				NKCUtil.SetLabelText(this.m_lbBuffCount, count.ToString());
				bool bValue = NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT) || NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT);
				NKCUtil.SetGameobjectActive(this.m_objFactoryEvent, bValue);
			}
		}

		// Token: 0x060074A4 RID: 29860 RVA: 0x0026CF34 File Offset: 0x0026B134
		public void Refresh()
		{
			this.SetUserBuffCount();
			this.SetMissionTab();
			NKMUserData userData = NKCScenManager.CurrentUserData();
			foreach (KeyValuePair<NKCPopupHamburgerMenu.eButtonMenuType, NKCPopupHamburgerMenuSimpleButton> keyValuePair in this.m_dicButtons)
			{
				keyValuePair.Value.UpdateData(userData);
			}
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x0026CFA0 File Offset: 0x0026B1A0
		public void OnRecv(NKMPacket_MISSION_COMPLETE_ACK cNKMPacket_MISSION_COMPLETE_ACK)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKCUIResult.OnClose onClose = null;
			if (NKCGameEventManager.IsWaiting())
			{
				onClose = new NKCUIResult.OnClose(NKCGameEventManager.WaitFinished);
			}
			NKCUIResult.Instance.OpenComplexResult(myUserData.m_ArmyData, cNKMPacket_MISSION_COMPLETE_ACK.rewardDate, onClose, 0L, null, false, false);
		}

		// Token: 0x060074A6 RID: 29862 RVA: 0x0026CFEA File Offset: 0x0026B1EA
		private void SetNewMail(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objNewMail, bValue);
		}

		// Token: 0x060074A7 RID: 29863 RVA: 0x0026CFF8 File Offset: 0x0026B1F8
		private void OnBtnBuff()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				NKCCompanyBuff.RemoveExpiredBuffs(nkmuserData.m_companyBuffDataList);
				if (nkmuserData.m_companyBuffDataList.Count > 0)
				{
					base.Close();
					NKCPopupCompanyBuff.Instance.Open();
					return;
				}
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_LOBBY_USER_BUFF_NONE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x0026D04B File Offset: 0x0026B24B
		private void OnBtnMail()
		{
			if (NKCUIMail.IsInstanceOpen)
			{
				return;
			}
			base.Close();
			NKCUIMail.Instance.Open();
		}

		// Token: 0x060074A9 RID: 29865 RVA: 0x0026D065 File Offset: 0x0026B265
		private void OnBtnOption()
		{
			base.Close();
			NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.NORMAL, null);
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x0026D079 File Offset: 0x0026B279
		private void OnBtnClose()
		{
			base.Close();
		}

		// Token: 0x060074AB RID: 29867 RVA: 0x0026D081 File Offset: 0x0026B281
		private void OnBtnGuild()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_GUILD, "", false);
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x0026D098 File Offset: 0x0026B298
		private void OnBtnChat()
		{
			if (NKMOpenTagManager.IsOpened("CHAT_PRIVATE"))
			{
				bool flag;
				NKCContentManager.eContentStatus eContentStatus = NKCContentManager.CheckContentStatus(ContentsType.FRIENDS, out flag, 0, 0);
				if (eContentStatus == NKCContentManager.eContentStatus.Open)
				{
					if (NKCScenManager.GetScenManager().GetGameOptionData().UseChatContent)
					{
						base.Close();
						NKCPopupPrivateChatLobby.Instance.Open(0L);
						return;
					}
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_OPTION_GAME_CHAT_NOTICE, null, "");
					return;
				}
				else if (eContentStatus == NKCContentManager.eContentStatus.Lock)
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FRIENDS, 0);
					return;
				}
			}
			else
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COMING_SOON_SYSTEM, null, "");
			}
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x0026D11D File Offset: 0x0026B31D
		private void OnBtnFriends()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_FRIEND_ADD, "", false);
		}

		// Token: 0x060074AE RID: 29870 RVA: 0x0026D134 File Offset: 0x0026B334
		private void OnBtnHangar()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, NKMOfficeRoomTemplet.RoomType.Hangar.ToString(), false);
		}

		// Token: 0x060074AF RID: 29871 RVA: 0x0026D15E File Offset: 0x0026B35E
		private void OnBtnInventory()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_INVENTORY, "", false);
		}

		// Token: 0x060074B0 RID: 29872 RVA: 0x0026D174 File Offset: 0x0026B374
		private void OnBtnOfficeDorm()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, NKCUIOfficeMapFront.SectionType.Room.ToString(), false);
		}

		// Token: 0x060074B1 RID: 29873 RVA: 0x0026D1A0 File Offset: 0x0026B3A0
		private void OnBtnOfficeFacility()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, NKCUIOfficeMapFront.SectionType.Facility.ToString(), false);
		}

		// Token: 0x060074B2 RID: 29874 RVA: 0x0026D1CA File Offset: 0x0026B3CA
		private void OnBtnCollection()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION, "", false);
		}

		// Token: 0x060074B3 RID: 29875 RVA: 0x0026D1E0 File Offset: 0x0026B3E0
		private void OnBtnFactory()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, NKMOfficeRoomTemplet.RoomType.Forge.ToString(), false);
		}

		// Token: 0x060074B4 RID: 29876 RVA: 0x0026D20A File Offset: 0x0026B40A
		private void OnBtnUnitList()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_UNITLIST, "", false);
		}

		// Token: 0x060074B5 RID: 29877 RVA: 0x0026D220 File Offset: 0x0026B420
		private void OnBtnPersonnel()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, NKMOfficeRoomTemplet.RoomType.CEO.ToString(), false);
		}

		// Token: 0x060074B6 RID: 29878 RVA: 0x0026D24C File Offset: 0x0026B44C
		private void OnBtnLab()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, NKMOfficeRoomTemplet.RoomType.Lab.ToString(), false);
		}

		// Token: 0x060074B7 RID: 29879 RVA: 0x0026D276 File Offset: 0x0026B476
		private void OnBtnDeckSetup()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_DECKSETUP, "", false);
		}

		// Token: 0x060074B8 RID: 29880 RVA: 0x0026D28B File Offset: 0x0026B48B
		private void OnBtnPvp()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_PVP_MAIN, "", false);
		}

		// Token: 0x060074B9 RID: 29881 RVA: 0x0026D29F File Offset: 0x0026B49F
		private void OnBtnWorldmap()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_WORLDMAP_MISSION, "", false);
		}

		// Token: 0x060074BA RID: 29882 RVA: 0x0026D2B3 File Offset: 0x0026B4B3
		private void OnBtnOperation()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_MAINSTREAM, "", false);
		}

		// Token: 0x060074BB RID: 29883 RVA: 0x0026D2C7 File Offset: 0x0026B4C7
		private void OnBtnMission()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_MISSION, "", false);
		}

		// Token: 0x060074BC RID: 29884 RVA: 0x0026D2DC File Offset: 0x0026B4DC
		private void OnBtnContract()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_CONTRACT, "", false);
		}

		// Token: 0x060074BD RID: 29885 RVA: 0x0026D2F0 File Offset: 0x0026B4F0
		private void OnBtnBase()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_BASEMAIN, "", false);
		}

		// Token: 0x060074BE RID: 29886 RVA: 0x0026D305 File Offset: 0x0026B505
		private void OnBtnShop()
		{
			base.Close();
			if (NKCShopCategoryTemplet.Find(NKCShopManager.ShopTabCategory.PACKAGE) != null)
			{
				NKCUIShop.Instance.Open(NKCShopManager.ShopTabCategory.PACKAGE, "TAB_MAIN", 0, 0, NKCUIShop.eTabMode.Fold);
				return;
			}
			NKCUIShop.Instance.Open(NKCShopManager.ShopTabCategory.EXCHANGE, "TAB_SUPPLY", 0, 0, NKCUIShop.eTabMode.Fold);
		}

		// Token: 0x060074BF RID: 29887 RVA: 0x0026D33C File Offset: 0x0026B53C
		private void OnBtnRanking()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_RANKING, "", false);
		}

		// Token: 0x060074C0 RID: 29888 RVA: 0x0026D354 File Offset: 0x0026B554
		private void OnBtnQuickMove()
		{
			if (NKCUIForge.IsInstanceOpen && NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					this.BtnQuickMove();
				}, null, false);
				return;
			}
			if (NKCUIForge.IsInstanceOpen && NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_SET_OPTION_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					this.BtnQuickMove();
				}, null, false);
				return;
			}
			if (NKCPopupShipCommandModule.IsInstanceOpen && NKCScenManager.GetScenManager().GetMyUserData().GetShipCandidateData().shipUid > 0L)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHIP_COMMANDMODULE_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ();
					this.BtnQuickMove();
				}, null, false);
				return;
			}
			this.BtnQuickMove();
		}

		// Token: 0x060074C1 RID: 29889 RVA: 0x0026D410 File Offset: 0x0026B610
		private void BtnQuickMove()
		{
			NKMMissionTemplet trackingMissionTemplet = NKMMissionManager.GetTrackingMissionTemplet();
			NKMUserData userData = NKCScenManager.CurrentUserData();
			if (trackingMissionTemplet != null)
			{
				bool flag = false;
				NKMMissionData missionData = NKCScenManager.CurrentUserData().m_MissionData.GetMissionData(trackingMissionTemplet);
				if (NKMMissionManager.IsCumulativeCondition(trackingMissionTemplet.m_MissionCond.mission_cond))
				{
					if (missionData != null)
					{
						flag = (NKMMissionManager.CanComplete(trackingMissionTemplet, userData, missionData) == NKM_ERROR_CODE.NEC_OK);
					}
				}
				else
				{
					flag = (trackingMissionTemplet.m_MissionCond.mission_cond == NKM_MISSION_COND.JUST_OPEN || NKMMissionManager.CanComplete(trackingMissionTemplet, userData, missionData) == NKM_ERROR_CODE.NEC_OK);
				}
				if (flag)
				{
					NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(trackingMissionTemplet.m_MissionTabId, trackingMissionTemplet.m_GroupId, trackingMissionTemplet.m_MissionID);
					return;
				}
				base.Close();
				NKCContentManager.MoveToShortCut(trackingMissionTemplet.m_ShortCutType, trackingMissionTemplet.m_ShortCut, false);
			}
		}

		// Token: 0x060074C2 RID: 29890 RVA: 0x0026D4B7 File Offset: 0x0026B6B7
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
		}

		// Token: 0x060074C3 RID: 29891 RVA: 0x0026D4B9 File Offset: 0x0026B6B9
		public override void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipItem)
		{
		}

		// Token: 0x060074C4 RID: 29892 RVA: 0x0026D4BB File Offset: 0x0026B6BB
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
		}

		// Token: 0x060074C5 RID: 29893 RVA: 0x0026D4BD File Offset: 0x0026B6BD
		public override void OnUserLevelChanged(NKMUserData newUserData)
		{
		}

		// Token: 0x060074C6 RID: 29894 RVA: 0x0026D4BF File Offset: 0x0026B6BF
		public override void OnGuildDataChanged()
		{
			this.SetGuildData();
		}

		// Token: 0x060074C7 RID: 29895 RVA: 0x0026D4C7 File Offset: 0x0026B6C7
		public void OnUserBuffChanged()
		{
			this.SetUserBuffCount();
		}

		// Token: 0x040060F3 RID: 24819
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x040060F4 RID: 24820
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_HAMBURGER_MENU";

		// Token: 0x040060F5 RID: 24821
		private static NKCPopupHamburgerMenu m_Instance;

		// Token: 0x040060F6 RID: 24822
		public Animator m_ani;

		// Token: 0x040060F7 RID: 24823
		public NKCUIComStateButton m_btnBackground;

		// Token: 0x040060F8 RID: 24824
		[Header("Top")]
		public Text m_lbUserName;

		// Token: 0x040060F9 RID: 24825
		public NKCUIComStateButton m_btnUserBuff;

		// Token: 0x040060FA RID: 24826
		public Text m_lbBuffCount;

		// Token: 0x040060FB RID: 24827
		public NKCUIComStateButton m_btnMail;

		// Token: 0x040060FC RID: 24828
		public GameObject m_objNewMail;

		// Token: 0x040060FD RID: 24829
		public NKCUIComStateButton m_btnOption;

		// Token: 0x040060FE RID: 24830
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040060FF RID: 24831
		[Header("")]
		public NKCPopupHamburgerMenuGuildButton m_btnGuild;

		// Token: 0x04006100 RID: 24832
		public NKCPopupHamburgerMenuSimpleButton m_btnChat;

		// Token: 0x04006101 RID: 24833
		[Header("Line 1")]
		public NKCPopupHamburgerMenuSimpleButton m_btnCollection;

		// Token: 0x04006102 RID: 24834
		public NKCPopupHamburgerMenuSimpleButton m_btnOfficeDorm;

		// Token: 0x04006103 RID: 24835
		public NKCPopupHamburgerMenuSimpleButton m_btnInventory;

		// Token: 0x04006104 RID: 24836
		[Header("Line 2")]
		public NKCPopupHamburgerMenuSimpleButton m_btnHangar;

		// Token: 0x04006105 RID: 24837
		public NKCPopupHamburgerMenuSimpleButton m_btnLab;

		// Token: 0x04006106 RID: 24838
		public NKCPopupHamburgerMenuSimpleButton m_btnUnitList;

		// Token: 0x04006107 RID: 24839
		[Header("Line 3")]
		public NKCPopupHamburgerMenuSimpleButton m_btnPersonnel;

		// Token: 0x04006108 RID: 24840
		public NKCPopupHamburgerMenuSimpleButton m_btnFactory;

		// Token: 0x04006109 RID: 24841
		public GameObject m_objFactoryEvent;

		// Token: 0x0400610A RID: 24842
		public NKCPopupHamburgerMenuSimpleButton m_btnDeckSetup;

		// Token: 0x0400610B RID: 24843
		[Header("Line 4")]
		public NKCPopupHamburgerMenuSimpleButton m_btnPvp;

		// Token: 0x0400610C RID: 24844
		public NKCPopupHamburgerMenuSimpleButton m_btnWorldmap;

		// Token: 0x0400610D RID: 24845
		public NKCPopupHamburgerMenuSimpleButton m_btnOperation;

		// Token: 0x0400610E RID: 24846
		[Header("DownLeft")]
		public NKCPopupHamburgerMenuSimpleButton m_btnFriends;

		// Token: 0x0400610F RID: 24847
		public NKCPopupHamburgerMenuSimpleButton m_btnOfficeFacility;

		// Token: 0x04006110 RID: 24848
		public NKCPopupHamburgerMenuSimpleButton m_btnMission;

		// Token: 0x04006111 RID: 24849
		public NKCPopupHamburgerMenuSimpleButton m_btnRanking;

		// Token: 0x04006112 RID: 24850
		[Header("DownRight")]
		public NKCPopupHamburgerMenuSimpleButton m_btnContract;

		// Token: 0x04006113 RID: 24851
		public NKCPopupHamburgerMenuShop m_btnShop;

		// Token: 0x04006114 RID: 24852
		[Header("추적중인 미션")]
		public GameObject m_objTrackMission;

		// Token: 0x04006115 RID: 24853
		public Image m_imgMissionBG;

		// Token: 0x04006116 RID: 24854
		public Image m_imgMissionIcon;

		// Token: 0x04006117 RID: 24855
		public Text m_lbMissionNum;

		// Token: 0x04006118 RID: 24856
		public Text m_lbMissionTitle;

		// Token: 0x04006119 RID: 24857
		public Text m_lbMissionDesc;

		// Token: 0x0400611A RID: 24858
		public Text m_lbProgress;

		// Token: 0x0400611B RID: 24859
		public Slider m_sliderProgress;

		// Token: 0x0400611C RID: 24860
		public List<NKCUISlot> m_lstRewardSlot;

		// Token: 0x0400611D RID: 24861
		public NKCUIComStateButton m_btnQuickMove;

		// Token: 0x0400611E RID: 24862
		public NKCUIComStateButton m_btnComplete;

		// Token: 0x0400611F RID: 24863
		private Dictionary<NKCPopupHamburgerMenu.eButtonMenuType, NKCPopupHamburgerMenuSimpleButton> m_dicButtons = new Dictionary<NKCPopupHamburgerMenu.eButtonMenuType, NKCPopupHamburgerMenuSimpleButton>();

		// Token: 0x04006120 RID: 24864
		private NKMMissionTemplet m_trackingMissionTemplet;

		// Token: 0x020017B6 RID: 6070
		public enum eButtonMenuType
		{
			// Token: 0x0400A757 RID: 42839
			Guild,
			// Token: 0x0400A758 RID: 42840
			Chat,
			// Token: 0x0400A759 RID: 42841
			Friends,
			// Token: 0x0400A75A RID: 42842
			Hangar,
			// Token: 0x0400A75B RID: 42843
			Inventory,
			// Token: 0x0400A75C RID: 42844
			Collection,
			// Token: 0x0400A75D RID: 42845
			Factory,
			// Token: 0x0400A75E RID: 42846
			UnitList,
			// Token: 0x0400A75F RID: 42847
			Personnel,
			// Token: 0x0400A760 RID: 42848
			Lab,
			// Token: 0x0400A761 RID: 42849
			DeckSetup,
			// Token: 0x0400A762 RID: 42850
			Pvp,
			// Token: 0x0400A763 RID: 42851
			Worldmap,
			// Token: 0x0400A764 RID: 42852
			Operation,
			// Token: 0x0400A765 RID: 42853
			Mission,
			// Token: 0x0400A766 RID: 42854
			Contract,
			// Token: 0x0400A767 RID: 42855
			Base,
			// Token: 0x0400A768 RID: 42856
			Shop,
			// Token: 0x0400A769 RID: 42857
			OfficeDorm,
			// Token: 0x0400A76A RID: 42858
			OfficeFacility,
			// Token: 0x0400A76B RID: 42859
			Ranking
		}
	}
}
