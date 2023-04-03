using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C61 RID: 3169
	public class NKCUIUnitInfoSummary : MonoBehaviour
	{
		// Token: 0x06009384 RID: 37764 RVA: 0x003259B0 File Offset: 0x00323BB0
		public void InitUI()
		{
			this.m_NKCUICharInfoSummary = base.gameObject.GetComponent<NKCUICharInfoSummary>();
			this.m_NKCUICharInfoSummary.Init(true);
			this.m_NKM_UI_UNIT_CHANGE = base.transform.Find("NKM_UI_LAB_UNIT_CHANGE").GetComponent<NKCUIComButton>();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06009385 RID: 37765 RVA: 0x00325A01 File Offset: 0x00323C01
		public void FindLabObject()
		{
			if (!this.IsSet)
			{
				this.m_NKCUICharInfoSummary.m_lbPowerSummary = GameObject.Find("NKM_UI_LAB_UNIT_SUMMARY_UNIT_POWER_TEXT").GetComponent<Text>();
				this.m_NKCUICharInfoSummary.m_lbPowerSummary.text = "";
				this.IsSet = true;
			}
		}

		// Token: 0x06009386 RID: 37766 RVA: 0x00325A41 File Offset: 0x00323C41
		public void LinkLab(UnityAction addListener)
		{
			this.m_NKM_UI_UNIT_CHANGE.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_UNIT_CHANGE.PointerClick.AddListener(addListener);
		}

		// Token: 0x06009387 RID: 37767 RVA: 0x00325A64 File Offset: 0x00323C64
		public void SetData(NKMUnitData unitData)
		{
			this.m_NKCUICharInfoSummary.SetData(unitData);
		}

		// Token: 0x06009388 RID: 37768 RVA: 0x00325A72 File Offset: 0x00323C72
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06009389 RID: 37769 RVA: 0x00325A80 File Offset: 0x00323C80
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04008073 RID: 32883
		private NKCUICharInfoSummary m_NKCUICharInfoSummary;

		// Token: 0x04008074 RID: 32884
		private NKCUIComButton m_NKM_UI_UNIT_CHANGE;

		// Token: 0x04008075 RID: 32885
		private bool IsSet;
	}
}
