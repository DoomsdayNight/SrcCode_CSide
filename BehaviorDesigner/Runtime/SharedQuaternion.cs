using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public class SharedQuaternion : SharedVariable<Quaternion>
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00016B68 File Offset: 0x00014D68
		public static implicit operator SharedQuaternion(Quaternion value)
		{
			return new SharedQuaternion
			{
				mValue = value
			};
		}
	}
}
