using System;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x0200035C RID: 860
	public struct HsvColor
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0004D2E0 File Offset: 0x0004B4E0
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x0004D2EF File Offset: 0x0004B4EF
		public float NormalizedH
		{
			get
			{
				return (float)this.H / 360f;
			}
			set
			{
				this.H = (double)value * 360.0;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0004D303 File Offset: 0x0004B503
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0004D30C File Offset: 0x0004B50C
		public float NormalizedS
		{
			get
			{
				return (float)this.S;
			}
			set
			{
				this.S = (double)value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0004D316 File Offset: 0x0004B516
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0004D31F File Offset: 0x0004B51F
		public float NormalizedV
		{
			get
			{
				return (float)this.V;
			}
			set
			{
				this.V = (double)value;
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0004D329 File Offset: 0x0004B529
		public HsvColor(double h, double s, double v)
		{
			this.H = h;
			this.S = s;
			this.V = v;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0004D340 File Offset: 0x0004B540
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{",
				this.H.ToString("f2"),
				",",
				this.S.ToString("f2"),
				",",
				this.V.ToString("f2"),
				"}"
			});
		}

		// Token: 0x04000E44 RID: 3652
		public double H;

		// Token: 0x04000E45 RID: 3653
		public double S;

		// Token: 0x04000E46 RID: 3654
		public double V;
	}
}
