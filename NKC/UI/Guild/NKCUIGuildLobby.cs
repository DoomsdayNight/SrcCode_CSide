using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using Cs.Core.Util;
using NKM;
using NKM.Guild;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B51 RID: 2897
	public class NKCUIGuildLobby : NKCUIBase
	{
		// Token: 0x060083FF RID: 33791 RVA: 0x002C763F File Offset: 0x002C583F
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUIGuildLobby>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_LOBBY");
		}

		// Token: 0x06008400 RID: 33792 RVA: 0x002C7650 File Offset: 0x002C5850
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUIGuildLobby retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUIGuildLobby>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x06008401 RID: 33793 RVA: 0x002C7660 File Offset: 0x002C5860
		public void CloseInstance()
		{
			int num = NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_LOBBY");
			Debug.Log(string.Format("NKCUIConsortiumLobby close resource retval is {0}", num));
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06008402 RID: 33794 RVA: 0x002C769D File Offset: 0x002C589D
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					21,
					1,
					2,
					101
				};
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06008403 RID: 33795 RVA: 0x002C76C2 File Offset: 0x002C58C2
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_CONSORTIUM_INTRO;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06008404 RID: 33796 RVA: 0x002C76C9 File Offset: 0x002C58C9
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06008405 RID: 33797 RVA: 0x002C76CC File Offset: 0x002C58CC
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_GUILD_INFORMATION";
			}
		}

		// Token: 0x06008406 RID: 33798 RVA: 0x002C76D4 File Offset: 0x002C58D4
		public override void UnHide()
		{
			base.UnHide();
			for (int i = 0; i < this.m_lstTab.Count; i++)
			{
				this.m_lstTab[i].CheckRedDot();
				this.m_lstTab[i].UpdateState();
			}
			if (this.m_GuildLobbyInfo.gameObject.activeSelf)
			{
				this.m_GuildLobbyInfo.ResetPosition();
				return;
			}
			if (this.m_GuildLobbyMission.gameObject.activeSelf)
			{
				this.m_GuildLobbyMission.SetData();
			}
		}

		// Token: 0x06008407 RID: 33799 RVA: 0x002C775A File Offset: 0x002C595A
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_CurrentUIType = NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.None;
		}

		// Token: 0x06008408 RID: 33800 RVA: 0x002C776F File Offset: 0x002C596F
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
		}

		// Token: 0x06008409 RID: 33801 RVA: 0x002C7780 File Offset: 0x002C5980
		public void InitUI()
		{
			for (int i = 0; i < this.m_lstTab.Count; i++)
			{
				this.m_lstTab[i].InitUI(new NKCUIGuildLobbyTab.OnToggle(this.OnClickTab));
				this.m_lstTab[i].CheckRedDot();
				this.m_lstTab[i].UpdateState();
			}
			this.m_GuildLobbyInfo.InitUI();
			this.m_GuildLobbyMember.InitUI();
			this.m_GuildLobbyManage.InitUI();
			this.m_GuildLobbyWelfare.InitUI();
			this.m_GuildLobbyMission.InitUI();
			this.m_GuildLobbyRank.InitUI();
			this.m_btnLvInfo.PointerClick.RemoveAllListeners();
			this.m_btnLvInfo.PointerClick.AddListener(new UnityAction(this.OnClickLvInfo));
			this.m_btnHelp.PointerClick.RemoveAllListeners();
			this.m_btnHelp.PointerClick.AddListener(new UnityAction(this.OnClickHelp));
			NKCUIComStateButton btnDonation = this.m_btnDonation;
			if (btnDonation != null)
			{
				btnDonation.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnDonation2 = this.m_btnDonation;
			if (btnDonation2 != null)
			{
				btnDonation2.PointerClick.AddListener(new UnityAction(this.OnClickDonation));
			}
			this.m_btnBreakup.PointerClick.RemoveAllListeners();
			this.m_btnBreakup.PointerClick.AddListener(new UnityAction(this.OnClickBreakup));
			this.m_btnBreakupClose.PointerClick.RemoveAllListeners();
			this.m_btnBreakupClose.PointerClick.AddListener(new UnityAction(this.OnClickBreakupClose));
			this.m_btnChat.PointerClick.RemoveAllListeners();
			this.m_btnChat.PointerClick.AddListener(new UnityAction(this.OnClickChat));
			this.m_CurrentUIType = NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.None;
		}

		// Token: 0x0600840A RID: 33802 RVA: 0x002C7940 File Offset: 0x002C5B40
		public void Open(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE reservedTab = NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info)
		{
			this.m_GuildData = NKCGuildManager.MyGuildData;
			if (this.m_GuildData == null)
			{
				Debug.LogError("길드정보 없음");
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			this.m_fDeltaTime = 0f;
			this.SetGuildBasicData();
			this.SetNewChatCount(NKCChatManager.GetUncheckedMessageCount(NKCGuildManager.MyData.guildUid));
			this.m_lstTab[0].m_tgl.Select(true, true, true);
			this.m_CurrentUIType = NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.None;
			if (reservedTab == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member)
			{
				base.gameObject.SetActive(true);
			}
			this.OnClickTab(reservedTab, false);
			base.UIOpened(true);
			if (!this.m_GuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID).HasAttendanceData(ServiceTime.Recent))
			{
				NKCPacketSender.Send_NKMPacket_GUILD_ATTENDANCE_REQ(this.m_GuildData.guildUid);
			}
		}

		// Token: 0x0600840B RID: 33803 RVA: 0x002C7A34 File Offset: 0x002C5C34
		private void SetGuildBasicData()
		{
			this.m_BadgeUI.SetData(this.m_GuildData.badgeId);
			NKCUtil.SetLabelText(this.m_lbName, this.m_GuildData.name);
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, this.m_GuildData.guildLevel));
			float guildLevelExp = (float)this.m_GuildData.guildLevelExp;
			long guildExpRequired = GuildExpTemplet.Find(this.m_GuildData.guildLevel).GuildExpRequired;
			float num = guildLevelExp / (float)guildExpRequired * 100f;
			NKCUtil.SetLabelText(this.m_lbExp, string.Format("({0}%)", num.ToString("F1")));
			this.m_imgExpGauge.fillAmount = num / 100f;
			NKMGuildMemberData nkmguildMemberData = this.m_GuildData.members.Find((NKMGuildMemberData x) => x.grade == GuildMemberGrade.Master);
			NKCUtil.SetLabelText(this.m_lbLeaderName, (nkmguildMemberData != null) ? nkmguildMemberData.commonProfile.nickname : null);
			NKCUtil.SetLabelText(this.m_lbMemberCount, string.Format("{0}/{1}", this.m_GuildData.members.Count, NKMTempletContainer<GuildExpTemplet>.Find(this.m_GuildData.guildLevel).MaxMemberCount));
			NKMGuildAttendanceData todayAttendance = this.m_GuildData.GetTodayAttendance(ServiceTime.Recent);
			NKCUtil.SetLabelText(this.m_lbAttendanceCount, string.Format("{0}/{1}", (todayAttendance != null) ? todayAttendance.count : 0, this.m_GuildData.members.Count));
			NKCUtil.SetGameobjectActive(this.m_objBreakupDelay, this.m_GuildData.guildState == GuildState.Closing);
			if (this.m_objBreakupDelay.activeSelf)
			{
				if (this.m_GuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID).grade == GuildMemberGrade.Master)
				{
					NKCUtil.SetLabelText(this.m_lbBreakupBtnText, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_TITLE_DESC);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbBreakupBtnText, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_INFORMATION_BTN_TEXT);
				}
				this.m_tExpireTime = this.m_GuildData.closingTime;
				this.SetRemainTime();
			}
			this.m_btnDonation.UnLock(false);
			for (int i = 0; i < this.m_lstTab.Count; i++)
			{
				this.m_lstTab[i].CheckRedDot();
			}
		}

		// Token: 0x0600840C RID: 33804 RVA: 0x002C7C9C File Offset: 0x002C5E9C
		private void SetRemainTime()
		{
			if (NKCGuildManager.MyGuildData != null && NKCGuildManager.MyGuildData.guildState == GuildState.Closing)
			{
				TimeSpan timeSpan = this.m_tExpireTime - ServiceTime.Recent;
				if (timeSpan.TotalSeconds > 1.0)
				{
					NKCUtil.SetLabelText(this.m_lbBreakupDelayTime, string.Format(NKCUtilString.GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM_CLOSE, NKCUtilString.GetRemainTimeString(timeSpan, 2, true)));
					return;
				}
				NKCUtil.SetLabelText(this.m_lbBreakupDelayTime, NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_END_SOON", false));
			}
		}

		// Token: 0x0600840D RID: 33805 RVA: 0x002C7D14 File Offset: 0x002C5F14
		private void OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE uiType, bool bForce = false)
		{
			if (this.m_CurrentUIType == uiType && !bForce)
			{
				return;
			}
			this.m_CurrentUIType = uiType;
			NKCUtil.SetGameobjectActive(this.m_GuildLobbyInfo, uiType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info);
			NKCUtil.SetGameobjectActive(this.m_GuildLobbyMember, uiType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member);
			NKCUtil.SetGameobjectActive(this.m_GuildLobbyWelfare, uiType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point);
			NKCUtil.SetGameobjectActive(this.m_GuildLobbyMission, uiType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission);
			NKCUtil.SetGameobjectActive(this.m_GuildLobbyRank, uiType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Ranking);
			NKCUtil.SetGameobjectActive(this.m_GuildLobbyManage, uiType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Manage);
			for (int i = 0; i < this.m_lstTab.Count; i++)
			{
				if (this.m_lstTab[i].GetTabType() == uiType)
				{
					this.m_lstTab[i].m_tgl.Select(true, true, true);
				}
			}
			switch (uiType)
			{
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info:
			{
				NKCUtil.SetGameobjectActive(this.m_objNone, false);
				NKMGuildMemberData nkmguildMemberData = this.m_GuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
				if (nkmguildMemberData != null)
				{
					this.m_GuildLobbyInfo.SetData(nkmguildMemberData.grade, new NKCUIGuildLobbyInfo.OnMoveToTab(this.OnClickTab));
					return;
				}
				break;
			}
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member:
				NKCUtil.SetGameobjectActive(this.m_objNone, false);
				this.m_GuildLobbyMember.SetData(this.m_GuildData);
				return;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission:
				if (NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_MISSION, 0, 0))
				{
					NKCUtil.SetGameobjectActive(this.m_objNone, false);
					this.m_GuildLobbyMission.SetData();
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_GuildLobbyMission, false);
				NKCUtil.SetGameobjectActive(this.m_objNone, true);
				return;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point:
				if (NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_POINT, 0, 0))
				{
					NKCUtil.SetGameobjectActive(this.m_objNone, false);
					this.m_GuildLobbyWelfare.SetData();
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_GuildLobbyWelfare, false);
				NKCUtil.SetGameobjectActive(this.m_objNone, true);
				return;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Ranking:
				if (NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_RANKING, 0, 0))
				{
					NKCUtil.SetGameobjectActive(this.m_objNone, false);
					this.m_GuildLobbyRank.SetData();
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_GuildLobbyRank, false);
				NKCUtil.SetGameobjectActive(this.m_objNone, true);
				return;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Manage:
				NKCUtil.SetGameobjectActive(this.m_objNone, false);
				this.m_GuildLobbyManage.SetData(new NKCUIGuildLobbyManage.OnClose(this.OnClickTab));
				return;
			default:
				NKCUtil.SetGameobjectActive(this.m_objNone, false);
				break;
			}
		}

		// Token: 0x0600840E RID: 33806 RVA: 0x002C7F5A File Offset: 0x002C615A
		private void OnClickLvInfo()
		{
			NKCPopupGuildLvInfo.Instance.Open();
		}

		// Token: 0x0600840F RID: 33807 RVA: 0x002C7F68 File Offset: 0x002C6168
		private void OnClickHelp()
		{
			NKMGuildAttendanceData yesterdayAttendance = this.m_GuildData.GetYesterdayAttendance(ServiceTime.Recent);
			int lastAttendanceCount = (yesterdayAttendance != null) ? yesterdayAttendance.count : 0;
			NKCPopupGuildAttendance.Instance.Open(lastAttendanceCount);
		}

		// Token: 0x06008410 RID: 33808 RVA: 0x002C7F9E File Offset: 0x002C619E
		private void OnClickDonation()
		{
			NKCPopupGuildDonation.Instance.Open();
		}

		// Token: 0x06008411 RID: 33809 RVA: 0x002C7FAC File Offset: 0x002C61AC
		private void OnClickBreakup()
		{
			if (NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID).grade == GuildMemberGrade.Master)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_BODY_DESC, delegate()
				{
					NKCPacketSender.Send_NKMPacket_GUILD_CLOSE_CANCEL_REQ(NKCGuildManager.MyData.guildUid);
				}, null, false);
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_POPUP_BODY_DESC, delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_MASTER_MIGRATION_REQ(NKCGuildManager.MyData.guildUid);
			}, null, false);
		}

		// Token: 0x06008412 RID: 33810 RVA: 0x002C804F File Offset: 0x002C624F
		private void OnClickBreakupClose()
		{
			NKCUtil.SetGameobjectActive(this.m_objBreakupDelay, false);
		}

		// Token: 0x06008413 RID: 33811 RVA: 0x002C805D File Offset: 0x002C625D
		private void OnClickChat()
		{
			NKCUtil.SetGameobjectActive(this.m_objNewCount, false);
			NKCPopupGuildChat.Instance.Open(this.m_GuildData.guildUid);
		}

		// Token: 0x06008414 RID: 33812 RVA: 0x002C8080 File Offset: 0x002C6280
		public void SetNewChatCount(int count)
		{
			if (count > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objNewCount, true);
				NKCUtil.SetLabelText(this.m_lbNewCount, count.ToString());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNewCount, false);
		}

		// Token: 0x06008415 RID: 33813 RVA: 0x002C80B4 File Offset: 0x002C62B4
		public override void OnCompanyBuffUpdate(NKMUserData userData)
		{
			base.OnCompanyBuffUpdate(userData);
			if (this.m_CurrentUIType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point)
			{
				NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE currentUIType = this.m_CurrentUIType;
				this.m_CurrentUIType = NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.None;
				this.OnClickTab(currentUIType, true);
			}
		}

		// Token: 0x06008416 RID: 33814 RVA: 0x002C80E8 File Offset: 0x002C62E8
		public override void OnGuildDataChanged()
		{
			this.m_GuildData = NKCGuildManager.MyGuildData;
			if (this.m_GuildData != null)
			{
				this.SetGuildBasicData();
				NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE currentUIType = this.m_CurrentUIType;
				this.m_CurrentUIType = NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.None;
				this.OnClickTab(currentUIType, true);
			}
		}

		// Token: 0x06008417 RID: 33815 RVA: 0x002C8124 File Offset: 0x002C6324
		private void Update()
		{
			if (this.m_lbBreakupDelayTime.gameObject.activeInHierarchy)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetRemainTime();
				}
			}
		}

		// Token: 0x06008418 RID: 33816 RVA: 0x002C817A File Offset: 0x002C637A
		public void OpenShop()
		{
			NKCUIGuildLobbyInfo guildLobbyInfo = this.m_GuildLobbyInfo;
			if (guildLobbyInfo == null)
			{
				return;
			}
			guildLobbyInfo.OpenShop();
		}

		// Token: 0x06008419 RID: 33817 RVA: 0x002C818C File Offset: 0x002C638C
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.PrevTab)
			{
				return this.ToPrevTab();
			}
			if (hotkey == HotkeyEventType.NextTab)
			{
				return this.ToNextTab();
			}
			if (hotkey != HotkeyEventType.ShowHotkey)
			{
				return false;
			}
			if (this.m_lstTab.Count > 0)
			{
				NKCUIGuildLobbyTab nkcuiguildLobbyTab = this.m_lstTab[0];
				Transform parent;
				if (nkcuiguildLobbyTab == null)
				{
					parent = null;
				}
				else
				{
					NKCUIComToggle tgl = nkcuiguildLobbyTab.m_tgl;
					if (tgl == null)
					{
						parent = null;
					}
					else
					{
						NKCUIComToggleGroup toggleGroup = tgl.m_ToggleGroup;
						parent = ((toggleGroup != null) ? toggleGroup.transform : null);
					}
				}
				NKCUIComHotkeyDisplay.OpenInstance(parent, HotkeyEventType.NextTab);
			}
			return false;
		}

		// Token: 0x0600841A RID: 33818 RVA: 0x002C8200 File Offset: 0x002C6400
		private bool ToNextTab()
		{
			switch (this.m_CurrentUIType)
			{
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member, false);
				return true;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission, false);
				return true;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point, false);
				return true;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Ranking, false);
				return true;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Ranking:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info, false);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600841B RID: 33819 RVA: 0x002C8268 File Offset: 0x002C6468
		private bool ToPrevTab()
		{
			switch (this.m_CurrentUIType)
			{
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Ranking, false);
				return true;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info, false);
				return true;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member, false);
				return true;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission, false);
				return true;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Ranking:
				this.OnClickTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point, false);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0400701D RID: 28701
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x0400701E RID: 28702
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_LOBBY";

		// Token: 0x0400701F RID: 28703
		[Header("좌측 정보")]
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04007020 RID: 28704
		public Text m_lbName;

		// Token: 0x04007021 RID: 28705
		public Text m_lbLevel;

		// Token: 0x04007022 RID: 28706
		public Text m_lbExp;

		// Token: 0x04007023 RID: 28707
		public NKCUIComStateButton m_btnLvInfo;

		// Token: 0x04007024 RID: 28708
		public Image m_imgExpGauge;

		// Token: 0x04007025 RID: 28709
		public Text m_lbLeaderName;

		// Token: 0x04007026 RID: 28710
		public Text m_lbMemberCount;

		// Token: 0x04007027 RID: 28711
		public Text m_lbAttendanceCount;

		// Token: 0x04007028 RID: 28712
		public NKCUIComStateButton m_btnHelp;

		// Token: 0x04007029 RID: 28713
		public NKCUIComStateButton m_btnDonation;

		// Token: 0x0400702A RID: 28714
		public GameObject m_objAttendanceRedDot;

		// Token: 0x0400702B RID: 28715
		[Header("해체 유예기간")]
		public GameObject m_objBreakupDelay;

		// Token: 0x0400702C RID: 28716
		public Text m_lbBreakupDelayTime;

		// Token: 0x0400702D RID: 28717
		public NKCUIComStateButton m_btnBreakup;

		// Token: 0x0400702E RID: 28718
		public Text m_lbBreakupBtnText;

		// Token: 0x0400702F RID: 28719
		public NKCUIComStateButton m_btnBreakupClose;

		// Token: 0x04007030 RID: 28720
		[Header("상단 탭")]
		public List<NKCUIGuildLobbyTab> m_lstTab = new List<NKCUIGuildLobbyTab>();

		// Token: 0x04007031 RID: 28721
		[Header("우측 영역")]
		public NKCUIGuildLobbyInfo m_GuildLobbyInfo;

		// Token: 0x04007032 RID: 28722
		public NKCUIGuildLobbyMember m_GuildLobbyMember;

		// Token: 0x04007033 RID: 28723
		public NKCUIGuildLobbyMission m_GuildLobbyMission;

		// Token: 0x04007034 RID: 28724
		public NKCUIGuildLobbyWelfare m_GuildLobbyWelfare;

		// Token: 0x04007035 RID: 28725
		public NKCUIGuildLobbyRank m_GuildLobbyRank;

		// Token: 0x04007036 RID: 28726
		public NKCUIGuildLobbyManage m_GuildLobbyManage;

		// Token: 0x04007037 RID: 28727
		public GameObject m_objNone;

		// Token: 0x04007038 RID: 28728
		[Header("채팅창 호출")]
		public NKCUIComStateButton m_btnChat;

		// Token: 0x04007039 RID: 28729
		public GameObject m_objNewCount;

		// Token: 0x0400703A RID: 28730
		public Text m_lbNewCount;

		// Token: 0x0400703B RID: 28731
		private NKMGuildData m_GuildData;

		// Token: 0x0400703C RID: 28732
		private NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE m_CurrentUIType;

		// Token: 0x0400703D RID: 28733
		private DateTime m_tExpireTime;

		// Token: 0x0400703E RID: 28734
		private float m_fDeltaTime;

		// Token: 0x020018E7 RID: 6375
		public enum GUILD_LOBBY_UI_TYPE
		{
			// Token: 0x0400AA17 RID: 43543
			None,
			// Token: 0x0400AA18 RID: 43544
			Info,
			// Token: 0x0400AA19 RID: 43545
			Member,
			// Token: 0x0400AA1A RID: 43546
			Mission,
			// Token: 0x0400AA1B RID: 43547
			Point,
			// Token: 0x0400AA1C RID: 43548
			Ranking,
			// Token: 0x0400AA1D RID: 43549
			Manage,
			// Token: 0x0400AA1E RID: 43550
			Invite
		}
	}
}
