using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.Util;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.Component
{
	// Token: 0x02000C55 RID: 3157
	public class NKCUIComDraggableList : MonoBehaviour
	{
		// Token: 0x06009323 RID: 37667 RVA: 0x003235D5 File Offset: 0x003217D5
		private void Awake()
		{
			this.Init(this.m_bResetIndexOnAwake);
		}

		// Token: 0x06009324 RID: 37668 RVA: 0x003235E4 File Offset: 0x003217E4
		public void Init(bool bResetIndex)
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_myCanvas = NKCUIUtility.FindCanvas(base.transform);
			for (int i = 0; i < this.m_lstDraggableSlot.Count; i++)
			{
				NKCUIComDraggableListSlot nkcuicomDraggableListSlot = this.m_lstDraggableSlot[i];
				if (bResetIndex)
				{
					nkcuicomDraggableListSlot.Index = i;
				}
				nkcuicomDraggableListSlot.dOnBeginDrag = new NKCUIComDraggableListSlot.OnDragEvent(this.OnBeginDragSlot);
				nkcuicomDraggableListSlot.dOnDrag = new NKCUIComDraggableListSlot.OnDragEvent(this.OnDragSlot);
				nkcuicomDraggableListSlot.dOnEndDrag = new NKCUIComDraggableListSlot.OnDragEvent(this.OnEndDragSlot);
			}
			this.m_bInit = true;
		}

		// Token: 0x06009325 RID: 37669 RVA: 0x00323675 File Offset: 0x00321875
		private void OnDisable()
		{
			this.ResetPosition(false);
		}

		// Token: 0x06009326 RID: 37670 RVA: 0x00323680 File Offset: 0x00321880
		public void ResetPosition(bool bAnim = false)
		{
			foreach (NKCUIComDraggableListSlot nkcuicomDraggableListSlot in this.m_lstDraggableSlot)
			{
				nkcuicomDraggableListSlot.ReturnToPos(bAnim);
			}
		}

		// Token: 0x06009327 RID: 37671 RVA: 0x003236D4 File Offset: 0x003218D4
		private void Swap(int oldIndex, int newIndex)
		{
			Debug.Log(string.Format("Swap {0} {1}", oldIndex, newIndex));
			NKCUIComDraggableList.OnSlotSwapped onSlotSwapped = this.dOnSlotSwapped;
			if (onSlotSwapped == null)
			{
				return;
			}
			onSlotSwapped(oldIndex, newIndex);
		}

		// Token: 0x06009328 RID: 37672 RVA: 0x00323703 File Offset: 0x00321903
		private void OnBeginDragSlot(NKCUIComDraggableListSlot slot, PointerEventData eventData)
		{
			this.m_trCurrentDragging = slot.m_trDragMovePart;
			this.m_trCurrentDragging.SetParent(this.m_rtRootDragging, true);
			this.m_trCurrentDragging.localScale = Vector3.one;
		}

		// Token: 0x06009329 RID: 37673 RVA: 0x00323734 File Offset: 0x00321934
		private void OnDragSlot(NKCUIComDraggableListSlot slot, PointerEventData eventData)
		{
			if (this.m_trCurrentDragging != null)
			{
				Vector2 v;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_rtRootDragging, eventData.position, this.m_myCanvas.worldCamera, out v);
				this.m_trCurrentDragging.localPosition = v;
				if (this.m_bUseSwapAnimation)
				{
					NKCUIComDraggableListSlot nkcuicomDraggableListSlot = this.GetSlotFromPoint(eventData);
					if (nkcuicomDraggableListSlot == slot)
					{
						nkcuicomDraggableListSlot = null;
					}
					if (nkcuicomDraggableListSlot != this.m_currentSwapTarget)
					{
						if (this.m_currentSwapTarget != null)
						{
							this.m_currentSwapTarget.ReturnToPos(true);
							this.m_currentSwapTarget = null;
						}
						if (nkcuicomDraggableListSlot != null)
						{
							this.m_currentSwapTarget = nkcuicomDraggableListSlot;
							this.m_currentSwapTarget.m_trDragMovePart.SetParent(this.m_rtRootDragging, true);
							this.m_currentSwapTarget.m_trDragMovePart.localScale = Vector3.one;
							this.m_currentSwapTarget.m_trDragMovePart.DOKill(false);
							this.m_currentSwapTarget.m_trDragMovePart.DOMove(slot.transform.position, 0.4f, false).SetEase(Ease.OutCubic);
						}
					}
				}
			}
		}

		// Token: 0x0600932A RID: 37674 RVA: 0x0032384C File Offset: 0x00321A4C
		private void OnEndDragSlot(NKCUIComDraggableListSlot slot, PointerEventData eventData)
		{
			NKCUIComDraggableListSlot slotFromPoint = this.GetSlotFromPoint(eventData);
			if (slotFromPoint != null && slotFromPoint != slot)
			{
				if (this.m_currentSwapTarget != null)
				{
					this.m_currentSwapTarget.ReturnToPos(false);
				}
				slot.ReturnToPos(false);
				this.Swap(slot.Index, slotFromPoint.Index);
			}
			else
			{
				if (this.m_currentSwapTarget != null)
				{
					this.m_currentSwapTarget.ReturnToPos(true);
				}
				slot.ReturnToPos(true);
			}
			this.m_currentSwapTarget = null;
			this.m_trCurrentDragging = null;
		}

		// Token: 0x0600932B RID: 37675 RVA: 0x003238D8 File Offset: 0x00321AD8
		private NKCUIComDraggableListSlot GetSlotFromPoint(PointerEventData eventData)
		{
			if (eventData.pointerCurrentRaycast.gameObject != null)
			{
				NKCUIComDraggableListSlot component = base.gameObject.GetComponent<NKCUIComDraggableListSlot>();
				if (component != null)
				{
					return component;
				}
			}
			foreach (NKCUIComDraggableListSlot nkcuicomDraggableListSlot in this.m_lstDraggableSlot)
			{
				RectTransform component2 = nkcuicomDraggableListSlot.GetComponent<RectTransform>();
				if (!(component2 == null) && RectTransformUtility.RectangleContainsScreenPoint(component2, eventData.position, this.m_myCanvas.worldCamera))
				{
					return nkcuicomDraggableListSlot;
				}
			}
			return null;
		}

		// Token: 0x04008019 RID: 32793
		public List<NKCUIComDraggableListSlot> m_lstDraggableSlot;

		// Token: 0x0400801A RID: 32794
		public NKCUIComDraggableList.OnSlotSwapped dOnSlotSwapped;

		// Token: 0x0400801B RID: 32795
		public RectTransform m_rtRootDragging;

		// Token: 0x0400801C RID: 32796
		private Transform m_trCurrentDragging;

		// Token: 0x0400801D RID: 32797
		public bool m_bUseSwapAnimation;

		// Token: 0x0400801E RID: 32798
		public bool m_bResetIndexOnAwake = true;

		// Token: 0x0400801F RID: 32799
		private Canvas m_myCanvas;

		// Token: 0x04008020 RID: 32800
		private NKCUIComDraggableListSlot m_currentSwapTarget;

		// Token: 0x04008021 RID: 32801
		private bool m_bInit;

		// Token: 0x02001A15 RID: 6677
		// (Invoke) Token: 0x0600BB03 RID: 47875
		public delegate void OnSlotSwapped(int oldIndex, int newIndex);
	}
}
