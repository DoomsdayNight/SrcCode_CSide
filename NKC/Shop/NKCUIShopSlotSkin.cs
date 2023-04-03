using System;
using System.Runtime.CompilerServices;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE8 RID: 2792
	public class NKCUIShopSlotSkin : NKCUIShopSlotCard
	{
		// Token: 0x06007D54 RID: 32084 RVA: 0x002A014C File Offset: 0x0029E34C
		protected override void SetGoodsImage(ShopItemTemplet shopTemplet, bool bFirstBuy)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(shopTemplet.m_ItemID);
			if (skinTemplet == null)
			{
				Debug.LogError(string.Format("Skintemplet {0} not found!", shopTemplet.m_ItemID));
				return;
			}
			Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, skinTemplet);
			if (sprite == null)
			{
				Debug.LogError(string.Format("Skin templet {0}(from productID {1}) null", shopTemplet.m_CardImage, shopTemplet.m_ItemID));
			}
			this.m_imgItem.sprite = sprite;
			this.SetGoodImageFromSkinData(skinTemplet);
		}

		// Token: 0x06007D55 RID: 32085 RVA: 0x002A01C8 File Offset: 0x0029E3C8
		public void SetGoodImageFromSkinData(NKMSkinTemplet skinTemplet)
		{
			if (skinTemplet == null)
			{
				Debug.LogError(string.Format("SkinTemplet not found", Array.Empty<object>()));
				return;
			}
			NKCUIShopSlotSkin.<>c__DisplayClass36_0 CS$<>8__locals1;
			CS$<>8__locals1.bActivatedBeforeThis = false;
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(skinTemplet.ChangesVoice(), this.m_objVoice, this.m_objVoiceDot, ref CS$<>8__locals1);
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(skinTemplet.ChangesHyperCutin(), this.m_objSkillCutin, this.m_objSkillCutinDot, ref CS$<>8__locals1);
			if (skinTemplet.ChangesHyperCutin())
			{
				NKCUtil.SetGameobjectActive(this.m_objCutInSpecial, skinTemplet.m_SkinSkillCutIn == NKMSkinTemplet.SKIN_CUTIN.CUTIN_PRIVATE);
				if (skinTemplet.m_SkinSkillCutIn == NKMSkinTemplet.SKIN_CUTIN.CUTIN_PRIVATE)
				{
					NKCUtil.SetImageSprite(this.m_bgSkillCutIn, this.m_spBG_CutinSpecial, false);
					NKCUtil.SetImageSprite(this.m_imgSkillCutIn, NKCUtil.GetShopSprite("NKM_UI_SHOP_SKIN_ICON_CUTIN_SPECIAL"), false);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_bgSkillCutIn, this.m_spBG_CutinNormal, false);
					NKCUtil.SetImageSprite(this.m_imgSkillCutIn, NKCUtil.GetShopSprite("NKM_UI_SHOP_SKIN_ICON_CUTIN"), false);
				}
			}
			NKCUtil.SetImageSprite(this.m_imgBackground, this.GetBGSprite(skinTemplet.m_SkinGrade), false);
			NKCUtil.SetImageSprite(this.m_imgGradeLine, this.GetLineSprite(skinTemplet.m_SkinGrade), false);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(skinTemplet.m_SkinEquipUnitID);
			string msg = "";
			if (unitTempletBase != null)
			{
				if (unitTempletBase.m_bAwaken)
				{
					msg = NKCStringTable.GetString("SI_PF_SHOP_SKIN_AWAKEN", new object[]
					{
						unitTempletBase.GetUnitName()
					});
				}
				else
				{
					msg = unitTempletBase.GetUnitName();
				}
			}
			NKCUtil.SetLabelText(this.m_lbUnitName, msg);
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(skinTemplet.m_bEffect, this.m_NKM_UI_SHOP_SKIN_SLOT_COMPONENT_ICON_SKIN_EFFECT, this.m_ellipse03, ref CS$<>8__locals1);
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(!string.IsNullOrEmpty(skinTemplet.m_CutscenePurchase), this.m_NKM_UI_SHOP_SKIN_SLOT_COMPONENT_ICON_SKIN_STORY, this.m_ellipse04, ref CS$<>8__locals1);
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(skinTemplet.HasLoginCutin, this.m_objComponentLoginAnim, this.m_dotLoginAnim, ref CS$<>8__locals1);
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(skinTemplet.m_LobbyFace, this.m_objComponentLobbyFace, this.m_dotLobbyFace, ref CS$<>8__locals1);
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(skinTemplet.m_Conversion, this.m_objComponentConversion, this.m_dotConversion, ref CS$<>8__locals1);
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(skinTemplet.m_Collabo, this.m_objComponentCollab, this.m_dotCollab, ref CS$<>8__locals1);
			NKCUIShopSlotSkin.<SetGoodImageFromSkinData>g__ActivateIcon|36_0(skinTemplet.m_Gauntlet, this.m_objComponentGauntlet, this.m_dotGauntlet, ref CS$<>8__locals1);
			NKCUtil.SetGameobjectActive(this.m_objBG_Special, skinTemplet.m_SkinGrade == NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL);
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x002A03E5 File Offset: 0x0029E5E5
		private Sprite GetBGSprite(NKMSkinTemplet.SKIN_GRADE grade)
		{
			switch (grade)
			{
			default:
				return this.m_spBG_Variation;
			case NKMSkinTemplet.SKIN_GRADE.SG_NORMAL:
				return this.m_spBG_Normal;
			case NKMSkinTemplet.SKIN_GRADE.SG_RARE:
				return this.m_spBG_Rare;
			case NKMSkinTemplet.SKIN_GRADE.SG_PREMIUM:
			case NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL:
				return this.m_spBG_Premium;
			}
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x002A041C File Offset: 0x0029E61C
		private Sprite GetLineSprite(NKMSkinTemplet.SKIN_GRADE grade)
		{
			switch (grade)
			{
			default:
				return this.m_spLine_Variation;
			case NKMSkinTemplet.SKIN_GRADE.SG_NORMAL:
				return this.m_spLine_Normal;
			case NKMSkinTemplet.SKIN_GRADE.SG_RARE:
				return this.m_spLine_Rare;
			case NKMSkinTemplet.SKIN_GRADE.SG_PREMIUM:
			case NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL:
				return this.m_spLine_Premium;
			}
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x002A045B File Offset: 0x0029E65B
		[CompilerGenerated]
		internal static void <SetGoodImageFromSkinData>g__ActivateIcon|36_0(bool value, GameObject mainObj, GameObject beforeDot, ref NKCUIShopSlotSkin.<>c__DisplayClass36_0 A_3)
		{
			NKCUtil.SetGameobjectActive(beforeDot, value & A_3.bActivatedBeforeThis);
			NKCUtil.SetGameobjectActive(mainObj, value);
			A_3.bActivatedBeforeThis = (A_3.bActivatedBeforeThis || value);
		}

		// Token: 0x04006A16 RID: 27158
		public GameObject m_objVoice;

		// Token: 0x04006A17 RID: 27159
		public GameObject m_objVoiceDot;

		// Token: 0x04006A18 RID: 27160
		public GameObject m_objSkillCutin;

		// Token: 0x04006A19 RID: 27161
		public GameObject m_objSkillCutinDot;

		// Token: 0x04006A1A RID: 27162
		public Text m_lbUnitName;

		// Token: 0x04006A1B RID: 27163
		[Header("스킬 컷인 아이콘")]
		public Image m_imgSkillCutIn;

		// Token: 0x04006A1C RID: 27164
		public Image m_bgSkillCutIn;

		// Token: 0x04006A1D RID: 27165
		public Sprite m_spBG_CutinNormal;

		// Token: 0x04006A1E RID: 27166
		public Sprite m_spBG_CutinSpecial;

		// Token: 0x04006A1F RID: 27167
		public GameObject m_objCutInSpecial;

		// Token: 0x04006A20 RID: 27168
		[Header("전용 이펙트 보유 여부")]
		public GameObject m_ellipse03;

		// Token: 0x04006A21 RID: 27169
		public GameObject m_NKM_UI_SHOP_SKIN_SLOT_COMPONENT_ICON_SKIN_EFFECT;

		// Token: 0x04006A22 RID: 27170
		[Header("컷신 스토리")]
		public GameObject m_ellipse04;

		// Token: 0x04006A23 RID: 27171
		public GameObject m_NKM_UI_SHOP_SKIN_SLOT_COMPONENT_ICON_SKIN_STORY;

		// Token: 0x04006A24 RID: 27172
		[Header("로그인 애니")]
		public GameObject m_dotLoginAnim;

		// Token: 0x04006A25 RID: 27173
		public GameObject m_objComponentLoginAnim;

		// Token: 0x04006A26 RID: 27174
		[Header("추가 표정")]
		public GameObject m_dotLobbyFace;

		// Token: 0x04006A27 RID: 27175
		public GameObject m_objComponentLobbyFace;

		// Token: 0x04006A28 RID: 27176
		[Header("스킨 컨버전")]
		public GameObject m_dotConversion;

		// Token: 0x04006A29 RID: 27177
		public GameObject m_objComponentConversion;

		// Token: 0x04006A2A RID: 27178
		[Header("콜라보")]
		public GameObject m_dotCollab;

		// Token: 0x04006A2B RID: 27179
		public GameObject m_objComponentCollab;

		// Token: 0x04006A2C RID: 27180
		[Header("-건-")]
		public GameObject m_dotGauntlet;

		// Token: 0x04006A2D RID: 27181
		public GameObject m_objComponentGauntlet;

		// Token: 0x04006A2E RID: 27182
		[Header("배경")]
		public Image m_imgBackground;

		// Token: 0x04006A2F RID: 27183
		public Sprite m_spBG_Variation;

		// Token: 0x04006A30 RID: 27184
		public Sprite m_spBG_Normal;

		// Token: 0x04006A31 RID: 27185
		public Sprite m_spBG_Rare;

		// Token: 0x04006A32 RID: 27186
		public Sprite m_spBG_Premium;

		// Token: 0x04006A33 RID: 27187
		public GameObject m_objBG_Special;

		// Token: 0x04006A34 RID: 27188
		[Header("등급 라인")]
		public Image m_imgGradeLine;

		// Token: 0x04006A35 RID: 27189
		public Sprite m_spLine_Variation;

		// Token: 0x04006A36 RID: 27190
		public Sprite m_spLine_Normal;

		// Token: 0x04006A37 RID: 27191
		public Sprite m_spLine_Rare;

		// Token: 0x04006A38 RID: 27192
		public Sprite m_spLine_Premium;
	}
}
