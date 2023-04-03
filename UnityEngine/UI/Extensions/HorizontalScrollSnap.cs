using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200030D RID: 781
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("Layout/Extensions/Horizontal Scroll Snap")]
	public class HorizontalScrollSnap : ScrollSnapBase
	{
		// Token: 0x06001177 RID: 4471 RVA: 0x0003D734 File Offset: 0x0003B934
		private void Start()
		{
			this._isVertical = false;
			this._childAnchorPoint = new Vector2(0f, 0.5f);
			this._currentPage = this.StartingScreen;
			this.panelDimensions = base.gameObject.GetComponent<RectTransform>().rect;
			this.UpdateLayout();
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0003D788 File Offset: 0x0003B988
		private void Update()
		{
			this.updated = false;
			if (!this._lerp && this._scroll_rect.velocity == Vector2.zero && this._scroll_rect.inertia)
			{
				if (!this._settled && !this._pointerDown && !base.IsRectSettledOnaPage(this._screensContainer.anchoredPosition))
				{
					base.ScrollToClosestElement();
				}
				return;
			}
			if (this._lerp)
			{
				this._screensContainer.anchoredPosition = Vector3.Lerp(this._screensContainer.anchoredPosition, this._lerp_target, this.transitionSpeed * (this.UseTimeScale ? Time.deltaTime : Time.unscaledDeltaTime));
				if (Vector3.Distance(this._screensContainer.anchoredPosition, this._lerp_target) < 0.2f)
				{
					this._screensContainer.anchoredPosition = this._lerp_target;
					this._lerp = false;
					base.EndScreenChange();
				}
			}
			if (this.UseHardSwipe)
			{
				return;
			}
			base.CurrentPage = base.GetPageforPosition(this._screensContainer.anchoredPosition);
			if (!this._pointerDown && ((double)this._scroll_rect.velocity.x > 0.01 || (double)this._scroll_rect.velocity.x < -0.01) && this.IsRectMovingSlowerThanThreshold(0f))
			{
				base.ScrollToClosestElement();
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0003D908 File Offset: 0x0003BB08
		private bool IsRectMovingSlowerThanThreshold(float startingSpeed)
		{
			return (this._scroll_rect.velocity.x > startingSpeed && this._scroll_rect.velocity.x < (float)this.SwipeVelocityThreshold) || (this._scroll_rect.velocity.x < startingSpeed && this._scroll_rect.velocity.x > (float)(-(float)this.SwipeVelocityThreshold));
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0003D974 File Offset: 0x0003BB74
		public void DistributePages()
		{
			this._screens = this._screensContainer.childCount;
			this._scroll_rect.horizontalNormalizedPosition = 0f;
			float num = 0f;
			Rect rect = base.gameObject.GetComponent<RectTransform>().rect;
			float num2 = 0f;
			float num3 = this._childSize = (float)((int)rect.width) * ((this.PageStep == 0f) ? 3f : this.PageStep);
			for (int i = 0; i < this._screensContainer.transform.childCount; i++)
			{
				RectTransform component = this._screensContainer.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
				num2 = num + (float)i * num3;
				component.sizeDelta = new Vector2(rect.width, rect.height);
				component.anchoredPosition = new Vector2(num2, 0f);
				component.anchorMin = (component.anchorMax = (component.pivot = this._childAnchorPoint));
			}
			float x = num2 + num * -1f;
			this._screensContainer.GetComponent<RectTransform>().offsetMax = new Vector2(x, 0f);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0003DAAD File Offset: 0x0003BCAD
		public void AddChild(GameObject GO)
		{
			this.AddChild(GO, false);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0003DAB8 File Offset: 0x0003BCB8
		public void AddChild(GameObject GO, bool WorldPositionStays)
		{
			this._scroll_rect.horizontalNormalizedPosition = 0f;
			GO.transform.SetParent(this._screensContainer, WorldPositionStays);
			base.InitialiseChildObjectsFromScene();
			this.DistributePages();
			if (this.MaskArea)
			{
				base.UpdateVisible();
			}
			this.SetScrollContainerPosition();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0003DB0C File Offset: 0x0003BD0C
		public void RemoveChild(int index, out GameObject ChildRemoved)
		{
			this.RemoveChild(index, false, out ChildRemoved);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003DB18 File Offset: 0x0003BD18
		public void RemoveChild(int index, bool WorldPositionStays, out GameObject ChildRemoved)
		{
			ChildRemoved = null;
			if (index < 0 || index > this._screensContainer.childCount)
			{
				return;
			}
			this._scroll_rect.horizontalNormalizedPosition = 0f;
			Transform child = this._screensContainer.transform.GetChild(index);
			child.SetParent(null, WorldPositionStays);
			ChildRemoved = child.gameObject;
			base.InitialiseChildObjectsFromScene();
			this.DistributePages();
			if (this.MaskArea)
			{
				base.UpdateVisible();
			}
			if (this._currentPage > this._screens - 1)
			{
				base.CurrentPage = this._screens - 1;
			}
			this.SetScrollContainerPosition();
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0003DBB0 File Offset: 0x0003BDB0
		public void RemoveAllChildren(out GameObject[] ChildrenRemoved)
		{
			this.RemoveAllChildren(false, out ChildrenRemoved);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003DBBC File Offset: 0x0003BDBC
		public void RemoveAllChildren(bool WorldPositionStays, out GameObject[] ChildrenRemoved)
		{
			int childCount = this._screensContainer.childCount;
			ChildrenRemoved = new GameObject[childCount];
			for (int i = childCount - 1; i >= 0; i--)
			{
				ChildrenRemoved[i] = this._screensContainer.GetChild(i).gameObject;
				ChildrenRemoved[i].transform.SetParent(null, WorldPositionStays);
			}
			this._scroll_rect.horizontalNormalizedPosition = 0f;
			base.CurrentPage = 0;
			base.InitialiseChildObjectsFromScene();
			this.DistributePages();
			if (this.MaskArea)
			{
				base.UpdateVisible();
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0003DC46 File Offset: 0x0003BE46
		private void SetScrollContainerPosition()
		{
			this._scrollStartPosition = this._screensContainer.anchoredPosition.x;
			this._scroll_rect.horizontalNormalizedPosition = (float)this._currentPage / (float)(this._screens - 1);
			base.OnCurrentScreenChange(this._currentPage);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003DC86 File Offset: 0x0003BE86
		public void UpdateLayout()
		{
			this._lerp = false;
			this.DistributePages();
			if (this.MaskArea)
			{
				base.UpdateVisible();
			}
			this.SetScrollContainerPosition();
			base.OnCurrentScreenChange(this._currentPage);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0003DCBA File Offset: 0x0003BEBA
		private void OnRectTransformDimensionsChange()
		{
			if (this._childAnchorPoint != Vector2.zero)
			{
				this.UpdateLayout();
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003DCD4 File Offset: 0x0003BED4
		private void OnEnable()
		{
			base.InitialiseChildObjectsFromScene();
			this.DistributePages();
			if (this.MaskArea)
			{
				base.UpdateVisible();
			}
			if (this.JumpOnEnable || !this.RestartOnEnable)
			{
				this.SetScrollContainerPosition();
			}
			if (this.RestartOnEnable)
			{
				base.GoToScreen(this.StartingScreen, false);
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003DD2C File Offset: 0x0003BF2C
		public override void OnEndDrag(PointerEventData eventData)
		{
			if (this.updated)
			{
				return;
			}
			this.updated = true;
			this._pointerDown = false;
			if (this._scroll_rect.horizontal)
			{
				if (this.UseSwipeDeltaThreshold && Math.Abs(eventData.delta.x) < this.SwipeDeltaThreshold)
				{
					base.ScrollToClosestElement();
					return;
				}
				float num = Vector3.Distance(this._startPosition, this._screensContainer.anchoredPosition);
				if (this.UseHardSwipe)
				{
					this._scroll_rect.velocity = Vector3.zero;
					if (num <= (float)this.FastSwipeThreshold)
					{
						base.ScrollToClosestElement();
						return;
					}
					if (this._startPosition.x - this._screensContainer.anchoredPosition.x > 0f)
					{
						base.NextScreen();
						return;
					}
					base.PreviousScreen();
					return;
				}
				else if (this.UseFastSwipe && num < this.panelDimensions.width && num >= (float)this.FastSwipeThreshold)
				{
					this._scroll_rect.velocity = Vector3.zero;
					if (this._startPosition.x - this._screensContainer.anchoredPosition.x > 0f)
					{
						if (this._startPosition.x - this._screensContainer.anchoredPosition.x > this._childSize / 3f)
						{
							base.ScrollToClosestElement();
							return;
						}
						base.NextScreen();
						return;
					}
					else
					{
						if (this._startPosition.x - this._screensContainer.anchoredPosition.x < -this._childSize / 3f)
						{
							base.ScrollToClosestElement();
							return;
						}
						base.PreviousScreen();
					}
				}
			}
		}

		// Token: 0x04000C13 RID: 3091
		private bool updated = true;
	}
}
