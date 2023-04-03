using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000BE RID: 190
	[TaskDescription("Sets the property to the value specified. Returns success if the property was set.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class SetPropertyValue : Action
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x000171B8 File Offset: 0x000153B8
		public override TaskStatus OnUpdate()
		{
			if (this.propertyValue == null)
			{
				Debug.LogWarning("Unable to get field - field value is null");
				return TaskStatus.Failure;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to set property - type is null");
				return TaskStatus.Failure;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to set the property with component " + this.componentName.Value);
				return TaskStatus.Failure;
			}
			component.GetType().GetProperty(this.propertyName.Value).SetValue(component, this.propertyValue.GetValue(), null);
			return TaskStatus.Success;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00017265 File Offset: 0x00015465
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.propertyValue = null;
		}

		// Token: 0x040002E7 RID: 743
		[Tooltip("The GameObject to set the property on")]
		public SharedGameObject targetGameObject;

		// Token: 0x040002E8 RID: 744
		[Tooltip("The component to set the property on")]
		public SharedString componentName;

		// Token: 0x040002E9 RID: 745
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x040002EA RID: 746
		[Tooltip("The value to set")]
		public SharedVariable propertyValue;
	}
}
