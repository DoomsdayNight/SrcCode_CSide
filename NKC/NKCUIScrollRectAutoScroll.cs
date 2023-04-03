using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000770 RID: 1904
	[RequireComponent(typeof(ScrollRect))]
	public class NKCUIScrollRectAutoScroll : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06004BF9 RID: 19449 RVA: 0x0016BA11 File Offset: 0x00169C11
		private void OnEnable()
		{
			if (this.m_ScrollRect)
			{
				this.m_ScrollRect.content.GetComponentsInChildren<Selectable>(this.m_Selectables);
			}
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0016BA36 File Offset: 0x00169C36
		private void Awake()
		{
			this.m_ScrollRect = base.GetComponent<ScrollRect>();
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x0016BA44 File Offset: 0x00169C44
		private void Start()
		{
			if (this.m_ScrollRect)
			{
				this.m_ScrollRect.content.GetComponentsInChildren<Selectable>(this.m_Selectables);
			}
			this.ScrollToSelected(true);
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x0016BA70 File Offset: 0x00169C70
		private void Update()
		{
			this.InputScroll();
			if (!this.mouseOver)
			{
				this.m_ScrollRect.normalizedPosition = Vector2.Lerp(this.m_ScrollRect.normalizedPosition, this.m_NextScrollPosition, this.scrollSpeed * Time.deltaTime);
				return;
			}
			this.m_NextScrollPosition = this.m_ScrollRect.normalizedPosition;
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0016BACC File Offset: 0x00169CCC
		private void InputScroll()
		{
			if (this.m_Selectables.Count > 0 && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical") || Input.GetButton("Horizontal") || Input.GetButton("Vertical")))
			{
				this.ScrollToSelected(false);
			}
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0016BB20 File Offset: 0x00169D20
		private void ScrollToSelected(bool quickScroll)
		{
			int num = -1;
			Selectable selectable = EventSystem.current.currentSelectedGameObject ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null;
			if (selectable)
			{
				num = this.m_Selectables.IndexOf(selectable);
			}
			if (num > -1)
			{
				if (quickScroll)
				{
					this.m_ScrollRect.normalizedPosition = new Vector2(0f, 1f - (float)num / ((float)this.m_Selectables.Count - 1f));
					this.m_NextScrollPosition = this.m_ScrollRect.normalizedPosition;
					return;
				}
				this.m_NextScrollPosition = new Vector2(0f, 1f - (float)num / ((float)this.m_Selectables.Count - 1f));
			}
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0016BBDC File Offset: 0x00169DDC
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.mouseOver = true;
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x0016BBE5 File Offset: 0x00169DE5
		public void OnPointerExit(PointerEventData eventData)
		{
			this.mouseOver = false;
			this.ScrollToSelected(false);
		}

		// Token: 0x04003A62 RID: 14946
		public float scrollSpeed = 10f;

		// Token: 0x04003A63 RID: 14947
		private bool mouseOver;

		// Token: 0x04003A64 RID: 14948
		private List<Selectable> m_Selectables = new List<Selectable>();

		// Token: 0x04003A65 RID: 14949
		private ScrollRect m_ScrollRect;

		// Token: 0x04003A66 RID: 14950
		private Vector2 m_NextScrollPosition = Vector2.up;
	}
}
