using System;
using System.Collections.Generic;

namespace NLua.Method
{
	// Token: 0x0200006F RID: 111
	internal class EventHandlerContainer : IDisposable
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x00013D30 File Offset: 0x00011F30
		public void Add(Delegate handler, RegisterEventHandler eventInfo)
		{
			this._dict.Add(handler, eventInfo);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00013D3F File Offset: 0x00011F3F
		public void Remove(Delegate handler)
		{
			this._dict.Remove(handler);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00013D50 File Offset: 0x00011F50
		public void Dispose()
		{
			foreach (KeyValuePair<Delegate, RegisterEventHandler> keyValuePair in this._dict)
			{
				keyValuePair.Value.RemovePending(keyValuePair.Key);
			}
			this._dict.Clear();
		}

		// Token: 0x04000244 RID: 580
		private readonly Dictionary<Delegate, RegisterEventHandler> _dict = new Dictionary<Delegate, RegisterEventHandler>();
	}
}
