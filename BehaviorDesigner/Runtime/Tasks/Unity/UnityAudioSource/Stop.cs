using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200027E RID: 638
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stops playing the audio clip. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x00026E74 File Offset: 0x00025074
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00026EB4 File Offset: 0x000250B4
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.Stop();
			return TaskStatus.Success;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00026EDC File Offset: 0x000250DC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040008C3 RID: 2243
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008C4 RID: 2244
		private AudioSource audioSource;

		// Token: 0x040008C5 RID: 2245
		private GameObject prevGameObject;
	}
}
