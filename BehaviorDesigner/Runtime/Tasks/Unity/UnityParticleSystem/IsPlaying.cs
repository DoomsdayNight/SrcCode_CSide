using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CA RID: 458
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Is the Particle System playing?")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x0002108C File Offset: 0x0001F28C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000210CC File Offset: 0x0001F2CC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			if (!this.particleSystem.isPlaying)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000210F8 File Offset: 0x0001F2F8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000669 RID: 1641
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400066A RID: 1642
		private ParticleSystem particleSystem;

		// Token: 0x0400066B RID: 1643
		private GameObject prevGameObject;
	}
}
