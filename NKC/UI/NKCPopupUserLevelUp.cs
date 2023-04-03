using System;
using System.Collections;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A90 RID: 2704
	public class NKCPopupUserLevelUp : NKCUIBase
	{
		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060077B4 RID: 30644 RVA: 0x0027CAE0 File Offset: 0x0027ACE0
		public static NKCPopupUserLevelUp instance
		{
			get
			{
				if (NKCPopupUserLevelUp.m_Instance == null)
				{
					NKCPopupUserLevelUp.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupUserLevelUp>("ab_ui_nkm_ui_levelup", "NKM_UI_LEVELUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupUserLevelUp.CleanupInstance)).GetInstance<NKCPopupUserLevelUp>();
				}
				return NKCPopupUserLevelUp.m_Instance;
			}
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x0027CB1A File Offset: 0x0027AD1A
		private static void CleanupInstance()
		{
			NKCPopupUserLevelUp.m_Instance = null;
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x060077B6 RID: 30646 RVA: 0x0027CB22 File Offset: 0x0027AD22
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x060077B7 RID: 30647 RVA: 0x0027CB25 File Offset: 0x0027AD25
		public override string MenuName
		{
			get
			{
				return "LevelUp";
			}
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x0027CB2C File Offset: 0x0027AD2C
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			base.Close();
			if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
			{
				Action action = this.dOnClose;
				if (action != null)
				{
					action();
				}
				this.dOnClose = null;
				return;
			}
			NKCContentManager.ShowContentUnlockPopup(delegate
			{
				Action action2 = this.dOnClose;
				if (action2 != null)
				{
					action2();
				}
				this.dOnClose = null;
			}, new STAGE_UNLOCK_REQ_TYPE[]
			{
				STAGE_UNLOCK_REQ_TYPE.SURT_PLAYER_LEVEL
			});
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x0027CB91 File Offset: 0x0027AD91
		public void Open(NKMUserData userData, Action onClose)
		{
			if (userData != null)
			{
				this.Open(userData.UserLevel - 1, userData.UserLevel, onClose);
			}
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x0027CBAC File Offset: 0x0027ADAC
		public void Open(int curLevel, int nextLevel, Action onClose)
		{
			this.m_fStartTime = 0f;
			this.m_lbCurLevel.text = curLevel.ToString();
			this.m_lbNextLevel.text = nextLevel.ToString();
			this.m_nextLevel = nextLevel;
			NKMUserExpTemplet nkmuserExpTemplet = NKMUserExpTemplet.Find(nextLevel);
			if (nkmuserExpTemplet != null)
			{
				this.m_lbDesc.text = NKCStringTable.GetString(nkmuserExpTemplet.m_strLevelUpDesc, false);
			}
			else
			{
				this.m_lbDesc.text = string.Empty;
			}
			this.dOnClose = onClose;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.StartCoroutine(this.WaitAnimator());
			base.UIOpened(true);
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x0027CC49 File Offset: 0x0027AE49
		private IEnumerator WaitAnimator()
		{
			while (this.m_ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			base.Close();
			yield break;
		}

		// Token: 0x060077BC RID: 30652 RVA: 0x0027CC58 File Offset: 0x0027AE58
		private void Update()
		{
			this.m_fStartTime += Time.deltaTime;
			if (this.m_fStartTime > 5.05f)
			{
				base.StopAllCoroutines();
				base.Close();
			}
		}

		// Token: 0x04006451 RID: 25681
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_levelup";

		// Token: 0x04006452 RID: 25682
		private const string UI_ASSET_NAME = "NKM_UI_LEVELUP";

		// Token: 0x04006453 RID: 25683
		private static NKCPopupUserLevelUp m_Instance;

		// Token: 0x04006454 RID: 25684
		public Animator m_ani;

		// Token: 0x04006455 RID: 25685
		public Text m_lbCurLevel;

		// Token: 0x04006456 RID: 25686
		public Text m_lbNextLevel;

		// Token: 0x04006457 RID: 25687
		public Text m_lbDesc;

		// Token: 0x04006458 RID: 25688
		private Action dOnClose;

		// Token: 0x04006459 RID: 25689
		private const int m_iTotalAniFrame = 303;

		// Token: 0x0400645A RID: 25690
		private const float m_fTotalAniTime = 5.05f;

		// Token: 0x0400645B RID: 25691
		private float m_fStartTime;

		// Token: 0x0400645C RID: 25692
		private int m_nextLevel;
	}
}
