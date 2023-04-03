using System;
using System.Reflection;

namespace NLua.Method
{
	// Token: 0x02000077 RID: 119
	internal class RegisterEventHandler
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x000146EF File Offset: 0x000128EF
		public RegisterEventHandler(EventHandlerContainer pendingEvents, object target, EventInfo eventInfo)
		{
			this._target = target;
			this._eventInfo = eventInfo;
			this._pendingEvents = pendingEvents;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001470C File Offset: 0x0001290C
		public Delegate Add(LuaFunction function)
		{
			Delegate @delegate = CodeGeneration.Instance.GetDelegate(this._eventInfo.EventHandlerType, function);
			return this.Add(@delegate);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00014737 File Offset: 0x00012937
		public Delegate Add(Delegate handlerDelegate)
		{
			this._eventInfo.AddEventHandler(this._target, handlerDelegate);
			this._pendingEvents.Add(handlerDelegate, this);
			return handlerDelegate;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00014759 File Offset: 0x00012959
		public void Remove(Delegate handlerDelegate)
		{
			this.RemovePending(handlerDelegate);
			this._pendingEvents.Remove(handlerDelegate);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001476E File Offset: 0x0001296E
		internal void RemovePending(Delegate handlerDelegate)
		{
			this._eventInfo.RemoveEventHandler(this._target, handlerDelegate);
		}

		// Token: 0x0400025A RID: 602
		private readonly EventHandlerContainer _pendingEvents;

		// Token: 0x0400025B RID: 603
		private readonly EventInfo _eventInfo;

		// Token: 0x0400025C RID: 604
		private readonly object _target;
	}
}
