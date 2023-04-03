using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000310 RID: 784
	public class ScrollPositionController : UIBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x0003E07C File Offset: 0x0003C27C
		public void OnUpdatePosition(Action<float> onUpdatePosition)
		{
			this.onUpdatePosition = onUpdatePosition;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0003E085 File Offset: 0x0003C285
		public void OnItemSelected(Action<int> onItemSelected)
		{
			this.onItemSelected = onItemSelected;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0003E08E File Offset: 0x0003C28E
		public void SetDataCount(int dataCount)
		{
			this.dataCount = dataCount;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0003E098 File Offset: 0x0003C298
		public void ScrollTo(int index, float duration)
		{
			this.autoScrollState.Reset();
			this.autoScrollState.Enable = true;
			this.autoScrollState.Duration = duration;
			this.autoScrollState.StartTime = Time.unscaledTime;
			this.autoScrollState.EndScrollPosition = (float)this.CalculateDestinationIndex(index);
			this.velocity = 0f;
			this.dragStartScrollPosition = this.currentScrollPosition;
			this.ItemSelected(Mathf.RoundToInt(this.GetCircularPosition(this.autoScrollState.EndScrollPosition, this.dataCount)));
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003E124 File Offset: 0x0003C324
		public void JumpTo(int index)
		{
			this.autoScrollState.Reset();
			this.velocity = 0f;
			this.dragging = false;
			index = this.CalculateDestinationIndex(index);
			this.ItemSelected(index);
			this.UpdatePosition((float)index);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0003E15C File Offset: 0x0003C35C
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.pointerStartLocalPosition = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewport, eventData.position, eventData.pressEventCamera, out this.pointerStartLocalPosition);
			this.dragStartScrollPosition = this.currentScrollPosition;
			this.dragging = true;
			this.autoScrollState.Reset();
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0003E1BC File Offset: 0x0003C3BC
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.dragging)
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewport, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			Vector2 vector = a - this.pointerStartLocalPosition;
			float num = ((this.directionOfRecognize == ScrollPositionController.ScrollDirection.Horizontal) ? (-vector.x) : vector.y) / this.GetViewportSize() * this.scrollSensitivity + this.dragStartScrollPosition;
			float num2 = this.CalculateOffset(num);
			num += num2;
			if (this.movementType == ScrollPositionController.MovementType.Elastic && num2 != 0f)
			{
				num -= this.RubberDelta(num2, this.scrollSensitivity);
			}
			this.UpdatePosition(num);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0003E266 File Offset: 0x0003C466
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.dragging = false;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0003E278 File Offset: 0x0003C478
		private float GetViewportSize()
		{
			if (this.directionOfRecognize != ScrollPositionController.ScrollDirection.Horizontal)
			{
				return this.viewport.rect.size.y;
			}
			return this.viewport.rect.size.x;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003E2BF File Offset: 0x0003C4BF
		private float CalculateOffset(float position)
		{
			if (this.movementType == ScrollPositionController.MovementType.Unrestricted)
			{
				return 0f;
			}
			if (position < 0f)
			{
				return -position;
			}
			if (position > (float)(this.dataCount - 1))
			{
				return (float)(this.dataCount - 1) - position;
			}
			return 0f;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003E2F7 File Offset: 0x0003C4F7
		private void UpdatePosition(float position)
		{
			this.currentScrollPosition = position;
			if (this.onUpdatePosition != null)
			{
				this.onUpdatePosition(this.currentScrollPosition);
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0003E319 File Offset: 0x0003C519
		private void ItemSelected(int index)
		{
			if (this.onItemSelected != null)
			{
				this.onItemSelected(index);
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003E32F File Offset: 0x0003C52F
		private float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0003E35C File Offset: 0x0003C55C
		private void Update()
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			float num = this.CalculateOffset(this.currentScrollPosition);
			if (this.autoScrollState.Enable)
			{
				float num3;
				if (this.autoScrollState.Elastic)
				{
					float num2 = this.velocity;
					num3 = Mathf.SmoothDamp(this.currentScrollPosition, this.currentScrollPosition + num, ref num2, this.elasticity, float.PositiveInfinity, unscaledDeltaTime);
					this.velocity = num2;
					if (Mathf.Abs(this.velocity) < 0.01f)
					{
						num3 = (float)Mathf.Clamp(Mathf.RoundToInt(num3), 0, this.dataCount - 1);
						this.velocity = 0f;
						this.autoScrollState.Reset();
					}
				}
				else
				{
					float num4 = Mathf.Clamp01((Time.unscaledTime - this.autoScrollState.StartTime) / Mathf.Max(this.autoScrollState.Duration, float.Epsilon));
					num3 = Mathf.Lerp(this.dragStartScrollPosition, this.autoScrollState.EndScrollPosition, this.EaseInOutCubic(0f, 1f, num4));
					if (Mathf.Approximately(num4, 1f))
					{
						this.autoScrollState.Reset();
					}
				}
				this.UpdatePosition(num3);
			}
			else if (!this.dragging && (!Mathf.Approximately(num, 0f) || !Mathf.Approximately(this.velocity, 0f)))
			{
				float num5 = this.currentScrollPosition;
				if (this.movementType == ScrollPositionController.MovementType.Elastic && !Mathf.Approximately(num, 0f))
				{
					this.autoScrollState.Reset();
					this.autoScrollState.Enable = true;
					this.autoScrollState.Elastic = true;
					this.ItemSelected(Mathf.Clamp(Mathf.RoundToInt(num5), 0, this.dataCount - 1));
				}
				else if (this.inertia)
				{
					this.velocity *= Mathf.Pow(this.decelerationRate, unscaledDeltaTime);
					if (Mathf.Abs(this.velocity) < 0.001f)
					{
						this.velocity = 0f;
					}
					num5 += this.velocity * unscaledDeltaTime;
					if (this.snap.Enable && Mathf.Abs(this.velocity) < this.snap.VelocityThreshold)
					{
						this.ScrollTo(Mathf.RoundToInt(this.currentScrollPosition), this.snap.Duration);
					}
				}
				else
				{
					this.velocity = 0f;
				}
				if (!Mathf.Approximately(this.velocity, 0f))
				{
					if (this.movementType == ScrollPositionController.MovementType.Clamped)
					{
						num = this.CalculateOffset(num5);
						num5 += num;
						if (Mathf.Approximately(num5, 0f) || Mathf.Approximately(num5, (float)this.dataCount - 1f))
						{
							this.velocity = 0f;
							this.ItemSelected(Mathf.RoundToInt(num5));
						}
					}
					this.UpdatePosition(num5);
				}
			}
			if (!this.autoScrollState.Enable && this.dragging && this.inertia)
			{
				float b = (this.currentScrollPosition - this.prevScrollPosition) / unscaledDeltaTime;
				this.velocity = Mathf.Lerp(this.velocity, b, unscaledDeltaTime * 10f);
			}
			if (this.currentScrollPosition != this.prevScrollPosition)
			{
				this.prevScrollPosition = this.currentScrollPosition;
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0003E688 File Offset: 0x0003C888
		private int CalculateDestinationIndex(int index)
		{
			if (this.movementType != ScrollPositionController.MovementType.Unrestricted)
			{
				return Mathf.Clamp(index, 0, this.dataCount - 1);
			}
			return this.CalculateClosestIndex(index);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0003E6AC File Offset: 0x0003C8AC
		private int CalculateClosestIndex(int index)
		{
			float num = this.GetCircularPosition((float)index, this.dataCount) - this.GetCircularPosition(this.currentScrollPosition, this.dataCount);
			if (Mathf.Abs(num) > (float)this.dataCount * 0.5f)
			{
				num = Mathf.Sign(-num) * ((float)this.dataCount - Mathf.Abs(num));
			}
			return Mathf.RoundToInt(num + this.currentScrollPosition);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0003E714 File Offset: 0x0003C914
		private float GetCircularPosition(float position, int length)
		{
			if (position >= 0f)
			{
				return position % (float)length;
			}
			return (float)(length - 1) + (position + 1f) % (float)length;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003E734 File Offset: 0x0003C934
		private float EaseInOutCubic(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value + 2f) + start;
		}

		// Token: 0x04000C19 RID: 3097
		[SerializeField]
		private RectTransform viewport;

		// Token: 0x04000C1A RID: 3098
		[SerializeField]
		private ScrollPositionController.ScrollDirection directionOfRecognize;

		// Token: 0x04000C1B RID: 3099
		[SerializeField]
		private ScrollPositionController.MovementType movementType = ScrollPositionController.MovementType.Elastic;

		// Token: 0x04000C1C RID: 3100
		[SerializeField]
		private float elasticity = 0.1f;

		// Token: 0x04000C1D RID: 3101
		[SerializeField]
		private float scrollSensitivity = 1f;

		// Token: 0x04000C1E RID: 3102
		[SerializeField]
		private bool inertia = true;

		// Token: 0x04000C1F RID: 3103
		[SerializeField]
		[Tooltip("Only used when inertia is enabled")]
		private float decelerationRate = 0.03f;

		// Token: 0x04000C20 RID: 3104
		[SerializeField]
		[Tooltip("Only used when inertia is enabled")]
		private ScrollPositionController.Snap snap = new ScrollPositionController.Snap
		{
			Enable = true,
			VelocityThreshold = 0.5f,
			Duration = 0.3f
		};

		// Token: 0x04000C21 RID: 3105
		[SerializeField]
		private int dataCount;

		// Token: 0x04000C22 RID: 3106
		private readonly ScrollPositionController.AutoScrollState autoScrollState = new ScrollPositionController.AutoScrollState();

		// Token: 0x04000C23 RID: 3107
		private Action<float> onUpdatePosition;

		// Token: 0x04000C24 RID: 3108
		private Action<int> onItemSelected;

		// Token: 0x04000C25 RID: 3109
		private Vector2 pointerStartLocalPosition;

		// Token: 0x04000C26 RID: 3110
		private float dragStartScrollPosition;

		// Token: 0x04000C27 RID: 3111
		private float prevScrollPosition;

		// Token: 0x04000C28 RID: 3112
		private float currentScrollPosition;

		// Token: 0x04000C29 RID: 3113
		private bool dragging;

		// Token: 0x04000C2A RID: 3114
		private float velocity;

		// Token: 0x02001152 RID: 4434
		private enum ScrollDirection
		{
			// Token: 0x04009208 RID: 37384
			Vertical,
			// Token: 0x04009209 RID: 37385
			Horizontal
		}

		// Token: 0x02001153 RID: 4435
		private enum MovementType
		{
			// Token: 0x0400920B RID: 37387
			Unrestricted,
			// Token: 0x0400920C RID: 37388
			Elastic,
			// Token: 0x0400920D RID: 37389
			Clamped
		}

		// Token: 0x02001154 RID: 4436
		[Serializable]
		private struct Snap
		{
			// Token: 0x0400920E RID: 37390
			public bool Enable;

			// Token: 0x0400920F RID: 37391
			public float VelocityThreshold;

			// Token: 0x04009210 RID: 37392
			public float Duration;
		}

		// Token: 0x02001155 RID: 4437
		private class AutoScrollState
		{
			// Token: 0x06009F8B RID: 40843 RVA: 0x0033CAFF File Offset: 0x0033ACFF
			public void Reset()
			{
				this.Enable = false;
				this.Elastic = false;
				this.Duration = 0f;
				this.StartTime = 0f;
				this.EndScrollPosition = 0f;
			}

			// Token: 0x04009211 RID: 37393
			public bool Enable;

			// Token: 0x04009212 RID: 37394
			public bool Elastic;

			// Token: 0x04009213 RID: 37395
			public float Duration;

			// Token: 0x04009214 RID: 37396
			public float StartTime;

			// Token: 0x04009215 RID: 37397
			public float EndScrollPosition;
		}
	}
}
