using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007D6 RID: 2006
	public class NKCUISkinSlot : MonoBehaviour
	{
		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x0017E672 File Offset: 0x0017C872
		// (set) Token: 0x06004F22 RID: 20258 RVA: 0x0017E67A File Offset: 0x0017C87A
		public int SkinID { get; private set; }

		// Token: 0x06004F23 RID: 20259 RVA: 0x0017E683 File Offset: 0x0017C883
		public void Init(NKCUISkinSlot.OnClick onClick)
		{
			this.dOnClick = onClick;
			this.m_cbtnSlot.PointerClick.RemoveAllListeners();
			this.m_cbtnSlot.PointerClick.AddListener(new UnityAction(this.OnBtnSlot));
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x0017E6B8 File Offset: 0x0017C8B8
		public void SetData(NKMSkinTemplet skinTemplet, bool bOwned = false, bool bEquipped = false, bool bBlack = false)
		{
			if (skinTemplet == null)
			{
				this.m_imgSkin.sprite = null;
				this.m_lbName.text = "";
				this.SetEquipped(false);
				this.SetOwned(false);
				this.SkinID = 0;
				return;
			}
			this.SetEquipped(bEquipped);
			this.SetOwned(bOwned);
			this.SetSkinBG(skinTemplet.m_SkinGrade);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_UNIT_SKIN_SLOT_ON_BLACK, bBlack);
			this.SkinID = skinTemplet.m_SkinID;
			this.m_imgSkin.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, skinTemplet);
			this.m_lbName.text = skinTemplet.GetTitle();
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x0017E750 File Offset: 0x0017C950
		public void SetData(NKMUnitTempletBase unitTemplet, bool bEquipped = false)
		{
			this.SetOwned(true);
			this.SetEquipped(bEquipped);
			this.SkinID = 0;
			this.m_imgSkin.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTemplet);
			this.m_lbName.text = unitTemplet.GetUnitTitle();
			this.SetSkinBG(NKMSkinTemplet.SKIN_GRADE.SG_VARIATION);
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x0017E79C File Offset: 0x0017C99C
		public void SetOwned(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objNotOwned, !bValue);
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x0017E7AD File Offset: 0x0017C9AD
		public void SetEquipped(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objEquipped, bValue);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x0017E7BB File Offset: 0x0017C9BB
		public void SetSelected(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelected, bValue);
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x0017E7CC File Offset: 0x0017C9CC
		private void SetSkinBG(NKMSkinTemplet.SKIN_GRADE grade)
		{
			switch (grade)
			{
			default:
				NKCUtil.SetImageSprite(this.m_imgBG, this.m_spBG_Variation, false);
				break;
			case NKMSkinTemplet.SKIN_GRADE.SG_NORMAL:
				NKCUtil.SetImageSprite(this.m_imgBG, this.m_spBG_Normal, false);
				break;
			case NKMSkinTemplet.SKIN_GRADE.SG_RARE:
				NKCUtil.SetImageSprite(this.m_imgBG, this.m_spBG_Rare, false);
				break;
			case NKMSkinTemplet.SKIN_GRADE.SG_PREMIUM:
			case NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL:
				NKCUtil.SetImageSprite(this.m_imgBG, this.m_spBG_Premium, false);
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_objSpecialSkinBG, grade == NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL);
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0017E850 File Offset: 0x0017CA50
		private void OnBtnSlot()
		{
			if (this.dOnClick != null)
			{
				this.dOnClick(this.SkinID);
			}
		}

		// Token: 0x04003F1F RID: 16159
		public Image m_imgSkin;

		// Token: 0x04003F20 RID: 16160
		public Image m_imgBG;

		// Token: 0x04003F21 RID: 16161
		public Text m_lbName;

		// Token: 0x04003F22 RID: 16162
		public GameObject m_objEquipped;

		// Token: 0x04003F23 RID: 16163
		public GameObject m_objNotOwned;

		// Token: 0x04003F24 RID: 16164
		public GameObject m_objSelected;

		// Token: 0x04003F25 RID: 16165
		public GameObject m_objSpecialSkinBG;

		// Token: 0x04003F26 RID: 16166
		public NKCUIComButton m_cbtnSlot;

		// Token: 0x04003F27 RID: 16167
		public Sprite m_spBG_Variation;

		// Token: 0x04003F28 RID: 16168
		public Sprite m_spBG_Normal;

		// Token: 0x04003F29 RID: 16169
		public Sprite m_spBG_Rare;

		// Token: 0x04003F2A RID: 16170
		public Sprite m_spBG_Premium;

		// Token: 0x04003F2C RID: 16172
		private NKCUISkinSlot.OnClick dOnClick;

		// Token: 0x04003F2D RID: 16173
		public GameObject m_NKM_UI_COLLECTION_UNIT_INFO_UNIT_SKIN_SLOT_ON_BLACK;

		// Token: 0x02001490 RID: 5264
		// (Invoke) Token: 0x0600A941 RID: 43329
		public delegate void OnClick(int skinID);
	}
}
