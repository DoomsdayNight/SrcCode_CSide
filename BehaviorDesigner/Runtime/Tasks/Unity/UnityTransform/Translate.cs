using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000128 RID: 296
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Moves the transform in the direction and distance of translation. Returns Success.")]
	public class Translate : Action
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001BF18 File Offset: 0x0001A118
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.Translate(this.translation.Value, this.relativeTo);
			return TaskStatus.Success;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001BF51 File Offset: 0x0001A151
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.translation = Vector3.zero;
			this.relativeTo = Space.Self;
		}

		// Token: 0x04000450 RID: 1104
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000451 RID: 1105
		[Tooltip("Move direction and distance")]
		public SharedVector3 translation;

		// Token: 0x04000452 RID: 1106
		[Tooltip("Specifies which axis the rotation is relative to")]
		public Space relativeTo = Space.Self;

		// Token: 0x04000453 RID: 1107
		private Transform targetTransform;

		// Token: 0x04000454 RID: 1108
		private GameObject prevGameObject;
	}
}
