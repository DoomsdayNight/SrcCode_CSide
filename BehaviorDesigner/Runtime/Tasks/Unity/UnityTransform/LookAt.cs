using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200011A RID: 282
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Rotates the transform so the forward vector points at worldPosition. Returns Success.")]
	public class LookAt : Action
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x0001B61C File Offset: 0x0001981C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001B65C File Offset: 0x0001985C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			if (this.targetLookAt.Value != null)
			{
				this.targetTransform.LookAt(this.targetLookAt.Value.transform);
			}
			else
			{
				this.targetTransform.LookAt(this.worldPosition.Value, this.worldUp);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001B6D0 File Offset: 0x000198D0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.targetLookAt = null;
			this.worldPosition = Vector3.up;
			this.worldUp = Vector3.up;
		}

		// Token: 0x04000413 RID: 1043
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000414 RID: 1044
		[Tooltip("The GameObject to look at. If null the world position will be used.")]
		public SharedGameObject targetLookAt;

		// Token: 0x04000415 RID: 1045
		[Tooltip("Point to look at")]
		public SharedVector3 worldPosition;

		// Token: 0x04000416 RID: 1046
		[Tooltip("Vector specifying the upward direction")]
		public Vector3 worldUp;

		// Token: 0x04000417 RID: 1047
		private Transform targetTransform;

		// Token: 0x04000418 RID: 1048
		private GameObject prevGameObject;
	}
}
