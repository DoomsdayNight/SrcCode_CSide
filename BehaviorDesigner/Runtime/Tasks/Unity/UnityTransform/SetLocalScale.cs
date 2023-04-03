using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000122 RID: 290
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the local scale of the Transform. Returns Success.")]
	public class SetLocalScale : Action
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x0001BB68 File Offset: 0x00019D68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.localScale = this.localScale.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001BBDB File Offset: 0x00019DDB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localScale = Vector3.zero;
		}

		// Token: 0x04000438 RID: 1080
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000439 RID: 1081
		[Tooltip("The local scale of the Transform")]
		public SharedVector3 localScale;

		// Token: 0x0400043A RID: 1082
		private Transform targetTransform;

		// Token: 0x0400043B RID: 1083
		private GameObject prevGameObject;
	}
}
