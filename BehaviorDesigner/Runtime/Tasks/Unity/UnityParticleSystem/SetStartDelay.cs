using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D4 RID: 468
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start delay of the Particle System.")]
	public class SetStartDelay : Action
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x0002165C File Offset: 0x0001F85C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002169C File Offset: 0x0001F89C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startDelay = this.startDelay.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000216E7 File Offset: 0x0001F8E7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startDelay = 0f;
		}

		// Token: 0x0400068D RID: 1677
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400068E RID: 1678
		[Tooltip("The start delay of the ParticleSystem")]
		public SharedFloat startDelay;

		// Token: 0x0400068F RID: 1679
		private ParticleSystem particleSystem;

		// Token: 0x04000690 RID: 1680
		private GameObject prevGameObject;
	}
}
