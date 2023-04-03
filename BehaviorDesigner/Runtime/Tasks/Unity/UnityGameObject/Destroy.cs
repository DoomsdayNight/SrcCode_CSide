using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000227 RID: 551
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Destorys the specified GameObject. Returns Success.")]
	public class Destroy : Action
	{
		// Token: 0x06000BBF RID: 3007 RVA: 0x00023EFC File Offset: 0x000220FC
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (this.time == 0f)
			{
				UnityEngine.Object.Destroy(defaultGameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(defaultGameObject, this.time);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00023F3D File Offset: 0x0002213D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x04000788 RID: 1928
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000789 RID: 1929
		[Tooltip("Time to destroy the GameObject in")]
		public float time;
	}
}
