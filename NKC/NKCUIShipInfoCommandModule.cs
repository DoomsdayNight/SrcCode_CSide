using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x020009E2 RID: 2530
	public class NKCUIShipInfoCommandModule : MonoBehaviour
	{
		// Token: 0x06006CD6 RID: 27862 RVA: 0x00239B30 File Offset: 0x00237D30
		public void Init(UnityAction dOnClickModuleSetting)
		{
			this.m_btnModuleSetting.PointerClick.RemoveAllListeners();
			this.m_btnModuleSetting.PointerClick.AddListener(new UnityAction(this.OnClickModuleSetting));
			this.m_dOnClickModuleSetting = dOnClickModuleSetting;
		}

		// Token: 0x06006CD7 RID: 27863 RVA: 0x00239B68 File Offset: 0x00237D68
		public void SetData(NKMUnitData shipData)
		{
			for (int i = 0; i < this.m_lstModuleStep.Count; i++)
			{
				if (i < (int)shipData.m_LimitBreakLevel)
				{
					NKCUtil.SetGameobjectActive(this.m_lstModuleStep[i], true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstModuleStep[i], false);
				}
			}
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x00239BBA File Offset: 0x00237DBA
		private void OnClickModuleSetting()
		{
			UnityAction dOnClickModuleSetting = this.m_dOnClickModuleSetting;
			if (dOnClickModuleSetting == null)
			{
				return;
			}
			dOnClickModuleSetting();
		}

		// Token: 0x040058AB RID: 22699
		public NKCUIComStateButton m_btnModuleSetting;

		// Token: 0x040058AC RID: 22700
		public List<GameObject> m_lstModuleStep = new List<GameObject>();

		// Token: 0x040058AD RID: 22701
		private UnityAction m_dOnClickModuleSetting;
	}
}
