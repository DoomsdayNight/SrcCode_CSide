using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	public class SharedRect : SharedVariable<Rect>
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00016B7E File Offset: 0x00014D7E
		public static implicit operator SharedRect(Rect value)
		{
			return new SharedRect
			{
				mValue = value
			};
		}
	}
}
