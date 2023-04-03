using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B43 RID: 2883
	public class NKCPopupGuildKick : NKCUIBase
	{
		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06008357 RID: 33623 RVA: 0x002C47A4 File Offset: 0x002C29A4
		public static NKCPopupGuildKick Instance
		{
			get
			{
				if (NKCPopupGuildKick.m_Instance == null)
				{
					NKCPopupGuildKick.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildKick>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_POPUP_KICK", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildKick.CleanupInstance)).GetInstance<NKCPopupGuildKick>();
					if (NKCPopupGuildKick.m_Instance != null)
					{
						NKCPopupGuildKick.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildKick.m_Instance;
			}
		}

		// Token: 0x06008358 RID: 33624 RVA: 0x002C4805 File Offset: 0x002C2A05
		private static void CleanupInstance()
		{
			NKCPopupGuildKick.m_Instance = null;
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06008359 RID: 33625 RVA: 0x002C480D File Offset: 0x002C2A0D
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildKick.m_Instance != null && NKCPopupGuildKick.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600835A RID: 33626 RVA: 0x002C4828 File Offset: 0x002C2A28
		private void OnDestroy()
		{
			NKCPopupGuildKick.m_Instance = null;
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x0600835B RID: 33627 RVA: 0x002C4830 File Offset: 0x002C2A30
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x0600835C RID: 33628 RVA: 0x002C4833 File Offset: 0x002C2A33
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600835D RID: 33629 RVA: 0x002C483A File Offset: 0x002C2A3A
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600835E RID: 33630 RVA: 0x002C4848 File Offset: 0x002C2A48
		private void InitUI()
		{
			for (int i = 0; i < this.m_lstTglBanReason.Count; i++)
			{
				this.m_lstTglBanReason[i].OnValueChanged.RemoveAllListeners();
				this.m_lstTglBanReason[i].OnValueChanged.AddListener(new UnityAction<bool>(this.OnSelectReason));
			}
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnOK.PointerClick.RemoveAllListeners();
			this.m_btnOK.PointerClick.AddListener(new UnityAction(this.OnClickOK));
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
		}

		// Token: 0x0600835F RID: 33631 RVA: 0x002C4928 File Offset: 0x002C2B28
		public void Open(string userName, NKCPopupGuildKick.OnClose onClose)
		{
			this.m_dOnClose = onClose;
			for (int i = 0; i < this.m_lstTglBanReason.Count; i++)
			{
				this.m_lstTglBanReason[i].Select(false, true, true);
			}
			this.m_selectedReason = 0;
			NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_FORCE_EXIT_CONFIRM_POPUP_TITLE_DESC);
			NKCUtil.SetLabelText(this.m_lbDesc, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_FORCE_EXIT_CONFIRM_POPUP_BODY_DESC, userName));
			NKCUtil.SetGameobjectActive(this.m_objOKDisabled, true);
			base.UIOpened(true);
		}

		// Token: 0x06008360 RID: 33632 RVA: 0x002C49A8 File Offset: 0x002C2BA8
		private void OnSelectReason(bool bSelect)
		{
			if (bSelect)
			{
				this.m_selectedReason = 0;
				for (int i = 0; i < this.m_lstTglBanReason.Count; i++)
				{
					if (this.m_lstTglBanReason[i].m_bChecked)
					{
						this.m_selectedReason = i + 1;
						break;
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objOKDisabled, this.m_selectedReason == 0);
		}

		// Token: 0x06008361 RID: 33633 RVA: 0x002C4A07 File Offset: 0x002C2C07
		private void OnClickOK()
		{
			if (this.m_selectedReason == 0)
			{
				return;
			}
			NKCPopupGuildKick.OnClose dOnClose = this.m_dOnClose;
			if (dOnClose == null)
			{
				return;
			}
			dOnClose(this.m_selectedReason);
		}

		// Token: 0x06008362 RID: 33634 RVA: 0x002C4A28 File Offset: 0x002C2C28
		private void OnClickOKDisabled()
		{
			NKCPopupMessageManager.AddPopupMessage("", NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x04006F80 RID: 28544
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006F81 RID: 28545
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_POPUP_KICK";

		// Token: 0x04006F82 RID: 28546
		private static NKCPopupGuildKick m_Instance;

		// Token: 0x04006F83 RID: 28547
		public Text m_lbTitle;

		// Token: 0x04006F84 RID: 28548
		public Text m_lbDesc;

		// Token: 0x04006F85 RID: 28549
		public List<NKCUIComToggle> m_lstTglBanReason;

		// Token: 0x04006F86 RID: 28550
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006F87 RID: 28551
		public NKCUIComStateButton m_btnOK;

		// Token: 0x04006F88 RID: 28552
		public GameObject m_objOKDisabled;

		// Token: 0x04006F89 RID: 28553
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x04006F8A RID: 28554
		private NKCPopupGuildKick.OnClose m_dOnClose;

		// Token: 0x04006F8B RID: 28555
		private int m_selectedReason;

		// Token: 0x020018DC RID: 6364
		// (Invoke) Token: 0x0600B6F5 RID: 46837
		public delegate void OnClose(int reason);
	}
}
