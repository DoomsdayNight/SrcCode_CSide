using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A87 RID: 2695
	public class NKCPopupSkillFullInfo : NKCUIBase
	{
		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06007734 RID: 30516 RVA: 0x0027A93C File Offset: 0x00278B3C
		public static NKCPopupSkillFullInfo UnitInstance
		{
			get
			{
				if (NKCPopupSkillFullInfo.m_UnitInstance == null)
				{
					NKCPopupSkillFullInfo.m_UnitInstance = NKCUIManager.OpenNewInstance<NKCPopupSkillFullInfo>("ab_ui_nkm_ui_unit_info", "NKM_UI_POPUP_SKILL_FULL_INFO", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupSkillFullInfo.CleanupUnitInstance)).GetInstance<NKCPopupSkillFullInfo>();
					NKCPopupSkillFullInfo.m_UnitInstance.InitUI();
				}
				return NKCPopupSkillFullInfo.m_UnitInstance;
			}
		}

		// Token: 0x06007735 RID: 30517 RVA: 0x0027A98B File Offset: 0x00278B8B
		private static void CleanupUnitInstance()
		{
			NKCPopupSkillFullInfo.m_UnitInstance = null;
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06007736 RID: 30518 RVA: 0x0027A994 File Offset: 0x00278B94
		public static NKCPopupSkillFullInfo ShipInstance
		{
			get
			{
				if (NKCPopupSkillFullInfo.m_ShipInstance == null)
				{
					NKCPopupSkillFullInfo.m_ShipInstance = NKCUIManager.OpenNewInstance<NKCPopupSkillFullInfo>("ab_ui_nkm_ui_ship_info", "NKM_UI_POPUP_SKILL_SHIP_INFO", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupSkillFullInfo.CleanupShipInstance)).GetInstance<NKCPopupSkillFullInfo>();
					NKCPopupSkillFullInfo.m_ShipInstance.InitUI();
				}
				return NKCPopupSkillFullInfo.m_ShipInstance;
			}
		}

		// Token: 0x06007737 RID: 30519 RVA: 0x0027A9E3 File Offset: 0x00278BE3
		private static void CleanupShipInstance()
		{
			NKCPopupSkillFullInfo.m_ShipInstance = null;
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06007738 RID: 30520 RVA: 0x0027A9EB File Offset: 0x00278BEB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06007739 RID: 30521 RVA: 0x0027A9EE File Offset: 0x00278BEE
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_SKILL_FULL_INFO;
			}
		}

		// Token: 0x0600773A RID: 30522 RVA: 0x0027A9F8 File Offset: 0x00278BF8
		private void InitUI()
		{
			if (this.m_bInitComplete)
			{
				return;
			}
			for (int i = 0; i < this.m_lstSkillInfo.Count; i++)
			{
				if (this.m_lstSkillInfo[i] == null)
				{
					Debug.LogError(string.Format("m_lstSkillInfo[{0}] 스킬정보 프리팹이 없음", i));
					return;
				}
			}
			if (this.m_lbTitle == null)
			{
				Debug.LogError("m_lbTitle is null");
				return;
			}
			if (this.m_btnClose == null)
			{
				Debug.LogError("m_btnClose is null");
				return;
			}
			this.m_openAni = new NKCUIOpenAnimator(base.gameObject);
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnUserInputEvent));
			this.m_eventTrigger = base.transform.Find("BG").GetComponent<EventTrigger>();
			this.m_eventTrigger.triggers.Add(entry);
			this.m_bInitComplete = true;
		}

		// Token: 0x0600773B RID: 30523 RVA: 0x0027AB1F File Offset: 0x00278D1F
		public override void CloseInternal()
		{
			this.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600773C RID: 30524 RVA: 0x0027AB34 File Offset: 0x00278D34
		private void Clear()
		{
			foreach (NKCPopupSkillInfo nkcpopupSkillInfo in this.m_lstSkillInfo)
			{
				nkcpopupSkillInfo.Clear();
			}
		}

		// Token: 0x0600773D RID: 30525 RVA: 0x0027AB84 File Offset: 0x00278D84
		public void OpenForUnit(NKMUnitData unitData, string unitName, int unitStarGradeMax, int unitLimitBreakLevel, bool bIsRearmUnit)
		{
			this.m_openAni.PlayOpenAni();
			this.m_lbTitle.text = string.Format(NKCUtilString.GET_STRING_UNIT_SKILL_INFO_ONE_PARAM, unitName);
			NKCUtil.SetGameobjectActive(this.m_objRearmUnitTitle, false);
			List<NKMUnitSkillTemplet> unitAllSkillTemplets = NKMUnitSkillManager.GetUnitAllSkillTemplets(unitData);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			bool bValue = false;
			int num = 1;
			int num2 = 0;
			while (num2 < this.m_lstSkillInfo.Count && unitAllSkillTemplets.Count >= num)
			{
				NKMUnitSkillTemplet nkmunitSkillTemplet = unitAllSkillTemplets[num2];
				if (nkmunitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER)
				{
					this.m_lstSkillInfo[0].InitUI();
					this.m_lstSkillInfo[0].OpenForUnit(nkmunitSkillTemplet, unitStarGradeMax, unitLimitBreakLevel, unitTempletBase.m_RearmGrade, unitTempletBase.StopDefaultCoolTime);
					bValue = true;
				}
				else
				{
					this.m_lstSkillInfo[num].InitUI();
					this.m_lstSkillInfo[num].OpenForUnit(nkmunitSkillTemplet, unitStarGradeMax, unitLimitBreakLevel, unitTempletBase.m_RearmGrade, unitTempletBase.StopDefaultCoolTime);
					NKCUtil.SetGameobjectActive(this.m_lstSkillInfo[num], true);
					num++;
				}
				num2++;
			}
			NKCUtil.SetGameobjectActive(this.m_lstSkillInfo[0], bValue);
			for (int i = num; i < this.m_lstSkillInfo.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSkillInfo[i], false);
			}
			NKCUtil.SetGameobjectActive(this.m_objRearmUnitTitle, bIsRearmUnit);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x0600773E RID: 30526 RVA: 0x0027ACF8 File Offset: 0x00278EF8
		public void OpenForShip(int unitID, long shipUID = 0L)
		{
			this.m_openAni.PlayOpenAni();
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTitle, string.Format(NKCUtilString.GET_STRING_UNIT_SKILL_INFO_ONE_PARAM, unitTempletBase.GetUnitName()));
			int hasShipLv = 0;
			if (shipUID == -1L)
			{
				hasShipLv = 6;
			}
			else if (shipUID != 0L)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					NKMUnitData shipFromUID = nkmuserData.m_ArmyData.GetShipFromUID(shipUID);
					if (shipFromUID != null)
					{
						hasShipLv = this.GetShipLevel(shipFromUID.m_UnitID);
					}
				}
			}
			for (int i = 0; i < this.m_lstSkillInfo.Count; i++)
			{
				this.m_lstSkillInfo[i].InitUI();
				this.m_lstSkillInfo[i].OpenForShip(i, unitID, hasShipLv);
			}
			NKCUtil.SetGameobjectActive(this.m_objRearmUnitTitle, false);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x0600773F RID: 30527 RVA: 0x0027ADCB File Offset: 0x00278FCB
		private int GetShipLevel(int shipID)
		{
			if (shipID < 10000)
			{
				return 0;
			}
			return (int)((double)shipID * 0.001) % 10;
		}

		// Token: 0x06007740 RID: 30528 RVA: 0x0027ADE7 File Offset: 0x00278FE7
		private void OnUserInputEvent(BaseEventData eventData)
		{
			base.Close();
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x0027ADEF File Offset: 0x00278FEF
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_openAni.Update();
			}
		}

		// Token: 0x040063C1 RID: 25537
		private const string UNIT_POPUP_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_info";

		// Token: 0x040063C2 RID: 25538
		private const string UI_ASSET_NAME_FOR_UNIT = "NKM_UI_POPUP_SKILL_FULL_INFO";

		// Token: 0x040063C3 RID: 25539
		private const string SHIP_POPUP_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_ship_info";

		// Token: 0x040063C4 RID: 25540
		private const string UI_ASSET_NAME_FOR_SHIP = "NKM_UI_POPUP_SKILL_SHIP_INFO";

		// Token: 0x040063C5 RID: 25541
		private static NKCPopupSkillFullInfo m_UnitInstance;

		// Token: 0x040063C6 RID: 25542
		private static NKCPopupSkillFullInfo m_ShipInstance;

		// Token: 0x040063C7 RID: 25543
		private NKCUIOpenAnimator m_openAni;

		// Token: 0x040063C8 RID: 25544
		public GameObject m_objRearmUnitTitle;

		// Token: 0x040063C9 RID: 25545
		public List<NKCPopupSkillInfo> m_lstSkillInfo = new List<NKCPopupSkillInfo>();

		// Token: 0x040063CA RID: 25546
		public Text m_lbTitle;

		// Token: 0x040063CB RID: 25547
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040063CC RID: 25548
		public EventTrigger m_eventTrigger;

		// Token: 0x040063CD RID: 25549
		private bool m_bInitComplete;
	}
}
