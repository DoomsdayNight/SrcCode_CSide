using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000210 RID: 528
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the shadow strength of the light.")]
	public class SetShadowSoftnessStrength : Action
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x00023824 File Offset: 0x00021A24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00023864 File Offset: 0x00021A64
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.light.shadowStrength = this.shadowStrength.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00023897 File Offset: 0x00021A97
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.shadowStrength = 0f;
		}

		// Token: 0x0400075A RID: 1882
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400075B RID: 1883
		[Tooltip("The shadow strength to set")]
		public SharedFloat shadowStrength;

		// Token: 0x0400075C RID: 1884
		private Light light;

		// Token: 0x0400075D RID: 1885
		private GameObject prevGameObject;
	}
}
