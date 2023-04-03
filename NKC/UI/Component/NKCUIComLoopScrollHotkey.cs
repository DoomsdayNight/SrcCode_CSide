using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C5C RID: 3164
	[RequireComponent(typeof(LoopScrollRect))]
	public class NKCUIComLoopScrollHotkey : MonoBehaviour
	{
		// Token: 0x06009366 RID: 37734 RVA: 0x0032500F File Offset: 0x0032320F
		public static void AddHotkey(LoopScrollRect sr, NKCUIBase uiRoot = null)
		{
			if (sr == null)
			{
				return;
			}
			if (sr.gameObject.GetComponent<NKCUIComLoopScrollHotkey>() == null)
			{
				sr.gameObject.AddComponent<NKCUIComLoopScrollHotkey>().m_uiRoot = uiRoot;
			}
		}

		// Token: 0x06009367 RID: 37735 RVA: 0x0032503F File Offset: 0x0032323F
		private void Start()
		{
			if (this.m_uiRoot == null)
			{
				this.m_uiRoot = NKCUIManager.FindRootUIBase(base.transform);
			}
			this.m_srTarget = base.GetComponent<LoopScrollRect>();
		}

		// Token: 0x06009368 RID: 37736 RVA: 0x0032506C File Offset: 0x0032326C
		private void Update()
		{
			if (this.m_srTarget == null)
			{
				return;
			}
			if (!NKCUIManager.IsTopmostUI(this.m_uiRoot))
			{
				return;
			}
			if (this.m_srTarget.horizontal)
			{
				if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Right))
				{
					this.m_srTarget.MovePosition(new Vector2(-this.Speed * Time.deltaTime, 0f));
				}
				else if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Left))
				{
					this.m_srTarget.MovePosition(new Vector2(this.Speed * Time.deltaTime, 0f));
				}
			}
			if (this.m_srTarget.vertical)
			{
				if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Up))
				{
					this.m_srTarget.MovePosition(new Vector2(0f, -this.Speed * Time.deltaTime));
				}
				else if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Down))
				{
					this.m_srTarget.MovePosition(new Vector2(0f, this.Speed * Time.deltaTime));
				}
			}
			if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
			{
				NKCUIComHotkeyDisplay.OpenInstance(this.m_srTarget, null);
			}
		}

		// Token: 0x0400805A RID: 32858
		public float Speed = 4000f;

		// Token: 0x0400805B RID: 32859
		private LoopScrollRect m_srTarget;

		// Token: 0x0400805C RID: 32860
		private NKCUIBase m_uiRoot;
	}
}
