using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200020A RID: 522
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the cookie of the light.")]
	public class SetCookie : Action
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x000234BC File Offset: 0x000216BC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000234FC File Offset: 0x000216FC
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.cookie = this.cookie;
			return TaskStatus.Success;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002352A File Offset: 0x0002172A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cookie = null;
		}

		// Token: 0x04000742 RID: 1858
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000743 RID: 1859
		[Tooltip("The cookie to set")]
		public Texture2D cookie;

		// Token: 0x04000744 RID: 1860
		private Light light;

		// Token: 0x04000745 RID: 1861
		private GameObject prevGameObject;
	}
}
