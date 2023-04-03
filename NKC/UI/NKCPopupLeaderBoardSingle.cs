using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000958 RID: 2392
	public class NKCPopupLeaderBoardSingle : NKCUILeaderBoard
	{
		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06005F78 RID: 24440 RVA: 0x001DABAC File Offset: 0x001D8DAC
		public new static NKCPopupLeaderBoardSingle Instance
		{
			get
			{
				if (NKCPopupLeaderBoardSingle.m_Instance == null)
				{
					NKCPopupLeaderBoardSingle.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupLeaderBoardSingle>("ab_ui_nkm_ui_leader_board", "NKM_UI_POPUP_LEADER_BOARD", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupLeaderBoardSingle.CleanupInstance)).GetInstance<NKCPopupLeaderBoardSingle>();
					NKCPopupLeaderBoardSingle.m_Instance.InitUI();
				}
				return NKCPopupLeaderBoardSingle.m_Instance;
			}
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x001DABFB File Offset: 0x001D8DFB
		private static void CleanupInstance()
		{
			NKCPopupLeaderBoardSingle.m_Instance = null;
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06005F7A RID: 24442 RVA: 0x001DAC03 File Offset: 0x001D8E03
		public new static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupLeaderBoardSingle.m_Instance != null && NKCPopupLeaderBoardSingle.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x001DAC1E File Offset: 0x001D8E1E
		private void OnDestroy()
		{
			NKCPopupLeaderBoardSingle.m_Instance = null;
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06005F7C RID: 24444 RVA: 0x001DAC26 File Offset: 0x001D8E26
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06005F7D RID: 24445 RVA: 0x001DAC29 File Offset: 0x001D8E29
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Invalid;
			}
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x001DAC2C File Offset: 0x001D8E2C
		private void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			base.Init();
		}

		// Token: 0x06005F7F RID: 24447 RVA: 0x001DAC60 File Offset: 0x001D8E60
		public void OpenSingle(NKMLeaderBoardTemplet reservedTemplet)
		{
			bool flag = !string.IsNullOrEmpty(reservedTemplet.m_BoardPopupImg);
			NKCUtil.SetGameobjectActive(this.m_imgSingleBanner, flag);
			if (flag)
			{
				NKCUtil.SetImageSprite(this.m_imgSingleBanner, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_TEXTURE", reservedTemplet.m_BoardPopupImg, false), false);
			}
			bool flag2 = !string.IsNullOrEmpty(reservedTemplet.GetPopupTitle());
			NKCUtil.SetGameobjectActive(this.m_lbSingleTitle, flag2);
			if (flag2)
			{
				NKCUtil.SetLabelText(this.m_lbSingleTitle, reservedTemplet.GetPopupTitle());
			}
			bool flag3 = !string.IsNullOrEmpty(reservedTemplet.GetPopupName());
			NKCUtil.SetGameobjectActive(this.m_lbSingleSubTitle, flag3);
			if (flag3)
			{
				NKCUtil.SetLabelText(this.m_lbSingleSubTitle, reservedTemplet.GetPopupName());
			}
			bool flag4 = !string.IsNullOrEmpty(reservedTemplet.GetPopupDesc());
			NKCUtil.SetGameobjectActive(this.m_lbSingleDesc, flag4);
			if (flag4)
			{
				NKCUtil.SetLabelText(this.m_lbSingleDesc, reservedTemplet.GetPopupDesc());
			}
			base.Open(reservedTemplet, true);
		}

		// Token: 0x04004B8D RID: 19341
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_leader_board";

		// Token: 0x04004B8E RID: 19342
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_LEADER_BOARD";

		// Token: 0x04004B8F RID: 19343
		private static NKCPopupLeaderBoardSingle m_Instance;

		// Token: 0x04004B90 RID: 19344
		[Header("팝업 전용 좌측메뉴")]
		public Image m_imgSingleBanner;

		// Token: 0x04004B91 RID: 19345
		public Text m_lbSingleTitle;

		// Token: 0x04004B92 RID: 19346
		public Text m_lbSingleSubTitle;

		// Token: 0x04004B93 RID: 19347
		public Text m_lbSingleDesc;

		// Token: 0x04004B94 RID: 19348
		public NKCUIComStateButton m_btnClose;
	}
}
