using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000270 RID: 624
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the ignore listener pause value of the AudioSource. Returns Success.")]
	public class SetIgnoreListenerPause : Action
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002665C File Offset: 0x0002485C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002669C File Offset: 0x0002489C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.ignoreListenerPause = this.ignoreListenerPause.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000266CF File Offset: 0x000248CF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.ignoreListenerPause = false;
		}

		// Token: 0x0400088B RID: 2187
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400088C RID: 2188
		[Tooltip("The ignore listener pause value of the AudioSource")]
		public SharedBool ignoreListenerPause;

		// Token: 0x0400088D RID: 2189
		private AudioSource audioSource;

		// Token: 0x0400088E RID: 2190
		private GameObject prevGameObject;
	}
}
