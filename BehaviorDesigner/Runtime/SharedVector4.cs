using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	public class SharedVector4 : SharedVariable<Vector4>
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x00016C44 File Offset: 0x00014E44
		public static implicit operator SharedVector4(Vector4 value)
		{
			return new SharedVector4
			{
				mValue = value
			};
		}
	}
}
