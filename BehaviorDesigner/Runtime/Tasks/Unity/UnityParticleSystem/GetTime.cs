using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C7 RID: 455
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the time of the Particle System.")]
	public class GetTime : Action
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x00020EF8 File Offset: 0x0001F0F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00020F38 File Offset: 0x0001F138
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.storeResult.Value = this.particleSystem.time;
			return TaskStatus.Success;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00020F6B File Offset: 0x0001F16B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x0400065F RID: 1631
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000660 RID: 1632
		[Tooltip("The time of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04000661 RID: 1633
		private ParticleSystem particleSystem;

		// Token: 0x04000662 RID: 1634
		private GameObject prevGameObject;
	}
}
