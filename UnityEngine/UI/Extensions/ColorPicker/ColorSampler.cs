using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x02000357 RID: 855
	public class ColorSampler : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x0004C710 File Offset: 0x0004A910
		protected virtual void OnEnable()
		{
			this.screenCapture = ScreenCapture.CaptureScreenshotAsTexture();
			this.sampleRectTransform = this.sampler.GetComponent<RectTransform>();
			this.sampler.gameObject.SetActive(true);
			this.sampler.onClick.AddListener(new UnityAction(this.SelectColor));
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004C767 File Offset: 0x0004A967
		protected virtual void OnDisable()
		{
			Object.Destroy(this.screenCapture);
			this.sampler.gameObject.SetActive(false);
			this.sampler.onClick.RemoveListener(new UnityAction(this.SelectColor));
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004C7A4 File Offset: 0x0004A9A4
		protected virtual void Update()
		{
			if (this.screenCapture == null)
			{
				return;
			}
			this.sampleRectTransform.position = this.m_screenPos;
			this.color = this.screenCapture.GetPixel((int)this.m_screenPos.x, (int)this.m_screenPos.y);
			this.HandleSamplerColoring();
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0004C808 File Offset: 0x0004AA08
		protected virtual void HandleSamplerColoring()
		{
			this.sampler.image.color = this.color;
			if (this.samplerOutline)
			{
				Color effectColor = Color.Lerp(Color.white, Color.black, (float)((this.color.grayscale > 0.5f) ? 1 : 0));
				effectColor.a = this.samplerOutline.effectColor.a;
				this.samplerOutline.effectColor = effectColor;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0004C882 File Offset: 0x0004AA82
		protected virtual void SelectColor()
		{
			if (this.oncolorSelected != null)
			{
				this.oncolorSelected.Invoke(this.color);
			}
			base.enabled = false;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0004C8A4 File Offset: 0x0004AAA4
		public void OnPointerDown(PointerEventData eventData)
		{
			this.m_screenPos = eventData.position;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004C8B2 File Offset: 0x0004AAB2
		public void OnPointerUp(PointerEventData eventData)
		{
			this.m_screenPos = Vector2.zero;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0004C8BF File Offset: 0x0004AABF
		public void OnDrag(PointerEventData eventData)
		{
			this.m_screenPos = eventData.position;
		}

		// Token: 0x04000E2D RID: 3629
		private Vector2 m_screenPos;

		// Token: 0x04000E2E RID: 3630
		[SerializeField]
		protected Button sampler;

		// Token: 0x04000E2F RID: 3631
		private RectTransform sampleRectTransform;

		// Token: 0x04000E30 RID: 3632
		[SerializeField]
		protected Outline samplerOutline;

		// Token: 0x04000E31 RID: 3633
		protected Texture2D screenCapture;

		// Token: 0x04000E32 RID: 3634
		public ColorChangedEvent oncolorSelected = new ColorChangedEvent();

		// Token: 0x04000E33 RID: 3635
		protected Color color;
	}
}
