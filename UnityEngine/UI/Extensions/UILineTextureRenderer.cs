using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000323 RID: 803
	[AddComponentMenu("UI/Extensions/Primitives/UILineTextureRenderer")]
	public class UILineTextureRenderer : UIPrimitiveBase
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x000447AA File Offset: 0x000429AA
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x000447B2 File Offset: 0x000429B2
		public Rect uvRect
		{
			get
			{
				return this.m_UVRect;
			}
			set
			{
				if (this.m_UVRect == value)
				{
					return;
				}
				this.m_UVRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x000447D0 File Offset: 0x000429D0
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x000447D8 File Offset: 0x000429D8
		public Vector2[] Points
		{
			get
			{
				return this.m_points;
			}
			set
			{
				if (this.m_points == value)
				{
					return;
				}
				this.m_points = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000447F4 File Offset: 0x000429F4
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (this.m_points == null || this.m_points.Length < 2)
			{
				this.m_points = new Vector2[]
				{
					new Vector2(0f, 0f),
					new Vector2(1f, 1f)
				};
			}
			int num = 24;
			float num2 = base.rectTransform.rect.width;
			float num3 = base.rectTransform.rect.height;
			float num4 = -base.rectTransform.pivot.x * base.rectTransform.rect.width;
			float num5 = -base.rectTransform.pivot.y * base.rectTransform.rect.height;
			if (!this.relativeSize)
			{
				num2 = 1f;
				num3 = 1f;
			}
			List<Vector2> list = new List<Vector2>();
			list.Add(this.m_points[0]);
			Vector2 item = this.m_points[0] + (this.m_points[1] - this.m_points[0]).normalized * (float)num;
			list.Add(item);
			for (int i = 1; i < this.m_points.Length - 1; i++)
			{
				list.Add(this.m_points[i]);
			}
			item = this.m_points[this.m_points.Length - 1] - (this.m_points[this.m_points.Length - 1] - this.m_points[this.m_points.Length - 2]).normalized * (float)num;
			list.Add(item);
			list.Add(this.m_points[this.m_points.Length - 1]);
			Vector2[] array = list.ToArray();
			if (this.UseMargins)
			{
				num2 -= this.Margin.x;
				num3 -= this.Margin.y;
				num4 += this.Margin.x / 2f;
				num5 += this.Margin.y / 2f;
			}
			vh.Clear();
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			for (int j = 1; j < array.Length; j++)
			{
				Vector2 vector3 = array[j - 1];
				Vector2 vector4 = array[j];
				vector3 = new Vector2(vector3.x * num2 + num4, vector3.y * num3 + num5);
				vector4 = new Vector2(vector4.x * num2 + num4, vector4.y * num3 + num5);
				float z = Mathf.Atan2(vector4.y - vector3.y, vector4.x - vector3.x) * 180f / 3.1415927f;
				Vector2 vector5 = vector3 + new Vector2(0f, -this.LineThickness / 2f);
				Vector2 vector6 = vector3 + new Vector2(0f, this.LineThickness / 2f);
				Vector2 vector7 = vector4 + new Vector2(0f, this.LineThickness / 2f);
				Vector2 vector8 = vector4 + new Vector2(0f, -this.LineThickness / 2f);
				vector5 = this.RotatePointAroundPivot(vector5, vector3, new Vector3(0f, 0f, z));
				vector6 = this.RotatePointAroundPivot(vector6, vector3, new Vector3(0f, 0f, z));
				vector7 = this.RotatePointAroundPivot(vector7, vector4, new Vector3(0f, 0f, z));
				vector8 = this.RotatePointAroundPivot(vector8, vector4, new Vector3(0f, 0f, z));
				Vector2 zero = Vector2.zero;
				Vector2 vector9 = new Vector2(0f, 1f);
				Vector2 vector10 = new Vector2(0.5f, 0f);
				Vector2 vector11 = new Vector2(0.5f, 1f);
				Vector2 vector12 = new Vector2(1f, 0f);
				Vector2 vector13 = new Vector2(1f, 1f);
				Vector2[] uvs = new Vector2[]
				{
					vector10,
					vector11,
					vector11,
					vector10
				};
				if (j > 1)
				{
					vh.AddUIVertexQuad(base.SetVbo(new Vector2[]
					{
						vector,
						vector2,
						vector5,
						vector6
					}, uvs));
				}
				if (j == 1)
				{
					uvs = new Vector2[]
					{
						zero,
						vector9,
						vector11,
						vector10
					};
				}
				else if (j == array.Length - 1)
				{
					uvs = new Vector2[]
					{
						vector10,
						vector11,
						vector13,
						vector12
					};
				}
				vh.AddUIVertexQuad(base.SetVbo(new Vector2[]
				{
					vector5,
					vector6,
					vector7,
					vector8
				}, uvs));
				vector = vector7;
				vector2 = vector8;
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00044D94 File Offset: 0x00042F94
		public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
		{
			Vector3 vector = point - pivot;
			vector = Quaternion.Euler(angles) * vector;
			point = vector + pivot;
			return point;
		}

		// Token: 0x04000CEC RID: 3308
		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000CED RID: 3309
		[SerializeField]
		private Vector2[] m_points;

		// Token: 0x04000CEE RID: 3310
		public float LineThickness = 2f;

		// Token: 0x04000CEF RID: 3311
		public bool UseMargins;

		// Token: 0x04000CF0 RID: 3312
		public Vector2 Margin;

		// Token: 0x04000CF1 RID: 3313
		public bool relativeSize;
	}
}
