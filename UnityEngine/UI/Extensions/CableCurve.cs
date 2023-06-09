﻿using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000332 RID: 818
	[Serializable]
	public class CableCurve
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x00048125 File Offset: 0x00046325
		// (set) Token: 0x06001335 RID: 4917 RVA: 0x0004812D File Offset: 0x0004632D
		public bool regenPoints
		{
			get
			{
				return this.m_regen;
			}
			set
			{
				this.m_regen = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00048136 File Offset: 0x00046336
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x0004813E File Offset: 0x0004633E
		public Vector2 start
		{
			get
			{
				return this.m_start;
			}
			set
			{
				if (value != this.m_start)
				{
					this.m_regen = true;
				}
				this.m_start = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0004815C File Offset: 0x0004635C
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x00048164 File Offset: 0x00046364
		public Vector2 end
		{
			get
			{
				return this.m_end;
			}
			set
			{
				if (value != this.m_end)
				{
					this.m_regen = true;
				}
				this.m_end = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00048182 File Offset: 0x00046382
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x0004818A File Offset: 0x0004638A
		public float slack
		{
			get
			{
				return this.m_slack;
			}
			set
			{
				if (value != this.m_slack)
				{
					this.m_regen = true;
				}
				this.m_slack = Mathf.Max(0f, value);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x000481AD File Offset: 0x000463AD
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x000481B5 File Offset: 0x000463B5
		public int steps
		{
			get
			{
				return this.m_steps;
			}
			set
			{
				if (value != this.m_steps)
				{
					this.m_regen = true;
				}
				this.m_steps = Mathf.Max(2, value);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x000481D4 File Offset: 0x000463D4
		public Vector2 midPoint
		{
			get
			{
				Vector2 result = Vector2.zero;
				if (this.m_steps == 2)
				{
					return (this.points[0] + this.points[1]) * 0.5f;
				}
				if (this.m_steps > 2)
				{
					int num = this.m_steps / 2;
					if (this.m_steps % 2 == 0)
					{
						result = (this.points[num] + this.points[num + 1]) * 0.5f;
					}
					else
					{
						result = this.points[num];
					}
				}
				return result;
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00048270 File Offset: 0x00046470
		public CableCurve()
		{
			this.points = CableCurve.emptyCurve;
			this.m_start = Vector2.up;
			this.m_end = Vector2.up + Vector2.right;
			this.m_slack = 0.5f;
			this.m_steps = 20;
			this.m_regen = true;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000482C8 File Offset: 0x000464C8
		public CableCurve(Vector2[] inputPoints)
		{
			this.points = inputPoints;
			this.m_start = inputPoints[0];
			this.m_end = inputPoints[1];
			this.m_slack = 0.5f;
			this.m_steps = 20;
			this.m_regen = true;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00048318 File Offset: 0x00046518
		public CableCurve(List<Vector2> inputPoints)
		{
			this.points = inputPoints.ToArray();
			this.m_start = inputPoints[0];
			this.m_end = inputPoints[1];
			this.m_slack = 0.5f;
			this.m_steps = 20;
			this.m_regen = true;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004836C File Offset: 0x0004656C
		public CableCurve(CableCurve v)
		{
			this.points = v.Points();
			this.m_start = v.start;
			this.m_end = v.end;
			this.m_slack = v.slack;
			this.m_steps = v.steps;
			this.m_regen = v.regenPoints;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000483C8 File Offset: 0x000465C8
		public Vector2[] Points()
		{
			if (!this.m_regen)
			{
				return this.points;
			}
			if (this.m_steps < 2)
			{
				return CableCurve.emptyCurve;
			}
			float num = Vector2.Distance(this.m_end, this.m_start);
			float num2 = Vector2.Distance(new Vector2(this.m_end.x, this.m_start.y), this.m_start);
			float num3 = num + Mathf.Max(0.0001f, this.m_slack);
			float num4 = 0f;
			float y = this.m_start.y;
			float num5 = num2;
			float y2 = this.end.y;
			if (num5 - num4 == 0f)
			{
				return CableCurve.emptyCurve;
			}
			float num6 = Mathf.Sqrt(Mathf.Pow(num3, 2f) - Mathf.Pow(y2 - y, 2f)) / (num5 - num4);
			int num7 = 30;
			int num8 = 0;
			int num9 = num7 * 10;
			bool flag = false;
			float num10 = 0f;
			float num11 = 100f;
			for (int i = 0; i < num7; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					num8++;
					float num12 = num10 + num11;
					float num13 = (float)Math.Sinh((double)num12) / num12;
					if (!float.IsInfinity(num13))
					{
						if (num13 == num6)
						{
							flag = true;
							num10 = num12;
							break;
						}
						if (num13 > num6)
						{
							break;
						}
						num10 = num12;
						if (num8 > num9)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
				num11 *= 0.1f;
			}
			float num14 = (num5 - num4) / 2f / num10;
			float num15 = (num4 + num5 - num14 * Mathf.Log((num3 + y2 - y) / (num3 - y2 + y))) / 2f;
			float num16 = (y2 + y - num3 * (float)Math.Cosh((double)num10) / (float)Math.Sinh((double)num10)) / 2f;
			this.points = new Vector2[this.m_steps];
			float num17 = (float)(this.m_steps - 1);
			for (int k = 0; k < this.m_steps; k++)
			{
				float num18 = (float)k / num17;
				Vector2 zero = Vector2.zero;
				zero.x = Mathf.Lerp(this.start.x, this.end.x, num18);
				zero.y = num14 * (float)Math.Cosh((double)((num18 * num2 - num15) / num14)) + num16;
				this.points[k] = zero;
			}
			this.m_regen = false;
			return this.points;
		}

		// Token: 0x04000D5C RID: 3420
		[SerializeField]
		private Vector2 m_start;

		// Token: 0x04000D5D RID: 3421
		[SerializeField]
		private Vector2 m_end;

		// Token: 0x04000D5E RID: 3422
		[SerializeField]
		private float m_slack;

		// Token: 0x04000D5F RID: 3423
		[SerializeField]
		private int m_steps;

		// Token: 0x04000D60 RID: 3424
		[SerializeField]
		private bool m_regen;

		// Token: 0x04000D61 RID: 3425
		private static Vector2[] emptyCurve = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, 0f)
		};

		// Token: 0x04000D62 RID: 3426
		[SerializeField]
		private Vector2[] points;
	}
}
