using System;
using ClientPacket.Common;
using ClientPacket.Guild;
using NKC.Publisher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200091F RID: 2335
	public class NKCPopupChatSlotText : MonoBehaviour
	{
		// Token: 0x06005D6E RID: 23918 RVA: 0x001CD050 File Offset: 0x001CB250
		public void SetData(long channelUid, NKMChatMessageData data, bool disableTranslate = false)
		{
			this.m_ChannelUid = channelUid;
			this.m_NKMChatMessageData = data;
			string translatedMessage = NKCChatManager.GetTranslatedMessage(this.m_NKMChatMessageData.messageUid);
			if (this.m_btnReport != null)
			{
				this.m_btnReport.PointerClick.RemoveAllListeners();
				this.m_btnReport.PointerClick.AddListener(new UnityAction(this.OnClickReport));
				NKCUtil.SetGameobjectActive(this.m_btnReport, data.commonProfile.userUid != NKCScenManager.CurrentUserData().m_UserUID);
			}
			if (NKCPublisherModule.Localization.UseTranslation && !disableTranslate)
			{
				if (this.m_btnTranslate != null)
				{
					this.m_btnTranslate.PointerClick.RemoveAllListeners();
					this.m_btnTranslate.PointerClick.AddListener(new UnityAction(this.OnClickTranslate));
					NKCUtil.SetGameobjectActive(this.m_btnTranslate, string.IsNullOrEmpty(translatedMessage));
					if (this.m_LayoutGroup != null)
					{
						if (this.m_btnTranslate.gameObject.activeSelf)
						{
							this.m_LayoutGroup.padding.left = this.TRANSLATE_LEFT_PADDING;
						}
						else
						{
							this.m_LayoutGroup.padding.left = this.DEFAULT_LEFT_PADDING;
						}
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_btnTranslate, false);
				if (this.m_LayoutGroup != null)
				{
					this.m_LayoutGroup.padding.left = this.DEFAULT_LEFT_PADDING;
				}
			}
			NKMGuildData myGuildData = NKCGuildManager.MyGuildData;
			NKMGuildMemberData nkmguildMemberData = (myGuildData != null) ? myGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == data.commonProfile.userUid) : null;
			if (NKCGuildManager.HasGuild() && NKCGuildManager.MyData.guildUid == channelUid && nkmguildMemberData != null)
			{
				switch (nkmguildMemberData.grade)
				{
				case GuildMemberGrade.Master:
					NKCUtil.SetGameobjectActive(this.m_imgLeader, true);
					NKCUtil.SetImageSprite(this.m_imgLeader, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_consortium_sprite", "AB_UI_NKM_UI_CONSORTIUM_ICON_LEADER", false), false);
					NKCUtil.SetImageColor(this.m_imgBubble, NKCUtil.GetColor(this.GUILD_CHAT_LEADER_BG_COLOR));
					break;
				case GuildMemberGrade.Staff:
					NKCUtil.SetGameobjectActive(this.m_imgLeader, true);
					NKCUtil.SetImageSprite(this.m_imgLeader, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_consortium_sprite", "AB_UI_NKM_UI_CONSORTIUM_ICON_OFFICER", false), false);
					if (data.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID)
					{
						NKCUtil.SetImageColor(this.m_imgBubble, NKCUtil.GetColor(this.GUILD_CHAT_MY_BG_COLOR));
					}
					else
					{
						NKCUtil.SetImageColor(this.m_imgBubble, NKCUtil.GetColor(this.GUILD_CHAT_OTHER_BG_COLOR));
					}
					break;
				case GuildMemberGrade.Member:
					NKCUtil.SetGameobjectActive(this.m_imgLeader, false);
					if (data.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID)
					{
						NKCUtil.SetImageColor(this.m_imgBubble, NKCUtil.GetColor(this.GUILD_CHAT_MY_BG_COLOR));
					}
					else
					{
						NKCUtil.SetImageColor(this.m_imgBubble, NKCUtil.GetColor(this.GUILD_CHAT_OTHER_BG_COLOR));
					}
					break;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgLeader, false);
				if (data.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID)
				{
					NKCUtil.SetImageColor(this.m_imgBubble, NKCUtil.GetColor(this.PRIVATE_CHAT_MY_BG_COLOR));
				}
				else
				{
					NKCUtil.SetImageColor(this.m_imgBubble, NKCUtil.GetColor(this.PRIVATE_CHAT_TARGET_BG_COLOR));
				}
			}
			NKCUtil.SetLabelText(this.m_lbName, data.commonProfile.nickname);
			this.m_slot.SetProfiledata(data.commonProfile, null);
			if (data.blocked)
			{
				NKCUtil.SetLabelText(this.m_lbMessage, NKCUtilString.GET_STRING_CONSORTIUM_CHAT_ACCUMELATED_RECEIPT_REPORT_TEXT);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbMessage, data.message);
			}
			NKCUtil.SetGameobjectActive(this.m_objTranslateProgress, false);
			if (!NKCPublisherModule.Localization.UseTranslation || string.IsNullOrEmpty(translatedMessage))
			{
				NKCUtil.SetGameobjectActive(this.m_objTranslateLine, false);
				NKCUtil.SetGameobjectActive(this.m_lbTranslated, false);
				NKCUtil.SetGameobjectActive(this.m_objTranslateComplete, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objTranslateLine, true);
				NKCUtil.SetGameobjectActive(this.m_lbTranslated, true);
				NKCUtil.SetGameobjectActive(this.m_objTranslateComplete, true);
				NKCUtil.SetLabelText(this.m_lbTranslated, translatedMessage);
			}
			DateTime systemLocalTime = NKCSynchronizedTime.GetSystemLocalTime(data.createdAt, NKMTime.INTERVAL_FROM_UTC);
			NKCUtil.SetLabelText(this.m_lbTime, systemLocalTime.ToString());
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x001CD4A8 File Offset: 0x001CB6A8
		private void OnClickReport()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_CHAT_REPORT_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_CHAT_REPORT_POPUP_BODY_DESC, this.m_NKMChatMessageData.commonProfile.nickname), new NKCPopupOKCancel.OnButton(this.OnConfirmReport), null, false);
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x001CD4DC File Offset: 0x001CB6DC
		private void OnConfirmReport()
		{
			NKCPacketSender.Send_NKMPacket_GUILD_CHAT_COMPLAIN_REQ(this.m_ChannelUid, this.m_NKMChatMessageData.messageUid);
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x001CD4F4 File Offset: 0x001CB6F4
		private void OnClickTranslate()
		{
			NKCUtil.SetGameobjectActive(this.m_objTranslateLine, true);
			NKCUtil.SetGameobjectActive(this.m_objTranslateProgress, true);
			NKCUtil.SetGameobjectActive(this.m_btnTranslate, false);
			if (this.m_LayoutGroup != null)
			{
				this.m_LayoutGroup.padding.left = this.DEFAULT_LEFT_PADDING;
			}
			NKCPublisherModule.Localization.Translate(this.m_NKMChatMessageData.messageUid, this.m_NKMChatMessageData.message, NKCPublisherModule.Localization.GetDefaultLanguage(), new NKCPublisherModule.NKCPMLocalization.TranslateCallback(NKCChatManager.OnRecv));
		}

		// Token: 0x04004999 RID: 18841
		public Text m_lbName;

		// Token: 0x0400499A RID: 18842
		public NKCUISlotProfile m_slot;

		// Token: 0x0400499B RID: 18843
		public Image m_imgLeader;

		// Token: 0x0400499C RID: 18844
		public Text m_lbMessage;

		// Token: 0x0400499D RID: 18845
		public NKCUIComStateButton m_btnReport;

		// Token: 0x0400499E RID: 18846
		public Text m_lbTime;

		// Token: 0x0400499F RID: 18847
		public Image m_imgBubble;

		// Token: 0x040049A0 RID: 18848
		[Header("번역 관련")]
		public VerticalLayoutGroup m_LayoutGroup;

		// Token: 0x040049A1 RID: 18849
		public NKCUIComStateButton m_btnTranslate;

		// Token: 0x040049A2 RID: 18850
		public GameObject m_objTranslateLine;

		// Token: 0x040049A3 RID: 18851
		public Text m_lbTranslated;

		// Token: 0x040049A4 RID: 18852
		public GameObject m_objTranslateProgress;

		// Token: 0x040049A5 RID: 18853
		public GameObject m_objTranslateComplete;

		// Token: 0x040049A6 RID: 18854
		[Header("채팅 배경 색")]
		public string PRIVATE_CHAT_MY_BG_COLOR = "#3394FF";

		// Token: 0x040049A7 RID: 18855
		public string PRIVATE_CHAT_TARGET_BG_COLOR = "#FFFFFF";

		// Token: 0x040049A8 RID: 18856
		public string GUILD_CHAT_LEADER_BG_COLOR = "#FFA21D";

		// Token: 0x040049A9 RID: 18857
		public string GUILD_CHAT_MY_BG_COLOR = "#3394FF";

		// Token: 0x040049AA RID: 18858
		public string GUILD_CHAT_OTHER_BG_COLOR = "#EAEFF3";

		// Token: 0x040049AB RID: 18859
		private long m_ChannelUid;

		// Token: 0x040049AC RID: 18860
		private NKMChatMessageData m_NKMChatMessageData;

		// Token: 0x040049AD RID: 18861
		private int DEFAULT_LEFT_PADDING = 30;

		// Token: 0x040049AE RID: 18862
		private int TRANSLATE_LEFT_PADDING = 121;
	}
}
