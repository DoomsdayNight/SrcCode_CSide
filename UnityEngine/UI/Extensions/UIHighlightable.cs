using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000345 RID: 837
	[AddComponentMenu("UI/Extensions/UI Highlightable Extension")]
	[RequireComponent(typeof(RectTransform), typeof(Graphic))]
	public class UIHighlightable : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00049758 File Offset: 0x00047958
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00049760 File Offset: 0x00047960
		public bool Interactable
		{
			get
			{
				return this.m_Interactable;
			}
			set
			{
				this.m_Interactable = value;
				this.HighlightInteractable(this.m_Graphic);
				this.OnInteractableChanged.Invoke(this.m_Interactable);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00049786 File Offset: 0x00047986
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0004978E File Offset: 0x0004798E
		public bool ClickToHold
		{
			get
			{
				return this.m_ClickToHold;
			}
			set
			{
				this.m_ClickToHold = value;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00049797 File Offset: 0x00047997
		private void Awake()
		{
			this.m_Graphic = base.GetComponent<Graphic>();
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000497A5 File Offset: 0x000479A5
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.Interactable && !this.m_Pressed)
			{
				this.m_Highlighted = true;
				this.m_Graphic.color = this.HighlightedColor;
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000497CF File Offset: 0x000479CF
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.Interactable && !this.m_Pressed)
			{
				this.m_Highlighted = false;
				this.m_Graphic.color = this.NormalColor;
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000497F9 File Offset: 0x000479F9
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.Interactable)
			{
				this.m_Graphic.color = this.PressedColor;
				if (this.ClickToHold)
				{
					this.m_Pressed = !this.m_Pressed;
				}
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0004982B File Offset: 0x00047A2B
		public void OnPointerUp(PointerEventData eventData)
		{
			if (!this.m_Pressed)
			{
				this.HighlightInteractable(this.m_Graphic);
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00049841 File Offset: 0x00047A41
		private void HighlightInteractable(Graphic graphic)
		{
			if (!this.m_Interactable)
			{
				graphic.color = this.DisabledColor;
				return;
			}
			if (this.m_Highlighted)
			{
				graphic.color = this.HighlightedColor;
				return;
			}
			graphic.color = this.NormalColor;
		}

		// Token: 0x04000D8A RID: 3466
		private Graphic m_Graphic;

		// Token: 0x04000D8B RID: 3467
		private bool m_Highlighted;

		// Token: 0x04000D8C RID: 3468
		private bool m_Pressed;

		// Token: 0x04000D8D RID: 3469
		[SerializeField]
		[Tooltip("Can this panel be interacted with or is it disabled? (does not affect child components)")]
		private bool m_Interactable = true;

		// Token: 0x04000D8E RID: 3470
		[SerializeField]
		[Tooltip("Does the panel remain in the pressed state when clicked? (default false)")]
		private bool m_ClickToHold;

		// Token: 0x04000D8F RID: 3471
		[Tooltip("The default color for the panel")]
		public Color NormalColor = Color.grey;

		// Token: 0x04000D90 RID: 3472
		[Tooltip("The color for the panel when a mouse is over it or it is in focus")]
		public Color HighlightedColor = Color.yellow;

		// Token: 0x04000D91 RID: 3473
		[Tooltip("The color for the panel when it is clicked/held")]
		public Color PressedColor = Color.green;

		// Token: 0x04000D92 RID: 3474
		[Tooltip("The color for the panel when it is not interactable (see Interactable)")]
		public Color DisabledColor = Color.gray;

		// Token: 0x04000D93 RID: 3475
		[Tooltip("Event for when the panel is enabled / disabled, to enable disabling / enabling of child or other gameobjects")]
		public UIHighlightable.InteractableChangedEvent OnInteractableChanged;

		// Token: 0x02001171 RID: 4465
		[Serializable]
		public class InteractableChangedEvent : UnityEvent<bool>
		{
		}
	}
}
