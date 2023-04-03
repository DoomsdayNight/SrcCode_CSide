using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A66 RID: 2662
	public class NKCPopupInventoryAdd : NKCUIBase, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06007523 RID: 29987 RVA: 0x0026F114 File Offset: 0x0026D314
		public static NKCPopupInventoryAdd Instance
		{
			get
			{
				if (NKCPopupInventoryAdd.m_Instance == null)
				{
					NKCPopupInventoryAdd.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupInventoryAdd>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_INVENTORY_ADD", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupInventoryAdd.CleanupInstance)).GetInstance<NKCPopupInventoryAdd>();
					NKCPopupInventoryAdd instance = NKCPopupInventoryAdd.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupInventoryAdd.m_Instance;
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06007524 RID: 29988 RVA: 0x0026F169 File Offset: 0x0026D369
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupInventoryAdd.m_Instance != null && NKCPopupInventoryAdd.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007525 RID: 29989 RVA: 0x0026F184 File Offset: 0x0026D384
		private static void CleanupInstance()
		{
			NKCPopupInventoryAdd.m_Instance = null;
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06007526 RID: 29990 RVA: 0x0026F18C File Offset: 0x0026D38C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06007527 RID: 29991 RVA: 0x0026F18F File Offset: 0x0026D38F
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06007528 RID: 29992 RVA: 0x0026F196 File Offset: 0x0026D396
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				if (this.m_bShowResource)
				{
					return NKCUIUpsideMenu.eMode.ResourceOnly;
				}
				return base.eUpsideMenuMode;
			}
		}

		// Token: 0x06007529 RID: 29993 RVA: 0x0026F1A8 File Offset: 0x0026D3A8
		private void Init()
		{
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			NKCUtil.SetButtonClickDelegate(this.m_sbtnPlusButton, new UnityAction(this.OnPlus));
			NKCUtil.SetHotkey(this.m_sbtnPlusButton, HotkeyEventType.Plus, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_sbtnMinusButton, new UnityAction(this.OnMinus));
			NKCUtil.SetHotkey(this.m_sbtnMinusButton, HotkeyEventType.Minus, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_sbtnOkButton, new UnityAction(this.OnOk));
			NKCUtil.SetHotkey(this.m_sbtnOkButton, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_sbtnCancleButton, new UnityAction(this.OnCancle));
			NKCUtil.SetButtonClickDelegate(this.m_sbtnCloseButton, new UnityAction(this.OnCancle));
			NKCUtil.SetSliderValueChangedDelegate(this.m_sliderGauge, new UnityAction<float>(this.OnSliderChanged));
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetButtonClickDelegate(this.m_sbtnMoveButton, new UnityAction(this.OnClickMove));
			NKCUtil.SetButtonClickDelegate(this.m_sbtnAdOnButton, new UnityAction(this.OnClickAd));
		}

		// Token: 0x0600752A RID: 29994 RVA: 0x0026F2C0 File Offset: 0x0026D4C0
		public void Open(string title, string contentText, NKCPopupInventoryAdd.SliderInfo sliderInfo, int requiredItemCount, int requiredItemID, NKCPopupInventoryAdd.OnClickOK onClickOK = null, bool showResource = false)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (sliderInfo.currentCount >= sliderInfo.maxCount)
			{
				requiredItemCount = 0;
			}
			long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(requiredItemID);
			this.m_ItemCostSlot.SetData(requiredItemID, requiredItemCount, countMiscItem, true, true, false);
			this.m_eInventoryType = sliderInfo.inventoryType;
			this.m_iCurrentMaxCount = sliderInfo.currentCount;
			this.m_iIncreaseCount = sliderInfo.increaseCount;
			this.m_iIncreaseMaxCount = sliderInfo.maxCount;
			this.m_iFirstIncreaseCount = Mathf.Min(sliderInfo.maxCount, sliderInfo.currentCount + sliderInfo.increaseCount);
			this.m_iNextMaxCount = this.m_iFirstIncreaseCount;
			this.m_iRequiredResourceCount = requiredItemCount;
			this.m_iNeedResourceID = requiredItemID;
			NKCUtil.SetLabelText(this.m_lbTopText, title);
			NKCUtil.SetLabelText(this.m_lbInfoText, contentText);
			NKCUtil.SetLabelText(this.m_lbCurrentCount, sliderInfo.currentCount.ToString());
			NKCUtil.SetLabelText(this.m_lbMinusText, string.Format("-{0}", sliderInfo.increaseCount));
			NKCUtil.SetLabelText(this.m_lbPlusText, string.Format("+{0}", sliderInfo.increaseCount));
			int num = (this.m_iIncreaseMaxCount - this.m_iFirstIncreaseCount) / this.m_iIncreaseCount;
			NKCUtil.SetSliderMinMax(this.m_sliderGauge, 1f, (float)(num + 1));
			NKCUtil.SetSliderValue(this.m_sliderGauge, 1f);
			this.UpdateNextMaxCountText(this.m_lbNextCount);
			this.UpdateAddCountText(this.m_lbAddCount);
			this.CheckResourceIsEnough(requiredItemCount, countMiscItem);
			this.m_bShowResource = showResource;
			this.m_dOnClickOK = onClickOK;
			NKCUtil.SetGameobjectActive(this.m_JPN_POLICY, NKCUtil.IsJPNPolicyRelatedItem(requiredItemID));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetAdButtonState(sliderInfo.inventoryType);
			this.SetMoveButtonState();
			NKCUIComStateButton sbtnOkButton = this.m_sbtnOkButton;
			if (sbtnOkButton != null)
			{
				sbtnOkButton.SetLock(this.m_iCurrentMaxCount > this.m_iIncreaseMaxCount - this.m_iIncreaseCount, false);
			}
			NKCUIOpenAnimator nkcuiopenAnimator = this.m_NKCUIOpenAnimator;
			if (nkcuiopenAnimator != null)
			{
				nkcuiopenAnimator.PlayOpenAni();
			}
			base.UIOpened(true);
		}

		// Token: 0x0600752B RID: 29995 RVA: 0x0026F4BB File Offset: 0x0026D6BB
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600752C RID: 29996 RVA: 0x0026F4C9 File Offset: 0x0026D6C9
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x0600752D RID: 29997 RVA: 0x0026F4DE File Offset: 0x0026D6DE
		private void UpdateNextMaxCountText(Text nextCountText)
		{
			NKCUtil.SetLabelText(nextCountText, this.m_iNextMaxCount.ToString());
		}

		// Token: 0x0600752E RID: 29998 RVA: 0x0026F4F1 File Offset: 0x0026D6F1
		private void UpdateAddCountText(Text addCountText)
		{
			NKCUtil.SetLabelText(addCountText, string.Format("+{0}", this.m_iNextMaxCount - this.m_iCurrentMaxCount));
		}

		// Token: 0x0600752F RID: 29999 RVA: 0x0026F515 File Offset: 0x0026D715
		private int GetRequiredResourceCount()
		{
			return this.m_iRequiredResourceCount * this.GetExpandTimes();
		}

		// Token: 0x06007530 RID: 30000 RVA: 0x0026F524 File Offset: 0x0026D724
		private int GetExpandTimes()
		{
			return Mathf.RoundToInt(this.m_sliderGauge.value);
		}

		// Token: 0x06007531 RID: 30001 RVA: 0x0026F536 File Offset: 0x0026D736
		private void CheckResourceIsEnough(int requiredResourceCount, long currentItemCount)
		{
			this.m_bResourceEnough = ((long)requiredResourceCount <= currentItemCount);
			if (!this.m_bResourceEnough)
			{
				this.m_iNeedResourceCount = requiredResourceCount - (int)currentItemCount;
			}
		}

		// Token: 0x06007532 RID: 30002 RVA: 0x0026F558 File Offset: 0x0026D758
		private void UpdateItemCostSlotCount()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(this.m_iNeedResourceID);
			int requiredResourceCount = this.GetRequiredResourceCount();
			this.m_ItemCostSlot.SetCount(requiredResourceCount, countMiscItem);
			this.CheckResourceIsEnough(requiredResourceCount, countMiscItem);
		}

		// Token: 0x06007533 RID: 30003 RVA: 0x0026F59D File Offset: 0x0026D79D
		private int GetSliderStepValue(int nextMaxCount)
		{
			return 1 + (nextMaxCount - this.m_iFirstIncreaseCount) / this.m_iIncreaseCount;
		}

		// Token: 0x06007534 RID: 30004 RVA: 0x0026F5B0 File Offset: 0x0026D7B0
		private void SetAdButtonState(NKM_INVENTORY_EXPAND_TYPE inventoryType)
		{
			if (!NKCAdManager.IsAdRewardInventory(inventoryType))
			{
				NKCUtil.SetGameobjectActive(this.m_sbtnAdOnButton, false);
				NKCUtil.SetGameobjectActive(this.m_sbtnAdOffButton, false);
				return;
			}
			if (NKCAdManager.InventoryRewardReceived(inventoryType) || this.m_iCurrentMaxCount >= this.m_iIncreaseMaxCount)
			{
				NKCUtil.SetGameobjectActive(this.m_sbtnAdOnButton, false);
				NKCUtil.SetGameobjectActive(this.m_sbtnAdOffButton, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_sbtnAdOnButton, true);
			NKCUtil.SetGameobjectActive(this.m_sbtnAdOffButton, false);
			NKCUtil.SetLabelText(this.m_lbAdRemainCount, "(1/1)");
			this.m_sbtnAdOnButton.SetLock(false, false);
		}

		// Token: 0x06007535 RID: 30005 RVA: 0x0026F644 File Offset: 0x0026D844
		private void SetMoveButtonState()
		{
			if (this.m_sbtnMoveButton == null)
			{
				return;
			}
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID == NKM_SCEN_ID.NSI_UNIT_LIST || nowScenID == NKM_SCEN_ID.NSI_INVENTORY)
			{
				NKCUtil.SetGameobjectActive(this.m_sbtnMoveButton, false);
				return;
			}
			NKM_INVENTORY_EXPAND_TYPE eInventoryType = this.m_eInventoryType;
			if (eInventoryType == NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP)
			{
				this.m_sbtnMoveButton.SetTitleText(NKCUtilString.GET_STRING_INVEN);
				NKCUtil.SetGameobjectActive(this.m_sbtnMoveButton, true);
				return;
			}
			if (eInventoryType - NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT > 2)
			{
				NKCUtil.SetGameobjectActive(this.m_sbtnMoveButton, false);
				return;
			}
			this.m_sbtnMoveButton.SetTitleText(NKCStringTable.GetString("SI_LOBBY_RIGHT_MENU_2_UNITLIST_TEXT", false));
			NKCUtil.SetGameobjectActive(this.m_sbtnMoveButton, true);
		}

		// Token: 0x06007536 RID: 30006 RVA: 0x0026F6E0 File Offset: 0x0026D8E0
		private void OnOk()
		{
			if (!this.m_bResourceEnough)
			{
				base.Close();
				NKCShopManager.OpenItemLackPopup(this.m_iNeedResourceID, this.m_iNeedResourceCount);
				return;
			}
			if (this.m_iCurrentMaxCount < this.m_iIncreaseMaxCount)
			{
				base.Close();
				if (this.m_dOnClickOK != null)
				{
					this.m_dOnClickOK(this.GetExpandTimes());
				}
			}
		}

		// Token: 0x06007537 RID: 30007 RVA: 0x0026F73A File Offset: 0x0026D93A
		private void OnCancle()
		{
			base.Close();
		}

		// Token: 0x06007538 RID: 30008 RVA: 0x0026F744 File Offset: 0x0026D944
		private void OnPlus()
		{
			this.m_iNextMaxCount = Mathf.Min(this.m_iIncreaseMaxCount, this.m_iNextMaxCount + this.m_iIncreaseCount);
			this.UpdateNextMaxCountText(this.m_lbNextCount);
			NKCUtil.SetSliderValue(this.m_sliderGauge, (float)this.GetSliderStepValue(this.m_iNextMaxCount));
			this.UpdateItemCostSlotCount();
			this.UpdateAddCountText(this.m_lbAddCount);
		}

		// Token: 0x06007539 RID: 30009 RVA: 0x0026F7A8 File Offset: 0x0026D9A8
		private void OnMinus()
		{
			this.m_iNextMaxCount = Mathf.Max(this.m_iFirstIncreaseCount, this.m_iNextMaxCount - this.m_iIncreaseCount);
			this.UpdateNextMaxCountText(this.m_lbNextCount);
			NKCUtil.SetSliderValue(this.m_sliderGauge, (float)this.GetSliderStepValue(this.m_iNextMaxCount));
			this.UpdateItemCostSlotCount();
			this.UpdateAddCountText(this.m_lbAddCount);
		}

		// Token: 0x0600753A RID: 30010 RVA: 0x0026F80C File Offset: 0x0026DA0C
		private void OnSliderChanged(float sliderValue)
		{
			int iNextMaxCount = Mathf.Min(this.m_iIncreaseMaxCount, this.m_iCurrentMaxCount + Mathf.RoundToInt(sliderValue) * this.m_iIncreaseCount);
			this.m_iNextMaxCount = iNextMaxCount;
			this.UpdateNextMaxCountText(this.m_lbNextCount);
			this.UpdateItemCostSlotCount();
			this.UpdateAddCountText(this.m_lbAddCount);
		}

		// Token: 0x0600753B RID: 30011 RVA: 0x0026F85E File Offset: 0x0026DA5E
		private void OnClickAd()
		{
			base.Close();
			NKCAdManager.WatchInventoryRewardAd(this.m_eInventoryType);
		}

		// Token: 0x0600753C RID: 30012 RVA: 0x0026F874 File Offset: 0x0026DA74
		private void OnClickMove()
		{
			switch (this.m_eInventoryType)
			{
			case NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP:
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_INVENTORY, "NIT_EQUIP", false);
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT:
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_UNITLIST, "", false);
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP:
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_UNITLIST, "ULT_SHIP", false);
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR:
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_UNITLIST, "ULT_OPERATOR", false);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600753D RID: 30013 RVA: 0x0026F8D8 File Offset: 0x0026DAD8
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData.scrollDelta.y < 0f)
			{
				this.m_iNextMaxCount -= this.m_iIncreaseCount;
			}
			else if (eventData.scrollDelta.y > 0f)
			{
				this.m_iNextMaxCount += this.m_iIncreaseCount;
			}
			this.m_iNextMaxCount = Mathf.Clamp(this.m_iNextMaxCount, this.m_iFirstIncreaseCount, this.m_iIncreaseMaxCount);
			this.UpdateNextMaxCountText(this.m_lbNextCount);
			NKCUtil.SetSliderValue(this.m_sliderGauge, (float)this.GetSliderStepValue(this.m_iNextMaxCount));
			this.UpdateItemCostSlotCount();
			this.UpdateAddCountText(this.m_lbAddCount);
		}

		// Token: 0x04006167 RID: 24935
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04006168 RID: 24936
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_INVENTORY_ADD";

		// Token: 0x04006169 RID: 24937
		private static NKCPopupInventoryAdd m_Instance;

		// Token: 0x0400616A RID: 24938
		public Text m_lbTopText;

		// Token: 0x0400616B RID: 24939
		public Text m_lbInfoText;

		// Token: 0x0400616C RID: 24940
		public Text m_lbCurrentCount;

		// Token: 0x0400616D RID: 24941
		public Text m_lbNextCount;

		// Token: 0x0400616E RID: 24942
		public Text m_lbAddCount;

		// Token: 0x0400616F RID: 24943
		public Text m_lbPlusText;

		// Token: 0x04006170 RID: 24944
		public Text m_lbMinusText;

		// Token: 0x04006171 RID: 24945
		public Text m_lbAdRemainCount;

		// Token: 0x04006172 RID: 24946
		[Header("슬라이더")]
		public GameObject m_objGauge;

		// Token: 0x04006173 RID: 24947
		public Slider m_sliderGauge;

		// Token: 0x04006174 RID: 24948
		[Header("소모 아이템 아이콘")]
		public NKCUIItemCostSlot m_ItemCostSlot;

		// Token: 0x04006175 RID: 24949
		[Header("버튼들")]
		public NKCUIComStateButton m_sbtnPlusButton;

		// Token: 0x04006176 RID: 24950
		public NKCUIComStateButton m_sbtnMinusButton;

		// Token: 0x04006177 RID: 24951
		public NKCUIComStateButton m_sbtnOkButton;

		// Token: 0x04006178 RID: 24952
		public NKCUIComStateButton m_sbtnCancleButton;

		// Token: 0x04006179 RID: 24953
		public NKCUIComStateButton m_sbtnCloseButton;

		// Token: 0x0400617A RID: 24954
		public NKCUIComStateButton m_sbtnAdOnButton;

		// Token: 0x0400617B RID: 24955
		public NKCUIComStateButton m_sbtnAdOffButton;

		// Token: 0x0400617C RID: 24956
		public NKCUIComStateButton m_sbtnMoveButton;

		// Token: 0x0400617D RID: 24957
		[Header("일본 법무 대응")]
		public GameObject m_JPN_POLICY;

		// Token: 0x0400617E RID: 24958
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x0400617F RID: 24959
		private NKM_INVENTORY_EXPAND_TYPE m_eInventoryType;

		// Token: 0x04006180 RID: 24960
		private int m_iCurrentMaxCount;

		// Token: 0x04006181 RID: 24961
		private int m_iIncreaseCount;

		// Token: 0x04006182 RID: 24962
		private int m_iIncreaseMaxCount;

		// Token: 0x04006183 RID: 24963
		private int m_iNextMaxCount;

		// Token: 0x04006184 RID: 24964
		private int m_iFirstIncreaseCount;

		// Token: 0x04006185 RID: 24965
		private int m_iRequiredResourceCount;

		// Token: 0x04006186 RID: 24966
		private int m_iNeedResourceID;

		// Token: 0x04006187 RID: 24967
		private int m_iNeedResourceCount;

		// Token: 0x04006188 RID: 24968
		private bool m_bResourceEnough;

		// Token: 0x04006189 RID: 24969
		private bool m_bShowResource;

		// Token: 0x0400618A RID: 24970
		public NKCPopupInventoryAdd.OnClickOK m_dOnClickOK;

		// Token: 0x020017C4 RID: 6084
		public struct SliderInfo
		{
			// Token: 0x0400A77A RID: 42874
			public NKM_INVENTORY_EXPAND_TYPE inventoryType;

			// Token: 0x0400A77B RID: 42875
			public int increaseCount;

			// Token: 0x0400A77C RID: 42876
			public int maxCount;

			// Token: 0x0400A77D RID: 42877
			public int currentCount;
		}

		// Token: 0x020017C5 RID: 6085
		// (Invoke) Token: 0x0600B42A RID: 46122
		public delegate void OnClickOK(int value);
	}
}
