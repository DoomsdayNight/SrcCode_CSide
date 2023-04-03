using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200025C RID: 604
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the ignore listener pause value of the AudioSource. Returns Success.")]
	public class GetIgnoreListenerPause : Action
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x00025AD8 File Offset: 0x00023CD8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00025B18 File Offset: 0x00023D18
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.ignoreListenerPause;
			return TaskStatus.Success;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00025B4B File Offset: 0x00023D4B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x0400083D RID: 2109
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400083E RID: 2110
		[Tooltip("The ignore listener pause value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400083F RID: 2111
		private AudioSource audioSource;

		// Token: 0x04000840 RID: 2112
		private GameObject prevGameObject;
	}
}
