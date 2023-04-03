using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guide
{
	// Token: 0x02000C34 RID: 3124
	public class NKCUIStatInfoSlot : MonoBehaviour
	{
		// Token: 0x06009123 RID: 37155 RVA: 0x003177E4 File Offset: 0x003159E4
		public static NKCUIStatInfoSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("nkm_ui_tutorial_pf_unit", "TUTORIAL_PF_UNIT_STAT_SLOT", false, null);
			if (nkcassetInstanceData == null || nkcassetInstanceData.m_Instant == null)
			{
				Debug.LogError("TUTORIAL_PF_UNIT_STAT_SLOT Prefab null!");
				return null;
			}
			NKCUIStatInfoSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIStatInfoSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIUnitInfoDetailStatSlot null!");
				return null;
			}
			component.m_Instance = nkcassetInstanceData;
			component.transform.SetParent(parent, false);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06009124 RID: 37156 RVA: 0x00317862 File Offset: 0x00315A62
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_Instance);
			this.m_Instance = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06009125 RID: 37157 RVA: 0x00317881 File Offset: 0x00315A81
		public void SetData(string title, string desc)
		{
			NKCUtil.SetLabelText(this.m_title, title);
			NKCUtil.SetLabelText(this.m_Desc, desc);
		}

		// Token: 0x04007E60 RID: 32352
		public Text m_title;

		// Token: 0x04007E61 RID: 32353
		public Text m_Desc;

		// Token: 0x04007E62 RID: 32354
		private NKCAssetInstanceData m_Instance;
	}
}
