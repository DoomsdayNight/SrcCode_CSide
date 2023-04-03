using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000164 RID: 356
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedTransformList values from the Transforms. Returns Success.")]
	public class SharedTransformsToTransformList : Action
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0001D740 File Offset: 0x0001B940
		public override void OnAwake()
		{
			this.storedTransformList.Value = new List<Transform>();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001D754 File Offset: 0x0001B954
		public override TaskStatus OnUpdate()
		{
			if (this.transforms == null || this.transforms.Length == 0)
			{
				return TaskStatus.Failure;
			}
			this.storedTransformList.Value.Clear();
			for (int i = 0; i < this.transforms.Length; i++)
			{
				this.storedTransformList.Value.Add(this.transforms[i].Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001D7B5 File Offset: 0x0001B9B5
		public override void OnReset()
		{
			this.transforms = null;
			this.storedTransformList = null;
		}

		// Token: 0x040004E0 RID: 1248
		[Tooltip("The Transforms value")]
		public SharedTransform[] transforms;

		// Token: 0x040004E1 RID: 1249
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedTransformList storedTransformList;
	}
}
