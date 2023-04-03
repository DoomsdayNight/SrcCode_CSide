using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x020002B0 RID: 688
	[RequireComponent(typeof(InputField))]
	[AddComponentMenu("UI/Extensions/Return Key Trigger")]
	public class ReturnKeyTriggersButton : MonoBehaviour, ISubmitHandler, IEventSystemHandler
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x00029D68 File Offset: 0x00027F68
		private void Start()
		{
			this._system = EventSystem.current;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00029D75 File Offset: 0x00027F75
		private void RemoveHighlight()
		{
			this.button.OnPointerExit(new PointerEventData(this._system));
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00029D90 File Offset: 0x00027F90
		public void OnSubmit(BaseEventData eventData)
		{
			if (this.highlight)
			{
				this.button.OnPointerEnter(new PointerEventData(this._system));
			}
			this.button.OnPointerClick(new PointerEventData(this._system));
			if (this.highlight)
			{
				base.Invoke("RemoveHighlight", this.highlightDuration);
			}
		}

		// Token: 0x040009AE RID: 2478
		private EventSystem _system;

		// Token: 0x040009AF RID: 2479
		public Button button;

		// Token: 0x040009B0 RID: 2480
		private bool highlight = true;

		// Token: 0x040009B1 RID: 2481
		public float highlightDuration = 0.2f;
	}
}
