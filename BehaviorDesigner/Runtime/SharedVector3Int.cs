using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	public class SharedVector3Int : SharedVariable<Vector3Int>
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00016C2E File Offset: 0x00014E2E
		public static implicit operator SharedVector3Int(Vector3Int value)
		{
			return new SharedVector3Int
			{
				mValue = value
			};
		}
	}
}
