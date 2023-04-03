using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x020009FB RID: 2555
	public class NKCUIUnitSelectListRemovePopup : MonoBehaviour
	{
		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06006F27 RID: 28455 RVA: 0x0024B9D1 File Offset: 0x00249BD1
		public bool IsOpen
		{
			get
			{
				return base.gameObject.activeSelf;
			}
		}

		// Token: 0x06006F28 RID: 28456 RVA: 0x0024B9E0 File Offset: 0x00249BE0
		private void Init()
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_ctglSSR.Select(false, true, false);
			this.m_ctglSR.Select(false, true, false);
			this.m_ctglR.Select(true, true, false);
			this.m_ctglN.Select(true, true, false);
			this.m_ctglSmart.Select(true, true, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(this.OnOK));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(this.Close));
			this.m_bInit = true;
			this.m_fRemovePanelOriginalPosY = this.m_NKM_UI_POPUP_REMOVE_PANEL.transform.position.y;
			this.m_fRemovePanelOriginalHeight = this.m_NKM_UI_POPUP_REMOVE_PANEL.GetHeight();
		}

		// Token: 0x06006F29 RID: 28457 RVA: 0x0024BAA2 File Offset: 0x00249CA2
		public void Open(NKCUIUnitSelectListRemovePopup.OnRemoveUnits onRemoveUnits, bool bOperator = false)
		{
			this.Init();
			this.dOnRemoveUnits = onRemoveUnits;
			base.gameObject.SetActive(true);
			this.m_bOperatorOption = bOperator;
			NKCUtil.SetGameobjectActive(this.m_ENABLE, !this.m_bOperatorOption);
			this.ChangePanelSize();
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x0024BAE0 File Offset: 0x00249CE0
		private void OnOK()
		{
			HashSet<NKM_UNIT_GRADE> hashSet = new HashSet<NKM_UNIT_GRADE>();
			if (this.m_ctglSSR != null && this.m_ctglSSR.m_bChecked)
			{
				hashSet.Add(NKM_UNIT_GRADE.NUG_SSR);
			}
			if (this.m_ctglSR != null && this.m_ctglSR.m_bChecked)
			{
				hashSet.Add(NKM_UNIT_GRADE.NUG_SR);
			}
			if (this.m_ctglR != null && this.m_ctglR.m_bChecked)
			{
				hashSet.Add(NKM_UNIT_GRADE.NUG_R);
			}
			if (this.m_ctglN != null && this.m_ctglN.m_bChecked)
			{
				hashSet.Add(NKM_UNIT_GRADE.NUG_N);
			}
			if (!this.m_bOperatorOption)
			{
				NKCUIUnitSelectListRemovePopup.OnRemoveUnits onRemoveUnits = this.dOnRemoveUnits;
				if (onRemoveUnits != null)
				{
					onRemoveUnits(hashSet, this.m_ctglSmart != null && this.m_ctglSmart.m_bChecked);
				}
			}
			else
			{
				NKCUIUnitSelectListRemovePopup.OnRemoveUnits onRemoveUnits2 = this.dOnRemoveUnits;
				if (onRemoveUnits2 != null)
				{
					onRemoveUnits2(hashSet, false);
				}
			}
			this.Close();
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x0024BBD0 File Offset: 0x00249DD0
		public void Close()
		{
			this.dOnRemoveUnits = null;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x0024BBE8 File Offset: 0x00249DE8
		private void ChangePanelSize()
		{
			if (this.m_bOperatorOption)
			{
				this.m_NKM_UI_POPUP_REMOVE_PANEL.transform.position = new Vector3(this.m_NKM_UI_POPUP_REMOVE_PANEL.transform.position.x, this.m_fRemovePanelOriginalPosY - this.m_fRemovePanelPosYGap, this.m_NKM_UI_POPUP_REMOVE_PANEL.transform.position.z);
				this.m_NKM_UI_POPUP_REMOVE_PANEL.SetHeight(this.m_fRemovePanelHeightForOperator);
				return;
			}
			this.m_NKM_UI_POPUP_REMOVE_PANEL.transform.position = new Vector3(this.m_NKM_UI_POPUP_REMOVE_PANEL.transform.position.x, this.m_fRemovePanelOriginalPosY, this.m_NKM_UI_POPUP_REMOVE_PANEL.transform.position.z);
			this.m_NKM_UI_POPUP_REMOVE_PANEL.SetHeight(this.m_fRemovePanelOriginalHeight);
		}

		// Token: 0x04005A8F RID: 23183
		[Header("희귀도 옵션")]
		public NKCUIComToggle m_ctglSSR;

		// Token: 0x04005A90 RID: 23184
		public NKCUIComToggle m_ctglSR;

		// Token: 0x04005A91 RID: 23185
		public NKCUIComToggle m_ctglR;

		// Token: 0x04005A92 RID: 23186
		public NKCUIComToggle m_ctglN;

		// Token: 0x04005A93 RID: 23187
		[Header("해고 옵션")]
		public GameObject m_ENABLE;

		// Token: 0x04005A94 RID: 23188
		public NKCUIComToggle m_ctglSmart;

		// Token: 0x04005A95 RID: 23189
		public NKCUIComToggle m_ctglAll;

		// Token: 0x04005A96 RID: 23190
		[Header("결정")]
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x04005A97 RID: 23191
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x04005A98 RID: 23192
		private NKCUIUnitSelectListRemovePopup.OnRemoveUnits dOnRemoveUnits;

		// Token: 0x04005A99 RID: 23193
		private bool m_bInit;

		// Token: 0x04005A9A RID: 23194
		public RectTransform m_NKM_UI_POPUP_REMOVE_PANEL;

		// Token: 0x04005A9B RID: 23195
		private float m_fRemovePanelOriginalPosY;

		// Token: 0x04005A9C RID: 23196
		private float m_fRemovePanelOriginalHeight;

		// Token: 0x04005A9D RID: 23197
		private float m_fRemovePanelPosYGap = 104.7f;

		// Token: 0x04005A9E RID: 23198
		private float m_fRemovePanelHeightForOperator = 490f;

		// Token: 0x04005A9F RID: 23199
		private bool m_bOperatorOption;

		// Token: 0x02001733 RID: 5939
		// (Invoke) Token: 0x0600B29E RID: 45726
		public delegate void OnRemoveUnits(HashSet<NKM_UNIT_GRADE> setUnitGrade, bool bSmart);
	}
}
