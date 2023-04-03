using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F3 RID: 2547
	public class NKCUIUnitInfoDetailStatSlot : MonoBehaviour
	{
		// Token: 0x06006E59 RID: 28249 RVA: 0x00243EF4 File Offset: 0x002420F4
		public static NKCUIUnitInfoDetailStatSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_UNIT_INFO", "NKM_UI_UNIT_INFO_POPUP_STAT_LIST_SLOT", false, null);
			if (nkcassetInstanceData == null || nkcassetInstanceData.m_Instant == null)
			{
				Debug.LogError("NKM_UI_UNIT_INFO_POPUP_STAT_LIST_SLOT Prefab null!");
				return null;
			}
			NKCUIUnitInfoDetailStatSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIUnitInfoDetailStatSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIUnitInfoDetailStatSlot null!");
				return null;
			}
			component.Init();
			component.m_Instance = nkcassetInstanceData;
			component.transform.SetParent(parent, false);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06006E5A RID: 28250 RVA: 0x00243F78 File Offset: 0x00242178
		private void Init()
		{
			if (this.m_InfoToolTip == null && this.m_STAT_NAME_01 != null)
			{
				this.m_STAT_NAME_01.raycastTarget = true;
				this.m_InfoToolTip = this.m_STAT_NAME_01.gameObject.GetComponent<NKCComStatInfoToolTip>();
				if (this.m_InfoToolTip == null)
				{
					this.m_InfoToolTip = this.m_STAT_NAME_01.gameObject.AddComponent<NKCComStatInfoToolTip>();
				}
			}
		}

		// Token: 0x06006E5B RID: 28251 RVA: 0x00243FE8 File Offset: 0x002421E8
		public void SetData(NKM_STAT_TYPE eType, NKMStatData statData)
		{
			decimal num = NKMUnitStatManager.GetFinalStatForUIOutput(eType, statData);
			this.m_STAT_NAME_01.text = NKCUtilString.GetStatShortName(eType, num);
			bool flag = NKMUnitStatManager.IsPercentStat(eType);
			bool bNegative = false;
			if (NKCUtilString.IsNameReversedIfNegative(eType) && num < 0m)
			{
				bNegative = true;
				num = -num;
			}
			float statPercentage = NKCUtil.GetStatPercentage(eType, statData.GetStatBase(eType) + statData.GetBaseBonusStat(eType));
			if (flag)
			{
				if (statPercentage != 0f)
				{
					this.m_STAT_TEXT.text = string.Format("{0:P1}({1}%)", num, statPercentage.ToString("N2"));
				}
				else
				{
					this.m_STAT_TEXT.text = string.Format("{0:P1}", num);
				}
			}
			else if (statPercentage != 0f)
			{
				this.m_STAT_TEXT.text = string.Format("{0:#;-#;0}({1}%)", num, statPercentage.ToString("N2"));
			}
			else
			{
				this.m_STAT_TEXT.text = string.Format("{0:#;-#;0}", num);
			}
			if (this.m_InfoToolTip != null)
			{
				this.m_InfoToolTip.SetType(eType, bNegative);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06006E5C RID: 28252 RVA: 0x0024410E File Offset: 0x0024230E
		public void Clear()
		{
			if (this.m_Instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_Instance);
			}
		}

		// Token: 0x040059AB RID: 22955
		public Text m_STAT_NAME_01;

		// Token: 0x040059AC RID: 22956
		public Text m_STAT_TEXT;

		// Token: 0x040059AD RID: 22957
		public NKCComStatInfoToolTip m_InfoToolTip;

		// Token: 0x040059AE RID: 22958
		private NKCAssetInstanceData m_Instance;
	}
}
