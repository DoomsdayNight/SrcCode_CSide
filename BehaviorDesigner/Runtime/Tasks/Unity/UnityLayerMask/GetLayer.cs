using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLayerMask
{
	// Token: 0x02000214 RID: 532
	[TaskCategory("Unity/LayerMask")]
	[TaskDescription("Gets the layer of a GameObject.")]
	public class GetLayer : Action
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00023A4C File Offset: 0x00021C4C
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			this.storeResult.Value = LayerMask.LayerToName(defaultGameObject.layer);
			return TaskStatus.Success;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00023A82 File Offset: 0x00021C82
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = "";
		}

		// Token: 0x0400076A RID: 1898
		[Tooltip("The GameObject to set the layer of")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400076B RID: 1899
		[Tooltip("The name of the layer to get")]
		[RequiredField]
		public SharedString storeResult;
	}
}
