using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200032B RID: 811
	[AddComponentMenu("UI/Extensions/Bound Tooltip/Bound Tooltip Trigger")]
	public class BoundTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x060012EF RID: 4847 RVA: 0x0004615C File Offset: 0x0004435C
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.useMousePosition)
			{
				this.StartHover(new Vector3(eventData.position.x, eventData.position.y, 0f));
				return;
			}
			this.StartHover(base.transform.position + this.offset);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000461B4 File Offset: 0x000443B4
		public void OnSelect(BaseEventData eventData)
		{
			this.StartHover(base.transform.position);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000461C7 File Offset: 0x000443C7
		public void OnPointerExit(PointerEventData eventData)
		{
			this.StopHover();
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000461CF File Offset: 0x000443CF
		public void OnDeselect(BaseEventData eventData)
		{
			this.StopHover();
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000461D7 File Offset: 0x000443D7
		private void StartHover(Vector3 position)
		{
			BoundTooltipItem.Instance.ShowTooltip(this.text, position);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000461EA File Offset: 0x000443EA
		private void StopHover()
		{
			BoundTooltipItem.Instance.HideTooltip();
		}

		// Token: 0x04000D19 RID: 3353
		[TextArea]
		public string text;

		// Token: 0x04000D1A RID: 3354
		public bool useMousePosition;

		// Token: 0x04000D1B RID: 3355
		public Vector3 offset;
	}
}
