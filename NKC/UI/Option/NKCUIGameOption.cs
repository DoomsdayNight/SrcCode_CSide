using System;
using System.Collections.Generic;
using NKC.Publisher;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B88 RID: 2952
	public class NKCUIGameOption : NKCUIBase
	{
		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06008821 RID: 34849 RVA: 0x002E0F74 File Offset: 0x002DF174
		public static NKCUIGameOption Instance
		{
			get
			{
				if (NKCUIGameOption.m_Instance == null)
				{
					NKCUIGameOption.m_Instance = NKCUIManager.OpenNewInstance<NKCUIGameOption>("ab_ui_nkm_ui_game_option", "NKM_UI_GAME_OPTION_REAL", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGameOption.CleanupInstance)).GetInstance<NKCUIGameOption>();
					NKCUIGameOption.m_Instance.Init();
				}
				return NKCUIGameOption.m_Instance;
			}
		}

		// Token: 0x06008822 RID: 34850 RVA: 0x002E0FC3 File Offset: 0x002DF1C3
		private static void CleanupInstance()
		{
			NKCUIGameOption.m_Instance = null;
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06008823 RID: 34851 RVA: 0x002E0FCB File Offset: 0x002DF1CB
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06008824 RID: 34852 RVA: 0x002E0FCE File Offset: 0x002DF1CE
		public static bool HasInstance
		{
			get
			{
				return NKCUIGameOption.m_Instance != null;
			}
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06008825 RID: 34853 RVA: 0x002E0FDB File Offset: 0x002DF1DB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGameOption.m_Instance != null && NKCUIGameOption.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008826 RID: 34854 RVA: 0x002E0FF6 File Offset: 0x002DF1F6
		public static void CheckInstanceAndClose()
		{
			if (NKCUIGameOption.m_Instance != null && NKCUIGameOption.m_Instance.IsOpen)
			{
				NKCUIGameOption.m_Instance.Close();
			}
		}

		// Token: 0x06008827 RID: 34855 RVA: 0x002E101B File Offset: 0x002DF21B
		private void OnDestroy()
		{
			NKCUIGameOption.m_Instance = null;
		}

		// Token: 0x06008828 RID: 34856 RVA: 0x002E1023 File Offset: 0x002DF223
		public static bool GetShowHiddenOption()
		{
			return NKCUIGameOption.s_bShowHiddenOption;
		}

		// Token: 0x06008829 RID: 34857 RVA: 0x002E102A File Offset: 0x002DF22A
		public static void InvalidShowHiddenOption()
		{
			NKCUIGameOption.s_bShowHiddenOption = false;
			if (NKCUIGameOption.s_lstCurrHiddenCombo != null)
			{
				NKCUIGameOption.s_lstCurrHiddenCombo.Clear();
			}
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x0600882A RID: 34858 RVA: 0x002E1043 File Offset: 0x002DF243
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x0600882B RID: 34859 RVA: 0x002E1046 File Offset: 0x002DF246
		public override string MenuName
		{
			get
			{
				return "GameOption";
			}
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x0600882C RID: 34860 RVA: 0x002E104D File Offset: 0x002DF24D
		public string RESET_BUTTON_CLICK_TITLE_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_WARNING;
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x0600882D RID: 34861 RVA: 0x002E1054 File Offset: 0x002DF254
		public string RESET_BUTTON_CLICK_CONTENT_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_RESET_WARNING;
			}
		}

		// Token: 0x0600882E RID: 34862 RVA: 0x002E105B File Offset: 0x002DF25B
		public override void OnBackButton()
		{
			this.OnClickConfirmButton();
		}

		// Token: 0x0600882F RID: 34863 RVA: 0x002E1064 File Offset: 0x002DF264
		private void Init()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_MenuButtons[0] = this.m_NKM_UI_GAME_OPTION_MENU_MISSION;
			this.m_MenuButtons[1] = this.m_NKM_UI_GAME_OPTION_MENU_GAME;
			this.m_NKM_UI_GAME_OPTION_MENU_GAME.m_Toggle.PointerDown.RemoveAllListeners();
			this.m_NKM_UI_GAME_OPTION_MENU_GAME.m_Toggle.PointerDown.AddListener(delegate(PointerEventData v)
			{
				this.ProcessHiddenOption(NKCUIGameOption.GameOptionGroup.Game);
			});
			this.m_MenuButtons[2] = this.m_NKM_UI_GAME_OPTION_MENU_GRAPHIC;
			this.m_NKM_UI_GAME_OPTION_MENU_GRAPHIC.m_Toggle.PointerDown.RemoveAllListeners();
			this.m_NKM_UI_GAME_OPTION_MENU_GRAPHIC.m_Toggle.PointerDown.AddListener(delegate(PointerEventData v)
			{
				this.ProcessHiddenOption(NKCUIGameOption.GameOptionGroup.Graphic);
			});
			this.m_MenuButtons[3] = this.m_NKM_UI_GAME_OPTION_MENU_SOUND;
			this.m_NKM_UI_GAME_OPTION_MENU_SOUND.m_Toggle.PointerDown.RemoveAllListeners();
			this.m_NKM_UI_GAME_OPTION_MENU_SOUND.m_Toggle.PointerDown.AddListener(delegate(PointerEventData v)
			{
				this.ProcessHiddenOption(NKCUIGameOption.GameOptionGroup.Sound);
			});
			this.m_MenuButtons[4] = this.m_NKM_UI_GAME_OPTION_MENU_ALARM;
			this.m_MenuButtons[5] = this.m_NKM_UI_GAME_OPTION_MENU_ACCOUNT;
			this.m_MenuButtons[6] = this.m_NKM_UI_GAME_OPTION_MENU_REPLAY;
			this.m_MenuButtons[7] = this.m_NKM_UI_GAME_OPTION_MENU_OBSERVE;
			this.m_Contents[0] = this.m_NKM_UI_GAME_OPTION_MISSION;
			this.m_Contents[1] = this.m_NKM_UI_GAME_OPTION_GAME;
			this.m_Contents[2] = this.m_NKM_UI_GAME_OPTION_GRAPHIC;
			this.m_Contents[3] = this.m_NKM_UI_GAME_OPTION_SOUND;
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.NOT_USE_LOCAL_PUSH))
			{
				this.m_Contents[4] = this.m_NKM_UI_GAME_OPTION_PUSH;
			}
			else
			{
				this.m_Contents[4] = this.m_NKM_UI_GAME_OPTION_ALARM;
			}
			this.m_Contents[5] = this.m_NKM_UI_GAME_OPTION_ACCOUNT;
			this.m_Contents[6] = this.m_NKM_UI_GAME_OPTION_REPLAY;
			this.m_Contents[7] = this.m_NKM_UI_GAME_OPTION_OBSERVE;
			this.m_NKM_UI_GAME_OPTION_BTN_RESET.PointerClick.AddListener(new UnityAction(this.OnClickResetButton));
			this.m_NKM_UI_GAME_OPTION_BTN_CONFIRM.PointerClick.AddListener(new UnityAction(this.OnClickConfirmButton));
			for (int i = 0; i < 8; i++)
			{
				NKCUIGameOption.GameOptionGroup group = (NKCUIGameOption.GameOptionGroup)i;
				this.m_MenuButtons[i].Init(this.m_SelectedMenuIconColor, this.m_SelectedMenuTextColor, this.m_SelectedMenuSubTextColor, delegate
				{
					this.ChangeContent(group);
				});
				this.m_Contents[i].Init();
			}
			string text = "CounterSide";
			text = text + " " + NKCUtilString.GetAppVersionText();
			if (!string.IsNullOrEmpty(NKCUtil.PatchVersion))
			{
				string[] array = NKCUtil.PatchVersion.Split(new char[]
				{
					'_'
				});
				if (array.Length != 0)
				{
					text = text + " A." + array[array.Length - 1];
				}
			}
			if (!string.IsNullOrEmpty(NKCUtil.PatchVersionEA))
			{
				string[] array2 = NKCUtil.PatchVersionEA.Split(new char[]
				{
					'_'
				});
				if (array2.Length != 0)
				{
					text = text + " E." + array2[array2.Length - 1];
				}
			}
			text = text + " " + NKCUtilString.GetProtocolVersionText();
			this.m_NKM_UI_GAME_OPTION_VERSION_TEXT.text = text;
			NKCUtil.SetGameobjectActive(this.m_AGREEMENT, false);
			if (NKMContentsVersionManager.HasTag("CHECK_AGREEMENT_NOTICE"))
			{
				NKCUtil.SetGameobjectActive(this.m_AGREEMENT, true);
				this.m_NKM_UI_GAME_OPTION_BTN_CHECK_AGREEMENT.PointerClick.AddListener(new UnityAction(this.OnClickCheckAgreement));
				this.m_NKM_UI_GAME_OPTION_BTN_RESET_AGREEMENT.PointerClick.AddListener(new UnityAction(this.OnClickResetAgreement));
			}
		}

		// Token: 0x06008830 RID: 34864 RVA: 0x002E13BF File Offset: 0x002DF5BF
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.dOnCloseCallBack != null)
			{
				this.dOnCloseCallBack();
				this.dOnCloseCallBack = null;
			}
		}

		// Token: 0x06008831 RID: 34865 RVA: 0x002E13E8 File Offset: 0x002DF5E8
		public void Open(NKC_GAME_OPTION_MENU_TYPE menuType, NKCUIGameOption.OnCloseCallBack closeCallBack = null)
		{
			if (NKCUIGameOption.s_lstCurrHiddenCombo != null)
			{
				NKCUIGameOption.s_lstCurrHiddenCombo.Clear();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_MenuType = menuType;
			this.dOnCloseCallBack = closeCallBack;
			this.ChangeMenu(menuType);
			NKCUIGameOption.GameOptionGroup defaultContentGroupByMenuType = this.GetDefaultContentGroupByMenuType(menuType);
			if (defaultContentGroupByMenuType != NKCUIGameOption.GameOptionGroup.None)
			{
				this.m_MenuButtons[(int)defaultContentGroupByMenuType].m_Toggle.Select(true, false, false);
			}
			else if (this.m_SelectedGroup != NKCUIGameOption.GameOptionGroup.None)
			{
				this.m_MenuButtons[(int)this.m_SelectedGroup].m_Toggle.Select(false, true, false);
			}
			this.ChangeContent(defaultContentGroupByMenuType);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06008832 RID: 34866 RVA: 0x002E1488 File Offset: 0x002DF688
		private void RefreshContent(bool bForceAll = false)
		{
			if (this.m_SelectedGroup == NKCUIGameOption.GameOptionGroup.None)
			{
				return;
			}
			if (bForceAll)
			{
				NKCUIGameOptionContentBase[] contents = this.m_Contents;
				for (int i = 0; i < contents.Length; i++)
				{
					contents[i].SetContent();
				}
				return;
			}
			this.m_Contents[(int)this.m_SelectedGroup].SetContent();
		}

		// Token: 0x06008833 RID: 34867 RVA: 0x002E14D2 File Offset: 0x002DF6D2
		public NKC_GAME_OPTION_MENU_TYPE GetMenuType()
		{
			return this.m_MenuType;
		}

		// Token: 0x06008834 RID: 34868 RVA: 0x002E14DA File Offset: 0x002DF6DA
		public void RemoveCloseCallBack()
		{
			this.dOnCloseCallBack = null;
		}

		// Token: 0x06008835 RID: 34869 RVA: 0x002E14E3 File Offset: 0x002DF6E3
		public void Update()
		{
			if (base.IsOpen && this.m_NKCUIOpenAnimator != null)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06008836 RID: 34870 RVA: 0x002E1500 File Offset: 0x002DF700
		private void ChangeMenu(NKC_GAME_OPTION_MENU_TYPE selectedMenuType)
		{
			for (int i = 0; i < 8; i++)
			{
				bool bValue = this.IsMenuGroupIncluded(selectedMenuType, (NKCUIGameOption.GameOptionGroup)i);
				NKCUtil.SetGameobjectActive(this.m_MenuButtons[i], bValue);
			}
		}

		// Token: 0x06008837 RID: 34871 RVA: 0x002E1530 File Offset: 0x002DF730
		private bool IsMenuGroupIncluded(NKC_GAME_OPTION_MENU_TYPE currentMenuType, NKCUIGameOption.GameOptionGroup targetGameOptionGroup)
		{
			return (targetGameOptionGroup != NKCUIGameOption.GameOptionGroup.Alarm || !NKCPublisherModule.IsPCBuild()) && (targetGameOptionGroup != NKCUIGameOption.GameOptionGroup.Replay || currentMenuType == NKC_GAME_OPTION_MENU_TYPE.REPLAY) && (targetGameOptionGroup != NKCUIGameOption.GameOptionGroup.Observe || currentMenuType == NKC_GAME_OPTION_MENU_TYPE.OBSERVE) && ((currentMenuType != NKC_GAME_OPTION_MENU_TYPE.NORMAL && currentMenuType - NKC_GAME_OPTION_MENU_TYPE.REPLAY > 1) || targetGameOptionGroup != NKCUIGameOption.GameOptionGroup.Mission);
		}

		// Token: 0x06008838 RID: 34872 RVA: 0x002E1564 File Offset: 0x002DF764
		private NKCUIGameOption.GameOptionGroup GetDefaultContentGroupByMenuType(NKC_GAME_OPTION_MENU_TYPE menuType)
		{
			NKCUIGameOption.GameOptionGroup result = NKCUIGameOption.GameOptionGroup.None;
			switch (menuType)
			{
			case NKC_GAME_OPTION_MENU_TYPE.NORMAL:
				result = NKCUIGameOption.GameOptionGroup.Game;
				break;
			case NKC_GAME_OPTION_MENU_TYPE.WARFARE:
			case NKC_GAME_OPTION_MENU_TYPE.DUNGEON:
			case NKC_GAME_OPTION_MENU_TYPE.DIVE:
				result = NKCUIGameOption.GameOptionGroup.Mission;
				break;
			case NKC_GAME_OPTION_MENU_TYPE.REPLAY:
				result = NKCUIGameOption.GameOptionGroup.Replay;
				break;
			case NKC_GAME_OPTION_MENU_TYPE.OBSERVE:
				result = NKCUIGameOption.GameOptionGroup.Observe;
				break;
			}
			return result;
		}

		// Token: 0x06008839 RID: 34873 RVA: 0x002E15A4 File Offset: 0x002DF7A4
		private void ProcessHiddenOption(NKCUIGameOption.GameOptionGroup group)
		{
			if (NKCUIGameOption.s_lstCurrHiddenCombo == null || NKCUIGameOption.s_arrComboToOpenHidden == null)
			{
				return;
			}
			if (NKCUIGameOption.s_bShowHiddenOption)
			{
				return;
			}
			if (NKCUIGameOption.s_lstCurrHiddenCombo.Count >= NKCUIGameOption.s_arrComboToOpenHidden.Length)
			{
				NKCUIGameOption.s_lstCurrHiddenCombo.Clear();
			}
			NKCUIGameOption.s_lstCurrHiddenCombo.Add(group);
			bool flag = false;
			int num = 0;
			while (num < NKCUIGameOption.s_lstCurrHiddenCombo.Count && num < NKCUIGameOption.s_arrComboToOpenHidden.Length)
			{
				if (NKCUIGameOption.s_lstCurrHiddenCombo[num] != NKCUIGameOption.s_arrComboToOpenHidden[num])
				{
					NKCUIGameOption.s_lstCurrHiddenCombo.Clear();
					flag = true;
				}
				num++;
			}
			if (!flag && NKCUIGameOption.s_lstCurrHiddenCombo.Count == NKCUIGameOption.s_arrComboToOpenHidden.Length)
			{
				NKCUIGameOption.s_bShowHiddenOption = true;
			}
		}

		// Token: 0x0600883A RID: 34874 RVA: 0x002E1650 File Offset: 0x002DF850
		private void ChangeContent(NKCUIGameOption.GameOptionGroup group)
		{
			for (int i = 0; i < 8; i++)
			{
				if (i == (int)group)
				{
					NKCUtil.SetGameobjectActive(this.m_Contents[i], true);
					this.m_Contents[i].SetContent();
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_Contents[i], false);
				}
			}
			this.m_SelectedGroup = group;
		}

		// Token: 0x0600883B RID: 34875 RVA: 0x002E169F File Offset: 0x002DF89F
		private void OnClickResetButton()
		{
			NKCPopupOKCancel.OpenOKCancelBox(this.RESET_BUTTON_CLICK_TITLE_STRING, this.RESET_BUTTON_CLICK_CONTENT_STRING, delegate()
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData == null)
				{
					return;
				}
				gameOptionData.Rollback();
				this.RefreshContent(true);
			}, null, false);
		}

		// Token: 0x0600883C RID: 34876 RVA: 0x002E16C0 File Offset: 0x002DF8C0
		private void OnClickConfirmButton()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (gameOptionData.ChangedPrivatePvpInvite)
			{
				NKCPrivatePVPMgr.Send_NKMPacket_UPDATE_PVP_INVITATION_OPTION_REQ(gameOptionData.ePrivatePvpInviteOption);
			}
			if (gameOptionData.ChangedServerOption)
			{
				bool bLocal = false;
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && NKCScenManager.GetScenManager().GetGameClient().GetGameData() != null)
				{
					bLocal = NKCScenManager.GetScenManager().GetGameClient().GetGameData().m_bLocal;
				}
				NKCPacketSender.Send_NKMPacket_GAME_OPTION_CHANGE_REQ(gameOptionData.ActionCamera, gameOptionData.TrackCamera, gameOptionData.ViewSkillCutIn, gameOptionData.PvPAutoRespawn, gameOptionData.AutSyncFriendDeck, bLocal);
				return;
			}
			gameOptionData.Save();
			base.Close();
		}

		// Token: 0x0600883D RID: 34877 RVA: 0x002E175D File Offset: 0x002DF95D
		public void UpdateOptionContent(NKCUIGameOption.GameOptionGroup group)
		{
			if (group != this.m_SelectedGroup)
			{
				return;
			}
			this.RefreshContent(false);
		}

		// Token: 0x0600883E RID: 34878 RVA: 0x002E1770 File Offset: 0x002DF970
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			return this.m_NKM_UI_GAME_OPTION_MISSION.Processhotkey(hotkey) || this.m_NKM_UI_GAME_OPTION_GAME.Processhotkey(hotkey) || this.m_NKM_UI_GAME_OPTION_GRAPHIC.Processhotkey(hotkey) || this.m_NKM_UI_GAME_OPTION_SOUND.Processhotkey(hotkey) || this.m_NKM_UI_GAME_OPTION_ALARM.Processhotkey(hotkey) || this.m_NKM_UI_GAME_OPTION_ACCOUNT.Processhotkey(hotkey) || this.m_NKM_UI_GAME_OPTION_REPLAY.Processhotkey(hotkey);
		}

		// Token: 0x0600883F RID: 34879 RVA: 0x002E17EE File Offset: 0x002DF9EE
		public void OnClickCheckAgreement()
		{
			NKCUIAgreementNotice.OnClickPrivacy();
		}

		// Token: 0x06008840 RID: 34880 RVA: 0x002E17F5 File Offset: 0x002DF9F5
		public void OnClickResetAgreement()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUIAgreementNotice.ResetPrivacyPolicyMessage, delegate()
			{
				NKCUIAgreementNotice.OnResetAgreement(true);
			}, null, false);
		}

		// Token: 0x04007492 RID: 29842
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_game_option";

		// Token: 0x04007493 RID: 29843
		private const string UI_ASSET_NAME = "NKM_UI_GAME_OPTION_REAL";

		// Token: 0x04007494 RID: 29844
		private static NKCUIGameOption m_Instance;

		// Token: 0x04007495 RID: 29845
		private static bool s_bShowHiddenOption = false;

		// Token: 0x04007496 RID: 29846
		private static NKCUIGameOption.GameOptionGroup[] s_arrComboToOpenHidden = new NKCUIGameOption.GameOptionGroup[]
		{
			NKCUIGameOption.GameOptionGroup.Game,
			NKCUIGameOption.GameOptionGroup.Game,
			NKCUIGameOption.GameOptionGroup.Game,
			NKCUIGameOption.GameOptionGroup.Sound,
			NKCUIGameOption.GameOptionGroup.Sound,
			NKCUIGameOption.GameOptionGroup.Sound,
			NKCUIGameOption.GameOptionGroup.Graphic,
			NKCUIGameOption.GameOptionGroup.Graphic,
			NKCUIGameOption.GameOptionGroup.Graphic,
			NKCUIGameOption.GameOptionGroup.Graphic,
			NKCUIGameOption.GameOptionGroup.Graphic,
			NKCUIGameOption.GameOptionGroup.Graphic,
			NKCUIGameOption.GameOptionGroup.Graphic
		};

		// Token: 0x04007497 RID: 29847
		private static List<NKCUIGameOption.GameOptionGroup> s_lstCurrHiddenCombo = new List<NKCUIGameOption.GameOptionGroup>();

		// Token: 0x04007498 RID: 29848
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04007499 RID: 29849
		private NKC_GAME_OPTION_MENU_TYPE m_MenuType;

		// Token: 0x0400749A RID: 29850
		private NKCUIGameOption.GameOptionGroup m_SelectedGroup = NKCUIGameOption.GameOptionGroup.None;

		// Token: 0x0400749B RID: 29851
		private NKCUIGameOption.OnCloseCallBack dOnCloseCallBack;

		// Token: 0x0400749C RID: 29852
		private NKCUIGameOptionMenuButton[] m_MenuButtons = new NKCUIGameOptionMenuButton[8];

		// Token: 0x0400749D RID: 29853
		private NKCUIGameOptionContentBase[] m_Contents = new NKCUIGameOptionContentBase[8];

		// Token: 0x0400749E RID: 29854
		[Header("상부 텍스트")]
		public Text m_NKM_UI_GAME_OPTION_VERSION_TEXT;

		// Token: 0x0400749F RID: 29855
		[Header("메뉴 버튼")]
		public NKCUIGameOptionMenuButton m_NKM_UI_GAME_OPTION_MENU_MISSION;

		// Token: 0x040074A0 RID: 29856
		public NKCUIGameOptionMenuButton m_NKM_UI_GAME_OPTION_MENU_GAME;

		// Token: 0x040074A1 RID: 29857
		public NKCUIGameOptionMenuButton m_NKM_UI_GAME_OPTION_MENU_GRAPHIC;

		// Token: 0x040074A2 RID: 29858
		public NKCUIGameOptionMenuButton m_NKM_UI_GAME_OPTION_MENU_SOUND;

		// Token: 0x040074A3 RID: 29859
		public NKCUIGameOptionMenuButton m_NKM_UI_GAME_OPTION_MENU_ALARM;

		// Token: 0x040074A4 RID: 29860
		public NKCUIGameOptionMenuButton m_NKM_UI_GAME_OPTION_MENU_ACCOUNT;

		// Token: 0x040074A5 RID: 29861
		public NKCUIGameOptionMenuButton m_NKM_UI_GAME_OPTION_MENU_REPLAY;

		// Token: 0x040074A6 RID: 29862
		public NKCUIGameOptionMenuButton m_NKM_UI_GAME_OPTION_MENU_OBSERVE;

		// Token: 0x040074A7 RID: 29863
		[Header("컨텐츠")]
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_MISSION;

		// Token: 0x040074A8 RID: 29864
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_GAME;

		// Token: 0x040074A9 RID: 29865
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_GRAPHIC;

		// Token: 0x040074AA RID: 29866
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_SOUND;

		// Token: 0x040074AB RID: 29867
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_ALARM;

		// Token: 0x040074AC RID: 29868
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_PUSH;

		// Token: 0x040074AD RID: 29869
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_ACCOUNT;

		// Token: 0x040074AE RID: 29870
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_REPLAY;

		// Token: 0x040074AF RID: 29871
		public NKCUIGameOptionContentBase m_NKM_UI_GAME_OPTION_OBSERVE;

		// Token: 0x040074B0 RID: 29872
		public Color m_SelectedMenuIconColor;

		// Token: 0x040074B1 RID: 29873
		public Color m_SelectedMenuTextColor;

		// Token: 0x040074B2 RID: 29874
		public Color m_SelectedMenuSubTextColor;

		// Token: 0x040074B3 RID: 29875
		[Header("하부 버튼")]
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_BTN_RESET;

		// Token: 0x040074B4 RID: 29876
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_BTN_CONFIRM;

		// Token: 0x040074B5 RID: 29877
		[Header("개인 정책 버튼")]
		public GameObject m_AGREEMENT;

		// Token: 0x040074B6 RID: 29878
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_BTN_CHECK_AGREEMENT;

		// Token: 0x040074B7 RID: 29879
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_BTN_RESET_AGREEMENT;

		// Token: 0x0200192E RID: 6446
		public enum GameOptionGroup
		{
			// Token: 0x0400AAD7 RID: 43735
			None = -1,
			// Token: 0x0400AAD8 RID: 43736
			Mission,
			// Token: 0x0400AAD9 RID: 43737
			Game,
			// Token: 0x0400AADA RID: 43738
			Graphic,
			// Token: 0x0400AADB RID: 43739
			Sound,
			// Token: 0x0400AADC RID: 43740
			Alarm,
			// Token: 0x0400AADD RID: 43741
			Account,
			// Token: 0x0400AADE RID: 43742
			Replay,
			// Token: 0x0400AADF RID: 43743
			Observe,
			// Token: 0x0400AAE0 RID: 43744
			Max
		}

		// Token: 0x0200192F RID: 6447
		// (Invoke) Token: 0x0600B7E1 RID: 47073
		public delegate void OnCloseCallBack();
	}
}
