using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D1 RID: 465
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the max particles of the Particle System.")]
	public class SetMaxParticles : Action
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x00021454 File Offset: 0x0001F654
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00021494 File Offset: 0x0001F694
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.maxParticles = this.maxParticles.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000214DA File Offset: 0x0001F6DA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.maxParticles = 0;
		}

		// Token: 0x04000681 RID: 1665
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000682 RID: 1666
		[Tooltip("The max particles of the ParticleSystem")]
		public SharedInt maxParticles;

		// Token: 0x04000683 RID: 1667
		private ParticleSystem particleSystem;

		// Token: 0x04000684 RID: 1668
		private GameObject prevGameObject;
	}
}
