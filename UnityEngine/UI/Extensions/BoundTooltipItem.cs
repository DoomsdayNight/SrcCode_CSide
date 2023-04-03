using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200032A RID: 810
	[AddComponentMenu("UI/Extensions/Bound Tooltip/Bound Tooltip Item")]
	public class BoundTooltipItem : MonoBehaviour
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x000460A3 File Offset: 0x000442A3
		public bool IsActive
		{
			get
			{
				return base.gameObject.activeSelf;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000460B0 File Offset: 0x000442B0
		private void Awake()
		{
			BoundTooltipItem.instance = this;
			if (!this.TooltipText)
			{
				this.TooltipText = base.GetComponentInChildren<Text>();
			}
			this.HideTooltip();
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000460D8 File Offset: 0x000442D8
		public void ShowTooltip(string text, Vector3 pos)
		{
			if (this.TooltipText.text != text)
			{
				this.TooltipText.text = text;
			}
			base.transform.position = pos + this.ToolTipOffset;
			base.gameObject.SetActive(true);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00046127 File Offset: 0x00044327
		public void HideTooltip()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00046135 File Offset: 0x00044335
		public static BoundTooltipItem Instance
		{
			get
			{
				if (BoundTooltipItem.instance == null)
				{
					BoundTooltipItem.instance = Object.FindObjectOfType<BoundTooltipItem>();
				}
				return BoundTooltipItem.instance;
			}
		}

		// Token: 0x04000D16 RID: 3350
		public Text TooltipText;

		// Token: 0x04000D17 RID: 3351
		public Vector3 ToolTipOffset;

		// Token: 0x04000D18 RID: 3352
		private static BoundTooltipItem instance;
	}
}
