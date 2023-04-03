using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CD RID: 461
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Play the Particle System.")]
	public class Play : Action
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x00021208 File Offset: 0x0001F408
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00021248 File Offset: 0x0001F448
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.Play();
			return TaskStatus.Success;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00021270 File Offset: 0x0001F470
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000672 RID: 1650
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000673 RID: 1651
		private ParticleSystem particleSystem;

		// Token: 0x04000674 RID: 1652
		private GameObject prevGameObject;
	}
}
