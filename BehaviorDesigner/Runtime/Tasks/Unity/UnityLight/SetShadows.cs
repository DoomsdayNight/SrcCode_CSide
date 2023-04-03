using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000211 RID: 529
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the shadow type of the light.")]
	public class SetShadows : Action
	{
		// Token: 0x06000B7A RID: 2938 RVA: 0x000238B8 File Offset: 0x00021AB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000238F8 File Offset: 0x00021AF8
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.shadows = this.shadows;
			return TaskStatus.Success;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00023926 File Offset: 0x00021B26
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400075E RID: 1886
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400075F RID: 1887
		[Tooltip("The shadow type to set")]
		public LightShadows shadows;

		// Token: 0x04000760 RID: 1888
		private Light light;

		// Token: 0x04000761 RID: 1889
		private GameObject prevGameObject;
	}
}
