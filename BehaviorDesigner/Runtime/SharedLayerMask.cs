using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A7 RID: 167
	[Serializable]
	public class SharedLayerMask : SharedVariable<LayerMask>
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x00016B10 File Offset: 0x00014D10
		public static implicit operator SharedLayerMask(LayerMask value)
		{
			return new SharedLayerMask
			{
				Value = value
			};
		}
	}
}
