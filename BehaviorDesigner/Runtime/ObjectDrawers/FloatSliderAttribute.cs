using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.ObjectDrawers
{
	// Token: 0x020002AA RID: 682
	public class FloatSliderAttribute : ObjectDrawerAttribute
	{
		// Token: 0x06000DBF RID: 3519 RVA: 0x00028D4A File Offset: 0x00026F4A
		public FloatSliderAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000995 RID: 2453
		public float min;

		// Token: 0x04000996 RID: 2454
		public float max;
	}
}
