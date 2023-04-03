using System;
using NKC.Templet;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C0A RID: 3082
	public class NKCUILobbyEventShortCut : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008EB7 RID: 36535 RVA: 0x00308BD4 File Offset: 0x00306DD4
		public void Init(NKCUILobbyEventShortCut.DotEnableConditionFunction conditionFunc, NKCUILobbyEventShortCut.OnButton onButton, NKCLobbyIconTemplet templet = null)
		{
			this.dDotEnableConditionFunction = conditionFunc;
			this.dOnButton = onButton;
			this.m_templet = templet;
			if (this.m_templet != null)
			{
				this.m_unlockInfo = new UnlockInfo(this.m_templet.m_UnlockReqType, this.m_templet.m_UnlockReqValue);
				if (!string.IsNullOrEmpty(this.m_templet.m_IconName))
				{
					NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
					NKCUtil.SetImageSprite(this.m_imgIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LOBBY_SPRITE", this.m_templet.m_IconName, false), false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
				}
				if (!string.IsNullOrEmpty(this.m_templet.m_Desc))
				{
					NKCUtil.SetGameobjectActive(this.m_lbDesc, true);
					NKCUtil.SetLabelText(this.m_lbDesc, this.m_templet.GetDesc());
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lbDesc, false);
				}
			}
			this.m_csbtnButton.PointerClick.RemoveAllListeners();
			this.m_csbtnButton.PointerClick.AddListener(new UnityAction(this.OnBtn));
		}

		// Token: 0x06008EB8 RID: 36536 RVA: 0x00308CE0 File Offset: 0x00306EE0
		protected override void UpdateLock()
		{
			this.m_bLocked = !NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), this.m_unlockInfo, false);
			NKCUtil.SetLabelText(this.m_lbLock, NKCContentManager.MakeUnlockConditionString(this.m_unlockInfo, true));
			NKCUtil.SetGameobjectActive(this.m_objLock, this.m_bLocked);
		}

		// Token: 0x06008EB9 RID: 36537 RVA: 0x00308D30 File Offset: 0x00306F30
		protected override void ContentsUpdate(NKMUserData userData)
		{
			bool flag = this.dDotEnableConditionFunction != null && this.dDotEnableConditionFunction(userData);
			this.SetNotify(flag);
			NKCUtil.SetGameobjectActive(this.m_objReddot, flag);
		}

		// Token: 0x06008EBA RID: 36538 RVA: 0x00308D68 File Offset: 0x00306F68
		private void OnBtn()
		{
			if (this.m_bLocked)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCContentManager.MakeUnlockConditionString(this.m_unlockInfo, false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (this.dOnButton != null)
			{
				this.dOnButton(this.m_templet);
			}
		}

		// Token: 0x04007BC7 RID: 31687
		public GameObject m_objReddot;

		// Token: 0x04007BC8 RID: 31688
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04007BC9 RID: 31689
		public Image m_imgIcon;

		// Token: 0x04007BCA RID: 31690
		public Text m_lbDesc;

		// Token: 0x04007BCB RID: 31691
		private NKCUILobbyEventShortCut.DotEnableConditionFunction dDotEnableConditionFunction;

		// Token: 0x04007BCC RID: 31692
		private NKCUILobbyEventShortCut.OnButton dOnButton;

		// Token: 0x04007BCD RID: 31693
		private UnlockInfo m_unlockInfo;

		// Token: 0x04007BCE RID: 31694
		private NKCLobbyIconTemplet m_templet;

		// Token: 0x020019D0 RID: 6608
		// (Invoke) Token: 0x0600BA37 RID: 47671
		public delegate bool DotEnableConditionFunction(NKMUserData userData);

		// Token: 0x020019D1 RID: 6609
		// (Invoke) Token: 0x0600BA3B RID: 47675
		public delegate void OnButton(NKCLobbyIconTemplet templet);
	}
}
