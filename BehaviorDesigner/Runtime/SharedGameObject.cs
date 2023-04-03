using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	public class SharedGameObject : SharedVariable<GameObject>
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00016ACE File Offset: 0x00014CCE
		public static implicit operator SharedGameObject(GameObject value)
		{
			return new SharedGameObject
			{
				mValue = value
			};
		}
	}
}
