using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000203 RID: 515
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the light's cookie size.")]
	public class GetCookieSize : Action
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x000230B0 File Offset: 0x000212B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000230F0 File Offset: 0x000212F0
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.cookieSize;
			return TaskStatus.Success;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00023123 File Offset: 0x00021323
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000726 RID: 1830
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000727 RID: 1831
		[RequiredField]
		[Tooltip("The size to store")]
		public SharedFloat storeValue;

		// Token: 0x04000728 RID: 1832
		private Light light;

		// Token: 0x04000729 RID: 1833
		private GameObject prevGameObject;
	}
}
