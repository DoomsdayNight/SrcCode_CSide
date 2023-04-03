using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000229 RID: 553
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Finds a GameObject by name. Returns success if an object is found.")]
	public class Find : Action
	{
		// Token: 0x06000BC5 RID: 3013 RVA: 0x00023F83 File Offset: 0x00022183
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = GameObject.Find(this.gameObjectName.Value);
			if (!(this.storeValue.Value != null))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00023FB6 File Offset: 0x000221B6
		public override void OnReset()
		{
			this.gameObjectName = null;
			this.storeValue = null;
		}

		// Token: 0x0400078B RID: 1931
		[Tooltip("The GameObject name to find")]
		public SharedString gameObjectName;

		// Token: 0x0400078C RID: 1932
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedGameObject storeValue;
	}
}
