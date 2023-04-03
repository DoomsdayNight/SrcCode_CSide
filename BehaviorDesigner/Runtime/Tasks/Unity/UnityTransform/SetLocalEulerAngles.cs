using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200011F RID: 287
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the local euler angles of the Transform. Returns Success.")]
	public class SetLocalEulerAngles : Action
	{
		// Token: 0x06000814 RID: 2068 RVA: 0x0001B9AC File Offset: 0x00019BAC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001B9EC File Offset: 0x00019BEC
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.localEulerAngles = this.localEulerAngles.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001BA1F File Offset: 0x00019C1F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localEulerAngles = Vector3.zero;
		}

		// Token: 0x0400042C RID: 1068
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400042D RID: 1069
		[Tooltip("The local euler angles of the Transform")]
		public SharedVector3 localEulerAngles;

		// Token: 0x0400042E RID: 1070
		private Transform targetTransform;

		// Token: 0x0400042F RID: 1071
		private GameObject prevGameObject;
	}
}
