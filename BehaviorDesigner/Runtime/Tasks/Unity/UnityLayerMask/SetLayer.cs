using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLayerMask
{
	// Token: 0x02000215 RID: 533
	[TaskCategory("Unity/LayerMask")]
	[TaskDescription("Sets the layer of a GameObject.")]
	public class SetLayer : Action
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x00023AA3 File Offset: 0x00021CA3
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).layer = LayerMask.NameToLayer(this.layerName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00023ACC File Offset: 0x00021CCC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.layerName = "Default";
		}

		// Token: 0x0400076C RID: 1900
		[Tooltip("The GameObject to set the layer of")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400076D RID: 1901
		[Tooltip("The name of the layer to set")]
		public SharedString layerName = "Default";
	}
}
