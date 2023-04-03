using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000293 RID: 659
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the int parameter on an animator. Returns Success.")]
	public class SetIntegerParameter : Action
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x00027CC8 File Offset: 0x00025EC8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00027D08 File Offset: 0x00025F08
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			int integer = this.animator.GetInteger(this.hashID);
			this.animator.SetInteger(this.hashID, this.intValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(integer));
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00027D8A File Offset: 0x00025F8A
		public IEnumerator ResetValue(int origVale)
		{
			yield return null;
			this.animator.SetInteger(this.hashID, origVale);
			yield break;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00027DA0 File Offset: 0x00025FA0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.intValue = 0;
		}

		// Token: 0x0400092A RID: 2346
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400092B RID: 2347
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x0400092C RID: 2348
		[Tooltip("The value of the int parameter")]
		public SharedInt intValue;

		// Token: 0x0400092D RID: 2349
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x0400092E RID: 2350
		private int hashID;

		// Token: 0x0400092F RID: 2351
		private Animator animator;

		// Token: 0x04000930 RID: 2352
		private GameObject prevGameObject;
	}
}
