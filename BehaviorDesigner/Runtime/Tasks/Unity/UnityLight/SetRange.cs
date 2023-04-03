using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200020E RID: 526
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the range of the light.")]
	public class SetRange : Action
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x000236FC File Offset: 0x000218FC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002373C File Offset: 0x0002193C
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.range = this.range.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002376F File Offset: 0x0002196F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.range = 0f;
		}

		// Token: 0x04000752 RID: 1874
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000753 RID: 1875
		[Tooltip("The range to set")]
		public SharedFloat range;

		// Token: 0x04000754 RID: 1876
		private Light light;

		// Token: 0x04000755 RID: 1877
		private GameObject prevGameObject;
	}
}
