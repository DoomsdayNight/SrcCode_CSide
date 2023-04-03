using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200026F RID: 623
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the ignore listener volume value of the AudioSource. Returns Success.")]
	public class SetIgnoreListenerVolume : Action
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x000265CC File Offset: 0x000247CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002660C File Offset: 0x0002480C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.ignoreListenerVolume = this.ignoreListenerVolume.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002663F File Offset: 0x0002483F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.ignoreListenerVolume = false;
		}

		// Token: 0x04000887 RID: 2183
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000888 RID: 2184
		[Tooltip("The ignore listener volume value of the AudioSource")]
		public SharedBool ignoreListenerVolume;

		// Token: 0x04000889 RID: 2185
		private AudioSource audioSource;

		// Token: 0x0400088A RID: 2186
		private GameObject prevGameObject;
	}
}
