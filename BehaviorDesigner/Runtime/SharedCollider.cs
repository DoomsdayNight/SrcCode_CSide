using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	public class SharedCollider : SharedVariable<Collider>
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x00016A8C File Offset: 0x00014C8C
		public static implicit operator SharedCollider(Collider value)
		{
			return new SharedCollider
			{
				mValue = value
			};
		}
	}
}
