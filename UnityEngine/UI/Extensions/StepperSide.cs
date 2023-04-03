using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002CD RID: 717
	[RequireComponent(typeof(Selectable))]
	public class StepperSide : UIBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00030E70 File Offset: 0x0002F070
		private Selectable button
		{
			get
			{
				return base.GetComponent<Selectable>();
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00030E78 File Offset: 0x0002F078
		private Stepper stepper
		{
			get
			{
				return base.GetComponentInParent<Stepper>();
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00030E80 File Offset: 0x0002F080
		private bool leftmost
		{
			get
			{
				return this.button == this.stepper.sides[0];
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00030E9A File Offset: 0x0002F09A
		protected StepperSide()
		{
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00030EA2 File Offset: 0x0002F0A2
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
			this.AdjustSprite(false);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00030EBA File Offset: 0x0002F0BA
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			this.AdjustSprite(true);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00030EC9 File Offset: 0x0002F0C9
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.AdjustSprite(false);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00030ED2 File Offset: 0x0002F0D2
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.AdjustSprite(true);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00030EDB File Offset: 0x0002F0DB
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.AdjustSprite(false);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00030EE4 File Offset: 0x0002F0E4
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.AdjustSprite(false);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00030EED File Offset: 0x0002F0ED
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.AdjustSprite(false);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00030EF6 File Offset: 0x0002F0F6
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.AdjustSprite(true);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00030EFF File Offset: 0x0002F0FF
		private void Press()
		{
			if (!this.button.IsActive() || !this.button.IsInteractable())
			{
				return;
			}
			if (this.leftmost)
			{
				this.stepper.StepDown();
				return;
			}
			this.stepper.StepUp();
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00030F3C File Offset: 0x0002F13C
		private void AdjustSprite(bool restore)
		{
			Image image = this.button.image;
			if (!image || image.overrideSprite == this.cutSprite)
			{
				return;
			}
			if (restore)
			{
				image.overrideSprite = this.cutSprite;
				return;
			}
			image.overrideSprite = Stepper.CutSprite(image.overrideSprite, this.leftmost);
		}

		// Token: 0x04000AC9 RID: 2761
		internal Sprite cutSprite;
	}
}
