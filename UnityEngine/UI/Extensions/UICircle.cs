using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200031E RID: 798
	[AddComponentMenu("UI/Extensions/Primitives/UI Circle")]
	public class UICircle : UIPrimitiveBase
	{
		// Token: 0x06001253 RID: 4691 RVA: 0x00041F64 File Offset: 0x00040164
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			int num = this.ArcInvert ? -1 : 1;
			float num2 = ((base.rectTransform.rect.width < base.rectTransform.rect.height) ? base.rectTransform.rect.width : base.rectTransform.rect.height) - (float)this.Padding;
			float num3 = -base.rectTransform.pivot.x * num2;
			float num4 = -base.rectTransform.pivot.x * num2 + this.Thickness;
			vh.Clear();
			this.indices.Clear();
			this.vertices.Clear();
			int item = 0;
			int num5 = 1;
			float num6 = this.Arc * 360f / (float)this.ArcSteps;
			this._progress = (float)this.ArcSteps * this.Progress;
			float f = (float)num * 0.017453292f * (float)this.ArcRotation;
			float num7 = Mathf.Cos(f);
			float num8 = Mathf.Sin(f);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = ((this._progress > 0f) ? this.ProgressColor : this.color);
			simpleVert.position = new Vector2(num3 * num7, num3 * num8);
			simpleVert.uv0 = new Vector2(simpleVert.position.x / num2 + 0.5f, simpleVert.position.y / num2 + 0.5f);
			this.vertices.Add(simpleVert);
			Vector2 zero = new Vector2(num4 * num7, num4 * num8);
			if (this.Fill)
			{
				zero = Vector2.zero;
			}
			simpleVert.position = zero;
			simpleVert.uv0 = (this.Fill ? this.uvCenter : new Vector2(simpleVert.position.x / num2 + 0.5f, simpleVert.position.y / num2 + 0.5f));
			this.vertices.Add(simpleVert);
			for (int i = 1; i <= this.ArcSteps; i++)
			{
				float f2 = (float)num * 0.017453292f * ((float)i * num6 + (float)this.ArcRotation);
				num7 = Mathf.Cos(f2);
				num8 = Mathf.Sin(f2);
				simpleVert.color = (((float)i > this._progress) ? this.color : this.ProgressColor);
				simpleVert.position = new Vector2(num3 * num7, num3 * num8);
				simpleVert.uv0 = new Vector2(simpleVert.position.x / num2 + 0.5f, simpleVert.position.y / num2 + 0.5f);
				this.vertices.Add(simpleVert);
				if (!this.Fill)
				{
					simpleVert.position = new Vector2(num4 * num7, num4 * num8);
					simpleVert.uv0 = new Vector2(simpleVert.position.x / num2 + 0.5f, simpleVert.position.y / num2 + 0.5f);
					this.vertices.Add(simpleVert);
					int item2 = num5;
					this.indices.Add(item);
					this.indices.Add(num5 + 1);
					this.indices.Add(num5);
					num5++;
					item = num5;
					num5++;
					this.indices.Add(item);
					this.indices.Add(num5);
					this.indices.Add(item2);
				}
				else
				{
					this.indices.Add(item);
					this.indices.Add(num5 + 1);
					if ((float)i > this._progress)
					{
						this.indices.Add(this.ArcSteps + 2);
					}
					else
					{
						this.indices.Add(1);
					}
					num5++;
					item = num5;
				}
			}
			if (this.Fill)
			{
				simpleVert.position = zero;
				simpleVert.color = this.color;
				simpleVert.uv0 = this.uvCenter;
				this.vertices.Add(simpleVert);
			}
			vh.AddUIVertexStream(this.vertices, this.indices);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000423C7 File Offset: 0x000405C7
		public void SetProgress(float progress)
		{
			this.Progress = progress;
			this.SetVerticesDirty();
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x000423D6 File Offset: 0x000405D6
		public void SetArcSteps(int steps)
		{
			this.ArcSteps = steps;
			this.SetVerticesDirty();
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000423E5 File Offset: 0x000405E5
		public void SetInvertArc(bool invert)
		{
			this.ArcInvert = invert;
			this.SetVerticesDirty();
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000423F4 File Offset: 0x000405F4
		public void SetArcRotation(int rotation)
		{
			this.ArcRotation = rotation;
			this.SetVerticesDirty();
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00042403 File Offset: 0x00040603
		public void SetFill(bool fill)
		{
			this.Fill = fill;
			this.SetVerticesDirty();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00042412 File Offset: 0x00040612
		public void SetBaseColor(Color color)
		{
			this.color = color;
			this.SetVerticesDirty();
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00042424 File Offset: 0x00040624
		public void UpdateBaseAlpha(float value)
		{
			Color color = this.color;
			color.a = value;
			this.color = color;
			this.SetVerticesDirty();
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004244D File Offset: 0x0004064D
		public void SetProgressColor(Color color)
		{
			this.ProgressColor = color;
			this.SetVerticesDirty();
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0004245C File Offset: 0x0004065C
		public void UpdateProgressAlpha(float value)
		{
			this.ProgressColor.a = value;
			this.SetVerticesDirty();
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00042470 File Offset: 0x00040670
		public void SetPadding(int padding)
		{
			this.Padding = padding;
			this.SetVerticesDirty();
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004247F File Offset: 0x0004067F
		public void SetThickness(int thickness)
		{
			this.Thickness = (float)thickness;
			this.SetVerticesDirty();
		}

		// Token: 0x04000CA4 RID: 3236
		[Tooltip("The Arc Invert property will invert the construction of the Arc.")]
		public bool ArcInvert = true;

		// Token: 0x04000CA5 RID: 3237
		[Tooltip("The Arc property is a percentage of the entire circumference of the circle.")]
		[Range(0f, 1f)]
		public float Arc = 1f;

		// Token: 0x04000CA6 RID: 3238
		[Tooltip("The Arc Steps property defines the number of segments that the Arc will be divided into.")]
		[Range(0f, 1000f)]
		public int ArcSteps = 100;

		// Token: 0x04000CA7 RID: 3239
		[Tooltip("The Arc Rotation property permits adjusting the geometry orientation around the Z axis.")]
		[Range(0f, 360f)]
		public int ArcRotation;

		// Token: 0x04000CA8 RID: 3240
		[Tooltip("The Progress property allows the primitive to be used as a progression indicator.")]
		[Range(0f, 1f)]
		public float Progress;

		// Token: 0x04000CA9 RID: 3241
		private float _progress;

		// Token: 0x04000CAA RID: 3242
		public Color ProgressColor = new Color(255f, 255f, 255f, 255f);

		// Token: 0x04000CAB RID: 3243
		public bool Fill = true;

		// Token: 0x04000CAC RID: 3244
		public float Thickness = 5f;

		// Token: 0x04000CAD RID: 3245
		public int Padding;

		// Token: 0x04000CAE RID: 3246
		private List<int> indices = new List<int>();

		// Token: 0x04000CAF RID: 3247
		private List<UIVertex> vertices = new List<UIVertex>();

		// Token: 0x04000CB0 RID: 3248
		private Vector2 uvCenter = new Vector2(0.5f, 0.5f);
	}
}
