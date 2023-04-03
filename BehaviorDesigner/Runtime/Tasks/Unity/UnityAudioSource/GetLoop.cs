using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200025E RID: 606
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the loop value of the AudioSource. Returns Success.")]
	public class GetLoop : Action
	{
		// Token: 0x06000C8A RID: 3210 RVA: 0x00025BF8 File Offset: 0x00023DF8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00025C38 File Offset: 0x00023E38
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.loop;
			return TaskStatus.Success;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00025C6B File Offset: 0x00023E6B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04000845 RID: 2117
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000846 RID: 2118
		[Tooltip("The loop value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04000847 RID: 2119
		private AudioSource audioSource;

		// Token: 0x04000848 RID: 2120
		private GameObject prevGameObject;
	}
}
