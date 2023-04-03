using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Collider
{
	// Token: 0x02000236 RID: 566
	[TaskCategory("Unity/Collider")]
	[TaskDescription("Stores the enabled state of the collider. Returns Success.")]
	public class GetEnabled : Action
	{
		// Token: 0x06000BEE RID: 3054 RVA: 0x000245FB File Offset: 0x000227FB
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedCollider == null)
			{
				Debug.LogWarning("SpecifiedObject is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.specifiedCollider.Value.enabled;
			return TaskStatus.Success;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002462D File Offset: 0x0002282D
		public override void OnReset()
		{
			this.specifiedCollider.Value = null;
			this.storeValue = false;
		}

		// Token: 0x040007B1 RID: 1969
		[Tooltip("The Collider to use")]
		public SharedCollider specifiedCollider;

		// Token: 0x040007B2 RID: 1970
		[Tooltip("The enabled/disabled state")]
		[RequiredField]
		public SharedBool storeValue;
	}
}
