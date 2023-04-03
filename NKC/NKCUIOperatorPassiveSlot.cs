using System;
using NKC.UI.Tooltip;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A1F RID: 2591
	public class NKCUIOperatorPassiveSlot : MonoBehaviour
	{
		// Token: 0x06007179 RID: 29049 RVA: 0x0025BA6C File Offset: 0x00259C6C
		public static NKCUIOperatorPassiveSlot GetResource()
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_operator_info", "NKM_UI_OPERATOR_POPUP_SKILL_PASSIVE_SLOT", false, null);
			if (nkcassetInstanceData == null)
			{
				return null;
			}
			NKCUIOperatorPassiveSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIOperatorPassiveSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIOperatorPassiveSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			return component;
		}

		// Token: 0x0600717A RID: 29050 RVA: 0x0025BABC File Offset: 0x00259CBC
		public void Init()
		{
			if (null != this.m_SkillIBnt)
			{
				this.m_SkillIBnt.PointerDown.RemoveAllListeners();
				this.m_SkillIBnt.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointDownSkill));
			}
			this.m_curSkillID = 0;
			this.m_curSkillLevel = 0;
		}

		// Token: 0x0600717B RID: 29051 RVA: 0x0025BB11 File Offset: 0x00259D11
		public void SetData(Sprite icon, string name, int skillID, int skillLv)
		{
			NKCUtil.SetImageSprite(this.m_SkillIcon, icon, false);
			NKCUtil.SetLabelText(this.m_SkillName, name);
			this.m_curSkillID = skillID;
			this.m_curSkillLevel = skillLv;
		}

		// Token: 0x0600717C RID: 29052 RVA: 0x0025BB3B File Offset: 0x00259D3B
		private void OnPointDownSkill(PointerEventData eventData)
		{
			if (NKCOperatorUtil.GetSkillTemplet(this.m_curSkillID) != null)
			{
				NKCUITooltip.Instance.Open(this.m_curSkillID, this.m_curSkillLevel, new Vector2?(eventData.position));
			}
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x0025BB6B File Offset: 0x00259D6B
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04005D5C RID: 23900
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operator_info";

		// Token: 0x04005D5D RID: 23901
		private const string UI_ASSET_NAME = "NKM_UI_OPERATOR_POPUP_SKILL_PASSIVE_SLOT";

		// Token: 0x04005D5E RID: 23902
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04005D5F RID: 23903
		public Image m_SkillIcon;

		// Token: 0x04005D60 RID: 23904
		public Text m_SkillName;

		// Token: 0x04005D61 RID: 23905
		public NKCUIComStateButton m_SkillIBnt;

		// Token: 0x04005D62 RID: 23906
		private int m_curSkillID;

		// Token: 0x04005D63 RID: 23907
		private int m_curSkillLevel;
	}
}
