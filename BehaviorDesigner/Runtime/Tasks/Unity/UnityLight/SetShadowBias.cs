using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200020F RID: 527
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the shadow bias of the light.")]
	public class SetShadowBias : Action
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x00023790 File Offset: 0x00021990
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000237D0 File Offset: 0x000219D0
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.shadowBias = this.shadowBias.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00023803 File Offset: 0x00021A03
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.shadowBias = 0f;
		}

		// Token: 0x04000756 RID: 1878
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000757 RID: 1879
		[Tooltip("The shadow bias to set")]
		public SharedFloat shadowBias;

		// Token: 0x04000758 RID: 1880
		private Light light;

		// Token: 0x04000759 RID: 1881
		private GameObject prevGameObject;
	}
}
