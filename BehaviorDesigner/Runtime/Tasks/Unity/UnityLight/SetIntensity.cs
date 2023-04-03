using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200020D RID: 525
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the intensity of the light.")]
	public class SetIntensity : Action
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x00023668 File Offset: 0x00021868
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000236A8 File Offset: 0x000218A8
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.intensity = this.intensity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000236DB File Offset: 0x000218DB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.intensity = 0f;
		}

		// Token: 0x0400074E RID: 1870
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400074F RID: 1871
		[Tooltip("The intensity to set")]
		public SharedFloat intensity;

		// Token: 0x04000750 RID: 1872
		private Light light;

		// Token: 0x04000751 RID: 1873
		private GameObject prevGameObject;
	}
}
