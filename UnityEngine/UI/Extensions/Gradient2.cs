﻿using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002DC RID: 732
	[AddComponentMenu("UI/Effects/Extensions/Gradient2")]
	public class Gradient2 : BaseMeshEffect
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00034D22 File Offset: 0x00032F22
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x00034D2A File Offset: 0x00032F2A
		public Gradient2.Blend BlendMode
		{
			get
			{
				return this._blendMode;
			}
			set
			{
				this._blendMode = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00034D3E File Offset: 0x00032F3E
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x00034D46 File Offset: 0x00032F46
		public Gradient EffectGradient
		{
			get
			{
				return this._effectGradient;
			}
			set
			{
				this._effectGradient = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00034D5A File Offset: 0x00032F5A
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x00034D62 File Offset: 0x00032F62
		public Gradient2.Type GradientType
		{
			get
			{
				return this._gradientType;
			}
			set
			{
				this._gradientType = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00034D76 File Offset: 0x00032F76
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x00034D7E File Offset: 0x00032F7E
		public bool ModifyVertices
		{
			get
			{
				return this._modifyVertices;
			}
			set
			{
				this._modifyVertices = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00034D92 File Offset: 0x00032F92
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x00034D9A File Offset: 0x00032F9A
		public float Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = Mathf.Clamp(value, -1f, 1f);
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00034DBD File Offset: 0x00032FBD
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x00034DC5 File Offset: 0x00032FC5
		public float Zoom
		{
			get
			{
				return this._zoom;
			}
			set
			{
				this._zoom = Mathf.Clamp(value, 0.1f, 10f);
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00034DE8 File Offset: 0x00032FE8
		public override void ModifyMesh(VertexHelper helper)
		{
			if (!this.IsActive() || helper.currentVertCount == 0)
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			helper.GetUIVertexStream(list);
			int count = list.Count;
			switch (this.GradientType)
			{
			case Gradient2.Type.Horizontal:
			case Gradient2.Type.Vertical:
			{
				Rect bounds = this.GetBounds(list);
				float num = bounds.xMin;
				float num2 = bounds.width;
				Func<UIVertex, float> func = (UIVertex v) => v.position.x;
				if (this.GradientType == Gradient2.Type.Vertical)
				{
					num = bounds.yMin;
					num2 = bounds.height;
					func = ((UIVertex v) => v.position.y);
				}
				float num3 = (num2 == 0f) ? 0f : (1f / num2 / this.Zoom);
				float num4 = (1f - 1f / this.Zoom) * 0.5f;
				float num5 = this.Offset * (1f - num4) - num4;
				if (this.ModifyVertices)
				{
					this.SplitTrianglesAtGradientStops(list, bounds, num4, helper);
				}
				UIVertex uivertex = default(UIVertex);
				for (int i = 0; i < helper.currentVertCount; i++)
				{
					helper.PopulateUIVertex(ref uivertex, i);
					uivertex.color = this.BlendColor(uivertex.color, this.EffectGradient.Evaluate((func(uivertex) - num) * num3 - num5));
					helper.SetUIVertex(uivertex, i);
				}
				return;
			}
			case Gradient2.Type.Radial:
			{
				Rect bounds2 = this.GetBounds(list);
				float num6 = (bounds2.width == 0f) ? 0f : (1f / bounds2.width / this.Zoom);
				float num7 = (bounds2.height == 0f) ? 0f : (1f / bounds2.height / this.Zoom);
				if (this.ModifyVertices)
				{
					helper.Clear();
					float d = bounds2.width / 2f;
					float d2 = bounds2.height / 2f;
					UIVertex v3 = default(UIVertex);
					v3.position = Vector3.right * bounds2.center.x + Vector3.up * bounds2.center.y + Vector3.forward * list[0].position.z;
					v3.normal = list[0].normal;
					v3.uv0 = new Vector2(0.5f, 0.5f);
					v3.color = Color.white;
					int num8 = 64;
					for (int j = 0; j < num8; j++)
					{
						UIVertex v2 = default(UIVertex);
						float num9 = (float)j * 360f / (float)num8;
						float num10 = Mathf.Cos(0.017453292f * num9);
						float num11 = Mathf.Sin(0.017453292f * num9);
						v2.position = Vector3.right * num10 * d + Vector3.up * num11 * d2 + Vector3.forward * list[0].position.z;
						v2.normal = list[0].normal;
						v2.uv0 = new Vector2((num10 + 1f) * 0.5f, (num11 + 1f) * 0.5f);
						v2.color = Color.white;
						helper.AddVert(v2);
					}
					helper.AddVert(v3);
					for (int k = 1; k < num8; k++)
					{
						helper.AddTriangle(k - 1, k, num8);
					}
					helper.AddTriangle(0, num8 - 1, num8);
				}
				UIVertex uivertex2 = default(UIVertex);
				for (int l = 0; l < helper.currentVertCount; l++)
				{
					helper.PopulateUIVertex(ref uivertex2, l);
					uivertex2.color = this.BlendColor(uivertex2.color, this.EffectGradient.Evaluate(Mathf.Sqrt(Mathf.Pow(Mathf.Abs(uivertex2.position.x - bounds2.center.x) * num6, 2f) + Mathf.Pow(Mathf.Abs(uivertex2.position.y - bounds2.center.y) * num7, 2f)) * 2f - this.Offset));
					helper.SetUIVertex(uivertex2, l);
				}
				return;
			}
			case Gradient2.Type.Diamond:
			{
				Rect bounds3 = this.GetBounds(list);
				float num12 = (bounds3.height == 0f) ? 0f : (1f / bounds3.height / this.Zoom);
				float d3 = bounds3.center.y / 2f;
				Vector3 vector = (Vector3.right + Vector3.up) * d3 + Vector3.forward * list[0].position.z;
				if (this.ModifyVertices)
				{
					helper.Clear();
					for (int m = 0; m < count; m++)
					{
						helper.AddVert(list[m]);
					}
					helper.AddVert(new UIVertex
					{
						position = vector,
						normal = list[0].normal,
						uv0 = new Vector2(0.5f, 0.5f),
						color = Color.white
					});
					for (int n = 1; n < count; n++)
					{
						helper.AddTriangle(n - 1, n, count);
					}
					helper.AddTriangle(0, count - 1, count);
				}
				UIVertex uivertex3 = default(UIVertex);
				for (int num13 = 0; num13 < helper.currentVertCount; num13++)
				{
					helper.PopulateUIVertex(ref uivertex3, num13);
					uivertex3.color = this.BlendColor(uivertex3.color, this.EffectGradient.Evaluate(Vector3.Distance(uivertex3.position, vector) * num12 - this.Offset));
					helper.SetUIVertex(uivertex3, num13);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00035448 File Offset: 0x00033648
		private Rect GetBounds(List<UIVertex> vertices)
		{
			float num = vertices[0].position.x;
			float num2 = num;
			float num3 = vertices[0].position.y;
			float num4 = num3;
			for (int i = vertices.Count - 1; i >= 1; i--)
			{
				float x = vertices[i].position.x;
				float y = vertices[i].position.y;
				if (x > num2)
				{
					num2 = x;
				}
				else if (x < num)
				{
					num = x;
				}
				if (y > num4)
				{
					num4 = y;
				}
				else if (y < num3)
				{
					num3 = y;
				}
			}
			return new Rect(num, num3, num2 - num, num4 - num3);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000354F0 File Offset: 0x000336F0
		private void SplitTrianglesAtGradientStops(List<UIVertex> _vertexList, Rect bounds, float zoomOffset, VertexHelper helper)
		{
			List<float> list = this.FindStops(zoomOffset, bounds);
			if (list.Count > 0)
			{
				helper.Clear();
				int count = _vertexList.Count;
				for (int i = 0; i < count; i += 3)
				{
					float[] positions = this.GetPositions(_vertexList, i);
					List<int> list2 = new List<int>(3);
					List<UIVertex> list3 = new List<UIVertex>(3);
					List<UIVertex> list4 = new List<UIVertex>(2);
					for (int j = 0; j < list.Count; j++)
					{
						int currentVertCount = helper.currentVertCount;
						bool flag = list4.Count > 0;
						bool flag2 = false;
						for (int k = 0; k < 3; k++)
						{
							if (!list2.Contains(k) && positions[k] < list[j])
							{
								int num = (k + 1) % 3;
								UIVertex item = _vertexList[k + i];
								if (positions[num] > list[j])
								{
									list2.Insert(0, k);
									list3.Insert(0, item);
									flag2 = true;
								}
								else
								{
									list2.Add(k);
									list3.Add(item);
								}
							}
						}
						if (list2.Count != 0)
						{
							if (list2.Count == 3)
							{
								break;
							}
							foreach (UIVertex v in list3)
							{
								helper.AddVert(v);
							}
							list4.Clear();
							foreach (int num2 in list2)
							{
								int num3 = (num2 + 1) % 3;
								if (positions[num3] < list[j])
								{
									num3 = (num3 + 1) % 3;
								}
								list4.Add(this.CreateSplitVertex(_vertexList[num2 + i], _vertexList[num3 + i], list[j]));
							}
							if (list4.Count == 1)
							{
								int num4 = (list2[0] + 2) % 3;
								list4.Add(this.CreateSplitVertex(_vertexList[list2[0] + i], _vertexList[num4 + i], list[j]));
							}
							foreach (UIVertex v2 in list4)
							{
								helper.AddVert(v2);
							}
							if (flag)
							{
								helper.AddTriangle(currentVertCount - 2, currentVertCount, currentVertCount + 1);
								helper.AddTriangle(currentVertCount - 2, currentVertCount + 1, currentVertCount - 1);
								if (list3.Count > 0)
								{
									if (flag2)
									{
										helper.AddTriangle(currentVertCount - 2, currentVertCount + 3, currentVertCount);
									}
									else
									{
										helper.AddTriangle(currentVertCount + 1, currentVertCount + 3, currentVertCount - 1);
									}
								}
							}
							else
							{
								int currentVertCount2 = helper.currentVertCount;
								helper.AddTriangle(currentVertCount, currentVertCount2 - 2, currentVertCount2 - 1);
								if (list3.Count > 1)
								{
									helper.AddTriangle(currentVertCount, currentVertCount2 - 1, currentVertCount + 1);
								}
							}
							list3.Clear();
						}
					}
					if (list4.Count > 0)
					{
						if (list3.Count == 0)
						{
							for (int l = 0; l < 3; l++)
							{
								if (!list2.Contains(l) && positions[l] > list[list.Count - 1])
								{
									int num5 = (l + 1) % 3;
									UIVertex item2 = _vertexList[l + i];
									if (positions[num5] > list[list.Count - 1])
									{
										list3.Insert(0, item2);
									}
									else
									{
										list3.Add(item2);
									}
								}
							}
						}
						foreach (UIVertex v3 in list3)
						{
							helper.AddVert(v3);
						}
						int currentVertCount3 = helper.currentVertCount;
						if (list3.Count > 1)
						{
							helper.AddTriangle(currentVertCount3 - 4, currentVertCount3 - 2, currentVertCount3 - 1);
							helper.AddTriangle(currentVertCount3 - 4, currentVertCount3 - 1, currentVertCount3 - 3);
						}
						else if (list3.Count > 0)
						{
							helper.AddTriangle(currentVertCount3 - 3, currentVertCount3 - 1, currentVertCount3 - 2);
						}
					}
					else
					{
						helper.AddVert(_vertexList[i]);
						helper.AddVert(_vertexList[i + 1]);
						helper.AddVert(_vertexList[i + 2]);
						int currentVertCount4 = helper.currentVertCount;
						helper.AddTriangle(currentVertCount4 - 3, currentVertCount4 - 2, currentVertCount4 - 1);
					}
				}
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00035970 File Offset: 0x00033B70
		private float[] GetPositions(List<UIVertex> _vertexList, int index)
		{
			float[] array = new float[3];
			if (this.GradientType == Gradient2.Type.Horizontal)
			{
				array[0] = _vertexList[index].position.x;
				array[1] = _vertexList[index + 1].position.x;
				array[2] = _vertexList[index + 2].position.x;
			}
			else
			{
				array[0] = _vertexList[index].position.y;
				array[1] = _vertexList[index + 1].position.y;
				array[2] = _vertexList[index + 2].position.y;
			}
			return array;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00035A10 File Offset: 0x00033C10
		private List<float> FindStops(float zoomOffset, Rect bounds)
		{
			List<float> list = new List<float>();
			float num = this.Offset * (1f - zoomOffset);
			float num2 = zoomOffset - num;
			float num3 = 1f - zoomOffset - num;
			foreach (GradientColorKey gradientColorKey in this.EffectGradient.colorKeys)
			{
				if (gradientColorKey.time >= num3)
				{
					break;
				}
				if (gradientColorKey.time > num2)
				{
					list.Add((gradientColorKey.time - num2) * this.Zoom);
				}
			}
			foreach (GradientAlphaKey gradientAlphaKey in this.EffectGradient.alphaKeys)
			{
				if (gradientAlphaKey.time >= num3)
				{
					break;
				}
				if (gradientAlphaKey.time > num2)
				{
					list.Add((gradientAlphaKey.time - num2) * this.Zoom);
				}
			}
			float num4 = bounds.xMin;
			float num5 = bounds.width;
			if (this.GradientType == Gradient2.Type.Vertical)
			{
				num4 = bounds.yMin;
				num5 = bounds.height;
			}
			list.Sort();
			for (int j = 0; j < list.Count; j++)
			{
				list[j] = list[j] * num5 + num4;
				if (j > 0 && Math.Abs(list[j] - list[j - 1]) < 2f)
				{
					list.RemoveAt(j);
					j--;
				}
			}
			return list;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00035B7C File Offset: 0x00033D7C
		private UIVertex CreateSplitVertex(UIVertex vertex1, UIVertex vertex2, float stop)
		{
			if (this.GradientType == Gradient2.Type.Horizontal)
			{
				float num = vertex1.position.x - stop;
				float num2 = vertex1.position.x - vertex2.position.x;
				float num3 = vertex1.position.y - vertex2.position.y;
				float num4 = vertex1.uv0.x - vertex2.uv0.x;
				float num5 = vertex1.uv0.y - vertex2.uv0.y;
				float num6 = num / num2;
				float y = vertex1.position.y - num3 * num6;
				return new UIVertex
				{
					position = new Vector3(stop, y, vertex1.position.z),
					normal = vertex1.normal,
					uv0 = new Vector2(vertex1.uv0.x - num4 * num6, vertex1.uv0.y - num5 * num6),
					color = Color.white
				};
			}
			float num7 = vertex1.position.y - stop;
			float num8 = vertex1.position.y - vertex2.position.y;
			float num9 = vertex1.position.x - vertex2.position.x;
			float num10 = vertex1.uv0.x - vertex2.uv0.x;
			float num11 = vertex1.uv0.y - vertex2.uv0.y;
			float num12 = num7 / num8;
			float x = vertex1.position.x - num9 * num12;
			return new UIVertex
			{
				position = new Vector3(x, stop, vertex1.position.z),
				normal = vertex1.normal,
				uv0 = new Vector2(vertex1.uv0.x - num10 * num12, vertex1.uv0.y - num11 * num12),
				color = Color.white
			};
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00035D8C File Offset: 0x00033F8C
		private Color BlendColor(Color colorA, Color colorB)
		{
			Gradient2.Blend blendMode = this.BlendMode;
			if (blendMode == Gradient2.Blend.Add)
			{
				return colorA + colorB;
			}
			if (blendMode != Gradient2.Blend.Multiply)
			{
				return colorB;
			}
			return colorA * colorB;
		}

		// Token: 0x04000B27 RID: 2855
		[SerializeField]
		private Gradient2.Type _gradientType;

		// Token: 0x04000B28 RID: 2856
		[SerializeField]
		private Gradient2.Blend _blendMode = Gradient2.Blend.Multiply;

		// Token: 0x04000B29 RID: 2857
		[SerializeField]
		[Tooltip("Add vertices to display complex gradients. Turn off if your shape is already very complex, like text.")]
		private bool _modifyVertices = true;

		// Token: 0x04000B2A RID: 2858
		[SerializeField]
		[Range(-1f, 1f)]
		private float _offset;

		// Token: 0x04000B2B RID: 2859
		[SerializeField]
		[Range(0.1f, 10f)]
		private float _zoom = 1f;

		// Token: 0x04000B2C RID: 2860
		[SerializeField]
		private Gradient _effectGradient = new Gradient
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(Color.black, 0f),
				new GradientColorKey(Color.white, 1f)
			}
		};

		// Token: 0x02001141 RID: 4417
		public enum Type
		{
			// Token: 0x040091D9 RID: 37337
			Horizontal,
			// Token: 0x040091DA RID: 37338
			Vertical,
			// Token: 0x040091DB RID: 37339
			Radial,
			// Token: 0x040091DC RID: 37340
			Diamond
		}

		// Token: 0x02001142 RID: 4418
		public enum Blend
		{
			// Token: 0x040091DE RID: 37342
			Override,
			// Token: 0x040091DF RID: 37343
			Add,
			// Token: 0x040091E0 RID: 37344
			Multiply
		}
	}
}
