using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000205 RID: 517
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the range of the light.")]
	public class GetRange : Action
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x000231D8 File Offset: 0x000213D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00023218 File Offset: 0x00021418
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.range;
			return TaskStatus.Success;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002324B File Offset: 0x0002144B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400072E RID: 1838
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400072F RID: 1839
		[RequiredField]
		[Tooltip("The range to store")]
		public SharedFloat storeValue;

		// Token: 0x04000730 RID: 1840
		private Light light;

		// Token: 0x04000731 RID: 1841
		private GameObject prevGameObject;
	}
}
