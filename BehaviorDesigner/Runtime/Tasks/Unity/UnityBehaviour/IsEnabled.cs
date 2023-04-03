using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBehaviour
{
	// Token: 0x0200025A RID: 602
	[TaskCategory("Unity/Behaviour")]
	[TaskDescription("Returns Success if the object is enabled, otherwise Failure.")]
	public class IsEnabled : Conditional
	{
		// Token: 0x06000C7C RID: 3196 RVA: 0x00025A18 File Offset: 0x00023C18
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null && !(this.specifiedObject.Value is Behaviour))
			{
				Debug.LogWarning("SpecifiedObject is null or not a subclass of UnityEngine.Behaviour");
				return TaskStatus.Failure;
			}
			if (!(this.specifiedObject.Value as Behaviour).enabled)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00025A65 File Offset: 0x00023C65
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
		}

		// Token: 0x0400083A RID: 2106
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;
	}
}
