using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C51 RID: 3153
	public class NKCUICharInfoSummary : MonoBehaviour
	{
		// Token: 0x060092D5 RID: 37589 RVA: 0x00321C31 File Offset: 0x0031FE31
		public void SetUnitClassRootActive(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objUnitClassRoot, value);
		}

		// Token: 0x060092D6 RID: 37590 RVA: 0x00321C40 File Offset: 0x0031FE40
		public void Init(bool bShowLevel = true)
		{
			if (null != this.m_NKM_UI_UNIT_CLASS)
			{
				this.m_NKM_UI_UNIT_CLASS.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_UNIT_CLASS.PointerClick.AddListener(new UnityAction(this.OpenInfo));
			}
			if (null != this.m_NKM_UI_UNIT_BATTLE_TYPE)
			{
				this.m_NKM_UI_UNIT_BATTLE_TYPE.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_UNIT_BATTLE_TYPE.PointerClick.AddListener(new UnityAction(this.OpenInfo));
			}
			if (null != this.m_NKM_UI_UNIT_BATTLE_TYPE_ATK)
			{
				this.m_NKM_UI_UNIT_BATTLE_TYPE_ATK.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_UNIT_BATTLE_TYPE_ATK.PointerClick.AddListener(new UnityAction(this.OpenInfo));
			}
			if (null != this.m_NKM_UI_UNIT_DEFENCE_TYPE)
			{
				this.m_NKM_UI_UNIT_DEFENCE_TYPE.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_UNIT_DEFENCE_TYPE.PointerClick.AddListener(new UnityAction(this.OpenInfo));
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL, bShowLevel);
		}

		// Token: 0x060092D7 RID: 37591 RVA: 0x00321D44 File Offset: 0x0031FF44
		public void SetData(NKMUnitData unitData)
		{
			if (unitData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objUnitClassRoot, true);
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
				NKCUtil.SetGameobjectActive(this.m_objClassIconCounter, unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_COUNTER);
				NKCUtil.SetGameobjectActive(this.m_objClassIconMechanic, unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_MECHANIC);
				NKCUtil.SetGameobjectActive(this.m_objClassIconSolider, unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_SOLDIER);
				NKCUtil.SetGameobjectActive(this.m_objClassIconETC, unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER);
				NKCUtil.SetLabelText(this.m_lbClass, NKCUtilString.GetUnitStyleMarkString(unitTempletBase));
				if (this.m_lbClass != null)
				{
					this.m_lbClass.color = NKCUtil.GetColorForUnitGrade(unitTempletBase.m_NKM_UNIT_GRADE);
				}
				string unitAbilityName = NKCUtilString.GetUnitAbilityName(unitData.m_UnitID, "   ");
				NKCUtil.SetGameobjectActive(this.m_objTag, !string.IsNullOrEmpty(unitAbilityName));
				NKCUtil.SetLabelText(this.m_lbTag, unitAbilityName);
				this.SetUnitRank(unitTempletBase.m_NKM_UNIT_GRADE, unitTempletBase.m_bAwaken);
				NKCUtil.SetLabelText(this.m_lbCodename, unitTempletBase.GetUnitTitle());
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
				NKCUtil.SetLabelText(this.m_lbCost, (unitStatTemplet != null) ? unitStatTemplet.GetRespawnCost(false, null, null).ToString() : "-");
				NKCUtil.SetLabelText(this.m_lbName, unitTempletBase.GetUnitName() + NKCUtilString.GetRespawnCountText(unitData.m_UnitID));
				NKCUIComTextUnitLevel lbLevel = this.m_lbLevel;
				if (lbLevel != null)
				{
					lbLevel.SetLevel(unitData, 0, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, Array.Empty<Text>());
				}
				NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo((int)unitData.m_LimitBreakLevel);
				if (lbinfo != null)
				{
					NKCUtil.SetLabelText(this.m_lbMaxLevel, "/" + lbinfo.m_iMaxLevel.ToString());
				}
				NKCUtil.SetLabelText(this.m_lbPowerSummary, unitData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, null, null).ToString());
				NKCUtil.SetGameobjectActive(this.m_imgExpBar, true);
				if (this.m_imgExpBar != null)
				{
					if (NKCExpManager.GetUnitMaxLevel(unitData) == unitData.m_UnitLevel)
					{
						this.m_imgExpBar.fillAmount = 1f;
					}
					else
					{
						this.m_imgExpBar.fillAmount = NKCExpManager.GetUnitNextLevelExpProgress(unitData);
					}
				}
				NKCUtil.SetLabelText(this.m_lbExp, string.Format("{0}/{1}", NKCExpManager.GetCurrentExp(unitData), NKCExpManager.GetRequiredExp(unitData)));
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(unitData);
				}
				if (this.m_tacticUpdateLvSlot != null)
				{
					this.m_tacticUpdateLvSlot.SetLevel(unitData.tacticLevel, false);
					NKCUtil.SetGameobjectActive(this.m_tacticUpdateLvSlot.gameObject, true);
				}
				this.m_UnitInfo = unitData;
				this.SetUnitInfo(unitData.m_UnitID);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objUnitClassRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objClassIconCounter, false);
				NKCUtil.SetGameobjectActive(this.m_objClassIconMechanic, false);
				NKCUtil.SetGameobjectActive(this.m_objClassIconSolider, false);
				NKCUtil.SetLabelText(this.m_lbClass, "");
				this.SetUnitRank(NKM_UNIT_GRADE.NUG_COUNT, false);
				NKCUtil.SetLabelText(this.m_lbCodename, "");
				NKCUtil.SetLabelText(this.m_lbName, "");
				NKCUtil.SetLabelText(this.m_lbLevel, "-");
				NKCUtil.SetLabelText(this.m_lbPowerSummary, "");
				NKCUtil.SetGameobjectActive(this.m_imgExpBar, false);
				NKCUtil.SetLabelText(this.m_lbCost, "-");
				NKCUIComStarRank comStarRank2 = this.m_comStarRank;
				if (comStarRank2 != null)
				{
					comStarRank2.SetStarRank(0, 0, false);
				}
				NKCUtil.SetGameobjectActive(this.m_tacticUpdateLvSlot.gameObject, false);
			}
			this.SetEnableClassStar(true);
		}

		// Token: 0x060092D8 RID: 37592 RVA: 0x003220B0 File Offset: 0x003202B0
		private void SetUnitRank(NKM_UNIT_GRADE grade, bool isAwaken)
		{
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
			NKCUtil.SetImageSprite(this.m_imgRarity, orLoadAssetResource, true);
			NKCUtil.SetGameobjectActive(this.m_objRarityN, grade == NKM_UNIT_GRADE.NUG_N);
			NKCUtil.SetGameobjectActive(this.m_objRarityR, grade == NKM_UNIT_GRADE.NUG_R);
			NKCUtil.SetGameobjectActive(this.m_objRaritySR, grade == NKM_UNIT_GRADE.NUG_SR);
			NKCUtil.SetGameobjectActive(this.m_objRaritySSR, grade == NKM_UNIT_GRADE.NUG_SSR);
			NKCUtil.SetGameobjectActive(this.m_objAwakenSR, isAwaken && grade == NKM_UNIT_GRADE.NUG_SR);
			NKCUtil.SetGameobjectActive(this.m_objAwakenSSR, isAwaken && grade == NKM_UNIT_GRADE.NUG_SSR);
		}

		// Token: 0x060092D9 RID: 37593 RVA: 0x00322184 File Offset: 0x00320384
		private void SetUnitInfo(int unitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				return;
			}
			this.m_SelectUnitID = unitID;
			if (this.m_imgRole != null)
			{
				this.m_imgRole.sprite = NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, false);
			}
			if (this.m_lbRole != null)
			{
				this.m_lbRole.text = NKCUtilString.GetRoleText(unitTempletBase);
			}
			if (this.m_imgMoveType != null)
			{
				this.m_imgMoveType.sprite = NKCUtil.GetMoveTypeImg(unitTempletBase.m_bAirUnit);
			}
			if (this.m_lbMoveType != null)
			{
				this.m_lbMoveType.text = NKCUtilString.GetMoveTypeText(unitTempletBase.m_bAirUnit);
			}
			if (this.m_imgAttackType != null)
			{
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					this.m_imgAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE, false);
				}
				else
				{
					this.m_imgAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc, false);
				}
			}
			if (this.m_lbAttackType != null)
			{
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					this.m_lbAttackType.text = NKCUtilString.GetAtkTypeText(unitTempletBase.m_NKM_FIND_TARGET_TYPE);
					return;
				}
				this.m_lbAttackType.text = NKCUtilString.GetAtkTypeText(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc);
			}
		}

		// Token: 0x060092DA RID: 37594 RVA: 0x003222B3 File Offset: 0x003204B3
		public void OpenInfo()
		{
			if (this.m_UnitInfo == null)
			{
				return;
			}
			NKCPopupUnitRoleInfo.Instance.OpenPopup(this.m_UnitInfo);
		}

		// Token: 0x060092DB RID: 37595 RVA: 0x003222CE File Offset: 0x003204CE
		public void SetEnableClassStar(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_objClassStar, bEnable);
		}

		// Token: 0x060092DC RID: 37596 RVA: 0x003222DC File Offset: 0x003204DC
		public void SetEnableLevelInfo(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL, bEnable);
		}

		// Token: 0x04007FC8 RID: 32712
		[Header("기본")]
		public Text m_lbCodename;

		// Token: 0x04007FC9 RID: 32713
		public Text m_lbName;

		// Token: 0x04007FCA RID: 32714
		public NKCUIComTextUnitLevel m_lbLevel;

		// Token: 0x04007FCB RID: 32715
		public Text m_lbMaxLevel;

		// Token: 0x04007FCC RID: 32716
		public Text m_lbCost;

		// Token: 0x04007FCD RID: 32717
		public Image m_imgExpBar;

		// Token: 0x04007FCE RID: 32718
		public Text m_lbExp;

		// Token: 0x04007FCF RID: 32719
		public NKCUIComStarRank m_comStarRank;

		// Token: 0x04007FD0 RID: 32720
		[Header("작전능력")]
		public Text m_lbPowerSummary;

		// Token: 0x04007FD1 RID: 32721
		[Header("타입")]
		public GameObject m_objUnitClassRoot;

		// Token: 0x04007FD2 RID: 32722
		public GameObject m_objClassIconCounter;

		// Token: 0x04007FD3 RID: 32723
		public GameObject m_objClassIconMechanic;

		// Token: 0x04007FD4 RID: 32724
		public GameObject m_objClassIconSolider;

		// Token: 0x04007FD5 RID: 32725
		public GameObject m_objClassIconETC;

		// Token: 0x04007FD6 RID: 32726
		[Header("클래스 & 레어리티")]
		public Text m_lbClass;

		// Token: 0x04007FD7 RID: 32727
		public GameObject m_objTag;

		// Token: 0x04007FD8 RID: 32728
		public Text m_lbTag;

		// Token: 0x04007FD9 RID: 32729
		public GameObject m_objRarityN;

		// Token: 0x04007FDA RID: 32730
		public GameObject m_objRarityR;

		// Token: 0x04007FDB RID: 32731
		public GameObject m_objRaritySR;

		// Token: 0x04007FDC RID: 32732
		public GameObject m_objRaritySSR;

		// Token: 0x04007FDD RID: 32733
		public Image m_imgRarity;

		// Token: 0x04007FDE RID: 32734
		public GameObject m_objAwakenSR;

		// Token: 0x04007FDF RID: 32735
		public GameObject m_objAwakenSSR;

		// Token: 0x04007FE0 RID: 32736
		[Header("클래스 정보")]
		public Image m_imgRole;

		// Token: 0x04007FE1 RID: 32737
		public Text m_lbRole;

		// Token: 0x04007FE2 RID: 32738
		public Image m_imgMoveType;

		// Token: 0x04007FE3 RID: 32739
		public Text m_lbMoveType;

		// Token: 0x04007FE4 RID: 32740
		public Image m_imgAttackType;

		// Token: 0x04007FE5 RID: 32741
		public Text m_lbAttackType;

		// Token: 0x04007FE6 RID: 32742
		public Image m_imgDefType;

		// Token: 0x04007FE7 RID: 32743
		public Text m_lbDefType;

		// Token: 0x04007FE8 RID: 32744
		public GameObject m_objClassStar;

		// Token: 0x04007FE9 RID: 32745
		[Header("정보창 연결")]
		public NKCUIComStateButton m_NKM_UI_UNIT_CLASS;

		// Token: 0x04007FEA RID: 32746
		public NKCUIComStateButton m_NKM_UI_UNIT_BATTLE_TYPE;

		// Token: 0x04007FEB RID: 32747
		public NKCUIComStateButton m_NKM_UI_UNIT_BATTLE_TYPE_ATK;

		// Token: 0x04007FEC RID: 32748
		public NKCUIComStateButton m_NKM_UI_UNIT_DEFENCE_TYPE;

		// Token: 0x04007FED RID: 32749
		[Header("레벨&경험치")]
		public GameObject m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL;

		// Token: 0x04007FEE RID: 32750
		[Header("전술업데이트")]
		public NKCUITacticUpdateLevelSlot m_tacticUpdateLvSlot;

		// Token: 0x04007FEF RID: 32751
		private int m_SelectUnitID;

		// Token: 0x04007FF0 RID: 32752
		private NKMUnitData m_UnitInfo;
	}
}
