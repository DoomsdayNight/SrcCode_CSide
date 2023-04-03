using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C5 RID: 453
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the particle count of the Particle System.")]
	public class GetParticleCount : Action
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00020DF8 File Offset: 0x0001EFF8
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.storeResult.Value = (float)this.particleSystem.particleCount;
			return TaskStatus.Success;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00020E2C File Offset: 0x0001F02C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x04000657 RID: 1623
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000658 RID: 1624
		[Tooltip("The particle count of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x04000659 RID: 1625
		private ParticleSystem particleSystem;

		// Token: 0x0400065A RID: 1626
		private GameObject prevGameObject;
	}
}
