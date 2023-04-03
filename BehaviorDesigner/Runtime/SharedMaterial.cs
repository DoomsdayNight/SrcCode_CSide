using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	public class SharedMaterial : SharedVariable<Material>
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x00016B26 File Offset: 0x00014D26
		public static implicit operator SharedMaterial(Material value)
		{
			return new SharedMaterial
			{
				mValue = value
			};
		}
	}
}
