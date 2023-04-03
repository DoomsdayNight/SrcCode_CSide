using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRenderer
{
	// Token: 0x020001A3 RID: 419
	[TaskCategory("Unity/Renderer")]
	[TaskDescription("Sets the material on the Renderer.")]
	public class SetMaterial : Action
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x0001FB8C File Offset: 0x0001DD8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.renderer = defaultGameObject.GetComponent<Renderer>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		public override TaskStatus OnUpdate()
		{
			if (this.renderer == null)
			{
				Debug.LogWarning("Renderer is null");
				return TaskStatus.Failure;
			}
			this.renderer.material = this.material.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001FBFF File Offset: 0x0001DDFF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.material = null;
		}

		// Token: 0x040005DB RID: 1499
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005DC RID: 1500
		[Tooltip("The material to set")]
		public SharedMaterial material;

		// Token: 0x040005DD RID: 1501
		private Renderer renderer;

		// Token: 0x040005DE RID: 1502
		private GameObject prevGameObject;
	}
}
