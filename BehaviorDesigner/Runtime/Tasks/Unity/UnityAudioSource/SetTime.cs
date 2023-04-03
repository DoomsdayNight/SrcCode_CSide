using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200027B RID: 635
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the time value of the AudioSource. Returns Success.")]
	public class SetTime : Action
	{
		// Token: 0x06000CFE RID: 3326 RVA: 0x00026CC4 File Offset: 0x00024EC4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00026D04 File Offset: 0x00024F04
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.time = this.time.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00026D37 File Offset: 0x00024F37
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 1f;
		}

		// Token: 0x040008B7 RID: 2231
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008B8 RID: 2232
		[Tooltip("The time value of the AudioSource")]
		public SharedFloat time;

		// Token: 0x040008B9 RID: 2233
		private AudioSource audioSource;

		// Token: 0x040008BA RID: 2234
		private GameObject prevGameObject;
	}
}
