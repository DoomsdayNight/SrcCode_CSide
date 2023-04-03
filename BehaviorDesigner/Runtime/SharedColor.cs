using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A2 RID: 162
	[Serializable]
	public class SharedColor : SharedVariable<Color>
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00016AA2 File Offset: 0x00014CA2
		public static implicit operator SharedColor(Color value)
		{
			return new SharedColor
			{
				mValue = value
			};
		}
	}
}
