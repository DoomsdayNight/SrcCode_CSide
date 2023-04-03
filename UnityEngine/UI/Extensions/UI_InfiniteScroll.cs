using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200034A RID: 842
	[AddComponentMenu("UI/Extensions/UI Infinite Scroll")]
	public class UI_InfiniteScroll : MonoBehaviour
	{
		// Token: 0x060013DD RID: 5085 RVA: 0x0004A1D9 File Offset: 0x000483D9
		protected virtual void Awake()
		{
			if (!this.InitByUser)
			{
				this.Init();
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0004A1EC File Offset: 0x000483EC
		public virtual void SetNewItems(ref List<Transform> newItems)
		{
			if (this._scrollRect != null)
			{
				if (this._scrollRect.content == null && newItems == null)
				{
					return;
				}
				if (this.items != null)
				{
					this.items.Clear();
				}
				for (int i = this._scrollRect.content.childCount - 1; i >= 0; i--)
				{
					Transform child = this._scrollRect.content.GetChild(i);
					child.SetParent(null);
					Object.DestroyImmediate(child.gameObject);
				}
				foreach (Transform transform in newItems)
				{
					transform.SetParent(this._scrollRect.content);
				}
				this.SetItems();
			}
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004A2C4 File Offset: 0x000484C4
		private void SetItems()
		{
			for (int i = 0; i < this._scrollRect.content.childCount; i++)
			{
				this.items.Add(this._scrollRect.content.GetChild(i).GetComponent<RectTransform>());
			}
			this._itemCount = this._scrollRect.content.childCount;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004A324 File Offset: 0x00048524
		public void Init()
		{
			if (base.GetComponent<ScrollRect>() != null)
			{
				this._scrollRect = base.GetComponent<ScrollRect>();
				this._scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScroll));
				this._scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
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
				}
				this._isHorizontal = this._scrollRect.horizontal;
				this._isVertical = this._scrollRect.vertical;
				if (this._isHorizontal && this._isVertical)
				{
					Debug.LogError("UI_InfiniteScroll doesn't support scrolling in both directions, please choose one direction (horizontal or vertical)");
				}
				this.SetItems();
				return;
			}
			Debug.LogError("UI_InfiniteScroll => No ScrollRect component found");
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004A47C File Offset: 0x0004867C
		private void DisableGridComponents()
		{
			if (this._isVertical)
			{
				this._recordOffsetY = this.items[1].GetComponent<RectTransform>().anchoredPosition.y - this.items[0].GetComponent<RectTransform>().anchoredPosition.y;
				if (this._recordOffsetY < 0f)
				{
					this._recordOffsetY *= -1f;
				}
				this._disableMarginY = this._recordOffsetY * (float)this._itemCount / 2f;
			}
			if (this._isHorizontal)
			{
				this._recordOffsetX = this.items[1].GetComponent<RectTransform>().anchoredPosition.x - this.items[0].GetComponent<RectTransform>().anchoredPosition.x;
				if (this._recordOffsetX < 0f)
				{
					this._recordOffsetX *= -1f;
				}
				this._disableMarginX = this._recordOffsetX * (float)this._itemCount / 2f;
			}
			if (this._verticalLayoutGroup)
			{
				this._verticalLayoutGroup.enabled = false;
			}
			if (this._horizontalLayoutGroup)
			{
				this._horizontalLayoutGroup.enabled = false;
			}
			if (this._contentSizeFitter)
			{
				this._contentSizeFitter.enabled = false;
			}
			if (this._gridLayoutGroup)
			{
				this._gridLayoutGroup.enabled = false;
			}
			this._hasDisabledGridComponents = true;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004A5F0 File Offset: 0x000487F0
		public void OnScroll(Vector2 pos)
		{
			if (!this._hasDisabledGridComponents)
			{
				this.DisableGridComponents();
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if (this._isHorizontal)
				{
					if (this._scrollRect.transform.InverseTransformPoint(this.items[i].gameObject.transform.position).x > this._disableMarginX + this._threshold)
					{
						this._newAnchoredPosition = this.items[i].anchoredPosition;
						this._newAnchoredPosition.x = this._newAnchoredPosition.x - (float)this._itemCount * this._recordOffsetX;
						this.items[i].anchoredPosition = this._newAnchoredPosition;
						this._scrollRect.content.GetChild(this._itemCount - 1).transform.SetAsFirstSibling();
					}
					else if (this._scrollRect.transform.InverseTransformPoint(this.items[i].gameObject.transform.position).x < -this._disableMarginX)
					{
						this._newAnchoredPosition = this.items[i].anchoredPosition;
						this._newAnchoredPosition.x = this._newAnchoredPosition.x + (float)this._itemCount * this._recordOffsetX;
						this.items[i].anchoredPosition = this._newAnchoredPosition;
						this._scrollRect.content.GetChild(0).transform.SetAsLastSibling();
					}
				}
				if (this._isVertical)
				{
					if (this._scrollRect.transform.InverseTransformPoint(this.items[i].gameObject.transform.position).y > this._disableMarginY + this._threshold)
					{
						this._newAnchoredPosition = this.items[i].anchoredPosition;
						this._newAnchoredPosition.y = this._newAnchoredPosition.y - (float)this._itemCount * this._recordOffsetY;
						this.items[i].anchoredPosition = this._newAnchoredPosition;
						this._scrollRect.content.GetChild(this._itemCount - 1).transform.SetAsFirstSibling();
					}
					else if (this._scrollRect.transform.InverseTransformPoint(this.items[i].gameObject.transform.position).y < -this._disableMarginY)
					{
						this._newAnchoredPosition = this.items[i].anchoredPosition;
						this._newAnchoredPosition.y = this._newAnchoredPosition.y + (float)this._itemCount * this._recordOffsetY;
						this.items[i].anchoredPosition = this._newAnchoredPosition;
						this._scrollRect.content.GetChild(0).transform.SetAsLastSibling();
					}
				}
			}
		}

		// Token: 0x04000DB2 RID: 3506
		[Tooltip("If false, will Init automatically, otherwise you need to call Init() method")]
		public bool InitByUser;

		// Token: 0x04000DB3 RID: 3507
		protected ScrollRect _scrollRect;

		// Token: 0x04000DB4 RID: 3508
		private ContentSizeFitter _contentSizeFitter;

		// Token: 0x04000DB5 RID: 3509
		private VerticalLayoutGroup _verticalLayoutGroup;

		// Token: 0x04000DB6 RID: 3510
		private HorizontalLayoutGroup _horizontalLayoutGroup;

		// Token: 0x04000DB7 RID: 3511
		private GridLayoutGroup _gridLayoutGroup;

		// Token: 0x04000DB8 RID: 3512
		protected bool _isVertical;

		// Token: 0x04000DB9 RID: 3513
		protected bool _isHorizontal;

		// Token: 0x04000DBA RID: 3514
		private float _disableMarginX;

		// Token: 0x04000DBB RID: 3515
		private float _disableMarginY;

		// Token: 0x04000DBC RID: 3516
		private bool _hasDisabledGridComponents;

		// Token: 0x04000DBD RID: 3517
		protected List<RectTransform> items = new List<RectTransform>();

		// Token: 0x04000DBE RID: 3518
		private Vector2 _newAnchoredPosition = Vector2.zero;

		// Token: 0x04000DBF RID: 3519
		private float _threshold = 100f;

		// Token: 0x04000DC0 RID: 3520
		private int _itemCount;

		// Token: 0x04000DC1 RID: 3521
		private float _recordOffsetX;

		// Token: 0x04000DC2 RID: 3522
		private float _recordOffsetY;
	}
}
