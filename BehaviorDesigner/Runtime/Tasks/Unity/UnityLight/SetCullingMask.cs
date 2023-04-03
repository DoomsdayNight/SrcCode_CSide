using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200020C RID: 524
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the culling mask of the light.")]
	public class SetCullingMask : Action
	{
		// Token: 0x06000B66 RID: 2918 RVA: 0x000235D8 File Offset: 0x000217D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00023618 File Offset: 0x00021818
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.cullingMask = this.cullingMask.value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002364B File Offset: 0x0002184B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cullingMask = -1;
		}

		// Token: 0x0400074A RID: 1866
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400074B RID: 1867
		[Tooltip("The culling mask to set")]
		public LayerMask cullingMask;

		// Token: 0x0400074C RID: 1868
		private Light light;

		// Token: 0x0400074D RID: 1869
		private GameObject prevGameObject;
	}
}
