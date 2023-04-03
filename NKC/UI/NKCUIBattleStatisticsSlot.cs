using System;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000976 RID: 2422
	public class NKCUIBattleStatisticsSlot : MonoBehaviour
	{
		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x0600621B RID: 25115 RVA: 0x001EC07D File Offset: 0x001EA27D
		private string COUNT_TEXT
		{
			get
			{
				return NKCUtilString.GET_STRING_ATTACK_COUNT_ONE_PARAM;
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x001EC084 File Offset: 0x001EA284
		private string KILL_TEXT
		{
			get
			{
				return NKCUtilString.GET_STRING_KILL_COUNT_ONE_PARAM;
			}
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x001EC08B File Offset: 0x001EA28B
		public void SetEnableShowBan(bool bSet)
		{
			this.m_bEnableShowBan = bSet;
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x001EC094 File Offset: 0x001EA294
		public void SetEnableShowUpUnit(bool bSet)
		{
			this.m_bEnableShowUpUnit = bSet;
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x001EC0A0 File Offset: 0x001EA2A0
		public static NKCUIBattleStatisticsSlot GetNewInstance(Transform parent, bool bLeader)
		{
			string assetName = bLeader ? "NKM_UI_RESULT_BATTLE_STATISTICS_SLOT_LEADER" : "NKM_UI_RESULT_BATTLE_STATISTICS_SLOT_UNIT";
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_RESULT", assetName, false, null);
			NKCUIBattleStatisticsSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIBattleStatisticsSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIBattleStatisticsSlot Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.Init(bLeader);
			component.transform.localScale = Vector3.one;
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x001EC12C File Offset: 0x001EA32C
		public void CloseInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
			this.m_instance = null;
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x001EC140 File Offset: 0x001EA340
		public void Init(bool bLeader)
		{
			this.m_teamA = base.transform.Find("LEFT/CONTENTS");
			this.m_teamB = base.transform.Find("RIGHT/CONTENTS");
			this.m_leader = bLeader;
			this.initUI(this.m_slotA, this.m_teamA);
			this.initUI(this.m_slotB, this.m_teamB);
			if (!bLeader)
			{
				this.initNormalUI(this.m_slotA, this.m_teamA, false);
				this.initNormalUI(this.m_slotB, this.m_teamB, true);
				return;
			}
			this.initLeaderUI(this.m_slotA, this.m_teamA, false);
			this.initLeaderUI(this.m_slotB, this.m_teamB, true);
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x001EC1F4 File Offset: 0x001EA3F4
		private void initUI(NKCUIBattleStatisticsSlot.UnitSlot slot, Transform teamRoot)
		{
			slot.unitSlot = teamRoot.Find("ICON/UNIT_DECK/NKM_UI_DECK_VIEW_UNIT_SLOT").GetComponent<NKCDeckViewUnitSlot>();
			slot.unitSlot.Init(0, false);
			slot.name = teamRoot.Find("NAME/NAME_TEXT").GetComponent<Text>();
			slot.damageSlider = teamRoot.Find("GAUGE/GAUGE_01/GAUGE_01_Slider").GetComponent<Slider>();
			slot.recvDamageSlider = teamRoot.Find("GAUGE/GAUGE_02/GAUGE_02_Slider").GetComponent<Slider>();
			slot.healSlider = teamRoot.Find("GAUGE/GAUGE_03/GAUGE_03_Slider").GetComponent<Slider>();
			slot.damageText = teamRoot.Find("GAUGE/GAUGE_01/GAUGE_01_Slider/Handle Slide Area/Handle").GetComponent<Text>();
			slot.recvDamageText = teamRoot.Find("GAUGE/GAUGE_02/GAUGE_02_Slider/Handle Slide Area/Handle").GetComponent<Text>();
			slot.healText = teamRoot.Find("GAUGE/GAUGE_03/GAUGE_03_Slider/Handle Slide Area/Handle").GetComponent<Text>();
			slot.objHeal = teamRoot.Find("GAUGE/GAUGE_03").gameObject;
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x001EC2D4 File Offset: 0x001EA4D4
		private void initNormalUI(NKCUIBattleStatisticsSlot.UnitSlot slot, Transform teamRoot, bool bRight)
		{
			slot.objCount = teamRoot.Find("NAME/COUNT").gameObject;
			slot.count = teamRoot.Find("NAME/COUNT/COUNT_TEXT").GetComponent<Text>();
			if (!bRight)
			{
				slot.time = teamRoot.Find("NAME/TIME/TIME_TEXT").GetComponent<Text>();
			}
			slot.objSummon = teamRoot.Find("ICON/SUMMON").gameObject;
			slot.objAssist = teamRoot.Find("ICON/ASSIST").gameObject;
			slot.objKill = teamRoot.Find("NAME/KILL").gameObject;
			slot.lbKill = teamRoot.Find("NAME/KILL/COUNT_TEXT").GetComponent<Text>();
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x001EC37E File Offset: 0x001EA57E
		private void initLeaderUI(NKCUIBattleStatisticsSlot.UnitSlot slot, Transform teamRoot, bool bRight)
		{
			slot.imgShip = teamRoot.Find("ICON/SHIP_DECK/ICON").GetComponent<Image>();
			if (bRight)
			{
				slot.objBoss = teamRoot.Find("ICON/BOSS").gameObject;
			}
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x001EC3B0 File Offset: 0x001EA5B0
		public void SetDataA(NKCUIBattleStatistics.UnitBattleData data, float maxValue, bool isDps = false)
		{
			if (data == null)
			{
				NKCUtil.SetGameobjectActive(this.m_teamA, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_teamA, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(data.unitData.m_UnitID);
			if (this.m_slotA.unitSlot != null)
			{
				this.m_slotA.unitSlot.SetEnableShowBan(this.m_bEnableShowBan);
				this.m_slotA.unitSlot.SetEnableShowUpUnit(this.m_bEnableShowUpUnit);
			}
			if (this.m_leader)
			{
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					this.m_slotA.imgShip.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
				}
				else
				{
					NKCDeckViewUnitSlot unitSlot = this.m_slotA.unitSlot;
					if (unitSlot != null)
					{
						unitSlot.SetData(data.unitData, false);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_slotA.unitSlot, unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP);
				this.m_slotA.imgShip.enabled = (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP);
			}
			else
			{
				NKCDeckViewUnitSlot unitSlot2 = this.m_slotA.unitSlot;
				if (unitSlot2 != null)
				{
					unitSlot2.SetData(data.unitData, false);
				}
				NKCDeckViewUnitSlot unitSlot3 = this.m_slotA.unitSlot;
				if (unitSlot3 != null)
				{
					unitSlot3.SetLeader(data.bLeader, false);
				}
				this.m_slotA.time.text = data.recordData.playtime.ToString();
				NKCUtil.SetGameobjectActive(this.m_slotA.objSummon, data.bSummon);
				NKCUtil.SetGameobjectActive(this.m_slotA.objCount, !data.bSummon);
				NKCUtil.SetGameobjectActive(this.m_slotA.objAssist, data.bAssist);
				if (!data.bSummon)
				{
					this.m_slotA.count.text = string.Format(this.COUNT_TEXT, data.recordData.recordSummonCount);
				}
			}
			string text = (!string.IsNullOrEmpty(data.recordData.changeUnitName)) ? NKCStringTable.GetString(data.recordData.changeUnitName, false) : unitTempletBase.GetUnitName();
			this.m_slotA.name.text = text;
			NKCUtil.SetGameobjectActive(this.m_slotA.objKill, data.recordData.recordKillCount > 0);
			if (this.m_slotA.objKill != null && this.m_slotA.objKill.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_slotA.lbKill, string.Format(this.KILL_TEXT, data.recordData.recordKillCount));
			}
			this.setSlider(this.m_slotA, data.recordData, maxValue, isDps);
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x001EC63C File Offset: 0x001EA83C
		public void SetDataB(NKCUIBattleStatistics.UnitBattleData data, float maxValue, bool isDps = false)
		{
			if (data == null)
			{
				NKCUtil.SetGameobjectActive(this.m_teamB, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_teamB, true);
			if (this.m_slotB.unitSlot != null)
			{
				this.m_slotB.unitSlot.SetEnableShowBan(this.m_bEnableShowBan);
				this.m_slotB.unitSlot.SetEnableShowUpUnit(this.m_bEnableShowUpUnit);
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(data.unitData.m_UnitID);
			if (this.m_leader)
			{
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					this.m_slotB.imgShip.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
				}
				else
				{
					NKCDeckViewUnitSlot unitSlot = this.m_slotB.unitSlot;
					if (unitSlot != null)
					{
						unitSlot.SetData(data.unitData, false);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_slotB.unitSlot, unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP);
				this.m_slotB.imgShip.enabled = (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP);
			}
			else
			{
				NKCDeckViewUnitSlot unitSlot2 = this.m_slotB.unitSlot;
				if (unitSlot2 != null)
				{
					unitSlot2.SetData(data.unitData, false);
				}
				NKCDeckViewUnitSlot unitSlot3 = this.m_slotB.unitSlot;
				if (unitSlot3 != null)
				{
					unitSlot3.SetLeader(data.bLeader, false);
				}
				NKCUtil.SetGameobjectActive(this.m_slotB.objSummon, data.bSummon);
				NKCUtil.SetGameobjectActive(this.m_slotB.objCount, !data.bSummon);
				NKCUtil.SetGameobjectActive(this.m_slotB.objAssist, data.bAssist);
				if (!data.bSummon)
				{
					this.m_slotB.count.text = string.Format(this.COUNT_TEXT, data.recordData.recordSummonCount);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_slotB.objBoss, this.m_leader);
			string text = (!string.IsNullOrEmpty(data.recordData.changeUnitName)) ? NKCStringTable.GetString(data.recordData.changeUnitName, false) : unitTempletBase.GetUnitName();
			this.m_slotB.name.text = text;
			NKCUtil.SetGameobjectActive(this.m_slotB.objKill, data.recordData.recordKillCount > 0);
			if (this.m_slotB.objKill != null && this.m_slotB.objKill.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_slotB.lbKill, string.Format(this.KILL_TEXT, data.recordData.recordKillCount));
			}
			this.setSlider(this.m_slotB, data.recordData, maxValue, isDps);
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x001EC8C0 File Offset: 0x001EAAC0
		private void setSlider(NKCUIBattleStatisticsSlot.UnitSlot unitSlot, NKMGameRecordUnitData data, float maxValue, bool isDps)
		{
			if (maxValue <= 0f)
			{
				maxValue = 1f;
			}
			float num = 0f;
			float num2 = 0f;
			if (!isDps)
			{
				num = data.recordGiveDamage;
				num2 = data.recordTakeDamage;
			}
			else if (data.playtime > 0)
			{
				num = data.recordGiveDamage / (float)data.playtime;
				num2 = data.recordTakeDamage / (float)data.playtime;
			}
			unitSlot.damageSlider.value = num / maxValue;
			unitSlot.recvDamageSlider.value = num2 / maxValue;
			unitSlot.healSlider.value = data.recordHeal / maxValue;
			unitSlot.damageText.text = string.Format("{0:#,##0}", (int)num);
			unitSlot.recvDamageText.text = string.Format("{0:#,##0}", (int)num2);
			unitSlot.healText.text = string.Format("{0:#,##0}", (int)data.recordHeal);
			NKCUtil.SetGameobjectActive(unitSlot.objHeal, data.recordHeal > 0f && !isDps);
		}

		// Token: 0x04004E0E RID: 19982
		private Transform m_teamA;

		// Token: 0x04004E0F RID: 19983
		private Transform m_teamB;

		// Token: 0x04004E10 RID: 19984
		private NKCUIBattleStatisticsSlot.UnitSlot m_slotA = new NKCUIBattleStatisticsSlot.UnitSlot();

		// Token: 0x04004E11 RID: 19985
		private NKCUIBattleStatisticsSlot.UnitSlot m_slotB = new NKCUIBattleStatisticsSlot.UnitSlot();

		// Token: 0x04004E12 RID: 19986
		private bool m_leader;

		// Token: 0x04004E13 RID: 19987
		private const string BundleName = "AB_UI_NKM_UI_RESULT";

		// Token: 0x04004E14 RID: 19988
		private const string AssetNameNormal = "NKM_UI_RESULT_BATTLE_STATISTICS_SLOT_UNIT";

		// Token: 0x04004E15 RID: 19989
		private const string AssetNameLeader = "NKM_UI_RESULT_BATTLE_STATISTICS_SLOT_LEADER";

		// Token: 0x04004E16 RID: 19990
		private NKCAssetInstanceData m_instance;

		// Token: 0x04004E17 RID: 19991
		private bool m_bEnableShowBan;

		// Token: 0x04004E18 RID: 19992
		private bool m_bEnableShowUpUnit;

		// Token: 0x02001621 RID: 5665
		public class UnitSlot
		{
			// Token: 0x0400A358 RID: 41816
			public NKCDeckViewUnitSlot unitSlot;

			// Token: 0x0400A359 RID: 41817
			public Text name;

			// Token: 0x0400A35A RID: 41818
			public Slider damageSlider;

			// Token: 0x0400A35B RID: 41819
			public Text damageText;

			// Token: 0x0400A35C RID: 41820
			public Slider recvDamageSlider;

			// Token: 0x0400A35D RID: 41821
			public Text recvDamageText;

			// Token: 0x0400A35E RID: 41822
			public GameObject objHeal;

			// Token: 0x0400A35F RID: 41823
			public Slider healSlider;

			// Token: 0x0400A360 RID: 41824
			public Text healText;

			// Token: 0x0400A361 RID: 41825
			public GameObject objCount;

			// Token: 0x0400A362 RID: 41826
			public Text count;

			// Token: 0x0400A363 RID: 41827
			public Text time;

			// Token: 0x0400A364 RID: 41828
			public GameObject objSummon;

			// Token: 0x0400A365 RID: 41829
			public GameObject objAssist;

			// Token: 0x0400A366 RID: 41830
			public Image imgShip;

			// Token: 0x0400A367 RID: 41831
			public GameObject objBoss;

			// Token: 0x0400A368 RID: 41832
			public GameObject objKill;

			// Token: 0x0400A369 RID: 41833
			public Text lbKill;
		}
	}
}
