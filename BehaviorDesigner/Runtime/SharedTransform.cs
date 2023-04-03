using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public class SharedTransform : SharedVariable<Transform>
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x00016BAA File Offset: 0x00014DAA
		public static implicit operator SharedTransform(Transform value)
		{
			return new SharedTransform
			{
				mValue = value
			};
		}
	}
}
