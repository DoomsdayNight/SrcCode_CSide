using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200025D RID: 605
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the ignore listener volume value of the AudioSource. Returns Success.")]
	public class GetIgnoreListenerVolume : Action
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x00025B68 File Offset: 0x00023D68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00025BA8 File Offset: 0x00023DA8
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.ignoreListenerVolume;
			return TaskStatus.Success;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00025BDB File Offset: 0x00023DDB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04000841 RID: 2113
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000842 RID: 2114
		[Tooltip("The ignore listener volume value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04000843 RID: 2115
		private AudioSource audioSource;

		// Token: 0x04000844 RID: 2116
		private GameObject prevGameObject;
	}
}
