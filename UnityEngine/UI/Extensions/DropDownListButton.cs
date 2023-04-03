using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002B9 RID: 697
	[RequireComponent(typeof(RectTransform), typeof(Button))]
	public class DropDownListButton
	{
		// Token: 0x06000E85 RID: 3717 RVA: 0x0002C980 File Offset: 0x0002AB80
		public DropDownListButton(GameObject btnObj)
		{
			this.gameobject = btnObj;
			this.rectTransform = btnObj.GetComponent<RectTransform>();
			this.btnImg = btnObj.GetComponent<Image>();
			this.btn = btnObj.GetComponent<Button>();
			this.txt = this.rectTransform.Find("Text").GetComponent<Text>();
			this.img = this.rectTransform.Find("Image").GetComponent<Image>();
		}

		// Token: 0x04000A24 RID: 2596
		public RectTransform rectTransform;

		// Token: 0x04000A25 RID: 2597
		public Button btn;

		// Token: 0x04000A26 RID: 2598
		public Text txt;

		// Token: 0x04000A27 RID: 2599
		public Image btnImg;

		// Token: 0x04000A28 RID: 2600
		public Image img;

		// Token: 0x04000A29 RID: 2601
		public GameObject gameobject;
	}
}
