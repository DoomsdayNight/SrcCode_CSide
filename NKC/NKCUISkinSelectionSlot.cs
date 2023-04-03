using System;
using Cs.Logging;
using NKC.UI.Shop;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x020009E8 RID: 2536
	[RequireComponent(typeof(NKCUIShopSlotSkin))]
	public class NKCUISkinSelectionSlot : MonoBehaviour
	{
		// Token: 0x06006D1E RID: 27934 RVA: 0x0023C34A File Offset: 0x0023A54A
		public void Init()
		{
			base.TryGetComponent<NKCUIShopSlotSkin>(out this.m_NKCUIShopSlotSkin);
			NKCUtil.SetButtonClickDelegate(this.m_cbtnSlotButton, new UnityAction(this.OnClickSlot));
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x0023C370 File Offset: 0x0023A570
		public void SetData(NKMSkinTemplet skinTemplet, bool haveSkin, NKCUISkinSelectionSlot.OnClick onClickSlot)
		{
			this.m_skinId = 0;
			this.m_dOnClickSlot = onClickSlot;
			if (this.m_NKCUIShopSlotSkin == null)
			{
				Log.Error("NKCUISkinSelectionSlot needs NKCUIShopSlotSkin", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUISkinSelectionSlot.cs", 33);
				return;
			}
			if (skinTemplet == null)
			{
				Log.Error("SkinTemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUISkinSelectionSlot.cs", 39);
				return;
			}
			this.m_skinId = skinTemplet.m_SkinID;
			this.m_NKCUIShopSlotSkin.SetNameText(skinTemplet.GetTitle());
			Sprite slotCardItemImage = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, skinTemplet);
			this.m_NKCUIShopSlotSkin.SetSlotCardItemImage(slotCardItemImage);
			this.m_NKCUIShopSlotSkin.SetGoodImageFromSkinData(skinTemplet);
			NKCUtil.SetGameobjectActive(this.m_objHaveSkin, haveSkin);
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x0023C409 File Offset: 0x0023A609
		private void OnClickSlot()
		{
			if (this.m_dOnClickSlot != null)
			{
				this.m_dOnClickSlot(this.m_skinId);
			}
		}

		// Token: 0x040058FA RID: 22778
		public GameObject m_objHaveSkin;

		// Token: 0x040058FB RID: 22779
		public NKCUIComButton m_cbtnSlotButton;

		// Token: 0x040058FC RID: 22780
		private NKCUIShopSlotSkin m_NKCUIShopSlotSkin;

		// Token: 0x040058FD RID: 22781
		private int m_skinId;

		// Token: 0x040058FE RID: 22782
		private NKCUISkinSelectionSlot.OnClick m_dOnClickSlot;

		// Token: 0x020016F4 RID: 5876
		// (Invoke) Token: 0x0600B1CA RID: 45514
		public delegate void OnClick(int skinId);
	}
}
