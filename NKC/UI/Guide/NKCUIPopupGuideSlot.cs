using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guide
{
	// Token: 0x02000C37 RID: 3127
	public class NKCUIPopupGuideSlot : MonoBehaviour
	{
		// Token: 0x06009146 RID: 37190 RVA: 0x0031846C File Offset: 0x0031666C
		public void Init(string title, NKCUIComToggleGroup toggleGroup)
		{
			this.m_Toggle.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChange));
			NKCUtil.SetLabelText(this.m_TEXT, title);
			this.m_Toggle.SetToggleGroup(toggleGroup);
		}

		// Token: 0x06009147 RID: 37191 RVA: 0x003184A4 File Offset: 0x003166A4
		public void OnValueChange(bool bVal)
		{
			Color col = bVal ? NKCUtil.GetColor("#011B3B") : NKCUtil.GetColor("#FFFFFF");
			NKCUtil.SetLabelTextColor(this.m_TEXT, col);
			this.OnActiveChild(bVal);
		}

		// Token: 0x06009148 RID: 37192 RVA: 0x003184DE File Offset: 0x003166DE
		public void AddSubSlot(NKCUIPopupGuideSubSlot child)
		{
			this.m_lstSubSlot.Add(child);
		}

		// Token: 0x06009149 RID: 37193 RVA: 0x003184EC File Offset: 0x003166EC
		public bool SelectSubSlot(string ARTICLE_ID)
		{
			bool flag = false;
			foreach (NKCUIPopupGuideSubSlot nkcuipopupGuideSubSlot in this.m_lstSubSlot)
			{
				bool flag2 = nkcuipopupGuideSubSlot.OnSelected(ARTICLE_ID);
				if (!flag)
				{
					flag = flag2;
				}
			}
			this.m_Toggle.Select(flag, false, false);
			return flag;
		}

		// Token: 0x0600914A RID: 37194 RVA: 0x00318558 File Offset: 0x00316758
		private void OnActiveChild(bool bActive)
		{
			foreach (NKCUIPopupGuideSubSlot nkcuipopupGuideSubSlot in this.m_lstSubSlot)
			{
				nkcuipopupGuideSubSlot.OnActive(bActive);
			}
		}

		// Token: 0x0600914B RID: 37195 RVA: 0x003185AC File Offset: 0x003167AC
		public bool HasChild(string ARTICLE_ID)
		{
			for (int i = 0; i < this.m_lstSubSlot.Count; i++)
			{
				if (string.Equals(ARTICLE_ID, this.m_lstSubSlot[i].ARTICLE_ID))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600914C RID: 37196 RVA: 0x003185EC File Offset: 0x003167EC
		public void Clear()
		{
			foreach (NKCUIPopupGuideSubSlot nkcuipopupGuideSubSlot in this.m_lstSubSlot)
			{
				UnityEngine.Object.Destroy(nkcuipopupGuideSubSlot.gameObject);
			}
			this.m_lstSubSlot.Clear();
		}

		// Token: 0x04007E78 RID: 32376
		public Text m_TEXT;

		// Token: 0x04007E79 RID: 32377
		public NKCUIComToggle m_Toggle;

		// Token: 0x04007E7A RID: 32378
		private List<NKCUIPopupGuideSubSlot> m_lstSubSlot = new List<NKCUIPopupGuideSubSlot>();
	}
}
