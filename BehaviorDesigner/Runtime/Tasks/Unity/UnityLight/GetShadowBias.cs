using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000206 RID: 518
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the shadow bias of the light.")]
	public class GetShadowBias : Action
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x0002326C File Offset: 0x0002146C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000232AC File Offset: 0x000214AC
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.shadowBias;
			return TaskStatus.Success;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000232DF File Offset: 0x000214DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000732 RID: 1842
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000733 RID: 1843
		[RequiredField]
		[Tooltip("The shadow bias to store")]
		public SharedFloat storeValue;

		// Token: 0x04000734 RID: 1844
		private Light light;

		// Token: 0x04000735 RID: 1845
		private GameObject prevGameObject;
	}
}
