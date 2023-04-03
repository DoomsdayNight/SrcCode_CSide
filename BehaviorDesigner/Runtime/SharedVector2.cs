using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class SharedVector2 : SharedVariable<Vector2>
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x00016BEC File Offset: 0x00014DEC
		public static implicit operator SharedVector2(Vector2 value)
		{
			return new SharedVector2
			{
				mValue = value
			};
		}
	}
}
