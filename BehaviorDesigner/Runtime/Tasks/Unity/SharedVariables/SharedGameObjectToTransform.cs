using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000161 RID: 353
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Gets the Transform from the GameObject. Returns Success.")]
	public class SharedGameObjectToTransform : Action
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x0001D61E File Offset: 0x0001B81E
		public override TaskStatus OnUpdate()
		{
			if (this.sharedGameObject.Value == null)
			{
				return TaskStatus.Failure;
			}
			this.sharedTransform.Value = this.sharedGameObject.Value.GetComponent<Transform>();
			return TaskStatus.Success;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001D651 File Offset: 0x0001B851
		public override void OnReset()
		{
			this.sharedGameObject = null;
			this.sharedTransform = null;
		}

		// Token: 0x040004DA RID: 1242
		[Tooltip("The GameObject to get the Transform of")]
		public SharedGameObject sharedGameObject;

		// Token: 0x040004DB RID: 1243
		[RequiredField]
		[Tooltip("The Transform to set")]
		public SharedTransform sharedTransform;
	}
}
