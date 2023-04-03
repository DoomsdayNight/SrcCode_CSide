using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A9 RID: 169
	[Serializable]
	public class SharedObject : SharedVariable<UnityEngine.Object>
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00016B3C File Offset: 0x00014D3C
		public static explicit operator SharedObject(UnityEngine.Object value)
		{
			return new SharedObject
			{
				mValue = value
			};
		}
	}
}
