using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000012 RID: 18
public class NKCUIComDragScrollInputField : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x1700002B RID: 43
	// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003641 File Offset: 0x00001841
	public LoopScrollRect ScrollRect
	{
		set
		{
			this.scrollRect = value;
		}
	}

	// Token: 0x1700002C RID: 44
	// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000364A File Offset: 0x0000184A
	public bool ActiveInput
	{
		set
		{
			this.activeInput = value;
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00003653 File Offset: 0x00001853
	private void Awake()
	{
		this.inputField.enabled = false;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00003661 File Offset: 0x00001861
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (this.scrollRect != null)
		{
			this.scrollRect.OnBeginDrag(eventData);
		}
		this.drag = true;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00003684 File Offset: 0x00001884
	public void OnDrag(PointerEventData eventData)
	{
		if (this.scrollRect != null)
		{
			this.scrollRect.OnDrag(eventData);
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000036A0 File Offset: 0x000018A0
	public void OnEndDrag(PointerEventData eventData)
	{
		if (this.scrollRect != null)
		{
			this.scrollRect.OnEndDrag(eventData);
		}
		this.drag = false;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000036C3 File Offset: 0x000018C3
	public void OnScroll(PointerEventData data)
	{
		if (this.scrollRect != null)
		{
			this.scrollRect.OnScroll(data);
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x000036DF File Offset: 0x000018DF
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000036E1 File Offset: 0x000018E1
	public void OnPointerUp(PointerEventData eventData)
	{
		if (!this.drag && !this.activeInput)
		{
			this.activeInput = true;
			this.inputField.enabled = true;
			this.inputField.Select();
			this.inputField.ActivateInputField();
		}
	}

	// Token: 0x04000042 RID: 66
	public InputField inputField;

	// Token: 0x04000043 RID: 67
	private LoopScrollRect scrollRect;

	// Token: 0x04000044 RID: 68
	private bool drag;

	// Token: 0x04000045 RID: 69
	private bool activeInput;
}
