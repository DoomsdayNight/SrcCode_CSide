using System;
using ClientPacket.Common;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200091C RID: 2332
	public class NKCPopupChatSlot : MonoBehaviour
	{
		// Token: 0x06005D65 RID: 23909 RVA: 0x001CCB44 File Offset: 0x001CAD44
		public void SetData(long channelUid, NKMChatMessageData data, bool disableTranslate = false)
		{
			this.m_ChannelUid = channelUid;
			this.m_ChatMessageType = data.messageType;
			NKCUtil.SetGameobjectActive(this.m_MyTextChat, false);
			NKCUtil.SetGameobjectActive(this.m_UserTextChat, false);
			NKCUtil.SetGameobjectActive(this.m_MyEmoticonChat, false);
			NKCUtil.SetGameobjectActive(this.m_UserEmoticonChat, false);
			NKCUtil.SetGameobjectActive(this.m_SystemChat, false);
			NKCUtil.SetGameobjectActive(this.m_EventChat, false);
			switch (data.messageType)
			{
			case ChatMessageType.Normal:
				if (data.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID)
				{
					if (data.emotionId > 0)
					{
						NKCUtil.SetGameobjectActive(this.m_MyEmoticonChat, true);
						this.m_MyEmoticonChat.SetData(this.m_ChannelUid, data);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_MyTextChat, true);
						this.m_MyTextChat.SetData(this.m_ChannelUid, data, disableTranslate);
					}
					this.m_LayoutGroup.padding = new RectOffset(this.MY_CHAT_PADDING_LEFT, this.MY_CHAT_PADDING_RIGHT, this.MY_CHAT_PADDING_TOP, this.MY_CHAT_PADDING_BOTTOM);
					this.m_LayoutGroup.childAlignment = this.MY_CHAT_ANCHOR;
					return;
				}
				if (data.emotionId > 0)
				{
					NKCUtil.SetGameobjectActive(this.m_UserEmoticonChat, true);
					this.m_UserEmoticonChat.SetData(this.m_ChannelUid, data);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_UserTextChat, true);
					this.m_UserTextChat.SetData(this.m_ChannelUid, data, disableTranslate);
				}
				this.m_LayoutGroup.padding = new RectOffset(this.USER_CHAT_PADDING_LEFT, this.USER_CHAT_PADDING_RIGHT, this.USER_CHAT_PADDING_TOP, this.USER_CHAT_PADDING_BOTTOM);
				this.m_LayoutGroup.childAlignment = this.USER_CHAT_ANCHOR;
				return;
			case ChatMessageType.System:
			case ChatMessageType.SystemJoin:
			case ChatMessageType.SystemExit:
			case ChatMessageType.SystemBan:
			case ChatMessageType.SystemPromotion:
			case ChatMessageType.SystemMasterMigration:
				NKCUtil.SetGameobjectActive(this.m_SystemChat, true);
				this.m_SystemChat.SetData(data);
				this.m_LayoutGroup.padding = new RectOffset(this.SYSTEM_CHAT_PADDING_LEFT, this.SYSTEM_CHAT_PADDING_RIGHT, this.SYSTEM_CHAT_PADDING_TOP, this.SYSTEM_CHAT_PADDING_BOTTOM);
				this.m_LayoutGroup.childAlignment = this.SYSTEM_CHAT_ANCHOR;
				return;
			case ChatMessageType.SystemLevelUp:
				NKCUtil.SetGameobjectActive(this.m_EventChat, true);
				this.m_EventChat.SetData(data);
				this.m_LayoutGroup.padding = new RectOffset(this.LEVEL_UP_CHAT_PADDING_LEFT, this.LEVEL_UP_CHAT_PADDING_RIGHT, this.LEVEL_UP_CHAT_PADDING_TOP, this.LEVEL_UP_CHAT_PADDING_BOTTOM);
				this.m_LayoutGroup.childAlignment = this.LEVEL_UP_CHAT_ANCHOR;
				return;
			default:
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x001CCDAC File Offset: 0x001CAFAC
		public void PlaySDAni()
		{
			if (this.m_ChatMessageType != ChatMessageType.Normal)
			{
				return;
			}
			if (this.m_MyEmoticonChat.gameObject.activeSelf)
			{
				this.m_MyEmoticonChat.PlaySDAni();
				return;
			}
			if (this.m_UserEmoticonChat.gameObject.activeSelf)
			{
				this.m_UserEmoticonChat.PlaySDAni();
			}
		}

		// Token: 0x04004976 RID: 18806
		public NKCPopupChatSlotText m_MyTextChat;

		// Token: 0x04004977 RID: 18807
		public NKCPopupChatSlotText m_UserTextChat;

		// Token: 0x04004978 RID: 18808
		public NKCPopupChatSlotEmoticon m_MyEmoticonChat;

		// Token: 0x04004979 RID: 18809
		public NKCPopupChatSlotEmoticon m_UserEmoticonChat;

		// Token: 0x0400497A RID: 18810
		public NKCPopupChatSlotSystem m_SystemChat;

		// Token: 0x0400497B RID: 18811
		public NKCpopupChatSlotSpecial m_EventChat;

		// Token: 0x0400497C RID: 18812
		public LayoutGroup m_LayoutGroup;

		// Token: 0x0400497D RID: 18813
		[Header("내 채팅")]
		public int MY_CHAT_PADDING_LEFT;

		// Token: 0x0400497E RID: 18814
		public int MY_CHAT_PADDING_RIGHT = 155;

		// Token: 0x0400497F RID: 18815
		public int MY_CHAT_PADDING_TOP = 59;

		// Token: 0x04004980 RID: 18816
		public int MY_CHAT_PADDING_BOTTOM = 22;

		// Token: 0x04004981 RID: 18817
		public TextAnchor MY_CHAT_ANCHOR = TextAnchor.UpperRight;

		// Token: 0x04004982 RID: 18818
		[Header("상대방 채팅")]
		public int USER_CHAT_PADDING_LEFT = 155;

		// Token: 0x04004983 RID: 18819
		public int USER_CHAT_PADDING_RIGHT;

		// Token: 0x04004984 RID: 18820
		public int USER_CHAT_PADDING_TOP = 59;

		// Token: 0x04004985 RID: 18821
		public int USER_CHAT_PADDING_BOTTOM = 22;

		// Token: 0x04004986 RID: 18822
		public TextAnchor USER_CHAT_ANCHOR;

		// Token: 0x04004987 RID: 18823
		[Header("레벨업")]
		public int LEVEL_UP_CHAT_PADDING_LEFT;

		// Token: 0x04004988 RID: 18824
		public int LEVEL_UP_CHAT_PADDING_RIGHT;

		// Token: 0x04004989 RID: 18825
		public int LEVEL_UP_CHAT_PADDING_TOP = 26;

		// Token: 0x0400498A RID: 18826
		public int LEVEL_UP_CHAT_PADDING_BOTTOM = 26;

		// Token: 0x0400498B RID: 18827
		public TextAnchor LEVEL_UP_CHAT_ANCHOR = TextAnchor.UpperRight;

		// Token: 0x0400498C RID: 18828
		[Header("시스템 메세지")]
		public int SYSTEM_CHAT_PADDING_LEFT;

		// Token: 0x0400498D RID: 18829
		public int SYSTEM_CHAT_PADDING_RIGHT;

		// Token: 0x0400498E RID: 18830
		public int SYSTEM_CHAT_PADDING_TOP = 26;

		// Token: 0x0400498F RID: 18831
		public int SYSTEM_CHAT_PADDING_BOTTOM = 26;

		// Token: 0x04004990 RID: 18832
		public TextAnchor SYSTEM_CHAT_ANCHOR = TextAnchor.UpperRight;

		// Token: 0x04004991 RID: 18833
		private long m_ChannelUid;

		// Token: 0x04004992 RID: 18834
		private ChatMessageType m_ChatMessageType;
	}
}
