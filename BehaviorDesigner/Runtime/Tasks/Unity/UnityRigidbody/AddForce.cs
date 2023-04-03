using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200017F RID: 383
	[RequiredComponent(typeof(Rigidbody))]
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody. Returns Success.")]
	public class AddForce : Action
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0001E700 File Offset: 0x0001C900
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001E740 File Offset: 0x0001C940
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.AddForce(this.force.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001E779 File Offset: 0x0001C979
		public override void OnReset()
		{
			this.targetGameObject = null;
			if (this.force != null)
			{
				this.force.Value = Vector3.zero;
			}
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x0400054B RID: 1355
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400054C RID: 1356
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x0400054D RID: 1357
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x0400054E RID: 1358
		private Rigidbody rigidbody;

		// Token: 0x0400054F RID: 1359
		private GameObject prevGameObject;
	}
}
