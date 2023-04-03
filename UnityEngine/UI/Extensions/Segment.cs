using System;
using System.Collections;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C7 RID: 711
	[AddComponentMenu("UI/Extensions/Segmented Control/Segment")]
	[RequireComponent(typeof(Selectable))]
	public class Segment : UIBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0002F700 File Offset: 0x0002D900
		internal bool leftmost
		{
			get
			{
				return this.index == 0;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0002F70B File Offset: 0x0002D90B
		internal bool rightmost
		{
			get
			{
				return this.index == this.segmentedControl.segments.Length - 1;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002F724 File Offset: 0x0002D924
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x0002F73C File Offset: 0x0002D93C
		public bool selected
		{
			get
			{
				return this.segmentedControl.selectedSegment == this.button;
			}
			set
			{
				this.SetSelected(value);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0002F745 File Offset: 0x0002D945
		internal Selectable button
		{
			get
			{
				return base.GetComponent<Selectable>();
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0002F74D File Offset: 0x0002D94D
		protected Segment()
		{
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0002F755 File Offset: 0x0002D955
		protected override void Start()
		{
			base.StartCoroutine(this.DelayedInit());
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002F764 File Offset: 0x0002D964
		private IEnumerator DelayedInit()
		{
			yield return null;
			yield return null;
			this.button.image.overrideSprite = this.cutSprite;
			if (this.selected)
			{
				this.MaintainSelection();
			}
			yield break;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0002F773 File Offset: 0x0002D973
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.selected = true;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0002F785 File Offset: 0x0002D985
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.MaintainSelection();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0002F78D File Offset: 0x0002D98D
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.MaintainSelection();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0002F795 File Offset: 0x0002D995
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.MaintainSelection();
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0002F79D File Offset: 0x0002D99D
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.MaintainSelection();
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0002F7A5 File Offset: 0x0002D9A5
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.MaintainSelection();
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0002F7AD File Offset: 0x0002D9AD
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.MaintainSelection();
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002F7B5 File Offset: 0x0002D9B5
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.segmentedControl)
			{
				this.MaintainSelection();
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0002F7D0 File Offset: 0x0002D9D0
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.selected = true;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0002F7DC File Offset: 0x0002D9DC
		private void SetSelected(bool value)
		{
			if (!value || !this.button.IsActive() || !this.button.IsInteractable())
			{
				if (this.segmentedControl.selectedSegment == this.button)
				{
					this.Deselect();
				}
				return;
			}
			if (!(this.segmentedControl.selectedSegment == this.button))
			{
				if (this.segmentedControl.selectedSegment)
				{
					Segment component = this.segmentedControl.selectedSegment.GetComponent<Segment>();
					this.segmentedControl.selectedSegment = null;
					if (component)
					{
						component.TransitionButton();
					}
				}
				this.segmentedControl.selectedSegment = this.button;
				this.TransitionButton();
				this.segmentedControl.onValueChanged.Invoke(this.index);
				return;
			}
			if (this.segmentedControl.allowSwitchingOff)
			{
				this.Deselect();
				return;
			}
			this.MaintainSelection();
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002F8CB File Offset: 0x0002DACB
		private void Deselect()
		{
			this.segmentedControl.selectedSegment = null;
			this.TransitionButton();
			this.segmentedControl.onValueChanged.Invoke(-1);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002F8F0 File Offset: 0x0002DAF0
		private void MaintainSelection()
		{
			if (this.button != this.segmentedControl.selectedSegment)
			{
				return;
			}
			this.TransitionButton(true);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002F912 File Offset: 0x0002DB12
		internal void TransitionButton()
		{
			this.TransitionButton(false);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0002F91C File Offset: 0x0002DB1C
		internal void TransitionButton(bool instant)
		{
			Color a = this.selected ? this.button.colors.pressedColor : this.button.colors.normalColor;
			Color a2 = this.selected ? this.button.colors.normalColor : this.button.colors.pressedColor;
			Sprite sprite = this.selected ? this.button.spriteState.pressedSprite : this.cutSprite;
			string triggername = this.selected ? this.button.animationTriggers.pressedTrigger : this.button.animationTriggers.normalTrigger;
			switch (this.button.transition)
			{
			case Selectable.Transition.ColorTint:
				this.button.image.overrideSprite = this.cutSprite;
				this.StartColorTween(a * this.button.colors.colorMultiplier, instant);
				this.ChangeTextColor(a2 * this.button.colors.colorMultiplier);
				return;
			case Selectable.Transition.SpriteSwap:
				if (sprite != this.cutSprite)
				{
					sprite = SegmentedControl.CutSprite(sprite, this.leftmost, this.rightmost);
				}
				this.DoSpriteSwap(sprite);
				return;
			case Selectable.Transition.Animation:
				this.button.image.overrideSprite = this.cutSprite;
				this.TriggerAnimation(triggername);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0002FAA0 File Offset: 0x0002DCA0
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (this.button.targetGraphic == null)
			{
				return;
			}
			this.button.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : this.button.colors.fadeDuration, true, true);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		private void ChangeTextColor(Color targetColor)
		{
			Text componentInChildren = base.GetComponentInChildren<Text>();
			if (!componentInChildren)
			{
				return;
			}
			componentInChildren.color = targetColor;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002FB18 File Offset: 0x0002DD18
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (this.button.image == null)
			{
				return;
			}
			this.button.image.overrideSprite = newSprite;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0002FB40 File Offset: 0x0002DD40
		private void TriggerAnimation(string triggername)
		{
			if (this.button.animator == null || !this.button.animator.isActiveAndEnabled || !this.button.animator.hasBoundPlayables || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			this.button.animator.ResetTrigger(this.button.animationTriggers.normalTrigger);
			this.button.animator.ResetTrigger(this.button.animationTriggers.pressedTrigger);
			this.button.animator.ResetTrigger(this.button.animationTriggers.highlightedTrigger);
			this.button.animator.ResetTrigger(this.button.animationTriggers.disabledTrigger);
			this.button.animator.SetTrigger(triggername);
		}

		// Token: 0x04000AA7 RID: 2727
		internal int index;

		// Token: 0x04000AA8 RID: 2728
		internal SegmentedControl segmentedControl;

		// Token: 0x04000AA9 RID: 2729
		internal Sprite cutSprite;
	}
}
