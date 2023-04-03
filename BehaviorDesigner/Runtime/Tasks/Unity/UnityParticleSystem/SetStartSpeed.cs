using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D8 RID: 472
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start speed of the Particle System.")]
	public class SetStartSpeed : Action
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x0002190C File Offset: 0x0001FB0C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002194C File Offset: 0x0001FB4C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startSpeed = this.startSpeed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00021997 File Offset: 0x0001FB97
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startSpeed = 0f;
		}

		// Token: 0x0400069D RID: 1693
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400069E RID: 1694
		[Tooltip("The start speed of the ParticleSystem")]
		public SharedFloat startSpeed;

		// Token: 0x0400069F RID: 1695
		private ParticleSystem particleSystem;

		// Token: 0x040006A0 RID: 1696
		private GameObject prevGameObject;
	}
}
