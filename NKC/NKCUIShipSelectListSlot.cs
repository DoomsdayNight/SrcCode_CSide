using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009E6 RID: 2534
	public class NKCUIShipSelectListSlot : NKCUIUnitSelectListSlotBase
	{
		// Token: 0x06006D06 RID: 27910 RVA: 0x0023BAB8 File Offset: 0x00239CB8
		private void ProcessBanUI()
		{
			if (this.m_NKMUnitTempletBase != null)
			{
				if (this.m_bEnableShowBan && NKCBanManager.IsBanShip(this.m_NKMUnitTempletBase.m_ShipGroupID, this.m_eBanDataType))
				{
					NKCUtil.SetGameobjectActive(this.m_objBan, true);
					int shipBanLevel = NKCBanManager.GetShipBanLevel(this.m_NKMUnitTempletBase.m_ShipGroupID, this.m_eBanDataType);
					NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, shipBanLevel));
					int nerfPercentByShipBanLevel = NKMUnitStatManager.GetNerfPercentByShipBanLevel(shipBanLevel);
					NKCUtil.SetLabelText(this.m_lbBanApplyDesc, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_APPLY_DESC_ONE_PARAM, nerfPercentByShipBanLevel));
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objBan, false);
			}
		}

		// Token: 0x06006D07 RID: 27911 RVA: 0x0023BB60 File Offset: 0x00239D60
		public override void SetData(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			base.SetData(cNKMUnitData, deckIndex, bEnableLayoutElement, onSelectThisSlot);
			this.ProcessBanUI();
			if (cNKMUnitData != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, this.m_NKMUnitTempletBase.GetUnitTitle());
				NKCUtil.SetLabelText(this.m_lbShipClass, NKCUtilString.GetUnitStyleName(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE));
				if (this.m_imgShipClass != null)
				{
					this.m_imgShipClass.sprite = this.GetClassIcon(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				}
				if (this.m_imgShipMove != null)
				{
					this.m_imgShipMove.sprite = this.GetSpriteMoveType(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(cNKMUnitData);
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID);
				this.SetSkillSlot(unitTempletBase);
			}
			else
			{
				this.SetSkillSlot(null);
			}
			NKCUtil.SetGameobjectActive(this.m_objRootMainUnitMark, false);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
		}

		// Token: 0x06006D08 RID: 27912 RVA: 0x0023BC4C File Offset: 0x00239E4C
		public override void SetData(NKMUnitTempletBase templetBase, int levelToDisplay, int skinID, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			base.SetData(templetBase, levelToDisplay, skinID, bEnableLayoutElement, onSelectThisSlot);
			if (templetBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, this.m_NKMUnitTempletBase.GetUnitTitle());
				NKCUtil.SetLabelText(this.m_lbShipClass, NKCUtilString.GetUnitStyleName(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE));
				if (this.m_imgShipClass != null)
				{
					Sprite classIcon = this.GetClassIcon(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					NKCUtil.SetGameobjectActive(this.m_imgShipClass, classIcon != null);
					this.m_imgShipClass.sprite = classIcon;
				}
				if (this.m_imgShipMove != null)
				{
					Sprite spriteMoveType = this.GetSpriteMoveType(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					NKCUtil.SetGameobjectActive(this.m_imgShipMove, spriteMoveType != null);
					this.m_imgShipMove.sprite = spriteMoveType;
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(templetBase.m_StarGradeMax, 6, false);
				}
			}
			this.SetSkillSlot(templetBase);
			NKCUtil.SetGameobjectActive(this.m_objRootMainUnitMark, false);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
		}

		// Token: 0x06006D09 RID: 27913 RVA: 0x0023BD4C File Offset: 0x00239F4C
		public override void SetDataForBan(NKMUnitTempletBase templetBase, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bUp = false, bool bSetOriginalCost = false)
		{
			base.SetData(templetBase, 0, 0, bEnableLayoutElement, onSelectThisSlot);
			this.ProcessBanUI();
			if (templetBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, this.m_NKMUnitTempletBase.GetUnitTitle());
				NKCUtil.SetLabelText(this.m_lbShipClass, NKCUtilString.GetUnitStyleName(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE));
				if (this.m_imgShipClass != null)
				{
					Sprite classIcon = this.GetClassIcon(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					NKCUtil.SetGameobjectActive(this.m_imgShipClass, classIcon != null);
					this.m_imgShipClass.sprite = classIcon;
				}
				if (this.m_imgShipMove != null)
				{
					Sprite spriteMoveType = this.GetSpriteMoveType(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					NKCUtil.SetGameobjectActive(this.m_imgShipMove, spriteMoveType != null);
					this.m_imgShipMove.sprite = spriteMoveType;
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(templetBase.m_StarGradeMax, 6, false);
				}
			}
			this.SetSkillSlot(templetBase);
			NKCUtil.SetGameobjectActive(this.m_objRootMainUnitMark, false);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
		}

		// Token: 0x06006D0A RID: 27914 RVA: 0x0023BE50 File Offset: 0x0023A050
		public override void SetDataForCollection(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bEnable = false)
		{
			base.SetData(cNKMUnitData, deckIndex, true, onSelectThisSlot);
			if (cNKMUnitData != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, this.m_NKMUnitTempletBase.GetUnitTitle());
				NKCUtil.SetLabelText(this.m_lbShipClass, NKCUtilString.GetUnitStyleName(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE));
				if (this.m_imgShipClass != null)
				{
					this.m_imgShipClass.sprite = this.GetClassIcon(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				}
				if (this.m_imgShipMove != null)
				{
					this.m_imgShipMove.sprite = this.GetSpriteMoveType(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(6, 6, false);
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID);
				this.SetSkillSlot(unitTempletBase);
			}
			else
			{
				this.SetSkillSlot(null);
			}
			this.SetFierceBattleOtherBossAlreadyUsed(false);
			NKCUtil.SetGameobjectActive(this.m_objMaxExp, false);
			NKCUtil.SetGameobjectActive(this.m_objRootMainUnitMark, false);
			NKCUtil.SetGameobjectActive(this.m_objDisableSelectSlot, !bEnable);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_CARD_COLLECITON_DENIED, !bEnable);
		}

		// Token: 0x06006D0B RID: 27915 RVA: 0x0023BF61 File Offset: 0x0023A161
		public override void SetDataForRearm(NKMUnitData unitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bShowEqup = true, bool bShowLevel = false, bool bUnable = false)
		{
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x0023BF63 File Offset: 0x0023A163
		protected override void SetFierceBattleOtherBossAlreadyUsed(bool bVal)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_FIERCE_BATTLE, bVal);
		}

		// Token: 0x06006D0D RID: 27917 RVA: 0x0023BF71 File Offset: 0x0023A171
		public void SetMainShipMark()
		{
			NKCUtil.SetGameobjectActive(this.m_objRootMainUnitMark, true);
			NKCUtil.SetLabelText(this.m_lbMainUnitMark, NKCUtilString.GET_STRING_MAIN_SHIP);
			if (this.m_imgMainUnitMark != null)
			{
				this.m_imgMainUnitMark.color = this.m_colMainUnitMark;
			}
		}

		// Token: 0x06006D0E RID: 27918 RVA: 0x0023BFAE File Offset: 0x0023A1AE
		private Sprite GetClassIcon(NKM_UNIT_STYLE_TYPE classType)
		{
			return NKCResourceUtility.GetOrLoadUnitStyleIcon(classType, true);
		}

		// Token: 0x06006D0F RID: 27919 RVA: 0x0023BFB8 File Offset: 0x0023A1B8
		private Sprite GetSpriteMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string assetName;
			switch (type)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				assetName = "NKM_UI_SHIP_SELECT_LIST_SHIP_MOVETYPE_1";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				assetName = "NKM_UI_SHIP_SELECT_LIST_SHIP_MOVETYPE_4";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				assetName = "NKM_UI_SHIP_SELECT_LIST_SHIP_MOVETYPE_2";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				assetName = "NKM_UI_SHIP_SELECT_LIST_SHIP_MOVETYPE_3";
				break;
			default:
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_SHIP_SLOT_CARD_SPRITE", assetName, false);
		}

		// Token: 0x06006D10 RID: 27920 RVA: 0x0023C010 File Offset: 0x0023A210
		private void SetSkillSlot(NKMUnitTempletBase cNKMShipTemplet)
		{
			if (this.m_lstSkillSlot == null)
			{
				return;
			}
			if (cNKMShipTemplet != null)
			{
				for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
				{
					NKCUIShipSkillSlot nkcuishipSkillSlot = this.m_lstSkillSlot[i];
					NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(cNKMShipTemplet, i);
					if (shipSkillTempletByIndex != null)
					{
						NKCUtil.SetGameobjectActive(nkcuishipSkillSlot, true);
						nkcuishipSkillSlot.SetData(shipSkillTempletByIndex, NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcuishipSkillSlot, false);
					}
				}
				return;
			}
			foreach (NKCUIShipSkillSlot targetMono in this.m_lstSkillSlot)
			{
				NKCUtil.SetGameobjectActive(targetMono, false);
			}
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x0023C0B4 File Offset: 0x0023A2B4
		protected override void RestoreSprite()
		{
			if (this.m_spSSR == null)
			{
				this.m_spSSR = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_ship_slot_card_sprite", "NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_SSR", false);
			}
			if (this.m_spSR == null)
			{
				this.m_spSR = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_ship_slot_card_sprite", "NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_SR", false);
			}
			if (this.m_spR == null)
			{
				this.m_spR = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_ship_slot_card_sprite", "NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_R", false);
			}
			if (this.m_spN == null)
			{
				this.m_spN = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_ship_slot_card_sprite", "NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_N", false);
			}
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x0023C151 File Offset: 0x0023A351
		public override void SetRecall(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objRecall, bValue);
		}

		// Token: 0x040058E0 RID: 22752
		[Header("함선 전용 정보")]
		public Text m_lbTitle;

		// Token: 0x040058E1 RID: 22753
		public Text m_lbShipClass;

		// Token: 0x040058E2 RID: 22754
		public Image m_imgShipClass;

		// Token: 0x040058E3 RID: 22755
		public Image m_imgShipMove;

		// Token: 0x040058E4 RID: 22756
		[Header("대표 유닛 마크")]
		public GameObject m_objRootMainUnitMark;

		// Token: 0x040058E5 RID: 22757
		public Text m_lbMainUnitMark;

		// Token: 0x040058E6 RID: 22758
		public Image m_imgMainUnitMark;

		// Token: 0x040058E7 RID: 22759
		public Color m_colMainUnitMark;

		// Token: 0x040058E8 RID: 22760
		[Header("함선 스킬 슬롯")]
		public List<NKCUIShipSkillSlot> m_lstSkillSlot;

		// Token: 0x040058E9 RID: 22761
		[Header("함선 비활성화")]
		public GameObject m_NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_CARD_COLLECITON_DENIED;

		// Token: 0x040058EA RID: 22762
		[Header("격전지원 UI")]
		public GameObject m_NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_FIERCE_BATTLE;

		// Token: 0x040058EB RID: 22763
		public Text m_NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_FIERCE_BATTLE_TEXT;

		// Token: 0x040058EC RID: 22764
		[Header("리콜")]
		public GameObject m_objRecall;
	}
}
