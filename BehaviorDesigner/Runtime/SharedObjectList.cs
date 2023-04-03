using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	public class SharedObjectList : SharedVariable<List<UnityEngine.Object>>
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x00016B52 File Offset: 0x00014D52
		public static implicit operator SharedObjectList(List<UnityEngine.Object> value)
		{
			return new SharedObjectList
			{
				mValue = value
			};
		}
	}
}
