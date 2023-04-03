using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200020B RID: 523
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the light's cookie size.")]
	public class SetCookieSize : Action
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x00023544 File Offset: 0x00021744
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00023584 File Offset: 0x00021784
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.cookieSize = this.cookieSize.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x000235B7 File Offset: 0x000217B7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cookieSize = 0f;
		}

		// Token: 0x04000746 RID: 1862
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000747 RID: 1863
		[Tooltip("The size to set")]
		public SharedFloat cookieSize;

		// Token: 0x04000748 RID: 1864
		private Light light;

		// Token: 0x04000749 RID: 1865
		private GameObject prevGameObject;
	}
}
