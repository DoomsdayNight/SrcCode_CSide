using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D5 RID: 469
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start lifetime of the Particle System.")]
	public class SetStartLifetime : Action
	{
		// Token: 0x06000AA0 RID: 2720 RVA: 0x00021708 File Offset: 0x0001F908
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00021748 File Offset: 0x0001F948
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startLifetime = this.startLifetime.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00021793 File Offset: 0x0001F993
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startLifetime = 0f;
		}

		// Token: 0x04000691 RID: 1681
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000692 RID: 1682
		[Tooltip("The start lifetime of the ParticleSystem")]
		public SharedFloat startLifetime;

		// Token: 0x04000693 RID: 1683
		private ParticleSystem particleSystem;

		// Token: 0x04000694 RID: 1684
		private GameObject prevGameObject;
	}
}
