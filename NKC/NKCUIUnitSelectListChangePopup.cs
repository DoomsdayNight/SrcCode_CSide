using System;
using System.Collections.Generic;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F9 RID: 2553
	public class NKCUIUnitSelectListChangePopup : NKCUIBase
	{
		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06006F08 RID: 28424 RVA: 0x0024AFC8 File Offset: 0x002491C8
		public static NKCUIUnitSelectListChangePopup Instance
		{
			get
			{
				if (NKCUIUnitSelectListChangePopup.m_Instance == null)
				{
					NKCUIUnitSelectListChangePopup.m_Instance = NKCUIManager.OpenNewInstance<NKCUIUnitSelectListChangePopup>("ab_ui_nkm_ui_unit_change_popup", "NKM_UI_UNIT_CHANGE_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIUnitSelectListChangePopup.CleanupInstance)).GetInstance<NKCUIUnitSelectListChangePopup>();
					NKCUIUnitSelectListChangePopup.m_Instance.InitUI();
				}
				return NKCUIUnitSelectListChangePopup.m_Instance;
			}
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x0024B017 File Offset: 0x00249217
		private static void CleanupInstance()
		{
			NKCUIUnitSelectListChangePopup.m_Instance = null;
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06006F0A RID: 28426 RVA: 0x0024B01F File Offset: 0x0024921F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06006F0B RID: 28427 RVA: 0x0024B022 File Offset: 0x00249222
		public override string MenuName
		{
			get
			{
				return "유닛 변경 확인";
			}
		}

		// Token: 0x06006F0C RID: 28428 RVA: 0x0024B02C File Offset: 0x0024922C
		public void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_cbtnOK, new UnityAction(this.OnOkButton));
			NKCUtil.SetHotkey(this.m_cbtnOK, HotkeyEventType.Confirm);
			NKCUtil.SetButtonClickDelegate(this.m_cbtnCancel, new UnityAction(base.Close));
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_slotBefore.Init(true);
			this.m_slotAfter.Init(true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006F0D RID: 28429 RVA: 0x0024B0A8 File Offset: 0x002492A8
		public void Open(NKMUnitData beforeUnit, NKMDeckIndex beforeUnitDeckIndex, NKMUnitData afterUnit, NKMDeckIndex afterUnitDeckIndex, NKCUIUnitSelectListChangePopup.OnUnitChangePopupOK onOK, bool bEnableShowBan = false, bool bEnableShowUpUnit = false)
		{
			if (beforeUnit == null || afterUnit == null)
			{
				Debug.LogError("[UnitSelectListChangePopup] UnitData is null");
				return;
			}
			int statBefore;
			int statBefore2;
			int statBefore3;
			int statBefore4;
			this.GetUnitStat(beforeUnit, out statBefore, out statBefore2, out statBefore3, out statBefore4);
			int statAfter;
			int statAfter2;
			int statAfter3;
			int statAfter4;
			this.GetUnitStat(afterUnit, out statAfter, out statAfter2, out statAfter3, out statAfter4);
			this.SetStatInfo(statBefore, statAfter, this.m_statCount);
			this.SetStatInfo(statBefore2, statAfter2, this.m_statHP);
			this.SetStatInfo(statBefore3, statAfter3, this.m_statATK);
			this.SetStatInfo(statBefore4, statAfter4, this.m_statDEF);
			this.SetSkillInfo(beforeUnit, this.m_listSkillInfoBefore);
			this.SetSkillInfo(afterUnit, this.m_listSkillInfoAfter);
			NKCUtil.SetGameobjectActive(this.m_objLeaderSkillBeforeUnit, NKCRearmamentUtil.IsHasLeaderSkill(beforeUnit));
			NKCUtil.SetGameobjectActive(this.m_objLeaderSkillAfterUnit, NKCRearmamentUtil.IsHasLeaderSkill(afterUnit));
			base.gameObject.SetActive(true);
			this.dOnOK = onOK;
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
			this.m_slotBefore.SetEnableShowBan(bEnableShowBan);
			this.m_slotBefore.SetEnableShowUpUnit(bEnableShowUpUnit);
			this.m_slotBefore.SetData(beforeUnit, beforeUnitDeckIndex, false, null);
			this.m_slotBefore.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			this.m_slotAfter.SetEnableShowBan(bEnableShowBan);
			this.m_slotAfter.SetEnableShowUpUnit(bEnableShowUpUnit);
			this.m_slotAfter.SetData(afterUnit, afterUnitDeckIndex, false, null);
			this.m_slotAfter.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x0024B1ED File Offset: 0x002493ED
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06006F0F RID: 28431 RVA: 0x0024B202 File Offset: 0x00249402
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006F10 RID: 28432 RVA: 0x0024B210 File Offset: 0x00249410
		private void OnOkButton()
		{
			base.Close();
			if (this.dOnOK != null)
			{
				NKCSoundManager.PlaySound("FX_UI_DECK_UNIIT_SELECT", 1f, 0f, 0f, false, 0f, false, 0f);
				this.dOnOK();
			}
		}

		// Token: 0x06006F11 RID: 28433 RVA: 0x0024B25C File Offset: 0x0024945C
		private void SetSkillInfo(NKMUnitData unitData, List<NKCUIUnitSelectListChangePopup.SimpleSkillInfo> skillInfoList)
		{
			bool flag = false;
			int num = 1;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			List<NKMUnitSkillTemplet> unitAllSkillTemplets = NKMUnitSkillManager.GetUnitAllSkillTemplets(unitData);
			for (int i = 0; i < unitAllSkillTemplets.Count; i++)
			{
				NKMUnitSkillTemplet skillTemplet = unitAllSkillTemplets[i];
				bool flag2 = NKMUnitSkillManager.IsLockedSkill(skillTemplet.m_ID, (int)unitData.m_LimitBreakLevel);
				if (!flag2)
				{
					int index;
					if (skillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER)
					{
						flag = true;
						index = 0;
					}
					else
					{
						index = num;
					}
					NKCUIUnitSelectListChangePopup.SimpleSkillInfo simpleSkillInfo = skillInfoList[index];
					simpleSkillInfo.Icon.sprite = NKCUtil.GetSkillIconSprite(skillTemplet);
					simpleSkillInfo.Icon.enabled = true;
					simpleSkillInfo.Level.text = (flag2 ? "" : string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, skillTemplet.m_Level));
					NKCUtil.SetGameobjectActive(simpleSkillInfo.Lock, flag2);
					int unitStarGradeMax = unitTempletBase.m_StarGradeMax;
					int limitBreakLevel = (int)unitData.m_LimitBreakLevel;
					simpleSkillInfo.Button.PointerDown.RemoveAllListeners();
					simpleSkillInfo.Button.PointerDown.AddListener(delegate(PointerEventData e)
					{
						this.OnSkillTooltip(skillTemplet, unitStarGradeMax, limitBreakLevel, e);
					});
					if (skillTemplet.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_LEADER)
					{
						num++;
					}
				}
			}
			if (!flag)
			{
				NKCUIUnitSelectListChangePopup.SimpleSkillInfo simpleSkillInfo2 = skillInfoList[0];
				simpleSkillInfo2.Icon.enabled = false;
				simpleSkillInfo2.Level.text = "";
				NKCUtil.SetGameobjectActive(simpleSkillInfo2.Lock, false);
				simpleSkillInfo2.Button.PointerDown.RemoveAllListeners();
			}
			for (int j = num; j < skillInfoList.Count; j++)
			{
				NKCUIUnitSelectListChangePopup.SimpleSkillInfo simpleSkillInfo3 = skillInfoList[j];
				simpleSkillInfo3.Icon.enabled = false;
				simpleSkillInfo3.Level.text = "";
				NKCUtil.SetGameobjectActive(simpleSkillInfo3.Lock, false);
				simpleSkillInfo3.Button.PointerDown.RemoveAllListeners();
			}
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x0024B44C File Offset: 0x0024964C
		private void OnSkillTooltip(NKMUnitSkillTemplet unitSkillTemplet, int unitStarGradeMax, int unitLimitBreakLevel, PointerEventData eventData)
		{
			if (unitSkillTemplet != null)
			{
				NKCUITooltip.Instance.Open(unitSkillTemplet, new Vector2?(eventData.position), unitStarGradeMax, unitLimitBreakLevel);
			}
		}

		// Token: 0x06006F13 RID: 28435 RVA: 0x0024B46C File Offset: 0x0024966C
		private void GetUnitStat(NKMUnitData unitData, out int respawnCount, out int statHP, out int statATK, out int statDEF)
		{
			bool flag = false;
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			nkmstatData.MakeBaseStat(null, flag, unitData, unitStatTemplet.m_StatData, false, 0, null);
			nkmstatData.MakeBaseBonusFactor(unitData, NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.EquipItems, null, null, flag);
			respawnCount = unitStatTemplet.m_RespawnCount;
			statHP = (int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_HP) + (int)nkmstatData.GetBaseBonusStat(NKM_STAT_TYPE.NST_HP);
			statATK = (int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_ATK) + (int)nkmstatData.GetBaseBonusStat(NKM_STAT_TYPE.NST_ATK);
			statDEF = (int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_DEF) + (int)nkmstatData.GetBaseBonusStat(NKM_STAT_TYPE.NST_DEF);
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x0024B508 File Offset: 0x00249708
		private void SetStatInfo(int statBefore, int statAfter, NKCUIUnitSelectListChangePopup.StatInfo statInfo)
		{
			string hexRGB = "#A3FF66";
			string hexRGB2 = "#FF3D40";
			string format = "<size=20>{0}</size>{1}";
			statInfo.Before.text = statBefore.ToString();
			statInfo.After.text = statAfter.ToString();
			int num = statAfter - statBefore;
			if (num > 0)
			{
				statInfo.Compare.text = string.Format(format, "▲", num.ToString());
				statInfo.Compare.color = NKCUtil.GetColor(hexRGB);
				return;
			}
			if (num < 0)
			{
				statInfo.Compare.text = string.Format(format, "▼", num.ToString());
				statInfo.Compare.color = NKCUtil.GetColor(hexRGB2);
				return;
			}
			statInfo.Compare.text = "";
		}

		// Token: 0x04005A6A RID: 23146
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_change_popup";

		// Token: 0x04005A6B RID: 23147
		private const string UI_ASSET_NAME = "NKM_UI_UNIT_CHANGE_POPUP";

		// Token: 0x04005A6C RID: 23148
		private static NKCUIUnitSelectListChangePopup m_Instance;

		// Token: 0x04005A6D RID: 23149
		public NKCUIComButton m_cbtnOK;

		// Token: 0x04005A6E RID: 23150
		public NKCUIComButton m_cbtnCancel;

		// Token: 0x04005A6F RID: 23151
		public NKCUIUnitSelectListSlot m_slotBefore;

		// Token: 0x04005A70 RID: 23152
		public NKCUIUnitSelectListSlot m_slotAfter;

		// Token: 0x04005A71 RID: 23153
		public List<NKCUIUnitSelectListChangePopup.SimpleSkillInfo> m_listSkillInfoBefore;

		// Token: 0x04005A72 RID: 23154
		public List<NKCUIUnitSelectListChangePopup.SimpleSkillInfo> m_listSkillInfoAfter;

		// Token: 0x04005A73 RID: 23155
		public NKCUIUnitSelectListChangePopup.StatInfo m_statCount;

		// Token: 0x04005A74 RID: 23156
		public NKCUIUnitSelectListChangePopup.StatInfo m_statHP;

		// Token: 0x04005A75 RID: 23157
		public NKCUIUnitSelectListChangePopup.StatInfo m_statATK;

		// Token: 0x04005A76 RID: 23158
		public NKCUIUnitSelectListChangePopup.StatInfo m_statDEF;

		// Token: 0x04005A77 RID: 23159
		public GameObject m_objLeaderSkillBeforeUnit;

		// Token: 0x04005A78 RID: 23160
		public GameObject m_objLeaderSkillAfterUnit;

		// Token: 0x04005A79 RID: 23161
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005A7A RID: 23162
		private NKCUIUnitSelectListChangePopup.OnUnitChangePopupOK dOnOK;

		// Token: 0x0200172C RID: 5932
		[Serializable]
		public struct SimpleSkillInfo
		{
			// Token: 0x0400A63A RID: 42554
			public Image Icon;

			// Token: 0x0400A63B RID: 42555
			public Text Level;

			// Token: 0x0400A63C RID: 42556
			public NKCUIComStateButton Button;

			// Token: 0x0400A63D RID: 42557
			public GameObject Lock;
		}

		// Token: 0x0200172D RID: 5933
		[Serializable]
		public struct StatInfo
		{
			// Token: 0x0400A63E RID: 42558
			public Text Before;

			// Token: 0x0400A63F RID: 42559
			public Text After;

			// Token: 0x0400A640 RID: 42560
			public Text Compare;
		}

		// Token: 0x0200172E RID: 5934
		// (Invoke) Token: 0x0600B28C RID: 45708
		public delegate void OnUnitChangePopupOK();
	}
}
