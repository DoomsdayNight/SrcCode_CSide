using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBehaviour
{
	// Token: 0x0200025B RID: 603
	[TaskCategory("Unity/Behaviour")]
	[TaskDescription("Enables/Disables the object. Returns Success.")]
	public class SetEnabled : Action
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x00025A83 File Offset: 0x00023C83
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null)
			{
				Debug.LogWarning("SpecifiedObject is null");
				return TaskStatus.Failure;
			}
			this.specifiedObject.Value.enabled = this.enabled.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00025AB5 File Offset: 0x00023CB5
		public override void OnReset()
		{
			this.specifiedObject.Value = null;
			this.enabled = false;
		}

		// Token: 0x0400083B RID: 2107
		[Tooltip("The Behavior to use")]
		public SharedBehaviour specifiedObject;

		// Token: 0x0400083C RID: 2108
		[Tooltip("The enabled/disabled state")]
		public SharedBool enabled;
	}
}
