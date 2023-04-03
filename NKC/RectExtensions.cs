using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006F4 RID: 1780
	public static class RectExtensions
	{
		// Token: 0x06003F36 RID: 16182 RVA: 0x00148834 File Offset: 0x00146A34
		public static Rect Union(this Rect RA, Rect RB)
		{
			return new Rect
			{
				min = Vector2.Min(RA.min, RB.min),
				max = Vector2.Max(RA.max, RB.max)
			};
		}
	}
}
