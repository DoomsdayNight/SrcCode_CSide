using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBehaviour
{
	// Token: 0x02000259 RID: 601
	[TaskCategory("Unity/Behaviour")]
	[TaskDescription("Stores the enabled state of the object. Returns Success.")]
	public class GetEnabled : Action
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x000259C4 File Offset: 0x00023BC4
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null)
			{
				Debug.LogWarning("SpecifiedObject is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.specifiedObject.Value.enabled;
			return TaskStatus.Success;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000259F6 File Offset: 0x00023BF6
		public override void OnReset()
		{
			this.specifiedObject.Value = null;
			this.storeValue = false;
		}

		// Token: 0x04000838 RID: 2104
		[Tooltip("The Behavior to use")]
		public SharedBehaviour specifiedObject;

		// Token: 0x04000839 RID: 2105
		[Tooltip("The enabled/disabled state")]
		[RequiredField]
		public SharedBool storeValue;
	}
}
