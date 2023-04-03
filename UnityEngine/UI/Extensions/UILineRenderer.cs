using System;
using System.Collections.Generic;
using UnityEngine.Sprites;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000321 RID: 801
	[AddComponentMenu("UI/Extensions/Primitives/UILineRenderer")]
	[RequireComponent(typeof(RectTransform))]
	public class UILineRenderer : UIPrimitiveBase
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00042E11 File Offset: 0x00041011
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x00042E19 File Offset: 0x00041019
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

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00042E28 File Offset: 0x00041028
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x00042E30 File Offset: 0x00041030
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

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00042E3F File Offset: 0x0004103F
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x00042E47 File Offset: 0x00041047
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

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00042E56 File Offset: 0x00041056
		// (set) Token: 0x06001284 RID: 4740 RVA: 0x00042E5E File Offset: 0x0004105E
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

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00042E6D File Offset: 0x0004106D
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x00042E75 File Offset: 0x00041075
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00042E7E File Offset: 0x0004107E
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x00042E86 File Offset: 0x00041086
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

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00042E9F File Offset: 0x0004109F
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x00042EA7 File Offset: 0x000410A7
		public List<Vector2[]> Segments
		{
			get
			{
				return this.m_segments;
			}
			set
			{
				this.m_segments = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00042EB8 File Offset: 0x000410B8
		private void PopulateMesh(VertexHelper vh, Vector2[] pointsToDraw)
		{
			if (this.BezierMode != UILineRenderer.BezierType.None && this.BezierMode != UILineRenderer.BezierType.Catenary && pointsToDraw.Length > 3)
			{
				BezierPath bezierPath = new BezierPath();
				bezierPath.SetControlPoints(pointsToDraw);
				bezierPath.SegmentsPerCurve = this.bezierSegmentsPerCurve;
				UILineRenderer.BezierType bezierMode = this.BezierMode;
				List<Vector2> list;
				if (bezierMode != UILineRenderer.BezierType.Basic)
				{
					if (bezierMode != UILineRenderer.BezierType.Improved)
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
				pointsToDraw = list.ToArray();
			}
			if (this.BezierMode == UILineRenderer.BezierType.Catenary && pointsToDraw.Length == 2)
			{
				pointsToDraw = new CableCurve(pointsToDraw)
				{
					slack = base.Resolution,
					steps = this.BezierSegmentsPerCurve
				}.Points();
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
				for (int i = 1; i < pointsToDraw.Length; i += 2)
				{
					Vector2 vector = pointsToDraw[i - 1];
					Vector2 vector2 = pointsToDraw[i];
					vector = new Vector2(vector.x * num + num3, vector.y * num2 + num4);
					vector2 = new Vector2(vector2.x * num + num3, vector2.y * num2 + num4);
					if (this.lineCaps)
					{
						list2.Add(this.CreateLineCap(vector, vector2, UILineRenderer.SegmentType.Start));
					}
					list2.Add(this.CreateLineSegment(vector, vector2, UILineRenderer.SegmentType.Middle, null));
					if (this.lineCaps)
					{
						list2.Add(this.CreateLineCap(vector, vector2, UILineRenderer.SegmentType.End));
					}
				}
			}
			else
			{
				for (int j = 1; j < pointsToDraw.Length; j++)
				{
					Vector2 vector3 = pointsToDraw[j - 1];
					Vector2 vector4 = pointsToDraw[j];
					vector3 = new Vector2(vector3.x * num + num3, vector3.y * num2 + num4);
					vector4 = new Vector2(vector4.x * num + num3, vector4.y * num2 + num4);
					if (this.lineCaps && j == 1)
					{
						list2.Add(this.CreateLineCap(vector3, vector4, UILineRenderer.SegmentType.Start));
					}
					list2.Add(this.CreateLineSegment(vector3, vector4, UILineRenderer.SegmentType.Middle, null));
					if (this.lineCaps && j == pointsToDraw.Length - 1)
					{
						list2.Add(this.CreateLineCap(vector3, vector4, UILineRenderer.SegmentType.End));
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
					UILineRenderer.JoinType joinType = this.LineJoins;
					if (joinType == UILineRenderer.JoinType.Miter)
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
							joinType = UILineRenderer.JoinType.Bevel;
						}
					}
					if (joinType == UILineRenderer.JoinType.Bevel)
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

		// Token: 0x0600128C RID: 4748 RVA: 0x000434A8 File Offset: 0x000416A8
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (this.m_points != null && this.m_points.Length != 0)
			{
				this.GeneratedUVs();
				vh.Clear();
				this.PopulateMesh(vh, this.m_points);
				return;
			}
			if (this.m_segments != null && this.m_segments.Count > 0)
			{
				this.GeneratedUVs();
				vh.Clear();
				for (int i = 0; i < this.m_segments.Count; i++)
				{
					Vector2[] pointsToDraw = this.m_segments[i];
					this.PopulateMesh(vh, pointsToDraw);
				}
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00043530 File Offset: 0x00041730
		private UIVertex[] CreateLineCap(Vector2 start, Vector2 end, UILineRenderer.SegmentType type)
		{
			if (type == UILineRenderer.SegmentType.Start)
			{
				Vector2 start2 = start - (end - start).normalized * this.lineThickness / 2f;
				return this.CreateLineSegment(start2, start, UILineRenderer.SegmentType.Start, null);
			}
			if (type == UILineRenderer.SegmentType.End)
			{
				Vector2 end2 = end + (end - start).normalized * this.lineThickness / 2f;
				return this.CreateLineSegment(end, end2, UILineRenderer.SegmentType.End, null);
			}
			Debug.LogError("Bad SegmentType passed in to CreateLineCap. Must be SegmentType.Start or SegmentType.End");
			return null;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000435BC File Offset: 0x000417BC
		private UIVertex[] CreateLineSegment(Vector2 start, Vector2 end, UILineRenderer.SegmentType type, UIVertex[] previousVert = null)
		{
			Vector2 b = new Vector2(start.y - end.y, end.x - start.x).normalized * this.lineThickness / 2f;
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			if (previousVert != null)
			{
				vector = new Vector2(previousVert[3].position.x, previousVert[3].position.y);
				vector2 = new Vector2(previousVert[2].position.x, previousVert[2].position.y);
			}
			else
			{
				vector = start - b;
				vector2 = start + b;
			}
			Vector2 vector3 = end + b;
			Vector2 vector4 = end - b;
			switch (type)
			{
			case UILineRenderer.SegmentType.Start:
				return base.SetVbo(new Vector2[]
				{
					vector,
					vector2,
					vector3,
					vector4
				}, UILineRenderer.startUvs);
			case UILineRenderer.SegmentType.End:
				return base.SetVbo(new Vector2[]
				{
					vector,
					vector2,
					vector3,
					vector4
				}, UILineRenderer.endUvs);
			case UILineRenderer.SegmentType.Full:
				return base.SetVbo(new Vector2[]
				{
					vector,
					vector2,
					vector3,
					vector4
				}, UILineRenderer.fullUvs);
			}
			return base.SetVbo(new Vector2[]
			{
				vector,
				vector2,
				vector3,
				vector4
			}, UILineRenderer.middleUvs);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00043778 File Offset: 0x00041978
		protected override void GeneratedUVs()
		{
			if (base.activeSprite != null)
			{
				Vector4 outerUV = DataUtility.GetOuterUV(base.activeSprite);
				Vector4 innerUV = DataUtility.GetInnerUV(base.activeSprite);
				UILineRenderer.UV_TOP_LEFT = new Vector2(outerUV.x, outerUV.y);
				UILineRenderer.UV_BOTTOM_LEFT = new Vector2(outerUV.x, outerUV.w);
				UILineRenderer.UV_TOP_CENTER_LEFT = new Vector2(innerUV.x, innerUV.y);
				UILineRenderer.UV_TOP_CENTER_RIGHT = new Vector2(innerUV.z, innerUV.y);
				UILineRenderer.UV_BOTTOM_CENTER_LEFT = new Vector2(innerUV.x, innerUV.w);
				UILineRenderer.UV_BOTTOM_CENTER_RIGHT = new Vector2(innerUV.z, innerUV.w);
				UILineRenderer.UV_TOP_RIGHT = new Vector2(outerUV.z, outerUV.y);
				UILineRenderer.UV_BOTTOM_RIGHT = new Vector2(outerUV.z, outerUV.w);
			}
			else
			{
				UILineRenderer.UV_TOP_LEFT = Vector2.zero;
				UILineRenderer.UV_BOTTOM_LEFT = new Vector2(0f, 1f);
				UILineRenderer.UV_TOP_CENTER_LEFT = new Vector2(0.5f, 0f);
				UILineRenderer.UV_TOP_CENTER_RIGHT = new Vector2(0.5f, 0f);
				UILineRenderer.UV_BOTTOM_CENTER_LEFT = new Vector2(0.5f, 1f);
				UILineRenderer.UV_BOTTOM_CENTER_RIGHT = new Vector2(0.5f, 1f);
				UILineRenderer.UV_TOP_RIGHT = new Vector2(1f, 0f);
				UILineRenderer.UV_BOTTOM_RIGHT = Vector2.one;
			}
			UILineRenderer.startUvs = new Vector2[]
			{
				UILineRenderer.UV_TOP_LEFT,
				UILineRenderer.UV_BOTTOM_LEFT,
				UILineRenderer.UV_BOTTOM_CENTER_LEFT,
				UILineRenderer.UV_TOP_CENTER_LEFT
			};
			UILineRenderer.middleUvs = new Vector2[]
			{
				UILineRenderer.UV_TOP_CENTER_LEFT,
				UILineRenderer.UV_BOTTOM_CENTER_LEFT,
				UILineRenderer.UV_BOTTOM_CENTER_RIGHT,
				UILineRenderer.UV_TOP_CENTER_RIGHT
			};
			UILineRenderer.endUvs = new Vector2[]
			{
				UILineRenderer.UV_TOP_CENTER_RIGHT,
				UILineRenderer.UV_BOTTOM_CENTER_RIGHT,
				UILineRenderer.UV_BOTTOM_RIGHT,
				UILineRenderer.UV_TOP_RIGHT
			};
			UILineRenderer.fullUvs = new Vector2[]
			{
				UILineRenderer.UV_TOP_LEFT,
				UILineRenderer.UV_BOTTOM_LEFT,
				UILineRenderer.UV_BOTTOM_RIGHT,
				UILineRenderer.UV_TOP_RIGHT
			};
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000439DC File Offset: 0x00041BDC
		protected override void ResolutionToNativeSize(float distance)
		{
			if (base.UseNativeSize)
			{
				this.m_Resolution = distance / (base.activeSprite.rect.width / base.pixelsPerUnit);
				this.lineThickness = base.activeSprite.rect.height / base.pixelsPerUnit;
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00043A34 File Offset: 0x00041C34
		private int GetSegmentPointCount()
		{
			List<Vector2[]> segments = this.Segments;
			if (segments != null && segments.Count > 0)
			{
				int num = 0;
				foreach (Vector2[] array in this.Segments)
				{
					num += array.Length;
				}
				return num;
			}
			return this.Points.Length;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00043AAC File Offset: 0x00041CAC
		public Vector2 GetPosition(int index, int segmentIndex = 0)
		{
			if (segmentIndex > 0)
			{
				return this.Segments[segmentIndex - 1][index - 1];
			}
			if (this.Segments.Count > 0)
			{
				int num = 0;
				int num2 = index;
				foreach (Vector2[] array in this.Segments)
				{
					if (num2 - array.Length <= 0)
					{
						break;
					}
					num2 -= array.Length;
					num++;
				}
				return this.Segments[num][num2 - 1];
			}
			return this.Points[index - 1];
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00043B5C File Offset: 0x00041D5C
		public Vector2 GetPositionBySegment(int index, int segment)
		{
			return this.Segments[segment][index - 1];
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00043B74 File Offset: 0x00041D74
		public Vector2 GetClosestPoint(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			Vector2 lhs = p3 - p1;
			Vector2 a = p2 - p1;
			float d = Mathf.Clamp01(Vector2.Dot(lhs, a.normalized) / a.magnitude);
			return p1 + a * d;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00043BB7 File Offset: 0x00041DB7
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_points.Length == 0)
			{
				this.m_points = new Vector2[1];
			}
		}

		// Token: 0x04000CBD RID: 3261
		private const float MIN_MITER_JOIN = 0.2617994f;

		// Token: 0x04000CBE RID: 3262
		private const float MIN_BEVEL_NICE_JOIN = 0.5235988f;

		// Token: 0x04000CBF RID: 3263
		private static Vector2 UV_TOP_LEFT;

		// Token: 0x04000CC0 RID: 3264
		private static Vector2 UV_BOTTOM_LEFT;

		// Token: 0x04000CC1 RID: 3265
		private static Vector2 UV_TOP_CENTER_LEFT;

		// Token: 0x04000CC2 RID: 3266
		private static Vector2 UV_TOP_CENTER_RIGHT;

		// Token: 0x04000CC3 RID: 3267
		private static Vector2 UV_BOTTOM_CENTER_LEFT;

		// Token: 0x04000CC4 RID: 3268
		private static Vector2 UV_BOTTOM_CENTER_RIGHT;

		// Token: 0x04000CC5 RID: 3269
		private static Vector2 UV_TOP_RIGHT;

		// Token: 0x04000CC6 RID: 3270
		private static Vector2 UV_BOTTOM_RIGHT;

		// Token: 0x04000CC7 RID: 3271
		private static Vector2[] startUvs;

		// Token: 0x04000CC8 RID: 3272
		private static Vector2[] middleUvs;

		// Token: 0x04000CC9 RID: 3273
		private static Vector2[] endUvs;

		// Token: 0x04000CCA RID: 3274
		private static Vector2[] fullUvs;

		// Token: 0x04000CCB RID: 3275
		[SerializeField]
		[Tooltip("Points to draw lines between\n Can be improved using the Resolution Option")]
		internal Vector2[] m_points;

		// Token: 0x04000CCC RID: 3276
		[SerializeField]
		[Tooltip("Segments to be drawn\n This is a list of arrays of points")]
		internal List<Vector2[]> m_segments;

		// Token: 0x04000CCD RID: 3277
		[SerializeField]
		[Tooltip("Thickness of the line")]
		internal float lineThickness = 2f;

		// Token: 0x04000CCE RID: 3278
		[SerializeField]
		[Tooltip("Use the relative bounds of the Rect Transform (0,0 -> 0,1) or screen space coordinates")]
		internal bool relativeSize;

		// Token: 0x04000CCF RID: 3279
		[SerializeField]
		[Tooltip("Do the points identify a single line or split pairs of lines")]
		internal bool lineList;

		// Token: 0x04000CD0 RID: 3280
		[SerializeField]
		[Tooltip("Add end caps to each line\nMultiple caps when used with Line List")]
		internal bool lineCaps;

		// Token: 0x04000CD1 RID: 3281
		[SerializeField]
		[Tooltip("Resolution of the Bezier curve, different to line Resolution")]
		internal int bezierSegmentsPerCurve = 10;

		// Token: 0x04000CD2 RID: 3282
		[Tooltip("The type of Join used between lines, Square/Mitre or Curved/Bevel")]
		public UILineRenderer.JoinType LineJoins;

		// Token: 0x04000CD3 RID: 3283
		[Tooltip("Bezier method to apply to line, see docs for options\nCan't be used in conjunction with Resolution as Bezier already changes the resolution")]
		public UILineRenderer.BezierType BezierMode;

		// Token: 0x04000CD4 RID: 3284
		[HideInInspector]
		public bool drivenExternally;

		// Token: 0x0200115E RID: 4446
		private enum SegmentType
		{
			// Token: 0x04009221 RID: 37409
			Start,
			// Token: 0x04009222 RID: 37410
			Middle,
			// Token: 0x04009223 RID: 37411
			End,
			// Token: 0x04009224 RID: 37412
			Full
		}

		// Token: 0x0200115F RID: 4447
		public enum JoinType
		{
			// Token: 0x04009226 RID: 37414
			Bevel,
			// Token: 0x04009227 RID: 37415
			Miter
		}

		// Token: 0x02001160 RID: 4448
		public enum BezierType
		{
			// Token: 0x04009229 RID: 37417
			None,
			// Token: 0x0400922A RID: 37418
			Quick,
			// Token: 0x0400922B RID: 37419
			Basic,
			// Token: 0x0400922C RID: 37420
			Improved,
			// Token: 0x0400922D RID: 37421
			Catenary
		}
	}
}
