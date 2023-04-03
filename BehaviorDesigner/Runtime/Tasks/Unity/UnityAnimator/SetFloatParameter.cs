using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000292 RID: 658
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the float parameter on an animator. Returns Success.")]
	public class SetFloatParameter : Action
	{
		// Token: 0x06000D5A RID: 3418 RVA: 0x00027BBC File Offset: 0x00025DBC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00027BFC File Offset: 0x00025DFC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			float @float = this.animator.GetFloat(this.hashID);
			this.animator.SetFloat(this.hashID, this.floatValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(@float));
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00027C7E File Offset: 0x00025E7E
		public IEnumerator ResetValue(float origVale)
		{
			yield return null;
			this.animator.SetFloat(this.hashID, origVale);
			yield break;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00027C94 File Offset: 0x00025E94
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.floatValue = 0f;
		}

		// Token: 0x04000923 RID: 2339
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000924 RID: 2340
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04000925 RID: 2341
		[Tooltip("The value of the float parameter")]
		public SharedFloat floatValue;

		// Token: 0x04000926 RID: 2342
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x04000927 RID: 2343
		private int hashID;

		// Token: 0x04000928 RID: 2344
		private Animator animator;

		// Token: 0x04000929 RID: 2345
		private GameObject prevGameObject;
	}
}
