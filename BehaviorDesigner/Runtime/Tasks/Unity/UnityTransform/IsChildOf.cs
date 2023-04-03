using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000119 RID: 281
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Returns Success if the transform is a child of the specified GameObject.")]
	public class IsChildOf : Conditional
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x0001B58C File Offset: 0x0001978C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001B5CC File Offset: 0x000197CC
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			if (!this.targetTransform.IsChildOf(this.transformName.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001B603 File Offset: 0x00019803
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.transformName = null;
		}

		// Token: 0x0400040F RID: 1039
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000410 RID: 1040
		[Tooltip("The interested transform")]
		public SharedTransform transformName;

		// Token: 0x04000411 RID: 1041
		private Transform targetTransform;

		// Token: 0x04000412 RID: 1042
		private GameObject prevGameObject;
	}
}
