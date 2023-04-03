using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000315 RID: 789
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("Layout/Extensions/Tile Size Fitter")]
	public class TileSizeFitter : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x000407B3 File Offset: 0x0003E9B3
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x000407BB File Offset: 0x0003E9BB
		public Vector2 Border
		{
			get
			{
				return this.m_Border;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Vector2>(ref this.m_Border, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x000407D1 File Offset: 0x0003E9D1
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x000407D9 File Offset: 0x0003E9D9
		public Vector2 TileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Vector2>(ref this.m_TileSize, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000407EF File Offset: 0x0003E9EF
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00040811 File Offset: 0x0003EA11
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004081F File Offset: 0x0003EA1F
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0004083D File Offset: 0x0003EA3D
		protected override void OnRectTransformDimensionsChange()
		{
			this.UpdateRect();
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00040848 File Offset: 0x0003EA48
		private void UpdateRect()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_Tracker.Clear();
			this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY);
			this.rectTransform.anchorMin = Vector2.zero;
			this.rectTransform.anchorMax = Vector2.one;
			this.rectTransform.anchoredPosition = Vector2.zero;
			this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDelta);
			Vector2 vector = this.GetParentSize() - this.Border;
			if (this.TileSize.x > 0.001f)
			{
				vector.x -= Mathf.Floor(vector.x / this.TileSize.x) * this.TileSize.x;
			}
			else
			{
				vector.x = 0f;
			}
			if (this.TileSize.y > 0.001f)
			{
				vector.y -= Mathf.Floor(vector.y / this.TileSize.y) * this.TileSize.y;
			}
			else
			{
				vector.y = 0f;
			}
			this.rectTransform.sizeDelta = -vector;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00040988 File Offset: 0x0003EB88
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = this.rectTransform.parent as RectTransform;
			if (!rectTransform)
			{
				return Vector2.zero;
			}
			return rectTransform.rect.size;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000409C2 File Offset: 0x0003EBC2
		public virtual void SetLayoutHorizontal()
		{
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000409C4 File Offset: 0x0003EBC4
		public virtual void SetLayoutVertical()
		{
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000409C6 File Offset: 0x0003EBC6
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateRect();
		}

		// Token: 0x04000C83 RID: 3203
		[SerializeField]
		private Vector2 m_Border = Vector2.zero;

		// Token: 0x04000C84 RID: 3204
		[SerializeField]
		private Vector2 m_TileSize = Vector2.zero;

		// Token: 0x04000C85 RID: 3205
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000C86 RID: 3206
		private DrivenRectTransformTracker m_Tracker;
	}
}
