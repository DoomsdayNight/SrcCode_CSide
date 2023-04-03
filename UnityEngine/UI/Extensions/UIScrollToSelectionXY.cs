using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000348 RID: 840
	[AddComponentMenu("UI/Extensions/UI ScrollTo Selection XY")]
	[RequireComponent(typeof(ScrollRect))]
	public class UIScrollToSelectionXY : MonoBehaviour
	{
		// Token: 0x060013D0 RID: 5072 RVA: 0x00049F53 File Offset: 0x00048153
		private void Start()
		{
			this.targetScrollRect = base.GetComponent<ScrollRect>();
			this.scrollWindow = this.targetScrollRect.GetComponent<RectTransform>();
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00049F72 File Offset: 0x00048172
		private void Update()
		{
			this.ScrollRectToLevelSelection();
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00049F7C File Offset: 0x0004817C
		private void ScrollRectToLevelSelection()
		{
			EventSystem current = EventSystem.current;
			if (this.targetScrollRect == null || this.layoutListGroup == null || this.scrollWindow == null)
			{
				return;
			}
			RectTransform rectTransform = (current.currentSelectedGameObject != null) ? current.currentSelectedGameObject.GetComponent<RectTransform>() : null;
			if (rectTransform != this.targetScrollObject)
			{
				this.scrollToSelection = true;
			}
			if (rectTransform == null || !this.scrollToSelection || rectTransform.transform.parent != this.layoutListGroup.transform)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			if (this.targetScrollRect.vertical)
			{
				float num = -rectTransform.anchoredPosition.y;
				float num2 = this.layoutListGroup.anchoredPosition.y - num;
				this.targetScrollRect.verticalNormalizedPosition += num2 / this.layoutListGroup.sizeDelta.y * Time.deltaTime * this.scrollSpeed;
				flag2 = (Mathf.Abs(num2) < 2f);
			}
			if (this.targetScrollRect.horizontal)
			{
				float num3 = -rectTransform.anchoredPosition.x;
				float num4 = this.layoutListGroup.anchoredPosition.x - num3;
				this.targetScrollRect.horizontalNormalizedPosition += num4 / this.layoutListGroup.sizeDelta.x * Time.deltaTime * this.scrollSpeed;
				flag = (Mathf.Abs(num4) < 2f);
			}
			if (flag && flag2)
			{
				this.scrollToSelection = false;
			}
			this.targetScrollObject = rectTransform;
		}

		// Token: 0x04000DA7 RID: 3495
		public float scrollSpeed = 10f;

		// Token: 0x04000DA8 RID: 3496
		[SerializeField]
		private RectTransform layoutListGroup;

		// Token: 0x04000DA9 RID: 3497
		private RectTransform targetScrollObject;

		// Token: 0x04000DAA RID: 3498
		private bool scrollToSelection = true;

		// Token: 0x04000DAB RID: 3499
		private RectTransform scrollWindow;

		// Token: 0x04000DAC RID: 3500
		private ScrollRect targetScrollRect;
	}
}
