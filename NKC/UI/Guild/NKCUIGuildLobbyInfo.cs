using System;
using ClientPacket.Guild;
using Cs.Core.Util;
using NKC.UI.Shop;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B52 RID: 2898
	public class NKCUIGuildLobbyInfo : MonoBehaviour
	{
		// Token: 0x0600841D RID: 33821 RVA: 0x002C82E0 File Offset: 0x002C64E0
		public void InitUI()
		{
			this.m_IFNotice.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			this.m_IFNotice.onEndEdit.RemoveAllListeners();
			this.m_IFNotice.onEndEdit.AddListener(new UnityAction<string>(this.OnIFChanged));
			this.m_IFNotice.characterLimit = 36;
			this.m_btnEditGuildNotice.PointerClick.RemoveAllListeners();
			this.m_btnEditGuildNotice.PointerClick.AddListener(new UnityAction(this.OnClickEditGuildNotice));
			this.m_btnEditGuildNotice.m_bGetCallbackWhileLocked = true;
			if (this.m_btnConsortiumCoop != null)
			{
				this.m_btnConsortiumCoop.PointerClick.RemoveAllListeners();
				this.m_btnConsortiumCoop.PointerClick.AddListener(new UnityAction(this.OnClickCoop));
				this.m_btnConsortiumCoop.m_bGetCallbackWhileLocked = true;
			}
			NKCUIComStateButton btnShop = this.m_btnShop;
			if (btnShop != null)
			{
				btnShop.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnShop2 = this.m_btnShop;
			if (btnShop2 != null)
			{
				btnShop2.PointerClick.AddListener(new UnityAction(this.OnClickShop));
			}
			NKCUIComStateButton btnMission = this.m_btnMission;
			if (btnMission != null)
			{
				btnMission.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnMission2 = this.m_btnMission;
			if (btnMission2 != null)
			{
				btnMission2.PointerClick.AddListener(new UnityAction(this.OnClickMission));
			}
			this.m_btnManage.PointerClick.RemoveAllListeners();
			this.m_btnManage.PointerClick.AddListener(new UnityAction(this.OnClickManage));
			this.m_tLastPacketSendTime = DateTime.MinValue;
		}

		// Token: 0x0600841E RID: 33822 RVA: 0x002C8464 File Offset: 0x002C6664
		public void SetData(GuildMemberGrade myGrade, NKCUIGuildLobbyInfo.OnMoveToTab onMoveToTab)
		{
			this.m_dOnMoveToTab = onMoveToTab;
			this.ResetPosition();
			this.m_IFNotice.text = NKCFilterManager.CheckBadChat(NKCGuildManager.MyGuildData.notice);
			this.m_IFNotice.interactable = (myGrade != GuildMemberGrade.Member);
			NKCUtil.SetGameobjectActive(this.m_btnManage, myGrade == GuildMemberGrade.Master);
			NKCUtil.SetGameobjectActive(this.m_btnEditGuildNotice, myGrade != GuildMemberGrade.Member);
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_SHOP, 0, 0))
			{
				this.m_btnShop.Lock(false);
			}
			else
			{
				this.m_btnShop.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON, 0, 0) || !NKCGuildCoopManager.CheckFirstSeasonStarted())
			{
				NKCUtil.SetGameobjectActive(this.m_objCoopNotice, false);
				this.m_btnConsortiumCoop.Lock(false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objCoopNotice, NKCGuildCoopManager.m_GuildDungeonState > GuildDungeonState.Invalid);
				if (this.m_objCoopNotice.activeSelf)
				{
					this.SetCoopStatusTime();
				}
				this.m_btnConsortiumCoop.UnLock(false);
			}
			this.m_bCoopStatusChanged = false;
			switch (NKCGuildCoopManager.m_GuildDungeonState)
			{
			case GuildDungeonState.PlayableGuildDungeon:
				NKCUtil.SetLabelText(this.m_lbCoopStatus, NKCUtilString.GET_STRING_CONSORTIUM_LOBBY_MESSAGE_IN_PROGRESS);
				break;
			case GuildDungeonState.SeasonOut:
			case GuildDungeonState.SessionOut:
				NKCUtil.SetLabelText(this.m_lbCoopStatus, NKCUtilString.GET_STRING_CONSORTIUM_LOBBY_MESSAGE_NOT_IN_PROGRESS);
				break;
			case GuildDungeonState.Adjust:
				NKCUtil.SetLabelText(this.m_lbCoopStatus, NKCUtilString.GET_STRING_CONSORTIUM_LOBBY_MESSAGE_CALCULATE);
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_objGuildCoopRedDot, NKCGuildCoopManager.CheckSeasonRewardEnable());
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_MISSION, 0, 0))
			{
				this.m_btnMission.Lock(false);
				return;
			}
			this.m_btnMission.UnLock(false);
		}

		// Token: 0x0600841F RID: 33823 RVA: 0x002C85E0 File Offset: 0x002C67E0
		private void SetCoopStatusTime()
		{
			switch (NKCGuildCoopManager.m_GuildDungeonState)
			{
			case GuildDungeonState.Invalid:
				if (!NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC) || !NKCGuildCoopManager.HasNextSessionData(NKCGuildCoopManager.m_NextSessionStartDateUTC))
				{
					NKCUtil.SetGameobjectActive(this.m_objCoopNotice, false);
					return;
				}
				if (!this.m_bCoopStatusChanged)
				{
					this.m_bCoopStatusChanged = true;
					this.SendPacket();
					return;
				}
				break;
			case GuildDungeonState.PlayableGuildDungeon:
				if (!NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_SessionEndDateUTC))
				{
					NKCUtil.SetLabelText(this.m_lbCoopRemainTime, NKCUtilString.GetTimeSpanStringDay(NKCGuildCoopManager.m_SessionEndDateUTC - NKCSynchronizedTime.GetServerUTCTime(0.0)));
					return;
				}
				if (!this.m_bCoopStatusChanged)
				{
					this.m_bCoopStatusChanged = true;
					this.SendPacket();
					return;
				}
				break;
			case GuildDungeonState.SeasonOut:
				NKCUtil.SetGameobjectActive(this.m_objCoopNotice, true);
				if (!NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC))
				{
					NKCUtil.SetLabelText(this.m_lbCoopRemainTime, NKCUtilString.GetTimeSpanStringDay(NKCGuildCoopManager.m_NextSessionStartDateUTC - NKCSynchronizedTime.GetServerUTCTime(0.0)));
					return;
				}
				if (!NKCGuildCoopManager.HasNextSessionData(NKCGuildCoopManager.m_NextSessionStartDateUTC))
				{
					NKCUtil.SetGameobjectActive(this.m_objCoopNotice, false);
					return;
				}
				if (!this.m_bCoopStatusChanged)
				{
					this.m_bCoopStatusChanged = true;
					this.SendPacket();
					return;
				}
				break;
			case GuildDungeonState.SessionOut:
				if (!NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC))
				{
					NKCUtil.SetLabelText(this.m_lbCoopRemainTime, NKCUtilString.GetTimeSpanStringDay(NKCGuildCoopManager.m_NextSessionStartDateUTC - NKCSynchronizedTime.GetServerUTCTime(0.0)));
					return;
				}
				if (!this.m_bCoopStatusChanged)
				{
					this.m_bCoopStatusChanged = true;
					this.SendPacket();
					return;
				}
				break;
			case GuildDungeonState.Adjust:
				if (NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC) && !this.m_bCoopStatusChanged)
				{
					this.m_bCoopStatusChanged = true;
					this.SendPacket();
				}
				NKCUtil.SetLabelText(this.m_lbCoopRemainTime, NKCUtilString.GetTimeSpanStringDay(NKCGuildCoopManager.m_NextSessionStartDateUTC - NKCSynchronizedTime.GetServerUTCTime(0.0)));
				break;
			default:
				return;
			}
		}

		// Token: 0x06008420 RID: 33824 RVA: 0x002C87A4 File Offset: 0x002C69A4
		private void SendPacket()
		{
			if ((NKCSynchronizedTime.ServiceTime - this.m_tLastPacketSendTime).TotalSeconds > 10.0)
			{
				this.m_tLastPacketSendTime = NKCSynchronizedTime.ServiceTime;
				NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_INFO_REQ(NKCGuildManager.MyData.guildUid);
			}
		}

		// Token: 0x06008421 RID: 33825 RVA: 0x002C87EE File Offset: 0x002C69EE
		public void ResetPosition()
		{
			this.m_scMain.normalizedPosition = new Vector2(0.5f, 0.5f);
		}

		// Token: 0x06008422 RID: 33826 RVA: 0x002C880A File Offset: 0x002C6A0A
		private void OnIFChanged(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return;
			}
			this.m_IFNotice.text = NKCFilterManager.CheckBadChat(str);
			if (NKCInputManager.IsChatSubmitEnter())
			{
				this.OnClickEditGuildNotice();
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x06008423 RID: 33827 RVA: 0x002C8840 File Offset: 0x002C6A40
		private void OnClickEditGuildNotice()
		{
			if (this.m_btnEditGuildNotice.m_bLock)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_NOTICE_CHANGE_COOLTIME_TOAST_TEXT", false), null, "");
				return;
			}
			if (!string.IsNullOrWhiteSpace(this.m_IFNotice.text) && !string.Equals(this.m_IFNotice.text, NKCGuildManager.MyGuildData.notice))
			{
				NKCPacketSender.Send_NKMPacket_GUILD_UPDATE_NOTICE_REQ(NKCGuildManager.MyData.guildUid, this.m_IFNotice.text);
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_NOTICE_NOT_CHANGE_TOAST_TEXT", false), null, "");
		}

		// Token: 0x06008424 RID: 33828 RVA: 0x002C88DC File Offset: 0x002C6ADC
		private void OnClickCoop()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON, 0, 0) || NKCGuildCoopManager.GetFirstSeasonTemplet() == null)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.GUILD_DUNGEON, 0);
				return;
			}
			if (NKCGuildCoopManager.m_GuildDungeonState == GuildDungeonState.Invalid && !NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CONSORTIUM_SEASON_OPEN_BEFORE_TOAST_TEXT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (NKCSynchronizedTime.IsFinished(NKCGuildCoopManager.m_NextSessionStartDateUTC) && NKCGuildCoopManager.HasNextSessionData(NKCGuildCoopManager.m_NextSessionStartDateUTC))
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().SetReserveMoveToCoopScen(true);
				NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_INFO_REQ(NKCGuildManager.MyData.guildUid);
				return;
			}
			if (NKCGuildCoopManager.m_GuildDungeonState != GuildDungeonState.Invalid && GuildDungeonTempletManager.GetCurrentSeasonTemplet(ServiceTime.Recent) != null)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_COOP, true);
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06008425 RID: 33829 RVA: 0x002C898E File Offset: 0x002C6B8E
		private NKCUIShopSingle ConsortiumShop
		{
			get
			{
				if (this.m_ConsortiumShop == null)
				{
					this.m_ConsortiumShop = NKCUIShopSingle.GetInstance("ab_ui_nkm_ui_consortium_shop", "NKM_UI_CONSORTIUM_SHOP");
				}
				return this.m_ConsortiumShop;
			}
		}

		// Token: 0x06008426 RID: 33830 RVA: 0x002C89B9 File Offset: 0x002C6BB9
		private void OnClickShop()
		{
			this.OpenShop();
		}

		// Token: 0x06008427 RID: 33831 RVA: 0x002C89C1 File Offset: 0x002C6BC1
		public void OpenShop()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_SHOP, 0, 0))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_NONE_SYSTEM", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCUIShop.ShopShortcut("TAB_EXCHANGE_GUILD_COIN", 0, 0);
		}

		// Token: 0x06008428 RID: 33832 RVA: 0x002C8A00 File Offset: 0x002C6C00
		private void OnClickMission()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_MISSION, 0, 0))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_PF_CONSORTIUM_LOBBY_NONE_SYSTEM", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCUIGuildLobbyInfo.OnMoveToTab dOnMoveToTab = this.m_dOnMoveToTab;
			if (dOnMoveToTab == null)
			{
				return;
			}
			dOnMoveToTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission, false);
		}

		// Token: 0x06008429 RID: 33833 RVA: 0x002C8A4E File Offset: 0x002C6C4E
		private void OnClickManage()
		{
			NKCUIGuildLobbyInfo.OnMoveToTab dOnMoveToTab = this.m_dOnMoveToTab;
			if (dOnMoveToTab == null)
			{
				return;
			}
			dOnMoveToTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Manage, false);
		}

		// Token: 0x0600842A RID: 33834 RVA: 0x002C8A64 File Offset: 0x002C6C64
		private void Update()
		{
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				if (NKCGuildManager.LastNoticeChangedTimeUTC.Add(GuildTemplet.NoticeCooltime) > NKCSynchronizedTime.GetServerUTCTime(0.0))
				{
					this.m_IFNotice.enabled = false;
					this.m_btnEditGuildNotice.Lock(false);
				}
				else
				{
					this.m_IFNotice.enabled = true;
					this.m_btnEditGuildNotice.UnLock(false);
				}
				if (NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON, 0, 0) && NKCGuildCoopManager.GetFirstSeasonTemplet() != null)
				{
					this.SetCoopStatusTime();
				}
			}
		}

		// Token: 0x0400703F RID: 28735
		public InputField m_IFNotice;

		// Token: 0x04007040 RID: 28736
		public NKCUIComStateButton m_btnEditGuildNotice;

		// Token: 0x04007041 RID: 28737
		public NKCUIComStateButton m_btnConsortiumCoop;

		// Token: 0x04007042 RID: 28738
		public NKCUIComStateButton m_btnShop;

		// Token: 0x04007043 RID: 28739
		public NKCUIComStateButton m_btnMission;

		// Token: 0x04007044 RID: 28740
		public NKCUIComStateButton m_btnManage;

		// Token: 0x04007045 RID: 28741
		public ScrollRect m_scMain;

		// Token: 0x04007046 RID: 28742
		[Header("협력전 알림")]
		public GameObject m_objCoopNotice;

		// Token: 0x04007047 RID: 28743
		public Text m_lbCoopStatus;

		// Token: 0x04007048 RID: 28744
		public Text m_lbCoopRemainTime;

		// Token: 0x04007049 RID: 28745
		public GameObject m_objGuildCoopRedDot;

		// Token: 0x0400704A RID: 28746
		private NKCUIGuildLobbyInfo.OnMoveToTab m_dOnMoveToTab;

		// Token: 0x0400704B RID: 28747
		private float m_fDeltaTime;

		// Token: 0x0400704C RID: 28748
		private bool m_bCoopStatusChanged;

		// Token: 0x0400704D RID: 28749
		private DateTime m_tLastPacketSendTime = DateTime.MinValue;

		// Token: 0x0400704E RID: 28750
		private NKCUIShopSingle m_ConsortiumShop;

		// Token: 0x020018E9 RID: 6377
		// (Invoke) Token: 0x0600B726 RID: 46886
		public delegate void OnMoveToTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE lobbyUiType, bool bForce = false);
	}
}
