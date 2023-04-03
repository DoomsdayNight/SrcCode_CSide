using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKC.UI.Guild;

namespace NKC.UI
{
	// Token: 0x02000920 RID: 2336
	public class NKCPopupGuildChat : NKCUIBase
	{
		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x001CD5DC File Offset: 0x001CB7DC
		public static NKCPopupGuildChat Instance
		{
			get
			{
				if (NKCPopupGuildChat.m_Instance == null)
				{
					NKCPopupGuildChat.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildChat>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_CHAT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildChat.CleanupInstance)).GetInstance<NKCPopupGuildChat>();
					NKCPopupGuildChat.m_Instance.InitUI();
				}
				return NKCPopupGuildChat.m_Instance;
			}
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x001CD62B File Offset: 0x001CB82B
		private static void CleanupInstance()
		{
			NKCPopupGuildChat.m_Instance = null;
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x001CD633 File Offset: 0x001CB833
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildChat.m_Instance != null && NKCPopupGuildChat.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x001CD64E File Offset: 0x001CB84E
		private void OnDestroy()
		{
			NKCPopupGuildChat.m_Instance = null;
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x001CD656 File Offset: 0x001CB856
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06005D78 RID: 23928 RVA: 0x001CD659 File Offset: 0x001CB859
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06005D79 RID: 23929 RVA: 0x001CD660 File Offset: 0x001CB860
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x001CD66E File Offset: 0x001CB86E
		private void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_Chat.InitUI(new NKCUIComChat.OnSendMessage(this.OnSendMessage), new NKCUIComChat.OnClose(this.OnCloseChat), false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x001CD6AC File Offset: 0x001CB8AC
		private void OnSendMessage(long channelUid, ChatMessageType messageType, string message, int emotionId)
		{
			NKCPacketSender.Send_NKMPacket_GUILD_CHAT_REQ(channelUid, messageType, message, emotionId);
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x001CD6B8 File Offset: 0x001CB8B8
		private void OnCloseChat()
		{
			base.Close();
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x001CD6C0 File Offset: 0x001CB8C0
		public void Open(long defaultChannel = 0L)
		{
			this.m_CurChannelUid = defaultChannel;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (NKCGuildManager.HasGuild())
			{
				NKCUtil.SetGameobjectActive(this.m_GuildBadgeUI, true);
				this.m_GuildBadgeUI.SetData(NKCGuildManager.MyGuildData.badgeId);
				this.m_Chat.SetData(this.m_CurChannelUid, true, NKCGuildManager.MyGuildData.name);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_GuildBadgeUI, false);
				this.m_Chat.SetData(this.m_CurChannelUid, true, "");
			}
			base.UIOpened(true);
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x001CD750 File Offset: 0x001CB950
		public void AddMessage(NKMChatMessageData data)
		{
			this.m_Chat.AddMessage(data, true, false);
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x001CD760 File Offset: 0x001CB960
		public void RefreshList(bool bResetPosition = false)
		{
			this.m_Chat.RefreshList(bResetPosition);
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x001CD76E File Offset: 0x001CB96E
		public void OnChatDataReceived(long channelUid, List<NKMChatMessageData> lstData, bool bRefresh = false)
		{
			this.m_Chat.OnChatDataReceived(channelUid, lstData, bRefresh);
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x001CD77E File Offset: 0x001CB97E
		public void CheckMute()
		{
			this.m_Chat.CheckMute();
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x001CD78B File Offset: 0x001CB98B
		public override void OnGuildDataChanged()
		{
			base.OnGuildDataChanged();
			if (!NKCGuildManager.HasGuild())
			{
				base.Close();
			}
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x001CD7A0 File Offset: 0x001CB9A0
		public override void OnScreenResolutionChanged()
		{
			base.OnScreenResolutionChanged();
			this.m_Chat.OnScreenResolutionChanged();
		}

		// Token: 0x040049AF RID: 18863
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x040049B0 RID: 18864
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_CHAT";

		// Token: 0x040049B1 RID: 18865
		private static NKCPopupGuildChat m_Instance;

		// Token: 0x040049B2 RID: 18866
		public NKCUIComChat m_Chat;

		// Token: 0x040049B3 RID: 18867
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x040049B4 RID: 18868
		private long m_CurChannelUid;
	}
}
