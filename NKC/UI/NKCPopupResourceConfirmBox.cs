using System;
using System.Collections.Generic;
using NKM;
using NKM.Contract2;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A78 RID: 2680
	public class NKCPopupResourceConfirmBox : NKCUIBase
	{
		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x0600769D RID: 30365 RVA: 0x00277C90 File Offset: 0x00275E90
		public static NKCPopupResourceConfirmBox Instance
		{
			get
			{
				if (NKCPopupResourceConfirmBox.m_Instance == null)
				{
					NKCPopupResourceConfirmBox.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupResourceConfirmBox>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_RESOURCE_USE_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupResourceConfirmBox.CleanupInstance)).GetInstance<NKCPopupResourceConfirmBox>();
					NKCPopupResourceConfirmBox instance = NKCPopupResourceConfirmBox.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupResourceConfirmBox.m_Instance;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x0600769E RID: 30366 RVA: 0x00277CE5 File Offset: 0x00275EE5
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupResourceConfirmBox.m_Instance != null && NKCPopupResourceConfirmBox.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600769F RID: 30367 RVA: 0x00277D00 File Offset: 0x00275F00
		private static void CleanupInstance()
		{
			NKCPopupResourceConfirmBox.m_Instance = null;
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x060076A0 RID: 30368 RVA: 0x00277D08 File Offset: 0x00275F08
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x060076A1 RID: 30369 RVA: 0x00277D0B File Offset: 0x00275F0B
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x060076A2 RID: 30370 RVA: 0x00277D12 File Offset: 0x00275F12
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

		// Token: 0x060076A3 RID: 30371 RVA: 0x00277D24 File Offset: 0x00275F24
		private void Init()
		{
			foreach (NKCUISlot nkcuislot in this.m_lstInvenSlot)
			{
				if (nkcuislot != null)
				{
					nkcuislot.Init();
				}
			}
			NKCUtil.SetHotkey(this.m_cbtnOK, HotkeyEventType.Confirm);
		}

		// Token: 0x060076A4 RID: 30372 RVA: 0x00277D88 File Offset: 0x00275F88
		public void OpenWithLeftCount(string Title, string Content, int itemID, int requiredCount, int leftCount, int maxCount, NKCPopupResourceConfirmBox.OnButton onOkButton, NKCPopupResourceConfirmBox.OnButton onCancelButton = null)
		{
			NKCUtil.SetGameobjectActive(this.m_objCountLimit, true);
			NKCUtil.SetLabelText(this.m_lbCountLimit, string.Format(NKCUtilString.GET_STRING_REMAIN_COUNT_TWO_PARAM, leftCount, maxCount));
			this.m_lbCountLimit.color = Color.white;
			this.OpenInternal(Title, Content, itemID, requiredCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemID), onOkButton, onCancelButton);
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x00277DF3 File Offset: 0x00275FF3
		public void Open(string Title, string Content, int itemID, int requiredCount, long curItemCount, NKCPopupResourceConfirmBox.OnButton onOkButton, NKCPopupResourceConfirmBox.OnButton onCancelButton = null, bool showResource = false)
		{
			this.m_bShowResource = showResource;
			NKCUtil.SetGameobjectActive(this.m_objCountLimit, false);
			this.OpenInternal(Title, Content, itemID, requiredCount, curItemCount, onOkButton, onCancelButton);
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x00277E1A File Offset: 0x0027601A
		public void Open(string Title, string Content, int itemID, int requiredCount, NKCPopupResourceConfirmBox.OnButton onOkButton, NKCPopupResourceConfirmBox.OnButton onCancelButton = null, bool showResource = false)
		{
			this.m_bShowResource = showResource;
			NKCUtil.SetGameobjectActive(this.m_objCountLimit, false);
			this.OpenInternal(Title, Content, itemID, requiredCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemID), onOkButton, onCancelButton);
		}

		// Token: 0x060076A7 RID: 30375 RVA: 0x00277E50 File Offset: 0x00276050
		public void OpenForContract(string Title, string Content, int itemID, int requiredCount, NKCPopupResourceConfirmBox.OnButton onOkButton, NKCPopupResourceConfirmBox.OnButton onCancelButton = null, int contractID = 0, int contractCnt = 0)
		{
			this.m_ContractID = contractID;
			this.m_ContractCnt = contractCnt;
			NKCUtil.SetGameobjectActive(this.m_objCountLimit, false);
			this.OpenInternal(Title, Content, itemID, requiredCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemID), onOkButton, onCancelButton);
			NKCUtil.SetGameobjectActive(this.m_objItemCostSlot, itemID > 0 && requiredCount > 0);
			this.m_ContractID = 0;
			this.m_ContractCnt = 0;
		}

		// Token: 0x060076A8 RID: 30376 RVA: 0x00277EC0 File Offset: 0x002760C0
		private void OpenInternal(string Title, string Content, int itemID, int requiredCount, long curItemCount, NKCPopupResourceConfirmBox.OnButton onOkButton, NKCPopupResourceConfirmBox.OnButton onCancelButton)
		{
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			NKCUtil.SetGameobjectActive(this.m_objInvenSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objCountLimit, false);
			NKCScenManager.CurrentUserData();
			NKCUtil.SetGameobjectActive(this.m_objItemCostSlot, true);
			this.m_ItemCostSlot.SetData(itemID, requiredCount, curItemCount, true, true, false);
			this.m_bResourceEnough = ((long)requiredCount <= curItemCount);
			if (!this.m_bResourceEnough)
			{
				this.m_iNeedResourceID = itemID;
				this.m_iNeedResourceCount = requiredCount - (int)curItemCount;
			}
			NKCUtil.SetLabelText(this.m_lbTitle, Title);
			NKCUtil.SetLabelText(this.m_lbMessage, Content);
			NKCUtil.SetGameobjectActive(this.m_CONTRACT_POINT, false);
			if (this.m_ContractID != 0 && this.m_ContractCnt > 0)
			{
				ContractTempletV2 contractTempletV = ContractTempletV2.Find(this.m_ContractID);
				if (contractTempletV != null && contractTempletV.m_ResultRewards != null && contractTempletV.m_ResultRewards.Count > 0)
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(contractTempletV.m_ResultRewards[0].ItemID);
					if (itemMiscTempletByID != null)
					{
						Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
						NKCUtil.SetImageSprite(this.m_CONTRACT_POINT_ICON, orLoadMiscItemSmallIcon, false);
						NKCUtil.SetLabelText(this.m_CONTRACT_POINT_TEXT, string.Format(NKCUtilString.GET_STRING_POPUP_RESOURCE_CONFIRM_REWARD_DESC_02, itemMiscTempletByID.GetItemName(), contractTempletV.m_ResultRewards[0].Count * this.m_ContractCnt));
						NKCUtil.SetGameobjectActive(this.m_CONTRACT_POINT, !contractTempletV.MissionCountIgnore);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_JPN_POLICY, NKCUtil.IsJPNPolicyRelatedItem(itemID));
			this.dOnOKButton = onOkButton;
			this.dOnCancelButton = onCancelButton;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x060076A9 RID: 30377 RVA: 0x00278070 File Offset: 0x00276270
		public void OpenForSelection(NKMItemMiscTemplet itemMiscTemplet, int targetID, long targetCount, NKCPopupResourceConfirmBox.OnButton onOkButton, NKCPopupResourceConfirmBox.OnButton onCancelButton = null, bool showResource = false, int setItemID = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_objCountLimit, false);
			NKCUtil.SetGameobjectActive(this.m_objItemCostSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objInvenSlot, true);
			NKCUtil.SetGameobjectActive(this.m_CONTRACT_POINT, false);
			this.m_bResourceEnough = true;
			NKCUtil.SetLabelText(this.m_lbTitle, itemMiscTemplet.GetItemName());
			NKCUISlot.SlotData data = new NKCUISlot.SlotData();
			NKM_ITEM_MISC_TYPE itemMiscType = itemMiscTemplet.m_ItemMiscType;
			switch (itemMiscType)
			{
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT:
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_OPERATOR:
				NKCUtil.SetLabelText(this.m_lbMessage, NKCUtilString.GET_STRING_CHOICE_UNIT_RECHECK);
				data = NKCUISlot.SlotData.MakeUnitData(targetID, 1, 0, 0);
				break;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP:
				NKCUtil.SetLabelText(this.m_lbMessage, NKCUtilString.GET_STRING_CHOICE_SHIP_RECHECK);
				data = NKCUISlot.SlotData.MakeUnitData(targetID, 1, 0, 0);
				break;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_EQUIP:
				NKCUtil.SetLabelText(this.m_lbMessage, NKCUtilString.GET_STRING_CHOICE_EQUIP_RECHECK);
				data = NKCUISlot.SlotData.MakeEquipData(targetID, (int)targetCount, setItemID);
				break;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC:
				NKCUtil.SetLabelText(this.m_lbMessage, NKCUtilString.GET_STRING_CHOICE_MISC_RECHECK);
				data = NKCUISlot.SlotData.MakeMiscItemData(targetID, targetCount, 0);
				break;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_MOLD:
				break;
			default:
				if (itemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_SKIN)
				{
					NKCUtil.SetLabelText(this.m_lbMessage, NKCUtilString.GET_STRING_CHOICE_SKIN_RECHECK);
					data = NKCUISlot.SlotData.MakeSkinData(targetID);
				}
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_lstInvenSlot[0], true);
			this.m_lstInvenSlot[0].SetData(data, true, null);
			for (int i = 1; i < this.m_lstInvenSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstInvenSlot[i], false);
			}
			this.dOnOKButton = onOkButton;
			this.dOnCancelButton = onCancelButton;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x060076AA RID: 30378 RVA: 0x00278218 File Offset: 0x00276418
		public void OpenItemSlotList(string title, string content, List<NKCUISlot.SlotData> lstSlot, NKCPopupResourceConfirmBox.OnButton onOkButton, NKCPopupResourceConfirmBox.OnButton onCancelButton = null, bool mustShowNum = false)
		{
			if (lstSlot == null)
			{
				Debug.LogError("lstSlot Null!");
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objCountLimit, false);
			NKCUtil.SetGameobjectActive(this.m_objItemCostSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objInvenSlot, true);
			NKCUtil.SetGameobjectActive(this.m_CONTRACT_POINT, false);
			this.m_bResourceEnough = true;
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			NKCUtil.SetLabelText(this.m_lbMessage, content);
			for (int i = 0; i < this.m_lstInvenSlot.Count; i++)
			{
				if (i < lstSlot.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstInvenSlot[i], true);
					if (mustShowNum)
					{
						this.m_lstInvenSlot[i].SetData(lstSlot[i], false, true, true, null);
					}
					else
					{
						this.m_lstInvenSlot[i].SetData(lstSlot[i], true, null);
					}
					this.m_lstInvenSlot[i].SetOnClickAction(new NKCUISlot.SlotClickType[1]);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstInvenSlot[i], false);
				}
			}
			this.dOnOKButton = onOkButton;
			this.dOnCancelButton = onCancelButton;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x060076AB RID: 30379 RVA: 0x00278368 File Offset: 0x00276568
		public void OpenForConfirm(string title, string content, NKCPopupResourceConfirmBox.OnButton onOkButton, NKCPopupResourceConfirmBox.OnButton onCancelButton = null, bool showResource = false)
		{
			this.m_bShowResource = showResource;
			this.m_bResourceEnough = true;
			NKCUtil.SetGameobjectActive(this.m_objCountLimit, false);
			NKCUtil.SetGameobjectActive(this.m_objItemCostSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objInvenSlot, false);
			NKCUtil.SetGameobjectActive(this.m_CONTRACT_POINT, false);
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			NKCUtil.SetLabelText(this.m_lbMessage, content);
			this.dOnOKButton = onOkButton;
			this.dOnCancelButton = onCancelButton;
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x060076AC RID: 30380 RVA: 0x00278412 File Offset: 0x00276612
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x060076AD RID: 30381 RVA: 0x00278427 File Offset: 0x00276627
		public void OnOK()
		{
			if (!this.m_bResourceEnough)
			{
				base.Close();
				NKCShopManager.OpenItemLackPopup(this.m_iNeedResourceID, this.m_iNeedResourceCount);
				return;
			}
			base.Close();
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton();
			}
		}

		// Token: 0x060076AE RID: 30382 RVA: 0x00278462 File Offset: 0x00276662
		public void OnCancel()
		{
			base.Close();
			if (this.dOnCancelButton != null)
			{
				this.dOnCancelButton();
			}
		}

		// Token: 0x060076AF RID: 30383 RVA: 0x0027847D File Offset: 0x0027667D
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04006313 RID: 25363
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04006314 RID: 25364
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_RESOURCE_USE_CONFIRM";

		// Token: 0x04006315 RID: 25365
		private static NKCPopupResourceConfirmBox m_Instance;

		// Token: 0x04006316 RID: 25366
		private NKCPopupResourceConfirmBox.OnButton dOnOKButton;

		// Token: 0x04006317 RID: 25367
		private NKCPopupResourceConfirmBox.OnButton dOnCancelButton;

		// Token: 0x04006318 RID: 25368
		public Text m_lbTitle;

		// Token: 0x04006319 RID: 25369
		public Text m_lbMessage;

		// Token: 0x0400631A RID: 25370
		public GameObject m_objCountLimit;

		// Token: 0x0400631B RID: 25371
		public Text m_lbCountLimit;

		// Token: 0x0400631C RID: 25372
		public GameObject m_objItemCostSlot;

		// Token: 0x0400631D RID: 25373
		public NKCUIItemCostSlot m_ItemCostSlot;

		// Token: 0x0400631E RID: 25374
		public GameObject m_objInvenSlot;

		// Token: 0x0400631F RID: 25375
		public List<NKCUISlot> m_lstInvenSlot;

		// Token: 0x04006320 RID: 25376
		public NKCUIComButton m_cbtnOK;

		// Token: 0x04006321 RID: 25377
		public NKCUIComButton m_cbtnCancel;

		// Token: 0x04006322 RID: 25378
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04006323 RID: 25379
		private bool m_bResourceEnough;

		// Token: 0x04006324 RID: 25380
		private int m_iNeedResourceID;

		// Token: 0x04006325 RID: 25381
		private int m_iNeedResourceCount;

		// Token: 0x04006326 RID: 25382
		private bool m_bShowResource;

		// Token: 0x04006327 RID: 25383
		[Header("채용 포인트")]
		public GameObject m_CONTRACT_POINT;

		// Token: 0x04006328 RID: 25384
		public Image m_CONTRACT_POINT_ICON;

		// Token: 0x04006329 RID: 25385
		public Text m_CONTRACT_POINT_TEXT;

		// Token: 0x0400632A RID: 25386
		[Header("일본 법무 대응")]
		public GameObject m_JPN_POLICY;

		// Token: 0x0400632B RID: 25387
		private int m_ContractID;

		// Token: 0x0400632C RID: 25388
		private int m_ContractCnt;

		// Token: 0x020017D8 RID: 6104
		// (Invoke) Token: 0x0600B45C RID: 46172
		public delegate void OnButton();
	}
}
