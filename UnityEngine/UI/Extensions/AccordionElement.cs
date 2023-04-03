using System;
using UnityEngine.Events;
using UnityEngine.UI.Extensions.Tweens;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002B2 RID: 690
	[RequireComponent(typeof(RectTransform), typeof(LayoutElement))]
	[AddComponentMenu("UI/Extensions/Accordion/Accordion Element")]
	public class AccordionElement : Toggle
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00029E68 File Offset: 0x00028068
		public float MinHeight
		{
			get
			{
				return this.m_MinHeight;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00029E70 File Offset: 0x00028070
		public float MinWidth
		{
			get
			{
				return this.m_MinWidth;
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00029E78 File Offset: 0x00028078
		protected AccordionElement()
		{
			if (this.m_FloatTweenRunner == null)
			{
				this.m_FloatTweenRunner = new TweenRunner<FloatTween>();
			}
			this.m_FloatTweenRunner.Init(this);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00029EB8 File Offset: 0x000280B8
		protected override void Awake()
		{
			base.Awake();
			base.transition = Selectable.Transition.None;
			this.toggleTransition = Toggle.ToggleTransition.None;
			this.m_Accordion = base.gameObject.GetComponentInParent<Accordion>();
			this.m_RectTransform = (base.transform as RectTransform);
			this.m_LayoutElement = base.gameObject.GetComponent<LayoutElement>();
			this.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00029F24 File Offset: 0x00028124
		public void OnValueChanged(bool state)
		{
			if (this.m_LayoutElement == null)
			{
				return;
			}
			Accordion.Transition transition = (this.m_Accordion != null) ? this.m_Accordion.transition : Accordion.Transition.Instant;
			if (transition != Accordion.Transition.Instant || !(this.m_Accordion != null))
			{
				if (transition == Accordion.Transition.Tween)
				{
					if (state)
					{
						if (this.m_Accordion.ExpandVerticval)
						{
							this.StartTween(this.m_MinHeight, this.GetExpandedHeight());
							return;
						}
						this.StartTween(this.m_MinWidth, this.GetExpandedWidth());
						return;
					}
					else
					{
						if (this.m_Accordion.ExpandVerticval)
						{
							this.StartTween(this.m_RectTransform.rect.height, this.m_MinHeight);
							return;
						}
						this.StartTween(this.m_RectTransform.rect.width, this.m_MinWidth);
					}
				}
				return;
			}
			if (state)
			{
				if (this.m_Accordion.ExpandVerticval)
				{
					this.m_LayoutElement.preferredHeight = -1f;
					return;
				}
				this.m_LayoutElement.preferredWidth = -1f;
				return;
			}
			else
			{
				if (this.m_Accordion.ExpandVerticval)
				{
					this.m_LayoutElement.preferredHeight = this.m_MinHeight;
					return;
				}
				this.m_LayoutElement.preferredWidth = this.m_MinWidth;
				return;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0002A05C File Offset: 0x0002825C
		protected float GetExpandedHeight()
		{
			if (this.m_LayoutElement == null)
			{
				return this.m_MinHeight;
			}
			float preferredHeight = this.m_LayoutElement.preferredHeight;
			this.m_LayoutElement.preferredHeight = -1f;
			float preferredHeight2 = LayoutUtility.GetPreferredHeight(this.m_RectTransform);
			this.m_LayoutElement.preferredHeight = preferredHeight;
			return preferredHeight2;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0002A0B4 File Offset: 0x000282B4
		protected float GetExpandedWidth()
		{
			if (this.m_LayoutElement == null)
			{
				return this.m_MinWidth;
			}
			float preferredWidth = this.m_LayoutElement.preferredWidth;
			this.m_LayoutElement.preferredWidth = -1f;
			float preferredWidth2 = LayoutUtility.GetPreferredWidth(this.m_RectTransform);
			this.m_LayoutElement.preferredWidth = preferredWidth;
			return preferredWidth2;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0002A10C File Offset: 0x0002830C
		protected void StartTween(float startFloat, float targetFloat)
		{
			float duration = (this.m_Accordion != null) ? this.m_Accordion.transitionDuration : 0.3f;
			FloatTween info = new FloatTween
			{
				duration = duration,
				startFloat = startFloat,
				targetFloat = targetFloat
			};
			if (this.m_Accordion.ExpandVerticval)
			{
				info.AddOnChangedCallback(new UnityAction<float>(this.SetHeight));
			}
			else
			{
				info.AddOnChangedCallback(new UnityAction<float>(this.SetWidth));
			}
			info.ignoreTimeScale = true;
			this.m_FloatTweenRunner.StartTween(info);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0002A1A5 File Offset: 0x000283A5
		protected void SetHeight(float height)
		{
			if (this.m_LayoutElement == null)
			{
				return;
			}
			this.m_LayoutElement.preferredHeight = height;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0002A1C2 File Offset: 0x000283C2
		protected void SetWidth(float width)
		{
			if (this.m_LayoutElement == null)
			{
				return;
			}
			this.m_LayoutElement.preferredWidth = width;
		}

		// Token: 0x040009B5 RID: 2485
		[SerializeField]
		private float m_MinHeight = 18f;

		// Token: 0x040009B6 RID: 2486
		[SerializeField]
		private float m_MinWidth = 40f;

		// Token: 0x040009B7 RID: 2487
		private Accordion m_Accordion;

		// Token: 0x040009B8 RID: 2488
		private RectTransform m_RectTransform;

		// Token: 0x040009B9 RID: 2489
		private LayoutElement m_LayoutElement;

		// Token: 0x040009BA RID: 2490
		[NonSerialized]
		private readonly TweenRunner<FloatTween> m_FloatTweenRunner;
	}
}
