using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000261 RID: 609
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the mute value of the AudioSource. Returns Success.")]
	public class GetMute : Action
	{
		// Token: 0x06000C96 RID: 3222 RVA: 0x00025DB0 File Offset: 0x00023FB0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00025DF0 File Offset: 0x00023FF0
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.mute;
			return TaskStatus.Success;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00025E23 File Offset: 0x00024023
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04000851 RID: 2129
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000852 RID: 2130
		[Tooltip("The mute value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04000853 RID: 2131
		private AudioSource audioSource;

		// Token: 0x04000854 RID: 2132
		private GameObject prevGameObject;
	}
}
