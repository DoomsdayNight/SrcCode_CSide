using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CE RID: 462
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the emission rate of the Particle System.")]
	public class SetEmissionRate : Action
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x00021284 File Offset: 0x0001F484
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000212C4 File Offset: 0x0001F4C4
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			Debug.Log("Warning: SetEmissionRate is not used in Unity 5.3 or later.");
			return TaskStatus.Success;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000212EB File Offset: 0x0001F4EB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.emissionRate = 0f;
		}

		// Token: 0x04000675 RID: 1653
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000676 RID: 1654
		[Tooltip("The emission rate of the ParticleSystem")]
		public SharedFloat emissionRate;

		// Token: 0x04000677 RID: 1655
		private ParticleSystem particleSystem;

		// Token: 0x04000678 RID: 1656
		private GameObject prevGameObject;
	}
}
