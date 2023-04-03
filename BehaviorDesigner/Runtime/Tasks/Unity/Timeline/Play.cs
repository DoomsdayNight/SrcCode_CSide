using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200012C RID: 300
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Instatiates a Playable using the provided PlayableAsset and starts playback.")]
	public class Play : Action
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x0001C0FC File Offset: 0x0001A2FC
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

		// Token: 0x06000849 RID: 2121 RVA: 0x0001C144 File Offset: 0x0001A344
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
				if (this.playableAsset == null)
				{
					this.playableDirector.Play();
				}
				else
				{
					this.playableDirector.Play(this.playableAsset);
				}
				this.playbackStarted = true;
				if (!this.stopWhenComplete.Value)
				{
					return TaskStatus.Success;
				}
				return TaskStatus.Running;
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001C1D5 File Offset: 0x0001A3D5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.playableAsset = null;
			this.stopWhenComplete = false;
		}

		// Token: 0x0400045E RID: 1118
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400045F RID: 1119
		[Tooltip("An asset to instantiate a playable from.")]
		public PlayableAsset playableAsset;

		// Token: 0x04000460 RID: 1120
		[Tooltip("Should the task be stopped when the timeline has stopped playing?")]
		public SharedBool stopWhenComplete;

		// Token: 0x04000461 RID: 1121
		private PlayableDirector playableDirector;

		// Token: 0x04000462 RID: 1122
		private GameObject prevGameObject;

		// Token: 0x04000463 RID: 1123
		private bool playbackStarted;
	}
}
