using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002D4 RID: 724
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Image))]
	[AddComponentMenu("UI/Effects/Extensions/Curly UI Image")]
	public class CUIImage : CUIGraphic
	{
		// Token: 0x06000FE5 RID: 4069 RVA: 0x000340F1 File Offset: 0x000322F1
		public static int ImageTypeCornerRefVertexIdx(Image.Type _type)
		{
			if (_type == Image.Type.Sliced)
			{
				return CUIImage.SlicedImageCornerRefVertexIdx;
			}
			return CUIImage.FilledImageCornerRefVertexIdx;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00034102 File Offset: 0x00032302
		public Vector2 OriCornerPosRatio
		{
			get
			{
				return this.oriCornerPosRatio;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0003410A File Offset: 0x0003230A
		public Image UIImage
		{
			get
			{
				return (Image)this.uiGraphic;
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00034117 File Offset: 0x00032317
		public override void ReportSet()
		{
			if (this.uiGraphic == null)
			{
				this.uiGraphic = base.GetComponent<Image>();
			}
			base.ReportSet();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003413C File Offset: 0x0003233C
		protected override void modifyVertices(List<UIVertex> _verts)
		{
			if (!this.IsActive())
			{
				return;
			}
			if (this.UIImage.type == Image.Type.Filled)
			{
				Debug.LogWarning("Might not work well Radial Filled at the moment!");
			}
			else if (this.UIImage.type == Image.Type.Sliced || this.UIImage.type == Image.Type.Tiled)
			{
				if (this.cornerPosRatio == Vector2.one * -1f)
				{
					this.cornerPosRatio = _verts[CUIImage.ImageTypeCornerRefVertexIdx(this.UIImage.type)].position;
					this.cornerPosRatio.x = (this.cornerPosRatio.x + this.rectTrans.pivot.x * this.rectTrans.rect.width) / this.rectTrans.rect.width;
					this.cornerPosRatio.y = (this.cornerPosRatio.y + this.rectTrans.pivot.y * this.rectTrans.rect.height) / this.rectTrans.rect.height;
					this.oriCornerPosRatio = this.cornerPosRatio;
				}
				if (this.cornerPosRatio.x < 0f)
				{
					this.cornerPosRatio.x = 0f;
				}
				if (this.cornerPosRatio.x >= 0.5f)
				{
					this.cornerPosRatio.x = 0.5f;
				}
				if (this.cornerPosRatio.y < 0f)
				{
					this.cornerPosRatio.y = 0f;
				}
				if (this.cornerPosRatio.y >= 0.5f)
				{
					this.cornerPosRatio.y = 0.5f;
				}
				for (int i = 0; i < _verts.Count; i++)
				{
					UIVertex uivertex = _verts[i];
					float num = (uivertex.position.x + this.rectTrans.rect.width * this.rectTrans.pivot.x) / this.rectTrans.rect.width;
					float num2 = (uivertex.position.y + this.rectTrans.rect.height * this.rectTrans.pivot.y) / this.rectTrans.rect.height;
					if (num < this.oriCornerPosRatio.x)
					{
						num = Mathf.Lerp(0f, this.cornerPosRatio.x, num / this.oriCornerPosRatio.x);
					}
					else if (num > 1f - this.oriCornerPosRatio.x)
					{
						num = Mathf.Lerp(1f - this.cornerPosRatio.x, 1f, (num - (1f - this.oriCornerPosRatio.x)) / this.oriCornerPosRatio.x);
					}
					else
					{
						num = Mathf.Lerp(this.cornerPosRatio.x, 1f - this.cornerPosRatio.x, (num - this.oriCornerPosRatio.x) / (1f - this.oriCornerPosRatio.x * 2f));
					}
					if (num2 < this.oriCornerPosRatio.y)
					{
						num2 = Mathf.Lerp(0f, this.cornerPosRatio.y, num2 / this.oriCornerPosRatio.y);
					}
					else if (num2 > 1f - this.oriCornerPosRatio.y)
					{
						num2 = Mathf.Lerp(1f - this.cornerPosRatio.y, 1f, (num2 - (1f - this.oriCornerPosRatio.y)) / this.oriCornerPosRatio.y);
					}
					else
					{
						num2 = Mathf.Lerp(this.cornerPosRatio.y, 1f - this.cornerPosRatio.y, (num2 - this.oriCornerPosRatio.y) / (1f - this.oriCornerPosRatio.y * 2f));
					}
					uivertex.position.x = num * this.rectTrans.rect.width - this.rectTrans.rect.width * this.rectTrans.pivot.x;
					uivertex.position.y = num2 * this.rectTrans.rect.height - this.rectTrans.rect.height * this.rectTrans.pivot.y;
					_verts[i] = uivertex;
				}
			}
			base.modifyVertices(_verts);
		}

		// Token: 0x04000B10 RID: 2832
		public static int SlicedImageCornerRefVertexIdx = 2;

		// Token: 0x04000B11 RID: 2833
		public static int FilledImageCornerRefVertexIdx = 0;

		// Token: 0x04000B12 RID: 2834
		[Tooltip("For changing the size of the corner for tiled or sliced Image")]
		[HideInInspector]
		[SerializeField]
		public Vector2 cornerPosRatio = Vector2.one * -1f;

		// Token: 0x04000B13 RID: 2835
		[HideInInspector]
		[SerializeField]
		protected Vector2 oriCornerPosRatio = Vector2.one * -1f;
	}
}
