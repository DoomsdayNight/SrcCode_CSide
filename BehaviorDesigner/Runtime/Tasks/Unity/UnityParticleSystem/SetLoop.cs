using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D0 RID: 464
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets if the Particle System should loop.")]
	public class SetLoop : Action
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x000213B0 File Offset: 0x0001F5B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000213F0 File Offset: 0x0001F5F0
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.loop = this.loop.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00021436 File Offset: 0x0001F636
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.loop = false;
		}

		// Token: 0x0400067D RID: 1661
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400067E RID: 1662
		[Tooltip("Should the ParticleSystem loop?")]
		public SharedBool loop;

		// Token: 0x0400067F RID: 1663
		private ParticleSystem particleSystem;

		// Token: 0x04000680 RID: 1664
		private GameObject prevGameObject;
	}
}
