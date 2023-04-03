using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000BD RID: 189
	[TaskDescription("Sets the field to the value specified. Returns success if the field was set.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class SetFieldValue : Action
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x000170E4 File Offset: 0x000152E4
		public override TaskStatus OnUpdate()
		{
			if (this.fieldValue == null)
			{
				Debug.LogWarning("Unable to get field - field value is null");
				return TaskStatus.Failure;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to set field - type is null");
				return TaskStatus.Failure;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to set the field with component " + this.componentName.Value);
				return TaskStatus.Failure;
			}
			component.GetType().GetField(this.fieldName.Value).SetValue(component, this.fieldValue.GetValue());
			return TaskStatus.Success;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00017190 File Offset: 0x00015390
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.fieldValue = null;
		}

		// Token: 0x040002E3 RID: 739
		[Tooltip("The GameObject to set the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x040002E4 RID: 740
		[Tooltip("The component to set the field on")]
		public SharedString componentName;

		// Token: 0x040002E5 RID: 741
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x040002E6 RID: 742
		[Tooltip("The value to set")]
		public SharedVariable fieldValue;
	}
}
