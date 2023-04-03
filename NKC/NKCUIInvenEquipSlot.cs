using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009A9 RID: 2473
	public class NKCUIInvenEquipSlot : MonoBehaviour
	{
		// Token: 0x06006704 RID: 26372 RVA: 0x00210631 File Offset: 0x0020E831
		public NKMEquipItemData GetNKMEquipItemData()
		{
			return this.m_cNKMEquipItemData;
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x00210639 File Offset: 0x0020E839
		public NKMEquipTemplet GetNKMEquipTemplet()
		{
			return this.m_cNKMEquipTemplet;
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x00210641 File Offset: 0x0020E841
		public NKCUIInvenEquipSlot.EQUIP_SLOT_STATE Get_EQUIP_SLOT_STATE()
		{
			return this.m_EQUIP_SLOT_STATE;
		}

		// Token: 0x06006707 RID: 26375 RVA: 0x00210649 File Offset: 0x0020E849
		public bool IsActive()
		{
			return base.gameObject.activeInHierarchy;
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x00210656 File Offset: 0x0020E856
		public void SetActive(bool bSet)
		{
			if (base.gameObject.activeSelf == !bSet)
			{
				base.gameObject.SetActive(bSet);
			}
		}

		// Token: 0x06006709 RID: 26377 RVA: 0x00210675 File Offset: 0x0020E875
		private string GetBGIconName(NKM_ITEM_GRADE eNKM_ITEM_GRADE)
		{
			if (eNKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_N)
			{
				return "AB_UI_ITEM_EQUIP_SLOT_CARD_N";
			}
			if (eNKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_R)
			{
				return "AB_UI_ITEM_EQUIP_SLOT_CARD_R";
			}
			if (eNKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_SR)
			{
				return "AB_UI_ITEM_EQUIP_SLOT_CARD_SR";
			}
			if (eNKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_SSR)
			{
				return "AB_UI_ITEM_EQUIP_SLOT_CARD_SSR";
			}
			return "";
		}

		// Token: 0x0600670A RID: 26378 RVA: 0x002106A3 File Offset: 0x0020E8A3
		private string GetGradeIconName(NKM_ITEM_GRADE grade)
		{
			switch (grade)
			{
			case NKM_ITEM_GRADE.NIG_N:
				return "AB_UI_ITEM_EQUIP_SLOT_N";
			case NKM_ITEM_GRADE.NIG_R:
				return "AB_UI_ITEM_EQUIP_SLOT_R";
			case NKM_ITEM_GRADE.NIG_SR:
				return "AB_UI_ITEM_EQUIP_SLOT_SR";
			case NKM_ITEM_GRADE.NIG_SSR:
				return "AB_UI_ITEM_EQUIP_SLOT_SSR";
			default:
				return "";
			}
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x002106DA File Offset: 0x0020E8DA
		public void SetHighlightOnlyOneStatColor(int index)
		{
			NKCUtil.SetGameobjectActive(this.NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_01_SELECT, index == 0);
			NKCUtil.SetGameobjectActive(this.NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_02_SELECT, index == 1);
			NKCUtil.SetGameobjectActive(this.NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_03_SELECT, index == 2);
			NKCUtil.SetGameobjectActive(this.NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_RELIC_SELECT, index == 3);
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x00210718 File Offset: 0x0020E918
		public void SetEmpty(NKCUISlotEquip.OnSelectedEquipSlot selectedSlot = null, NKMEquipItemData cNKMEquipItemData = null)
		{
			if (cNKMEquipItemData != null)
			{
				this.m_cNKMEquipItemData = cNKMEquipItemData;
			}
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x00210724 File Offset: 0x0020E924
		public void SetData(NKMEquipItemData cNKMEquipItemData, bool bShowFierceInfo = false, bool bPresetContained = false)
		{
			if (cNKMEquipItemData == null)
			{
				return;
			}
			this.m_cNKMEquipItemData = cNKMEquipItemData;
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(cNKMEquipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_ITEM_EQUIP_SLOT_NOT_EMPTY, true);
			this.m_cNKMEquipTemplet = equipTemplet;
			this.m_NKM_UI_ITEM_EQUIP_SLOT_BG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_ITEM_EQUIP_SLOT_CARD_SPRITE", this.GetBGIconName(equipTemplet.m_NKM_ITEM_GRADE), false);
			NKCUtil.SetImageSprite(this.m_imgGrade, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_ITEM_EQUIP_SLOT_CARD_SPRITE", this.GetGradeIconName(equipTemplet.m_NKM_ITEM_GRADE), false), false);
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_NAME, equipTemplet.GetItemName());
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_TIER_TEXT, NKCUtilString.GetItemEquipTier(equipTemplet.m_NKM_ITEM_TIER));
			NKCUtil.SetGameobjectActive(this.m_objTier_7, equipTemplet.m_bShowEffect);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(equipTemplet.GetPrivateUnitID());
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbEquipReqUnit, "- " + NKCUtilString.GetEquipPositionStringByUnitStyle(equipTemplet, false));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbEquipReqUnit, "");
			}
			NKCUtil.SetLabelText(this.m_lbEquipType, "- " + NKCUtilString.GetEquipPositionStringByUnitStyle(equipTemplet, unitTempletBase != null));
			if (cNKMEquipItemData.m_EnchantLevel > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_ITEM_EQUIP_SLOT_REINFORCE, true);
				this.m_NKM_UI_ITEM_EQUIP_SLOT_REINFORCE_TEXT.text = "+" + cNKMEquipItemData.m_EnchantLevel.ToString();
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_ITEM_EQUIP_SLOT_REINFORCE, false);
				this.m_NKM_UI_ITEM_EQUIP_SLOT_REINFORCE_TEXT.text = "";
			}
			NKCUtil.SetImageSprite(this.m_NKM_UI_ITEM_EQUIP_SLOT_ICON, NKCResourceUtility.GetOrLoadEquipIcon(equipTemplet), false);
			NKCUtil.SetGameobjectActive(this.m_objRelic, equipTemplet.IsRelic());
			if (equipTemplet.IsRelic())
			{
				for (int i = 0; i < this.m_lstRelicImg.Count; i++)
				{
					if (cNKMEquipItemData.potentialOption == null || i >= cNKMEquipItemData.potentialOption.sockets.Length)
					{
						NKCUtil.SetGameobjectActive(this.m_lstRelicImg[i], false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstRelicImg[i], cNKMEquipItemData.potentialOption.sockets[i] != null);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_INFO, equipTemplet.m_EquipUnitStyleType != NKM_UNIT_STYLE_TYPE.NUST_ENCHANT);
			if (this.NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_INFO.gameObject.activeSelf)
			{
				NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(cNKMEquipItemData.m_SetOptionId);
				if (equipSetOptionTemplet != null)
				{
					NKCUtil.SetGameobjectActive(this.m_SET_TEXT01_DOT, true);
					NKCUtil.SetGameobjectActive(this.m_SET_TEXT01_TEXT, true);
					NKCUtil.SetImageSprite(this.m_NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_ICON, NKCUtil.GetSpriteEquipSetOptionIcon(equipSetOptionTemplet), false);
					int num = 0;
					if (cNKMEquipItemData.m_OwnerUnitUID > 0L)
					{
						num = NKMItemManager.GetMatchingSetOptionItem(cNKMEquipItemData);
					}
					NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_NAME, string.Format("{0} ({1}/{2})", NKCStringTable.GetString(equipSetOptionTemplet.m_EquipSetName, false), num, equipSetOptionTemplet.m_EquipSetPart));
					string setOptionDescription = NKMItemManager.GetSetOptionDescription(equipSetOptionTemplet.m_StatType_1, equipSetOptionTemplet.m_StatRate_1, equipSetOptionTemplet.m_StatValue_1);
					NKCUtil.SetLabelText(this.m_SET_TEXT01_TEXT, setOptionDescription);
					if (equipSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM)
					{
						string setOptionDescription2 = NKMItemManager.GetSetOptionDescription(equipSetOptionTemplet.m_StatType_2, equipSetOptionTemplet.m_StatRate_2, equipSetOptionTemplet.m_StatValue_2);
						NKCUtil.SetLabelText(this.m_SET_TEXT02_TEXT, setOptionDescription2);
					}
					Color color;
					if (NKMItemManager.IsActiveSetOptionItem(cNKMEquipItemData))
					{
						color = NKCUtil.GetColor("#FFFFFF");
					}
					else
					{
						color = NKCUtil.GetColor("#656565");
					}
					NKCUtil.SetImageColor(this.m_SET_TEXT01_DOT, color);
					NKCUtil.SetImageColor(this.m_SET_TEXT02_DOT, color);
					NKCUtil.SetGameobjectActive(this.m_SET_TEXT02_DOT, equipSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM);
					NKCUtil.SetGameobjectActive(this.m_SET_TEXT02_TEXT, equipSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_ICON, NKCUtil.GetSpriteEquipSetOptionIcon(null), false);
					NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_NAME, "");
					NKCUtil.SetGameobjectActive(this.m_SET_TEXT01_DOT, false);
					NKCUtil.SetGameobjectActive(this.m_SET_TEXT02_DOT, false);
					NKCUtil.SetGameobjectActive(this.m_SET_TEXT01_TEXT, false);
					NKCUtil.SetGameobjectActive(this.m_SET_TEXT02_TEXT, false);
				}
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01, "");
			NKCUtil.SetLabelTextColor(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01, NKCUIInvenEquipSlot.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_ORG_Color);
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_RELIC, "");
			NKCUtil.SetLabelTextColor(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_RELIC, NKCUIInvenEquipSlot.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_RELIC_ORG_Color);
			NKCUtil.SetGameobjectActive(this.m_ObjStat_RELIC, this.m_cNKMEquipTemplet.IsRelic());
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02, "");
			NKCUtil.SetLabelTextColor(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02, NKCUIInvenEquipSlot.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02_ORG_Color);
			NKCUtil.SetGameobjectActive(this.m_ObjStat_02, this.m_cNKMEquipTemplet.m_StatGroupID != 0);
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_03, "");
			NKCUtil.SetLabelTextColor(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_03, NKCUIInvenEquipSlot.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02_ORG_Color);
			NKCUtil.SetGameobjectActive(this.m_ObjStat_03, this.m_cNKMEquipTemplet.m_StatGroupID_2 != 0);
			if (equipTemplet.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_ENCHANT)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01, NKCStringTable.GetString("SI_DP_STAT_SHORT_NAME_FOR_INVEN_EQUIP_ENCHANT", false));
				this.SetColor(0, NKCUIInvenEquipSlot.eOptionType.MainStat);
			}
			else
			{
				for (int j = 0; j < cNKMEquipItemData.m_Stat.Count; j++)
				{
					EQUIP_ITEM_STAT statData = cNKMEquipItemData.m_Stat[j];
					if (j == 0)
					{
						this.SetItemStatText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01, cNKMEquipItemData, statData, j);
					}
					else if (j == 1)
					{
						this.SetItemStatText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02, cNKMEquipItemData, statData, j);
					}
					else if (j == 2)
					{
						this.SetItemStatText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_03, cNKMEquipItemData, statData, j);
					}
					if (j == 0)
					{
						this.SetColor(j, NKCUIInvenEquipSlot.eOptionType.MainStat);
					}
					else if (!NKMEquipTuningManager.IsChangeableStatGroup((j == 1) ? this.m_cNKMEquipTemplet.m_StatGroupID : this.m_cNKMEquipTemplet.m_StatGroupID_2))
					{
						this.SetColor(j, NKCUIInvenEquipSlot.eOptionType.ExclusiveStat);
					}
					else
					{
						this.SetColor(j, NKCUIInvenEquipSlot.eOptionType.ModifiableStat);
					}
				}
				if (this.m_cNKMEquipTemplet.IsRelic())
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_RELIC, NKCUtil.GetPotentialStatText(cNKMEquipItemData));
					if (this.m_objSocketSymbol != null)
					{
						int num2 = this.m_objSocketSymbol.Length;
						for (int k = 0; k < num2; k++)
						{
							if (cNKMEquipItemData.potentialOption == null || cNKMEquipItemData.potentialOption.sockets.Length <= k)
							{
								NKCUtil.SetGameobjectActive(this.m_objSocketSymbol[k], false);
							}
							else
							{
								NKCUtil.SetGameobjectActive(this.m_objSocketSymbol[k], cNKMEquipItemData.potentialOption.sockets[k] != null);
							}
						}
					}
				}
			}
			bool flag = false;
			if (cNKMEquipItemData.m_OwnerUnitUID > 0L)
			{
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(cNKMEquipItemData.m_OwnerUnitUID);
				if (unitFromUID != null)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
					if (unitTempletBase2 != null)
					{
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_ITEM_EQUIP_SLOT_USED, true);
						flag = true;
						NKCUtil.SetImageSprite(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_USED_UNIT, NKCResourceUtility.GetOrLoadMinimapFaceIcon(unitTempletBase2, false), false);
						NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_USED_UNIT_DESC, unitTempletBase2.GetUnitName() + " " + NKCStringTable.GetString("SI_PF_FILTER_EQUIP_2", false));
					}
				}
			}
			if (!flag)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_ITEM_EQUIP_SLOT_USED, false);
			}
			NKCUtil.SetLabelText(this.m_lbEquipDesc, equipTemplet.GetItemDesc());
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_ITEM_EQUIP_SLOT_LOCK, this.m_cNKMEquipItemData.m_bLock);
			this.m_srDetail.normalizedPosition = Vector2.one;
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x00210DF4 File Offset: 0x0020EFF4
		private void SetItemStatText(Text label, NKMEquipItemData cNKMEquipItemData, EQUIP_ITEM_STAT statData, int idx = -1)
		{
			if (label == null || cNKMEquipItemData == null || statData == null)
			{
				return;
			}
			bool bPercentStat = false;
			if (idx > 0)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(cNKMEquipItemData.m_ItemEquipID);
				if (equipTemplet == null)
				{
					goto IL_82;
				}
				using (IEnumerator<NKMEquipRandomStatTemplet> enumerator = NKMEquipTuningManager.GetEquipRandomStatGroupList((idx == 1) ? equipTemplet.m_StatGroupID : equipTemplet.m_StatGroupID_2).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMEquipRandomStatTemplet nkmequipRandomStatTemplet = enumerator.Current;
						if (nkmequipRandomStatTemplet.m_StatType == statData.type)
						{
							bPercentStat = NKCUIForgeTuning.IsPercentStat(nkmequipRandomStatTemplet);
						}
					}
					goto IL_82;
				}
			}
			bPercentStat = NKMUnitStatManager.IsPercentStat(statData.type);
			IL_82:
			if (statData.type == NKM_STAT_TYPE.NST_RANDOM)
			{
				label.text = NKCUtilString.GET_STRING_INVEN_RANDOM_OPTION;
				return;
			}
			if (statData.stat_value == 0f && statData.stat_factor == 0f)
			{
				label.text = NKCUtilString.GetStatShortName(statData.type);
				return;
			}
			NKCUtil.SetLabelText(label, NKCUIForgeTuning.GetTuningOptionStatString(statData, cNKMEquipItemData, bPercentStat));
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x00210EE4 File Offset: 0x0020F0E4
		private void SetColor(int idx, NKCUIInvenEquipSlot.eOptionType type)
		{
			Color color = Color.white;
			switch (type)
			{
			case NKCUIInvenEquipSlot.eOptionType.ModifiableStat:
				color = NKCUtil.GetColor("#CD9833");
				break;
			case NKCUIInvenEquipSlot.eOptionType.ExclusiveStat:
				color = NKCUtil.GetColor("#9A3FA8");
				break;
			case NKCUIInvenEquipSlot.eOptionType.RelicStat:
				color = NKCUtil.GetColor("#C2C2C2");
				break;
			}
			switch (idx)
			{
			case 0:
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01, color);
				return;
			case 1:
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02, color);
				NKCUtil.SetImageColor(this.m_imgStat_02_Num, color);
				NKCUtil.SetGameobjectActive(this.m_imgStat_02_Lock, type == NKCUIInvenEquipSlot.eOptionType.ExclusiveStat);
				return;
			case 2:
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_03, color);
				NKCUtil.SetImageColor(this.m_imgStat_03_Num, color);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x00210F94 File Offset: 0x0020F194
		public long GetEquipItemUID()
		{
			if (this.m_cNKMEquipItemData != null)
			{
				return this.m_cNKMEquipItemData.m_ItemUid;
			}
			return 0L;
		}

		// Token: 0x04005305 RID: 21253
		public Image m_NKM_UI_ITEM_EQUIP_SLOT_BG;

		// Token: 0x04005306 RID: 21254
		public Image m_NKM_UI_ITEM_EQUIP_SLOT_ICON;

		// Token: 0x04005307 RID: 21255
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_NAME;

		// Token: 0x04005308 RID: 21256
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_TIER_TEXT;

		// Token: 0x04005309 RID: 21257
		[Space]
		public Text m_lbEquipType;

		// Token: 0x0400530A RID: 21258
		public Text m_lbEquipReqUnit;

		// Token: 0x0400530B RID: 21259
		public Image m_imgGrade;

		// Token: 0x0400530C RID: 21260
		public GameObject m_objTier_7;

		// Token: 0x0400530D RID: 21261
		[Space]
		public GameObject m_NKM_UI_ITEM_EQUIP_SLOT_REINFORCE;

		// Token: 0x0400530E RID: 21262
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_REINFORCE_TEXT;

		// Token: 0x0400530F RID: 21263
		[Space]
		public GameObject m_ObjStat_01;

		// Token: 0x04005310 RID: 21264
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01;

		// Token: 0x04005311 RID: 21265
		private static Color m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_ORG_Color = new Color(0.7058824f, 0.7058824f, 0.7058824f, 1f);

		// Token: 0x04005312 RID: 21266
		public GameObject NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_01_SELECT;

		// Token: 0x04005313 RID: 21267
		public GameObject m_ObjStat_RELIC;

		// Token: 0x04005314 RID: 21268
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_RELIC;

		// Token: 0x04005315 RID: 21269
		private static Color m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_RELIC_ORG_Color = new Color(0.7058824f, 0.7058824f, 0.7058824f, 1f);

		// Token: 0x04005316 RID: 21270
		public GameObject NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_RELIC_SELECT;

		// Token: 0x04005317 RID: 21271
		public GameObject m_ObjStat_02;

		// Token: 0x04005318 RID: 21272
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02;

		// Token: 0x04005319 RID: 21273
		private static Color m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02_ORG_Color = new Color(0.7058824f, 0.7058824f, 0.7058824f, 1f);

		// Token: 0x0400531A RID: 21274
		public Image m_imgStat_02_Num;

		// Token: 0x0400531B RID: 21275
		public Image m_imgStat_02_Lock;

		// Token: 0x0400531C RID: 21276
		public GameObject NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_02_SELECT;

		// Token: 0x0400531D RID: 21277
		public GameObject m_ObjStat_03;

		// Token: 0x0400531E RID: 21278
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_03;

		// Token: 0x0400531F RID: 21279
		private static Color m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_03_ORG_Color = new Color(0.7058824f, 0.7058824f, 0.7058824f, 1f);

		// Token: 0x04005320 RID: 21280
		public Image m_imgStat_03_Num;

		// Token: 0x04005321 RID: 21281
		public GameObject NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_03_SELECT;

		// Token: 0x04005322 RID: 21282
		[Header("렐릭")]
		public GameObject m_objRelic;

		// Token: 0x04005323 RID: 21283
		public List<Image> m_lstRelicImg = new List<Image>();

		// Token: 0x04005324 RID: 21284
		public GameObject[] m_objSocketSymbol;

		// Token: 0x04005325 RID: 21285
		[Header("장착중 유닛")]
		public GameObject m_NKM_UI_ITEM_EQUIP_SLOT_USED;

		// Token: 0x04005326 RID: 21286
		public Image m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_USED_UNIT;

		// Token: 0x04005327 RID: 21287
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_USED_UNIT_DESC;

		// Token: 0x04005328 RID: 21288
		[Header("세트옵션")]
		public GameObject NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_INFO;

		// Token: 0x04005329 RID: 21289
		public Image m_NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_ICON;

		// Token: 0x0400532A RID: 21290
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_BOTTOM_SET_NAME;

		// Token: 0x0400532B RID: 21291
		public Image m_SET_TEXT01_DOT;

		// Token: 0x0400532C RID: 21292
		public Text m_SET_TEXT01_TEXT;

		// Token: 0x0400532D RID: 21293
		public Image m_SET_TEXT02_DOT;

		// Token: 0x0400532E RID: 21294
		public Text m_SET_TEXT02_TEXT;

		// Token: 0x0400532F RID: 21295
		[Header("장비 설명")]
		public Text m_lbEquipDesc;

		// Token: 0x04005330 RID: 21296
		[Header("스크롤")]
		public ScrollRect m_srDetail;

		// Token: 0x04005331 RID: 21297
		[Space]
		public GameObject m_NKM_UI_ITEM_EQUIP_SLOT_NOT_EMPTY;

		// Token: 0x04005332 RID: 21298
		public GameObject m_NKM_UI_ITEM_EQUIP_SLOT_LOCK;

		// Token: 0x04005333 RID: 21299
		private NKMEquipItemData m_cNKMEquipItemData;

		// Token: 0x04005334 RID: 21300
		private NKMEquipTemplet m_cNKMEquipTemplet;

		// Token: 0x04005335 RID: 21301
		private NKCUIInvenEquipSlot.EQUIP_SLOT_STATE m_EQUIP_SLOT_STATE;

		// Token: 0x02001680 RID: 5760
		public enum EQUIP_SLOT_STATE
		{
			// Token: 0x0400A484 RID: 42116
			ESS_NONE,
			// Token: 0x0400A485 RID: 42117
			ESS_SELECTED,
			// Token: 0x0400A486 RID: 42118
			ESS_DELETE
		}

		// Token: 0x02001681 RID: 5761
		private enum eOptionType
		{
			// Token: 0x0400A488 RID: 42120
			MainStat,
			// Token: 0x0400A489 RID: 42121
			ModifiableStat,
			// Token: 0x0400A48A RID: 42122
			ExclusiveStat,
			// Token: 0x0400A48B RID: 42123
			RelicStat
		}
	}
}
