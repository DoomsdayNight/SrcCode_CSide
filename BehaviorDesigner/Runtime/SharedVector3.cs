using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public class SharedVector3 : SharedVariable<Vector3>
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x00016C18 File Offset: 0x00014E18
		public static implicit operator SharedVector3(Vector3 value)
		{
			return new SharedVector3
			{
				mValue = value
			};
		}
	}
}
