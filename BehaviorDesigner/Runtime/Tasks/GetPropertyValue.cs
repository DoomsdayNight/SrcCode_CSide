using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000BB RID: 187
	[TaskDescription("Gets the value from the property specified. Returns success if the property was retrieved.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class GetPropertyValue : Action
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x00016E64 File Offset: 0x00015064
		public override TaskStatus OnUpdate()
		{
			if (this.propertyValue == null)
			{
				Debug.LogWarning("Unable to get property - property value is null");
				return TaskStatus.Failure;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to get property - type is null");
				return TaskStatus.Failure;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to get the property with component " + this.componentName.Value);
				return TaskStatus.Failure;
			}
			PropertyInfo property = component.GetType().GetProperty(this.propertyName.Value);
			this.propertyValue.SetValue(property.GetValue(component, null));
			return TaskStatus.Success;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00016F13 File Offset: 0x00015113
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.propertyValue = null;
		}

		// Token: 0x040002D7 RID: 727
		[Tooltip("The GameObject to get the property of")]
		public SharedGameObject targetGameObject;

		// Token: 0x040002D8 RID: 728
		[Tooltip("The component to get the property of")]
		public SharedString componentName;

		// Token: 0x040002D9 RID: 729
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x040002DA RID: 730
		[Tooltip("The value of the property")]
		[RequiredField]
		public SharedVariable propertyValue;
	}
}
