using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000207 RID: 519
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the color of the light.")]
	public class GetShadowStrength : Action
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x00023300 File Offset: 0x00021500
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00023340 File Offset: 0x00021540
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.shadowStrength;
			return TaskStatus.Success;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00023373 File Offset: 0x00021573
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000736 RID: 1846
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000737 RID: 1847
		[RequiredField]
		[Tooltip("The color to store")]
		public SharedFloat storeValue;

		// Token: 0x04000738 RID: 1848
		private Light light;

		// Token: 0x04000739 RID: 1849
		private GameObject prevGameObject;
	}
}
