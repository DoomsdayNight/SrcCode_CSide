using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public class SharedAnimationCurve : SharedVariable<AnimationCurve>
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00016A4A File Offset: 0x00014C4A
		public static implicit operator SharedAnimationCurve(AnimationCurve value)
		{
			return new SharedAnimationCurve
			{
				mValue = value
			};
		}
	}
}
