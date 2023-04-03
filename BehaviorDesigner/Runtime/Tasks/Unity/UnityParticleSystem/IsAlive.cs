using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001C8 RID: 456
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Is the Particle System alive?")]
	public class IsAlive : Conditional
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x00020F8C File Offset: 0x0001F18C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00020FCC File Offset: 0x0001F1CC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				Debug.LogWarning("ParticleSystem is null");
				return TaskStatus.Failure;
			}
			if (!this.particleSystem.IsAlive())
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000663 RID: 1635
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000664 RID: 1636
		private ParticleSystem particleSystem;

		// Token: 0x04000665 RID: 1637
		private GameObject prevGameObject;
	}
}
