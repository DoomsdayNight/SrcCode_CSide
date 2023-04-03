using System;
using System.Collections.Generic;
using ClientPacket.Item;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A5B RID: 2651
	public class NKCPopupForgeCraft : NKCUIBase
	{
		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x0600745E RID: 29790 RVA: 0x0026B0A4 File Offset: 0x002692A4
		public static NKCPopupForgeCraft Instance
		{
			get
			{
				if (NKCPopupForgeCraft.m_Instance == null)
				{
					NKCPopupForgeCraft.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupForgeCraft>("AB_UI_NKM_UI_FACTORY", "NKM_UI_FACTORY_CRAFT_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupForgeCraft.CleanupInstance)).GetInstance<NKCPopupForgeCraft>();
					NKCPopupForgeCraft.m_Instance.InitUI();
				}
				return NKCPopupForgeCraft.m_Instance;
			}
		}

		// Token: 0x0600745F RID: 29791 RVA: 0x0026B0F3 File Offset: 0x002692F3
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupForgeCraft.m_Instance != null && NKCPopupForgeCraft.m_Instance.IsOpen)
			{
				NKCPopupForgeCraft.m_Instance.Close();
			}
		}

		// Token: 0x06007460 RID: 29792 RVA: 0x0026B118 File Offset: 0x00269318
		private static void CleanupInstance()
		{
			NKCPopupForgeCraft.m_Instance = null;
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06007461 RID: 29793 RVA: 0x0026B120 File Offset: 0x00269320
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06007462 RID: 29794 RVA: 0x0026B123 File Offset: 0x00269323
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_FORGE_CRAFT_POPUP;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06007463 RID: 29795 RVA: 0x0026B12A File Offset: 0x0026932A
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06007464 RID: 29796 RVA: 0x0026B132 File Offset: 0x00269332
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.ResourceOnly;
			}
		}

		// Token: 0x06007465 RID: 29797 RVA: 0x0026B138 File Offset: 0x00269338
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(this.m_goToAnimate);
			if (this.m_AB_ICON_SLOT != null)
			{
				this.m_AB_ICON_SLOT.Init();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnCloseBtn();
			});
			this.m_etBG.triggers.Clear();
			this.m_etBG.triggers.Add(entry);
			this.m_btnMinus.PointerClick.RemoveAllListeners();
			this.m_btnMinus.PointerClick.AddListener(new UnityAction(this.OnClickMinus));
			NKCUtil.SetHotkey(this.m_btnMinus, HotkeyEventType.Minus);
			this.m_btnPlus.PointerClick.RemoveAllListeners();
			this.m_btnPlus.PointerClick.AddListener(new UnityAction(this.OnClickPlus));
			NKCUtil.SetHotkey(this.m_btnPlus, HotkeyEventType.Plus);
			this.m_btnMax.PointerClick.RemoveAllListeners();
			this.m_btnMax.PointerClick.AddListener(new UnityAction(this.OnClickMax));
			this.m_btnCraft.PointerClick.RemoveAllListeners();
			this.m_btnCraft.PointerClick.AddListener(new UnityAction(this.OnClickMake));
			NKCUtil.SetHotkey(this.m_btnCraft, HotkeyEventType.Confirm);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(this.OnCloseBtn));
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(this.OnCloseBtn));
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007466 RID: 29798 RVA: 0x0026B2EE File Offset: 0x002694EE
		public void Open(NKMMoldItemData cNKMMoldItemData, int index)
		{
			if (cNKMMoldItemData == null)
			{
				return;
			}
			this.m_Index = index;
			base.gameObject.SetActive(true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.m_CurrCountToMake = 1;
			this.SetUI(cNKMMoldItemData);
			base.UIOpened(true);
		}

		// Token: 0x06007467 RID: 29799 RVA: 0x0026B328 File Offset: 0x00269528
		private void SetTimeUI()
		{
			if (this.m_cNKMMoldItemData == null)
			{
				return;
			}
			int num = this.m_CurrCountToMake;
			if (num <= 0)
			{
				num = 1;
			}
			int num2 = 0;
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_cNKMMoldItemData.m_MoldID);
			if (itemMoldTempletByID != null)
			{
				num2 = itemMoldTempletByID.m_Time * num;
			}
			TimeSpan timeSpan = new TimeSpan(num2 / 60, num2 % 60, 0);
			this.m_NKM_UI_POPUP_CRAFT_TIME_TEXT.text = NKCUtilString.GetTimeSpanString(timeSpan);
		}

		// Token: 0x06007468 RID: 29800 RVA: 0x0026B38C File Offset: 0x0026958C
		private void SetMaterialUI()
		{
			if (this.m_cNKMMoldItemData == null)
			{
				return;
			}
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_cNKMMoldItemData.m_MoldID);
			if (itemMoldTempletByID != null)
			{
				int num = this.m_CurrCountToMake;
				if (num < 1)
				{
					num = 1;
				}
				for (int i = 0; i < this.m_lst_Material.Count; i++)
				{
					if (i >= itemMoldTempletByID.m_MaterialList.Count)
					{
						this.m_lst_Material[i].SetData(0, 0, 0L, true, true, false);
					}
					else
					{
						long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemMoldTempletByID.m_MaterialList[i].m_MaterialID);
						int reqCnt = itemMoldTempletByID.m_MaterialList[i].m_MaterialValue * num;
						bool flag = itemMoldTempletByID.m_MaterialList[i].m_MaterialID == 1 && NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT);
						if (flag)
						{
							NKCCompanyBuff.SetDiscountOfCreditInCraft(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref reqCnt);
						}
						this.m_lst_Material[i].SetData(itemMoldTempletByID.m_MaterialList[i].m_MaterialID, reqCnt, countMiscItem, true, true, flag);
					}
				}
			}
		}

		// Token: 0x06007469 RID: 29801 RVA: 0x0026B4AC File Offset: 0x002696AC
		private void SetUI(NKMMoldItemData cNKMMoldItemData)
		{
			if (cNKMMoldItemData == null)
			{
				return;
			}
			this.m_cNKMMoldItemData = cNKMMoldItemData;
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(cNKMMoldItemData.m_MoldID);
			if (itemMoldTempletByID != null)
			{
				this.m_AB_ICON_SLOT.SetData(NKCUISlot.SlotData.MakeMoldItemData(itemMoldTempletByID.m_MoldID, cNKMMoldItemData.m_Count), true, null);
				if (itemMoldTempletByID.m_bPermanent)
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CRAFT_COUNT, NKCUtilString.GET_STRING_FORGE_CRAFT_COUNT_INFINITE);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CRAFT_COUNT, string.Format(NKCUtilString.GET_STRING_ITEM_COUNT_ONE_PARAM, cNKMMoldItemData.m_Count));
				}
				NKCUtil.SetLabelText(this.m_NKM_UI_FACTORY_CRAFT_POPUP_TOP_TEXT, NKCUtilString.GET_STRING_FORGE_CRAFT_POPUP_TITLE);
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CRAFT_NAME, itemMoldTempletByID.GetItemName());
				this.SetTimeUI();
				this.SetMaterialUI();
				this.SetCountUI();
			}
		}

		// Token: 0x0600746A RID: 29802 RVA: 0x0026B561 File Offset: 0x00269761
		public void OnClickMinus()
		{
			if (this.m_CurrCountToMake >= 2)
			{
				this.m_CurrCountToMake--;
				this.SetTimeUI();
				this.SetCountUI();
				this.SetMaterialUI();
			}
		}

		// Token: 0x0600746B RID: 29803 RVA: 0x0026B58C File Offset: 0x0026978C
		public void OnClickPlus()
		{
			long num = 10L;
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_cNKMMoldItemData.m_MoldID);
			if (itemMoldTempletByID != null && !itemMoldTempletByID.m_bPermanent)
			{
				num = this.m_cNKMMoldItemData.m_Count;
			}
			if (this.m_CurrCountToMake < 10 && (long)this.m_CurrCountToMake < num)
			{
				this.m_CurrCountToMake++;
				this.SetTimeUI();
				this.SetCountUI();
				this.SetMaterialUI();
			}
		}

		// Token: 0x0600746C RID: 29804 RVA: 0x0026B5F9 File Offset: 0x002697F9
		private void SetCountUI()
		{
			this.m_STACK_TEXT.text = this.m_CurrCountToMake.ToString();
		}

		// Token: 0x0600746D RID: 29805 RVA: 0x0026B614 File Offset: 0x00269814
		public void OnClickMax()
		{
			if (NKMItemManager.GetItemMoldTempletByID(this.m_cNKMMoldItemData.m_MoldID) == null)
			{
				return;
			}
			this.m_CurrCountToMake = NKCUtil.GetEquipCreatableCount(this.m_cNKMMoldItemData, NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData);
			this.SetTimeUI();
			this.SetCountUI();
			this.SetMaterialUI();
		}

		// Token: 0x0600746E RID: 29806 RVA: 0x0026B668 File Offset: 0x00269868
		public void OnClickMake()
		{
			if (this.m_CurrCountToMake < 1 || this.m_cNKMMoldItemData == null)
			{
				return;
			}
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_cNKMMoldItemData.m_MoldID);
			if (itemMoldTempletByID != null)
			{
				for (int i = 0; i < itemMoldTempletByID.m_MaterialList.Count; i++)
				{
					long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemMoldTempletByID.m_MaterialList[i].m_MaterialID);
					int materialValue = itemMoldTempletByID.m_MaterialList[i].m_MaterialValue;
					if (itemMoldTempletByID.m_MaterialList[i].m_MaterialID == 1 && NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT))
					{
						NKCCompanyBuff.SetDiscountOfCreditInCraft(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref materialValue);
					}
					if ((long)(materialValue * this.m_CurrCountToMake) > countMiscItem)
					{
						NKCShopManager.OpenItemLackPopup(itemMoldTempletByID.m_MaterialList[i].m_MaterialID, itemMoldTempletByID.m_MaterialList[i].m_MaterialValue * this.m_CurrCountToMake);
						return;
					}
				}
			}
			if (itemMoldTempletByID != null && itemMoldTempletByID.IsEquipMold)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					NKMInventoryData inventoryData = nkmuserData.m_InventoryData;
					if (inventoryData != null)
					{
						int maxItemEqipCount = inventoryData.m_MaxItemEqipCount;
						if (inventoryData.GetCountEquipItemTypes() >= maxItemEqipCount)
						{
							int count = 1;
							int num;
							bool flag = !NKCAdManager.IsAdRewardInventory(NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP) || !NKMInventoryManager.CanExpandInventoryByAd(NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP, nkmuserData, count, out num);
							if (!NKMInventoryManager.CanExpandInventory(NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP, nkmuserData, count, out num) && flag)
							{
								NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_CANNOT_EXPAND_INVENTORY), null, "");
								return;
							}
							string expandDesc = NKCUtilString.GetExpandDesc(NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP, true);
							NKCPopupInventoryAdd.SliderInfo sliderInfo = default(NKCPopupInventoryAdd.SliderInfo);
							sliderInfo.increaseCount = 5;
							sliderInfo.maxCount = 2000;
							sliderInfo.currentCount = maxItemEqipCount;
							sliderInfo.inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP;
							NKCPopupInventoryAdd.Instance.Open(NKCUtilString.GET_STRING_INVENTORY_EQUIP, expandDesc, sliderInfo, 50, 101, delegate(int value)
							{
								NKCPacketSender.Send_NKMPacket_INVENTORY_EXPAND_REQ(NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP, value);
							}, true);
							return;
						}
					}
				}
			}
			NKMPacket_CRAFT_START_REQ nkmpacket_CRAFT_START_REQ = new NKMPacket_CRAFT_START_REQ();
			nkmpacket_CRAFT_START_REQ.moldID = this.m_cNKMMoldItemData.m_MoldID;
			nkmpacket_CRAFT_START_REQ.index = (byte)this.m_Index;
			nkmpacket_CRAFT_START_REQ.count = this.m_CurrCountToMake;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CRAFT_START_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600746F RID: 29807 RVA: 0x0026B8A7 File Offset: 0x00269AA7
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x0026B8BC File Offset: 0x00269ABC
		public void CloseForgeCraftPopup()
		{
			base.Close();
		}

		// Token: 0x06007471 RID: 29809 RVA: 0x0026B8C4 File Offset: 0x00269AC4
		public void OnCloseBtn()
		{
			base.Close();
		}

		// Token: 0x06007472 RID: 29810 RVA: 0x0026B8CC File Offset: 0x00269ACC
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x040060BD RID: 24765
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_FACTORY";

		// Token: 0x040060BE RID: 24766
		private const string UI_ASSET_NAME = "NKM_UI_FACTORY_CRAFT_POPUP";

		// Token: 0x040060BF RID: 24767
		private static NKCPopupForgeCraft m_Instance;

		// Token: 0x040060C0 RID: 24768
		private readonly List<int> RESOURCE_LIST = new List<int>
		{
			1012,
			1,
			2,
			101
		};

		// Token: 0x040060C1 RID: 24769
		public GameObject m_goToAnimate;

		// Token: 0x040060C2 RID: 24770
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040060C3 RID: 24771
		public NKCUISlot m_AB_ICON_SLOT;

		// Token: 0x040060C4 RID: 24772
		public Text m_NKM_UI_POPUP_CRAFT_NAME;

		// Token: 0x040060C5 RID: 24773
		public Text m_NKM_UI_POPUP_CRAFT_COUNT;

		// Token: 0x040060C6 RID: 24774
		public Text m_NKM_UI_POPUP_CRAFT_TIME_TEXT;

		// Token: 0x040060C7 RID: 24775
		public Text m_STACK_TEXT;

		// Token: 0x040060C8 RID: 24776
		public List<NKCUIItemCostSlot> m_lst_Material;

		// Token: 0x040060C9 RID: 24777
		public EventTrigger m_etBG;

		// Token: 0x040060CA RID: 24778
		public NKCUIComButton m_btnMinus;

		// Token: 0x040060CB RID: 24779
		public NKCUIComButton m_btnPlus;

		// Token: 0x040060CC RID: 24780
		public NKCUIComButton m_btnMax;

		// Token: 0x040060CD RID: 24781
		public NKCUIComButton m_btnCancel;

		// Token: 0x040060CE RID: 24782
		public NKCUIComButton m_btnCraft;

		// Token: 0x040060CF RID: 24783
		public NKCUIComButton m_btnClose;

		// Token: 0x040060D0 RID: 24784
		private int m_CurrCountToMake = 1;

		// Token: 0x040060D1 RID: 24785
		private NKMMoldItemData m_cNKMMoldItemData;

		// Token: 0x040060D2 RID: 24786
		private int m_Index;

		// Token: 0x040060D3 RID: 24787
		public Text m_NKM_UI_FACTORY_CRAFT_POPUP_TOP_TEXT;
	}
}
