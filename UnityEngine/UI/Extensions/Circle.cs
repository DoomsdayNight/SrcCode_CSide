using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000333 RID: 819
	public class Circle
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00048665 File Offset: 0x00046865
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x0004866D File Offset: 0x0004686D
		public float X
		{
			get
			{
				return this.xAxis;
			}
			set
			{
				this.xAxis = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00048676 File Offset: 0x00046876
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x0004867E File Offset: 0x0004687E
		public float Y
		{
			get
			{
				return this.yAxis;
			}
			set
			{
				this.yAxis = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00048687 File Offset: 0x00046887
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x0004868F File Offset: 0x0004688F
		public int Steps
		{
			get
			{
				return this.steps;
			}
			set
			{
				this.steps = value;
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00048698 File Offset: 0x00046898
		public Circle(float radius)
		{
			this.xAxis = radius;
			this.yAxis = radius;
			this.steps = 1;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000486B5 File Offset: 0x000468B5
		public Circle(float radius, int steps)
		{
			this.xAxis = radius;
			this.yAxis = radius;
			this.steps = steps;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000486D2 File Offset: 0x000468D2
		public Circle(float xAxis, float yAxis)
		{
			this.xAxis = xAxis;
			this.yAxis = yAxis;
			this.steps = 10;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000486F0 File Offset: 0x000468F0
		public Circle(float xAxis, float yAxis, int steps)
		{
			this.xAxis = xAxis;
			this.yAxis = yAxis;
			this.steps = steps;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00048710 File Offset: 0x00046910
		public Vector2 Evaluate(float t)
		{
			float num = 360f / (float)this.steps;
			float f = 0.017453292f * num * t;
			float x = Mathf.Sin(f) * this.xAxis;
			float y = Mathf.Cos(f) * this.yAxis;
			return new Vector2(x, y);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00048758 File Offset: 0x00046958
		public void Evaluate(float t, out Vector2 eval)
		{
			float num = 360f / (float)this.steps;
			float f = 0.017453292f * num * t;
			eval.x = Mathf.Sin(f) * this.xAxis;
			eval.y = Mathf.Cos(f) * this.yAxis;
		}

		// Token: 0x04000D63 RID: 3427
		[SerializeField]
		private float xAxis;

		// Token: 0x04000D64 RID: 3428
		[SerializeField]
		private float yAxis;

		// Token: 0x04000D65 RID: 3429
		[SerializeField]
		private int steps;
	}
}
