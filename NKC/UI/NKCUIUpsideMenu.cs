using System;
using System.Collections.Generic;
using NKC.UI.Guide;
using NKC.UI.Option;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009FF RID: 2559
	public class NKCUIUpsideMenu : MonoBehaviour
	{
		// Token: 0x06006F90 RID: 28560 RVA: 0x0024E14C File Offset: 0x0024C34C
		private NKCUIComItemCount GetUIItemCount(int itemID)
		{
			int itemID2 = itemID;
			if (itemID2 <= 101)
			{
				switch (itemID2)
				{
				case 1:
					return this.m_UICountCredit;
				case 2:
					return this.m_UICountEternium;
				case 3:
					return this.m_UICountInformation;
				case 4:
					break;
				case 5:
					return this.m_UICountPVPPoint;
				default:
					if (itemID2 == 13)
					{
						return this.m_UICountPVPTicket;
					}
					if (itemID2 == 101)
					{
						return this.m_UICountCash;
					}
					break;
				}
			}
			else if (itemID2 <= 1001)
			{
				if (itemID2 == 401)
				{
					return this.m_UICountContractSP;
				}
				if (itemID2 == 1001)
				{
					return this.m_UICountContract;
				}
			}
			else
			{
				if (itemID2 == 1002)
				{
					return this.m_UICountContractInstant;
				}
				if (itemID2 == 1015)
				{
					return this.m_UICountContractShip;
				}
			}
			return this.m_lstUICountEtc.Find((NKCUIComItemCount x) => x.CurrentItemID == itemID);
		}

		// Token: 0x06006F91 RID: 28561 RVA: 0x0024E224 File Offset: 0x0024C424
		public void InitUI()
		{
			base.gameObject.SetActive(false);
			this.SetMoveToShop(this.m_UICountCredit);
			this.SetMoveToShop(this.m_UICountEternium);
			this.SetMoveToShop(this.m_UICountCash);
			this.SetMoveToShop(this.m_UICountInformation);
			this.SetMoveToShop(this.m_UICountDailyTicket);
			this.SetMoveToShop(this.m_UICountContract);
			this.SetMoveToShop(this.m_UICountContractSP);
			this.SetMoveToShop(this.m_UICountContractInstant);
			this.SetMoveToShop(this.m_UICountContractShip);
			this.SetMoveToShop(this.m_UICountContractInstantShip);
			this.SetMoveToShop(this.m_UICountPVPPoint);
			this.SetMoveToShop(this.m_UICountPVPTicket);
			foreach (NKCUIComItemCount moveToShop in this.m_lstUICountEtc)
			{
				this.SetMoveToShop(moveToShop);
			}
			if (this.m_btnChat != null)
			{
				this.m_btnChat.PointerClick.RemoveAllListeners();
				this.m_btnChat.PointerClick.AddListener(new UnityAction(this.OnChatButton));
			}
			if (this.m_cbtnMail != null)
			{
				this.m_cbtnMail.PointerClick.RemoveAllListeners();
				this.m_cbtnMail.PointerClick.AddListener(new UnityAction(this.OnMailButton));
			}
			if (this.m_cbtnOption != null)
			{
				this.m_cbtnOption.PointerClick.RemoveAllListeners();
				this.m_cbtnOption.PointerClick.AddListener(new UnityAction(this.OnOptionButton));
			}
			if (this.m_cBtnHamburger != null)
			{
				this.m_cBtnHamburger.PointerClick.RemoveAllListeners();
				this.m_cBtnHamburger.PointerClick.AddListener(new UnityAction(this.OnHamburgerButton));
			}
			if (this.csbtnGuide != null)
			{
				this.csbtnGuide.PointerClick.RemoveAllListeners();
				this.csbtnGuide.PointerClick.AddListener(new UnityAction(this.OnInformationButton));
			}
			if (this.cbtnHome != null)
			{
				this.cbtnHome.PointerClick.RemoveAllListeners();
				this.cbtnHome.PointerClick.AddListener(new UnityAction(this.OnHomeButton));
			}
			NKCMailManager.dOnMailFlagChange = (NKCMailManager.OnMailFlagChange)Delegate.Combine(NKCMailManager.dOnMailFlagChange, new NKCMailManager.OnMailFlagChange(this.SetNewMail));
			if (this.m_UICountPVPTicket != null)
			{
				this.m_UICountPVPTicket.SetMaxCount((long)NKMPvpCommonConst.Instance.AsyncTicketMaxCount);
				this.m_UICountPVPTicket.SetTimeLabel(NKCUtilString.GET_STRING_UPSIDE_MENU_WAIT_ITEM);
			}
		}

		// Token: 0x06006F92 RID: 28562 RVA: 0x0024E4C4 File Offset: 0x0024C6C4
		public void UpdateTimeContents()
		{
			this.RfreshDailyContents();
		}

		// Token: 0x06006F93 RID: 28563 RVA: 0x0024E4CC File Offset: 0x0024C6CC
		public void RfreshDailyContents()
		{
		}

		// Token: 0x06006F94 RID: 28564 RVA: 0x0024E4CE File Offset: 0x0024C6CE
		public void Update()
		{
			if (this.m_bOpen)
			{
				this.ProcessHotkey();
			}
		}

		// Token: 0x06006F95 RID: 28565 RVA: 0x0024E4E0 File Offset: 0x0024C6E0
		private void ProcessHotkey()
		{
			NKCUIUpsideMenu.eMode topmoseUIUpsidemenuMode = NKCUIManager.GetTopmoseUIUpsidemenuMode();
			if (topmoseUIUpsidemenuMode == NKCUIUpsideMenu.eMode.Disable || topmoseUIUpsidemenuMode - NKCUIUpsideMenu.eMode.ResourceOnly <= 1)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.m_GuideStrID) && this.csbtnGuide.gameObject.activeInHierarchy && NKCInputManager.CheckHotKeyEvent(HotkeyEventType.Help))
			{
				NKCInputManager.ConsumeHotKeyEvent(HotkeyEventType.Help);
				this.OnInformationButton();
			}
			if (this.m_cBtnHamburger.gameObject.activeInHierarchy && NKCInputManager.CheckHotKeyEvent(HotkeyEventType.HamburgerMenu))
			{
				this.OnHamburgerButton();
			}
			if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
			{
				NKCUIComStateButton nkcuicomStateButton = this.csbtnGuide;
				NKCUIComHotkeyDisplay.OpenInstance((nkcuicomStateButton != null) ? nkcuicomStateButton.transform : null, HotkeyEventType.Help);
				NKCUIComButton nkcuicomButton = this.btnBackButton;
				NKCUIComHotkeyDisplay.OpenInstance((nkcuicomButton != null) ? nkcuicomButton.transform : null, HotkeyEventType.Cancel);
				NKCUIComStateButton cBtnHamburger = this.m_cBtnHamburger;
				NKCUIComHotkeyDisplay.OpenInstance((cBtnHamburger != null) ? cBtnHamburger.transform : null, HotkeyEventType.HamburgerMenu);
			}
		}

		// Token: 0x06006F96 RID: 28566 RVA: 0x0024E5A6 File Offset: 0x0024C7A6
		private void SetMoveToShop(NKCUIComItemCount targetButton)
		{
			if (targetButton == null)
			{
				return;
			}
			targetButton.SetOnClickPlusBtn(new NKCUIComItemCount.OnClickPlusBtn(targetButton.OpenMoveToShopPopup));
		}

		// Token: 0x06006F97 RID: 28567 RVA: 0x0024E5C4 File Offset: 0x0024C7C4
		public void Move(bool bOutsideScreen, bool bAnimate)
		{
			if (this.m_rmRectMove != null)
			{
				string name = bOutsideScreen ? "Out" : "In";
				this.m_rmRectMove.Move(name, bAnimate);
			}
		}

		// Token: 0x06006F98 RID: 28568 RVA: 0x0024E5FC File Offset: 0x0024C7FC
		private void OpenWithBackButton(string CurrentMenuName, UnityAction OnBackButton, bool bShowHome = false, bool bShowResource = true, bool bShowPost = true, bool bShowOption = true, bool bShowHamburger = true)
		{
			NKCUtil.SetGameobjectActive(this.objBackMenuRoot, true);
			this.OpenCommonAction(bShowHome, bShowResource, bShowPost, bShowOption, bShowHamburger);
			if (this.lbBackButtonTitleText != null && !string.IsNullOrEmpty(CurrentMenuName))
			{
				this.lbBackButtonTitleText.text = CurrentMenuName;
			}
			if (this.btnBackButton != null)
			{
				this.btnBackButton.PointerClick.RemoveAllListeners();
				if (OnBackButton != null)
				{
					this.btnBackButton.PointerClick.AddListener(OnBackButton);
				}
			}
		}

		// Token: 0x06006F99 RID: 28569 RVA: 0x0024E678 File Offset: 0x0024C878
		public void OnHomeButton()
		{
			NKCUIManager.OnHomeButton();
		}

		// Token: 0x06006F9A RID: 28570 RVA: 0x0024E67F File Offset: 0x0024C87F
		public void OnInformationButton()
		{
			NKCUIPopUpGuide.Instance.Open(this.m_GuideStrID, 0);
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x0024E692 File Offset: 0x0024C892
		public void OnChatButton()
		{
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x0024E694 File Offset: 0x0024C894
		public void OnMailButton()
		{
			if (NKCUIMail.IsInstanceOpen)
			{
				NKCUIMail.Instance.Close();
				return;
			}
			NKCUIMail.Instance.Open();
		}

		// Token: 0x06006F9D RID: 28573 RVA: 0x0024E6B2 File Offset: 0x0024C8B2
		public void OnOptionButton()
		{
			if (NKCUIGameOption.IsInstanceOpen)
			{
				NKCUIGameOption.Instance.Close();
				return;
			}
			NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.NORMAL, null);
		}

		// Token: 0x06006F9E RID: 28574 RVA: 0x0024E6D2 File Offset: 0x0024C8D2
		public void OnHamburgerButton()
		{
			NKCPopupHamburgerMenu.instance.OpenUI();
		}

		// Token: 0x06006F9F RID: 28575 RVA: 0x0024E6DE File Offset: 0x0024C8DE
		private void UpdateEterniumCap()
		{
			if (NKCScenManager.CurrentUserData() != null)
			{
				NKCUtil.SetImageFillAmount(this.m_imgEterniumCap, NKCScenManager.CurrentUserData().GetEterniumCapProgress());
			}
		}

		// Token: 0x06006FA0 RID: 28576 RVA: 0x0024E6FC File Offset: 0x0024C8FC
		public void SetNewMail(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.objNewMail, bValue);
		}

		// Token: 0x06006FA1 RID: 28577 RVA: 0x0024E70A File Offset: 0x0024C90A
		public void SetHamburgerNotify(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objHamburgerNotify, bValue);
		}

		// Token: 0x06006FA2 RID: 28578 RVA: 0x0024E718 File Offset: 0x0024C918
		private void OpenCommonAction(bool bShowHome, bool bShowResource, bool bShowPost, bool bShowOption, bool bShowHamburger)
		{
			NKCUtil.SetGameobjectActive(this.objHome, bShowHome);
			if (this.objResourceRoot != null && this.objResourceRoot.activeSelf == !bShowResource)
			{
				this.objResourceRoot.SetActive(bShowResource);
			}
			bool flag = bShowPost || bShowOption || bShowHamburger;
			if (this.objSubMenuRoot != null && this.objSubMenuRoot.activeSelf == !flag)
			{
				this.objSubMenuRoot.SetActive(flag);
			}
			if (flag)
			{
				NKCUtil.SetGameobjectActive(this.m_cbtnMail, bShowPost);
				NKCUtil.SetGameobjectActive(this.m_cbtnOption, bShowOption);
				NKCUtil.SetGameobjectActive(this.m_cBtnHamburger, bShowHamburger);
			}
		}

		// Token: 0x06006FA3 RID: 28579 RVA: 0x0024E7B8 File Offset: 0x0024C9B8
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.m_lstShowResourceList == null)
			{
				return;
			}
			NKCUIComItemCount uiitemCount = this.GetUIItemCount(itemData.ItemID);
			if (uiitemCount != null)
			{
				uiitemCount.UpdateData(itemData, 0);
			}
			if (itemData.ItemID == 13)
			{
				NKCUIRechargePvpAsyncTicket rechargeAsyncTicket = this.m_RechargeAsyncTicket;
				if (rechargeAsyncTicket != null)
				{
					rechargeAsyncTicket.UpdateData();
				}
			}
			if (itemData.ItemID == 2)
			{
				this.UpdateEterniumCap();
			}
		}

		// Token: 0x06006FA4 RID: 28580 RVA: 0x0024E824 File Offset: 0x0024CA24
		public void RegisterUserdataCallback(NKMUserData newUserData)
		{
			newUserData.m_InventoryData.dOnMiscInventoryUpdate += this.OnInventoryChange;
		}

		// Token: 0x06006FA5 RID: 28581 RVA: 0x0024E840 File Offset: 0x0024CA40
		public void Open(List<int> lstShowResource, NKCUIUpsideMenu.eMode mode, string name, string guideTempletStrID, bool disableSubMenu)
		{
			this.m_bOpen = true;
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_READY || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT)
			{
				mode = NKCUIUpsideMenu.eMode.BackButtonOnly;
			}
			switch (mode)
			{
			case NKCUIUpsideMenu.eMode.Normal:
				this.OpenWithBackButton(name, new UnityAction(this.OnBackButton), true, true, true, true, true);
				this.UpdateResourceUI(NKCScenManager.CurrentUserData(), lstShowResource);
				this.SetHamburgerNotify(NKCAlarmManager.CheckAllNotify(NKCScenManager.CurrentUserData()));
				break;
			case NKCUIUpsideMenu.eMode.BackButtonOnly:
				this.OpenWithBackButton(name, new UnityAction(this.OnBackButton), false, false, false, false, false);
				break;
			case NKCUIUpsideMenu.eMode.LeftsideOnly:
				this.OpenWithBackButton(name, new UnityAction(this.OnBackButton), true, false, false, false, false);
				break;
			case NKCUIUpsideMenu.eMode.LeftsideWithHamburger:
				this.OpenWithBackButton(name, new UnityAction(this.OnBackButton), true, false, false, false, true);
				this.SetHamburgerNotify(NKCAlarmManager.CheckAllNotify(NKCScenManager.CurrentUserData()));
				break;
			}
			if (mode == NKCUIUpsideMenu.eMode.ResourceOnly)
			{
				this.SetParentOfResourceUI(NKCUIManager.eUIBaseRect.UIFrontPopup);
				this.UpdateResourceUI(NKCScenManager.CurrentUserData(), lstShowResource);
			}
			else
			{
				this.ResetParentOfResourceUI();
			}
			this.m_GuideStrID = guideTempletStrID;
			NKCUtil.SetGameobjectActive(this.goRootGuide, !string.IsNullOrEmpty(guideTempletStrID));
			if (disableSubMenu)
			{
				this.cgBackButton.alpha = 0.5f;
				this.cgBackButton.blocksRaycasts = false;
				this.cgSubMenu.alpha = 0.5f;
				this.cgSubMenu.blocksRaycasts = false;
				this.cgResources.blocksRaycasts = false;
			}
			else
			{
				this.cgBackButton.alpha = 1f;
				this.cgBackButton.blocksRaycasts = true;
				this.cgSubMenu.alpha = 1f;
				this.cgSubMenu.blocksRaycasts = true;
				this.cgResources.blocksRaycasts = true;
			}
			this.Move(false, true);
		}

		// Token: 0x06006FA6 RID: 28582 RVA: 0x0024EA11 File Offset: 0x0024CC11
		private void OnBackButton()
		{
			NKCUIManager.OnBackButton();
		}

		// Token: 0x06006FA7 RID: 28583 RVA: 0x0024EA18 File Offset: 0x0024CC18
		private void UpdateResourceUI(NKMUserData userData, List<int> lstShowResource)
		{
			this.m_lstShowResourceList = lstShowResource;
			foreach (NKCUIComItemCount nkcuicomItemCount in this.m_lstUICountEtc)
			{
				nkcuicomItemCount.CleanUp();
				NKCUtil.SetGameobjectActive(nkcuicomItemCount, false);
			}
			NKCUtil.SetGameobjectActive(this.m_UICountCredit, false);
			NKCUtil.SetGameobjectActive(this.m_UICountEternium, false);
			NKCUtil.SetGameobjectActive(this.m_UICountCash, false);
			NKCUtil.SetGameobjectActive(this.m_UICountInformation, false);
			NKCUtil.SetGameobjectActive(this.m_UICountDailyTicket, false);
			NKCUtil.SetGameobjectActive(this.m_UICountContract, false);
			NKCUtil.SetGameobjectActive(this.m_UICountContractSP, false);
			NKCUtil.SetGameobjectActive(this.m_UICountContractInstant, false);
			NKCUtil.SetGameobjectActive(this.m_UICountContractShip, false);
			NKCUtil.SetGameobjectActive(this.m_UICountContractInstantShip, false);
			NKCUtil.SetGameobjectActive(this.m_UICountPVPPoint, false);
			NKCUtil.SetGameobjectActive(this.m_UICountPVPTicket, false);
			int num = 0;
			foreach (int num2 in lstShowResource)
			{
				NKCUIComItemCount nkcuicomItemCount2 = this.GetUIItemCount(num2);
				if (nkcuicomItemCount2 == null)
				{
					if (num < this.m_lstUICountEtc.Count)
					{
						nkcuicomItemCount2 = this.m_lstUICountEtc[num];
						num++;
					}
					else
					{
						Debug.LogError("not enough Upsidemenu Item show ui buffer");
					}
				}
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(num2);
				if (itemMiscTempletByID != null && itemMiscTempletByID.EnableByTag && nkcuicomItemCount2 != null)
				{
					NKCUtil.SetGameobjectActive(nkcuicomItemCount2, true);
					nkcuicomItemCount2.SetData(userData, num2);
					nkcuicomItemCount2.transform.SetAsLastSibling();
				}
			}
			NKCUIRechargePvpAsyncTicket rechargeAsyncTicket = this.m_RechargeAsyncTicket;
			if (rechargeAsyncTicket != null)
			{
				rechargeAsyncTicket.UpdateData();
			}
			this.UpdateEterniumCap();
		}

		// Token: 0x06006FA8 RID: 28584 RVA: 0x0024EBD8 File Offset: 0x0024CDD8
		public void Close()
		{
			if (!this.m_bOpen)
			{
				return;
			}
			this.ResetParentOfResourceUI();
			this.m_bOpen = false;
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06006FA9 RID: 28585 RVA: 0x0024EC09 File Offset: 0x0024CE09
		public void SetParentOfResourceUI(NKCUIManager.eUIBaseRect type)
		{
			this.objResourceRoot.transform.SetParent(NKCUIManager.GetUIBaseRect(type).transform, true);
			this.objResourceRoot.transform.SetAsLastSibling();
			this.m_bIsSetOtherParent = true;
		}

		// Token: 0x06006FAA RID: 28586 RVA: 0x0024EC3E File Offset: 0x0024CE3E
		public void ResetParentOfResourceUI()
		{
			if (this.m_bIsSetOtherParent)
			{
				this.objResourceRoot.transform.SetParent(base.transform, true);
				this.m_bIsSetOtherParent = false;
			}
		}

		// Token: 0x04005B2C RID: 23340
		private bool m_bOpen;

		// Token: 0x04005B2D RID: 23341
		public GameObject objBackMenuRoot;

		// Token: 0x04005B2E RID: 23342
		public GameObject objResourceRoot;

		// Token: 0x04005B2F RID: 23343
		public GameObject objSubMenuRoot;

		// Token: 0x04005B30 RID: 23344
		[Header("BackMenu")]
		public NKCUIComButton btnBackButton;

		// Token: 0x04005B31 RID: 23345
		public Text lbBackButtonTitleText;

		// Token: 0x04005B32 RID: 23346
		[Header("Guide")]
		public GameObject goRootGuide;

		// Token: 0x04005B33 RID: 23347
		public NKCUIComStateButton csbtnGuide;

		// Token: 0x04005B34 RID: 23348
		[Header("Home")]
		public GameObject objHome;

		// Token: 0x04005B35 RID: 23349
		public NKCUIComButton cbtnHome;

		// Token: 0x04005B36 RID: 23350
		[Header("Canvas Groups")]
		public CanvasGroup cgBackButton;

		// Token: 0x04005B37 RID: 23351
		public CanvasGroup cgSubMenu;

		// Token: 0x04005B38 RID: 23352
		public CanvasGroup cgResources;

		// Token: 0x04005B39 RID: 23353
		[Header("Resources")]
		public NKCUIComItemCount m_UICountCredit;

		// Token: 0x04005B3A RID: 23354
		public NKCUIComItemCount m_UICountEternium;

		// Token: 0x04005B3B RID: 23355
		public Image m_imgEterniumCap;

		// Token: 0x04005B3C RID: 23356
		public NKCUIComItemCount m_UICountCash;

		// Token: 0x04005B3D RID: 23357
		public NKCUIComItemCount m_UICountInformation;

		// Token: 0x04005B3E RID: 23358
		public NKCUIComItemCount m_UICountDailyTicket;

		// Token: 0x04005B3F RID: 23359
		public NKCUIComItemCount m_UICountContract;

		// Token: 0x04005B40 RID: 23360
		public NKCUIComItemCount m_UICountContractSP;

		// Token: 0x04005B41 RID: 23361
		public NKCUIComItemCount m_UICountContractInstant;

		// Token: 0x04005B42 RID: 23362
		public NKCUIComItemCount m_UICountContractShip;

		// Token: 0x04005B43 RID: 23363
		public NKCUIComItemCount m_UICountContractInstantShip;

		// Token: 0x04005B44 RID: 23364
		public NKCUIComItemCount m_UICountPVPPoint;

		// Token: 0x04005B45 RID: 23365
		public NKCUIComItemCount m_UICountPVPTicket;

		// Token: 0x04005B46 RID: 23366
		public NKCUIRechargePvpAsyncTicket m_RechargeAsyncTicket;

		// Token: 0x04005B47 RID: 23367
		public List<NKCUIComItemCount> m_lstUICountEtc;

		// Token: 0x04005B48 RID: 23368
		[Header("Rightmost Icons")]
		public NKCUIComStateButton m_btnChat;

		// Token: 0x04005B49 RID: 23369
		public NKCUIComButton m_cbtnMail;

		// Token: 0x04005B4A RID: 23370
		public GameObject objNewMail;

		// Token: 0x04005B4B RID: 23371
		public NKCUIComButton m_cbtnOption;

		// Token: 0x04005B4C RID: 23372
		public NKCUIComStateButton m_cBtnHamburger;

		// Token: 0x04005B4D RID: 23373
		public GameObject m_objHamburgerNotify;

		// Token: 0x04005B4E RID: 23374
		[Header("Etc")]
		public NKCUIRectMove m_rmRectMove;

		// Token: 0x04005B4F RID: 23375
		private List<int> m_lstShowResourceList;

		// Token: 0x04005B50 RID: 23376
		private string m_GuideStrID;

		// Token: 0x04005B51 RID: 23377
		private bool m_bIsSetOtherParent;

		// Token: 0x02001739 RID: 5945
		[Serializable]
		public struct ResourceUI
		{
			// Token: 0x0400A652 RID: 42578
			public GameObject objRoot;

			// Token: 0x0400A653 RID: 42579
			public Image imgIcon;

			// Token: 0x0400A654 RID: 42580
			public Text lbCount;
		}

		// Token: 0x0200173A RID: 5946
		public enum eMode
		{
			// Token: 0x0400A656 RID: 42582
			Disable,
			// Token: 0x0400A657 RID: 42583
			Normal,
			// Token: 0x0400A658 RID: 42584
			BackButtonOnly,
			// Token: 0x0400A659 RID: 42585
			LeftsideOnly,
			// Token: 0x0400A65A RID: 42586
			LeftsideWithHamburger,
			// Token: 0x0400A65B RID: 42587
			ResourceOnly,
			// Token: 0x0400A65C RID: 42588
			Invalid
		}
	}
}
