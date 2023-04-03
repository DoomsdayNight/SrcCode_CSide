using System;
using System.Linq;
using System.Text;
using ClientPacket.Guild;
using Cs.Core.Util;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B53 RID: 2899
	public class NKCUIGuildLobbyManage : MonoBehaviour
	{
		// Token: 0x0600842C RID: 33836 RVA: 0x002C8B2C File Offset: 0x002C6D2C
		public void InitUI()
		{
			this.m_tglJoinTypeDirect.OnValueChanged.RemoveAllListeners();
			this.m_tglJoinTypeDirect.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelectDirect));
			this.m_tglJoinTypeApproval.OnValueChanged.RemoveAllListeners();
			this.m_tglJoinTypeApproval.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelectApproval));
			this.m_tglJoinTypeClosed.OnValueChanged.RemoveAllListeners();
			this.m_tglJoinTypeClosed.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelectClosed));
			this.m_inputDesc.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			this.m_inputDesc.onEndEdit.RemoveAllListeners();
			this.m_inputDesc.onEndEdit.AddListener(new UnityAction<string>(this.OnGreetingChanged));
			this.m_inputDesc.characterLimit = 40;
			NKCUIGuildBadge badgeUI = this.m_badgeUI;
			if (badgeUI != null)
			{
				badgeUI.InitUI();
			}
			this.m_btnBadgeSetting.PointerClick.RemoveAllListeners();
			this.m_btnBadgeSetting.PointerClick.AddListener(new UnityAction(this.OnClickBadgeSetting));
			this.m_btnBadgeRandom.PointerClick.RemoveAllListeners();
			this.m_btnBadgeRandom.PointerClick.AddListener(new UnityAction(this.OnClickBadgeRandom));
			this.m_btnGuildClose.PointerClick.RemoveAllListeners();
			this.m_btnGuildClose.PointerClick.AddListener(new UnityAction(this.OnClickGuildClose));
			this.m_btnGuildCloseCancel.PointerClick.RemoveAllListeners();
			this.m_btnGuildCloseCancel.PointerClick.AddListener(new UnityAction(this.OnClickGuildCloseCancel));
			this.m_btnOk.PointerClick.RemoveAllListeners();
			this.m_btnOk.PointerClick.AddListener(new UnityAction(this.OnClickOk));
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(this.OnClickCancel));
		}

		// Token: 0x0600842D RID: 33837 RVA: 0x002C8D28 File Offset: 0x002C6F28
		public void SetData(NKCUIGuildLobbyManage.OnClose onClose)
		{
			this.m_dOnClose = onClose;
			this.m_fDeltaTime = 0f;
			this.m_BadgeId = NKCGuildManager.MyGuildData.badgeId;
			this.m_JoinType = NKCGuildManager.MyGuildData.guildJoinType;
			this.m_Greeting = NKCGuildManager.MyGuildData.greeting;
			NKCUtil.SetLabelText(this.m_lbGuildName, NKCGuildManager.MyGuildData.name);
			this.m_badgeUI.SetData(this.m_BadgeId);
			this.m_tglJoinTypeDirect.Select(this.m_JoinType == GuildJoinType.DirectJoin, true, true);
			this.m_tglJoinTypeApproval.Select(this.m_JoinType == GuildJoinType.NeedApproval, true, true);
			this.m_tglJoinTypeClosed.Select(this.m_JoinType == GuildJoinType.Closed, true, true);
			NKCUtil.SetGameobjectActive(this.m_btnGuildClose, NKCGuildManager.MyGuildData.guildState != GuildState.Closing);
			NKCUtil.SetGameobjectActive(this.m_btnGuildCloseCancel, NKCGuildManager.MyGuildData.guildState == GuildState.Closing);
			NKCUtil.SetGameobjectActive(this.m_objGuildBreakupTime, NKCGuildManager.MyGuildData.guildState == GuildState.Closing);
			NKCUtil.SetLabelText(this.m_inputDesc.textComponent, this.m_Greeting);
			this.m_inputDesc.text = this.m_Greeting;
			this.m_tExpireTime = NKCGuildManager.MyGuildData.closingTime;
			this.SetButton();
			if (this.m_objGuildBreakupTime.activeSelf)
			{
				this.SetRemainTime();
			}
		}

		// Token: 0x0600842E RID: 33838 RVA: 0x002C8E80 File Offset: 0x002C7080
		private void SetRemainTime()
		{
			if (NKCGuildManager.MyGuildData != null)
			{
				if (NKCGuildManager.MyGuildData.guildState == GuildState.Closing)
				{
					TimeSpan timeSpan = this.m_tExpireTime - ServiceTime.Recent;
					if (timeSpan.TotalSeconds > 1.0)
					{
						NKCUtil.SetLabelText(this.m_lbGuildBreakupTime, string.Format(NKCUtilString.GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM_CLOSE, NKCUtilString.GetRemainTimeString(timeSpan, 2, true)));
						return;
					}
					NKCUtil.SetLabelText(this.m_lbGuildBreakupTime, NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_END_SOON", false));
					return;
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objGuildBreakupTime, false);
				}
			}
		}

		// Token: 0x0600842F RID: 33839 RVA: 0x002C8F05 File Offset: 0x002C7105
		private void OnSelectDirect(bool bValue)
		{
			if (bValue)
			{
				this.m_JoinType = GuildJoinType.DirectJoin;
				this.SetButton();
			}
		}

		// Token: 0x06008430 RID: 33840 RVA: 0x002C8F17 File Offset: 0x002C7117
		private void OnSelectApproval(bool bValue)
		{
			if (bValue)
			{
				this.m_JoinType = GuildJoinType.NeedApproval;
				this.SetButton();
			}
		}

		// Token: 0x06008431 RID: 33841 RVA: 0x002C8F29 File Offset: 0x002C7129
		private void OnSelectClosed(bool bValue)
		{
			if (bValue)
			{
				this.m_JoinType = GuildJoinType.Closed;
				this.SetButton();
			}
		}

		// Token: 0x06008432 RID: 33842 RVA: 0x002C8F3C File Offset: 0x002C713C
		private void SetButton()
		{
			if (this.m_JoinType != GuildJoinType.NeedApproval && NKCGuildManager.MyGuildData.joinWaitingList.Count > 0)
			{
				this.m_btnOk.Lock(false);
				NKCUtil.SetGameobjectActive(this.m_objJoinTypeError, true);
				return;
			}
			this.m_btnOk.UnLock(false);
			NKCUtil.SetGameobjectActive(this.m_objJoinTypeError, false);
		}

		// Token: 0x06008433 RID: 33843 RVA: 0x002C8F95 File Offset: 0x002C7195
		private void OnGreetingChanged(string str)
		{
			if (string.Equals(str, NKCGuildManager.MyGuildData.greeting))
			{
				return;
			}
			this.m_Greeting = NKCFilterManager.CheckBadChat(str);
			this.m_inputDesc.text = this.m_Greeting;
		}

		// Token: 0x06008434 RID: 33844 RVA: 0x002C8FC8 File Offset: 0x002C71C8
		private void OnClickBadgeRandom()
		{
			int frameId = UnityEngine.Random.Range(1, NKMTempletContainer<NKMGuildBadgeFrameTemplet>.Values.Count<NKMGuildBadgeFrameTemplet>());
			int frameColorId = UnityEngine.Random.Range(1, NKMTempletContainer<NKMGuildBadgeColorTemplet>.Values.Count<NKMGuildBadgeColorTemplet>());
			int markId = UnityEngine.Random.Range(1, NKMTempletContainer<NKMGuildBadgeMarkTemplet>.Values.Count<NKMGuildBadgeMarkTemplet>());
			int markColorId = UnityEngine.Random.Range(1, NKMTempletContainer<NKMGuildBadgeColorTemplet>.Values.Count<NKMGuildBadgeColorTemplet>());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(frameId.ToString("D3"));
			stringBuilder.Append(frameColorId.ToString("D3"));
			stringBuilder.Append(markId.ToString("D3"));
			stringBuilder.Append(markColorId.ToString("D3"));
			this.m_BadgeId = long.Parse(stringBuilder.ToString());
			this.m_badgeUI.SetData(frameId, frameColorId, markId, markColorId);
		}

		// Token: 0x06008435 RID: 33845 RVA: 0x002C9091 File Offset: 0x002C7291
		private void OnClickBadgeSetting()
		{
			if (!NKCPopupGuildBadgeSetting.IsInstanceOpen)
			{
				NKCPopupGuildBadgeSetting.Instance.Open(new NKCPopupGuildBadgeSetting.OnClose(this.OnCloseSetting), this.m_BadgeId);
			}
		}

		// Token: 0x06008436 RID: 33846 RVA: 0x002C90B6 File Offset: 0x002C72B6
		private void OnCloseSetting(long badgeId)
		{
			this.m_BadgeId = badgeId;
			this.m_badgeUI.SetData(this.m_BadgeId);
		}

		// Token: 0x06008437 RID: 33847 RVA: 0x002C90D0 File Offset: 0x002C72D0
		private void OnClickGuildClose()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_POPUP_BODY_DESC, delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_CLOSE_REQ(NKCGuildManager.MyData.guildUid);
			}, null, false);
		}

		// Token: 0x06008438 RID: 33848 RVA: 0x002C9102 File Offset: 0x002C7302
		private void OnClickGuildCloseCancel()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_BODY_DESC, delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_CLOSE_CANCEL_REQ(NKCGuildManager.MyData.guildUid);
			}, null, false);
		}

		// Token: 0x06008439 RID: 33849 RVA: 0x002C9134 File Offset: 0x002C7334
		private void OnClickOk()
		{
			if (this.m_JoinType != NKCGuildManager.MyGuildData.guildJoinType || this.m_BadgeId != NKCGuildManager.MyGuildData.badgeId || !string.Equals(this.m_Greeting, NKCGuildManager.MyGuildData.greeting))
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DATA_SAVE_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DATA_SAVE_POPUP_BODY_DESC, new NKCPopupOKCancel.OnButton(this.OnCinformOK), null, false);
				return;
			}
			this.Close();
		}

		// Token: 0x0600843A RID: 33850 RVA: 0x002C91A0 File Offset: 0x002C73A0
		private void OnCinformOK()
		{
			NKCPacketSender.Send_NKMPacket_GUILD_UPDATE_DATA_REQ(NKCGuildManager.MyData.guildUid, this.m_BadgeId, this.m_inputDesc.text, this.m_JoinType);
			this.Close();
		}

		// Token: 0x0600843B RID: 33851 RVA: 0x002C91D0 File Offset: 0x002C73D0
		private void OnClickCancel()
		{
			if (this.m_JoinType != NKCGuildManager.MyGuildData.guildJoinType || this.m_BadgeId != NKCGuildManager.MyGuildData.badgeId || !string.Equals(this.m_Greeting, NKCGuildManager.MyGuildData.greeting))
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DATA_SAVE_CANCEL_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DATA_SAVE_CANCEL_POPUP_BODY_DESC, new NKCPopupOKCancel.OnButton(this.Close), null, false);
				return;
			}
			this.Close();
		}

		// Token: 0x0600843C RID: 33852 RVA: 0x002C923C File Offset: 0x002C743C
		private void Close()
		{
			NKCUIGuildLobbyManage.OnClose dOnClose = this.m_dOnClose;
			if (dOnClose == null)
			{
				return;
			}
			dOnClose(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info, false);
		}

		// Token: 0x0600843D RID: 33853 RVA: 0x002C9250 File Offset: 0x002C7450
		private void Update()
		{
			if (this.m_objGuildBreakupTime.activeSelf)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetRemainTime();
				}
			}
		}

		// Token: 0x0400704F RID: 28751
		public Text m_lbGuildName;

		// Token: 0x04007050 RID: 28752
		public InputField m_inputDesc;

		// Token: 0x04007051 RID: 28753
		public NKCUIComToggle m_tglJoinTypeDirect;

		// Token: 0x04007052 RID: 28754
		public NKCUIComToggle m_tglJoinTypeApproval;

		// Token: 0x04007053 RID: 28755
		public NKCUIComToggle m_tglJoinTypeClosed;

		// Token: 0x04007054 RID: 28756
		public GameObject m_objJoinTypeError;

		// Token: 0x04007055 RID: 28757
		public NKCUIGuildBadge m_badgeUI;

		// Token: 0x04007056 RID: 28758
		public NKCUIComStateButton m_btnBadgeSetting;

		// Token: 0x04007057 RID: 28759
		public NKCUIComStateButton m_btnBadgeRandom;

		// Token: 0x04007058 RID: 28760
		public NKCUIComStateButton m_btnGuildClose;

		// Token: 0x04007059 RID: 28761
		public NKCUIComStateButton m_btnGuildCloseCancel;

		// Token: 0x0400705A RID: 28762
		public GameObject m_objGuildBreakupTime;

		// Token: 0x0400705B RID: 28763
		public Text m_lbGuildBreakupTime;

		// Token: 0x0400705C RID: 28764
		public NKCUIComStateButton m_btnOk;

		// Token: 0x0400705D RID: 28765
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x0400705E RID: 28766
		private NKCUIGuildLobbyManage.OnClose m_dOnClose;

		// Token: 0x0400705F RID: 28767
		private long m_BadgeId;

		// Token: 0x04007060 RID: 28768
		private GuildJoinType m_JoinType;

		// Token: 0x04007061 RID: 28769
		private string m_Greeting;

		// Token: 0x04007062 RID: 28770
		private DateTime m_tExpireTime;

		// Token: 0x04007063 RID: 28771
		private float m_fDeltaTime;

		// Token: 0x020018EA RID: 6378
		// (Invoke) Token: 0x0600B72A RID: 46890
		public delegate void OnClose(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE uiType, bool bForce = false);
	}
}
