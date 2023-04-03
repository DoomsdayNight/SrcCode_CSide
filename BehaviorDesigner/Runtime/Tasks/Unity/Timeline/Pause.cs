using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200012B RID: 299
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Pauses playback of the currently running playable.")]
	public class Pause : Action
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x0001C080 File Offset: 0x0001A280
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				Debug.LogWarning("PlayableDirector is null");
				return TaskStatus.Failure;
			}
			this.playableDirector.Pause();
			return TaskStatus.Success;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400045B RID: 1115
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400045C RID: 1116
		private PlayableDirector playableDirector;

		// Token: 0x0400045D RID: 1117
		private GameObject prevGameObject;
	}
}
