using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000312 RID: 786
	public class ScrollSnapBase : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollSnap, IPointerClickHandler
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0003F437 File Offset: 0x0003D637
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x0003F440 File Offset: 0x0003D640
		public int CurrentPage
		{
			get
			{
				return this._currentPage;
			}
			internal set
			{
				if (this._isInfinite)
				{
					float num = (float)value / (float)this._screensContainer.childCount;
					if (num < 0f)
					{
						this._infiniteWindow = (int)Math.Floor((double)num);
					}
					else
					{
						this._infiniteWindow = value / this._screensContainer.childCount;
					}
					this._infiniteWindow = ((value < 0) ? (-this._infiniteWindow) : this._infiniteWindow);
					value %= this._screensContainer.childCount;
					if (value < 0)
					{
						value = this._screensContainer.childCount + value;
					}
					else if (value > this._screensContainer.childCount - 1)
					{
						value -= this._screensContainer.childCount;
					}
				}
				if ((value != this._currentPage && value >= 0 && value < this._screensContainer.childCount) || (value == 0 && this._screensContainer.childCount == 0))
				{
					this._previousPage = this._currentPage;
					this._currentPage = value;
					if (this.MaskArea)
					{
						this.UpdateVisible();
					}
					if (!this._lerp)
					{
						this.ScreenChange();
					}
					this.OnCurrentScreenChange(this._currentPage);
				}
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0003F55A File Offset: 0x0003D75A
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x0003F562 File Offset: 0x0003D762
		public ScrollSnapBase.SelectionChangeStartEvent OnSelectionChangeStartEvent
		{
			get
			{
				return this.m_OnSelectionChangeStartEvent;
			}
			set
			{
				this.m_OnSelectionChangeStartEvent = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0003F56B File Offset: 0x0003D76B
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x0003F573 File Offset: 0x0003D773
		public ScrollSnapBase.SelectionPageChangedEvent OnSelectionPageChangedEvent
		{
			get
			{
				return this.m_OnSelectionPageChangedEvent;
			}
			set
			{
				this.m_OnSelectionPageChangedEvent = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0003F57C File Offset: 0x0003D77C
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x0003F584 File Offset: 0x0003D784
		public ScrollSnapBase.SelectionChangeEndEvent OnSelectionChangeEndEvent
		{
			get
			{
				return this.m_OnSelectionChangeEndEvent;
			}
			set
			{
				this.m_OnSelectionChangeEndEvent = value;
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0003F590 File Offset: 0x0003D790
		private void Awake()
		{
			if (this._scroll_rect == null)
			{
				this._scroll_rect = base.gameObject.GetComponent<ScrollRect>();
			}
			if (this._scroll_rect.horizontalScrollbar && this._scroll_rect.horizontal)
			{
				this._scroll_rect.horizontalScrollbar.gameObject.AddComponent<ScrollSnapScrollbarHelper>().ss = this;
			}
			if (this._scroll_rect.verticalScrollbar && this._scroll_rect.vertical)
			{
				this._scroll_rect.verticalScrollbar.gameObject.AddComponent<ScrollSnapScrollbarHelper>().ss = this;
			}
			this.panelDimensions = base.gameObject.GetComponent<RectTransform>().rect;
			if (this.StartingScreen < 0)
			{
				this.StartingScreen = 0;
			}
			this._screensContainer = this._scroll_rect.content;
			this.InitialiseChildObjects();
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
			this._isInfinite = (base.GetComponent<UI_InfiniteScroll>() != null);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0003F6DC File Offset: 0x0003D8DC
		internal void InitialiseChildObjects()
		{
			if (this.ChildObjects != null && this.ChildObjects.Length != 0)
			{
				if (this._screensContainer.transform.childCount > 0)
				{
					Debug.LogError("ScrollRect Content has children, this is not supported when using managed Child Objects\n Either remove the ScrollRect Content children or clear the ChildObjects array");
					return;
				}
				this.InitialiseChildObjectsFromArray();
				if (base.GetComponent<UI_InfiniteScroll>() != null)
				{
					base.GetComponent<UI_InfiniteScroll>().Init();
					return;
				}
			}
			else
			{
				this.InitialiseChildObjectsFromScene();
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003F740 File Offset: 0x0003D940
		internal void InitialiseChildObjectsFromScene()
		{
			int childCount = this._screensContainer.childCount;
			this.ChildObjects = new GameObject[childCount];
			for (int i = 0; i < childCount; i++)
			{
				this.ChildObjects[i] = this._screensContainer.transform.GetChild(i).gameObject;
				if (this.MaskArea && this.ChildObjects[i].activeSelf)
				{
					this.ChildObjects[i].SetActive(false);
				}
			}
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0003F7BC File Offset: 0x0003D9BC
		internal void InitialiseChildObjectsFromArray()
		{
			int num = this.ChildObjects.Length;
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.ChildObjects[i]);
				if (this.UseParentTransform)
				{
					RectTransform component = gameObject.GetComponent<RectTransform>();
					component.rotation = this._screensContainer.rotation;
					component.localScale = this._screensContainer.localScale;
					component.position = this._screensContainer.position;
				}
				gameObject.transform.SetParent(this._screensContainer.transform);
				this.ChildObjects[i] = gameObject;
				if (this.MaskArea && this.ChildObjects[i].activeSelf)
				{
					this.ChildObjects[i].SetActive(false);
				}
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0003F87C File Offset: 0x0003DA7C
		internal void UpdateVisible()
		{
			if (!this.MaskArea || this.ChildObjects == null || this.ChildObjects.Length < 1 || this._screensContainer.childCount < 1)
			{
				return;
			}
			this._maskSize = (this._isVertical ? this.MaskArea.rect.height : this.MaskArea.rect.width);
			this._halfNoVisibleItems = (int)Math.Round((double)(this._maskSize / (this._childSize * this.MaskBuffer)), MidpointRounding.AwayFromZero) / 2;
			this._bottomItem = (this._topItem = 0);
			for (int i = this._halfNoVisibleItems + 1; i > 0; i--)
			{
				this._bottomItem = ((this._currentPage - i < 0) ? 0 : i);
				if (this._bottomItem > 0)
				{
					break;
				}
			}
			for (int j = this._halfNoVisibleItems + 1; j > 0; j--)
			{
				this._topItem = ((this._screensContainer.childCount - this._currentPage - j < 0) ? 0 : j);
				if (this._topItem > 0)
				{
					break;
				}
			}
			for (int k = this.CurrentPage - this._bottomItem; k < this.CurrentPage + this._topItem; k++)
			{
				try
				{
					this.ChildObjects[k].SetActive(true);
				}
				catch
				{
					Debug.Log("Failed to setactive child [" + k.ToString() + "]");
				}
			}
			if (this._currentPage > this._halfNoVisibleItems)
			{
				this.ChildObjects[this.CurrentPage - this._bottomItem].SetActive(false);
			}
			if (this._screensContainer.childCount - this._currentPage > this._topItem)
			{
				this.ChildObjects[this.CurrentPage + this._topItem].SetActive(false);
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003FA54 File Offset: 0x0003DC54
		public void NextScreen()
		{
			if (this._currentPage < this._screens - 1 || this._isInfinite)
			{
				if (!this._lerp)
				{
					this.StartScreenChange();
				}
				this._lerp = true;
				if (this._isInfinite)
				{
					this.CurrentPage = this.GetPageforPosition(this._screensContainer.anchoredPosition) + 1;
				}
				else
				{
					this.CurrentPage = this._currentPage + 1;
				}
				this.GetPositionforPage(this._currentPage, ref this._lerp_target);
				this.ScreenChange();
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0003FADC File Offset: 0x0003DCDC
		public void PreviousScreen()
		{
			if (this._currentPage > 0 || this._isInfinite)
			{
				if (!this._lerp)
				{
					this.StartScreenChange();
				}
				this._lerp = true;
				if (this._isInfinite)
				{
					this.CurrentPage = this.GetPageforPosition(this._screensContainer.anchoredPosition) - 1;
				}
				else
				{
					this.CurrentPage = this._currentPage - 1;
				}
				this.GetPositionforPage(this._currentPage, ref this._lerp_target);
				this.ScreenChange();
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0003FB60 File Offset: 0x0003DD60
		public void GoToScreen(int screenIndex, bool pagination = false)
		{
			if (screenIndex <= this._screens - 1 && screenIndex >= 0)
			{
				if (!this._lerp || pagination)
				{
					this.StartScreenChange();
				}
				this._lerp = true;
				this.CurrentPage = screenIndex;
				this.GetPositionforPage(this._currentPage, ref this._lerp_target);
				this.ScreenChange();
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003FBB8 File Offset: 0x0003DDB8
		internal int GetPageforPosition(Vector3 pos)
		{
			if (!this._isVertical)
			{
				return (int)Math.Round((double)((this._scrollStartPosition - pos.x) / this._childSize));
			}
			return (int)Math.Round((double)((this._scrollStartPosition - pos.y) / this._childSize));
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0003FC04 File Offset: 0x0003DE04
		internal bool IsRectSettledOnaPage(Vector3 pos)
		{
			if (!this._isVertical)
			{
				return -((pos.x - this._scrollStartPosition) / this._childSize) == (float)(-(float)((int)Math.Round((double)((pos.x - this._scrollStartPosition) / this._childSize))));
			}
			return -((pos.y - this._scrollStartPosition) / this._childSize) == (float)(-(float)((int)Math.Round((double)((pos.y - this._scrollStartPosition) / this._childSize))));
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0003FC84 File Offset: 0x0003DE84
		internal void GetPositionforPage(int page, ref Vector3 target)
		{
			this._childPos = -this._childSize * (float)page;
			if (this._isVertical)
			{
				this._infiniteOffset = ((this._screensContainer.anchoredPosition.y < 0f) ? (-this._screensContainer.sizeDelta.y * (float)this._infiniteWindow) : (this._screensContainer.sizeDelta.y * (float)this._infiniteWindow));
				this._infiniteOffset = ((this._infiniteOffset == 0f) ? 0f : ((this._infiniteOffset < 0f) ? (this._infiniteOffset - this._childSize * (float)this._infiniteWindow) : (this._infiniteOffset + this._childSize * (float)this._infiniteWindow)));
				target.y = this._childPos + this._scrollStartPosition + this._infiniteOffset;
				return;
			}
			this._infiniteOffset = ((this._screensContainer.anchoredPosition.x < 0f) ? (-this._screensContainer.sizeDelta.x * (float)this._infiniteWindow) : (this._screensContainer.sizeDelta.x * (float)this._infiniteWindow));
			this._infiniteOffset = ((this._infiniteOffset == 0f) ? 0f : ((this._infiniteOffset < 0f) ? (this._infiniteOffset - this._childSize * (float)this._infiniteWindow) : (this._infiniteOffset + this._childSize * (float)this._infiniteWindow)));
			target.x = this._childPos + this._scrollStartPosition + this._infiniteOffset;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0003FE28 File Offset: 0x0003E028
		internal void ScrollToClosestElement()
		{
			this._lerp = true;
			this.CurrentPage = this.GetPageforPosition(this._screensContainer.anchoredPosition);
			this.GetPositionforPage(this._currentPage, ref this._lerp_target);
			this.OnCurrentScreenChange(this._currentPage);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0003FE76 File Offset: 0x0003E076
		internal void OnCurrentScreenChange(int currentScreen)
		{
			this.ChangeBulletsInfo(currentScreen);
			this.ToggleNavigationButtons(currentScreen);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003FE88 File Offset: 0x0003E088
		private void ChangeBulletsInfo(int targetScreen)
		{
			if (this.Pagination)
			{
				for (int i = 0; i < this.Pagination.transform.childCount; i++)
				{
					this.Pagination.transform.GetChild(i).GetComponent<Toggle>().isOn = (targetScreen == i);
				}
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003FEE0 File Offset: 0x0003E0E0
		private void ToggleNavigationButtons(int targetScreen)
		{
			if (!this._isInfinite)
			{
				if (this.PrevButton)
				{
					this.PrevButton.GetComponent<Button>().interactable = (targetScreen > 0);
				}
				if (this.NextButton)
				{
					this.NextButton.GetComponent<Button>().interactable = (targetScreen < this._screensContainer.transform.childCount - 1);
				}
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0003FF48 File Offset: 0x0003E148
		private void OnValidate()
		{
			if (this._scroll_rect == null)
			{
				this._scroll_rect = base.GetComponent<ScrollRect>();
			}
			if (!this._scroll_rect.horizontal && !this._scroll_rect.vertical)
			{
				Debug.LogError("ScrollRect has to have a direction, please select either Horizontal OR Vertical with the appropriate control.");
			}
			if (this._scroll_rect.horizontal && this._scroll_rect.vertical)
			{
				Debug.LogError("ScrollRect has to be unidirectional, only use either Horizontal or Vertical on the ScrollRect, NOT both.");
			}
			RectTransform content = base.gameObject.GetComponent<ScrollRect>().content;
			if (content != null)
			{
				int childCount = content.childCount;
				if (childCount != 0 || this.ChildObjects != null)
				{
					int num = (this.ChildObjects == null || this.ChildObjects.Length == 0) ? childCount : this.ChildObjects.Length;
					if (this.StartingScreen > num - 1)
					{
						this.StartingScreen = num - 1;
					}
					if (this.StartingScreen < 0)
					{
						this.StartingScreen = 0;
					}
				}
			}
			if (this.MaskBuffer <= 0f)
			{
				this.MaskBuffer = 1f;
			}
			if (this.PageStep < 0f)
			{
				this.PageStep = 0f;
			}
			if (this.PageStep > 8f)
			{
				this.PageStep = 9f;
			}
			UI_InfiniteScroll component = base.GetComponent<UI_InfiniteScroll>();
			if (this.ChildObjects != null && this.ChildObjects.Length != 0 && component != null && !component.InitByUser)
			{
				Debug.LogError("[" + base.gameObject.name + "]When using procedural children with a ScrollSnap (Adding Prefab ChildObjects) and the Infinite Scroll component\nYou must set the 'InitByUser' option to true, to enable late initialising");
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000400B4 File Offset: 0x0003E2B4
		public void StartScreenChange()
		{
			if (!this._startEventCalled)
			{
				this._suspendEvents = true;
				this._startEventCalled = true;
				this._endEventCalled = false;
				this.OnSelectionChangeStartEvent.Invoke();
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000400DE File Offset: 0x0003E2DE
		internal void ScreenChange()
		{
			this.OnSelectionPageChangedEvent.Invoke(this._currentPage);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000400F1 File Offset: 0x0003E2F1
		internal void EndScreenChange()
		{
			if (!this._endEventCalled)
			{
				this._suspendEvents = false;
				this._endEventCalled = true;
				this._startEventCalled = false;
				this._settled = true;
				this.OnSelectionChangeEndEvent.Invoke(this._currentPage);
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00040128 File Offset: 0x0003E328
		public Transform CurrentPageObject()
		{
			return this._screensContainer.GetChild(this.CurrentPage);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004013B File Offset: 0x0003E33B
		public void CurrentPageObject(out Transform returnObject)
		{
			returnObject = this._screensContainer.GetChild(this.CurrentPage);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00040150 File Offset: 0x0003E350
		public void OnBeginDrag(PointerEventData eventData)
		{
			this._pointerDown = true;
			this._settled = false;
			this.StartScreenChange();
			this._startPosition = this._screensContainer.anchoredPosition;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004017C File Offset: 0x0003E37C
		public void OnDrag(PointerEventData eventData)
		{
			this._lerp = false;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00040185 File Offset: 0x0003E385
		public virtual void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00040188 File Offset: 0x0003E388
		int IScrollSnap.CurrentPage()
		{
			return this.CurrentPage = this.GetPageforPosition(this._screensContainer.anchoredPosition);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000401B4 File Offset: 0x0003E3B4
		public void SetLerp(bool value)
		{
			this._lerp = value;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000401BD File Offset: 0x0003E3BD
		public void ChangePage(int page)
		{
			this.GoToScreen(page, false);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000401C7 File Offset: 0x0003E3C7
		public void OnPointerClick(PointerEventData eventData)
		{
			Vector2 anchoredPosition = this._screensContainer.anchoredPosition;
		}

		// Token: 0x04000C4B RID: 3147
		internal Rect panelDimensions;

		// Token: 0x04000C4C RID: 3148
		internal RectTransform _screensContainer;

		// Token: 0x04000C4D RID: 3149
		internal bool _isVertical;

		// Token: 0x04000C4E RID: 3150
		internal int _screens = 1;

		// Token: 0x04000C4F RID: 3151
		internal float _scrollStartPosition;

		// Token: 0x04000C50 RID: 3152
		internal float _childSize;

		// Token: 0x04000C51 RID: 3153
		private float _childPos;

		// Token: 0x04000C52 RID: 3154
		private float _maskSize;

		// Token: 0x04000C53 RID: 3155
		internal Vector2 _childAnchorPoint;

		// Token: 0x04000C54 RID: 3156
		internal ScrollRect _scroll_rect;

		// Token: 0x04000C55 RID: 3157
		internal Vector3 _lerp_target;

		// Token: 0x04000C56 RID: 3158
		internal bool _lerp;

		// Token: 0x04000C57 RID: 3159
		internal bool _pointerDown;

		// Token: 0x04000C58 RID: 3160
		internal bool _settled = true;

		// Token: 0x04000C59 RID: 3161
		internal Vector3 _startPosition;

		// Token: 0x04000C5A RID: 3162
		[Tooltip("The currently active page")]
		internal int _currentPage;

		// Token: 0x04000C5B RID: 3163
		internal int _previousPage;

		// Token: 0x04000C5C RID: 3164
		internal int _halfNoVisibleItems;

		// Token: 0x04000C5D RID: 3165
		internal bool _isInfinite;

		// Token: 0x04000C5E RID: 3166
		internal int _infiniteWindow;

		// Token: 0x04000C5F RID: 3167
		internal float _infiniteOffset;

		// Token: 0x04000C60 RID: 3168
		private int _bottomItem;

		// Token: 0x04000C61 RID: 3169
		private int _topItem;

		// Token: 0x04000C62 RID: 3170
		internal bool _startEventCalled;

		// Token: 0x04000C63 RID: 3171
		internal bool _endEventCalled;

		// Token: 0x04000C64 RID: 3172
		internal bool _suspendEvents;

		// Token: 0x04000C65 RID: 3173
		[Tooltip("The screen / page to start the control on\n*Note, this is a 0 indexed array")]
		[SerializeField]
		public int StartingScreen;

		// Token: 0x04000C66 RID: 3174
		[Tooltip("The distance between two pages based on page height, by default pages are next to each other")]
		[SerializeField]
		[Range(0f, 8f)]
		public float PageStep = 1f;

		// Token: 0x04000C67 RID: 3175
		[Tooltip("The gameobject that contains toggles which suggest pagination. (optional)")]
		public GameObject Pagination;

		// Token: 0x04000C68 RID: 3176
		[Tooltip("Button to go to the previous page. (optional)")]
		public GameObject PrevButton;

		// Token: 0x04000C69 RID: 3177
		[Tooltip("Button to go to the next page. (optional)")]
		public GameObject NextButton;

		// Token: 0x04000C6A RID: 3178
		[Tooltip("Transition speed between pages. (optional)")]
		public float transitionSpeed = 7.5f;

		// Token: 0x04000C6B RID: 3179
		[Tooltip("Hard Swipe forces to swiping to the next / previous page (optional)")]
		public bool UseHardSwipe;

		// Token: 0x04000C6C RID: 3180
		[Tooltip("Fast Swipe makes swiping page next / previous (optional)")]
		public bool UseFastSwipe;

		// Token: 0x04000C6D RID: 3181
		[Tooltip("Swipe Delta Threshold looks at the speed of input to decide if a swipe will be initiated (optional)")]
		public bool UseSwipeDeltaThreshold;

		// Token: 0x04000C6E RID: 3182
		[Tooltip("Offset for how far a swipe has to travel to initiate a page change (optional)")]
		public int FastSwipeThreshold = 100;

		// Token: 0x04000C6F RID: 3183
		[Tooltip("Speed at which the ScrollRect will keep scrolling before slowing down and stopping (optional)")]
		public int SwipeVelocityThreshold = 100;

		// Token: 0x04000C70 RID: 3184
		[Tooltip("Threshold for swipe speed to initiate a swipe, below threshold will return to closest page (optional)")]
		public float SwipeDeltaThreshold = 5f;

		// Token: 0x04000C71 RID: 3185
		[Tooltip("Use time scale instead of unscaled time (optional)")]
		public bool UseTimeScale = true;

		// Token: 0x04000C72 RID: 3186
		[Tooltip("The visible bounds area, controls which items are visible/enabled. *Note Should use a RectMask. (optional)")]
		public RectTransform MaskArea;

		// Token: 0x04000C73 RID: 3187
		[Tooltip("Pixel size to buffer around Mask Area. (optional)")]
		public float MaskBuffer = 1f;

		// Token: 0x04000C74 RID: 3188
		[Tooltip("By default the container will lerp to the start when enabled in the scene, this option overrides this and forces it to simply jump without lerping")]
		public bool JumpOnEnable;

		// Token: 0x04000C75 RID: 3189
		[Tooltip("By default the container will return to the original starting page when enabled, this option overrides this behaviour and stays on the current selection")]
		public bool RestartOnEnable;

		// Token: 0x04000C76 RID: 3190
		[Tooltip("(Experimental)\nBy default, child array objects will use the parent transform\nHowever you can disable this for some interesting effects")]
		public bool UseParentTransform = true;

		// Token: 0x04000C77 RID: 3191
		[Tooltip("Scroll Snap children. (optional)\nEither place objects in the scene as children OR\nPrefabs in this array, NOT BOTH")]
		public GameObject[] ChildObjects;

		// Token: 0x04000C78 RID: 3192
		[SerializeField]
		[Tooltip("Event fires when a user starts to change the selection")]
		private ScrollSnapBase.SelectionChangeStartEvent m_OnSelectionChangeStartEvent = new ScrollSnapBase.SelectionChangeStartEvent();

		// Token: 0x04000C79 RID: 3193
		[SerializeField]
		[Tooltip("Event fires as the page changes, while dragging or jumping")]
		private ScrollSnapBase.SelectionPageChangedEvent m_OnSelectionPageChangedEvent = new ScrollSnapBase.SelectionPageChangedEvent();

		// Token: 0x04000C7A RID: 3194
		[SerializeField]
		[Tooltip("Event fires when the page settles after a user has dragged")]
		private ScrollSnapBase.SelectionChangeEndEvent m_OnSelectionChangeEndEvent = new ScrollSnapBase.SelectionChangeEndEvent();

		// Token: 0x02001158 RID: 4440
		[Serializable]
		public class SelectionChangeStartEvent : UnityEvent
		{
		}

		// Token: 0x02001159 RID: 4441
		[Serializable]
		public class SelectionPageChangedEvent : UnityEvent<int>
		{
		}

		// Token: 0x0200115A RID: 4442
		[Serializable]
		public class SelectionChangeEndEvent : UnityEvent<int>
		{
		}
	}
}
