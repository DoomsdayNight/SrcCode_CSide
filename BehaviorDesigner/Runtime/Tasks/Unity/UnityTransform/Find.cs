using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200010A RID: 266
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Finds a transform by name. Returns success if an object is found.")]
	public class Find : Action
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x0001ABF8 File Offset: 0x00018DF8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001AC38 File Offset: 0x00018E38
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.Find(this.transformName.Value);
			if (!(this.storeValue.Value != null))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001AC96 File Offset: 0x00018E96
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.transformName = null;
			this.storeValue = null;
		}

		// Token: 0x040003CE RID: 974
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003CF RID: 975
		[Tooltip("The transform name to find")]
		public SharedString transformName;

		// Token: 0x040003D0 RID: 976
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x040003D1 RID: 977
		private Transform targetTransform;

		// Token: 0x040003D2 RID: 978
		private GameObject prevGameObject;
	}
}
