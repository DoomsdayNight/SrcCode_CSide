using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using NKM;
using NKM.Guild;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B41 RID: 2881
	public class NKCPopupGuildInfo : NKCUIBase
	{
		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06008332 RID: 33586 RVA: 0x002C3EC8 File Offset: 0x002C20C8
		public static NKCPopupGuildInfo Instance
		{
			get
			{
				if (NKCPopupGuildInfo.m_Instance == null)
				{
					NKCPopupGuildInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildInfo>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_POPUP_INFO", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildInfo.CleanupInstance)).GetInstance<NKCPopupGuildInfo>();
					if (NKCPopupGuildInfo.m_Instance != null)
					{
						NKCPopupGuildInfo.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildInfo.m_Instance;
			}
		}

		// Token: 0x06008333 RID: 33587 RVA: 0x002C3F29 File Offset: 0x002C2129
		private static void CleanupInstance()
		{
			NKCPopupGuildInfo.m_Instance = null;
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06008334 RID: 33588 RVA: 0x002C3F31 File Offset: 0x002C2131
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildInfo.m_Instance != null && NKCPopupGuildInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008335 RID: 33589 RVA: 0x002C3F4C File Offset: 0x002C214C
		private void OnDestroy()
		{
			NKCPopupGuildInfo.m_Instance = null;
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06008336 RID: 33590 RVA: 0x002C3F54 File Offset: 0x002C2154
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06008337 RID: 33591 RVA: 0x002C3F57 File Offset: 0x002C2157
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008338 RID: 33592 RVA: 0x002C3F5E File Offset: 0x002C215E
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008339 RID: 33593 RVA: 0x002C3F6C File Offset: 0x002C216C
		private RectTransform GetObject(int index)
		{
			NKCUIGuildMemberSlot nkcuiguildMemberSlot;
			if (this.m_stkMember.Count > 0)
			{
				nkcuiguildMemberSlot = this.m_stkMember.Pop();
			}
			else
			{
				nkcuiguildMemberSlot = NKCUIGuildMemberSlot.GetNewInstance(this.m_trContentParent, new NKCUIGuildMemberSlot.OnSelectedSlot(this.OnSelectedSlot));
			}
			this.m_lstVisibleSlot.Add(nkcuiguildMemberSlot);
			NKCUtil.SetGameobjectActive(nkcuiguildMemberSlot, false);
			if (nkcuiguildMemberSlot == null)
			{
				return null;
			}
			return nkcuiguildMemberSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600833A RID: 33594 RVA: 0x002C3FD0 File Offset: 0x002C21D0
		private void ReturnObject(Transform tr)
		{
			NKCUIGuildMemberSlot component = tr.GetComponent<NKCUIGuildMemberSlot>();
			this.m_lstVisibleSlot.Remove(component);
			this.m_stkMember.Push(component);
			NKCUtil.SetGameobjectActive(component, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x0600833B RID: 33595 RVA: 0x002C4010 File Offset: 0x002C2210
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIGuildMemberSlot component = tr.GetComponent<NKCUIGuildMemberSlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			component.SetData(this.m_GuildData.members[idx], this.m_GuildData.guildUid == NKCGuildManager.MyData.guildUid);
		}

		// Token: 0x0600833C RID: 33596 RVA: 0x002C4064 File Offset: 0x002C2264
		public void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnJoin.PointerClick.RemoveAllListeners();
			this.m_btnJoin.PointerClick.AddListener(new UnityAction(this.OnClickJoin));
			NKCUtil.SetHotkey(this.m_btnJoin, HotkeyEventType.Confirm, null, false);
			this.m_loopMember.dOnGetObject += this.GetObject;
			this.m_loopMember.dOnReturnObject += this.ReturnObject;
			this.m_loopMember.dOnProvideData += this.ProvideData;
			this.m_loopMember.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopMember, null);
			this.m_BadgeUI.InitUI();
		}

		// Token: 0x0600833D RID: 33597 RVA: 0x002C4140 File Offset: 0x002C2340
		public void Open(NKMGuildData guildData)
		{
			this.m_GuildData = guildData;
			this.SetBasicData(guildData);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loopMember.TotalCount = this.m_GuildData.members.Count;
			this.m_loopMember.SetIndexPosition(0);
			base.UIOpened(true);
		}

		// Token: 0x0600833E RID: 33598 RVA: 0x002C4198 File Offset: 0x002C2398
		private void SetBasicData(NKMGuildData guildData)
		{
			this.m_BadgeUI.SetData(guildData.badgeId);
			NKCUtil.SetLabelText(this.m_lbName, guildData.name);
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, guildData.guildLevel));
			NKCUtil.SetLabelText(this.m_lbDesc, guildData.greeting);
			switch (guildData.guildJoinType)
			{
			case GuildJoinType.DirectJoin:
				NKCUtil.SetLabelText(this.m_lbJoinType, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_RIGHTOFF_DESC);
				break;
			case GuildJoinType.NeedApproval:
				NKCUtil.SetLabelText(this.m_lbJoinType, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_CONFIRM_DESC);
				break;
			case GuildJoinType.Closed:
				NKCUtil.SetLabelText(this.m_lbJoinType, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_BLIND_DESC);
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_btnJoin, !NKCGuildManager.HasGuild() && guildData.guildJoinType != GuildJoinType.Closed && !NKCGuildManager.AlreadyRequested(this.m_GuildData.guildUid) && !NKCGuildManager.AlreadyInvited(this.m_GuildData.guildUid));
			NKCUtil.SetLabelText(this.m_lbMemberCount, string.Format("({0}/{1})", guildData.members.Count, NKMTempletContainer<GuildExpTemplet>.Find(guildData.guildLevel).MaxMemberCount));
		}

		// Token: 0x0600833F RID: 33599 RVA: 0x002C42C4 File Offset: 0x002C24C4
		private void OnSelectedSlot(long userUid)
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(userUid, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x06008340 RID: 33600 RVA: 0x002C42CD File Offset: 0x002C24CD
		private void OnClickJoin()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_CONFIRM_JOIN_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_RIGHTOFF_JOIN_POPUP_BODY_DESC, this.m_GuildData.name), new NKCPopupOKCancel.OnButton(this.OnConfirmJoin), null, false);
		}

		// Token: 0x06008341 RID: 33601 RVA: 0x002C42FC File Offset: 0x002C24FC
		private void OnConfirmJoin()
		{
			NKCGuildManager.Send_GUILD_JOIN_REQ(this.m_GuildData.guildUid, this.m_GuildData.name, this.m_GuildData.guildJoinType);
			base.Close();
		}

		// Token: 0x04006F63 RID: 28515
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006F64 RID: 28516
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_POPUP_INFO";

		// Token: 0x04006F65 RID: 28517
		private static NKCPopupGuildInfo m_Instance;

		// Token: 0x04006F66 RID: 28518
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04006F67 RID: 28519
		public Text m_lbLevel;

		// Token: 0x04006F68 RID: 28520
		public Text m_lbName;

		// Token: 0x04006F69 RID: 28521
		public Text m_lbDesc;

		// Token: 0x04006F6A RID: 28522
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006F6B RID: 28523
		public Text m_lbMemberCount;

		// Token: 0x04006F6C RID: 28524
		public LoopScrollRect m_loopMember;

		// Token: 0x04006F6D RID: 28525
		public Transform m_trContentParent;

		// Token: 0x04006F6E RID: 28526
		public NKCUIComStateButton m_btnJoin;

		// Token: 0x04006F6F RID: 28527
		public Text m_lbJoinType;

		// Token: 0x04006F70 RID: 28528
		private Stack<NKCUIGuildMemberSlot> m_stkMember = new Stack<NKCUIGuildMemberSlot>();

		// Token: 0x04006F71 RID: 28529
		private List<NKCUIGuildMemberSlot> m_lstVisibleSlot = new List<NKCUIGuildMemberSlot>();

		// Token: 0x04006F72 RID: 28530
		private NKMGuildData m_GuildData;
	}
}
