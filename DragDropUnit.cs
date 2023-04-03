using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000027 RID: 39
public class DragDropUnit : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
{
	// Token: 0x06000137 RID: 311 RVA: 0x000063B2 File Offset: 0x000045B2
	private void Awake()
	{
		this.canvas = GameObject.Find("NKM_SCEN_UI_MID_Canvas").GetComponent<Canvas>();
		this.rectTransform = base.GetComponent<RectTransform>();
		this.canvasGroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06000138 RID: 312 RVA: 0x000063E4 File Offset: 0x000045E4
	private void Update()
	{
		Vector2 mouseScrollDelta = Input.mouseScrollDelta;
		this.scaleFactor = this.rectTransform.localScale.y + Input.mouseScrollDelta.y * this.WheelFactor;
		if (this.scaleFactor > this.MinScale && this.scaleFactor < this.MaxScale)
		{
			this.vec3.Set(this.scaleFactor, this.scaleFactor, 1f);
			this.rectTransform.localScale = this.vec3;
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00006468 File Offset: 0x00004668
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.canvasGroup.blocksRaycasts = false;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00006476 File Offset: 0x00004676
	public void OnDrag(PointerEventData eventData)
	{
		this.rectTransform.anchoredPosition += eventData.delta / this.canvas.scaleFactor;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x000064A4 File Offset: 0x000046A4
	public void OnEndDrag(PointerEventData eventData)
	{
		this.canvasGroup.blocksRaycasts = true;
	}

	// Token: 0x040000D7 RID: 215
	[Range(0f, 1f)]
	public float MinScale = 0.3f;

	// Token: 0x040000D8 RID: 216
	[Range(1f, 3f)]
	public float MaxScale = 2f;

	// Token: 0x040000D9 RID: 217
	[Range(0f, 0.5f)]
	public float WheelFactor = 0.05f;

	// Token: 0x040000DA RID: 218
	private Canvas canvas;

	// Token: 0x040000DB RID: 219
	private RectTransform rectTransform;

	// Token: 0x040000DC RID: 220
	private CanvasGroup canvasGroup;

	// Token: 0x040000DD RID: 221
	private float scaleFactor = 1f;

	// Token: 0x040000DE RID: 222
	private Vector3 vec3;
}
