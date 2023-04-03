using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000BA RID: 186
	[TaskDescription("Gets the value from the field specified. Returns success if the field was retrieved.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class GetFieldValue : Action
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x00016D90 File Offset: 0x00014F90
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
				Debug.LogWarning("Unable to get field - type is null");
				return TaskStatus.Failure;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to get the field with component " + this.componentName.Value);
				return TaskStatus.Failure;
			}
			FieldInfo field = component.GetType().GetField(this.fieldName.Value);
			this.fieldValue.SetValue(field.GetValue(component));
			return TaskStatus.Success;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00016E3E File Offset: 0x0001503E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.fieldValue = null;
		}

		// Token: 0x040002D3 RID: 723
		[Tooltip("The GameObject to get the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x040002D4 RID: 724
		[Tooltip("The component to get the field on")]
		public SharedString componentName;

		// Token: 0x040002D5 RID: 725
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x040002D6 RID: 726
		[Tooltip("The value of the field")]
		[RequiredField]
		public SharedVariable fieldValue;
	}
}
