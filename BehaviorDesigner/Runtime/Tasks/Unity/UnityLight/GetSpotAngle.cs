using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000208 RID: 520
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the spot angle of the light.")]
	public class GetSpotAngle : Action
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x00023394 File Offset: 0x00021594
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000233D4 File Offset: 0x000215D4
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.spotAngle;
			return TaskStatus.Success;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00023407 File Offset: 0x00021607
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400073A RID: 1850
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400073B RID: 1851
		[RequiredField]
		[Tooltip("The spot angle to store")]
		public SharedFloat storeValue;

		// Token: 0x0400073C RID: 1852
		private Light light;

		// Token: 0x0400073D RID: 1853
		private GameObject prevGameObject;
	}
}
