using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200022B RID: 555
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Finds a GameObject by tag. Returns success if an object is found.")]
	public class FindWithTag : Action
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x00024034 File Offset: 0x00022234
		public override TaskStatus OnUpdate()
		{
			if (this.random.Value)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag(this.tag.Value);
				if (array == null || array.Length == 0)
				{
					return TaskStatus.Failure;
				}
				this.storeValue.Value = array[UnityEngine.Random.Range(0, array.Length)];
			}
			else
			{
				this.storeValue.Value = GameObject.FindWithTag(this.tag.Value);
			}
			if (!(this.storeValue.Value != null))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000240B1 File Offset: 0x000222B1
		public override void OnReset()
		{
			this.tag.Value = null;
			this.storeValue.Value = null;
		}

		// Token: 0x0400078F RID: 1935
		[Tooltip("The tag of the GameObject to find")]
		public SharedString tag;

		// Token: 0x04000790 RID: 1936
		[Tooltip("Should a random GameObject be found?")]
		public SharedBool random;

		// Token: 0x04000791 RID: 1937
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedGameObject storeValue;
	}
}
