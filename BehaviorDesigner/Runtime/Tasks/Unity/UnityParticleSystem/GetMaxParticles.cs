using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C4 RID: 452
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the max particles of the Particle System.")]
	public class GetMaxParticles : Action
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x00020D10 File Offset: 0x0001EF10
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00020D50 File Offset: 0x0001EF50
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.storeResult.Value = (float)this.particleSystem.main.maxParticles;
			return TaskStatus.Success;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00020D97 File Offset: 0x0001EF97
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04000653 RID: 1619
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000654 RID: 1620
		[Tooltip("The max particles of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04000655 RID: 1621
		private ParticleSystem particleSystem;

		// Token: 0x04000656 RID: 1622
		private GameObject prevGameObject;
	}
}
