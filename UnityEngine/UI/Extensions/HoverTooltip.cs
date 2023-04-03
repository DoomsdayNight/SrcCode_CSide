using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200032C RID: 812
	[AddComponentMenu("UI/Extensions/HoverTooltip")]
	public class HoverTooltip : MonoBehaviour
	{
		// Token: 0x060012F6 RID: 4854 RVA: 0x00046200 File Offset: 0x00044400
		private void Start()
		{
			this.GUICamera = GameObject.Find("GUICamera").GetComponent<Camera>();
			this.GUIMode = base.transform.parent.parent.GetComponent<Canvas>().renderMode;
			this.bgImageSource = this.bgImage.GetComponent<Image>();
			this.inside = false;
			this.HideTooltipVisibility();
			base.transform.parent.gameObject.SetActive(false);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00046276 File Offset: 0x00044476
		public void SetTooltip(string text)
		{
			this.NewTooltip();
			this.thisText.text = text;
			this.OnScreenSpaceCamera();
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00046290 File Offset: 0x00044490
		public void SetTooltip(string[] texts)
		{
			this.NewTooltip();
			string text = "";
			int num = 0;
			foreach (string text2 in texts)
			{
				if (num == 0)
				{
					text += text2;
				}
				else
				{
					text = text + "\n" + text2;
				}
				num++;
			}
			this.thisText.text = text;
			this.OnScreenSpaceCamera();
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000462F2 File Offset: 0x000444F2
		public void SetTooltip(string text, bool test)
		{
			this.NewTooltip();
			this.thisText.text = text;
			this.OnScreenSpaceCamera();
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004630C File Offset: 0x0004450C
		public void OnScreenSpaceCamera()
		{
			Vector3 vector = this.GUICamera.ScreenToViewportPoint(UIExtensionsInputManager.MousePosition);
			float num = this.GUICamera.ViewportToScreenPoint(vector).x + this.tooltipRealWidth * this.bgImage.pivot.x;
			if (num > this.upperRight.x)
			{
				float num2 = this.upperRight.x - num;
				float num3;
				if ((double)num2 > (double)this.defaultXOffset * 0.75)
				{
					num3 = num2;
				}
				else
				{
					num3 = this.defaultXOffset - this.tooltipRealWidth * 2f;
				}
				Vector3 position = new Vector3(this.GUICamera.ViewportToScreenPoint(vector).x + num3, 0f, 0f);
				vector.x = this.GUICamera.ScreenToViewportPoint(position).x;
			}
			num = this.GUICamera.ViewportToScreenPoint(vector).x - this.tooltipRealWidth * this.bgImage.pivot.x;
			if (num < this.lowerLeft.x)
			{
				float num4 = this.lowerLeft.x - num;
				float num3;
				if ((double)num4 < (double)this.defaultXOffset * 0.75 - (double)this.tooltipRealWidth)
				{
					num3 = -num4;
				}
				else
				{
					num3 = this.tooltipRealWidth * 2f;
				}
				Vector3 position2 = new Vector3(this.GUICamera.ViewportToScreenPoint(vector).x - num3, 0f, 0f);
				vector.x = this.GUICamera.ScreenToViewportPoint(position2).x;
			}
			num = this.GUICamera.ViewportToScreenPoint(vector).y - (this.bgImage.sizeDelta.y * this.currentYScaleFactor * this.bgImage.pivot.y - this.tooltipRealHeight);
			if (num > this.upperRight.y)
			{
				float num5 = this.upperRight.y - num;
				float num6 = this.bgImage.sizeDelta.y * this.currentYScaleFactor * this.bgImage.pivot.y;
				if ((double)num5 > (double)this.defaultYOffset * 0.75)
				{
					num6 = num5;
				}
				else
				{
					num6 = this.defaultYOffset - this.tooltipRealHeight * 2f;
				}
				Vector3 position3 = new Vector3(vector.x, this.GUICamera.ViewportToScreenPoint(vector).y + num6, 0f);
				vector.y = this.GUICamera.ScreenToViewportPoint(position3).y;
			}
			num = this.GUICamera.ViewportToScreenPoint(vector).y - this.bgImage.sizeDelta.y * this.currentYScaleFactor * this.bgImage.pivot.y;
			if (num < this.lowerLeft.y)
			{
				float num7 = this.lowerLeft.y - num;
				float num6 = this.bgImage.sizeDelta.y * this.currentYScaleFactor * this.bgImage.pivot.y;
				if ((double)num7 < (double)this.defaultYOffset * 0.75 - (double)this.tooltipRealHeight)
				{
					num6 = num7;
				}
				else
				{
					num6 = this.tooltipRealHeight * 2f;
				}
				Vector3 position4 = new Vector3(vector.x, this.GUICamera.ViewportToScreenPoint(vector).y + num6, 0f);
				vector.y = this.GUICamera.ScreenToViewportPoint(position4).y;
			}
			base.transform.parent.transform.position = new Vector3(this.GUICamera.ViewportToWorldPoint(vector).x, this.GUICamera.ViewportToWorldPoint(vector).y, 0f);
			base.transform.parent.gameObject.SetActive(true);
			this.inside = true;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000466E6 File Offset: 0x000448E6
		public void HideTooltip()
		{
			if (this.GUIMode == RenderMode.ScreenSpaceCamera && this != null)
			{
				base.transform.parent.gameObject.SetActive(false);
				this.inside = false;
				this.HideTooltipVisibility();
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004671D File Offset: 0x0004491D
		private void Update()
		{
			this.LayoutInit();
			if (this.inside && this.GUIMode == RenderMode.ScreenSpaceCamera)
			{
				this.OnScreenSpaceCamera();
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004673C File Offset: 0x0004493C
		private void LayoutInit()
		{
			if (this.firstUpdate)
			{
				this.firstUpdate = false;
				this.bgImage.sizeDelta = new Vector2(this.hlG.preferredWidth + (float)this.horizontalPadding, this.hlG.preferredHeight + (float)this.verticalPadding);
				this.defaultYOffset = this.bgImage.sizeDelta.y * this.currentYScaleFactor * this.bgImage.pivot.y;
				this.defaultXOffset = this.bgImage.sizeDelta.x * this.currentXScaleFactor * this.bgImage.pivot.x;
				this.tooltipRealHeight = this.bgImage.sizeDelta.y * this.currentYScaleFactor;
				this.tooltipRealWidth = this.bgImage.sizeDelta.x * this.currentXScaleFactor;
				this.ActivateTooltipVisibility();
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00046830 File Offset: 0x00044A30
		private void NewTooltip()
		{
			this.firstUpdate = true;
			this.lowerLeft = this.GUICamera.ViewportToScreenPoint(new Vector3(0f, 0f, 0f));
			this.upperRight = this.GUICamera.ViewportToScreenPoint(new Vector3(1f, 1f, 0f));
			this.currentYScaleFactor = (float)Screen.height / base.transform.root.GetComponent<CanvasScaler>().referenceResolution.y;
			this.currentXScaleFactor = (float)Screen.width / base.transform.root.GetComponent<CanvasScaler>().referenceResolution.x;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000468DC File Offset: 0x00044ADC
		public void ActivateTooltipVisibility()
		{
			Color color = this.thisText.color;
			this.thisText.color = new Color(color.r, color.g, color.b, 1f);
			this.bgImageSource.color = new Color(this.bgImageSource.color.r, this.bgImageSource.color.g, this.bgImageSource.color.b, 0.8f);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00046964 File Offset: 0x00044B64
		public void HideTooltipVisibility()
		{
			Color color = this.thisText.color;
			this.thisText.color = new Color(color.r, color.g, color.b, 0f);
			this.bgImageSource.color = new Color(this.bgImageSource.color.r, this.bgImageSource.color.g, this.bgImageSource.color.b, 0f);
		}

		// Token: 0x04000D1C RID: 3356
		public int horizontalPadding;

		// Token: 0x04000D1D RID: 3357
		public int verticalPadding;

		// Token: 0x04000D1E RID: 3358
		public Text thisText;

		// Token: 0x04000D1F RID: 3359
		public HorizontalLayoutGroup hlG;

		// Token: 0x04000D20 RID: 3360
		public RectTransform bgImage;

		// Token: 0x04000D21 RID: 3361
		private Image bgImageSource;

		// Token: 0x04000D22 RID: 3362
		private bool firstUpdate;

		// Token: 0x04000D23 RID: 3363
		private bool inside;

		// Token: 0x04000D24 RID: 3364
		private RenderMode GUIMode;

		// Token: 0x04000D25 RID: 3365
		private Camera GUICamera;

		// Token: 0x04000D26 RID: 3366
		private Vector3 lowerLeft;

		// Token: 0x04000D27 RID: 3367
		private Vector3 upperRight;

		// Token: 0x04000D28 RID: 3368
		private float currentYScaleFactor;

		// Token: 0x04000D29 RID: 3369
		private float currentXScaleFactor;

		// Token: 0x04000D2A RID: 3370
		private float defaultYOffset;

		// Token: 0x04000D2B RID: 3371
		private float defaultXOffset;

		// Token: 0x04000D2C RID: 3372
		private float tooltipRealHeight;

		// Token: 0x04000D2D RID: 3373
		private float tooltipRealWidth;
	}
}
