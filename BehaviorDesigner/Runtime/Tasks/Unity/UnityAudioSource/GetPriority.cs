using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000263 RID: 611
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the priority value of the AudioSource. Returns Success.")]
	public class GetPriority : Action
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x00025ED4 File Offset: 0x000240D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00025F14 File Offset: 0x00024114
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.priority;
			return TaskStatus.Success;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00025F47 File Offset: 0x00024147
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1;
		}

		// Token: 0x04000859 RID: 2137
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400085A RID: 2138
		[Tooltip("The priority value of the AudioSource")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x0400085B RID: 2139
		private AudioSource audioSource;

		// Token: 0x0400085C RID: 2140
		private GameObject prevGameObject;
	}
}
