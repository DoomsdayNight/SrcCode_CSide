using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000260 RID: 608
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the min distance value of the AudioSource. Returns Success.")]
	public class GetMinDistance : Action
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x00025D1C File Offset: 0x00023F1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00025D5C File Offset: 0x00023F5C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.minDistance;
			return TaskStatus.Success;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00025D8F File Offset: 0x00023F8F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x0400084D RID: 2125
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400084E RID: 2126
		[Tooltip("The min distance value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400084F RID: 2127
		private AudioSource audioSource;

		// Token: 0x04000850 RID: 2128
		private GameObject prevGameObject;
	}
}
