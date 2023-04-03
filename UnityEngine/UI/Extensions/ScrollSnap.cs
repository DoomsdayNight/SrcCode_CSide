using System;
using System.Collections;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000311 RID: 785
	[ExecuteInEditMode]
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("UI/Extensions/Scroll Snap")]
	public class ScrollSnap : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler, IScrollSnap
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060011A5 RID: 4517 RVA: 0x0003E804 File Offset: 0x0003CA04
		// (remove) Token: 0x060011A6 RID: 4518 RVA: 0x0003E83C File Offset: 0x0003CA3C
		public event ScrollSnap.PageSnapChange onPageChange;

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003E874 File Offset: 0x0003CA74
		private void Start()
		{
			this._lerp = false;
			this._scroll_rect = base.gameObject.GetComponent<ScrollRect>();
			this._scrollRectTransform = base.gameObject.GetComponent<RectTransform>();
			this._listContainerTransform = this._scroll_rect.content;
			this._listContainerRectTransform = this._listContainerTransform.GetComponent<RectTransform>();
			this.UpdateListItemsSize();
			this.UpdateListItemPositions();
			this.PageChanged(this.CurrentPage());
			if (this.NextButton)
			{
				this.NextButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.NextScreen();
				});
			}
			if (this.PrevButton)
			{
				this.PrevButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.PreviousScreen();
				});
			}
			if (this._scroll_rect.horizontalScrollbar != null && this._scroll_rect.horizontal)
			{
				this._scroll_rect.horizontalScrollbar.gameObject.GetOrAddComponent<ScrollSnapScrollbarHelper>().ss = this;
			}
			if (this._scroll_rect.verticalScrollbar != null && this._scroll_rect.vertical)
			{
				this._scroll_rect.verticalScrollbar.gameObject.GetOrAddComponent<ScrollSnapScrollbarHelper>().ss = this;
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003E9B8 File Offset: 0x0003CBB8
		public void UpdateListItemsSize()
		{
			float num;
			float num2;
			if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				num = this._scrollRectTransform.rect.width / (float)this.ItemsVisibleAtOnce;
				num2 = this._listContainerRectTransform.rect.width / (float)this._itemsCount;
			}
			else
			{
				num = this._scrollRectTransform.rect.height / (float)this.ItemsVisibleAtOnce;
				num2 = this._listContainerRectTransform.rect.height / (float)this._itemsCount;
			}
			this._itemSize = num;
			if (this.LinkScrolrectScrollSensitivity)
			{
				this._scroll_rect.scrollSensitivity = this._itemSize;
			}
			if (this.AutoLayoutItems && num2 != num && this._itemsCount > 0)
			{
				if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
				{
					using (IEnumerator enumerator = this._listContainerTransform.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							GameObject gameObject = ((Transform)obj).gameObject;
							if (gameObject.activeInHierarchy)
							{
								LayoutElement layoutElement = gameObject.GetComponent<LayoutElement>();
								if (layoutElement == null)
								{
									layoutElement = gameObject.AddComponent<LayoutElement>();
								}
								layoutElement.minWidth = this._itemSize;
							}
						}
						return;
					}
				}
				foreach (object obj2 in this._listContainerTransform)
				{
					GameObject gameObject2 = ((Transform)obj2).gameObject;
					if (gameObject2.activeInHierarchy)
					{
						LayoutElement layoutElement2 = gameObject2.GetComponent<LayoutElement>();
						if (layoutElement2 == null)
						{
							layoutElement2 = gameObject2.AddComponent<LayoutElement>();
						}
						layoutElement2.minHeight = this._itemSize;
					}
				}
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0003EB90 File Offset: 0x0003CD90
		public void UpdateListItemPositions()
		{
			if (!this._listContainerRectTransform.rect.size.Equals(this._listContainerCachedSize))
			{
				int num = 0;
				using (IEnumerator enumerator = this._listContainerTransform.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((Transform)enumerator.Current).gameObject.activeInHierarchy)
						{
							num++;
						}
					}
				}
				this._itemsCount = 0;
				Array.Resize<Vector3>(ref this._pageAnchorPositions, num);
				if (num > 0)
				{
					this._pages = Mathf.Max(num - this.ItemsVisibleAtOnce + 1, 1);
					if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
					{
						this._scroll_rect.horizontalNormalizedPosition = 0f;
						this._listContainerMaxPosition = this._listContainerTransform.localPosition.x;
						this._scroll_rect.horizontalNormalizedPosition = 1f;
						this._listContainerMinPosition = this._listContainerTransform.localPosition.x;
						this._listContainerSize = this._listContainerMaxPosition - this._listContainerMinPosition;
						for (int i = 0; i < this._pages; i++)
						{
							this._pageAnchorPositions[i] = new Vector3(this._listContainerMaxPosition - this._itemSize * (float)i, this._listContainerTransform.localPosition.y, this._listContainerTransform.localPosition.z);
						}
					}
					else
					{
						this._scroll_rect.verticalNormalizedPosition = 1f;
						this._listContainerMinPosition = this._listContainerTransform.localPosition.y;
						this._scroll_rect.verticalNormalizedPosition = 0f;
						this._listContainerMaxPosition = this._listContainerTransform.localPosition.y;
						this._listContainerSize = this._listContainerMaxPosition - this._listContainerMinPosition;
						for (int j = 0; j < this._pages; j++)
						{
							this._pageAnchorPositions[j] = new Vector3(this._listContainerTransform.localPosition.x, this._listContainerMinPosition + this._itemSize * (float)j, this._listContainerTransform.localPosition.z);
						}
					}
					this.UpdateScrollbar(this.LinkScrolbarSteps);
					this._startingPage = Mathf.Min(this._startingPage, this._pages);
					this.ResetPage();
				}
				if (this._itemsCount != num)
				{
					this.PageChanged(this.CurrentPage());
				}
				this._itemsCount = num;
				this._listContainerCachedSize.Set(this._listContainerRectTransform.rect.size.x, this._listContainerRectTransform.rect.size.y);
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0003EE4C File Offset: 0x0003D04C
		public void ResetPage()
		{
			if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				this._scroll_rect.horizontalNormalizedPosition = ((this._pages > 1) ? ((float)this._startingPage / (float)(this._pages - 1)) : 0f);
				return;
			}
			this._scroll_rect.verticalNormalizedPosition = ((this._pages > 1) ? ((float)(this._pages - this._startingPage - 1) / (float)(this._pages - 1)) : 0f);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0003EEC4 File Offset: 0x0003D0C4
		private void UpdateScrollbar(bool linkSteps)
		{
			if (linkSteps)
			{
				if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
				{
					if (this._scroll_rect.horizontalScrollbar != null)
					{
						this._scroll_rect.horizontalScrollbar.numberOfSteps = this._pages;
						return;
					}
				}
				else if (this._scroll_rect.verticalScrollbar != null)
				{
					this._scroll_rect.verticalScrollbar.numberOfSteps = this._pages;
					return;
				}
			}
			else if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				if (this._scroll_rect.horizontalScrollbar != null)
				{
					this._scroll_rect.horizontalScrollbar.numberOfSteps = 0;
					return;
				}
			}
			else if (this._scroll_rect.verticalScrollbar != null)
			{
				this._scroll_rect.verticalScrollbar.numberOfSteps = 0;
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003EF84 File Offset: 0x0003D184
		private void LateUpdate()
		{
			this.UpdateListItemsSize();
			this.UpdateListItemPositions();
			if (this._lerp)
			{
				this.UpdateScrollbar(false);
				this._listContainerTransform.localPosition = Vector3.Lerp(this._listContainerTransform.localPosition, this._lerpTarget, 7.5f * Time.deltaTime);
				if (Vector3.Distance(this._listContainerTransform.localPosition, this._lerpTarget) < 0.001f)
				{
					this._listContainerTransform.localPosition = this._lerpTarget;
					this._lerp = false;
					this.UpdateScrollbar(this.LinkScrolbarSteps);
				}
				if (Vector3.Distance(this._listContainerTransform.localPosition, this._lerpTarget) < 10f)
				{
					this.PageChanged(this.CurrentPage());
				}
			}
			if (this._fastSwipeTimer)
			{
				this._fastSwipeCounter++;
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0003F05C File Offset: 0x0003D25C
		public void NextScreen()
		{
			this.UpdateListItemPositions();
			if (this.CurrentPage() < this._pages - 1)
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[this.CurrentPage() + 1];
				this.PageChanged(this.CurrentPage() + 1);
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0003F0AD File Offset: 0x0003D2AD
		public void PreviousScreen()
		{
			this.UpdateListItemPositions();
			if (this.CurrentPage() > 0)
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[this.CurrentPage() - 1];
				this.PageChanged(this.CurrentPage() - 1);
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0003F0EC File Offset: 0x0003D2EC
		private void NextScreenCommand()
		{
			if (this._pageOnDragStart < this._pages - 1)
			{
				int num = Mathf.Min(this._pages - 1, this._pageOnDragStart + this.ItemsVisibleAtOnce);
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[num];
				this.PageChanged(num);
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0003F144 File Offset: 0x0003D344
		private void PrevScreenCommand()
		{
			if (this._pageOnDragStart > 0)
			{
				int num = Mathf.Max(0, this._pageOnDragStart - this.ItemsVisibleAtOnce);
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[num];
				this.PageChanged(num);
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0003F190 File Offset: 0x0003D390
		public int CurrentPage()
		{
			float num;
			if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				num = this._listContainerMaxPosition - this._listContainerTransform.localPosition.x;
				num = Mathf.Clamp(num, 0f, this._listContainerSize);
			}
			else
			{
				num = this._listContainerTransform.localPosition.y - this._listContainerMinPosition;
				num = Mathf.Clamp(num, 0f, this._listContainerSize);
			}
			return Mathf.Clamp(Mathf.RoundToInt(num / this._itemSize), 0, this._pages);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0003F214 File Offset: 0x0003D414
		public void SetLerp(bool value)
		{
			this._lerp = value;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0003F21D File Offset: 0x0003D41D
		public void ChangePage(int page)
		{
			if (0 <= page && page < this._pages)
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[page];
				this.PageChanged(page);
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0003F24C File Offset: 0x0003D44C
		private void PageChanged(int currentPage)
		{
			this._startingPage = currentPage;
			if (this.NextButton)
			{
				this.NextButton.interactable = (currentPage < this._pages - 1);
			}
			if (this.PrevButton)
			{
				this.PrevButton.interactable = (currentPage > 0);
			}
			if (this.onPageChange != null)
			{
				this.onPageChange(currentPage);
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0003F2B3 File Offset: 0x0003D4B3
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.UpdateScrollbar(false);
			this._fastSwipeCounter = 0;
			this._fastSwipeTimer = true;
			this._positionOnDragStart = eventData.position;
			this._pageOnDragStart = this.CurrentPage();
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003F2E8 File Offset: 0x0003D4E8
		public void OnEndDrag(PointerEventData eventData)
		{
			this._startDrag = true;
			float num;
			if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				num = this._positionOnDragStart.x - eventData.position.x;
			}
			else
			{
				num = -this._positionOnDragStart.y + eventData.position.y;
			}
			if (!this.UseFastSwipe)
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[this.CurrentPage()];
				return;
			}
			this.fastSwipe = false;
			this._fastSwipeTimer = false;
			if (this._fastSwipeCounter <= this._fastSwipeTarget && Math.Abs(num) > (float)this.FastSwipeThreshold)
			{
				this.fastSwipe = true;
			}
			if (!this.fastSwipe)
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[this.CurrentPage()];
				return;
			}
			if (num > 0f)
			{
				this.NextScreenCommand();
				return;
			}
			this.PrevScreenCommand();
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003F3D2 File Offset: 0x0003D5D2
		public void OnDrag(PointerEventData eventData)
		{
			this._lerp = false;
			if (this._startDrag)
			{
				this.OnBeginDrag(eventData);
				this._startDrag = false;
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003F3F1 File Offset: 0x0003D5F1
		public void StartScreenChange()
		{
		}

		// Token: 0x04000C2B RID: 3115
		private ScrollRect _scroll_rect;

		// Token: 0x04000C2C RID: 3116
		private RectTransform _scrollRectTransform;

		// Token: 0x04000C2D RID: 3117
		private Transform _listContainerTransform;

		// Token: 0x04000C2E RID: 3118
		private int _pages;

		// Token: 0x04000C2F RID: 3119
		private int _startingPage;

		// Token: 0x04000C30 RID: 3120
		private Vector3[] _pageAnchorPositions;

		// Token: 0x04000C31 RID: 3121
		private Vector3 _lerpTarget;

		// Token: 0x04000C32 RID: 3122
		private bool _lerp;

		// Token: 0x04000C33 RID: 3123
		private float _listContainerMinPosition;

		// Token: 0x04000C34 RID: 3124
		private float _listContainerMaxPosition;

		// Token: 0x04000C35 RID: 3125
		private float _listContainerSize;

		// Token: 0x04000C36 RID: 3126
		private RectTransform _listContainerRectTransform;

		// Token: 0x04000C37 RID: 3127
		private Vector2 _listContainerCachedSize;

		// Token: 0x04000C38 RID: 3128
		private float _itemSize;

		// Token: 0x04000C39 RID: 3129
		private int _itemsCount;

		// Token: 0x04000C3A RID: 3130
		private bool _startDrag = true;

		// Token: 0x04000C3B RID: 3131
		private Vector3 _positionOnDragStart;

		// Token: 0x04000C3C RID: 3132
		private int _pageOnDragStart;

		// Token: 0x04000C3D RID: 3133
		private bool _fastSwipeTimer;

		// Token: 0x04000C3E RID: 3134
		private int _fastSwipeCounter;

		// Token: 0x04000C3F RID: 3135
		private int _fastSwipeTarget = 10;

		// Token: 0x04000C40 RID: 3136
		[Tooltip("Button to go to the next page. (optional)")]
		public Button NextButton;

		// Token: 0x04000C41 RID: 3137
		[Tooltip("Button to go to the previous page. (optional)")]
		public Button PrevButton;

		// Token: 0x04000C42 RID: 3138
		[Tooltip("Number of items visible in one page of scroll frame.")]
		[Range(1f, 100f)]
		public int ItemsVisibleAtOnce = 1;

		// Token: 0x04000C43 RID: 3139
		[Tooltip("Sets minimum width of list items to 1/itemsVisibleAtOnce.")]
		public bool AutoLayoutItems = true;

		// Token: 0x04000C44 RID: 3140
		[Tooltip("If you wish to update scrollbar numberOfSteps to number of active children on list.")]
		public bool LinkScrolbarSteps;

		// Token: 0x04000C45 RID: 3141
		[Tooltip("If you wish to update scrollrect sensitivity to size of list element.")]
		public bool LinkScrolrectScrollSensitivity;

		// Token: 0x04000C46 RID: 3142
		public bool UseFastSwipe = true;

		// Token: 0x04000C47 RID: 3143
		public int FastSwipeThreshold = 100;

		// Token: 0x04000C49 RID: 3145
		public ScrollSnap.ScrollDirection direction;

		// Token: 0x04000C4A RID: 3146
		private bool fastSwipe;

		// Token: 0x02001156 RID: 4438
		public enum ScrollDirection
		{
			// Token: 0x04009217 RID: 37399
			Horizontal,
			// Token: 0x04009218 RID: 37400
			Vertical
		}

		// Token: 0x02001157 RID: 4439
		// (Invoke) Token: 0x06009F8E RID: 40846
		public delegate void PageSnapChange(int page);
	}
}
