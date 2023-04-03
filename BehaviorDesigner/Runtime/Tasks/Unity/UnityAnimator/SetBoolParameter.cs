using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000291 RID: 657
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the bool parameter on an animator. Returns Success.")]
	public class SetBoolParameter : Action
	{
		// Token: 0x06000D55 RID: 3413 RVA: 0x00027AB4 File Offset: 0x00025CB4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00027AF4 File Offset: 0x00025CF4
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			bool @bool = this.animator.GetBool(this.hashID);
			this.animator.SetBool(this.hashID, this.boolValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(@bool));
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00027B76 File Offset: 0x00025D76
		public IEnumerator ResetValue(bool origVale)
		{
			yield return null;
			this.animator.SetBool(this.hashID, origVale);
			yield break;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00027B8C File Offset: 0x00025D8C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.boolValue = false;
		}

		// Token: 0x0400091C RID: 2332
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400091D RID: 2333
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x0400091E RID: 2334
		[Tooltip("The value of the bool parameter")]
		public SharedBool boolValue;

		// Token: 0x0400091F RID: 2335
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x04000920 RID: 2336
		private int hashID;

		// Token: 0x04000921 RID: 2337
		private Animator animator;

		// Token: 0x04000922 RID: 2338
		private GameObject prevGameObject;
	}
}
