using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200025F RID: 607
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the max distance value of the AudioSource. Returns Success.")]
	public class GetMaxDistance : Action
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x00025C88 File Offset: 0x00023E88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00025CC8 File Offset: 0x00023EC8
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.maxDistance;
			return TaskStatus.Success;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00025CFB File Offset: 0x00023EFB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000849 RID: 2121
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400084A RID: 2122
		[Tooltip("The max distance value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400084B RID: 2123
		private AudioSource audioSource;

		// Token: 0x0400084C RID: 2124
		private GameObject prevGameObject;
	}
}
