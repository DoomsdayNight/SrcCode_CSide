using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000771 RID: 1905
	public class NKCUIShipInfoSummary : MonoBehaviour
	{
		// Token: 0x06004C02 RID: 19458 RVA: 0x0016BC20 File Offset: 0x00169E20
		public void SetShipData(NKMUnitData shipData, NKMUnitTempletBase shipTempletBase, bool bInDeck = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objNoDeckRoot, true);
			NKCUtil.SetGameobjectActive(this.m_lbDeckNumber, false);
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			if (shipTempletBase != null)
			{
				if (this.m_imgShipType != null)
				{
					Sprite shipTypeSprite = this.GetShipTypeSprite(shipTempletBase.m_NKM_UNIT_STYLE_TYPE, false);
					this.m_imgShipType.sprite = shipTypeSprite;
					NKCUtil.SetGameobjectActive(this.m_imgShipType, shipTypeSprite != null);
				}
				if (this.m_imgShipTypeSmall != null)
				{
					Sprite shipTypeSprite2 = this.GetShipTypeSprite(shipTempletBase.m_NKM_UNIT_STYLE_TYPE, true);
					this.m_imgShipTypeSmall.sprite = shipTypeSprite2;
					NKCUtil.SetGameobjectActive(this.m_imgShipTypeSmall, shipTypeSprite2 != null && !bInDeck);
				}
				NKCUtil.SetLabelText(this.m_lbShipType, NKCUtilString.GetUnitStyleName(shipTempletBase.m_NKM_UNIT_STYLE_TYPE));
				if (this.m_lbShipType != null && !bInDeck)
				{
					this.m_lbShipType.color = NKCUtil.GetColorForUnitGrade(shipTempletBase.m_NKM_UNIT_GRADE);
				}
				NKCUtil.SetLabelText(this.m_lbName, shipTempletBase.GetUnitName());
				this.SetGrade(shipTempletBase.m_NKM_UNIT_GRADE);
				bool flag = false;
				NKCUtil.SetGameobjectActive(this.m_objPassive, true);
				int num = 0;
				for (int i = 0; i < shipTempletBase.GetSkillCount(); i++)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(shipTempletBase, i);
					if (shipSkillTempletByIndex != null)
					{
						if (num < this.m_imgSkillIcon.Count && shipSkillTempletByIndex.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_PASSIVE)
						{
							this.m_imgSkillIcon[num].enabled = true;
							this.m_imgSkillIcon[num].sprite = NKCUtil.GetSkillIconSprite(shipSkillTempletByIndex);
							num++;
						}
						if (!flag)
						{
							flag = true;
							NKCUtil.SetLabelText(this.m_lbPassive, shipSkillTempletByIndex.GetBuildDesc());
						}
					}
				}
				if (!flag)
				{
					NKCUtil.SetLabelText(this.m_lbPassive, NKCStringTable.GetString("SI_MENU_EXCEPTION_SHIP_PASSIVE_EMPTY", false));
				}
				for (int j = 0; j < shipTempletBase.GetSkillCount(); j++)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex2 = NKMShipSkillManager.GetShipSkillTempletByIndex(shipTempletBase, j);
					if (shipSkillTempletByIndex2 != null && num < this.m_imgSkillIcon.Count && shipSkillTempletByIndex2.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_PASSIVE)
					{
						this.m_imgSkillIcon[num].enabled = true;
						this.m_imgSkillIcon[num].sprite = NKCUtil.GetSkillIconSprite(shipSkillTempletByIndex2);
						num++;
					}
				}
				for (int k = num; k < this.m_imgSkillIcon.Count; k++)
				{
					this.m_imgSkillIcon[k].enabled = false;
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_READY || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT)
				{
					bool flag2 = NKCBanManager.IsBanShip(shipTempletBase.m_ShipGroupID, NKCBanManager.BAN_DATA_TYPE.FINAL);
					bool flag3 = NKCUtil.CheckPossibleShowBan(NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady);
					if (flag2 && flag3)
					{
						NKCUtil.SetGameobjectActive(this.m_objBan, true);
						int shipBanLevel = NKCBanManager.GetShipBanLevel(shipTempletBase.m_ShipGroupID, NKCBanManager.BAN_DATA_TYPE.FINAL);
						NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, shipBanLevel));
						NKCUtil.SetLabelTextColor(this.m_lbBanLevel, Color.red);
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgShipType, false);
				NKCUtil.SetGameobjectActive(this.m_imgShipTypeSmall, false);
				NKCUtil.SetLabelText(this.m_lbShipType, "");
				NKCUtil.SetLabelText(this.m_lbPassive, "");
				NKCUtil.SetLabelText(this.m_lbName, NKCUtilString.GET_STRING_UNKNOWN);
				NKCUtil.SetGameobjectActive(this.m_objPassive, false);
				for (int l = 0; l < this.m_imgSkillIcon.Count; l++)
				{
					this.m_imgSkillIcon[l].enabled = false;
				}
			}
			if (shipData != null)
			{
				if (this.m_lbLevel != null)
				{
					NKCUIComTextUnitLevel nkcuicomTextUnitLevel = this.m_lbLevel as NKCUIComTextUnitLevel;
					if (nkcuicomTextUnitLevel != null)
					{
						nkcuicomTextUnitLevel.SetLevel(shipData, 0, Array.Empty<Text>());
						NKCUtil.SetLabelText(nkcuicomTextUnitLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, nkcuicomTextUnitLevel.text));
					}
					else
					{
						this.m_lbLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, shipData.m_UnitLevel.ToString());
					}
				}
				int unitMaxLevel = NKCExpManager.GetUnitMaxLevel(shipData);
				NKCUtil.SetLabelText(this.m_lbMaxLevel, string.Format("/ {0}", unitMaxLevel));
				NKCUtil.SetGameobjectActive(this.m_objMaxLevel, shipData.m_UnitLevel >= unitMaxLevel);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbLevel, "");
				NKCUtil.SetLabelText(this.m_lbMaxLevel, "");
				NKCUtil.SetGameobjectActive(this.m_objMaxLevel, false);
			}
			NKCUIComStarRank starRank = this.m_StarRank;
			if (starRank == null)
			{
				return;
			}
			starRank.SetStarRank(shipData);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0016C068 File Offset: 0x0016A268
		private void SetGrade(NKM_UNIT_GRADE grade)
		{
			for (int i = 0; i < this.m_lstGrade.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstGrade[i], i == (int)grade);
			}
			string assetName;
			switch (grade)
			{
			default:
				assetName = "NKM_UI_COMMON_RANK_N";
				break;
			case NKM_UNIT_GRADE.NUG_R:
				assetName = "NKM_UI_COMMON_RANK_R";
				break;
			case NKM_UNIT_GRADE.NUG_SR:
				assetName = "NKM_UI_COMMON_RANK_SR";
				break;
			case NKM_UNIT_GRADE.NUG_SSR:
				assetName = "NKM_UI_COMMON_RANK_SSR";
				break;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_ICON", assetName, false);
			if (orLoadAssetResource == null)
			{
				Debug.LogError("Rarity sprite not found!");
			}
			NKCUtil.SetImageSprite(this.m_imgGrade, orLoadAssetResource, true);
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0016C101 File Offset: 0x0016A301
		public void SetShipData(NKMUnitData shipData, NKMUnitTempletBase shipTempletBase, NKMDeckIndex deckIndex, bool bInDeck = false)
		{
			this.SetShipData(shipData, shipTempletBase, bInDeck);
			this.SetDeckNumber(deckIndex);
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x0016C114 File Offset: 0x0016A314
		private Sprite GetShipTypeSprite(NKM_UNIT_STYLE_TYPE type, bool isSmall)
		{
			return NKCResourceUtility.GetOrLoadUnitStyleIcon(type, isSmall);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x0016C120 File Offset: 0x0016A320
		public void SetDeckNumber(NKMDeckIndex deckIndex)
		{
			if (this.m_objNoDeckRoot != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoDeckRoot.transform.parent, true);
			}
			if (deckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_NORMAL)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoDeckRoot, false);
				NKCUtil.SetGameobjectActive(this.m_lbDeckNumber, true);
				NKCUtil.SetLabelText(this.m_lbDeckNumber, string.Format(NKCUtilString.GET_STRING_SQUAD_ONE_PARAM, NKCUtilString.GetDeckNumberString(deckIndex)));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNoDeckRoot, true);
			NKCUtil.SetGameobjectActive(this.m_lbDeckNumber, false);
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x0016C1A6 File Offset: 0x0016A3A6
		public void HideDeckNumber()
		{
			if (this.m_objNoDeckRoot != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoDeckRoot.transform.parent, false);
			}
		}

		// Token: 0x04003A67 RID: 14951
		public GameObject m_objNoDeckRoot;

		// Token: 0x04003A68 RID: 14952
		public Text m_lbDeckNumber;

		// Token: 0x04003A69 RID: 14953
		public Image m_imgShipType;

		// Token: 0x04003A6A RID: 14954
		public Image m_imgShipTypeSmall;

		// Token: 0x04003A6B RID: 14955
		public Text m_lbShipType;

		// Token: 0x04003A6C RID: 14956
		public Text m_lbName;

		// Token: 0x04003A6D RID: 14957
		public Image m_imgGrade;

		// Token: 0x04003A6E RID: 14958
		public List<GameObject> m_lstGrade;

		// Token: 0x04003A6F RID: 14959
		public NKCUIComStarRank m_StarRank;

		// Token: 0x04003A70 RID: 14960
		public Text m_lbLevel;

		// Token: 0x04003A71 RID: 14961
		public Text m_lbMaxLevel;

		// Token: 0x04003A72 RID: 14962
		public GameObject m_objMaxLevel;

		// Token: 0x04003A73 RID: 14963
		public List<Image> m_imgSkillIcon;

		// Token: 0x04003A74 RID: 14964
		public GameObject m_objPassive;

		// Token: 0x04003A75 RID: 14965
		public Text m_lbPassive;

		// Token: 0x04003A76 RID: 14966
		public GameObject m_objBan;

		// Token: 0x04003A77 RID: 14967
		public Text m_lbBanLevel;
	}
}
