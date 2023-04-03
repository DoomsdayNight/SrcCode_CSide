using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200010E RID: 270
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the euler angles of the Transform. Returns Success.")]
	public class GetEulerAngles : Action
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x0001AF38 File Offset: 0x00019138
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001AF78 File Offset: 0x00019178
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.eulerAngles;
			return TaskStatus.Success;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001AFAB File Offset: 0x000191AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040003E3 RID: 995
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003E4 RID: 996
		[Tooltip("The euler angles of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040003E5 RID: 997
		private Transform targetTransform;

		// Token: 0x040003E6 RID: 998
		private GameObject prevGameObject;
	}
}
