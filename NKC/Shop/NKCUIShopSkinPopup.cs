using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Event;
using NKM;
using NKM.Shop;
using NKM.Templet;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ADC RID: 2780
	public class NKCUIShopSkinPopup : NKCUIBase
	{
		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06007CC5 RID: 31941 RVA: 0x0029C200 File Offset: 0x0029A400
		public static NKCUIShopSkinPopup Instance
		{
			get
			{
				if (NKCUIShopSkinPopup.m_Instance == null)
				{
					NKCUIShopSkinPopup.m_Instance = NKCUIManager.OpenNewInstance<NKCUIShopSkinPopup>("ab_ui_nkm_ui_shop_skin", "NKM_UI_SHOP_SKIN", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIShopSkinPopup.CleanupInstance)).GetInstance<NKCUIShopSkinPopup>();
					NKCUIShopSkinPopup.m_Instance.Init();
				}
				return NKCUIShopSkinPopup.m_Instance;
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06007CC6 RID: 31942 RVA: 0x0029C24F File Offset: 0x0029A44F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIShopSkinPopup.m_Instance != null && NKCUIShopSkinPopup.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007CC7 RID: 31943 RVA: 0x0029C26A File Offset: 0x0029A46A
		public static void CheckInstanceAndClose()
		{
			if (NKCUIShopSkinPopup.m_Instance != null && NKCUIShopSkinPopup.m_Instance.IsOpen)
			{
				NKCUIShopSkinPopup.m_Instance.Close();
			}
		}

		// Token: 0x06007CC8 RID: 31944 RVA: 0x0029C28F File Offset: 0x0029A48F
		private static void CleanupInstance()
		{
			NKCUIShopSkinPopup.m_Instance = null;
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06007CC9 RID: 31945 RVA: 0x0029C297 File Offset: 0x0029A497
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06007CCA RID: 31946 RVA: 0x0029C29A File Offset: 0x0029A49A
		public override bool WillCloseUnderPopupOnOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06007CCB RID: 31947 RVA: 0x0029C29D File Offset: 0x0029A49D
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_SHOP_SKIN_INFO;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06007CCC RID: 31948 RVA: 0x0029C2A4 File Offset: 0x0029A4A4
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_lstUpsideMenuShowResource != null && this.m_lstUpsideMenuShowResource.Count > 0)
				{
					return this.m_lstUpsideMenuShowResource;
				}
				return this.DEFAULT_RESOURCE_LIST;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06007CCD RID: 31949 RVA: 0x0029C2C9 File Offset: 0x0029A4C9
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				if (!this.m_bShowUpsideMenu)
				{
					return NKCUIUpsideMenu.eMode.BackButtonOnly;
				}
				return base.eUpsideMenuMode;
			}
		}

		// Token: 0x06007CCE RID: 31950 RVA: 0x0029C2DB File Offset: 0x0029A4DB
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			this.CleanUp();
		}

		// Token: 0x06007CCF RID: 31951 RVA: 0x0029C2EF File Offset: 0x0029A4EF
		public override void Hide()
		{
			base.Hide();
			if (this.m_NKCASUIUnitCutinIllust != null)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCASUIUnitCutinIllust.GetRectTransform().gameObject, false);
			}
		}

		// Token: 0x06007CD0 RID: 31952 RVA: 0x0029C318 File Offset: 0x0029A518
		internal void Init()
		{
			this.m_CharacterView.Init(null, null);
			this.m_cbtnIllustViewMode.PointerClick.RemoveAllListeners();
			this.m_cbtnIllustViewMode.PointerClick.AddListener(new UnityAction(this.OnBtnIllust));
			this.m_cbtnShopBuy.PointerClick.RemoveAllListeners();
			this.m_cbtnShopBuy.PointerClick.AddListener(new UnityAction(this.OnBtnShopBuy));
			NKCUtil.SetButtonClickDelegate(this.m_cbtnSkinInfoTry, new UnityAction(this.OnBtnTryButton));
			NKCUtil.SetButtonClickDelegate(this.m_cbtnSKinInfoClose, new UnityAction(base.Close));
			this.m_cbtnShopTry.PointerClick.RemoveAllListeners();
			this.m_cbtnShopTry.PointerClick.AddListener(new UnityAction(this.OnBtnTryButton));
			this.m_cbtnUnitInfoEquip.PointerClick.RemoveAllListeners();
			this.m_cbtnUnitInfoEquip.PointerClick.AddListener(new UnityAction(this.OnBtnUnitInfoEquip));
			this.m_cbtnUnitInfoBuy.PointerClick.RemoveAllListeners();
			this.m_cbtnUnitInfoBuy.PointerClick.AddListener(new UnityAction(this.OnBtnUnitInfoBuy));
			this.m_sbtnIngameSD.PointerClick.RemoveAllListeners();
			this.m_sbtnIngameSD.PointerClick.AddListener(new UnityAction(this.OnBtnGameUnit));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnComponentVoice, new UnityAction(this.OnUnitVoice));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnComponentCutin, new UnityAction(this.OnUnitCutin));
			this.SetBindSkinFunction(false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnLoginAnim, new UnityAction(this.OnClickLoginAnim));
		}

		// Token: 0x06007CD1 RID: 31953 RVA: 0x0029C4B4 File Offset: 0x0029A6B4
		private void SetBindSkinFunction(bool bActive = false)
		{
			if (bActive)
			{
				NKCUtil.SetBindFunction(this.m_csbtnComponentStory, new UnityAction(this.OnClickStoryPlayable));
				return;
			}
			NKCUtil.SetBindFunction(this.m_csbtnComponentStory, new UnityAction(this.OnClickStoryNotOwned));
		}

		// Token: 0x06007CD2 RID: 31954 RVA: 0x0029C4E8 File Offset: 0x0029A6E8
		private void OnClickStoryPlayable()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SKIN_STORY_REPLAY_CONFIRM, delegate()
			{
				this.PlayCutScene(this.m_SelectedSkinID);
			}, null, false);
		}

		// Token: 0x06007CD3 RID: 31955 RVA: 0x0029C507 File Offset: 0x0029A707
		private void OnClickStoryNotOwned()
		{
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_SHOP_SKIN_STORY_MSG, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x06007CD4 RID: 31956 RVA: 0x0029C51C File Offset: 0x0029A71C
		private void OnClickLoginAnim()
		{
			if (!NKCScenManager.CurrentUserData().m_InventoryData.HasItemSkin(this.m_SelectedSkinID))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_SHOP_SKIN_LOGIN_CUTIN_MSG, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_SelectedSkinID);
			if (skinTemplet == null)
			{
				return;
			}
			if (!skinTemplet.HasLoginCutin)
			{
				return;
			}
			NKCUIEventSequence.PlaySkinCutin(skinTemplet, null);
		}

		// Token: 0x06007CD5 RID: 31957 RVA: 0x0029C574 File Offset: 0x0029A774
		private void PlayCutScene(int skinID)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
			if (skinTemplet != null && !string.IsNullOrEmpty(skinTemplet.m_CutscenePurchase))
			{
				NKCUICutScenPlayer.Instance.LoadAndPlay(skinTemplet.m_CutscenePurchase, 0, null, true);
			}
		}

		// Token: 0x06007CD6 RID: 31958 RVA: 0x0029C5AC File Offset: 0x0029A7AC
		public void CleanUp()
		{
			this.m_CharacterView.CleanUp();
			this.m_InGameUnitViewer.CleanUp();
			if (this.m_spineSD != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
				this.m_spineSD = null;
			}
			this.m_SelectedSkinID = 0;
			this.m_SelectedUnitID = 0;
			this.m_SelectedUnitUID = 0L;
			this.m_ProductID = 0;
			this.m_bUseUpdate = false;
			this.m_lBuyTimeLimit = 0L;
			this.m_tEndDateDiscountTime = DateTime.MinValue;
			if (this.m_NKCASUIUnitCutinIllust != null)
			{
				this.m_NKCASUIUnitCutinIllust.Unload();
				this.m_NKCASUIUnitCutinIllust = null;
			}
			NKCUIPopupIllustView.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().m_NKCMemoryCleaner.UnloadObjectPool();
		}

		// Token: 0x06007CD7 RID: 31959 RVA: 0x0029C658 File Offset: 0x0029A858
		private void OnBtnShopBuy()
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(this.m_ProductID);
			if (shopItemTemplet == null)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_SKIN_LOCK, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCPopupShopBuyConfirm.Instance.Open(shopItemTemplet, new NKCUIShop.OnProductBuyDelegate(NKCShopManager.TryProductBuy));
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x0029C6A0 File Offset: 0x0029A8A0
		private void OnBtnIllust()
		{
			if (this.m_SelectedSkinID == 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_SelectedUnitID);
				NKCUIPopupIllustView.Instance.Open(unitTempletBase);
				return;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_SelectedSkinID);
			NKCUIPopupIllustView.Instance.Open(skinTemplet);
		}

		// Token: 0x06007CD9 RID: 31961 RVA: 0x0029C6E4 File Offset: 0x0029A8E4
		private void OnBtnGameUnit()
		{
			this.ToggleGameUnit(!this.m_bGameUnitView);
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x0029C6F5 File Offset: 0x0029A8F5
		private void ToggleGameUnit(bool value)
		{
			this.m_bGameUnitView = value;
			NKCUtil.SetGameobjectActive(this.m_InGameUnitViewer, this.m_bGameUnitView);
			NKCUtil.SetGameobjectActive(this.m_rtSDRoot, !this.m_bGameUnitView);
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x0029C724 File Offset: 0x0029A924
		public void OpenForShop(ShopItemTemplet shopTemplet)
		{
			if (shopTemplet == null)
			{
				return;
			}
			long skinLimitedBuy = shopTemplet.HasDateLimit ? shopTemplet.EventDateEndUtc.Ticks : 0L;
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(shopTemplet.m_ItemID);
			int priceItemID = shopTemplet.m_PriceItemID;
			int realPrice = NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(shopTemplet, 1, false);
			int price = shopTemplet.m_Price;
			this.m_ProductID = shopTemplet.m_ProductID;
			if (skinTemplet == null)
			{
				return;
			}
			if (NKMUnitManager.GetUnitTempletBase(skinTemplet.m_SkinEquipUnitID) == null)
			{
				return;
			}
			this.UpdateUpsideMenuResource(skinTemplet.m_SkinID);
			this.m_bUseUpdate = false;
			this.m_eMode = NKCUIShopSkinPopup.Mode.ForShop;
			this.m_bShowUpsideMenu = true;
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinList, false);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinTitle, true);
			this.SetSkinTitleData(skinTemplet);
			this.SetEquippableUnit(skinTemplet);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinDesc, true);
			this.SetSkinDesc(skinTemplet);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinUnitInfoBuy, false);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinInfoOnly, false);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinShopBuy, true);
			this.SetSkinShopBuyButtons(skinTemplet.m_SkinID, priceItemID, realPrice, price);
			this.SetSkinCommon();
			this.SetSkinLimitedBuy(skinLimitedBuy);
			bool flag = false;
			if (shopTemplet != null)
			{
				if (shopTemplet.m_DiscountRate > 0f && NKCSynchronizedTime.IsEventTime(shopTemplet.discountIntervalId, shopTemplet.DiscountStartDateUtc, shopTemplet.DiscountEndDateUtc) && shopTemplet.DiscountEndDateUtc != DateTime.MaxValue)
				{
					this.m_bUseUpdate = true;
					flag = true;
					this.m_tEndDateDiscountTime = shopTemplet.DiscountEndDateUtc;
					this.UpdateDiscountTime(this.m_tEndDateDiscountTime);
				}
				else
				{
					this.m_tEndDateDiscountTime = DateTime.MinValue;
				}
				if (!shopTemplet.HasDiscountDateLimit)
				{
					NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopTemplet.m_DiscountRate > 0f);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopTemplet.m_DiscountRate > 0f && flag);
				}
				NKCUtil.SetLabelText(this.m_txtDiscountRate, string.Format("-{0}%", (int)shopTemplet.m_DiscountRate));
			}
			else
			{
				this.m_tEndDateDiscountTime = DateTime.MinValue;
			}
			this.SetShowDiscountTime(flag);
			this.m_SelectedSkinID = skinTemplet.m_SkinID;
			this.m_SelectedUnitID = skinTemplet.m_SkinEquipUnitID;
			this.m_SelectedUnitUID = 0L;
			this.SelectSkin(skinTemplet.m_SkinID);
			this.SetViewMode(NKCUICharacterView.eMode.Normal, false);
			base.UIOpened(true);
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x0029C958 File Offset: 0x0029AB58
		public void OpenForSkinInfo(NKMSkinTemplet skinTemplet, int productID)
		{
			if (skinTemplet == null)
			{
				return;
			}
			if (NKMUnitManager.GetUnitTempletBase(skinTemplet.m_SkinEquipUnitID) == null)
			{
				return;
			}
			this.UpdateUpsideMenuResource(skinTemplet.m_SkinID);
			this.m_bUseUpdate = false;
			this.m_eMode = NKCUIShopSkinPopup.Mode.ForSkinInfo;
			this.m_bShowUpsideMenu = true;
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinList, false);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinTitle, true);
			this.SetSkinTitleData(skinTemplet);
			this.SetEquippableUnit(skinTemplet);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinDesc, true);
			this.SetSkinDesc(skinTemplet);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinUnitInfoBuy, false);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinShopBuy, false);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinInfoOnly, true);
			this.SetSkinCommon();
			this.SetSkinLimitedBuy(0L);
			this.m_tEndDateDiscountTime = DateTime.MinValue;
			this.SetShowDiscountTime(false);
			this.m_ProductID = productID;
			this.m_SelectedSkinID = skinTemplet.m_SkinID;
			this.m_SelectedUnitID = skinTemplet.m_SkinEquipUnitID;
			this.m_SelectedUnitUID = 0L;
			this.SelectSkin(skinTemplet.m_SkinID);
			this.SetViewMode(NKCUICharacterView.eMode.Normal, false);
			base.UIOpened(true);
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x0029CA58 File Offset: 0x0029AC58
		private void UpdateUpsideMenuResource(int targetSkinID)
		{
			this.m_lstUpsideMenuShowResource.Clear();
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(this.m_ProductID);
			if (shopItemTemplet != null)
			{
				ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(shopItemTemplet.m_TabID, shopItemTemplet.m_TabSubIndex);
				if (shopTabTemplet != null)
				{
					this.AddUpsideMenuResource(shopTabTemplet.m_ResourceTypeID_1);
					this.AddUpsideMenuResource(shopTabTemplet.m_ResourceTypeID_2);
					this.AddUpsideMenuResource(shopTabTemplet.m_ResourceTypeID_3);
					this.AddUpsideMenuResource(shopTabTemplet.m_ResourceTypeID_4);
					this.AddUpsideMenuResource(shopTabTemplet.m_ResourceTypeID_5);
				}
			}
		}

		// Token: 0x06007CDE RID: 31966 RVA: 0x0029CAD0 File Offset: 0x0029ACD0
		private void AddUpsideMenuResource(int resourceID)
		{
			if (resourceID > 0 && !this.m_lstUpsideMenuShowResource.Contains(resourceID))
			{
				this.m_lstUpsideMenuShowResource.Add(resourceID);
			}
		}

		// Token: 0x06007CDF RID: 31967 RVA: 0x0029CAF0 File Offset: 0x0029ACF0
		public void OpenForUnitInfo(NKMUnitData unitData, bool bShowUpsideMenu = true)
		{
			if (unitData == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase == null)
			{
				return;
			}
			this.m_eMode = NKCUIShopSkinPopup.Mode.ForUnitInfo;
			this.m_bShowUpsideMenu = bShowUpsideMenu;
			this.m_SelectedSkinID = unitData.m_SkinID;
			this.m_SelectedUnitID = unitData.m_UnitID;
			this.m_SelectedUnitUID = unitData.m_UnitUID;
			ShopItemTemplet shopTempletBySkinID = NKCShopManager.GetShopTempletBySkinID(this.m_SelectedSkinID);
			this.m_ProductID = ((shopTempletBySkinID != null) ? shopTempletBySkinID.m_ProductID : 0);
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitData);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinList, true);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinTitle, true);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinDesc, true);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinUnitInfoBuy, true);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinInfoOnly, false);
			NKCUtil.SetGameobjectActive(this.m_objMenuSkinShopBuy, false);
			NKCUtil.SetGameobjectActive(this.m_objDiscountDay, false);
			NKCUtil.SetGameobjectActive(this.m_objDiscountRate, false);
			NKCUtil.SetGameobjectActive(this.m_objEquippableUnit, false);
			this.SetSkinListData(unitData);
			if (skinTemplet != null)
			{
				this.SetSkinTitleData(skinTemplet);
				this.SetSkinDesc(skinTemplet);
			}
			else
			{
				this.SetSkinTitleData(unitTempletBase);
				this.SetSkinDesc(unitTempletBase);
			}
			this.SetSkinCommon();
			this.SelectSkin(unitData.m_SkinID);
			this.SetViewMode(NKCUICharacterView.eMode.Normal, false);
			base.UIOpened(true);
		}

		// Token: 0x06007CE0 RID: 31968 RVA: 0x0029CC18 File Offset: 0x0029AE18
		private void SetSkinCommon()
		{
			this.m_InGameUnitViewer.Prepare(null);
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x0029CC28 File Offset: 0x0029AE28
		private void SetSkinListData(NKMUnitData unitData)
		{
			if (NKMSkinManager.IsCharacterHasSkin(unitData.m_UnitID))
			{
				NKCUtil.SetGameobjectActive(this.m_objNoSkin, false);
				NKCUtil.SetGameobjectActive(this.m_srScrollRect, true);
				List<NKMSkinTemplet> skinlistForCharacter = NKMSkinManager.GetSkinlistForCharacter(unitData.m_UnitID, NKCScenManager.CurrentUserData().m_InventoryData);
				this.SetSkinList(unitData, skinlistForCharacter);
				return;
			}
			this.SetDefaultSkin(unitData);
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x0029CC80 File Offset: 0x0029AE80
		private void SetEquippableUnit(NKMSkinTemplet skinTemplet)
		{
			if (skinTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objEquippableUnit, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEquippableUnit, true);
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(skinTemplet.m_SkinEquipUnitID);
			NKCUtil.SetGameobjectActive(this.m_objEquppableUnitNotOwned, !NKCScenManager.CurrentUserData().m_ArmyData.HaveUnit(skinTemplet.m_SkinEquipUnitID, true));
			NKCUtil.SetImageSprite(this.m_imgEquippableUnit, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet.m_SkinEquipUnitID, 0), false);
			NKCUtil.SetLabelText(this.m_lbEquippableUnitName, nkmunitTempletBase.GetUnitName());
			NKCUtil.SetLabelText(this.m_lbEquippableUnitTitle, nkmunitTempletBase.GetUnitTitle());
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x0029CD14 File Offset: 0x0029AF14
		private void SetSkinComponents(NKMSkinTemplet skinTemplet)
		{
			NKCUtil.SetGameobjectActive(this.m_csbtnComponentVoice, skinTemplet.ChangesVoice());
			NKCUtil.SetGameobjectActive(this.m_csbtnComponentCutin, skinTemplet.ChangesHyperCutin());
			NKCUtil.SetGameobjectActive(this.m_objComponentCutinEffect, skinTemplet.m_SkinSkillCutIn == NKMSkinTemplet.SKIN_CUTIN.CUTIN_PRIVATE);
			NKCUtil.SetGameobjectActive(this.m_csbtnComponentStory, !string.IsNullOrEmpty(skinTemplet.m_CutscenePurchase));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHOP_SKIN_POPUP_INFO_COMPONENT_CONTENT_EFFECT, skinTemplet.m_bEffect);
			NKCUtil.SetGameobjectActive(this.m_objComponentConversion, skinTemplet.m_Conversion);
			NKCUtil.SetGameobjectActive(this.m_objComponentLobbyFace, skinTemplet.m_LobbyFace);
			NKCUtil.SetGameobjectActive(this.m_objComponentCollab, skinTemplet.m_Collabo);
			NKCUtil.SetGameobjectActive(this.m_objComponentGauntlet, skinTemplet.m_Gauntlet);
			NKCUtil.SetGameobjectActive(this.m_objComponentLoginAnim, skinTemplet.HasLoginCutin);
			if (skinTemplet.m_SkinSkillCutIn == NKMSkinTemplet.SKIN_CUTIN.CUTIN_PRIVATE)
			{
				NKCUtil.SetImageSprite(this.m_imgComponentCutin, NKCUtil.GetShopSprite("NKM_UI_SHOP_SKIN_ICON_CUTIN_SPECIAL"), false);
				NKCUtil.SetImageColor(this.m_imgComponentCutin, this.CUTIN_PRIVATE_COLOR);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgComponentCutin, NKCUtil.GetShopSprite("NKM_UI_SHOP_SKIN_ICON_CUTIN"), false);
			NKCUtil.SetImageColor(this.m_imgComponentCutin, Color.white);
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x0029CE30 File Offset: 0x0029B030
		private void SetSkinTitleData(NKMUnitTempletBase unitTempletBase)
		{
			NKCUtil.SetGameobjectActive(this.m_objLimited, false);
			NKCUtil.SetLabelText(this.m_lbSkinName, unitTempletBase.GetUnitTitle());
			NKCUtil.SetLabelTextColor(this.m_lbSkinGrade, NKCUtil.GetColorForGrade(NKMSkinTemplet.SKIN_GRADE.SG_VARIATION));
			NKCUtil.SetImageSprite(this.m_imgSkinGradeLine, this.m_spGradeLineN, false);
			NKCUtil.SetLabelText(this.m_lbSkinGrade, NKCUtilString.GET_STRING_BASE_SKIN);
		}

		// Token: 0x06007CE5 RID: 31973 RVA: 0x0029CE90 File Offset: 0x0029B090
		private void SetSkinTitleData(NKMSkinTemplet skinTemplet)
		{
			NKCUtil.SetGameobjectActive(this.m_objLimited, skinTemplet.m_bLimited);
			NKCUtil.SetLabelText(this.m_lbSkinName, skinTemplet.GetTitle());
			NKCUtil.SetLabelTextColor(this.m_lbSkinGrade, NKCUtil.GetColorForGrade(skinTemplet.m_SkinGrade));
			NKCUtil.SetLabelText(this.m_lbSkinGrade, NKCUtil.GetStringForGrade(skinTemplet.m_SkinGrade));
			switch (skinTemplet.m_SkinGrade)
			{
			case NKMSkinTemplet.SKIN_GRADE.SG_VARIATION:
				NKCUtil.SetImageSprite(this.m_imgSkinGradeLine, this.m_spGradeLineN, false);
				return;
			case NKMSkinTemplet.SKIN_GRADE.SG_NORMAL:
				NKCUtil.SetImageSprite(this.m_imgSkinGradeLine, this.m_spGradeLineR, false);
				return;
			case NKMSkinTemplet.SKIN_GRADE.SG_RARE:
				NKCUtil.SetImageSprite(this.m_imgSkinGradeLine, this.m_spGradeLineSR, false);
				return;
			case NKMSkinTemplet.SKIN_GRADE.SG_PREMIUM:
				NKCUtil.SetImageSprite(this.m_imgSkinGradeLine, this.m_spGradeLineSSR, false);
				return;
			case NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL:
				NKCUtil.SetImageSprite(this.m_imgSkinGradeLine, this.m_spGradeLineSpecial, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06007CE6 RID: 31974 RVA: 0x0029CF6B File Offset: 0x0029B16B
		private void SetSkinDesc(NKMSkinTemplet skinTemplet)
		{
			NKCUtil.SetLabelText(this.m_lbDesc, skinTemplet.GetSkinDesc());
		}

		// Token: 0x06007CE7 RID: 31975 RVA: 0x0029CF80 File Offset: 0x0029B180
		private void SetSkinDesc(NKMUnitTempletBase unitTempletBase)
		{
			NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(this.m_SelectedUnitID);
			if (unitTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbDesc, unitTemplet.GetUnitIntro());
				return;
			}
			NKCUtil.SetLabelText(this.m_lbDesc, NKCUtilString.GET_STRING_BASE_SKIN);
		}

		// Token: 0x06007CE8 RID: 31976 RVA: 0x0029CFC0 File Offset: 0x0029B1C0
		private void SetSkinUnitInfoBuyButtons(bool skinOwned, bool equipped)
		{
			NKCUtil.SetGameobjectActive(this.m_cbtnUnitInfoEquip, skinOwned);
			NKCUtil.SetGameobjectActive(this.m_cbtnUnitInfoBuy, !skinOwned);
			if (skinOwned)
			{
				if (equipped)
				{
					NKCUIComButton cbtnUnitInfoEquip = this.m_cbtnUnitInfoEquip;
					if (cbtnUnitInfoEquip == null)
					{
						return;
					}
					cbtnUnitInfoEquip.Lock();
					return;
				}
				else
				{
					NKCUIComButton cbtnUnitInfoEquip2 = this.m_cbtnUnitInfoEquip;
					if (cbtnUnitInfoEquip2 == null)
					{
						return;
					}
					cbtnUnitInfoEquip2.UnLock();
					return;
				}
			}
			else
			{
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(this.m_ProductID);
				if (shopItemTemplet != null)
				{
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					bool flag;
					long num;
					if (NKCShopManager.CanBuyFixShop(myUserData, shopItemTemplet, out flag, out num, true) == NKM_ERROR_CODE.NEC_OK)
					{
						NKCUIComButton cbtnUnitInfoBuy = this.m_cbtnUnitInfoBuy;
						if (cbtnUnitInfoBuy == null)
						{
							return;
						}
						cbtnUnitInfoBuy.UnLock();
						return;
					}
					else
					{
						NKCUIComButton cbtnUnitInfoBuy2 = this.m_cbtnUnitInfoBuy;
						if (cbtnUnitInfoBuy2 == null)
						{
							return;
						}
						cbtnUnitInfoBuy2.Lock();
						return;
					}
				}
				else
				{
					NKCUIComButton cbtnUnitInfoBuy3 = this.m_cbtnUnitInfoBuy;
					if (cbtnUnitInfoBuy3 == null)
					{
						return;
					}
					cbtnUnitInfoBuy3.Lock();
					return;
				}
			}
		}

		// Token: 0x06007CE9 RID: 31977 RVA: 0x0029D06C File Offset: 0x0029B26C
		private void SetSkinShopBuyButtons(int skinID, int priceItemID, int price, int oldPrice)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(this.m_ProductID);
			if (shopItemTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, false);
				return;
			}
			if (!shopItemTemplet.HasDiscountDateLimit)
			{
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopItemTemplet.m_DiscountRate > 0f);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopItemTemplet.m_DiscountRate > 0f && NKCSynchronizedTime.IsEventTime(shopItemTemplet.discountIntervalId, shopItemTemplet.DiscountStartDateUtc, shopItemTemplet.DiscountEndDateUtc));
			}
			this.m_PriceTag.SetData(priceItemID, price, false, false, true);
			if (oldPrice > price)
			{
				NKCResourceUtility.GetOrLoadMiscItemSmallIcon(priceItemID);
				NKCUtil.SetLabelText(this.m_lbOldPrice, oldPrice.ToString());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, false);
		}

		// Token: 0x06007CEA RID: 31978 RVA: 0x0029D134 File Offset: 0x0029B334
		private void SetSkinLimitedBuy(long buyEndTick)
		{
			this.m_lBuyTimeLimit = buyEndTick;
			if (buyEndTick > 0L)
			{
				this.m_bUseUpdate = true;
				NKCUtil.SetGameobjectActive(this.m_objLimitedTime, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objLimitedTime, false);
		}

		// Token: 0x06007CEB RID: 31979 RVA: 0x0029D162 File Offset: 0x0029B362
		public void SetShowDiscountTime(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objDiscountDay, bValue);
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x0029D170 File Offset: 0x0029B370
		public void UpdateDiscountTime(DateTime endTime)
		{
			string msg;
			if (NKCSynchronizedTime.IsFinished(endTime))
			{
				msg = NKCUtilString.GET_STRING_QUIT;
			}
			else
			{
				msg = NKCUtilString.GetRemainTimeStringOneParam(endTime);
			}
			if (this.m_eMode == NKCUIShopSkinPopup.Mode.ForShop)
			{
				NKCUtil.SetLabelText(this.m_txtDiscountDay, msg);
				return;
			}
			if (this.m_eMode == NKCUIShopSkinPopup.Mode.ForUnitInfo)
			{
				NKCUtil.SetLabelText(this.m_txtDiscountDayForUnitInfo, msg);
			}
		}

		// Token: 0x06007CED RID: 31981 RVA: 0x0029D1BE File Offset: 0x0029B3BE
		private void UpdateBuyTime()
		{
			NKCUtil.SetLabelText(this.m_lbLimitedTime, NKCSynchronizedTime.GetTimeLeftString(this.m_lBuyTimeLimit));
		}

		// Token: 0x06007CEE RID: 31982 RVA: 0x0029D1D8 File Offset: 0x0029B3D8
		public void Update()
		{
			if (this.m_bUseUpdate)
			{
				this.m_fUpdateGap -= Time.unscaledDeltaTime;
				if (this.m_fUpdateGap <= 0f)
				{
					if (this.m_lBuyTimeLimit > 0L)
					{
						this.UpdateBuyTime();
					}
					if (this.m_tEndDateDiscountTime != DateTime.MinValue)
					{
						this.UpdateDiscountTime(this.m_tEndDateDiscountTime);
					}
					this.m_fUpdateGap = 1f;
				}
			}
		}

		// Token: 0x06007CEF RID: 31983 RVA: 0x0029D248 File Offset: 0x0029B448
		private void SelectSkin(int skinID)
		{
			this.m_SelectedSkinID = skinID;
			foreach (NKCUISkinSlot nkcuiskinSlot in this.m_lstSlot)
			{
				nkcuiskinSlot.SetSelected(nkcuiskinSlot.SkinID == skinID);
			}
			if (this.m_eMode == NKCUIShopSkinPopup.Mode.ForUnitInfo)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(this.m_SelectedUnitUID);
				bool equipped = unitFromUID != null && unitFromUID.m_SkinID == skinID;
				bool flag = myUserData.m_InventoryData.HasItemSkin(skinID);
				ShopItemTemplet shopTempletBySkinID = NKCShopManager.GetShopTempletBySkinID(skinID);
				this.m_ProductID = ((shopTempletBySkinID != null) ? shopTempletBySkinID.m_ProductID : 9);
				this.SetSkinUnitInfoBuyButtons(flag, equipped);
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
				if (skinTemplet != null)
				{
					bool bValue = !string.IsNullOrEmpty(skinTemplet.m_CutscenePurchase);
					NKCUtil.SetGameobjectActive(this.m_csbtnComponentStory, bValue);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnComponentStory, false);
				}
				this.SetBindSkinFunction(flag);
				if (!flag)
				{
					if (shopTempletBySkinID != null)
					{
						bool flag2 = false;
						if (shopTempletBySkinID.m_DiscountRate > 0f && NKCSynchronizedTime.IsEventTime(shopTempletBySkinID.discountIntervalId, shopTempletBySkinID.DiscountStartDateUtc, shopTempletBySkinID.DiscountEndDateUtc) && shopTempletBySkinID.DiscountEndDateUtc != DateTime.MaxValue)
						{
							flag2 = true;
							this.m_tEndDateDiscountTime = shopTempletBySkinID.DiscountEndDateUtc;
							this.UpdateDiscountTime(this.m_tEndDateDiscountTime);
						}
						else
						{
							this.m_tEndDateDiscountTime = DateTime.MinValue;
						}
						NKCUtil.SetGameobjectActive(this.m_objDiscountDayForUnitInfo, flag2);
						if (!shopTempletBySkinID.HasDiscountDateLimit)
						{
							NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopTempletBySkinID.m_DiscountRate > 0f);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopTempletBySkinID.m_DiscountRate > 0f && flag2);
						}
						NKCUtil.SetLabelText(this.m_txtDiscountRate, string.Format("-{0}%", (int)shopTempletBySkinID.m_DiscountRate));
						bool flag3 = false;
						if (shopTempletBySkinID.HasDateLimit && NKCSynchronizedTime.IsEventTime(shopTempletBySkinID.eventIntervalId, shopTempletBySkinID.EventDateStartUtc, shopTempletBySkinID.EventDateEndUtc))
						{
							flag3 = true;
							this.m_lBuyTimeLimit = shopTempletBySkinID.EventDateEndUtc.Ticks;
							this.UpdateBuyTime();
						}
						else
						{
							this.m_lBuyTimeLimit = 0L;
						}
						NKCUtil.SetGameobjectActive(this.m_objLimited, flag3);
						this.SetSkinLimitedBuy(this.m_lBuyTimeLimit);
						this.m_bUseUpdate = (flag2 || flag3);
					}
					else
					{
						this.m_bUseUpdate = false;
						NKCUtil.SetGameobjectActive(this.m_objDiscountDayForUnitInfo, false);
						NKCUtil.SetGameobjectActive(this.m_objDiscountRate, false);
						NKCUtil.SetGameobjectActive(this.m_objLimitedTime, false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objLimitedTime, false);
					NKCUtil.SetGameobjectActive(this.m_objDiscountDay, false);
					NKCUtil.SetGameobjectActive(this.m_objDiscountRate, false);
				}
			}
			else
			{
				this.SetBindSkinFunction(false);
			}
			if (skinID == 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_SelectedUnitID);
				if (unitTempletBase != null)
				{
					this.SetSkinTitleData(unitTempletBase);
					NKCUtil.SetGameobjectActive(this.m_objMenuSkinComponents, false);
					this.SetSkinDesc(unitTempletBase);
				}
				this.m_CharacterView.SetCharacterIllust(unitTempletBase, 0, false, true, 0);
				this.OpenSDIllust(unitTempletBase);
			}
			else
			{
				NKMSkinTemplet skinTemplet2 = NKMSkinManager.GetSkinTemplet(skinID);
				if (skinTemplet2 != null)
				{
					this.SetSkinTitleData(skinTemplet2);
					NKCUtil.SetGameobjectActive(this.m_objMenuSkinComponents, true);
					this.SetSkinComponents(skinTemplet2);
					this.SetSkinDesc(skinTemplet2);
				}
				this.m_CharacterView.SetCharacterIllust(skinTemplet2, false, true, 0);
				this.OpenSDIllust(skinTemplet2);
			}
			this.m_InGameUnitViewer.SetPreviewBattleUnit(this.m_SelectedUnitID, skinID);
			this.ToggleGameUnit(this.m_bGameUnitView);
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x0029D5B8 File Offset: 0x0029B7B8
		private void SetViewMode(NKCUICharacterView.eMode mode, bool bAnimate = true)
		{
			this.m_eCurrentViewMode = mode;
			this.m_CharacterView.SetMode(mode, bAnimate);
			this.m_rtRightUI.DOComplete(false);
			if (mode != NKCUICharacterView.eMode.Normal)
			{
				if (mode != NKCUICharacterView.eMode.CharacterView)
				{
					return;
				}
				if (bAnimate)
				{
					this.m_rtRightUI.DOAnchorMin(new Vector2(1f, 0f), 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rtRightUI.DOAnchorMax(new Vector2(2f, 1f), 0.4f, false).SetEase(Ease.OutCubic);
					return;
				}
				this.m_rtRightUI.anchorMin = new Vector2(1f, 0f);
				this.m_rtRightUI.anchorMax = new Vector2(2f, 1f);
				return;
			}
			else
			{
				if (bAnimate)
				{
					this.m_rtRightUI.DOAnchorMin(new Vector2(0f, 0f), 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rtRightUI.DOAnchorMax(new Vector2(1f, 1f), 0.4f, false).SetEase(Ease.OutCubic);
					return;
				}
				this.m_rtRightUI.anchorMin = new Vector2(0f, 0f);
				this.m_rtRightUI.anchorMax = new Vector2(1f, 1f);
				return;
			}
		}

		// Token: 0x06007CF1 RID: 31985 RVA: 0x0029D704 File Offset: 0x0029B904
		private void OnUnitVoice()
		{
			bool bLifetime = false;
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData.IsCollectedUnit(this.m_SelectedUnitID))
			{
				bLifetime = armyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, this.m_SelectedUnitID, NKMArmyData.UNIT_SEARCH_OPTION.Devotion, 0);
			}
			if (this.m_SelectedSkinID == 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_SelectedUnitID);
				NKCUIPopupVoice.Instance.Open(unitTempletBase, bLifetime);
				return;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_SelectedSkinID);
			NKCUIPopupVoice.Instance.Open(skinTemplet, bLifetime);
		}

		// Token: 0x06007CF2 RID: 31986 RVA: 0x0029D778 File Offset: 0x0029B978
		private void OnUnitCutin()
		{
			if (this.m_SelectedSkinID != 0)
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_SelectedSkinID);
				if (skinTemplet != null && !string.IsNullOrEmpty(skinTemplet.m_HyperSkillCutin))
				{
					if (this.m_NKCASUIUnitCutinIllust != null)
					{
						this.m_NKCASUIUnitCutinIllust.Unload();
						this.m_NKCASUIUnitCutinIllust = null;
					}
					this.m_NKCASUIUnitCutinIllust = NKCResourceUtility.OpenSpineIllust(skinTemplet.m_HyperSkillCutin, skinTemplet.m_HyperSkillCutin, false);
					NKCUtil.SetGameobjectActive(this.m_NKCASUIUnitCutinIllust.GetRectTransform().Find("VFX").gameObject, false);
					this.m_NKCASUIUnitCutinIllust.SetParent(base.transform, true);
					NKCUtil.SetGameobjectActive(this.m_NKCASUIUnitCutinIllust.GetRectTransform().gameObject, true);
					this.m_NKCASUIUnitCutinIllust.GetRectTransform().GetComponentInChildren<SkeletonGraphic>(true).AnimationState.SetAnimation(0, "BASE", false);
				}
			}
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x0029D850 File Offset: 0x0029BA50
		private void SetSlotCount(int count)
		{
			while (this.m_lstSlot.Count < count)
			{
				NKCUISkinSlot nkcuiskinSlot = UnityEngine.Object.Instantiate<NKCUISkinSlot>(this.m_pfbSkinSlot);
				nkcuiskinSlot.transform.SetParent(this.m_rtSlotRoot, false);
				nkcuiskinSlot.Init(new NKCUISkinSlot.OnClick(this.SelectSkin));
				this.m_lstSlot.Add(nkcuiskinSlot);
			}
			for (int i = count; i < this.m_lstSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSlot[i], false);
			}
		}

		// Token: 0x06007CF4 RID: 31988 RVA: 0x0029D8D4 File Offset: 0x0029BAD4
		public void OnSkinBuy(int skinID)
		{
			NKCUISkinSlot nkcuiskinSlot = this.m_lstSlot.Find((NKCUISkinSlot x) => x.SkinID == skinID);
			if (nkcuiskinSlot != null)
			{
				nkcuiskinSlot.SetOwned(true);
			}
			this.SelectSkin(skinID);
			this.PlayCutScene(skinID);
		}

		// Token: 0x06007CF5 RID: 31989 RVA: 0x0029D930 File Offset: 0x0029BB30
		public void OnSkinEquip(long unitUID, int equippedSkinID)
		{
			if (unitUID == this.m_SelectedUnitUID)
			{
				foreach (NKCUISkinSlot nkcuiskinSlot in this.m_lstSlot)
				{
					nkcuiskinSlot.SetEquipped(nkcuiskinSlot.SkinID == equippedSkinID);
				}
			}
			this.SelectSkin(equippedSkinID);
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x0029D99C File Offset: 0x0029BB9C
		private void SetDefaultSkin(NKMUnitData unitData = null)
		{
			if (unitData == null)
			{
				return;
			}
			this.SetSlotCount(1);
			if (null != this.m_lstSlot[0])
			{
				this.m_lstSlot[0].SetData(NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID), unitData.m_SkinID == 0);
				NKCUtil.SetGameobjectActive(this.m_lstSlot[0], true);
				this.SelectSkin(0);
				NKCUtil.SetLabelText(this.m_lstSlot[0].m_lbName, NKCUtilString.GET_STRING_BASE);
			}
		}

		// Token: 0x06007CF7 RID: 31991 RVA: 0x0029DA24 File Offset: 0x0029BC24
		private void SetSkinList(NKMUnitData unitData, List<NKMSkinTemplet> lstSkinTemplet)
		{
			if (lstSkinTemplet != null)
			{
				this.SetSlotCount(lstSkinTemplet.Count + 1);
				this.m_lstSlot[0].SetData(NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID), unitData.m_SkinID == 0);
				NKCUtil.SetGameobjectActive(this.m_lstSlot[0], true);
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				for (int i = 0; i < lstSkinTemplet.Count; i++)
				{
					NKMSkinTemplet nkmskinTemplet = lstSkinTemplet[i];
					NKCUISkinSlot nkcuiskinSlot = this.m_lstSlot[i + 1];
					nkcuiskinSlot.SetData(nkmskinTemplet, myUserData.m_InventoryData.HasItemSkin(nkmskinTemplet.m_SkinID), unitData.m_SkinID == nkmskinTemplet.m_SkinID, false);
					NKCUtil.SetGameobjectActive(nkcuiskinSlot, true);
				}
				NKCUtil.SetLabelText(this.m_lstSlot[0].m_lbName, NKCUtilString.GET_STRING_BASE);
			}
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x0029DAF8 File Offset: 0x0029BCF8
		private void OnBtnUnitInfoBuy()
		{
			if (null == this.m_cbtnUnitInfoBuy)
			{
				return;
			}
			if (!this.m_cbtnUnitInfoBuy.m_bLock)
			{
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(this.m_ProductID);
				if (shopItemTemplet == null)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_SKIN_LOCK, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCPopupShopBuyConfirm.Instance.Open(shopItemTemplet, new NKCUIShop.OnProductBuyDelegate(NKCShopManager.TryProductBuy));
			}
		}

		// Token: 0x06007CF9 RID: 31993 RVA: 0x0029DB5B File Offset: 0x0029BD5B
		private void OnBtnUnitInfoEquip()
		{
			if (!this.m_cbtnUnitInfoEquip.m_bLock)
			{
				NKCPacketSender.Send_NKMPacket_SET_UNIT_SKIN_REQ(this.m_SelectedUnitUID, this.m_SelectedSkinID);
			}
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x0029DB7C File Offset: 0x0029BD7C
		private void OnBtnTryButton()
		{
			NKMUnitData unitData = NKCUtil.MakeDummyUnit(this.m_SelectedUnitID, true);
			if (unitData != null)
			{
				unitData.m_SkinID = this.m_SelectedSkinID;
			}
			if (NKCUIShop.IsInstanceOpen)
			{
				string shopParam = string.Format("{0},{1}", NKCUIShop.Instance.GetShortcutParam(), this.m_ProductID);
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COLLECTION_TRAINING_MODE_CHANGE_REQ, delegate()
				{
					NKCScenManager.GetScenManager().Get_SCEN_GAME().PlayPracticeGame(unitData, NKM_SHORTCUT_TYPE.SHORTCUT_SHOP_SCENE, shopParam);
				}, null, false);
				return;
			}
			if (NKCUIEvent.IsInstanceOpen)
			{
				string param = NKCUIEvent.Instance.SelectedTabId.ToString();
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COLLECTION_TRAINING_MODE_CHANGE_REQ, delegate()
				{
					NKCScenManager.GetScenManager().Get_SCEN_GAME().PlayPracticeGame(unitData, NKM_SHORTCUT_TYPE.SHORTCUT_HOME_EVENT_BANNER, param);
				}, null, false);
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COLLECTION_TRAINING_MODE_CHANGE_REQ, delegate()
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().PlayPracticeGame(unitData, NKM_SHORTCUT_TYPE.SHORTCUT_NONE, "");
			}, null, false);
		}

		// Token: 0x06007CFB RID: 31995 RVA: 0x0029DC78 File Offset: 0x0029BE78
		private void OpenSDIllust(NKMSkinTemplet skinTemplet)
		{
			if (skinTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
				return;
			}
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(skinTemplet, false);
			if (this.m_spineSD != null)
			{
				this.m_spineSD.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				this.m_spineSD.SetParent(this.m_rtSDRoot, false);
				RectTransform rectTransform = this.m_spineSD.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector3.zero;
					rectTransform.localScale = Vector3.one * this.m_fSDScale;
				}
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
		}

		// Token: 0x06007CFC RID: 31996 RVA: 0x0029DD54 File Offset: 0x0029BF54
		private void OpenSDIllust(NKMUnitTempletBase unitTempletBase)
		{
			if (unitTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
				return;
			}
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(unitTempletBase, false);
			if (this.m_spineSD != null)
			{
				this.m_spineSD.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				this.m_spineSD.SetParent(this.m_rtSDRoot, false);
				RectTransform rectTransform = this.m_spineSD.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector3.zero;
					rectTransform.localScale = Vector3.one * this.m_fSDScale;
				}
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
		}

		// Token: 0x06007CFD RID: 31997 RVA: 0x0029DE2F File Offset: 0x0029C02F
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			if (this.m_eMode == NKCUIShopSkinPopup.Mode.ForUnitInfo && unitData.m_UnitUID == this.m_SelectedUnitUID)
			{
				this.SetSkinListData(unitData);
				this.SelectSkin(unitData.m_SkinID);
			}
		}

		// Token: 0x04006975 RID: 26997
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_shop_skin";

		// Token: 0x04006976 RID: 26998
		private const string UI_ASSET_NAME = "NKM_UI_SHOP_SKIN";

		// Token: 0x04006977 RID: 26999
		private static NKCUIShopSkinPopup m_Instance;

		// Token: 0x04006978 RID: 27000
		private List<int> DEFAULT_RESOURCE_LIST = new List<int>
		{
			1,
			2,
			101,
			102
		};

		// Token: 0x04006979 RID: 27001
		private List<int> m_lstUpsideMenuShowResource = new List<int>();

		// Token: 0x0400697A RID: 27002
		private bool m_bShowUpsideMenu = true;

		// Token: 0x0400697B RID: 27003
		private NKCUIShopSkinPopup.Mode m_eMode;

		// Token: 0x0400697C RID: 27004
		[Header("오른쪽 UI 루트")]
		public RectTransform m_rtRightUI;

		// Token: 0x0400697D RID: 27005
		[Header("캐릭터 뷰")]
		public NKCUICharacterView m_CharacterView;

		// Token: 0x0400697E RID: 27006
		private int m_ProductID;

		// Token: 0x0400697F RID: 27007
		private int m_SelectedSkinID;

		// Token: 0x04006980 RID: 27008
		private int m_SelectedUnitID;

		// Token: 0x04006981 RID: 27009
		private long m_SelectedUnitUID;

		// Token: 0x04006982 RID: 27010
		[Header("스킨 목록")]
		public GameObject m_objMenuSkinList;

		// Token: 0x04006983 RID: 27011
		public NKCUISkinSlot m_pfbSkinSlot;

		// Token: 0x04006984 RID: 27012
		public RectTransform m_rtSlotRoot;

		// Token: 0x04006985 RID: 27013
		public ScrollRect m_srScrollRect;

		// Token: 0x04006986 RID: 27014
		public GameObject m_objNoSkin;

		// Token: 0x04006987 RID: 27015
		private List<NKCUISkinSlot> m_lstSlot = new List<NKCUISkinSlot>();

		// Token: 0x04006988 RID: 27016
		[Header("스킨 기본정보")]
		public GameObject m_objMenuSkinTitle;

		// Token: 0x04006989 RID: 27017
		public Text m_lbSkinName;

		// Token: 0x0400698A RID: 27018
		public GameObject m_objLimited;

		// Token: 0x0400698B RID: 27019
		public Text m_lbSkinGrade;

		// Token: 0x0400698C RID: 27020
		public Image m_imgSkinGradeLine;

		// Token: 0x0400698D RID: 27021
		public Sprite m_spGradeLineN;

		// Token: 0x0400698E RID: 27022
		public Sprite m_spGradeLineR;

		// Token: 0x0400698F RID: 27023
		public Sprite m_spGradeLineSR;

		// Token: 0x04006990 RID: 27024
		public Sprite m_spGradeLineSSR;

		// Token: 0x04006991 RID: 27025
		public Sprite m_spGradeLineSpecial;

		// Token: 0x04006992 RID: 27026
		[Header("스킨 설명")]
		public GameObject m_objMenuSkinDesc;

		// Token: 0x04006993 RID: 27027
		public Text m_lbDesc;

		// Token: 0x04006994 RID: 27028
		[Header("착용 가능 사원")]
		public GameObject m_objEquippableUnit;

		// Token: 0x04006995 RID: 27029
		public GameObject m_objEquppableUnitNotOwned;

		// Token: 0x04006996 RID: 27030
		public Image m_imgEquippableUnit;

		// Token: 0x04006997 RID: 27031
		public Text m_lbEquippableUnitName;

		// Token: 0x04006998 RID: 27032
		public Text m_lbEquippableUnitTitle;

		// Token: 0x04006999 RID: 27033
		[Header("스킨 전체 구성요소")]
		public GameObject m_objMenuSkinComponents;

		// Token: 0x0400699A RID: 27034
		public NKCUIComStateButton m_csbtnComponentVoice;

		// Token: 0x0400699B RID: 27035
		public NKCUIComStateButton m_csbtnComponentCutin;

		// Token: 0x0400699C RID: 27036
		public Image m_imgComponentCutin;

		// Token: 0x0400699D RID: 27037
		public GameObject m_objComponentCutinEffect;

		// Token: 0x0400699E RID: 27038
		public NKCUIComStateButton m_csbtnComponentStory;

		// Token: 0x0400699F RID: 27039
		public GameObject m_NKM_UI_SHOP_SKIN_POPUP_INFO_COMPONENT_CONTENT_EFFECT;

		// Token: 0x040069A0 RID: 27040
		public GameObject m_objComponentConversion;

		// Token: 0x040069A1 RID: 27041
		public GameObject m_objComponentLobbyFace;

		// Token: 0x040069A2 RID: 27042
		public GameObject m_objComponentCollab;

		// Token: 0x040069A3 RID: 27043
		public GameObject m_objComponentGauntlet;

		// Token: 0x040069A4 RID: 27044
		public NKCUIComStateButton m_csbtnLoginAnim;

		// Token: 0x040069A5 RID: 27045
		public GameObject m_objComponentLoginAnim;

		// Token: 0x040069A6 RID: 27046
		[Header("유닛정보용 버튼")]
		public GameObject m_objMenuSkinUnitInfoBuy;

		// Token: 0x040069A7 RID: 27047
		public NKCUIComButton m_cbtnUnitInfoEquip;

		// Token: 0x040069A8 RID: 27048
		public NKCUIComButton m_cbtnUnitInfoBuy;

		// Token: 0x040069A9 RID: 27049
		[Header("상점용 버튼")]
		public GameObject m_objMenuSkinShopBuy;

		// Token: 0x040069AA RID: 27050
		public GameObject m_objSalePriceRoot;

		// Token: 0x040069AB RID: 27051
		public Text m_lbOldPrice;

		// Token: 0x040069AC RID: 27052
		public NKCUIPriceTag m_PriceTag;

		// Token: 0x040069AD RID: 27053
		public NKCUIComButton m_cbtnShopTry;

		// Token: 0x040069AE RID: 27054
		public NKCUIComButton m_cbtnShopBuy;

		// Token: 0x040069AF RID: 27055
		[Header("스킨정보용 버튼")]
		public GameObject m_objMenuSkinInfoOnly;

		// Token: 0x040069B0 RID: 27056
		public NKCUIComButton m_cbtnSkinInfoTry;

		// Token: 0x040069B1 RID: 27057
		public NKCUIComButton m_cbtnSKinInfoClose;

		// Token: 0x040069B2 RID: 27058
		[Header("왼쪽 구매 가능 시간")]
		public GameObject m_objLimitedTime;

		// Token: 0x040069B3 RID: 27059
		public Text m_lbLimitedTime;

		// Token: 0x040069B4 RID: 27060
		[Header("할인 관련")]
		public GameObject m_objDiscountDay;

		// Token: 0x040069B5 RID: 27061
		public Text m_txtDiscountDay;

		// Token: 0x040069B6 RID: 27062
		public GameObject m_objDiscountDayForUnitInfo;

		// Token: 0x040069B7 RID: 27063
		public Text m_txtDiscountDayForUnitInfo;

		// Token: 0x040069B8 RID: 27064
		public GameObject m_objDiscountRate;

		// Token: 0x040069B9 RID: 27065
		public Text m_txtDiscountRate;

		// Token: 0x040069BA RID: 27066
		[Header("SD/인게임 뷰")]
		public NKCUIComStateButton m_sbtnIngameSD;

		// Token: 0x040069BB RID: 27067
		private bool m_bGameUnitView;

		// Token: 0x040069BC RID: 27068
		public NKCUIInGameCharacterViewer m_InGameUnitViewer;

		// Token: 0x040069BD RID: 27069
		[Header("SD캐릭터 관련 설정")]
		public RectTransform m_rtSDRoot;

		// Token: 0x040069BE RID: 27070
		public float m_fSDScale = 1.2f;

		// Token: 0x040069BF RID: 27071
		private NKCASUIUnitIllust m_spineSD;

		// Token: 0x040069C0 RID: 27072
		[Header("기타")]
		public NKCUIComStateButton m_cbtnIllustViewMode;

		// Token: 0x040069C1 RID: 27073
		private long m_lBuyTimeLimit;

		// Token: 0x040069C2 RID: 27074
		private DateTime m_tEndDateDiscountTime = DateTime.MinValue;

		// Token: 0x040069C3 RID: 27075
		private bool m_bUseUpdate;

		// Token: 0x040069C4 RID: 27076
		private Color CUTIN_PRIVATE_COLOR = new Color(0.9490196f, 0.81960785f, 0.101960786f);

		// Token: 0x040069C5 RID: 27077
		private NKCASUIUnitIllust m_NKCASUIUnitCutinIllust;

		// Token: 0x040069C6 RID: 27078
		private NKCUICharacterView.eMode m_eCurrentViewMode;

		// Token: 0x040069C7 RID: 27079
		private const float ONE_SECOND = 1f;

		// Token: 0x040069C8 RID: 27080
		private float m_fUpdateGap = 1f;

		// Token: 0x02001854 RID: 6228
		private enum Mode
		{
			// Token: 0x0400A8A4 RID: 43172
			ForShop,
			// Token: 0x0400A8A5 RID: 43173
			ForUnitInfo,
			// Token: 0x0400A8A6 RID: 43174
			ForSkinInfo
		}
	}
}
