using System;
using System.Collections.Generic;
using NKM;
using NKM.Shop;
using NKM.Templet;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F5 RID: 2549
	public class NKCUIUnitInfoSkinPanel : MonoBehaviour
	{
		// Token: 0x06006E64 RID: 28260 RVA: 0x00244388 File Offset: 0x00242588
		public void Init(NKCUIUnitInfoSkinPanel.ChangeSkin changeSkinIllust)
		{
			this.dChangeSkin = changeSkinIllust;
			if (null != this.m_cbtnEquip)
			{
				this.m_cbtnEquip.PointerClick.RemoveAllListeners();
				this.m_cbtnEquip.PointerClick.AddListener(new UnityAction(this.OnBtnEquip));
			}
			if (null != this.m_cbtnBuy)
			{
				this.m_cbtnBuy.PointerClick.RemoveAllListeners();
				this.m_cbtnBuy.PointerClick.AddListener(new UnityAction(this.OnBtnBuy));
			}
		}

		// Token: 0x06006E65 RID: 28261 RVA: 0x00244410 File Offset: 0x00242610
		public void CleanUp()
		{
			this.CloseCurrentPreviewModel();
			this.m_TextureRenderer.CleanUp();
			this.m_SelectedSkinID = 0;
			this.m_SelectedUnitID = 0;
			this.m_SelectedUnitUID = 0L;
		}

		// Token: 0x06006E66 RID: 28262 RVA: 0x0024443C File Offset: 0x0024263C
		private void CloseCurrentPreviewModel()
		{
			if (this.m_UnitPreview != null && this.m_UnitPreview.m_UnitSpineSpriteInstant != null)
			{
				this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.localScale = Vector3.one;
				NKCUtil.SetLayer(this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform, this.m_UnitPreviewOrigLayer);
			}
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_UnitPreview);
			this.m_UnitPreview = null;
		}

		// Token: 0x06006E67 RID: 28263 RVA: 0x002444BC File Offset: 0x002426BC
		public void SetData(NKMUnitData unitData, bool resetSkin)
		{
			if (unitData == null)
			{
				return;
			}
			this.m_TextureRenderer.PrepareTexture(null);
			this.m_SelectedUnitID = unitData.m_UnitID;
			this.m_SelectedUnitUID = unitData.m_UnitUID;
			if (!NKMSkinManager.IsCharacterHasSkin(unitData.m_UnitID))
			{
				this.SetDefaultSkin(unitData);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNoSkin, false);
			NKCUtil.SetGameobjectActive(this.m_srScrollRect, true);
			List<NKMSkinTemplet> skinlistForCharacter = NKMSkinManager.GetSkinlistForCharacter(unitData.m_UnitID, NKCScenManager.CurrentUserData().m_InventoryData);
			this.SetSkinList(unitData, skinlistForCharacter);
			if (resetSkin)
			{
				this.SelectSkin(unitData.m_SkinID);
				return;
			}
			this.SelectSkin(this.m_SelectedSkinID);
		}

		// Token: 0x06006E68 RID: 28264 RVA: 0x00244558 File Offset: 0x00242758
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

		// Token: 0x06006E69 RID: 28265 RVA: 0x002445E0 File Offset: 0x002427E0
		private void SetSkinList(NKMUnitData unitData, List<NKMSkinTemplet> lstSkinTemplet)
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

		// Token: 0x06006E6A RID: 28266 RVA: 0x002446AC File Offset: 0x002428AC
		private void SetBottomButton(bool skinOwned, bool equipped)
		{
			NKCUtil.SetGameobjectActive(this.m_cbtnEquip, skinOwned);
			NKCUtil.SetGameobjectActive(this.m_cbtnBuy, !skinOwned);
			if (skinOwned)
			{
				if (equipped)
				{
					NKCUIComButton cbtnEquip = this.m_cbtnEquip;
					if (cbtnEquip == null)
					{
						return;
					}
					cbtnEquip.Lock();
					return;
				}
				else
				{
					NKCUIComButton cbtnEquip2 = this.m_cbtnEquip;
					if (cbtnEquip2 == null)
					{
						return;
					}
					cbtnEquip2.UnLock();
					return;
				}
			}
			else
			{
				ShopItemTemplet shopTempletBySkinID = NKCShopManager.GetShopTempletBySkinID(this.m_SelectedSkinID);
				if (shopTempletBySkinID != null)
				{
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					bool flag;
					long num;
					if (NKCShopManager.CanBuyFixShop(myUserData, shopTempletBySkinID, out flag, out num, true) == NKM_ERROR_CODE.NEC_OK)
					{
						NKCUIComButton cbtnBuy = this.m_cbtnBuy;
						if (cbtnBuy == null)
						{
							return;
						}
						cbtnBuy.UnLock();
						return;
					}
					else
					{
						NKCUIComButton cbtnBuy2 = this.m_cbtnBuy;
						if (cbtnBuy2 == null)
						{
							return;
						}
						cbtnBuy2.Lock();
						return;
					}
				}
				else
				{
					NKCUIComButton cbtnBuy3 = this.m_cbtnBuy;
					if (cbtnBuy3 == null)
					{
						return;
					}
					cbtnBuy3.Lock();
					return;
				}
			}
		}

		// Token: 0x06006E6B RID: 28267 RVA: 0x00244758 File Offset: 0x00242958
		private void SelectSkin(int skinID)
		{
			this.m_SelectedSkinID = skinID;
			foreach (NKCUISkinSlot nkcuiskinSlot in this.m_lstSlot)
			{
				nkcuiskinSlot.SetSelected(nkcuiskinSlot.SkinID == skinID);
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(this.m_SelectedUnitUID);
			bool equipped = unitFromUID != null && unitFromUID.m_SkinID == skinID;
			bool skinOwned = myUserData.m_InventoryData.HasItemSkin(skinID);
			this.SetBottomButton(skinOwned, equipped);
			if (skinID == 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_SelectedUnitID);
				if (unitTempletBase != null)
				{
					NKCUtil.SetLabelText(this.m_lbSkinTitle, unitTempletBase.GetUnitTitle());
				}
				NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(this.m_SelectedUnitID);
				if (unitTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbSkinDescription, unitTemplet.GetUnitIntro());
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbSkinDescription, NKCUtilString.GET_STRING_BASE_SKIN);
				}
			}
			else
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
				if (skinTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbSkinTitle, skinTemplet.GetTitle());
					NKCUtil.SetLabelText(this.m_lbSkinDescription, skinTemplet.GetSkinDesc());
				}
			}
			if (unitFromUID != null)
			{
				this.SetPreviewBattleUnit(unitFromUID.m_UnitID, skinID);
			}
			if (this.dChangeSkin != null)
			{
				this.dChangeSkin(skinID);
			}
		}

		// Token: 0x06006E6C RID: 28268 RVA: 0x002448AC File Offset: 0x00242AAC
		private void SetPreviewBattleUnit(int unitID, int skinID)
		{
			this.CloseCurrentPreviewModel();
			if (skinID == 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
				this.m_UnitPreview = NKCUnitViewer.OpenUnitViewerSpineSprite(unitTempletBase, false, true);
				this.bWaitingForLoading = true;
			}
			else
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
				this.m_UnitPreview = NKCUnitViewer.OpenUnitViewerSpineSprite(skinTemplet, false, true);
				this.bWaitingForLoading = true;
			}
			NKCUtil.SetGameobjectActive(this.m_TextureRenderer, false);
			NKCUtil.SetGameobjectActive(this.m_objLoading, this.bWaitingForLoading);
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x00244919 File Offset: 0x00242B19
		private void Update()
		{
			if (this.bWaitingForLoading && this.m_UnitPreview != null && this.m_UnitPreview.m_bIsLoaded)
			{
				this.AfterUnitLoadComplete();
			}
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x00244940 File Offset: 0x00242B40
		private void AfterUnitLoadComplete()
		{
			this.bWaitingForLoading = false;
			if (this.m_UnitPreview != null && this.m_UnitPreview.m_UnitSpineSpriteInstant != null)
			{
				this.m_UnitPreviewOrigLayer = this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.layer;
				NKCUtil.SetLayer(this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform, 31);
				this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.SetParent(this.m_TextureRenderer.transform, false);
				this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.localPosition = new Vector3(0f, 30f, 0f);
				float d = this.m_TextureRenderer.m_rtImage.GetHeight() / 300f;
				this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.localScale = Vector3.one * d;
				Transform transform = this.m_UnitPreview.m_UnitSpineSpriteInstant.m_Instant.transform.Find("SPINE_SkeletonAnimation");
				if (transform != null)
				{
					SkeletonAnimation component = transform.GetComponent<SkeletonAnimation>();
					if (component != null)
					{
						component.AnimationState.SetAnimation(0, "ASTAND", true);
					}
					NKCUtil.SetGameobjectActive(this.m_TextureRenderer, true);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_TextureRenderer, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objLoading, false);
		}

		// Token: 0x06006E6F RID: 28271 RVA: 0x00244AA8 File Offset: 0x00242CA8
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

		// Token: 0x06006E70 RID: 28272 RVA: 0x00244B14 File Offset: 0x00242D14
		public void OnSkinBuy(int skinID)
		{
			NKCUISkinSlot nkcuiskinSlot = this.m_lstSlot.Find((NKCUISkinSlot x) => x.SkinID == skinID);
			if (nkcuiskinSlot != null)
			{
				nkcuiskinSlot.SetOwned(true);
			}
			this.SelectSkin(skinID);
		}

		// Token: 0x06006E71 RID: 28273 RVA: 0x00244B64 File Offset: 0x00242D64
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

		// Token: 0x06006E72 RID: 28274 RVA: 0x00244BE5 File Offset: 0x00242DE5
		private void OnBtnEquip()
		{
			if (null == this.m_cbtnEquip)
			{
				return;
			}
			if (!this.m_cbtnEquip.m_bLock)
			{
				NKCPacketSender.Send_NKMPacket_SET_UNIT_SKIN_REQ(this.m_SelectedUnitUID, this.m_SelectedSkinID);
			}
		}

		// Token: 0x06006E73 RID: 28275 RVA: 0x00244C14 File Offset: 0x00242E14
		private void OnBtnBuy()
		{
			if (null == this.m_cbtnBuy)
			{
				return;
			}
			if (!this.m_cbtnBuy.m_bLock)
			{
				ShopItemTemplet shopTemplet = NKCShopManager.GetShopTempletBySkinID(this.m_SelectedSkinID);
				if (shopTemplet == null)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_SKIN_LOCK, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCPopupItemBox.Instance.Open(shopTemplet, delegate()
				{
					this.TryProductBuy(shopTemplet.m_ProductID);
				});
			}
		}

		// Token: 0x06006E74 RID: 28276 RVA: 0x00244C94 File Offset: 0x00242E94
		private void TryProductBuy(int ProductID)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(ProductID);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!myUserData.HaveEnoughResourceToBuy(shopItemTemplet, 1))
			{
				NKCShopManager.OpenItemLackPopup(shopItemTemplet.m_PriceItemID, myUserData.m_ShopData.GetRealPrice(shopItemTemplet, 1, false));
				return;
			}
			if (shopItemTemplet.m_PriceItemID == 0)
			{
				NKCPacketSender.Send_NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ(shopItemTemplet.m_MarketID);
				return;
			}
			NKCPacketSender.Send_NKMPacket_SHOP_FIX_SHOP_BUY_REQ(ProductID, 1, null);
		}

		// Token: 0x040059B8 RID: 22968
		public NKCUISkinSlot m_pfbSkinSlot;

		// Token: 0x040059B9 RID: 22969
		public RectTransform m_rtSlotRoot;

		// Token: 0x040059BA RID: 22970
		public ScrollRect m_srScrollRect;

		// Token: 0x040059BB RID: 22971
		public GameObject m_objNoSkin;

		// Token: 0x040059BC RID: 22972
		public Text m_lbSkinTitle;

		// Token: 0x040059BD RID: 22973
		public Text m_lbSkinDescription;

		// Token: 0x040059BE RID: 22974
		public NKCUIComButton m_cbtnEquip;

		// Token: 0x040059BF RID: 22975
		public NKCUIComButton m_cbtnBuy;

		// Token: 0x040059C0 RID: 22976
		public NKCUIComModelTextureRenderer m_TextureRenderer;

		// Token: 0x040059C1 RID: 22977
		public GameObject m_objLoading;

		// Token: 0x040059C2 RID: 22978
		private NKCASUnitSpineSprite m_UnitPreview;

		// Token: 0x040059C3 RID: 22979
		private int m_UnitPreviewOrigLayer;

		// Token: 0x040059C4 RID: 22980
		private List<NKCUISkinSlot> m_lstSlot = new List<NKCUISkinSlot>();

		// Token: 0x040059C5 RID: 22981
		private int m_SelectedSkinID;

		// Token: 0x040059C6 RID: 22982
		private int m_SelectedUnitID;

		// Token: 0x040059C7 RID: 22983
		private long m_SelectedUnitUID;

		// Token: 0x040059C8 RID: 22984
		private NKCUIUnitInfoSkinPanel.ChangeSkin dChangeSkin;

		// Token: 0x040059C9 RID: 22985
		private bool bWaitingForLoading;

		// Token: 0x0200170A RID: 5898
		// (Invoke) Token: 0x0600B217 RID: 45591
		public delegate void ChangeSkin(int skinID);
	}
}
