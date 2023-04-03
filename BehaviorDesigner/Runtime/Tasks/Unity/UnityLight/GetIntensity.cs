using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000204 RID: 516
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the intensity of the light.")]
	public class GetIntensity : Action
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x00023144 File Offset: 0x00021344
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00023184 File Offset: 0x00021384
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.intensity;
			return TaskStatus.Success;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000231B7 File Offset: 0x000213B7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400072A RID: 1834
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400072B RID: 1835
		[RequiredField]
		[Tooltip("The intensity to store")]
		public SharedFloat storeValue;

		// Token: 0x0400072C RID: 1836
		private Light light;

		// Token: 0x0400072D RID: 1837
		private GameObject prevGameObject;
	}
}
