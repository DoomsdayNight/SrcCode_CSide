using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D6 RID: 470
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start rotation of the Particle System.")]
	public class SetStartRotation : Action
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x000217B4 File Offset: 0x0001F9B4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000217F4 File Offset: 0x0001F9F4
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startRotation = this.startRotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002183F File Offset: 0x0001FA3F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startRotation = 0f;
		}

		// Token: 0x04000695 RID: 1685
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000696 RID: 1686
		[Tooltip("The start rotation of the ParticleSystem")]
		public SharedFloat startRotation;

		// Token: 0x04000697 RID: 1687
		private ParticleSystem particleSystem;

		// Token: 0x04000698 RID: 1688
		private GameObject prevGameObject;
	}
}
