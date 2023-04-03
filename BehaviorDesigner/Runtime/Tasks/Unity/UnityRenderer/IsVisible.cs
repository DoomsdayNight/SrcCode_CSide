using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRenderer
{
	// Token: 0x020001A2 RID: 418
	[TaskCategory("Unity/Renderer")]
	[TaskDescription("Returns Success if the Renderer is visible, otherwise Failure.")]
	public class IsVisible : Conditional
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x0001FB0C File Offset: 0x0001DD0C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.renderer = defaultGameObject.GetComponent<Renderer>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001FB4C File Offset: 0x0001DD4C
		public override TaskStatus OnUpdate()
		{
			if (this.renderer == null)
			{
				Debug.LogWarning("Renderer is null");
				return TaskStatus.Failure;
			}
			if (!this.renderer.isVisible)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001FB78 File Offset: 0x0001DD78
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040005D8 RID: 1496
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005D9 RID: 1497
		private Renderer renderer;

		// Token: 0x040005DA RID: 1498
		private GameObject prevGameObject;
	}
}
