using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000289 RID: 649
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Converts the state name to its corresponding hash code. Returns Success.")]
	public class GetStringToHash : Action
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x00027574 File Offset: 0x00025774
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = Animator.StringToHash(this.stateName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00027592 File Offset: 0x00025792
		public override void OnReset()
		{
			this.stateName = "";
			this.storeValue = 0;
		}

		// Token: 0x040008F5 RID: 2293
		[Tooltip("The name of the state to convert to a hash code")]
		public SharedString stateName;

		// Token: 0x040008F6 RID: 2294
		[Tooltip("The hash value")]
		[RequiredField]
		public SharedInt storeValue;
	}
}
