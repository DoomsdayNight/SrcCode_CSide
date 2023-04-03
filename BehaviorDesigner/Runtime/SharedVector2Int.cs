using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000B2 RID: 178
	[Serializable]
	public class SharedVector2Int : SharedVariable<Vector2Int>
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00016C02 File Offset: 0x00014E02
		public static implicit operator SharedVector2Int(Vector2Int value)
		{
			return new SharedVector2Int
			{
				mValue = value
			};
		}
	}
}
