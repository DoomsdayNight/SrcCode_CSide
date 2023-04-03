using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	public class SharedGameObjectList : SharedVariable<List<GameObject>>
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00016AE4 File Offset: 0x00014CE4
		public static implicit operator SharedGameObjectList(List<GameObject> value)
		{
			return new SharedGameObjectList
			{
				mValue = value
			};
		}
	}
}
