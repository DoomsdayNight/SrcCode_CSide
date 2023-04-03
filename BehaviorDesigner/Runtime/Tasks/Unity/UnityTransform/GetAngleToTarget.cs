using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200010B RID: 267
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Gets the Angle between a GameObject's forward direction and a target. Returns Success.")]
	public class GetAngleToTarget : Action
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001ACF8 File Offset: 0x00018EF8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			Vector3 a;
			if (this.targetObject.Value != null)
			{
				a = this.targetObject.Value.transform.InverseTransformPoint(this.targetPosition.Value);
			}
			else
			{
				a = this.targetPosition.Value;
			}
			if (this.ignoreHeight.Value)
			{
				a.y = this.targetTransform.position.y;
			}
			Vector3 from = a - this.targetTransform.position;
			this.storeValue.Value = Vector3.Angle(from, this.targetTransform.forward);
			return TaskStatus.Success;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.targetObject = null;
			this.targetPosition = Vector3.zero;
			this.ignoreHeight = true;
			this.storeValue = 0f;
		}

		// Token: 0x040003D3 RID: 979
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003D4 RID: 980
		[Tooltip("The target object to measure the angle to. If null the targetPosition will be used.")]
		public SharedGameObject targetObject;

		// Token: 0x040003D5 RID: 981
		[Tooltip("The world position to measure an angle to. If the targetObject is also not null, this value is used as an offset from that object's position.")]
		public SharedVector3 targetPosition;

		// Token: 0x040003D6 RID: 982
		[Tooltip("Ignore height differences when calculating the angle?")]
		public SharedBool ignoreHeight = true;

		// Token: 0x040003D7 RID: 983
		[Tooltip("The angle to the target")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040003D8 RID: 984
		private Transform targetTransform;

		// Token: 0x040003D9 RID: 985
		private GameObject prevGameObject;
	}
}
