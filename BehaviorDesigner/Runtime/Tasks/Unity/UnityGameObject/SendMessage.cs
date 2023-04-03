using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200022F RID: 559
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Sends a message to the target GameObject. Returns Success.")]
	public class SendMessage : Action
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x000241EC File Offset: 0x000223EC
		public override TaskStatus OnUpdate()
		{
			if (this.value.Value != null)
			{
				base.GetDefaultGameObject(this.targetGameObject.Value).SendMessage(this.message.Value, this.value.Value.value.GetValue());
			}
			else
			{
				base.GetDefaultGameObject(this.targetGameObject.Value).SendMessage(this.message.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00024260 File Offset: 0x00022460
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.message = "";
		}

		// Token: 0x0400079B RID: 1947
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400079C RID: 1948
		[Tooltip("The message to send")]
		public SharedString message;

		// Token: 0x0400079D RID: 1949
		[Tooltip("The value to send")]
		public SharedGenericVariable value;
	}
}
