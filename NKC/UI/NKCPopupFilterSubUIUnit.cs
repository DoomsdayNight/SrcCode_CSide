using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A56 RID: 2646
	public class NKCPopupFilterSubUIUnit : MonoBehaviour
	{
		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x0600741A RID: 29722 RVA: 0x00269C75 File Offset: 0x00267E75
		public RectTransform RectTransform
		{
			get
			{
				if (this.m_RectTransform == null)
				{
					this.m_RectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_RectTransform;
			}
		}

		// Token: 0x0600741B RID: 29723 RVA: 0x00269C98 File Offset: 0x00267E98
		private void Init()
		{
			this.m_dicFilterBtn.Clear();
			this.SetToggleListner(this.m_tglHave, NKCUnitSortSystem.eFilterOption.Have);
			this.SetToggleListner(this.m_tglNotHave, NKCUnitSortSystem.eFilterOption.NotHave);
			this.SetToggleListner(this.m_tglCounter, NKCUnitSortSystem.eFilterOption.Unit_Counter);
			this.SetToggleListner(this.m_tglSoldier, NKCUnitSortSystem.eFilterOption.Unit_Soldier);
			this.SetToggleListner(this.m_tglMechanic, NKCUnitSortSystem.eFilterOption.Unit_Mechanic);
			this.SetToggleListner(this.m_tglTrophy, NKCUnitSortSystem.eFilterOption.Unit_Trainer);
			this.SetToggleListner(this.m_tglStriker, NKCUnitSortSystem.eFilterOption.Role_Striker);
			this.SetToggleListner(this.m_tglDefender, NKCUnitSortSystem.eFilterOption.Role_Defender);
			this.SetToggleListner(this.m_tglRanger, NKCUnitSortSystem.eFilterOption.Role_Ranger);
			this.SetToggleListner(this.m_tglSniper, NKCUnitSortSystem.eFilterOption.Role_Sniper);
			this.SetToggleListner(this.m_tglSupporter, NKCUnitSortSystem.eFilterOption.Role_Supporter);
			this.SetToggleListner(this.m_tglSiege, NKCUnitSortSystem.eFilterOption.Role_Siege);
			this.SetToggleListner(this.m_tglTower, NKCUnitSortSystem.eFilterOption.Role_Tower);
			this.SetToggleListner(this.m_tglMoveGround, NKCUnitSortSystem.eFilterOption.Unit_Move_Ground);
			this.SetToggleListner(this.m_tglMoveAir, NKCUnitSortSystem.eFilterOption.Unit_Move_Air);
			this.SetToggleListner(this.m_tglGround, NKCUnitSortSystem.eFilterOption.Unit_Target_Ground);
			this.SetToggleListner(this.m_tglAir, NKCUnitSortSystem.eFilterOption.Unit_Target_Air);
			this.SetToggleListner(this.m_tglAll, NKCUnitSortSystem.eFilterOption.Unit_Target_All);
			this.SetToggleListner(this.m_tglCost_10, NKCUnitSortSystem.eFilterOption.Unit_Cost_10);
			this.SetToggleListner(this.m_tglCost_9, NKCUnitSortSystem.eFilterOption.Unit_Cost_9);
			this.SetToggleListner(this.m_tglCost_8, NKCUnitSortSystem.eFilterOption.Unit_Cost_8);
			this.SetToggleListner(this.m_tglCost_7, NKCUnitSortSystem.eFilterOption.Unit_Cost_7);
			this.SetToggleListner(this.m_tglCost_6, NKCUnitSortSystem.eFilterOption.Unit_Cost_6);
			this.SetToggleListner(this.m_tglCost_5, NKCUnitSortSystem.eFilterOption.Unit_Cost_5);
			this.SetToggleListner(this.m_tglCost_4, NKCUnitSortSystem.eFilterOption.Unit_Cost_4);
			this.SetToggleListner(this.m_tglCost_3, NKCUnitSortSystem.eFilterOption.Unit_Cost_3);
			this.SetToggleListner(this.m_tglCost_2, NKCUnitSortSystem.eFilterOption.Unit_Cost_2);
			this.SetToggleListner(this.m_tglCost_1, NKCUnitSortSystem.eFilterOption.Unit_Cost_1);
			this.SetToggleListner(this.m_tglTacticLv_6, NKCUnitSortSystem.eFilterOption.Unit_TacticLv_6);
			this.SetToggleListner(this.m_tglTacticLv_5, NKCUnitSortSystem.eFilterOption.Unit_TacticLv_5);
			this.SetToggleListner(this.m_tglTacticLv_4, NKCUnitSortSystem.eFilterOption.Unit_TacticLv_4);
			this.SetToggleListner(this.m_tglTacticLv_3, NKCUnitSortSystem.eFilterOption.Unit_TacticLv_3);
			this.SetToggleListner(this.m_tglTacticLv_2, NKCUnitSortSystem.eFilterOption.Unit_TacticLv_2);
			this.SetToggleListner(this.m_tglTacticLv_1, NKCUnitSortSystem.eFilterOption.Unit_TacticLv_1);
			this.SetToggleListner(this.m_tglTacticLv_0, NKCUnitSortSystem.eFilterOption.Unit_TacticLv_0);
			this.SetToggleListner(this.m_tglAssault, NKCUnitSortSystem.eFilterOption.Ship_Assault);
			this.SetToggleListner(this.m_tglCruiser, NKCUnitSortSystem.eFilterOption.Ship_Cruiser);
			this.SetToggleListner(this.m_tglHeavy, NKCUnitSortSystem.eFilterOption.Ship_Heavy);
			this.SetToggleListner(this.m_tglSpecial, NKCUnitSortSystem.eFilterOption.Ship_Special);
			this.SetToggleListner(this.m_tglRare_SSR, NKCUnitSortSystem.eFilterOption.Rarily_SSR);
			this.SetToggleListner(this.m_tglRare_SR, NKCUnitSortSystem.eFilterOption.Rarily_SR);
			this.SetToggleListner(this.m_tglRare_R, NKCUnitSortSystem.eFilterOption.Rarily_R);
			this.SetToggleListner(this.m_tglRare_N, NKCUnitSortSystem.eFilterOption.Rarily_N);
			this.SetToggleListner(this.m_tglLevel_1, NKCUnitSortSystem.eFilterOption.Level_1);
			this.SetToggleListner(this.m_tglLevel_Other, NKCUnitSortSystem.eFilterOption.Level_other);
			this.SetToggleListner(this.m_tglLevel_Max, NKCUnitSortSystem.eFilterOption.Level_Max);
			this.SetToggleListner(this.m_tglDecked, NKCUnitSortSystem.eFilterOption.Decked);
			this.SetToggleListner(this.m_tglWait, NKCUnitSortSystem.eFilterOption.NotDecked);
			this.SetToggleListner(this.m_tglLocked, NKCUnitSortSystem.eFilterOption.Locked);
			this.SetToggleListner(this.m_tglUnlocked, NKCUnitSortSystem.eFilterOption.Unlocked);
			this.SetToggleListner(this.m_tglCollected, NKCUnitSortSystem.eFilterOption.Collected);
			this.SetToggleListner(this.m_tglNotCollected, NKCUnitSortSystem.eFilterOption.NotCollected);
			this.SetToggleListner(this.m_tglCanScout, NKCUnitSortSystem.eFilterOption.CanScout);
			this.SetToggleListner(this.m_tglNoScout, NKCUnitSortSystem.eFilterOption.NoScout);
			this.SetToggleListner(this.m_tglReplacer, NKCUnitSortSystem.eFilterOption.Unit_Replacer);
			this.SetToggleListner(this.m_tglCorrupted, NKCUnitSortSystem.eFilterOption.Unit_Corrupted);
			this.SetToggleListner(this.m_tglRoomIn, NKCUnitSortSystem.eFilterOption.InRoom);
			this.SetToggleListner(this.m_tglRoomOut, NKCUnitSortSystem.eFilterOption.OutRoom);
			this.SetToggleListner(this.m_tglLoyaltyZero, NKCUnitSortSystem.eFilterOption.Loyalty_Zero);
			this.SetToggleListner(this.m_tglLoyaltyMid, NKCUnitSortSystem.eFilterOption.Loyalty_Intermediate);
			this.SetToggleListner(this.m_tglLoyaltyMax, NKCUnitSortSystem.eFilterOption.Loyalty_Max);
			this.SetToggleListner(this.m_tglLiftContractFalse, NKCUnitSortSystem.eFilterOption.LifeContract_Unsigned);
			this.SetToggleListner(this.m_tglLiftContractTrue, NKCUnitSortSystem.eFilterOption.LifeContract_Signed);
			this.SetToggleListner(this.m_tglAwaken, NKCUnitSortSystem.eFilterOption.SpecialType_Awaken);
			this.SetToggleListner(this.m_tglRearm, NKCUnitSortSystem.eFilterOption.SpecialType_Rearm);
			this.m_bInitComplete = true;
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x0026A040 File Offset: 0x00268240
		private void SetToggleListner(NKCUIComToggle toggle, NKCUnitSortSystem.eFilterOption filterOption)
		{
			if (toggle != null)
			{
				this.m_dicFilterBtn.Add(filterOption, toggle);
				toggle.OnValueChanged.RemoveAllListeners();
				toggle.OnValueChanged.AddListener(delegate(bool value)
				{
					this.OnFilterButton(value, filterOption);
				});
			}
		}

		// Token: 0x0600741D RID: 29725 RVA: 0x0026A09E File Offset: 0x0026829E
		public void OpenFilterPopup(HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption, NKCPopupFilterSubUIUnit.OnFilterOptionChange onFilterOptionChange, NKM_UNIT_TYPE unitType, NKCPopupFilterUnit.FILTER_OPEN_TYPE filterOpenType)
		{
			this.OpenFilterPopup(setFilterOption, NKCPopupFilterUnit.MakeDefaultFilterOption(unitType, filterOpenType), onFilterOptionChange);
		}

		// Token: 0x0600741E RID: 29726 RVA: 0x0026A0B0 File Offset: 0x002682B0
		public void OpenFilterPopup(HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption, HashSet<NKCUnitSortSystem.eFilterCategory> setFilterCategory, NKCPopupFilterSubUIUnit.OnFilterOptionChange onFilterOptionChange)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.dOnFilterOptionChange = onFilterOptionChange;
			this.SetFilter(setFilterOption);
			NKCUtil.SetGameobjectActive(this.m_objHave, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Have));
			NKCUtil.SetGameobjectActive(this.m_objUnitType, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.UnitType));
			NKCUtil.SetGameobjectActive(this.m_objUnitClass, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.UnitRole));
			NKCUtil.SetGameobjectActive(this.m_objUnitBattleType, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.UnitTargetType));
			NKCUtil.SetGameobjectActive(this.m_objUnitCost, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Cost));
			NKCUtil.SetGameobjectActive(this.m_objUnitTacticLv, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.TacticLv));
			NKCUtil.SetGameobjectActive(this.m_objShipType, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.ShipType));
			NKCUtil.SetGameobjectActive(this.m_objRare, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Rarity));
			NKCUtil.SetGameobjectActive(this.m_objLevel, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Level));
			NKCUtil.SetGameobjectActive(this.m_objDeck, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Decked));
			NKCUtil.SetGameobjectActive(this.m_objLock, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Locked));
			NKCUtil.SetGameobjectActive(this.m_objRoomIn, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.InRoom));
			NKCUtil.SetGameobjectActive(this.m_objLoyalty, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Loyalty));
			NKCUtil.SetGameobjectActive(this.m_objLiftContract, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.LifeContract));
			NKCUtil.SetGameobjectActive(this.m_objUnitMoveType, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.UnitMoveType));
			NKCUtil.SetGameobjectActive(this.m_objSpecialType, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.SpecialType));
			NKCUtil.SetGameobjectActive(this.m_objScoutType, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Scout));
			NKCUtil.SetGameobjectActive(this.m_objCollected, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.Collected));
			NKCUtil.SetGameobjectActive(this.m_objMonsterType, setFilterCategory.Contains(NKCUnitSortSystem.eFilterCategory.MonsterType));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x0600741F RID: 29727 RVA: 0x0026A248 File Offset: 0x00268448
		private void SetFilter(HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption)
		{
			this.ResetFilter();
			this.m_bReset = true;
			foreach (NKCUnitSortSystem.eFilterOption key in setFilterOption)
			{
				if (this.m_dicFilterBtn.ContainsKey(key) && this.m_dicFilterBtn[key] != null)
				{
					this.m_dicFilterBtn[key].Select(true, false, false);
				}
			}
			this.m_bReset = false;
		}

		// Token: 0x06007420 RID: 29728 RVA: 0x0026A2DC File Offset: 0x002684DC
		private void OnFilterButton(bool bSelect, NKCUnitSortSystem.eFilterOption filterOption)
		{
			if (this.m_dicFilterBtn.ContainsKey(filterOption))
			{
				NKCUIComToggle nkcuicomToggle = this.m_dicFilterBtn[filterOption];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.Select(bSelect, true, true);
					if (this.m_bReset)
					{
						return;
					}
					NKCPopupFilterSubUIUnit.OnFilterOptionChange onFilterOptionChange = this.dOnFilterOptionChange;
					if (onFilterOptionChange == null)
					{
						return;
					}
					onFilterOptionChange(filterOption);
				}
			}
		}

		// Token: 0x06007421 RID: 29729 RVA: 0x0026A334 File Offset: 0x00268534
		public void ResetFilter()
		{
			this.m_bReset = true;
			NKCUIComToggle[] componentsInChildren = base.transform.GetComponentsInChildren<NKCUIComToggle>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Select(false, false, false);
			}
			this.m_bReset = false;
		}

		// Token: 0x04006030 RID: 24624
		[Header("해당 프리팹에서 사용하는것만 연결")]
		[Header("치트전용 몬스터 타입")]
		public GameObject m_objMonsterType;

		// Token: 0x04006031 RID: 24625
		public NKCUIComToggle m_tglReplacer;

		// Token: 0x04006032 RID: 24626
		public NKCUIComToggle m_tglCorrupted;

		// Token: 0x04006033 RID: 24627
		[Header("Scout")]
		public GameObject m_objScoutType;

		// Token: 0x04006034 RID: 24628
		public NKCUIComToggle m_tglCanScout;

		// Token: 0x04006035 RID: 24629
		public NKCUIComToggle m_tglNoScout;

		// Token: 0x04006036 RID: 24630
		[Header("Unit Type")]
		public GameObject m_objUnitType;

		// Token: 0x04006037 RID: 24631
		public NKCUIComToggle m_tglCounter;

		// Token: 0x04006038 RID: 24632
		public NKCUIComToggle m_tglSoldier;

		// Token: 0x04006039 RID: 24633
		public NKCUIComToggle m_tglMechanic;

		// Token: 0x0400603A RID: 24634
		public NKCUIComToggle m_tglTrophy;

		// Token: 0x0400603B RID: 24635
		[Header("Unit Class")]
		public GameObject m_objUnitClass;

		// Token: 0x0400603C RID: 24636
		public NKCUIComToggle m_tglStriker;

		// Token: 0x0400603D RID: 24637
		public NKCUIComToggle m_tglDefender;

		// Token: 0x0400603E RID: 24638
		public NKCUIComToggle m_tglRanger;

		// Token: 0x0400603F RID: 24639
		public NKCUIComToggle m_tglSniper;

		// Token: 0x04006040 RID: 24640
		public NKCUIComToggle m_tglSupporter;

		// Token: 0x04006041 RID: 24641
		public NKCUIComToggle m_tglSiege;

		// Token: 0x04006042 RID: 24642
		public NKCUIComToggle m_tglTower;

		// Token: 0x04006043 RID: 24643
		[Header("이동 타입")]
		public GameObject m_objUnitMoveType;

		// Token: 0x04006044 RID: 24644
		public NKCUIComToggle m_tglMoveGround;

		// Token: 0x04006045 RID: 24645
		public NKCUIComToggle m_tglMoveAir;

		// Token: 0x04006046 RID: 24646
		[Header("공격 타입")]
		public GameObject m_objUnitBattleType;

		// Token: 0x04006047 RID: 24647
		public NKCUIComToggle m_tglGround;

		// Token: 0x04006048 RID: 24648
		public NKCUIComToggle m_tglAir;

		// Token: 0x04006049 RID: 24649
		public NKCUIComToggle m_tglAll;

		// Token: 0x0400604A RID: 24650
		[Header("Unit Cost")]
		public GameObject m_objUnitCost;

		// Token: 0x0400604B RID: 24651
		public NKCUIComToggle m_tglCost_10;

		// Token: 0x0400604C RID: 24652
		public NKCUIComToggle m_tglCost_9;

		// Token: 0x0400604D RID: 24653
		public NKCUIComToggle m_tglCost_8;

		// Token: 0x0400604E RID: 24654
		public NKCUIComToggle m_tglCost_7;

		// Token: 0x0400604F RID: 24655
		public NKCUIComToggle m_tglCost_6;

		// Token: 0x04006050 RID: 24656
		public NKCUIComToggle m_tglCost_5;

		// Token: 0x04006051 RID: 24657
		public NKCUIComToggle m_tglCost_4;

		// Token: 0x04006052 RID: 24658
		public NKCUIComToggle m_tglCost_3;

		// Token: 0x04006053 RID: 24659
		public NKCUIComToggle m_tglCost_2;

		// Token: 0x04006054 RID: 24660
		public NKCUIComToggle m_tglCost_1;

		// Token: 0x04006055 RID: 24661
		[Header("Tactic Level")]
		public GameObject m_objUnitTacticLv;

		// Token: 0x04006056 RID: 24662
		public NKCUIComToggle m_tglTacticLv_6;

		// Token: 0x04006057 RID: 24663
		public NKCUIComToggle m_tglTacticLv_5;

		// Token: 0x04006058 RID: 24664
		public NKCUIComToggle m_tglTacticLv_4;

		// Token: 0x04006059 RID: 24665
		public NKCUIComToggle m_tglTacticLv_3;

		// Token: 0x0400605A RID: 24666
		public NKCUIComToggle m_tglTacticLv_2;

		// Token: 0x0400605B RID: 24667
		public NKCUIComToggle m_tglTacticLv_1;

		// Token: 0x0400605C RID: 24668
		public NKCUIComToggle m_tglTacticLv_0;

		// Token: 0x0400605D RID: 24669
		[Header("MoveIn")]
		public GameObject m_objRoomIn;

		// Token: 0x0400605E RID: 24670
		public NKCUIComToggle m_tglRoomIn;

		// Token: 0x0400605F RID: 24671
		public NKCUIComToggle m_tglRoomOut;

		// Token: 0x04006060 RID: 24672
		[Header("Loyalty")]
		public GameObject m_objLoyalty;

		// Token: 0x04006061 RID: 24673
		public NKCUIComToggle m_tglLoyaltyZero;

		// Token: 0x04006062 RID: 24674
		public NKCUIComToggle m_tglLoyaltyMid;

		// Token: 0x04006063 RID: 24675
		public NKCUIComToggle m_tglLoyaltyMax;

		// Token: 0x04006064 RID: 24676
		[Header("Life Contract")]
		public GameObject m_objLiftContract;

		// Token: 0x04006065 RID: 24677
		public NKCUIComToggle m_tglLiftContractFalse;

		// Token: 0x04006066 RID: 24678
		public NKCUIComToggle m_tglLiftContractTrue;

		// Token: 0x04006067 RID: 24679
		[Header("Awaken / Rearm")]
		public GameObject m_objSpecialType;

		// Token: 0x04006068 RID: 24680
		public NKCUIComToggle m_tglAwaken;

		// Token: 0x04006069 RID: 24681
		public NKCUIComToggle m_tglRearm;

		// Token: 0x0400606A RID: 24682
		[Header("Ship Type")]
		public GameObject m_objShipType;

		// Token: 0x0400606B RID: 24683
		public NKCUIComToggle m_tglAssault;

		// Token: 0x0400606C RID: 24684
		public NKCUIComToggle m_tglCruiser;

		// Token: 0x0400606D RID: 24685
		public NKCUIComToggle m_tglHeavy;

		// Token: 0x0400606E RID: 24686
		public NKCUIComToggle m_tglSpecial;

		// Token: 0x0400606F RID: 24687
		[Header("In Collection")]
		public GameObject m_objCollected;

		// Token: 0x04006070 RID: 24688
		public NKCUIComToggle m_tglCollected;

		// Token: 0x04006071 RID: 24689
		public NKCUIComToggle m_tglNotCollected;

		// Token: 0x04006072 RID: 24690
		[Header("Have")]
		public GameObject m_objHave;

		// Token: 0x04006073 RID: 24691
		public NKCUIComToggle m_tglHave;

		// Token: 0x04006074 RID: 24692
		public NKCUIComToggle m_tglNotHave;

		// Token: 0x04006075 RID: 24693
		[Header("Rarity")]
		public GameObject m_objRare;

		// Token: 0x04006076 RID: 24694
		public NKCUIComToggle m_tglRare_SSR;

		// Token: 0x04006077 RID: 24695
		public NKCUIComToggle m_tglRare_SR;

		// Token: 0x04006078 RID: 24696
		public NKCUIComToggle m_tglRare_R;

		// Token: 0x04006079 RID: 24697
		public NKCUIComToggle m_tglRare_N;

		// Token: 0x0400607A RID: 24698
		[Header("Level")]
		public GameObject m_objLevel;

		// Token: 0x0400607B RID: 24699
		public NKCUIComToggle m_tglLevel_1;

		// Token: 0x0400607C RID: 24700
		public NKCUIComToggle m_tglLevel_Other;

		// Token: 0x0400607D RID: 24701
		public NKCUIComToggle m_tglLevel_Max;

		// Token: 0x0400607E RID: 24702
		[Header("Deck")]
		public GameObject m_objDeck;

		// Token: 0x0400607F RID: 24703
		public NKCUIComToggle m_tglDecked;

		// Token: 0x04006080 RID: 24704
		public NKCUIComToggle m_tglWait;

		// Token: 0x04006081 RID: 24705
		[Header("Lock")]
		public GameObject m_objLock;

		// Token: 0x04006082 RID: 24706
		public NKCUIComToggle m_tglLocked;

		// Token: 0x04006083 RID: 24707
		public NKCUIComToggle m_tglUnlocked;

		// Token: 0x04006084 RID: 24708
		private RectTransform m_RectTransform;

		// Token: 0x04006085 RID: 24709
		private Dictionary<NKCUnitSortSystem.eFilterOption, NKCUIComToggle> m_dicFilterBtn = new Dictionary<NKCUnitSortSystem.eFilterOption, NKCUIComToggle>();

		// Token: 0x04006086 RID: 24710
		private NKCPopupFilterSubUIUnit.OnFilterOptionChange dOnFilterOptionChange;

		// Token: 0x04006087 RID: 24711
		private bool m_bInitComplete;

		// Token: 0x04006088 RID: 24712
		private bool m_bReset;

		// Token: 0x020017AA RID: 6058
		// (Invoke) Token: 0x0600B3EB RID: 46059
		public delegate void OnFilterOptionChange(NKCUnitSortSystem.eFilterOption filterOption);
	}
}
