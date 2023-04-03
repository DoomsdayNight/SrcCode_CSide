using System;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x02000356 RID: 854
	public class ColorPickerTester : MonoBehaviour
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x0004C6C8 File Offset: 0x0004A8C8
		private void Awake()
		{
			this.pickerRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0004C6D6 File Offset: 0x0004A8D6
		private void Start()
		{
			this.picker.onValueChanged.AddListener(delegate(Color color)
			{
				this.pickerRenderer.material.color = color;
			});
		}

		// Token: 0x04000E2B RID: 3627
		public Renderer pickerRenderer;

		// Token: 0x04000E2C RID: 3628
		public ColorPickerControl picker;
	}
}
