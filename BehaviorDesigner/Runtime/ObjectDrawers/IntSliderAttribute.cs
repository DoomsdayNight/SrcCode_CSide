using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.ObjectDrawers
{
	// Token: 0x020002AB RID: 683
	public class IntSliderAttribute : ObjectDrawerAttribute
	{
		// Token: 0x06000DC0 RID: 3520 RVA: 0x00028D60 File Offset: 0x00026F60
		public IntSliderAttribute(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000997 RID: 2455
		public int min;

		// Token: 0x04000998 RID: 2456
		public int max;
	}
}
