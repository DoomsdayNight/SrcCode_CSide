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
	// Token: 0x02000A20 RID: 2592
	public class NKCUIOperatorPopUpSkill : NKCUIBase
	{
		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x0600717F RID: 29055 RVA: 0x0025BB94 File Offset: 0x00259D94
		public static NKCUIOperatorPopUpSkill Instance
		{
			get
			{
				if (NKCUIOperatorPopUpSkill.m_Instance == null)
				{
					NKCUIOperatorPopUpSkill.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOperatorPopUpSkill>("ab_ui_nkm_ui_operator_info", "NKM_UI_OPERATOR_POPUP_SKILL", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperatorPopUpSkill.CleanupInstance)).GetInstance<NKCUIOperatorPopUpSkill>();
					NKCUIOperatorPopUpSkill.m_Instance.Init();
				}
				return NKCUIOperatorPopUpSkill.m_Instance;
			}
		}

		// Token: 0x06007180 RID: 29056 RVA: 0x0025BBE3 File Offset: 0x00259DE3
		private static void CleanupInstance()
		{
			NKCUIOperatorPopUpSkill.m_Instance = null;
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06007181 RID: 29057 RVA: 0x0025BBEB File Offset: 0x00259DEB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOperatorPopUpSkill.m_Instance != null && NKCUIOperatorPopUpSkill.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007182 RID: 29058 RVA: 0x0025BC06 File Offset: 0x00259E06
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOperatorPopUpSkill.m_Instance != null && NKCUIOperatorPopUpSkill.m_Instance.IsOpen)
			{
				NKCUIOperatorPopUpSkill.m_Instance.Close();
			}
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x0025BC2B File Offset: 0x00259E2B
		private void OnDestroy()
		{
			NKCUIOperatorPopUpSkill.m_Instance = null;
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06007184 RID: 29060 RVA: 0x0025BC33 File Offset: 0x00259E33
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_OPERATOR_SKILL_POPUP;
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06007185 RID: 29061 RVA: 0x0025BC3A File Offset: 0x00259E3A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06007186 RID: 29062 RVA: 0x0025BC3D File Offset: 0x00259E3D
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.Clear();
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x0025BC60 File Offset: 0x00259E60
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_CancelBtn, new UnityAction(base.Close));
			if (this.m_ClickPopupClose != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnEventPanelClick));
				this.m_ClickPopupClose.triggers.Add(entry);
			}
		}

		// Token: 0x06007188 RID: 29064 RVA: 0x0025BCC7 File Offset: 0x00259EC7
		private void OnEventPanelClick(BaseEventData e)
		{
			base.Close();
		}

		// Token: 0x06007189 RID: 29065 RVA: 0x0025BCD0 File Offset: 0x00259ED0
		private void Clear()
		{
			foreach (NKCUIOperatorPassiveSlot nkcuioperatorPassiveSlot in this.m_lstVisibleSlot)
			{
				NKCUtil.SetGameobjectActive(nkcuioperatorPassiveSlot.gameObject, false);
				nkcuioperatorPassiveSlot.DestoryInstance();
			}
			this.m_lstVisibleSlot.Clear();
		}

		// Token: 0x0600718A RID: 29066 RVA: 0x0025BD38 File Offset: 0x00259F38
		public void Open(long operatorUID)
		{
			this.Open(NKCOperatorUtil.GetOperatorData(operatorUID));
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x0025BD48 File Offset: 0x00259F48
		public void Open(NKMOperator Operator)
		{
			if (Operator == null)
			{
				return;
			}
			this.m_MainSkill.SetData(Operator.mainSkill.id, (int)Operator.mainSkill.level, false);
			this.m_MainSkillCombo.SetData(Operator.id);
			NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID(Operator.mainSkill.id);
			if (tacticalCommandTempletByID != null)
			{
				NKCUtil.SetLabelText(this.m_lbSkillCoolTime, string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_SECONDS", false), (int)tacticalCommandTempletByID.m_fCoolTime));
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(Operator.id);
			this.UpdateSubSkill(unitTempletBase.m_OprPassiveGroupID, Operator.subSkill.id, (int)Operator.subSkill.level);
			NKCUtil.SetLabelText(this.m_PopupTitle, string.Format(NKCUtilString.GET_STRING_OPERATOR_SKILL_INFO_POPUP_TITLE, unitTempletBase.GetUnitName()));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x0600718C RID: 29068 RVA: 0x0025BE24 File Offset: 0x0025A024
		public void OpenForCollection(int unitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(unitTempletBase.m_lstSkillStrID[0]);
			if (skillTemplet == null)
			{
				return;
			}
			this.m_MainSkill.SetData(skillTemplet.m_OperSkillID, skillTemplet.m_MaxSkillLevel, false);
			this.m_MainSkillCombo.SetData(unitID);
			NKMTacticalCommandTemplet tacticalCommandTempletByStrID = NKMTacticalCommandManager.GetTacticalCommandTempletByStrID(skillTemplet.m_OperSkillTarget);
			if (tacticalCommandTempletByStrID != null)
			{
				NKCUtil.SetLabelText(this.m_lbSkillCoolTime, string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_SECONDS", false), (int)tacticalCommandTempletByStrID.m_fCoolTime));
			}
			NKCUtil.SetLabelText(this.m_PopupTitle, string.Format(NKCUtilString.GET_STRING_OPERATOR_SKILL_INFO_POPUP_TITLE, unitTempletBase.GetUnitName()));
			this.UpdateSubSkill(unitTempletBase.m_OprPassiveGroupID, 0, 0);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x0600718D RID: 29069 RVA: 0x0025BEE8 File Offset: 0x0025A0E8
		private void UpdateSubSkill(int groupID, int subSkillID = 0, int SubSkillLevel = 0)
		{
			if (subSkillID != 0)
			{
				this.m_SubSkill.SetData(subSkillID, SubSkillLevel, false);
			}
			NKCUtil.SetGameobjectActive(this.m_CurActivePassiveSkill, subSkillID != 0);
			NKMOperatorRandomPassiveGroupTemplet nkmoperatorRandomPassiveGroupTemplet = NKMOperatorRandomPassiveGroupTemplet.Find(groupID);
			if (nkmoperatorRandomPassiveGroupTemplet != null)
			{
				if (nkmoperatorRandomPassiveGroupTemplet.Groups.Count <= 0)
				{
					return;
				}
				foreach (NKMOperatorRandomPassiveTemplet nkmoperatorRandomPassiveTemplet in nkmoperatorRandomPassiveGroupTemplet.Groups)
				{
					if (subSkillID != nkmoperatorRandomPassiveTemplet.operSkillId)
					{
						NKCUIOperatorPassiveSlot slot = this.GetSlot();
						if (slot != null)
						{
							NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(nkmoperatorRandomPassiveTemplet.operSkillId);
							if (skillTemplet != null)
							{
								slot.SetData(NKCUtil.GetSkillIconSprite(skillTemplet), NKCStringTable.GetString(skillTemplet.m_OperSkillNameStrID, false), nkmoperatorRandomPassiveTemplet.operSkillId, skillTemplet.m_MaxSkillLevel);
							}
							this.m_lstVisibleSlot.Add(slot);
						}
					}
				}
			}
		}

		// Token: 0x0600718E RID: 29070 RVA: 0x0025BFD0 File Offset: 0x0025A1D0
		private NKCUIOperatorPassiveSlot GetSlot()
		{
			NKCUIOperatorPassiveSlot resource = NKCUIOperatorPassiveSlot.GetResource();
			if (resource != null)
			{
				NKCUtil.SetGameobjectActive(resource, true);
				resource.transform.SetParent(this.m_CanChangeableSkillScrollRect.content);
				resource.transform.localScale = Vector3.one;
				resource.Init();
				return resource;
			}
			return null;
		}

		// Token: 0x04005D64 RID: 23908
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operator_info";

		// Token: 0x04005D65 RID: 23909
		private const string UI_ASSET_NAME = "NKM_UI_OPERATOR_POPUP_SKILL";

		// Token: 0x04005D66 RID: 23910
		private static NKCUIOperatorPopUpSkill m_Instance;

		// Token: 0x04005D67 RID: 23911
		public NKCUIOperatorSkill m_MainSkill;

		// Token: 0x04005D68 RID: 23912
		public NKCUIOperatorSkill m_SubSkill;

		// Token: 0x04005D69 RID: 23913
		public NKCUIOperatorTacticalSkillCombo m_MainSkillCombo;

		// Token: 0x04005D6A RID: 23914
		public GameObject m_CurActivePassiveSkill;

		// Token: 0x04005D6B RID: 23915
		public GameObject m_CanChangeablePassiveSkill;

		// Token: 0x04005D6C RID: 23916
		public NKCUIComStateButton m_CancelBtn;

		// Token: 0x04005D6D RID: 23917
		public Text m_PopupTitle;

		// Token: 0x04005D6E RID: 23918
		public EventTrigger m_ClickPopupClose;

		// Token: 0x04005D6F RID: 23919
		public ScrollRect m_CanChangeableSkillScrollRect;

		// Token: 0x04005D70 RID: 23920
		public Text m_lbSkillCoolTime;

		// Token: 0x04005D71 RID: 23921
		private List<NKCUIOperatorPassiveSlot> m_lstVisibleSlot = new List<NKCUIOperatorPassiveSlot>();
	}
}
