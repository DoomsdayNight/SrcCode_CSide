using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000279 RID: 633
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Changes the time at which a sound that has already been scheduled to play will start. Returns Success.")]
	public class SetScheduledStartTime : Action
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x00026B88 File Offset: 0x00024D88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00026BC8 File Offset: 0x00024DC8
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.SetScheduledStartTime((double)this.time.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00026BFC File Offset: 0x00024DFC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040008AF RID: 2223
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008B0 RID: 2224
		[Tooltip("Time in seconds")]
		public SharedFloat time = 0f;

		// Token: 0x040008B1 RID: 2225
		private AudioSource audioSource;

		// Token: 0x040008B2 RID: 2226
		private GameObject prevGameObject;
	}
}
