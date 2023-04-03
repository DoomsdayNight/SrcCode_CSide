using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000213 RID: 531
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the type of the light.")]
	public class SetType : Action
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x000239CC File Offset: 0x00021BCC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00023A0C File Offset: 0x00021C0C
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.type = this.type;
			return TaskStatus.Success;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00023A3A File Offset: 0x00021C3A
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000766 RID: 1894
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000767 RID: 1895
		[Tooltip("The type to set")]
		public LightType type;

		// Token: 0x04000768 RID: 1896
		private Light light;

		// Token: 0x04000769 RID: 1897
		private GameObject prevGameObject;
	}
}
