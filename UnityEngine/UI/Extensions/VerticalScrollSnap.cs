using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000317 RID: 791
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("Layout/Extensions/Vertical Scroll Snap")]
	public class VerticalScrollSnap : ScrollSnapBase
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0004116C File Offset: 0x0003F36C
		private void Start()
		{
			this._isVertical = true;
			this._childAnchorPoint = new Vector2(0.5f, 0f);
			this._currentPage = this.StartingScreen;
			this.panelDimensions = base.gameObject.GetComponent<RectTransform>().rect;
			this.UpdateLayout();
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000411C0 File Offset: 0x0003F3C0
		private void Update()
		{
			this.updated = false;
			if (!this._lerp && this._scroll_rect.velocity == Vector2.zero)
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
				if (Vector3.Distance(this._screensContainer.anchoredPosition, this._lerp_target) < 0.1f)
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
			if (!this._pointerDown && ((double)this._scroll_rect.velocity.y > 0.01 || (double)this._scroll_rect.velocity.y < -0.01) && this.IsRectMovingSlowerThanThreshold(0f))
			{
				base.ScrollToClosestElement();
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00041330 File Offset: 0x0003F530
		private bool IsRectMovingSlowerThanThreshold(float startingSpeed)
		{
			return (this._scroll_rect.velocity.y > startingSpeed && this._scroll_rect.velocity.y < (float)this.SwipeVelocityThreshold) || (this._scroll_rect.velocity.y < startingSpeed && this._scroll_rect.velocity.y > (float)(-(float)this.SwipeVelocityThreshold));
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0004139C File Offset: 0x0003F59C
		public void DistributePages()
		{
			this._screens = this._screensContainer.childCount;
			this._scroll_rect.verticalNormalizedPosition = 0f;
			float num = 0f;
			Rect rect = base.gameObject.GetComponent<RectTransform>().rect;
			float num2 = 0f;
			float num3 = this._childSize = (float)((int)rect.height) * ((this.PageStep == 0f) ? 3f : this.PageStep);
			for (int i = 0; i < this._screensContainer.transform.childCount; i++)
			{
				RectTransform component = this._screensContainer.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
				num2 = num + (float)i * num3;
				component.sizeDelta = new Vector2(rect.width, rect.height);
				component.anchoredPosition = new Vector2(0f, num2);
				component.anchorMin = (component.anchorMax = (component.pivot = this._childAnchorPoint));
			}
			float y = num2 + num * -1f;
			this._screensContainer.GetComponent<RectTransform>().offsetMax = new Vector2(0f, y);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x000414D5 File Offset: 0x0003F6D5
		public void AddChild(GameObject GO)
		{
			this.AddChild(GO, false);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000414E0 File Offset: 0x0003F6E0
		public void AddChild(GameObject GO, bool WorldPositionStays)
		{
			this._scroll_rect.verticalNormalizedPosition = 0f;
			GO.transform.SetParent(this._screensContainer, WorldPositionStays);
			base.InitialiseChildObjectsFromScene();
			this.DistributePages();
			if (this.MaskArea)
			{
				base.UpdateVisible();
			}
			this.SetScrollContainerPosition();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00041534 File Offset: 0x0003F734
		public void RemoveChild(int index, out GameObject ChildRemoved)
		{
			this.RemoveChild(index, false, out ChildRemoved);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00041540 File Offset: 0x0003F740
		public void RemoveChild(int index, bool WorldPositionStays, out GameObject ChildRemoved)
		{
			ChildRemoved = null;
			if (index < 0 || index > this._screensContainer.childCount)
			{
				return;
			}
			this._scroll_rect.verticalNormalizedPosition = 0f;
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

		// Token: 0x06001223 RID: 4643 RVA: 0x000415D8 File Offset: 0x0003F7D8
		public void RemoveAllChildren(out GameObject[] ChildrenRemoved)
		{
			this.RemoveAllChildren(false, out ChildrenRemoved);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000415E4 File Offset: 0x0003F7E4
		public void RemoveAllChildren(bool WorldPositionStays, out GameObject[] ChildrenRemoved)
		{
			int childCount = this._screensContainer.childCount;
			ChildrenRemoved = new GameObject[childCount];
			for (int i = childCount - 1; i >= 0; i--)
			{
				ChildrenRemoved[i] = this._screensContainer.GetChild(i).gameObject;
				ChildrenRemoved[i].transform.SetParent(null, WorldPositionStays);
			}
			this._scroll_rect.verticalNormalizedPosition = 0f;
			base.CurrentPage = 0;
			base.InitialiseChildObjectsFromScene();
			this.DistributePages();
			if (this.MaskArea)
			{
				base.UpdateVisible();
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004166E File Offset: 0x0003F86E
		private void SetScrollContainerPosition()
		{
			this._scrollStartPosition = this._screensContainer.anchoredPosition.y;
			this._scroll_rect.verticalNormalizedPosition = (float)this._currentPage / (float)(this._screens - 1);
			base.OnCurrentScreenChange(this._currentPage);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x000416AE File Offset: 0x0003F8AE
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

		// Token: 0x06001227 RID: 4647 RVA: 0x000416E2 File Offset: 0x0003F8E2
		private void OnRectTransformDimensionsChange()
		{
			if (this._childAnchorPoint != Vector2.zero)
			{
				this.UpdateLayout();
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000416FC File Offset: 0x0003F8FC
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

		// Token: 0x06001229 RID: 4649 RVA: 0x00041754 File Offset: 0x0003F954
		public override void OnEndDrag(PointerEventData eventData)
		{
			if (this.updated)
			{
				return;
			}
			this.updated = true;
			this._pointerDown = false;
			if (this._scroll_rect.vertical)
			{
				if (this.UseSwipeDeltaThreshold && Math.Abs(eventData.delta.y) < this.SwipeDeltaThreshold)
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
					if (this._startPosition.y - this._screensContainer.anchoredPosition.y > 0f)
					{
						base.NextScreen();
						return;
					}
					base.PreviousScreen();
					return;
				}
				else if (this.UseFastSwipe && num < this.panelDimensions.height + (float)this.FastSwipeThreshold && num >= 1f)
				{
					this._scroll_rect.velocity = Vector3.zero;
					if (this._startPosition.y - this._screensContainer.anchoredPosition.y > 0f)
					{
						if (this._startPosition.y - this._screensContainer.anchoredPosition.y > this._childSize / 3f)
						{
							base.ScrollToClosestElement();
							return;
						}
						base.NextScreen();
						return;
					}
					else
					{
						if (this._startPosition.y - this._screensContainer.anchoredPosition.y > -this._childSize / 3f)
						{
							base.ScrollToClosestElement();
							return;
						}
						base.PreviousScreen();
					}
				}
			}
		}

		// Token: 0x04000C98 RID: 3224
		private bool updated = true;
	}
}
