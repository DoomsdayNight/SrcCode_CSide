using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D7 RID: 471
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start size of the Particle System.")]
	public class SetStartSize : Action
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00021860 File Offset: 0x0001FA60
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000218A0 File Offset: 0x0001FAA0
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startSize = this.startSize.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000218EB File Offset: 0x0001FAEB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startSize = 0f;
		}

		// Token: 0x04000699 RID: 1689
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400069A RID: 1690
		[Tooltip("The start size of the ParticleSystem")]
		public SharedFloat startSize;

		// Token: 0x0400069B RID: 1691
		private ParticleSystem particleSystem;

		// Token: 0x0400069C RID: 1692
		private GameObject prevGameObject;
	}
}
