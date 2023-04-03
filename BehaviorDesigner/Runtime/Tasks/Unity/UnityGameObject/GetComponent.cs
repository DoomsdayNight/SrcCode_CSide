using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200022C RID: 556
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns the component of Type type if the game object has one attached, null if it doesn't. Returns Success.")]
	public class GetComponent : Action
	{
		// Token: 0x06000BCE RID: 3022 RVA: 0x000240D3 File Offset: 0x000222D3
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(this.type.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00024102 File Offset: 0x00022302
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.type.Value = "";
			this.storeValue.Value = null;
		}

		// Token: 0x04000792 RID: 1938
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000793 RID: 1939
		[Tooltip("The type of component")]
		public SharedString type;

		// Token: 0x04000794 RID: 1940
		[Tooltip("The component")]
		[RequiredField]
		public SharedObject storeValue;
	}
}
