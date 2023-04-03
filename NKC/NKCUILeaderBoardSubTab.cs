using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200095D RID: 2397
	public class NKCUILeaderBoardSubTab : MonoBehaviour
	{
		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06005FB2 RID: 24498 RVA: 0x001DC8D0 File Offset: 0x001DAAD0
		// (set) Token: 0x06005FB3 RID: 24499 RVA: 0x001DC8D8 File Offset: 0x001DAAD8
		public int m_tabID { get; private set; }

		// Token: 0x06005FB4 RID: 24500 RVA: 0x001DC8E4 File Offset: 0x001DAAE4
		public void SetData(NKCUIComToggleGroup tglGroup, string title, int tabID, NKCUILeaderBoardSubTab.OnSelectSubTab onSelectSubTab)
		{
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			this.m_tgl.SetToggleGroup(tglGroup);
			this.m_tabID = tabID;
			this.dOnSelectSubTab = onSelectSubTab;
			this.m_tgl.OnValueChanged.RemoveAllListeners();
			this.m_tgl.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x001DC944 File Offset: 0x001DAB44
		public void OnValueChanged(bool bValue)
		{
			if (bValue)
			{
				NKCUILeaderBoardSubTab.OnSelectSubTab onSelectSubTab = this.dOnSelectSubTab;
				if (onSelectSubTab == null)
				{
					return;
				}
				onSelectSubTab(this.m_tabID);
			}
		}

		// Token: 0x04004BD3 RID: 19411
		public NKCUIComToggle m_tgl;

		// Token: 0x04004BD4 RID: 19412
		public Text m_lbTitle;

		// Token: 0x04004BD5 RID: 19413
		private NKCUILeaderBoardSubTab.OnSelectSubTab dOnSelectSubTab;

		// Token: 0x020015DD RID: 5597
		// (Invoke) Token: 0x0600AE79 RID: 44665
		public delegate void OnSelectSubTab(int tabID);
	}
}
