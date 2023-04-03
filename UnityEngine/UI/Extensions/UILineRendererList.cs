using System;
using System.Collections.Generic;
using UnityEngine.Sprites;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000322 RID: 802
	[AddComponentMenu("UI/Extensions/Primitives/UILineRendererList")]
	[RequireComponent(typeof(RectTransform))]
	public class UILineRendererList : UIPrimitiveBase
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00043BEF File Offset: 0x00041DEF
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x00043BF7 File Offset: 0x00041DF7
		public float LineThickness
		{
			get
			{
				return this.lineThickness;
			}
			set
			{
				this.lineThickness = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x00043C06 File Offset: 0x00041E06
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x00043C0E File Offset: 0x00041E0E
		public bool RelativeSize
		{
			get
			{
				return this.relativeSize;
			}
			set
			{
				this.relativeSize = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00043C1D File Offset: 0x00041E1D
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x00043C25 File Offset: 0x00041E25
		public bool LineList
		{
			get
			{
				return this.lineList;
			}
			set
			{
				this.lineList = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00043C34 File Offset: 0x00041E34
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x00043C3C File Offset: 0x00041E3C
		public bool LineCaps
		{
			get
			{
				return this.lineCaps;
			}
			set
			{
				this.lineCaps = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00043C4B File Offset: 0x00041E4B
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x00043C53 File Offset: 0x00041E53
		public int BezierSegmentsPerCurve
		{
			get
			{
				return this.bezierSegmentsPerCurve;
			}
			set
			{
				this.bezierSegmentsPerCurve = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00043C5C File Offset: 0x00041E5C
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x00043C64 File Offset: 0x00041E64
		public List<Vector2> Points
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

		// Token: 0x060012A3 RID: 4771 RVA: 0x00043C7D File Offset: 0x00041E7D
		public void AddPoint(Vector2 pointToAdd)
		{
			this.m_points.Add(pointToAdd);
			this.SetAllDirty();
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00043C91 File Offset: 0x00041E91
		public void RemovePoint(Vector2 pointToRemove)
		{
			this.m_points.Remove(pointToRemove);
			this.SetAllDirty();
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00043CA6 File Offset: 0x00041EA6
		public void ClearPoints()
		{
			this.m_points.Clear();
			this.SetAllDirty();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00043CBC File Offset: 0x00041EBC
		private void PopulateMesh(VertexHelper vh, List<Vector2> pointsToDraw)
		{
			if (this.BezierMode != UILineRendererList.BezierType.None && this.BezierMode != UILineRendererList.BezierType.Catenary && pointsToDraw.Count > 3)
			{
				BezierPath bezierPath = new BezierPath();
				bezierPath.SetControlPoints(pointsToDraw);
				bezierPath.SegmentsPerCurve = this.bezierSegmentsPerCurve;
				UILineRendererList.BezierType bezierMode = this.BezierMode;
				List<Vector2> list;
				if (bezierMode != UILineRendererList.BezierType.Basic)
				{
					if (bezierMode != UILineRendererList.BezierType.Improved)
					{
						list = bezierPath.GetDrawingPoints2();
					}
					else
					{
						list = bezierPath.GetDrawingPoints1();
					}
				}
				else
				{
					list = bezierPath.GetDrawingPoints0();
				}
				pointsToDraw = list;
			}
			if (this.BezierMode == UILineRendererList.BezierType.Catenary && pointsToDraw.Count == 2)
			{
				CableCurve cableCurve = new CableCurve(pointsToDraw);
				cableCurve.slack = base.Resolution;
				cableCurve.steps = this.BezierSegmentsPerCurve;
				pointsToDraw.Clear();
				pointsToDraw.AddRange(cableCurve.Points());
			}
			if (base.ImproveResolution != ResolutionMode.None)
			{
				pointsToDraw = base.IncreaseResolution(pointsToDraw);
			}
			float num = (!this.relativeSize) ? 1f : base.rectTransform.rect.width;
			float num2 = (!this.relativeSize) ? 1f : base.rectTransform.rect.height;
			float num3 = -base.rectTransform.pivot.x * num;
			float num4 = -base.rectTransform.pivot.y * num2;
			List<UIVertex[]> list2 = new List<UIVertex[]>();
			if (this.lineList)
			{
				for (int i = 1; i < pointsToDraw.Count; i += 2)
				{
					Vector2 vector = pointsToDraw[i - 1];
					Vector2 vector2 = pointsToDraw[i];
					vector = new Vector2(vector.x * num + num3, vector.y * num2 + num4);
					vector2 = new Vector2(vector2.x * num + num3, vector2.y * num2 + num4);
					if (this.lineCaps)
					{
						list2.Add(this.CreateLineCap(vector, vector2, UILineRendererList.SegmentType.Start));
					}
					list2.Add(this.CreateLineSegment(vector, vector2, UILineRendererList.SegmentType.Middle));
					if (this.lineCaps)
					{
						list2.Add(this.CreateLineCap(vector, vector2, UILineRendererList.SegmentType.End));
					}
				}
			}
			else
			{
				for (int j = 1; j < pointsToDraw.Count; j++)
				{
					Vector2 vector3 = pointsToDraw[j - 1];
					Vector2 vector4 = pointsToDraw[j];
					vector3 = new Vector2(vector3.x * num + num3, vector3.y * num2 + num4);
					vector4 = new Vector2(vector4.x * num + num3, vector4.y * num2 + num4);
					if (this.lineCaps && j == 1)
					{
						list2.Add(this.CreateLineCap(vector3, vector4, UILineRendererList.SegmentType.Start));
					}
					list2.Add(this.CreateLineSegment(vector3, vector4, UILineRendererList.SegmentType.Middle));
					if (this.lineCaps && j == pointsToDraw.Count - 1)
					{
						list2.Add(this.CreateLineCap(vector3, vector4, UILineRendererList.SegmentType.End));
					}
				}
			}
			for (int k = 0; k < list2.Count; k++)
			{
				if (!this.lineList && k < list2.Count - 1)
				{
					Vector3 v = list2[k][1].position - list2[k][2].position;
					Vector3 v2 = list2[k + 1][2].position - list2[k + 1][1].position;
					float num5 = Vector2.Angle(v, v2) * 0.017453292f;
					float num6 = Mathf.Sign(Vector3.Cross(v.normalized, v2.normalized).z);
					float num7 = this.lineThickness / (2f * Mathf.Tan(num5 / 2f));
					Vector3 position = list2[k][2].position - v.normalized * num7 * num6;
					Vector3 position2 = list2[k][3].position + v.normalized * num7 * num6;
					UILineRendererList.JoinType joinType = this.LineJoins;
					if (joinType == UILineRendererList.JoinType.Miter)
					{
						if (num7 < v.magnitude / 2f && num7 < v2.magnitude / 2f && num5 > 0.2617994f)
						{
							list2[k][2].position = position;
							list2[k][3].position = position2;
							list2[k + 1][0].position = position2;
							list2[k + 1][1].position = position;
						}
						else
						{
							joinType = UILineRendererList.JoinType.Bevel;
						}
					}
					if (joinType == UILineRendererList.JoinType.Bevel)
					{
						if (num7 < v.magnitude / 2f && num7 < v2.magnitude / 2f && num5 > 0.5235988f)
						{
							if (num6 < 0f)
							{
								list2[k][2].position = position;
								list2[k + 1][1].position = position;
							}
							else
							{
								list2[k][3].position = position2;
								list2[k + 1][0].position = position2;
							}
						}
						UIVertex[] verts = new UIVertex[]
						{
							list2[k][2],
							list2[k][3],
							list2[k + 1][0],
							list2[k + 1][1]
						};
						vh.AddUIVertexQuad(verts);
					}
				}
				vh.AddUIVertexQuad(list2[k]);
			}
			if (vh.currentVertCount > 64000)
			{
				Debug.LogError("Max Verticies size is 64000, current mesh verticies count is [" + vh.currentVertCount.ToString() + "] - Cannot Draw");
				vh.Clear();
				return;
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000442C1 File Offset: 0x000424C1
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (this.m_points != null && this.m_points.Count > 0)
			{
				this.GeneratedUVs();
				vh.Clear();
				this.PopulateMesh(vh, this.m_points);
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000442F4 File Offset: 0x000424F4
		private UIVertex[] CreateLineCap(Vector2 start, Vector2 end, UILineRendererList.SegmentType type)
		{
			if (type == UILineRendererList.SegmentType.Start)
			{
				Vector2 start2 = start - (end - start).normalized * this.lineThickness / 2f;
				return this.CreateLineSegment(start2, start, UILineRendererList.SegmentType.Start);
			}
			if (type == UILineRendererList.SegmentType.End)
			{
				Vector2 end2 = end + (end - start).normalized * this.lineThickness / 2f;
				return this.CreateLineSegment(end, end2, UILineRendererList.SegmentType.End);
			}
			Debug.LogError("Bad SegmentType passed in to CreateLineCap. Must be SegmentType.Start or SegmentType.End");
			return null;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00044380 File Offset: 0x00042580
		private UIVertex[] CreateLineSegment(Vector2 start, Vector2 end, UILineRendererList.SegmentType type)
		{
			Vector2 b = new Vector2(start.y - end.y, end.x - start.x).normalized * this.lineThickness / 2f;
			Vector2 vector = start - b;
			Vector2 vector2 = start + b;
			Vector2 vector3 = end + b;
			Vector2 vector4 = end - b;
			switch (type)
			{
			case UILineRendererList.SegmentType.Start:
				return base.SetVbo(new Vector2[]
				{
					vector,
					vector2,
					vector3,
					vector4
				}, UILineRendererList.startUvs);
			case UILineRendererList.SegmentType.End:
				return base.SetVbo(new Vector2[]
				{
					vector,
					vector2,
					vector3,
					vector4
				}, UILineRendererList.endUvs);
			case UILineRendererList.SegmentType.Full:
				return base.SetVbo(new Vector2[]
				{
					vector,
					vector2,
					vector3,
					vector4
				}, UILineRendererList.fullUvs);
			}
			return base.SetVbo(new Vector2[]
			{
				vector,
				vector2,
				vector3,
				vector4
			}, UILineRendererList.middleUvs);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000444D4 File Offset: 0x000426D4
		protected override void GeneratedUVs()
		{
			if (base.activeSprite != null)
			{
				Vector4 outerUV = DataUtility.GetOuterUV(base.activeSprite);
				Vector4 innerUV = DataUtility.GetInnerUV(base.activeSprite);
				UILineRendererList.UV_TOP_LEFT = new Vector2(outerUV.x, outerUV.y);
				UILineRendererList.UV_BOTTOM_LEFT = new Vector2(outerUV.x, outerUV.w);
				UILineRendererList.UV_TOP_CENTER_LEFT = new Vector2(innerUV.x, innerUV.y);
				UILineRendererList.UV_TOP_CENTER_RIGHT = new Vector2(innerUV.z, innerUV.y);
				UILineRendererList.UV_BOTTOM_CENTER_LEFT = new Vector2(innerUV.x, innerUV.w);
				UILineRendererList.UV_BOTTOM_CENTER_RIGHT = new Vector2(innerUV.z, innerUV.w);
				UILineRendererList.UV_TOP_RIGHT = new Vector2(outerUV.z, outerUV.y);
				UILineRendererList.UV_BOTTOM_RIGHT = new Vector2(outerUV.z, outerUV.w);
			}
			else
			{
				UILineRendererList.UV_TOP_LEFT = Vector2.zero;
				UILineRendererList.UV_BOTTOM_LEFT = new Vector2(0f, 1f);
				UILineRendererList.UV_TOP_CENTER_LEFT = new Vector2(0.5f, 0f);
				UILineRendererList.UV_TOP_CENTER_RIGHT = new Vector2(0.5f, 0f);
				UILineRendererList.UV_BOTTOM_CENTER_LEFT = new Vector2(0.5f, 1f);
				UILineRendererList.UV_BOTTOM_CENTER_RIGHT = new Vector2(0.5f, 1f);
				UILineRendererList.UV_TOP_RIGHT = new Vector2(1f, 0f);
				UILineRendererList.UV_BOTTOM_RIGHT = Vector2.one;
			}
			UILineRendererList.startUvs = new Vector2[]
			{
				UILineRendererList.UV_TOP_LEFT,
				UILineRendererList.UV_BOTTOM_LEFT,
				UILineRendererList.UV_BOTTOM_CENTER_LEFT,
				UILineRendererList.UV_TOP_CENTER_LEFT
			};
			UILineRendererList.middleUvs = new Vector2[]
			{
				UILineRendererList.UV_TOP_CENTER_LEFT,
				UILineRendererList.UV_BOTTOM_CENTER_LEFT,
				UILineRendererList.UV_BOTTOM_CENTER_RIGHT,
				UILineRendererList.UV_TOP_CENTER_RIGHT
			};
			UILineRendererList.endUvs = new Vector2[]
			{
				UILineRendererList.UV_TOP_CENTER_RIGHT,
				UILineRendererList.UV_BOTTOM_CENTER_RIGHT,
				UILineRendererList.UV_BOTTOM_RIGHT,
				UILineRendererList.UV_TOP_RIGHT
			};
			UILineRendererList.fullUvs = new Vector2[]
			{
				UILineRendererList.UV_TOP_LEFT,
				UILineRendererList.UV_BOTTOM_LEFT,
				UILineRendererList.UV_BOTTOM_RIGHT,
				UILineRendererList.UV_TOP_RIGHT
			};
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00044738 File Offset: 0x00042938
		protected override void ResolutionToNativeSize(float distance)
		{
			if (base.UseNativeSize)
			{
				this.m_Resolution = distance / (base.activeSprite.rect.width / base.pixelsPerUnit);
				this.lineThickness = base.activeSprite.rect.height / base.pixelsPerUnit;
			}
		}

		// Token: 0x04000CD5 RID: 3285
		private const float MIN_MITER_JOIN = 0.2617994f;

		// Token: 0x04000CD6 RID: 3286
		private const float MIN_BEVEL_NICE_JOIN = 0.5235988f;

		// Token: 0x04000CD7 RID: 3287
		private static Vector2 UV_TOP_LEFT;

		// Token: 0x04000CD8 RID: 3288
		private static Vector2 UV_BOTTOM_LEFT;

		// Token: 0x04000CD9 RID: 3289
		private static Vector2 UV_TOP_CENTER_LEFT;

		// Token: 0x04000CDA RID: 3290
		private static Vector2 UV_TOP_CENTER_RIGHT;

		// Token: 0x04000CDB RID: 3291
		private static Vector2 UV_BOTTOM_CENTER_LEFT;

		// Token: 0x04000CDC RID: 3292
		private static Vector2 UV_BOTTOM_CENTER_RIGHT;

		// Token: 0x04000CDD RID: 3293
		private static Vector2 UV_TOP_RIGHT;

		// Token: 0x04000CDE RID: 3294
		private static Vector2 UV_BOTTOM_RIGHT;

		// Token: 0x04000CDF RID: 3295
		private static Vector2[] startUvs;

		// Token: 0x04000CE0 RID: 3296
		private static Vector2[] middleUvs;

		// Token: 0x04000CE1 RID: 3297
		private static Vector2[] endUvs;

		// Token: 0x04000CE2 RID: 3298
		private static Vector2[] fullUvs;

		// Token: 0x04000CE3 RID: 3299
		[SerializeField]
		[Tooltip("Points to draw lines between\n Can be improved using the Resolution Option")]
		internal List<Vector2> m_points;

		// Token: 0x04000CE4 RID: 3300
		[SerializeField]
		[Tooltip("Thickness of the line")]
		internal float lineThickness = 2f;

		// Token: 0x04000CE5 RID: 3301
		[SerializeField]
		[Tooltip("Use the relative bounds of the Rect Transform (0,0 -> 0,1) or screen space coordinates")]
		internal bool relativeSize;

		// Token: 0x04000CE6 RID: 3302
		[SerializeField]
		[Tooltip("Do the points identify a single line or split pairs of lines")]
		internal bool lineList;

		// Token: 0x04000CE7 RID: 3303
		[SerializeField]
		[Tooltip("Add end caps to each line\nMultiple caps when used with Line List")]
		internal bool lineCaps;

		// Token: 0x04000CE8 RID: 3304
		[SerializeField]
		[Tooltip("Resolution of the Bezier curve, different to line Resolution")]
		internal int bezierSegmentsPerCurve = 10;

		// Token: 0x04000CE9 RID: 3305
		[Tooltip("The type of Join used between lines, Square/Mitre or Curved/Bevel")]
		public UILineRendererList.JoinType LineJoins;

		// Token: 0x04000CEA RID: 3306
		[Tooltip("Bezier method to apply to line, see docs for options\nCan't be used in conjunction with Resolution as Bezier already changes the resolution")]
		public UILineRendererList.BezierType BezierMode;

		// Token: 0x04000CEB RID: 3307
		[HideInInspector]
		public bool drivenExternally;

		// Token: 0x02001161 RID: 4449
		private enum SegmentType
		{
			// Token: 0x0400922F RID: 37423
			Start,
			// Token: 0x04009230 RID: 37424
			Middle,
			// Token: 0x04009231 RID: 37425
			End,
			// Token: 0x04009232 RID: 37426
			Full
		}

		// Token: 0x02001162 RID: 4450
		public enum JoinType
		{
			// Token: 0x04009234 RID: 37428
			Bevel,
			// Token: 0x04009235 RID: 37429
			Miter
		}

		// Token: 0x02001163 RID: 4451
		public enum BezierType
		{
			// Token: 0x04009237 RID: 37431
			None,
			// Token: 0x04009238 RID: 37432
			Quick,
			// Token: 0x04009239 RID: 37433
			Basic,
			// Token: 0x0400923A RID: 37434
			Improved,
			// Token: 0x0400923B RID: 37435
			Catenary
		}
	}
}
