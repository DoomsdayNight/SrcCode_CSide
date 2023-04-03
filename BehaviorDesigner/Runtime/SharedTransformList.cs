using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000AF RID: 175
	[Serializable]
	public class SharedTransformList : SharedVariable<List<Transform>>
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public static implicit operator SharedTransformList(List<Transform> value)
		{
			return new SharedTransformList
			{
				mValue = value
			};
		}
	}
}
