using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200034C RID: 844
	[AddComponentMenu("UI/Extensions/UI Scrollrect Occlusion")]
	public class UI_ScrollRectOcclusion : MonoBehaviour
	{
		// Token: 0x060013F3 RID: 5107 RVA: 0x0004AE5F File Offset: 0x0004905F
		private void Awake()
		{
			if (this.InitByUser)
			{
				return;
			}
			this.Init();
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0004AE70 File Offset: 0x00049070
		public void Init()
		{
			if (this._initialised)
			{
				Debug.LogError("Control already initialized\nYou have to enable the InitByUser setting on the control in order to use Init() when running");
				return;
			}
			if (base.GetComponent<ScrollRect>() != null)
			{
				this._initialised = true;
				this._scrollRect = base.GetComponent<ScrollRect>();
				this._scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScroll));
				this._isHorizontal = this._scrollRect.horizontal;
				this._isVertical = this._scrollRect.vertical;
				for (int i = 0; i < this._scrollRect.content.childCount; i++)
				{
					this._items.Add(this._scrollRect.content.GetChild(i).GetComponent<RectTransform>());
				}
				if (this._scrollRect.content.GetComponent<VerticalLayoutGroup>() != null)
				{
					this._verticalLayoutGroup = this._scrollRect.content.GetComponent<VerticalLayoutGroup>();
				}
				if (this._scrollRect.content.GetComponent<HorizontalLayoutGroup>() != null)
				{
					this._horizontalLayoutGroup = this._scrollRect.content.GetComponent<HorizontalLayoutGroup>();
				}
				if (this._scrollRect.content.GetComponent<GridLayoutGroup>() != null)
				{
					this._gridLayoutGroup = this._scrollRect.content.GetComponent<GridLayoutGroup>();
				}
				if (this._scrollRect.content.GetComponent<ContentSizeFitter>() != null)
				{
					this._contentSizeFitter = this._scrollRect.content.GetComponent<ContentSizeFitter>();
					return;
				}
			}
			else
			{
				Debug.LogError("UI_ScrollRectOcclusion => No ScrollRect component found");
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0004AFF4 File Offset: 0x000491F4
		private void ToggleGridComponents(bool toggle)
		{
			if (this._isVertical)
			{
				this._disableMarginY = this._scrollRect.GetComponent<RectTransform>().rect.height / 2f + this._items[0].sizeDelta.y;
			}
			if (this._isHorizontal)
			{
				this._disableMarginX = this._scrollRect.GetComponent<RectTransform>().rect.width / 2f + this._items[0].sizeDelta.x;
			}
			if (this._verticalLayoutGroup)
			{
				this._verticalLayoutGroup.enabled = toggle;
			}
			if (this._horizontalLayoutGroup)
			{
				this._horizontalLayoutGroup.enabled = toggle;
			}
			if (this._contentSizeFitter)
			{
				this._contentSizeFitter.enabled = toggle;
			}
			if (this._gridLayoutGroup)
			{
				this._gridLayoutGroup.enabled = toggle;
			}
			this._hasDisabledGridComponents = !toggle;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0004B0F8 File Offset: 0x000492F8
		public void OnScroll(Vector2 pos)
		{
			if (this._reset)
			{
				return;
			}
			if (!this._hasDisabledGridComponents)
			{
				this.ToggleGridComponents(false);
			}
			for (int i = 0; i < this._items.Count; i++)
			{
				if (this._isVertical && this._isHorizontal)
				{
					if (this._scrollRect.transform.InverseTransformPoint(this._items[i].position).y < -this._disableMarginY || this._scrollRect.transform.InverseTransformPoint(this._items[i].position).y > this._disableMarginY || this._scrollRect.transform.InverseTransformPoint(this._items[i].position).x < -this._disableMarginX || this._scrollRect.transform.InverseTransformPoint(this._items[i].position).x > this._disableMarginX)
					{
						this._items[i].gameObject.SetActive(false);
					}
					else
					{
						this._items[i].gameObject.SetActive(true);
					}
				}
				else
				{
					if (this._isVertical)
					{
						if (this._scrollRect.transform.InverseTransformPoint(this._items[i].position).y < -this._disableMarginY || this._scrollRect.transform.InverseTransformPoint(this._items[i].position).y > this._disableMarginY)
						{
							this._items[i].gameObject.SetActive(false);
						}
						else
						{
							this._items[i].gameObject.SetActive(true);
						}
					}
					if (this._isHorizontal)
					{
						if (this._scrollRect.transform.InverseTransformPoint(this._items[i].position).x < -this._disableMarginX || this._scrollRect.transform.InverseTransformPoint(this._items[i].position).x > this._disableMarginX)
						{
							this._items[i].gameObject.SetActive(false);
						}
						else
						{
							this._items[i].gameObject.SetActive(true);
						}
					}
				}
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0004B374 File Offset: 0x00049574
		public void SetDirty()
		{
			this._reset = true;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004B380 File Offset: 0x00049580
		private void LateUpdate()
		{
			if (this._reset)
			{
				this._reset = false;
				this._items.Clear();
				for (int i = 0; i < this._scrollRect.content.childCount; i++)
				{
					this._items.Add(this._scrollRect.content.GetChild(i).GetComponent<RectTransform>());
					this._items[i].gameObject.SetActive(true);
				}
				this.ToggleGridComponents(true);
			}
		}

		// Token: 0x04000DD4 RID: 3540
		public bool InitByUser;

		// Token: 0x04000DD5 RID: 3541
		private bool _initialised;

		// Token: 0x04000DD6 RID: 3542
		private ScrollRect _scrollRect;

		// Token: 0x04000DD7 RID: 3543
		private ContentSizeFitter _contentSizeFitter;

		// Token: 0x04000DD8 RID: 3544
		private VerticalLayoutGroup _verticalLayoutGroup;

		// Token: 0x04000DD9 RID: 3545
		private HorizontalLayoutGroup _horizontalLayoutGroup;

		// Token: 0x04000DDA RID: 3546
		private GridLayoutGroup _gridLayoutGroup;

		// Token: 0x04000DDB RID: 3547
		private bool _isVertical;

		// Token: 0x04000DDC RID: 3548
		private bool _isHorizontal;

		// Token: 0x04000DDD RID: 3549
		private float _disableMarginX;

		// Token: 0x04000DDE RID: 3550
		private float _disableMarginY;

		// Token: 0x04000DDF RID: 3551
		private bool _hasDisabledGridComponents;

		// Token: 0x04000DE0 RID: 3552
		private List<RectTransform> _items = new List<RectTransform>();

		// Token: 0x04000DE1 RID: 3553
		private bool _reset;
	}
}
