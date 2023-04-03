using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.Component
{
	// Token: 0x02000C56 RID: 3158
	public class NKCUIComDraggableListSlot : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x0600932D RID: 37677 RVA: 0x00323997 File Offset: 0x00321B97
		// (set) Token: 0x0600932E RID: 37678 RVA: 0x0032399F File Offset: 0x00321B9F
		public int Index { get; set; }

		// Token: 0x0600932F RID: 37679 RVA: 0x003239A8 File Offset: 0x00321BA8
		public void ReturnToPos(bool bAnim)
		{
			this.m_trDragMovePart.DOKill(false);
			this.m_trDragMovePart.SetParent(base.transform, true);
			this.m_trDragMovePart.localScale = Vector3.one;
			if (bAnim)
			{
				this.m_trDragMovePart.DOLocalMove(Vector3.zero, 0.4f, false).SetEase(Ease.OutCubic);
				return;
			}
			this.m_trDragMovePart.localPosition = Vector3.zero;
		}

		// Token: 0x06009330 RID: 37680 RVA: 0x00323A16 File Offset: 0x00321C16
		public void OnBeginDrag(PointerEventData eventData)
		{
			NKCUIComDraggableListSlot.OnDragEvent onDragEvent = this.dOnBeginDrag;
			if (onDragEvent == null)
			{
				return;
			}
			onDragEvent(this, eventData);
		}

		// Token: 0x06009331 RID: 37681 RVA: 0x00323A2A File Offset: 0x00321C2A
		public void OnDrag(PointerEventData eventData)
		{
			NKCUIComDraggableListSlot.OnDragEvent onDragEvent = this.dOnDrag;
			if (onDragEvent == null)
			{
				return;
			}
			onDragEvent(this, eventData);
		}

		// Token: 0x06009332 RID: 37682 RVA: 0x00323A3E File Offset: 0x00321C3E
		public void OnEndDrag(PointerEventData eventData)
		{
			NKCUIComDraggableListSlot.OnDragEvent onDragEvent = this.dOnEndDrag;
			if (onDragEvent == null)
			{
				return;
			}
			onDragEvent(this, eventData);
		}

		// Token: 0x04008023 RID: 32803
		public Transform m_trDragMovePart;

		// Token: 0x04008024 RID: 32804
		public NKCUIComDraggableListSlot.OnDragEvent dOnBeginDrag;

		// Token: 0x04008025 RID: 32805
		public NKCUIComDraggableListSlot.OnDragEvent dOnDrag;

		// Token: 0x04008026 RID: 32806
		public NKCUIComDraggableListSlot.OnDragEvent dOnEndDrag;

		// Token: 0x02001A16 RID: 6678
		// (Invoke) Token: 0x0600BB07 RID: 47879
		public delegate void OnDragEvent(NKCUIComDraggableListSlot slot, PointerEventData eventData);
	}
}
