using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C5E RID: 3166
	[RequireComponent(typeof(ScrollRect))]
	public class NKCUIComScrollRectHotkey : MonoBehaviour
	{
		// Token: 0x0600936D RID: 37741 RVA: 0x0032520B File Offset: 0x0032340B
		public static void AddHotkey(ScrollRect sr, NKCUIBase uiRoot = null)
		{
			if (sr == null)
			{
				return;
			}
			if (sr.gameObject.GetComponent<NKCUIComScrollRectHotkey>() == null)
			{
				sr.gameObject.AddComponent<NKCUIComScrollRectHotkey>().m_uiRoot = uiRoot;
			}
		}

		// Token: 0x0600936E RID: 37742 RVA: 0x0032523B File Offset: 0x0032343B
		private void Start()
		{
			if (this.m_uiRoot == null)
			{
				this.m_uiRoot = NKCUIManager.FindRootUIBase(base.transform);
			}
			this.m_srTarget = base.GetComponent<ScrollRect>();
		}

		// Token: 0x0600936F RID: 37743 RVA: 0x00325268 File Offset: 0x00323468
		private void Update()
		{
			if (this.m_srTarget == null)
			{
				return;
			}
			if (this.m_srTarget.content == null)
			{
				return;
			}
			if (!NKCUIManager.IsTopmostUI(this.m_uiRoot))
			{
				return;
			}
			Vector2 anchoredPosition = this.m_srTarget.content.anchoredPosition;
			if (this.m_srTarget.horizontal)
			{
				if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Right))
				{
					anchoredPosition.x -= this.Speed * Time.deltaTime;
					this.m_srTarget.content.anchoredPosition = anchoredPosition;
				}
				else if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Left))
				{
					anchoredPosition.x += this.Speed * Time.deltaTime;
					this.m_srTarget.content.anchoredPosition = anchoredPosition;
				}
			}
			if (this.m_srTarget.vertical)
			{
				if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Up))
				{
					anchoredPosition.y -= this.Speed * Time.deltaTime;
					this.m_srTarget.content.anchoredPosition = anchoredPosition;
				}
				else if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Down))
				{
					anchoredPosition.y += this.Speed * Time.deltaTime;
					this.m_srTarget.content.anchoredPosition = anchoredPosition;
				}
			}
			if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
			{
				NKCUIComHotkeyDisplay.OpenInstance(this.m_srTarget, null);
			}
		}

		// Token: 0x0400805F RID: 32863
		public float Speed = 4000f;

		// Token: 0x04008060 RID: 32864
		private ScrollRect m_srTarget;

		// Token: 0x04008061 RID: 32865
		private NKCUIBase m_uiRoot;
	}
}
