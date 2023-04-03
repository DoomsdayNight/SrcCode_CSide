using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000212 RID: 530
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the spot angle of the light.")]
	public class SetSpotAngle : Action
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x00023938 File Offset: 0x00021B38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00023978 File Offset: 0x00021B78
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.spotAngle = this.spotAngle.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000239AB File Offset: 0x00021BAB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spotAngle = 0f;
		}

		// Token: 0x04000762 RID: 1890
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000763 RID: 1891
		[Tooltip("The spot angle to set")]
		public SharedFloat spotAngle;

		// Token: 0x04000764 RID: 1892
		private Light light;

		// Token: 0x04000765 RID: 1893
		private GameObject prevGameObject;
	}
}
