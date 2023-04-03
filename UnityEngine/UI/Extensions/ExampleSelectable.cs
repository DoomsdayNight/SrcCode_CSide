using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C9 RID: 713
	public class ExampleSelectable : MonoBehaviour, IBoxSelectable
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00030120 File Offset: 0x0002E320
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x00030128 File Offset: 0x0002E328
		public bool selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				this._selected = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00030131 File Offset: 0x0002E331
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x00030139 File Offset: 0x0002E339
		public bool preSelected
		{
			get
			{
				return this._preSelected;
			}
			set
			{
				this._preSelected = value;
			}
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00030142 File Offset: 0x0002E342
		private void Start()
		{
			this.spriteRenderer = base.transform.GetComponent<SpriteRenderer>();
			this.image = base.transform.GetComponent<Image>();
			this.text = base.transform.GetComponent<Text>();
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00030178 File Offset: 0x0002E378
		private void Update()
		{
			Color color = Color.white;
			if (this.preSelected)
			{
				color = Color.yellow;
			}
			if (this.selected)
			{
				color = Color.green;
			}
			if (this.spriteRenderer)
			{
				this.spriteRenderer.color = color;
				return;
			}
			if (this.text)
			{
				this.text.color = color;
				return;
			}
			if (this.image)
			{
				this.image.color = color;
				return;
			}
			if (base.GetComponent<Renderer>())
			{
				base.GetComponent<Renderer>().material.color = color;
			}
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003021B File Offset: 0x0002E41B
		Transform IBoxSelectable.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000AB1 RID: 2737
		private bool _selected;

		// Token: 0x04000AB2 RID: 2738
		private bool _preSelected;

		// Token: 0x04000AB3 RID: 2739
		private SpriteRenderer spriteRenderer;

		// Token: 0x04000AB4 RID: 2740
		private Image image;

		// Token: 0x04000AB5 RID: 2741
		private Text text;
	}
}
