using System;
using System.Linq;
using System.Text;
using ClientPacket.Guild;
using Cs.Logging;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B4D RID: 2893
	public class NKCUIGuildCreate : NKCUIBase
	{
		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x060083AD RID: 33709 RVA: 0x002C5D94 File Offset: 0x002C3F94
		public static NKCUIGuildCreate Instance
		{
			get
			{
				if (NKCUIGuildCreate.m_Instance == null)
				{
					NKCUIGuildCreate.m_Instance = NKCUIManager.OpenNewInstance<NKCUIGuildCreate>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_FOUNDATION", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGuildCreate.CleanupInstance)).GetInstance<NKCUIGuildCreate>();
					if (NKCUIGuildCreate.m_Instance != null)
					{
						NKCUIGuildCreate.m_Instance.InitUI();
					}
				}
				return NKCUIGuildCreate.m_Instance;
			}
		}

		// Token: 0x060083AE RID: 33710 RVA: 0x002C5DF5 File Offset: 0x002C3FF5
		private static void CleanupInstance()
		{
			NKCUIGuildCreate.m_Instance = null;
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x060083AF RID: 33711 RVA: 0x002C5DFD File Offset: 0x002C3FFD
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGuildCreate.m_Instance != null && NKCUIGuildCreate.m_Instance.IsOpen;
			}
		}

		// Token: 0x060083B0 RID: 33712 RVA: 0x002C5E18 File Offset: 0x002C4018
		private void OnDestroy()
		{
			NKCUIGuildCreate.m_Instance = null;
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x060083B1 RID: 33713 RVA: 0x002C5E20 File Offset: 0x002C4020
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_CONSORTIUM_INTRO_FOUNDATION;
			}
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x060083B2 RID: 33714 RVA: 0x002C5E27 File Offset: 0x002C4027
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x060083B3 RID: 33715 RVA: 0x002C5E2A File Offset: 0x002C402A
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x060083B4 RID: 33716 RVA: 0x002C5E2D File Offset: 0x002C402D
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060083B5 RID: 33717 RVA: 0x002C5E3C File Offset: 0x002C403C
		public void InitUI()
		{
			this.m_tglJoinTypeDirect.OnValueChanged.RemoveAllListeners();
			this.m_tglJoinTypeDirect.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelectDirect));
			this.m_tglJoinTypeApproval.OnValueChanged.RemoveAllListeners();
			this.m_tglJoinTypeApproval.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelectApproval));
			this.m_tglJoinTypeClosed.OnValueChanged.RemoveAllListeners();
			this.m_tglJoinTypeClosed.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelectClosed));
			NKCUIGuildBadge badgeUI = this.m_badgeUI;
			if (badgeUI != null)
			{
				badgeUI.InitUI();
			}
			this.m_btnBadgeSetting.PointerClick.RemoveAllListeners();
			this.m_btnBadgeSetting.PointerClick.AddListener(new UnityAction(this.OnClickBadgeSetting));
			this.m_btnBadgeRandom.PointerClick.RemoveAllListeners();
			this.m_btnBadgeRandom.PointerClick.AddListener(new UnityAction(this.OnClickBadgeRandom));
			this.m_btnCreate.PointerClick.RemoveAllListeners();
			this.m_btnCreate.PointerClick.AddListener(new UnityAction(this.OnClickCreate));
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.NICKNAME_LIMIT_ENG))
			{
				this.m_inputName.contentType = InputField.ContentType.Alphanumeric;
			}
			else
			{
				this.m_inputName.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			}
			this.m_inputName.onValueChanged.RemoveAllListeners();
			this.m_inputName.onValueChanged.AddListener(new UnityAction<string>(this.OnNameChanged));
			this.m_inputName.onEndEdit.RemoveAllListeners();
			this.m_inputName.onEndEdit.AddListener(new UnityAction<string>(this.OnNameChangeEnd));
			this.m_inputName.characterLimit = 16;
			this.m_inputDesc.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			this.m_inputDesc.onEndEdit.RemoveAllListeners();
			this.m_inputDesc.onEndEdit.AddListener(new UnityAction<string>(this.OnDescChanged));
			this.m_inputDesc.characterLimit = 40;
		}

		// Token: 0x060083B6 RID: 33718 RVA: 0x002C6048 File Offset: 0x002C4248
		public void Open()
		{
			this.ResetUI();
			NKCUtil.SetImageSprite(this.m_imgCostItem, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(NKMCommonConst.Guild.Creation.ReqMiscItems[0].ItemId), false);
			NKCUtil.SetLabelText(this.m_lbCost, NKMCommonConst.Guild.Creation.ReqMiscItems[0].Count.ToString());
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetButtonState();
			base.UIOpened(true);
		}

		// Token: 0x060083B7 RID: 33719 RVA: 0x002C60CC File Offset: 0x002C42CC
		private void ResetUI()
		{
			this.m_inputName.text = string.Empty;
			this.m_inputDesc.text = string.Empty;
			NKCUtil.SetLabelText(this.m_lbGuildName, string.Format("[{0}]", this.m_GuildName));
			this.m_BadgeId = 0L;
			this.OnNameChanged(this.m_inputName.text);
			this.SetGuildBadge(this.m_BadgeId);
			switch (this.m_JoinType)
			{
			case GuildJoinType.DirectJoin:
				NKCUtil.SetLabelText(this.m_lbJoinTypeDesc, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_RIGHTOFF_DESC);
				break;
			case GuildJoinType.NeedApproval:
				NKCUtil.SetLabelText(this.m_lbJoinTypeDesc, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_CONFIRM_DESC);
				break;
			case GuildJoinType.Closed:
				NKCUtil.SetLabelText(this.m_lbJoinTypeDesc, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_BLIND_DESC);
				break;
			}
			NKCUtil.SetLabelText(this.m_lbDesc, string.Format(NKCStringTable.GetString("SI_DP_CONSORTIUM_CREATE_STORY_BODY_DESC", false), NKCScenManager.CurrentUserData().m_UserNickName));
		}

		// Token: 0x060083B8 RID: 33720 RVA: 0x002C61B0 File Offset: 0x002C43B0
		private void SetButtonState()
		{
			if (this.m_BadgeId == 0L || !this.m_bValidName)
			{
				this.m_btnCreate.Lock(false);
				NKCUtil.SetLabelTextColor(this.m_lbBtn, NKCUtil.GetColor(this.DISABLE_TEXT_COLOR_TEXT));
				NKCUtil.SetLabelTextColor(this.m_lbCost, NKCUtil.GetColor(this.DISABLE_TEXT_COLOR_TEXT));
				return;
			}
			this.m_btnCreate.UnLock(false);
			NKCUtil.SetLabelTextColor(this.m_lbBtn, NKCUtil.GetColor(this.NORMAL_TEXT_COLOR_TEXT));
			NKCUtil.SetLabelTextColor(this.m_lbCost, NKCUtil.GetColor(this.NORMAL_TEXT_COLOR_TEXT));
		}

		// Token: 0x060083B9 RID: 33721 RVA: 0x002C623E File Offset: 0x002C443E
		public void OnSelectDirect(bool bValue)
		{
			if (bValue)
			{
				this.m_JoinType = GuildJoinType.DirectJoin;
				NKCUtil.SetLabelText(this.m_lbJoinTypeDesc, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_RIGHTOFF_DESC);
			}
		}

		// Token: 0x060083BA RID: 33722 RVA: 0x002C625A File Offset: 0x002C445A
		public void OnSelectApproval(bool bValue)
		{
			if (bValue)
			{
				this.m_JoinType = GuildJoinType.NeedApproval;
				NKCUtil.SetLabelText(this.m_lbJoinTypeDesc, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_CONFIRM_DESC);
			}
		}

		// Token: 0x060083BB RID: 33723 RVA: 0x002C6276 File Offset: 0x002C4476
		public void OnSelectClosed(bool bValue)
		{
			if (bValue)
			{
				this.m_JoinType = GuildJoinType.Closed;
				NKCUtil.SetLabelText(this.m_lbJoinTypeDesc, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_GUIDE_BLIND_DESC);
			}
		}

		// Token: 0x060083BC RID: 33724 RVA: 0x002C6292 File Offset: 0x002C4492
		public void SetGuildBadge(long badgeId)
		{
			if (badgeId == 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_badgeUI, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_badgeUI, true);
				this.m_badgeUI.SetData(badgeId);
				this.m_BadgeId = badgeId;
			}
			this.SetButtonState();
		}

		// Token: 0x060083BD RID: 33725 RVA: 0x002C62CA File Offset: 0x002C44CA
		private void OnNameChanged(string str)
		{
			Log.Debug("GuildName : " + str, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCUIGuildCreate.cs", 250);
			this.CheckGuildName(str, false);
		}

		// Token: 0x060083BE RID: 33726 RVA: 0x002C62EE File Offset: 0x002C44EE
		private void OnNameChangeEnd(string str)
		{
			this.CheckGuildName(str, true);
		}

		// Token: 0x060083BF RID: 33727 RVA: 0x002C62F8 File Offset: 0x002C44F8
		private void CheckGuildName(string str, bool bChangeBadchat)
		{
			this.m_bValidName = true;
			this.m_GuildName = str;
			if (!NKCGuildManager.CheckNameLength(this.m_GuildName, 2, 16))
			{
				this.m_bValidName = false;
			}
			if (!NKCFilterManager.CheckNickNameFilter(this.m_GuildName))
			{
				this.m_bValidName = false;
			}
			if (bChangeBadchat)
			{
				this.m_inputName.text = NKCFilterManager.CheckBadChat(this.m_GuildName);
				if (!string.Equals(this.m_inputName.text, this.m_GuildName))
				{
					this.m_bValidName = false;
				}
			}
			else if (!string.Equals(NKCFilterManager.CheckBadChat(this.m_GuildName), this.m_GuildName))
			{
				this.m_bValidName = false;
			}
			if (!NKCFilterManager.CheckBadGuildname(this.m_GuildName))
			{
				this.m_bValidName = false;
			}
			NKCUtil.SetLabelText(this.m_lbGuildName, string.Format("[{0}]", this.m_GuildName));
			if (this.m_bValidName)
			{
				NKCUtil.SetLabelText(this.m_lbNameValid, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_NAME_SUB_GUIDE_USEFUL);
				NKCUtil.SetLabelTextColor(this.m_lbNameValid, NKCUtil.GetColor("#12a9ff"));
				NKCUtil.SetGameobjectActive(this.m_imgNameValid, true);
				NKCUtil.SetImageSprite(this.m_imgNameValid, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Sprite", "AB_UI_NKM_UI_CONSORTIUM_ICON_CHECK", false), false);
			}
			else if (string.IsNullOrWhiteSpace(this.m_GuildName))
			{
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.NICKNAME_LIMIT_ENG))
				{
					NKCUtil.SetLabelText(this.m_lbNameValid, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BASIC_DESC_GLOBAL);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbNameValid, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BASIC_DESC);
				}
				NKCUtil.SetLabelTextColor(this.m_lbNameValid, NKCUtil.GetColor("#656565"));
				NKCUtil.SetGameobjectActive(this.m_imgNameValid, false);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbNameValid, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_NAME_SUB_GUIDE_BADWORD);
				NKCUtil.SetLabelTextColor(this.m_lbNameValid, NKCUtil.GetColor("#ff2626"));
				NKCUtil.SetGameobjectActive(this.m_imgNameValid, true);
				NKCUtil.SetImageSprite(this.m_imgNameValid, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Sprite", "AB_UI_NKM_UI_CONSORTIUM_ICON_DENIED", false), false);
			}
			this.SetButtonState();
		}

		// Token: 0x060083C0 RID: 33728 RVA: 0x002C64CE File Offset: 0x002C46CE
		private void OnDescChanged(string str)
		{
			this.m_inputDesc.text = NKCFilterManager.CheckBadChat(str);
		}

		// Token: 0x060083C1 RID: 33729 RVA: 0x002C64E4 File Offset: 0x002C46E4
		private void OnClickBadgeRandom()
		{
			int num = UnityEngine.Random.Range(1, NKMTempletContainer<NKMGuildBadgeFrameTemplet>.Values.Count<NKMGuildBadgeFrameTemplet>());
			int num2 = UnityEngine.Random.Range(1, NKMTempletContainer<NKMGuildBadgeColorTemplet>.Values.Count<NKMGuildBadgeColorTemplet>());
			int num3 = UnityEngine.Random.Range(1, NKMTempletContainer<NKMGuildBadgeMarkTemplet>.Values.Count<NKMGuildBadgeMarkTemplet>());
			int num4 = UnityEngine.Random.Range(1, NKMTempletContainer<NKMGuildBadgeColorTemplet>.Values.Count<NKMGuildBadgeColorTemplet>());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(num.ToString("D3"));
			stringBuilder.Append(num2.ToString("D3"));
			stringBuilder.Append(num3.ToString("D3"));
			stringBuilder.Append(num4.ToString("D3"));
			this.m_BadgeId = long.Parse(stringBuilder.ToString());
			this.SetGuildBadge(this.m_BadgeId);
		}

		// Token: 0x060083C2 RID: 33730 RVA: 0x002C65AA File Offset: 0x002C47AA
		private void OnClickBadgeSetting()
		{
			if (!NKCPopupGuildBadgeSetting.IsInstanceOpen)
			{
				NKCPopupGuildBadgeSetting.Instance.Open(new NKCPopupGuildBadgeSetting.OnClose(this.SetGuildBadge), this.m_BadgeId);
			}
		}

		// Token: 0x060083C3 RID: 33731 RVA: 0x002C65D0 File Offset: 0x002C47D0
		private void OnClickCreate()
		{
			if (!this.m_bValidName)
			{
				return;
			}
			if (NKCScenManager.CurrentUserData().UserLevel < NKMCommonConst.Guild.Creation.UserMinLevel)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_REQUIRE_MORE_USER_LEVEL, null, "");
				return;
			}
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_CREATE_CONFIRM_POPUP_TITLE, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_CREATE_CONFIRM_POPUP_BODY, this.m_GuildName), NKMCommonConst.Guild.Creation.ReqMiscItems[0].ItemId, (int)NKMCommonConst.Guild.Creation.ReqMiscItems[0].Count, new NKCPopupResourceConfirmBox.OnButton(this.OnCreateConfirm), null, false);
		}

		// Token: 0x060083C4 RID: 33732 RVA: 0x002C6672 File Offset: 0x002C4872
		private void OnCreateConfirm()
		{
			NKCPacketSender.Send_NKMPacket_GUILD_CREATE_REQ(this.m_inputName.text, this.m_JoinType, this.m_BadgeId, this.m_inputDesc.text);
		}

		// Token: 0x060083C5 RID: 33733 RVA: 0x002C669B File Offset: 0x002C489B
		public override void OnGuildDataChanged()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
		}

		// Token: 0x04006FD4 RID: 28628
		private const string BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006FD5 RID: 28629
		private const string ASSET_NAME = "NKM_UI_CONSORTIUM_FOUNDATION";

		// Token: 0x04006FD6 RID: 28630
		private static NKCUIGuildCreate m_Instance;

		// Token: 0x04006FD7 RID: 28631
		public InputField m_inputName;

		// Token: 0x04006FD8 RID: 28632
		public Image m_imgNameValid;

		// Token: 0x04006FD9 RID: 28633
		public Text m_lbNameValid;

		// Token: 0x04006FDA RID: 28634
		public InputField m_inputDesc;

		// Token: 0x04006FDB RID: 28635
		public NKCUIComToggle m_tglJoinTypeDirect;

		// Token: 0x04006FDC RID: 28636
		public NKCUIComToggle m_tglJoinTypeApproval;

		// Token: 0x04006FDD RID: 28637
		public NKCUIComToggle m_tglJoinTypeClosed;

		// Token: 0x04006FDE RID: 28638
		public Text m_lbJoinTypeDesc;

		// Token: 0x04006FDF RID: 28639
		public NKCUIGuildBadge m_badgeUI;

		// Token: 0x04006FE0 RID: 28640
		public NKCUIComStateButton m_btnBadgeSetting;

		// Token: 0x04006FE1 RID: 28641
		public NKCUIComStateButton m_btnBadgeRandom;

		// Token: 0x04006FE2 RID: 28642
		public Text m_lbGuildName;

		// Token: 0x04006FE3 RID: 28643
		public Text m_lbDesc;

		// Token: 0x04006FE4 RID: 28644
		public NKCUIComStateButton m_btnCreate;

		// Token: 0x04006FE5 RID: 28645
		public Text m_lbBtn;

		// Token: 0x04006FE6 RID: 28646
		public Text m_lbCost;

		// Token: 0x04006FE7 RID: 28647
		public Image m_imgCostItem;

		// Token: 0x04006FE8 RID: 28648
		private string m_GuildName = string.Empty;

		// Token: 0x04006FE9 RID: 28649
		private GuildJoinType m_JoinType;

		// Token: 0x04006FEA RID: 28650
		private long m_BadgeId;

		// Token: 0x04006FEB RID: 28651
		private bool m_bValidName;

		// Token: 0x04006FEC RID: 28652
		private string NORMAL_TEXT_COLOR_TEXT = "#582817";

		// Token: 0x04006FED RID: 28653
		private string DISABLE_TEXT_COLOR_TEXT = "#212122";
	}
}
