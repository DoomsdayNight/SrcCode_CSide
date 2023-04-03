using System;
using System.Collections.Generic;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.Templet;
using NKC.UI.Collection;
using NKC.UI.Option;
using NKC.UI.Shop;
using NKM;
using NKM.Event;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C1D RID: 3101
	public class NKCUILobbyV2 : NKCUIBase
	{
		// Token: 0x06008F4B RID: 36683 RVA: 0x0030BAC8 File Offset: 0x00309CC8
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUILobbyV2.s_LoadedUIData))
			{
				NKCUILobbyV2.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUILobbyV2>("ab_ui_nkm_ui_lobby", "NUF_HOME_PREFAB_RENEWAL", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUILobbyV2.CleanupInstance));
			}
			return NKCUILobbyV2.s_LoadedUIData;
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06008F4C RID: 36684 RVA: 0x0030BAFC File Offset: 0x00309CFC
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUILobbyV2.s_LoadedUIData != null && NKCUILobbyV2.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06008F4D RID: 36685 RVA: 0x0030BB11 File Offset: 0x00309D11
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUILobbyV2.s_LoadedUIData != null && NKCUILobbyV2.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06008F4E RID: 36686 RVA: 0x0030BB26 File Offset: 0x00309D26
		public static NKCUILobbyV2 GetInstance()
		{
			if (NKCUILobbyV2.s_LoadedUIData != null && NKCUILobbyV2.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUILobbyV2.s_LoadedUIData.GetInstance<NKCUILobbyV2>();
			}
			return null;
		}

		// Token: 0x06008F4F RID: 36687 RVA: 0x0030BB47 File Offset: 0x00309D47
		public static void CleanupInstance()
		{
			NKCUILobbyV2.s_LoadedUIData = null;
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06008F50 RID: 36688 RVA: 0x0030BB4F File Offset: 0x00309D4F
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06008F51 RID: 36689 RVA: 0x0030BB52 File Offset: 0x00309D52
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06008F52 RID: 36690 RVA: 0x0030BB55 File Offset: 0x00309D55
		public override string MenuName
		{
			get
			{
				return "MainLobbyV2";
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06008F53 RID: 36691 RVA: 0x0030BB5C File Offset: 0x00309D5C
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06008F54 RID: 36692 RVA: 0x0030BB60 File Offset: 0x00309D60
		public void Init()
		{
			this.RegisterButtons();
			this.m_UILobby3D.Init();
			base.gameObject.SetActive(false);
			NKCUtil.SetGameobjectActive(this.m_UILobby3D, false);
			this.m_UIUserInfo.Init();
			this.m_UIEventPanel.Init();
			this.m_trHideBtnRoot = this.m_tglUIHide.transform.parent;
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglUIHide, new UnityAction<bool>(this.OnTglUIHide));
			this.m_tglUIHide.Select(true, true, true);
		}

		// Token: 0x06008F55 RID: 36693 RVA: 0x0030BBE8 File Offset: 0x00309DE8
		public override void OnBackButton()
		{
			if (this.m_hideUI)
			{
				this.TryUIUnhide();
				return;
			}
			this.OpenQuitApplicationPopup();
		}

		// Token: 0x06008F56 RID: 36694 RVA: 0x0030BC00 File Offset: 0x00309E00
		public override void CloseInternal()
		{
			if (this.m_hideUI)
			{
				this.TryUIUnhide();
			}
			this.SetPlayIntro(false);
			this.m_UILobby3D.transform.SetParent(base.transform);
			this.m_UILobby3D.CleanUp();
			base.gameObject.SetActive(false);
			this.CleanUpButtons();
		}

		// Token: 0x06008F57 RID: 36695 RVA: 0x0030BC55 File Offset: 0x00309E55
		public override void Hide()
		{
			base.Hide();
			this.m_CanvasGroup.alpha = 1f;
			NKCUtil.SetGameobjectActive(this.m_UILobby3D, false);
			this.m_UILobby3D.m_MenuCanvasGroup.alpha = 1f;
		}

		// Token: 0x06008F58 RID: 36696 RVA: 0x0030BC90 File Offset: 0x00309E90
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_UILobby3D, true);
			this.m_UILobby3D.SetData(NKCScenManager.CurrentUserData());
			this.m_UIUserInfo.SetData(NKCScenManager.CurrentUserData());
			this.m_UIEventPanel.CheckReddot();
			this.m_UIEventPanel.SetData(NKCScenManager.CurrentUserData());
			this.UpdateAllButtons(NKCScenManager.CurrentUserData());
			this.UpdateEventIntervalSlot();
			this.SetUIVisible(true);
			this.SetPlayIntro(true);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_HOME().OnHomeEnter();
				NKCSoundManager.PlayScenMusic(NKM_SCEN_ID.NSI_HOME, false);
			}
		}

		// Token: 0x06008F59 RID: 36697 RVA: 0x0030BD2C File Offset: 0x00309F2C
		private void RegisterButtons()
		{
			if (this.m_UIMail != null)
			{
				this.m_UIMail.Init();
				this.RegisterButton(NKCUILobbyV2.eUIMenu.Mail, this.m_UIMail);
			}
			if (this.m_UIOption != null)
			{
				this.m_UIOption.Init(null, new NKCUILobbySimpleMenu.OnButton(this.OnMenuOption), ContentsType.None);
				this.RegisterButton(NKCUILobbyV2.eUIMenu.Option, this.m_UIOption);
			}
			if (this.m_btnHamburger != null)
			{
				this.m_btnHamburger.PointerClick.RemoveAllListeners();
				this.m_btnHamburger.PointerClick.AddListener(new UnityAction(this.OnClickHamburger));
				NKCUtil.SetHotkey(this.m_btnHamburger, HotkeyEventType.HamburgerMenu, this, false);
			}
			if (this.m_UIOperation != null)
			{
				this.m_UIOperation.Init();
				this.RegisterButton(NKCUILobbyV2.eUIMenu.Operation, this.m_UIOperation);
			}
			if (this.m_UICurrentEvent != null)
			{
				this.m_UICurrentEvent.Init();
				this.RegisterButton(NKCUILobbyV2.eUIMenu.CurrentEvent, this.m_UICurrentEvent);
			}
			if (this.m_UIEventPass != null)
			{
				this.m_UIEventPass.Init(ContentsType.COUNTER_PASS);
				this.RegisterButton(NKCUILobbyV2.eUIMenu.CounterPass, this.m_UIEventPass);
			}
			if (this.m_UIPVP != null)
			{
				this.m_UIPVP.Init(ContentsType.PVP);
				this.RegisterButton(NKCUILobbyV2.eUIMenu.PVP, this.m_UIPVP);
			}
			if (this.m_UIWorldmap != null)
			{
				this.m_UIWorldmap.Init(ContentsType.WORLDMAP);
				this.RegisterButton(NKCUILobbyV2.eUIMenu.Worldmap, this.m_UIWorldmap);
			}
			if (this.m_lstUIEventIndexSlot != null)
			{
				foreach (NKCUILobbyEventIndexSlot nkcuilobbyEventIndexSlot in this.m_lstUIEventIndexSlot)
				{
					if (nkcuilobbyEventIndexSlot != null)
					{
						nkcuilobbyEventIndexSlot.Init();
					}
				}
			}
			this.RegisterButton(this.m_UIAttendance, NKCUILobbyV2.eUIMenu.Attendance, ContentsType.ATTENDANCE, new NKCUILobbySimpleMenu.DotEnableConditionFunction(this.ChechAttendanceEnable), new NKCUILobbySimpleMenu.OnButton(this.OnBtnAttendance));
			this.RegisterButton(this.m_UINotice, NKCUILobbyV2.eUIMenu.Event, ContentsType.ATTENDANCE, null, new NKCUILobbySimpleMenu.OnButton(this.OnBtnNotice));
			this.RegisterButton(this.m_UIChat, NKCUILobbyV2.eUIMenu.Chat, ContentsType.FRIENDS, new NKCUILobbySimpleMenu.DotEnableConditionFunction(NKCAlarmManager.CheckChatNotify), new NKCUILobbySimpleMenu.OnButton(this.OnBtnChat));
			NKCUtil.SetGameobjectActive(this.m_csbtnInfo, NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong && (NKMContentsVersionManager.HasCountryTag(CountryTagType.TWN) || NKMContentsVersionManager.HasCountryTag(CountryTagType.CHN)));
			if (this.m_csbtnInfo != null)
			{
				this.m_csbtnInfo.PointerClick.RemoveAllListeners();
				this.m_csbtnInfo.PointerClick.AddListener(new UnityAction(this.OnClickInfoBtn));
			}
			if (this.m_UISurvey != null)
			{
				this.m_UISurvey.Init();
				this.RegisterButton(NKCUILobbyV2.eUIMenu.Survey, this.m_UISurvey);
			}
			if (this.m_UIShop != null)
			{
				this.m_UIShop.Init(new NKCUILobbyMenuShop.DotEnableConditionFunction(this.HasSpecialItem), new NKCUILobbyMenuShop.OnButton(this.OnBtnShop), ContentsType.CASH_SHOP);
				this.RegisterButton(NKCUILobbyV2.eUIMenu.Shop, this.m_UIShop);
			}
			this.RegisterButton(this.m_UIMissionGuide, NKCUILobbyV2.eUIMenu.GuideMission, ContentsType.MISSION, new NKCUILobbySimpleMenu.DotEnableConditionFunction(this.HasClearableMissionGuide), new NKCUILobbySimpleMenu.OnButton(this.OnBtnMissionGuide));
			this.RegisterButton(this.m_UIMission, NKCUILobbyV2.eUIMenu.Mission, ContentsType.MISSION, new NKCUILobbySimpleMenu.DotEnableConditionFunction(this.HasClearableMission), new NKCUILobbySimpleMenu.OnButton(this.OnBtnMission));
			this.RegisterButton(this.m_UICollection, NKCUILobbyV2.eUIMenu.Collection, ContentsType.COLLECTION, new NKCUILobbySimpleMenu.DotEnableConditionFunction(this.HasCollectionReward), new NKCUILobbySimpleMenu.OnButton(this.OnBtnCollection));
			this.RegisterButton(this.m_UIGuild, NKCUILobbyV2.eUIMenu.Guild, ContentsType.GUILD, new NKCUILobbySimpleMenu.DotEnableConditionFunction(NKCAlarmManager.CheckGuildNotify), new NKCUILobbySimpleMenu.OnButton(this.OnBtnGuild));
			this.RegisterButton(this.m_UIBase, NKCUILobbyV2.eUIMenu.Base, ContentsType.BASE, new NKCUILobbySimpleMenu.DotEnableConditionFunction(this.BaseNotifyCondition), new NKCUILobbySimpleMenu.OnButton(this.OnMenuBase));
			this.RegisterButton(this.m_UIInventory, NKCUILobbyV2.eUIMenu.Inventory, ContentsType.None, new NKCUILobbySimpleMenu.DotEnableConditionFunction(this.HasInventoryNotify), new NKCUILobbySimpleMenu.OnButton(this.OnMenuInventory));
			this.RegisterButton(this.m_UIUnitList, NKCUILobbyV2.eUIMenu.UnitList, ContentsType.None, null, new NKCUILobbySimpleMenu.OnButton(this.OnMenuUnitList));
			this.RegisterButton(this.m_UIDeckSetup, NKCUILobbyV2.eUIMenu.DeckSetup, ContentsType.DECKVIEW, null, new NKCUILobbySimpleMenu.OnButton(this.OnMenuDeckSetup));
			this.RegisterButton(this.m_UIContract, NKCUILobbyV2.eUIMenu.Contract, ContentsType.CONTRACT, new NKCUILobbySimpleMenu.DotEnableConditionFunction(this.CheckFreeContract), new NKCUILobbySimpleMenu.OnButton(this.OnBtnContract));
			List<NKCLobbyIconTemplet> availableLobbyIconTemplet = NKCLobbyIconManager.GetAvailableLobbyIconTemplet();
			if (this.m_lstUIShortCutButton != null && availableLobbyIconTemplet.Count > 0)
			{
				for (int i = 0; i < this.m_lstUIShortCutButton.Count; i++)
				{
					if (i < availableLobbyIconTemplet.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_lstUIShortCutButton[i], true);
						this.m_lstUIShortCutButton[i].Init(null, new NKCUILobbyEventShortCut.OnButton(this.OnBtnShortCut), availableLobbyIconTemplet[i]);
						this.RegisterButton(availableLobbyIconTemplet[i].m_ShortCutType, this.m_lstUIShortCutButton[i]);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstUIShortCutButton[i], false);
					}
				}
			}
		}

		// Token: 0x06008F5A RID: 36698 RVA: 0x0030C21C File Offset: 0x0030A41C
		public void Open(NKMUserData userData)
		{
			base.gameObject.SetActive(true);
			this.m_UILobby3D.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas));
			NKCUtil.SetGameobjectActive(this.m_UILobby3D, true);
			this.m_UILobby3D.transform.localPosition = Vector3.zero;
			this.m_UILobby3D.transform.localRotation = Quaternion.identity;
			this.m_UILobby3D.transform.localScale = Vector3.one;
			if (userData != null)
			{
				this.UpdateAllButtons(userData);
				this.m_UILobby3D.SetData(userData);
				this.m_UIUserInfo.SetData(userData);
				this.m_UIEventPanel.SetData(userData);
				bool bValue = NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0);
				NKCUtil.SetGameobjectActive(this.m_UIEventPanel, bValue);
				NKCUtil.SetGameobjectActive(this.m_objBottomRightMenu, bValue);
			}
			this.UpdateEventIntervalSlot();
			base.UIOpened(true);
			this.SetUIVisible(true);
			this.SetPlayIntro(true);
		}

		// Token: 0x06008F5B RID: 36699 RVA: 0x0030C300 File Offset: 0x0030A500
		public void RegisterButton(NKCUILobbyV2.eUIMenu menu, NKCUILobbyMenuButtonBase button)
		{
			if (button == null)
			{
				Debug.LogError(menu.ToString() + " button null!");
				return;
			}
			this.m_dicButton.Add(menu, button);
		}

		// Token: 0x06008F5C RID: 36700 RVA: 0x0030C335 File Offset: 0x0030A535
		public void RegisterButton(NKCUILobbySimpleMenu button, NKCUILobbyV2.eUIMenu menuType, ContentsType contentsType, NKCUILobbySimpleMenu.DotEnableConditionFunction conditionFunc = null, NKCUILobbySimpleMenu.OnButton onButton = null)
		{
			if (button == null)
			{
				return;
			}
			button.Init(conditionFunc, onButton, contentsType);
			this.RegisterButton(menuType, button);
		}

		// Token: 0x06008F5D RID: 36701 RVA: 0x0030C354 File Offset: 0x0030A554
		public void RegisterButton(NKM_SHORTCUT_TYPE shortCut, NKCUILobbyMenuButtonBase button)
		{
			this.m_dicShortCutButton.Add(shortCut, button);
		}

		// Token: 0x06008F5E RID: 36702 RVA: 0x0030C364 File Offset: 0x0030A564
		private NKCUILobbyMenuButtonBase GetButton(NKCUILobbyV2.eUIMenu menu)
		{
			NKCUILobbyMenuButtonBase result;
			if (this.m_dicButton.TryGetValue(menu, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06008F5F RID: 36703 RVA: 0x0030C384 File Offset: 0x0030A584
		private NKCUILobbyMenuButtonBase GetButton(NKM_SHORTCUT_TYPE shortCut)
		{
			NKCUILobbyMenuButtonBase result;
			if (this.m_dicShortCutButton.TryGetValue(shortCut, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06008F60 RID: 36704 RVA: 0x0030C3A4 File Offset: 0x0030A5A4
		public void UpdateAllButtons(NKMUserData userData)
		{
			bool bValue = NKCContentManager.IsContentsUnlocked(ContentsType.ATTENDANCE, 0, 0);
			NKCUtil.SetGameobjectActive(this.m_UIAttendance, bValue);
			NKCUtil.SetGameobjectActive(this.m_UINotice, bValue);
			NKCUtil.SetLabelText(this.m_lbMissionGuideCompleteCnt, NKMMissionManager.GetGuideMissionClearCount().ToString());
			NKCUtil.SetGameobjectActive(this.m_objMissionGuideAllComplete, NKMMissionManager.IsGuideMissionAllClear());
			foreach (KeyValuePair<NKCUILobbyV2.eUIMenu, NKCUILobbyMenuButtonBase> keyValuePair in this.m_dicButton)
			{
				keyValuePair.Value.UpdateData(userData);
			}
			foreach (KeyValuePair<NKM_SHORTCUT_TYPE, NKCUILobbyMenuButtonBase> keyValuePair2 in this.m_dicShortCutButton)
			{
				if (keyValuePair2.Value != null)
				{
					keyValuePair2.Value.UpdateData(userData);
				}
			}
			this.UpdateHamburgerNotify();
		}

		// Token: 0x06008F61 RID: 36705 RVA: 0x0030C4A8 File Offset: 0x0030A6A8
		public void UpdateButton(NKCUILobbyV2.eUIMenu menu, NKMUserData userData)
		{
			NKCUILobbyMenuButtonBase button = this.GetButton(menu);
			if (button != null)
			{
				button.UpdateData(userData);
			}
			this.UpdateHamburgerNotify();
		}

		// Token: 0x06008F62 RID: 36706 RVA: 0x0030C4D4 File Offset: 0x0030A6D4
		public void CleanUpButtons()
		{
			foreach (KeyValuePair<NKCUILobbyV2.eUIMenu, NKCUILobbyMenuButtonBase> keyValuePair in this.m_dicButton)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.CleanUp();
				}
			}
			foreach (KeyValuePair<NKM_SHORTCUT_TYPE, NKCUILobbyMenuButtonBase> keyValuePair2 in this.m_dicShortCutButton)
			{
				if (keyValuePair2.Value != null)
				{
					keyValuePair2.Value.CleanUp();
				}
			}
		}

		// Token: 0x06008F63 RID: 36707 RVA: 0x0030C594 File Offset: 0x0030A794
		private void OnBtnShortCut(NKCLobbyIconTemplet templet)
		{
			if (templet != null)
			{
				NKCContentManager.MoveToShortCut(templet.m_ShortCutType, templet.m_shortCutParam, false);
			}
		}

		// Token: 0x06008F64 RID: 36708 RVA: 0x0030C5AB File Offset: 0x0030A7AB
		private void OnClickHamburger()
		{
			NKCPopupHamburgerMenu.instance.OpenUI();
		}

		// Token: 0x06008F65 RID: 36709 RVA: 0x0030C5B7 File Offset: 0x0030A7B7
		private void OnMenuOption()
		{
			NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.NORMAL, null);
		}

		// Token: 0x06008F66 RID: 36710 RVA: 0x0030C5C5 File Offset: 0x0030A7C5
		private void OnBtnShop()
		{
			if (NKCShopCategoryTemplet.Find(NKCShopManager.ShopTabCategory.PACKAGE) != null)
			{
				NKCUIShop.Instance.Open(NKCShopManager.ShopTabCategory.PACKAGE, "TAB_MAIN", 0, 0, NKCUIShop.eTabMode.Fold);
				return;
			}
			NKCUIShop.Instance.Open(NKCShopManager.ShopTabCategory.EXCHANGE, "TAB_SUPPLY", 0, 0, NKCUIShop.eTabMode.Fold);
		}

		// Token: 0x06008F67 RID: 36711 RVA: 0x0030C5F6 File Offset: 0x0030A7F6
		private void OnBtnMission()
		{
			NKCUIMissionAchievement.Instance.Open(0);
		}

		// Token: 0x06008F68 RID: 36712 RVA: 0x0030C603 File Offset: 0x0030A803
		private void OnBtnMissionGuide()
		{
			NKCUIMissionGuide.Instance.Open(0);
		}

		// Token: 0x06008F69 RID: 36713 RVA: 0x0030C610 File Offset: 0x0030A810
		private bool HasSpecialItem(NKMUserData userData)
		{
			ShopReddotType shopReddotType;
			return NKCShopManager.CheckTabReddotCount(out shopReddotType, "TAB_NONE", 0) > 0;
		}

		// Token: 0x06008F6A RID: 36714 RVA: 0x0030C62D File Offset: 0x0030A82D
		private bool HasClearableMission(NKMUserData userData)
		{
			return NKMMissionManager.GetHaveClearedMission();
		}

		// Token: 0x06008F6B RID: 36715 RVA: 0x0030C634 File Offset: 0x0030A834
		private bool HasClearableMissionGuide(NKMUserData userData)
		{
			return NKMMissionManager.GetHaveClearedMissionGuide();
		}

		// Token: 0x06008F6C RID: 36716 RVA: 0x0030C63C File Offset: 0x0030A83C
		private void OnBtnAttendance()
		{
			if (NKMAttendanceManager.IsAttendanceBlocked)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(NKM_ERROR_CODE.NEC_FAIL_SYSTEM_CONTENTS_BLOCK), null, "");
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0))
			{
				return;
			}
			List<int> attendanceKeyList = NKCScenManager.GetScenManager().Get_SCEN_HOME().GetAttendanceKeyList();
			if (attendanceKeyList != null && attendanceKeyList.Count > 0)
			{
				NKCUIAttendance.Instance.Open(attendanceKeyList);
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_AttendanceData = NKMAttendanceManager.AddNeedAttendanceKeyByTemplet(NKCScenManager.GetScenManager().GetMyUserData().m_AttendanceData);
			List<int> needAttendanceKey = NKMAttendanceManager.GetNeedAttendanceKey();
			NKCUIAttendance.Instance.Open(needAttendanceKey);
		}

		// Token: 0x06008F6D RID: 36717 RVA: 0x0030C6D2 File Offset: 0x0030A8D2
		private bool ChechAttendanceEnable(NKMUserData userData)
		{
			return NKCScenManager.GetScenManager().Get_SCEN_HOME().GetAttendanceRequired();
		}

		// Token: 0x06008F6E RID: 36718 RVA: 0x0030C6E3 File Offset: 0x0030A8E3
		private void OnBtnNotice()
		{
			NKCPublisherModule.Notice.OpenNotice(null);
		}

		// Token: 0x06008F6F RID: 36719 RVA: 0x0030C6F0 File Offset: 0x0030A8F0
		private bool CheckFreeContract(NKMUserData userData)
		{
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			if (nkccontractDataMgr == null)
			{
				return false;
			}
			bool flag = nkccontractDataMgr.IsActiveNewFreeChance();
			return (nkccontractDataMgr.PossibleFreeContract || flag) && (flag || nkccontractDataMgr.IsPossibleFreeChance());
		}

		// Token: 0x06008F70 RID: 36720 RVA: 0x0030C72F File Offset: 0x0030A92F
		private void OnBtnContract()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CONTRACT, false);
		}

		// Token: 0x06008F71 RID: 36721 RVA: 0x0030C73D File Offset: 0x0030A93D
		private void OnClickInfoBtn()
		{
			NKCPublisherModule.Notice.OpenInfoWindow(null);
		}

		// Token: 0x06008F72 RID: 36722 RVA: 0x0030C74C File Offset: 0x0030A94C
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

		// Token: 0x06008F73 RID: 36723 RVA: 0x0030C7CC File Offset: 0x0030A9CC
		private bool BaseNotifyCondition(NKMUserData userData)
		{
			bool flag;
			if (userData == null)
			{
				flag = (null != null);
			}
			else
			{
				NKMCraftData craftData = userData.m_CraftData;
				flag = (((craftData != null) ? craftData.SlotList : null) != null);
			}
			if (!flag)
			{
				return false;
			}
			foreach (KeyValuePair<byte, NKMCraftSlotData> keyValuePair in userData.m_CraftData.SlotList)
			{
				if (keyValuePair.Value.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED)
				{
					return true;
				}
			}
			return NKCAlarmManager.CheckScoutNotify(userData);
		}

		// Token: 0x06008F74 RID: 36724 RVA: 0x0030C868 File Offset: 0x0030AA68
		private void OnMenuBase()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
		}

		// Token: 0x06008F75 RID: 36725 RVA: 0x0030C877 File Offset: 0x0030AA77
		private void OnMenuDeckSetup()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_TEAM, false);
		}

		// Token: 0x06008F76 RID: 36726 RVA: 0x0030C885 File Offset: 0x0030AA85
		private void UpdateHamburgerNotify()
		{
			NKCUtil.SetGameobjectActive(this.m_objHamburgerNotify, NKCAlarmManager.CheckAllNotify(NKCScenManager.CurrentUserData()));
		}

		// Token: 0x06008F77 RID: 36727 RVA: 0x0030C89C File Offset: 0x0030AA9C
		public void RefreshUserBuff()
		{
			this.m_UIUserInfo.UpdateUserBuffCount(NKCScenManager.CurrentUserData());
		}

		// Token: 0x06008F78 RID: 36728 RVA: 0x0030C8B0 File Offset: 0x0030AAB0
		public bool HasCollectionReward(NKMUserData userData)
		{
			NKCCollectionManager.Init();
			NKMArmyData nkmarmyData = (userData != null) ? userData.m_ArmyData : null;
			if (nkmarmyData == null)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			bool flag;
			NKCUICollectionTeamUp.UpdateTeamUpList(ref num, ref num2, nkmarmyData, false, out flag);
			return (flag && NKCUnitMissionManager.GetOpenTagCollectionTeamUp()) || NKCUnitMissionManager.HasRewardEnableMission();
		}

		// Token: 0x06008F79 RID: 36729 RVA: 0x0030C8FC File Offset: 0x0030AAFC
		public void OnBtnCollection()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, true);
		}

		// Token: 0x06008F7A RID: 36730 RVA: 0x0030C90B File Offset: 0x0030AB0B
		public void OnBtnGuild()
		{
			if (NKCGuildManager.HasGuild())
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_INTRO, true);
		}

		// Token: 0x06008F7B RID: 36731 RVA: 0x0030C930 File Offset: 0x0030AB30
		public bool HasInventoryNotify(NKMUserData userData)
		{
			foreach (NKMItemMiscData nkmitemMiscData in NKCScenManager.CurrentUserData().m_InventoryData.MiscItems.Values)
			{
				if (nkmitemMiscData.TotalCount > 0L)
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(nkmitemMiscData.ItemID);
					if (itemMiscTempletByID != null && itemMiscTempletByID.IsUsable() && itemMiscTempletByID.IsTimeIntervalItem)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06008F7C RID: 36732 RVA: 0x0030C9B4 File Offset: 0x0030ABB4
		public void OnMenuInventory()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_INVENTORY, false);
		}

		// Token: 0x06008F7D RID: 36733 RVA: 0x0030C9C2 File Offset: 0x0030ABC2
		public void OnMenuUnitList()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_UNIT_LIST, false);
		}

		// Token: 0x06008F7E RID: 36734 RVA: 0x0030C9D1 File Offset: 0x0030ABD1
		public bool UseCameraTracking()
		{
			return this.m_UILobby3D.CameraTracking();
		}

		// Token: 0x06008F7F RID: 36735 RVA: 0x0030C9DE File Offset: 0x0030ABDE
		public void RefreshNickname()
		{
			if (this.m_UIUserInfo != null)
			{
				this.m_UIUserInfo.RefreshNickname();
			}
		}

		// Token: 0x06008F80 RID: 36736 RVA: 0x0030C9F9 File Offset: 0x0030ABF9
		public void RefreshRechargeEternium()
		{
			if (this.m_UIUserInfo != null)
			{
				this.m_UIUserInfo.RefreshRechargeEternium();
			}
		}

		// Token: 0x06008F81 RID: 36737 RVA: 0x0030CA14 File Offset: 0x0030AC14
		public void SetEventPanelAutoScroll(bool value)
		{
			if (this.m_UIEventPanel != null && this.m_UIEventPanel.m_EventSlidePanel != null)
			{
				this.m_UIEventPanel.m_EventSlidePanel.m_bAutoMove = value;
			}
		}

		// Token: 0x06008F82 RID: 36738 RVA: 0x0030CA48 File Offset: 0x0030AC48
		private void Update()
		{
		}

		// Token: 0x06008F83 RID: 36739 RVA: 0x0030CA4A File Offset: 0x0030AC4A
		public void TryUIUnhide()
		{
			if (this.m_hideUI)
			{
				this.SetUIVisible(true);
				this.SetPlayIntro(true);
			}
		}

		// Token: 0x06008F84 RID: 36740 RVA: 0x0030CA62 File Offset: 0x0030AC62
		private void OnTglUIHide(bool value)
		{
			this.SetPlayIntro(value);
			this.SetUIVisible(value);
		}

		// Token: 0x06008F85 RID: 36741 RVA: 0x0030CA74 File Offset: 0x0030AC74
		public void SetUIVisible(bool value)
		{
			if (!value)
			{
				this.m_hideUI = true;
				NKCUtil.SetGameobjectActive(this.m_UILobby3D.m_MenuCanvasGroup.gameObject, false);
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				NKCUtil.SetGameobjectActive(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), false);
				NKCUtil.SetGameobjectActive(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), false);
				if (this.m_tglUIHide != null)
				{
					this.m_tglUIHide.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommonLow), true);
					this.m_tglUIHide.Select(false, true, false);
				}
				return;
			}
			if (!this.m_hideUI)
			{
				return;
			}
			this.m_hideUI = false;
			this.m_CanvasGroup.alpha = 1f;
			this.m_UILobby3D.m_MenuCanvasGroup.alpha = 1f;
			NKCUtil.SetGameobjectActive(this.m_UILobby3D.m_MenuCanvasGroup.gameObject, true);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), true);
			NKCUtil.SetGameobjectActive(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), true);
			if (this.m_tglUIHide != null)
			{
				this.m_tglUIHide.transform.SetParent(this.m_trHideBtnRoot, true);
				this.m_tglUIHide.Select(true, true, false);
			}
			this.RefreshRechargeEternium();
		}

		// Token: 0x06008F86 RID: 36742 RVA: 0x0030CBA8 File Offset: 0x0030ADA8
		private void OpenQuitApplicationPopup()
		{
			if (NKCPublisherModule.Auth.CheckExitCallFirst())
			{
				NKCPublisherModule.Auth.Exit(new NKCPublisherModule.OnComplete(this.OnCompleteExitFirst));
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_LOBBY_CHECK_QUIT_GAME, new NKCPopupOKCancel.OnButton(this.OnQuit), null, false);
		}

		// Token: 0x06008F87 RID: 36743 RVA: 0x0030CBF5 File Offset: 0x0030ADF5
		private void OnQuit()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.Exit(new NKCPublisherModule.OnComplete(this.OnCompleteExit));
		}

		// Token: 0x06008F88 RID: 36744 RVA: 0x0030CC1C File Offset: 0x0030AE1C
		private void OnCompleteExit(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			Application.Quit();
		}

		// Token: 0x06008F89 RID: 36745 RVA: 0x0030CC30 File Offset: 0x0030AE30
		private void OnCompleteExitFirst(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_LOBBY_CHECK_QUIT_GAME, delegate()
			{
				Application.Quit();
			}, null, false);
		}

		// Token: 0x06008F8A RID: 36746 RVA: 0x0030CC6F File Offset: 0x0030AE6F
		public override void OnScreenResolutionChanged()
		{
			base.OnScreenResolutionChanged();
			this.m_UILobby3D.AdjustPositionByScreenRatio();
		}

		// Token: 0x06008F8B RID: 36747 RVA: 0x0030CC82 File Offset: 0x0030AE82
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			this.m_UIUserInfo.OnResourceValueChange(itemData);
			this.UpdateButton(NKCUILobbyV2.eUIMenu.Inventory, NKCScenManager.CurrentUserData());
			this.m_UIUserInfo.UpdateUserBuffCount(NKCScenManager.CurrentUserData());
		}

		// Token: 0x06008F8C RID: 36748 RVA: 0x0030CCAD File Offset: 0x0030AEAD
		public override void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipItem)
		{
			switch (eType)
			{
			case NKMUserData.eChangeNotifyType.Add:
			case NKMUserData.eChangeNotifyType.Remove:
				this.UpdateButton(NKCUILobbyV2.eUIMenu.Inventory, NKCScenManager.CurrentUserData());
				break;
			case NKMUserData.eChangeNotifyType.Update:
				break;
			default:
				return;
			}
		}

		// Token: 0x06008F8D RID: 36749 RVA: 0x0030CCCF File Offset: 0x0030AECF
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			switch (eEventType)
			{
			case NKMUserData.eChangeNotifyType.Add:
			case NKMUserData.eChangeNotifyType.Remove:
				this.UpdateButton(NKCUILobbyV2.eUIMenu.UnitList, NKCScenManager.CurrentUserData());
				break;
			case NKMUserData.eChangeNotifyType.Update:
				break;
			default:
				return;
			}
		}

		// Token: 0x06008F8E RID: 36750 RVA: 0x0030CCF1 File Offset: 0x0030AEF1
		public override void OnUserLevelChanged(NKMUserData newUserData)
		{
			this.m_UIUserInfo.UpdateLevelAndExp(newUserData);
			this.m_UIUserInfo.UpdateUserBuffCount(NKCScenManager.CurrentUserData());
			this.UpdateButton(NKCUILobbyV2.eUIMenu.Guild, NKCScenManager.CurrentUserData());
			this.UpdateHamburgerNotify();
		}

		// Token: 0x06008F8F RID: 36751 RVA: 0x0030CD22 File Offset: 0x0030AF22
		public override void OnGuildDataChanged()
		{
			this.m_UIUserInfo.SetGuildData();
			NKCUILobbyMenuButtonBase button = this.GetButton(NKCUILobbyV2.eUIMenu.Guild);
			if (button == null)
			{
				return;
			}
			button.UpdateData(NKCScenManager.CurrentUserData());
		}

		// Token: 0x06008F90 RID: 36752 RVA: 0x0030CD46 File Offset: 0x0030AF46
		public override void OnMissionUpdated()
		{
			this.UpdateHamburgerNotify();
		}

		// Token: 0x06008F91 RID: 36753 RVA: 0x0030CD50 File Offset: 0x0030AF50
		private void SetPlayIntro(bool value)
		{
			if (value)
			{
				foreach (Animator animator in this.m_lstAnimator)
				{
					if (!(animator == null))
					{
						animator.SetTrigger("Intro");
					}
				}
				return;
			}
			foreach (Animator animator2 in this.m_lstAnimator)
			{
				if (!(animator2 == null))
				{
					animator2.ResetTrigger("Intro");
				}
			}
		}

		// Token: 0x06008F92 RID: 36754 RVA: 0x0030CDBC File Offset: 0x0030AFBC
		private void UpdateEventIntervalSlot()
		{
			List<NKCLobbyEventIndexTemplet> currentLobbyEvents = NKCLobbyEventIndexTemplet.GetCurrentLobbyEvents();
			for (int i = 0; i < this.m_lstUIEventIndexSlot.Count; i++)
			{
				NKCUILobbyEventIndexSlot nkcuilobbyEventIndexSlot = this.m_lstUIEventIndexSlot[i];
				if (!(nkcuilobbyEventIndexSlot == null))
				{
					if (currentLobbyEvents.Count > i)
					{
						NKCLobbyEventIndexTemplet data = currentLobbyEvents[i];
						nkcuilobbyEventIndexSlot.SetData(data);
					}
					else
					{
						nkcuilobbyEventIndexSlot.SetData(null);
					}
				}
			}
		}

		// Token: 0x04007C5F RID: 31839
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_lobby";

		// Token: 0x04007C60 RID: 31840
		private const string UI_ASSET_NAME = "NUF_HOME_PREFAB_RENEWAL";

		// Token: 0x04007C61 RID: 31841
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04007C62 RID: 31842
		[Header("���")]
		public NKCUILobbyMenuMail m_UIMail;

		// Token: 0x04007C63 RID: 31843
		public NKCUILobbySimpleMenu m_UIOption;

		// Token: 0x04007C64 RID: 31844
		public NKCUIComStateButton m_btnHamburger;

		// Token: 0x04007C65 RID: 31845
		public GameObject m_objHamburgerNotify;

		// Token: 0x04007C66 RID: 31846
		[Header("3D ������Ʈ")]
		public NKCUILobby3DV2 m_UILobby3D;

		// Token: 0x04007C67 RID: 31847
		public NKCUILobbyMenuOperation m_UIOperation;

		// Token: 0x04007C68 RID: 31848
		public NKCUILobbyMenuCurrentEvent m_UICurrentEvent;

		// Token: 0x04007C69 RID: 31849
		public List<NKCUILobbyEventIndexSlot> m_lstUIEventIndexSlot;

		// Token: 0x04007C6A RID: 31850
		public NKCUILobbyMenuEventPass m_UIEventPass;

		// Token: 0x04007C6B RID: 31851
		public NKCUILobbyMenuPVP m_UIPVP;

		// Token: 0x04007C6C RID: 31852
		public NKCUILobbyMenuWorldmap m_UIWorldmap;

		// Token: 0x04007C6D RID: 31853
		[Header("�»�")]
		public NKCUILobbyUserInfo m_UIUserInfo;

		// Token: 0x04007C6E RID: 31854
		public NKCUILobbySimpleMenu m_UIAttendance;

		// Token: 0x04007C6F RID: 31855
		public NKCUILobbySimpleMenu m_UINotice;

		// Token: 0x04007C70 RID: 31856
		public NKCUIComStateButton m_csbtnInfo;

		// Token: 0x04007C71 RID: 31857
		public NKCUILobbySimpleMenu m_UIChat;

		// Token: 0x04007C72 RID: 31858
		public NKCUISurvey m_UISurvey;

		// Token: 0x04007C73 RID: 31859
		[Header("����")]
		public NKCUILobbyMenuShop m_UIShop;

		// Token: 0x04007C74 RID: 31860
		public NKCUILobbySimpleMenu m_UIMissionGuide;

		// Token: 0x04007C75 RID: 31861
		public GameObject m_objMissionGuideAllComplete;

		// Token: 0x04007C76 RID: 31862
		public Text m_lbMissionGuideCompleteCnt;

		// Token: 0x04007C77 RID: 31863
		public NKCUILobbyEventPanel m_UIEventPanel;

		// Token: 0x04007C78 RID: 31864
		[Header("����")]
		public GameObject m_objBottomRightMenu;

		// Token: 0x04007C79 RID: 31865
		public NKCUILobbySimpleMenu m_UIMission;

		// Token: 0x04007C7A RID: 31866
		public NKCUILobbySimpleMenu m_UICollection;

		// Token: 0x04007C7B RID: 31867
		public NKCUILobbySimpleMenu m_UIGuild;

		// Token: 0x04007C7C RID: 31868
		public NKCUILobbySimpleMenu m_UIBase;

		// Token: 0x04007C7D RID: 31869
		public NKCUILobbySimpleMenu m_UIInventory;

		// Token: 0x04007C7E RID: 31870
		public NKCUILobbySimpleMenu m_UIUnitList;

		// Token: 0x04007C7F RID: 31871
		public NKCUILobbySimpleMenu m_UIDeckSetup;

		// Token: 0x04007C80 RID: 31872
		public NKCUILobbySimpleMenu m_UIContract;

		// Token: 0x04007C81 RID: 31873
		[Header("�ִ�")]
		public Animator[] m_lstAnimator;

		// Token: 0x04007C82 RID: 31874
		private NKMEventCollectionIndexTemplet m_EventCollecionIndexTemplet;

		// Token: 0x04007C83 RID: 31875
		private Dictionary<NKCUILobbyV2.eUIMenu, NKCUILobbyMenuButtonBase> m_dicButton = new Dictionary<NKCUILobbyV2.eUIMenu, NKCUILobbyMenuButtonBase>();

		// Token: 0x04007C84 RID: 31876
		private Dictionary<NKM_SHORTCUT_TYPE, NKCUILobbyMenuButtonBase> m_dicShortCutButton = new Dictionary<NKM_SHORTCUT_TYPE, NKCUILobbyMenuButtonBase>();

		// Token: 0x04007C85 RID: 31877
		[Header("���� ��ư")]
		public List<NKCUILobbyEventShortCut> m_lstUIShortCutButton;

		// Token: 0x04007C86 RID: 31878
		[Header("UI����")]
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04007C87 RID: 31879
		public NKCUIComToggle m_tglUIHide;

		// Token: 0x04007C88 RID: 31880
		private Transform m_trHideBtnRoot;

		// Token: 0x04007C89 RID: 31881
		private bool m_hideUI;

		// Token: 0x020019DC RID: 6620
		public enum eUIMenu
		{
			// Token: 0x0400AD08 RID: 44296
			Mission,
			// Token: 0x0400AD09 RID: 44297
			Shop,
			// Token: 0x0400AD0A RID: 44298
			Event,
			// Token: 0x0400AD0B RID: 44299
			Operation,
			// Token: 0x0400AD0C RID: 44300
			CurrentEvent,
			// Token: 0x0400AD0D RID: 44301
			DeckSetup,
			// Token: 0x0400AD0E RID: 44302
			PVP,
			// Token: 0x0400AD0F RID: 44303
			Contract,
			// Token: 0x0400AD10 RID: 44304
			Base,
			// Token: 0x0400AD11 RID: 44305
			Worldmap,
			// Token: 0x0400AD12 RID: 44306
			Option,
			// Token: 0x0400AD13 RID: 44307
			Mail,
			// Token: 0x0400AD14 RID: 44308
			UnitList,
			// Token: 0x0400AD15 RID: 44309
			Collection,
			// Token: 0x0400AD16 RID: 44310
			Inventory,
			// Token: 0x0400AD17 RID: 44311
			Office,
			// Token: 0x0400AD18 RID: 44312
			Friends,
			// Token: 0x0400AD19 RID: 44313
			LeaderBoard,
			// Token: 0x0400AD1A RID: 44314
			Guild,
			// Token: 0x0400AD1B RID: 44315
			Attendance,
			// Token: 0x0400AD1C RID: 44316
			CounterPass,
			// Token: 0x0400AD1D RID: 44317
			GuideMission,
			// Token: 0x0400AD1E RID: 44318
			Chat,
			// Token: 0x0400AD1F RID: 44319
			Survey
		}
	}
}
