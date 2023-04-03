using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200012D RID: 301
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Resume playing a paused playable.")]
	public class Resume : Action
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
			this.playbackStarted = false;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001C244 File Offset: 0x0001A444
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				Debug.LogWarning("PlayableDirector is null");
				return TaskStatus.Failure;
			}
			if (this.playbackStarted)
			{
				if (this.stopWhenComplete.Value && this.playableDirector.state == PlayState.Playing)
				{
					return TaskStatus.Running;
				}
				return TaskStatus.Success;
			}
			else
			{
				this.playableDirector.Resume();
				this.playbackStarted = true;
				if (!this.stopWhenComplete.Value)
				{
					return TaskStatus.Success;
				}
				return TaskStatus.Running;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001C2B4 File Offset: 0x0001A4B4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stopWhenComplete = false;
		}

		// Token: 0x04000464 RID: 1124
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000465 RID: 1125
		[Tooltip("Should the task be stopped when the timeline has stopped playing?")]
		public SharedBool stopWhenComplete;

		// Token: 0x04000466 RID: 1126
		private PlayableDirector playableDirector;

		// Token: 0x04000467 RID: 1127
		private GameObject prevGameObject;

		// Token: 0x04000468 RID: 1128
		private bool playbackStarted;
	}
}
