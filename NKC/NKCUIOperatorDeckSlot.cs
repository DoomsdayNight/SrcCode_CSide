using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007EB RID: 2027
	public class NKCUIOperatorDeckSlot : MonoBehaviour
	{
		// Token: 0x0600506D RID: 20589 RVA: 0x0018553C File Offset: 0x0018373C
		public static NKCUIOperatorDeckSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_operator_deck", "NKM_UI_OPERATOR_DECK_SLOT", false, null);
			NKCUIOperatorDeckSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIOperatorDeckSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIOperatorDeckSlot Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.GetComponent<RectTransform>().localScale = Vector3.one;
				component.Init(null);
			}
			component.m_Instance = nkcassetInstanceData;
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x001855BC File Offset: 0x001837BC
		public void Init(NKCUIOperatorDeckSlot.OnSelectOperator callBack = null)
		{
			if (this.m_NKM_UI_OPERATOR_DECK_SLOT != null)
			{
				this.m_NKM_UI_OPERATOR_DECK_SLOT.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_OPERATOR_DECK_SLOT.PointerClick.AddListener(new UnityAction(this.OnClick));
			}
			this.m_OnClick = callBack;
			this.m_curOperatorUID = 0L;
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x00185612 File Offset: 0x00183812
		public void Clear()
		{
			if (this.m_Instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_Instance);
			}
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x00185627 File Offset: 0x00183827
		public void SetSelectEffect(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_DECK_SLOT_BORDER, bActive);
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x00185638 File Offset: 0x00183838
		private void UpdateCommonUI(NKMOperator operatorData)
		{
			if (operatorData == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
			if (unitTempletBase != null)
			{
				NKCUtil.SetImageSprite(this.m_NKM_UI_OPERATOR_DECK_SLOT_BG_Panel, NKCUtil.GetSpriteOperatorBG(unitTempletBase.m_NKM_UNIT_GRADE), false);
			}
			Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, operatorData);
			if (sprite != null)
			{
				NKCUtil.SetImageSprite(this.m_NKM_UI_OPERATOR_DECK_SLOT_IMG_Panel, sprite, false);
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_OPERATOR_DECK_SLOT_LEVEL_TEXT1, operatorData.level.ToString());
			NKCUtil.SetGameobjectActive(this.m_objMaxLevel, NKCOperatorUtil.IsMaximumLevel(operatorData.level));
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x001856BC File Offset: 0x001838BC
		private void HideSkillLevelUI()
		{
			foreach (NKCUIOperatorDeckSlot.Skill skill in this.m_lstSkill)
			{
				NKCUtil.SetGameobjectActive(skill.m_Obj, false);
			}
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x00185714 File Offset: 0x00183914
		public void SetData(NKMOperator operatorData, bool bEnableShowBan = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			if (operatorData == null)
			{
				this.SetState(NKCUIOperatorDeckSlot.STAT.EMPTY);
				this.m_curOperatorUID = 0L;
				return;
			}
			this.SetState(NKCUIOperatorDeckSlot.STAT.ACTIVE);
			this.m_curOperatorUID = operatorData.uid;
			this.UpdateCommonUI(operatorData);
			this.HideSkillLevelUI();
			if (bEnableShowBan)
			{
				this.ProcessBanUI();
			}
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x00185769 File Offset: 0x00183969
		public void UpdateData(NKMOperator operatorData)
		{
			if (this.m_curOperatorUID != 0L && this.m_curOperatorUID == operatorData.uid)
			{
				this.UpdateCommonUI(operatorData);
			}
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x00185788 File Offset: 0x00183988
		public void SetData(NKMUnitTempletBase unitTempletBase, int Level)
		{
			if (unitTempletBase == null)
			{
				this.SetState(NKCUIOperatorDeckSlot.STAT.EMPTY);
				this.m_curOperatorUID = 0L;
				this.HideSkillLevelUI();
				return;
			}
			NKCUtil.SetImageSprite(this.m_NKM_UI_OPERATOR_DECK_SLOT_BG_Panel, NKCUtil.GetSpriteOperatorBG(unitTempletBase.m_NKM_UNIT_GRADE), false);
			Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, new NKMUnitData
			{
				m_UnitID = unitTempletBase.m_UnitID,
				m_SkinID = 0
			});
			if (sprite != null)
			{
				NKCUtil.SetImageSprite(this.m_NKM_UI_OPERATOR_DECK_SLOT_IMG_Panel, sprite, false);
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_OPERATOR_DECK_SLOT_LEVEL_TEXT1, Level.ToString());
			NKCUtil.SetGameobjectActive(this.m_objMaxLevel, NKCOperatorUtil.IsMaximumLevel(Level));
			this.HideSkillLevelUI();
			this.SetState(NKCUIOperatorDeckSlot.STAT.ACTIVE);
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x0018582C File Offset: 0x00183A2C
		private void OnClick()
		{
			NKCUIOperatorDeckSlot.OnSelectOperator onClick = this.m_OnClick;
			if (onClick == null)
			{
				return;
			}
			onClick(this.m_curOperatorUID);
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00185844 File Offset: 0x00183A44
		public void SetHide()
		{
			this.SetState(NKCUIOperatorDeckSlot.STAT.HIDE);
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0018584D File Offset: 0x00183A4D
		public void SetLock()
		{
			this.SetState(NKCUIOperatorDeckSlot.STAT.LOCK);
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x00185856 File Offset: 0x00183A56
		public void SetEmpty()
		{
			this.SetState(NKCUIOperatorDeckSlot.STAT.EMPTY);
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x00185860 File Offset: 0x00183A60
		private void SetState(NKCUIOperatorDeckSlot.STAT newStat)
		{
			switch (newStat)
			{
			case NKCUIOperatorDeckSlot.STAT.LOCK:
				NKCUtil.SetImageSprite(this.m_NKM_UI_OPERATOR_DECK_SLOT_BG_Panel, NKCOperatorUtil.GetSpriteLockSlot(), false);
				break;
			case NKCUIOperatorDeckSlot.STAT.EMPTY:
				NKCUtil.SetImageSprite(this.m_NKM_UI_OPERATOR_DECK_SLOT_BG_Panel, NKCOperatorUtil.GetSpriteEmptySlot(), false);
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_DECK_SLOT_MAIN, newStat == NKCUIOperatorDeckSlot.STAT.ACTIVE);
			NKCUtil.SetGameobjectActive(base.gameObject, newStat != NKCUIOperatorDeckSlot.STAT.HIDE);
			this.m_curStat = newStat;
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x001858D0 File Offset: 0x00183AD0
		private void ProcessBanUI()
		{
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(this.m_curOperatorUID);
			if (operatorData == null)
			{
				return;
			}
			if (!NKCBanManager.IsBanOperator(operatorData.id, NKCBanManager.BAN_DATA_TYPE.FINAL))
			{
				NKCUtil.SetGameobjectActive(this.m_objBan, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objBan, true);
			int operBanLevel = NKCBanManager.GetOperBanLevel(operatorData.id, NKCBanManager.BAN_DATA_TYPE.FINAL);
			NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, operBanLevel));
		}

		// Token: 0x04004060 RID: 16480
		private NKCAssetInstanceData m_Instance;

		// Token: 0x04004061 RID: 16481
		[Header("오퍼레이터 슬롯")]
		public NKCUIComStateButton m_NKM_UI_OPERATOR_DECK_SLOT;

		// Token: 0x04004062 RID: 16482
		public GameObject m_NKM_UI_OPERATOR_DECK_SLOT_MAIN;

		// Token: 0x04004063 RID: 16483
		public Image m_NKM_UI_OPERATOR_DECK_SLOT_BG_Panel;

		// Token: 0x04004064 RID: 16484
		public Image m_NKM_UI_OPERATOR_DECK_SLOT_IMG_Panel;

		// Token: 0x04004065 RID: 16485
		public GameObject m_NKM_UI_OPERATOR_DECK_SLOT_BORDER;

		// Token: 0x04004066 RID: 16486
		public Text m_NKM_UI_OPERATOR_DECK_SLOT_LEVEL_TEXT1;

		// Token: 0x04004067 RID: 16487
		public GameObject m_objMaxLevel;

		// Token: 0x04004068 RID: 16488
		public List<NKCUIOperatorDeckSlot.Skill> m_lstSkill;

		// Token: 0x04004069 RID: 16489
		private NKCUIOperatorDeckSlot.OnSelectOperator m_OnClick;

		// Token: 0x0400406A RID: 16490
		[Header("밴 표시")]
		public GameObject m_objBan;

		// Token: 0x0400406B RID: 16491
		public Text m_lbBanLevel;

		// Token: 0x0400406C RID: 16492
		private long m_curOperatorUID;

		// Token: 0x0400406D RID: 16493
		private NKCUIOperatorDeckSlot.STAT m_curStat;

		// Token: 0x020014A8 RID: 5288
		[Serializable]
		public struct Skill
		{
			// Token: 0x04009EC4 RID: 40644
			public GameObject m_Obj;

			// Token: 0x04009EC5 RID: 40645
			public Text m_LV;

			// Token: 0x04009EC6 RID: 40646
			public GameObject m_MAX;

			// Token: 0x04009EC7 RID: 40647
			public GameObject m_ENHANCE;

			// Token: 0x04009EC8 RID: 40648
			public GameObject m_IMPLANT;
		}

		// Token: 0x020014A9 RID: 5289
		// (Invoke) Token: 0x0600A98D RID: 43405
		public delegate void OnSelectOperator(long unitUID);

		// Token: 0x020014AA RID: 5290
		private enum STAT
		{
			// Token: 0x04009ECA RID: 40650
			NONE,
			// Token: 0x04009ECB RID: 40651
			HIDE,
			// Token: 0x04009ECC RID: 40652
			LOCK,
			// Token: 0x04009ECD RID: 40653
			EMPTY,
			// Token: 0x04009ECE RID: 40654
			ACTIVE
		}
	}
}
