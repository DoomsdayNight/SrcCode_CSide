using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200022A RID: 554
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Finds a GameObject by tag. Returns Success.")]
	public class FindGameObjectsWithTag : Action
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x00023FD0 File Offset: 0x000221D0
		public override TaskStatus OnUpdate()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(this.tag.Value);
			for (int i = 0; i < array.Length; i++)
			{
				this.storeValue.Value.Add(array[i]);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00024010 File Offset: 0x00022210
		public override void OnReset()
		{
			this.tag.Value = null;
			this.storeValue.Value = null;
		}

		// Token: 0x0400078D RID: 1933
		[Tooltip("The tag of the GameObject to find")]
		public SharedString tag;

		// Token: 0x0400078E RID: 1934
		[Tooltip("The objects found by name")]
		[RequiredField]
		public SharedGameObjectList storeValue;
	}
}
