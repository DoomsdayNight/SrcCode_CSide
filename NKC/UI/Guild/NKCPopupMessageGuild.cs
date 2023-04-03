using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B4A RID: 2890
	public class NKCPopupMessageGuild : NKCUIBase
	{
		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06008390 RID: 33680 RVA: 0x002C5749 File Offset: 0x002C3949
		public static NKCPopupMessageGuild Instance
		{
			get
			{
				if (NKCPopupMessageGuild.m_Instance == null)
				{
					NKCPopupMessageGuild.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupMessageGuild>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_TOAST_POPUP", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupMessageGuild.CleanupInstance)).GetInstance<NKCPopupMessageGuild>();
				}
				return NKCPopupMessageGuild.m_Instance;
			}
		}

		// Token: 0x06008391 RID: 33681 RVA: 0x002C5783 File Offset: 0x002C3983
		private static void CleanupInstance()
		{
			NKCPopupMessageGuild.m_Instance = null;
		}

		// Token: 0x06008392 RID: 33682 RVA: 0x002C578B File Offset: 0x002C398B
		private void OnDestroy()
		{
			NKCPopupMessageGuild.m_Instance = null;
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06008393 RID: 33683 RVA: 0x002C5793 File Offset: 0x002C3993
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06008394 RID: 33684 RVA: 0x002C5796 File Offset: 0x002C3996
		public override string MenuName
		{
			get
			{
				return "Message";
			}
		}

		// Token: 0x06008395 RID: 33685 RVA: 0x002C57A0 File Offset: 0x002C39A0
		public void Open(string title, string message, bool bIsGoodNews)
		{
			GuildMessage guildMessage = new GuildMessage();
			guildMessage.title = title;
			guildMessage.desc = message;
			this.m_lstMessage.Enqueue(guildMessage);
			this.m_lstIsGoodNews.Enqueue(bIsGoodNews);
			if (!this.m_bPlaying)
			{
				base.gameObject.SetActive(true);
				base.UIOpened(true);
				base.StartCoroutine(this.Process());
				this.m_bPlaying = true;
			}
		}

		// Token: 0x06008396 RID: 33686 RVA: 0x002C5808 File Offset: 0x002C3A08
		private IEnumerator Process()
		{
			while (this.m_lstMessage.Count > 0)
			{
				GuildMessage guildMessage = this.m_lstMessage.Dequeue();
				bool bIsGoodNews = this.m_lstIsGoodNews.Dequeue();
				if (guildMessage != null)
				{
					NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
					this.m_lbTitle.text = guildMessage.title;
					this.m_lbMessage.text = guildMessage.desc;
					yield return base.StartCoroutine(this.ProcessShowMessage(bIsGoodNews));
				}
			}
			base.Close();
			yield break;
		}

		// Token: 0x06008397 RID: 33687 RVA: 0x002C5817 File Offset: 0x002C3A17
		private IEnumerator ProcessShowMessage(bool bIsGoodNews)
		{
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, true);
			if (bIsGoodNews)
			{
				this.m_Ani.Play("NKM_UI_POPUP_MESSAGE_EVENTBUFF_INTRO");
			}
			else
			{
				this.m_Ani.Play("NKM_UI_POPUP_MESSAGE_EVENTBUFF_OFF");
			}
			yield return new WaitForSeconds(3f);
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
			yield break;
		}

		// Token: 0x06008398 RID: 33688 RVA: 0x002C582D File Offset: 0x002C3A2D
		public override void CloseInternal()
		{
			this.m_bPlaying = false;
			this.m_lstMessage.Clear();
			this.m_lstIsGoodNews.Clear();
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x04006FBE RID: 28606
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006FBF RID: 28607
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_TOAST_POPUP";

		// Token: 0x04006FC0 RID: 28608
		private static NKCPopupMessageGuild m_Instance;

		// Token: 0x04006FC1 RID: 28609
		private const float MESSAGE_STAY_TIME = 3f;

		// Token: 0x04006FC2 RID: 28610
		public Animator m_Ani;

		// Token: 0x04006FC3 RID: 28611
		public RectTransform m_rtMessageRoot;

		// Token: 0x04006FC4 RID: 28612
		public Text m_lbTitle;

		// Token: 0x04006FC5 RID: 28613
		public Text m_lbMessage;

		// Token: 0x04006FC6 RID: 28614
		private Queue<GuildMessage> m_lstMessage = new Queue<GuildMessage>();

		// Token: 0x04006FC7 RID: 28615
		private Queue<bool> m_lstIsGoodNews = new Queue<bool>();

		// Token: 0x04006FC8 RID: 28616
		private bool m_bPlaying;
	}
}
