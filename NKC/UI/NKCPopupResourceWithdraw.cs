using System;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A7B RID: 2683
	public class NKCPopupResourceWithdraw : NKCUIBase
	{
		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x060076C9 RID: 30409 RVA: 0x0027894C File Offset: 0x00276B4C
		public static NKCPopupResourceWithdraw Instance
		{
			get
			{
				if (NKCPopupResourceWithdraw.m_Instance == null)
				{
					NKCPopupResourceWithdraw.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupResourceWithdraw>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_RESOURCE_WITHDRAW", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupResourceWithdraw.CleanupInstance)).GetInstance<NKCPopupResourceWithdraw>();
					NKCPopupResourceWithdraw.m_Instance.Init();
				}
				return NKCPopupResourceWithdraw.m_Instance;
			}
		}

		// Token: 0x060076CA RID: 30410 RVA: 0x0027899B File Offset: 0x00276B9B
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupResourceWithdraw.m_Instance != null && NKCPopupResourceWithdraw.m_Instance.IsOpen)
			{
				NKCPopupResourceWithdraw.m_Instance.Close();
			}
		}

		// Token: 0x060076CB RID: 30411 RVA: 0x002789C0 File Offset: 0x00276BC0
		private static void CleanupInstance()
		{
			NKCPopupResourceWithdraw.m_Instance = null;
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x060076CC RID: 30412 RVA: 0x002789C8 File Offset: 0x00276BC8
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x060076CD RID: 30413 RVA: 0x002789CB File Offset: 0x00276BCB
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_RESOURCE_WITHDRAW;
			}
		}

		// Token: 0x060076CE RID: 30414 RVA: 0x002789D2 File Offset: 0x00276BD2
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x002789E0 File Offset: 0x00276BE0
		private void Init()
		{
			if (this.m_csbtnCancel != null)
			{
				this.m_csbtnCancel.PointerClick.RemoveAllListeners();
				this.m_csbtnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			}
			else
			{
				Debug.LogError("Cancel Button Not Found!");
			}
			if (this.m_csbtnClose != null)
			{
				this.m_csbtnClose.PointerClick.RemoveAllListeners();
				this.m_csbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			else
			{
				Debug.LogError("Close Button Not Found!");
			}
			if (this.m_csbtnOK != null)
			{
				this.m_csbtnOK.PointerClick.RemoveAllListeners();
				this.m_csbtnOK.PointerClick.AddListener(new UnityAction(this.OnBtnOK));
				NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			}
			else
			{
				Debug.LogError("OK Button Not Found!");
			}
			if (this.m_btnBG != null)
			{
				this.m_btnBG.PointerClick.RemoveAllListeners();
				this.m_btnBG.PointerClick.AddListener(new UnityAction(base.Close));
			}
		}

		// Token: 0x060076D0 RID: 30416 RVA: 0x00278B08 File Offset: 0x00276D08
		public void OpenForShopBuyAll(ShopTabTemplet tabTemplet, NKCPopupResourceWithdraw.OnOk onOK)
		{
			if (tabTemplet == null)
			{
				return;
			}
			this.dOnOk = onOK;
			NKCUIBase.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_SHOP_BUY_ALL_TITLE);
			NKCUIBase.SetLabelText(this.m_lbText, NKCUtilString.GET_STRING_SHOP_BUY_ALL_DESC);
			NKCUtil.SetGameobjectActive(this.m_objRegain, false);
			NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
			this.m_tagPrice.SetData(NKCShopManager.GetBundleItemPriceItemID(tabTemplet), NKCShopManager.GetBundleItemPrice(tabTemplet), false, false, true);
			NKCUtil.SetLabelText(this.m_lbOK, NKCUtilString.GET_STRING_SHOP_BUY_ALL_TITLE);
			base.UIOpened(true);
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x00278B8C File Offset: 0x00276D8C
		public void OpenForWorldmapBuildingRemove(NKMWorldMapBuildingTemplet.LevelTemplet targetBuildingTemplet, NKCPopupResourceWithdraw.OnOk onOK)
		{
			if (targetBuildingTemplet == null)
			{
				return;
			}
			this.dOnOk = onOK;
			NKCUIBase.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_WORLDMAP_BUILDING_REMOVE);
			NKCUIBase.SetLabelText(this.m_lbText, NKCUtilString.GET_STRING_WORLDMAP_BUILDING_REMOVE_DESC_TWO_PARAM, new object[]
			{
				targetBuildingTemplet.level,
				targetBuildingTemplet.GetName()
			});
			NKCUtil.SetGameobjectActive(this.m_objRegain, true);
			NKCUIBase.SetLabelText(this.m_lbRegainDesc, NKCUtilString.GET_STRING_WORLDMAP_BUILDING_REMOVE_POINT);
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_ITEM_MISC_SMALL", "AB_INVEN_ICON_ITEM_MISC_RESOURCE_BUILDING_POINT", false);
			NKCUIBase.SetImageSprite(this.m_imgRegainItem, orLoadAssetResource, true);
			NKCUIBase.SetLabelText(this.m_lbRegainItemCount, NKMWorldMapManager.GetTotalBuildingPointUsed(targetBuildingTemplet).ToString());
			this.m_tagPrice.SetData(targetBuildingTemplet.ClearCostItem.ItemID, targetBuildingTemplet.ClearCostItem.Count, false, false, false);
			NKCUtil.SetLabelText(this.m_lbOK, NKCStringTable.GetString("SI_PF_COMMON_OK_2", false));
			base.UIOpened(true);
		}

		// Token: 0x060076D2 RID: 30418 RVA: 0x00278C78 File Offset: 0x00276E78
		public void OpenForRestoreEnterLimit(NKMStageTempletV2 stageTemplet, NKCPopupResourceWithdraw.OnOk onOK, int restoreCnt = 0)
		{
			if (stageTemplet == null)
			{
				return;
			}
			this.dOnOk = onOK;
			string msg;
			if (stageTemplet.EnterLimitCond == SHOP_RESET_TYPE.DAY || stageTemplet.EnterLimitCond == SHOP_RESET_TYPE.Unlimited)
			{
				msg = string.Format(NKCUtilString.GET_STRING_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_DAY_02, stageTemplet.RestoreLimit - restoreCnt, stageTemplet.RestoreLimit);
			}
			else if (stageTemplet.EnterLimitCond == SHOP_RESET_TYPE.MONTH)
			{
				msg = string.Format(NKCUtilString.GET_STRING_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_MONTH_02, stageTemplet.RestoreLimit - restoreCnt, stageTemplet.RestoreLimit);
			}
			else
			{
				msg = string.Format(NKCUtilString.GET_STRING_WARFARE_GAME_HUD_RESTORE_LIMIT_DESC_WEEK_02, stageTemplet.RestoreLimit - restoreCnt, stageTemplet.RestoreLimit);
			}
			NKCUtil.SetGameobjectActive(this.m_objRegain, false);
			NKCUIBase.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_NOTICE);
			NKCUIBase.SetLabelText(this.m_lbText, msg);
			this.m_tagPrice.SetData(stageTemplet.RestoreReqItem.ItemId, stageTemplet.RestoreReqItem.Count32, false, false, false);
			base.UIOpened(true);
		}

		// Token: 0x060076D3 RID: 30419 RVA: 0x00278D72 File Offset: 0x00276F72
		private void OnBtnOK()
		{
			NKCPopupResourceWithdraw.OnOk onOk = this.dOnOk;
			if (onOk != null)
			{
				onOk();
			}
			base.Close();
		}

		// Token: 0x0400634B RID: 25419
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400634C RID: 25420
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_RESOURCE_WITHDRAW";

		// Token: 0x0400634D RID: 25421
		private static NKCPopupResourceWithdraw m_Instance;

		// Token: 0x0400634E RID: 25422
		public NKCUIComStateButton m_btnBG;

		// Token: 0x0400634F RID: 25423
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006350 RID: 25424
		public Text m_lbTitle;

		// Token: 0x04006351 RID: 25425
		public Text m_lbText;

		// Token: 0x04006352 RID: 25426
		public GameObject m_objRegain;

		// Token: 0x04006353 RID: 25427
		public Text m_lbRegainDesc;

		// Token: 0x04006354 RID: 25428
		public Image m_imgRegainItem;

		// Token: 0x04006355 RID: 25429
		public Text m_lbRegainItemCount;

		// Token: 0x04006356 RID: 25430
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x04006357 RID: 25431
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x04006358 RID: 25432
		public Text m_lbOK;

		// Token: 0x04006359 RID: 25433
		public NKCUIPriceTag m_tagPrice;

		// Token: 0x0400635A RID: 25434
		private NKCPopupResourceWithdraw.OnOk dOnOk;

		// Token: 0x020017DA RID: 6106
		// (Invoke) Token: 0x0600B464 RID: 46180
		public delegate void OnOk();
	}
}
