using System;
using System.Collections;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200032E RID: 814
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Extensions/Tooltip/Tooltip Trigger")]
	public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x0600130F RID: 4879 RVA: 0x000471A8 File Offset: 0x000453A8
		private void Start()
		{
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			if (componentInParent && componentInParent.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				this.isChildOfOverlayCanvas = true;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000471D3 File Offset: 0x000453D3
		public bool WorldToScreenIsRequired
		{
			get
			{
				return (this.isChildOfOverlayCanvas && ToolTip.Instance.guiMode == RenderMode.ScreenSpaceCamera) || (!this.isChildOfOverlayCanvas && ToolTip.Instance.guiMode == RenderMode.ScreenSpaceOverlay);
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00047204 File Offset: 0x00045404
		public void OnPointerEnter(PointerEventData eventData)
		{
			switch (this.tooltipPositioningType)
			{
			case TooltipTrigger.TooltipPositioningType.mousePosition:
				this.StartHover(UIExtensionsInputManager.MousePosition + this.offset, true);
				return;
			case TooltipTrigger.TooltipPositioningType.mousePositionAndFollow:
				this.StartHover(UIExtensionsInputManager.MousePosition + this.offset, true);
				this.hovered = true;
				base.StartCoroutine(this.HoveredMouseFollowingLoop());
				return;
			case TooltipTrigger.TooltipPositioningType.transformPosition:
				this.StartHover((this.WorldToScreenIsRequired ? ToolTip.Instance.GuiCamera.WorldToScreenPoint(base.transform.position) : base.transform.position) + this.offset, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000472B0 File Offset: 0x000454B0
		private IEnumerator HoveredMouseFollowingLoop()
		{
			while (this.hovered)
			{
				this.StartHover(UIExtensionsInputManager.MousePosition + this.offset, false);
				yield return null;
			}
			yield break;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000472C0 File Offset: 0x000454C0
		public void OnSelect(BaseEventData eventData)
		{
			this.StartHover((this.WorldToScreenIsRequired ? ToolTip.Instance.GuiCamera.WorldToScreenPoint(base.transform.position) : base.transform.position) + this.offset, true);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004730E File Offset: 0x0004550E
		public void OnPointerExit(PointerEventData eventData)
		{
			this.StopHover();
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00047316 File Offset: 0x00045516
		public void OnDeselect(BaseEventData eventData)
		{
			this.StopHover();
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004731E File Offset: 0x0004551E
		private void StartHover(Vector3 position, bool shouldCanvasUpdate = false)
		{
			ToolTip.Instance.SetTooltip(this.text, position, shouldCanvasUpdate);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00047332 File Offset: 0x00045532
		private void StopHover()
		{
			this.hovered = false;
			ToolTip.Instance.HideTooltip();
		}

		// Token: 0x04000D45 RID: 3397
		[TextArea]
		public string text;

		// Token: 0x04000D46 RID: 3398
		[Tooltip("Defines where the tooltip will be placed and how that placement will occur. Transform position will always be used if this element wasn't selected via mouse")]
		public TooltipTrigger.TooltipPositioningType tooltipPositioningType;

		// Token: 0x04000D47 RID: 3399
		private bool isChildOfOverlayCanvas;

		// Token: 0x04000D48 RID: 3400
		private bool hovered;

		// Token: 0x04000D49 RID: 3401
		public Vector3 offset;

		// Token: 0x02001166 RID: 4454
		public enum TooltipPositioningType
		{
			// Token: 0x04009243 RID: 37443
			mousePosition,
			// Token: 0x04009244 RID: 37444
			mousePositionAndFollow,
			// Token: 0x04009245 RID: 37445
			transformPosition
		}
	}
}
