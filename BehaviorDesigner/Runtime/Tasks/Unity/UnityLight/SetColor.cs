using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000209 RID: 521
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the color of the light.")]
	public class SetColor : Action
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x00023428 File Offset: 0x00021628
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00023468 File Offset: 0x00021668
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.color = this.color.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002349B File Offset: 0x0002169B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.color = Color.white;
		}

		// Token: 0x0400073E RID: 1854
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400073F RID: 1855
		[Tooltip("The color to set")]
		public SharedColor color;

		// Token: 0x04000740 RID: 1856
		private Light light;

		// Token: 0x04000741 RID: 1857
		private GameObject prevGameObject;
	}
}
