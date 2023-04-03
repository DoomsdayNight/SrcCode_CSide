using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A1A RID: 2586
	public class NKCUIOperatorCardSlot : NKCUIUnitSelectListSlotBase
	{
		// Token: 0x060070E9 RID: 28905 RVA: 0x002578A8 File Offset: 0x00255AA8
		public static NKCUIOperatorCardSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_unit_slot_card", "NKM_UI_OPERATOR_CARD_SLOT", false, null);
			NKCUIOperatorCardSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIOperatorCardSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIOperatorCardSlot Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.GetComponent<RectTransform>().localScale = Vector3.one;
				component.Init();
			}
			component.m_Instance = nkcassetInstanceData;
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060070EA RID: 28906 RVA: 0x00257927 File Offset: 0x00255B27
		private void Init()
		{
			if (this.m_NKM_UI_OPERATOR_CARD_SLOT != null)
			{
				this.m_NKM_UI_OPERATOR_CARD_SLOT.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_OPERATOR_CARD_SLOT.PointerClick.AddListener(new UnityAction(this.OnClick));
			}
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x00257964 File Offset: 0x00255B64
		public void Clear()
		{
			if (this.m_Instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_Instance);
			}
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x00257979 File Offset: 0x00255B79
		protected override void OnClick()
		{
			if (this.dOnSelectThisOperatorSlot != null)
			{
				this.dOnSelectThisOperatorSlot(this.m_OperatorData, this.m_NKMUnitTempletBase, this.m_DeckIndex, this.m_eUnitSlotState, this.m_eUnitSelectState);
			}
		}

		// Token: 0x04005CAC RID: 23724
		private NKCAssetInstanceData m_Instance;

		// Token: 0x04005CAD RID: 23725
		public NKCUIComButton m_NKM_UI_OPERATOR_CARD_SLOT;

		// Token: 0x04005CAE RID: 23726
		public Image m_NKM_UI_OPERATOR_CARD_SLOT_BG;

		// Token: 0x04005CAF RID: 23727
		public Image m_NKM_UI_OPERATOR_CARD_ICON;

		// Token: 0x04005CB0 RID: 23728
		public Text m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_NUMBER_TEXT;

		// Token: 0x04005CB1 RID: 23729
		public Image m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE;

		// Token: 0x04005CB2 RID: 23730
		public Text m_NKM_UI_OPERATOR_CARD_LEVEL_TEXT1;

		// Token: 0x04005CB3 RID: 23731
		public List<NKCUIOperatorCardSlot.SkillInfo> m_lstSkill;

		// Token: 0x04005CB4 RID: 23732
		public Text m_NKM_UI_OPERATOR_CARD_TITLE_TEXT;

		// Token: 0x04005CB5 RID: 23733
		public Text m_NKM_UI_OPERATOR_CARD_NAME_TEXT;

		// Token: 0x04005CB6 RID: 23734
		public GameObject m_NKM_UI_OPERATOR_CARD_USED_BG;

		// Token: 0x04005CB7 RID: 23735
		public Text m_NKM_UI_OPERATOR_CARD_USED_TEXT;

		// Token: 0x04005CB8 RID: 23736
		public GameObject m_NKM_UI_OPERATOR_CARD_LOCK_SELECT;

		// Token: 0x02001758 RID: 5976
		[Serializable]
		public struct SkillInfo
		{
			// Token: 0x0400A687 RID: 42631
			public GameObject m_Object;

			// Token: 0x0400A688 RID: 42632
			public Text m_Lv;

			// Token: 0x0400A689 RID: 42633
			public GameObject m_Max;
		}
	}
}
