using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Collider
{
	// Token: 0x02000237 RID: 567
	[TaskCategory("Unity/Collider")]
	[TaskDescription("Enables/Disables the collider. Returns Success.")]
	public class SetEnabled : Action
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002464F File Offset: 0x0002284F
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedCollider == null)
			{
				Debug.LogWarning("SpecifiedCollider is null");
				return TaskStatus.Failure;
			}
			this.specifiedCollider.Value.enabled = this.enabled.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00024681 File Offset: 0x00022881
		public override void OnReset()
		{
			this.specifiedCollider.Value = null;
			this.enabled = false;
		}

		// Token: 0x040007B3 RID: 1971
		[Tooltip("The Behavior to use")]
		public SharedCollider specifiedCollider;

		// Token: 0x040007B4 RID: 1972
		[Tooltip("The enabled/disabled state")]
		public SharedBool enabled;
	}
}
