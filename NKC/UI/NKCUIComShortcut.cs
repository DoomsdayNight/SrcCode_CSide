using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000946 RID: 2374
	public class NKCUIComShortcut : MonoBehaviour
	{
		// Token: 0x06005ECE RID: 24270 RVA: 0x001D7070 File Offset: 0x001D5270
		private void Awake()
		{
			if (this.m_ShortcutType == NKM_SHORTCUT_TYPE.SHORTCUT_NONE)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUIComStateButton component = base.GetComponent<NKCUIComStateButton>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			component.PointerClick.RemoveAllListeners();
			component.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
		}

		// Token: 0x06005ECF RID: 24271 RVA: 0x001D70DD File Offset: 0x001D52DD
		private void OnClickBtn()
		{
			NKCContentManager.MoveToShortCut(this.m_ShortcutType, this.m_ShortcutParam, this.m_bForce);
		}

		// Token: 0x04004AEC RID: 19180
		public NKM_SHORTCUT_TYPE m_ShortcutType;

		// Token: 0x04004AED RID: 19181
		public string m_ShortcutParam = "";

		// Token: 0x04004AEE RID: 19182
		public bool m_bForce;
	}
}
