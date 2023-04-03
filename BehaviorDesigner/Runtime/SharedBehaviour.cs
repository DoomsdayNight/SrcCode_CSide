using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public class SharedBehaviour : SharedVariable<Behaviour>
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x00016A60 File Offset: 0x00014C60
		public static explicit operator SharedBehaviour(Behaviour value)
		{
			return new SharedBehaviour
			{
				mValue = value
			};
		}
	}
}
