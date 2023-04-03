using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000124 RID: 292
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the position of the Transform. Returns Success.")]
	public class SetPosition : Action
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x0001BC88 File Offset: 0x00019E88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.position = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001BCFB File Offset: 0x00019EFB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04000440 RID: 1088
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000441 RID: 1089
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04000442 RID: 1090
		private Transform targetTransform;

		// Token: 0x04000443 RID: 1091
		private GameObject prevGameObject;
	}
}
