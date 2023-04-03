using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000202 RID: 514
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the color of the light.")]
	public class GetColor : Action
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x0002301C File Offset: 0x0002121C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002305C File Offset: 0x0002125C
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.color;
			return TaskStatus.Success;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002308F File Offset: 0x0002128F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Color.white;
		}

		// Token: 0x04000722 RID: 1826
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000723 RID: 1827
		[RequiredField]
		[Tooltip("The color to store")]
		public SharedColor storeValue;

		// Token: 0x04000724 RID: 1828
		private Light light;

		// Token: 0x04000725 RID: 1829
		private GameObject prevGameObject;
	}
}
