using System;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions.EasingCore;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200030B RID: 779
	public class Scroller : UIBehaviour, IPointerUpHandler, IEventSystemHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0003C248 File Offset: 0x0003A448
		public float ViewportSize
		{
			get
			{
				if (this.scrollDirection != ScrollDirection.Horizontal)
				{
					return this.viewport.rect.size.y;
				}
				return this.viewport.rect.size.x;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0003C28F File Offset: 0x0003A48F
		public ScrollDirection ScrollDirection
		{
			get
			{
				return this.scrollDirection;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0003C297 File Offset: 0x0003A497
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x0003C29F File Offset: 0x0003A49F
		public MovementType MovementType
		{
			get
			{
				return this.movementType;
			}
			set
			{
				this.movementType = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		public float Elasticity
		{
			get
			{
				return this.elasticity;
			}
			set
			{
				this.elasticity = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0003C2B9 File Offset: 0x0003A4B9
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0003C2C1 File Offset: 0x0003A4C1
		public float ScrollSensitivity
		{
			get
			{
				return this.scrollSensitivity;
			}
			set
			{
				this.scrollSensitivity = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0003C2CA File Offset: 0x0003A4CA
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x0003C2D2 File Offset: 0x0003A4D2
		public bool Inertia
		{
			get
			{
				return this.inertia;
			}
			set
			{
				this.inertia = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0003C2DB File Offset: 0x0003A4DB
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x0003C2E3 File Offset: 0x0003A4E3
		public float DecelerationRate
		{
			get
			{
				return this.decelerationRate;
			}
			set
			{
				this.decelerationRate = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0003C2EC File Offset: 0x0003A4EC
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x0003C2F9 File Offset: 0x0003A4F9
		public bool SnapEnabled
		{
			get
			{
				return this.snap.Enable;
			}
			set
			{
				this.snap.Enable = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0003C307 File Offset: 0x0003A507
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x0003C30F File Offset: 0x0003A50F
		public bool Draggable
		{
			get
			{
				return this.draggable;
			}
			set
			{
				this.draggable = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0003C318 File Offset: 0x0003A518
		public Scrollbar Scrollbar
		{
			get
			{
				return this.scrollbar;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0003C320 File Offset: 0x0003A520
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0003C328 File Offset: 0x0003A528
		public float Position
		{
			get
			{
				return this.currentPosition;
			}
			set
			{
				this.autoScrollState.Reset();
				this.velocity = 0f;
				this.dragging = false;
				this.UpdatePosition(value, true);
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0003C34F File Offset: 0x0003A54F
		protected override void Start()
		{
			base.Start();
			if (this.scrollbar)
			{
				this.scrollbar.onValueChanged.AddListener(delegate(float x)
				{
					this.UpdatePosition(x * ((float)this.totalCount - 1f), false);
				});
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003C380 File Offset: 0x0003A580
		public void OnValueChanged(Action<float> callback)
		{
			this.onValueChanged = callback;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003C389 File Offset: 0x0003A589
		public void OnSelectionChanged(Action<int> callback)
		{
			this.onSelectionChanged = callback;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0003C392 File Offset: 0x0003A592
		public void SetTotalCount(int totalCount)
		{
			this.totalCount = totalCount;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003C39B File Offset: 0x0003A59B
		public void ScrollTo(float position, float duration, Action onComplete = null)
		{
			this.ScrollTo(position, duration, Ease.OutCubic, onComplete);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0003C3A8 File Offset: 0x0003A5A8
		public void ScrollTo(float position, float duration, Ease easing, Action onComplete = null)
		{
			this.ScrollTo(position, duration, Easing.Get(easing), onComplete);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003C3BC File Offset: 0x0003A5BC
		public void ScrollTo(float position, float duration, EasingFunction easingFunction, Action onComplete = null)
		{
			if (duration <= 0f)
			{
				this.Position = this.CircularPosition(position, this.totalCount);
				if (onComplete != null)
				{
					onComplete();
				}
				return;
			}
			this.autoScrollState.Reset();
			this.autoScrollState.Enable = true;
			this.autoScrollState.Duration = duration;
			this.autoScrollState.EasingFunction = (easingFunction ?? Scroller.DefaultEasingFunction);
			this.autoScrollState.StartTime = Time.unscaledTime;
			this.autoScrollState.EndPosition = this.currentPosition + this.CalculateMovementAmount(this.currentPosition, position);
			this.autoScrollState.OnComplete = onComplete;
			this.velocity = 0f;
			this.scrollStartPosition = this.currentPosition;
			this.UpdateSelection(Mathf.RoundToInt(this.CircularPosition(this.autoScrollState.EndPosition, this.totalCount)));
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003C49D File Offset: 0x0003A69D
		public void JumpTo(int index)
		{
			if (index < 0 || index > this.totalCount - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.UpdateSelection(index);
			this.Position = (float)index;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003C4C8 File Offset: 0x0003A6C8
		public MovementDirection GetMovementDirection(int sourceIndex, int destIndex)
		{
			float num = this.CalculateMovementAmount((float)sourceIndex, (float)destIndex);
			if (this.scrollDirection != ScrollDirection.Horizontal)
			{
				if (num <= 0f)
				{
					return MovementDirection.Down;
				}
				return MovementDirection.Up;
			}
			else
			{
				if (num <= 0f)
				{
					return MovementDirection.Right;
				}
				return MovementDirection.Left;
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0003C500 File Offset: 0x0003A700
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (!this.draggable || eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.hold = true;
			this.velocity = 0f;
			this.autoScrollState.Reset();
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0003C530 File Offset: 0x0003A730
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!this.draggable || eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.hold && this.snap.Enable)
			{
				this.UpdateSelection(Mathf.Clamp(Mathf.RoundToInt(this.currentPosition), 0, this.totalCount - 1));
				this.ScrollTo((float)Mathf.RoundToInt(this.currentPosition), this.snap.Duration, this.snap.Easing, null);
			}
			this.hold = false;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
		void IScrollHandler.OnScroll(PointerEventData eventData)
		{
			if (!this.draggable)
			{
				return;
			}
			Vector2 scrollDelta = eventData.scrollDelta;
			scrollDelta.y *= -1f;
			float num = (this.scrollDirection == ScrollDirection.Horizontal) ? ((Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x)) ? scrollDelta.y : scrollDelta.x) : ((Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y)) ? scrollDelta.x : scrollDelta.y);
			if (eventData.IsScrolling())
			{
				this.scrolling = true;
			}
			float num2 = this.currentPosition + num / this.ViewportSize * this.scrollSensitivity;
			if (this.movementType == MovementType.Clamped)
			{
				num2 += this.CalculateOffset(num2);
			}
			if (this.autoScrollState.Enable)
			{
				this.autoScrollState.Reset();
			}
			this.UpdatePosition(num2, true);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0003C694 File Offset: 0x0003A894
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (!this.draggable || eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.hold = false;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewport, eventData.position, eventData.pressEventCamera, out this.beginDragPointerPosition);
			this.scrollStartPosition = this.currentPosition;
			this.dragging = true;
			this.autoScrollState.Reset();
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003C6F8 File Offset: 0x0003A8F8
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!this.draggable || eventData.button != PointerEventData.InputButton.Left || !this.dragging)
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewport, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			Vector2 vector = a - this.beginDragPointerPosition;
			float num = ((this.scrollDirection == ScrollDirection.Horizontal) ? (-vector.x) : vector.y) / this.ViewportSize * this.scrollSensitivity + this.scrollStartPosition;
			float num2 = this.CalculateOffset(num);
			num += num2;
			if (this.movementType == MovementType.Elastic && num2 != 0f)
			{
				num -= this.RubberDelta(num2, this.scrollSensitivity);
			}
			this.UpdatePosition(num, true);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0003C7AA File Offset: 0x0003A9AA
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (!this.draggable || eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.dragging = false;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003C7C4 File Offset: 0x0003A9C4
		private float CalculateOffset(float position)
		{
			if (this.movementType == MovementType.Unrestricted)
			{
				return 0f;
			}
			if (position < 0f)
			{
				return -position;
			}
			if (position > (float)(this.totalCount - 1))
			{
				return (float)(this.totalCount - 1) - position;
			}
			return 0f;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003C7FC File Offset: 0x0003A9FC
		private void UpdatePosition(float position, bool updateScrollbar = true)
		{
			Action<float> action = this.onValueChanged;
			if (action != null)
			{
				this.currentPosition = position;
				action(position);
			}
			if (this.scrollbar && updateScrollbar)
			{
				this.scrollbar.value = Mathf.Clamp01(position / Mathf.Max((float)this.totalCount - 1f, 0.0001f));
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0003C85C File Offset: 0x0003AA5C
		private void UpdateSelection(int index)
		{
			Action<int> action = this.onSelectionChanged;
			if (action == null)
			{
				return;
			}
			action(index);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003C86F File Offset: 0x0003AA6F
		private float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0003C89C File Offset: 0x0003AA9C
		private void Update()
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			float num = this.CalculateOffset(this.currentPosition);
			if (this.autoScrollState.Enable)
			{
				float num2;
				if (this.autoScrollState.Elastic)
				{
					num2 = Mathf.SmoothDamp(this.currentPosition, this.currentPosition + num, ref this.velocity, this.elasticity, float.PositiveInfinity, unscaledDeltaTime);
					if (Mathf.Abs(this.velocity) < 0.01f)
					{
						num2 = (float)Mathf.Clamp(Mathf.RoundToInt(num2), 0, this.totalCount - 1);
						this.velocity = 0f;
						this.autoScrollState.Complete();
					}
				}
				else
				{
					float num3 = Mathf.Clamp01((Time.unscaledTime - this.autoScrollState.StartTime) / Mathf.Max(this.autoScrollState.Duration, float.Epsilon));
					num2 = Mathf.LerpUnclamped(this.scrollStartPosition, this.autoScrollState.EndPosition, this.autoScrollState.EasingFunction(num3));
					if (Mathf.Approximately(num3, 1f))
					{
						this.autoScrollState.Complete();
					}
				}
				this.UpdatePosition(num2, true);
			}
			else if (!this.dragging && !this.scrolling && (!Mathf.Approximately(num, 0f) || !Mathf.Approximately(this.velocity, 0f)))
			{
				float num4 = this.currentPosition;
				if (this.movementType == MovementType.Elastic && !Mathf.Approximately(num, 0f))
				{
					this.autoScrollState.Reset();
					this.autoScrollState.Enable = true;
					this.autoScrollState.Elastic = true;
					this.UpdateSelection(Mathf.Clamp(Mathf.RoundToInt(num4), 0, this.totalCount - 1));
				}
				else if (this.inertia)
				{
					this.velocity *= Mathf.Pow(this.decelerationRate, unscaledDeltaTime);
					if (Mathf.Abs(this.velocity) < 0.001f)
					{
						this.velocity = 0f;
					}
					num4 += this.velocity * unscaledDeltaTime;
					if (this.snap.Enable && Mathf.Abs(this.velocity) < this.snap.VelocityThreshold)
					{
						this.ScrollTo((float)Mathf.RoundToInt(this.currentPosition), this.snap.Duration, this.snap.Easing, null);
					}
				}
				else
				{
					this.velocity = 0f;
				}
				if (!Mathf.Approximately(this.velocity, 0f))
				{
					if (this.movementType == MovementType.Clamped)
					{
						num = this.CalculateOffset(num4);
						num4 += num;
						if (Mathf.Approximately(num4, 0f) || Mathf.Approximately(num4, (float)this.totalCount - 1f))
						{
							this.velocity = 0f;
							this.UpdateSelection(Mathf.RoundToInt(num4));
						}
					}
					this.UpdatePosition(num4, true);
				}
			}
			if (!this.autoScrollState.Enable && (this.dragging || this.scrolling) && this.inertia)
			{
				float b = (this.currentPosition - this.prevPosition) / unscaledDeltaTime;
				this.velocity = Mathf.Lerp(this.velocity, b, unscaledDeltaTime * 10f);
			}
			this.prevPosition = this.currentPosition;
			this.scrolling = false;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0003CBD8 File Offset: 0x0003ADD8
		private float CalculateMovementAmount(float sourcePosition, float destPosition)
		{
			if (this.movementType != MovementType.Unrestricted)
			{
				return Mathf.Clamp(destPosition, 0f, (float)(this.totalCount - 1)) - sourcePosition;
			}
			float num = this.CircularPosition(destPosition, this.totalCount) - this.CircularPosition(sourcePosition, this.totalCount);
			if (Mathf.Abs(num) > (float)this.totalCount * 0.5f)
			{
				num = Mathf.Sign(-num) * ((float)this.totalCount - Mathf.Abs(num));
			}
			return num;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0003CC4D File Offset: 0x0003AE4D
		private float CircularPosition(float p, int size)
		{
			if (size < 1)
			{
				return 0f;
			}
			if (p >= 0f)
			{
				return p % (float)size;
			}
			return (float)(size - 1) + (p + 1f) % (float)size;
		}

		// Token: 0x04000BF2 RID: 3058
		[SerializeField]
		private RectTransform viewport;

		// Token: 0x04000BF3 RID: 3059
		[SerializeField]
		private ScrollDirection scrollDirection;

		// Token: 0x04000BF4 RID: 3060
		[SerializeField]
		private MovementType movementType = MovementType.Elastic;

		// Token: 0x04000BF5 RID: 3061
		[SerializeField]
		private float elasticity = 0.1f;

		// Token: 0x04000BF6 RID: 3062
		[SerializeField]
		private float scrollSensitivity = 1f;

		// Token: 0x04000BF7 RID: 3063
		[SerializeField]
		private bool inertia = true;

		// Token: 0x04000BF8 RID: 3064
		[SerializeField]
		private float decelerationRate = 0.03f;

		// Token: 0x04000BF9 RID: 3065
		[SerializeField]
		private Scroller.Snap snap = new Scroller.Snap
		{
			Enable = true,
			VelocityThreshold = 0.5f,
			Duration = 0.3f,
			Easing = Ease.InOutCubic
		};

		// Token: 0x04000BFA RID: 3066
		[SerializeField]
		private bool draggable = true;

		// Token: 0x04000BFB RID: 3067
		[SerializeField]
		private Scrollbar scrollbar;

		// Token: 0x04000BFC RID: 3068
		private readonly Scroller.AutoScrollState autoScrollState = new Scroller.AutoScrollState();

		// Token: 0x04000BFD RID: 3069
		private Action<float> onValueChanged;

		// Token: 0x04000BFE RID: 3070
		private Action<int> onSelectionChanged;

		// Token: 0x04000BFF RID: 3071
		private Vector2 beginDragPointerPosition;

		// Token: 0x04000C00 RID: 3072
		private float scrollStartPosition;

		// Token: 0x04000C01 RID: 3073
		private float prevPosition;

		// Token: 0x04000C02 RID: 3074
		private float currentPosition;

		// Token: 0x04000C03 RID: 3075
		private int totalCount;

		// Token: 0x04000C04 RID: 3076
		private bool hold;

		// Token: 0x04000C05 RID: 3077
		private bool scrolling;

		// Token: 0x04000C06 RID: 3078
		private bool dragging;

		// Token: 0x04000C07 RID: 3079
		private float velocity;

		// Token: 0x04000C08 RID: 3080
		private static readonly EasingFunction DefaultEasingFunction = Easing.Get(Ease.OutCubic);

		// Token: 0x0200114F RID: 4431
		[Serializable]
		private class Snap
		{
			// Token: 0x040091F9 RID: 37369
			public bool Enable;

			// Token: 0x040091FA RID: 37370
			public float VelocityThreshold;

			// Token: 0x040091FB RID: 37371
			public float Duration;

			// Token: 0x040091FC RID: 37372
			public Ease Easing;
		}

		// Token: 0x02001150 RID: 4432
		private class AutoScrollState
		{
			// Token: 0x06009F88 RID: 40840 RVA: 0x0033CA90 File Offset: 0x0033AC90
			public void Reset()
			{
				this.Enable = false;
				this.Elastic = false;
				this.Duration = 0f;
				this.StartTime = 0f;
				this.EasingFunction = Scroller.DefaultEasingFunction;
				this.EndPosition = 0f;
				this.OnComplete = null;
			}

			// Token: 0x06009F89 RID: 40841 RVA: 0x0033CADE File Offset: 0x0033ACDE
			public void Complete()
			{
				Action onComplete = this.OnComplete;
				if (onComplete != null)
				{
					onComplete();
				}
				this.Reset();
			}

			// Token: 0x040091FD RID: 37373
			public bool Enable;

			// Token: 0x040091FE RID: 37374
			public bool Elastic;

			// Token: 0x040091FF RID: 37375
			public float Duration;

			// Token: 0x04009200 RID: 37376
			public EasingFunction EasingFunction;

			// Token: 0x04009201 RID: 37377
			public float StartTime;

			// Token: 0x04009202 RID: 37378
			public float EndPosition;

			// Token: 0x04009203 RID: 37379
			public Action OnComplete;
		}
	}
}
