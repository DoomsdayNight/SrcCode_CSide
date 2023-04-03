using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C9 RID: 457
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Is the Particle System paused?")]
	public class IsPaused : Conditional
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x0002100C File Offset: 0x0001F20C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002104C File Offset: 0x0001F24C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			if (!this.particleSystem.isPaused)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00021078 File Offset: 0x0001F278
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000666 RID: 1638
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000667 RID: 1639
		private ParticleSystem particleSystem;

		// Token: 0x04000668 RID: 1640
		private GameObject prevGameObject;
	}
}
