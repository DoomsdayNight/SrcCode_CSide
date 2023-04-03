using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x02000352 RID: 850
	[RequireComponent(typeof(Image))]
	public class ColorImage : MonoBehaviour
	{
		// Token: 0x06001424 RID: 5156 RVA: 0x0004BCBD File Offset: 0x00049EBD
		private void Awake()
		{
			this.image = base.GetComponent<Image>();
			this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0004BCE7 File Offset: 0x00049EE7
		private void OnDestroy()
		{
			this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0004BD05 File Offset: 0x00049F05
		private void ColorChanged(Color newColor)
		{
			this.image.color = newColor;
		}

		// Token: 0x04000E0B RID: 3595
		public ColorPickerControl picker;

		// Token: 0x04000E0C RID: 3596
		private Image image;
	}
}
