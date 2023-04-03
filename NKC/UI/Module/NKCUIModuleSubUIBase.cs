using System;
using NKM.Event;
using UnityEngine;

namespace NKC.UI.Module
{
	// Token: 0x02000B22 RID: 2850
	public class NKCUIModuleSubUIBase : MonoBehaviour
	{
		// Token: 0x060081D9 RID: 33241 RVA: 0x002BC395 File Offset: 0x002BA595
		public virtual void OnOpen(NKMEventCollectionIndexTemplet templet)
		{
		}

		// Token: 0x060081DA RID: 33242 RVA: 0x002BC397 File Offset: 0x002BA597
		public virtual void OnOpen(NKMEventTabTemplet eventTabTemplet)
		{
		}

		// Token: 0x060081DB RID: 33243 RVA: 0x002BC399 File Offset: 0x002BA599
		public virtual void OnClose()
		{
		}

		// Token: 0x060081DC RID: 33244 RVA: 0x002BC39B File Offset: 0x002BA59B
		public virtual void Init()
		{
		}

		// Token: 0x060081DD RID: 33245 RVA: 0x002BC39D File Offset: 0x002BA59D
		public virtual void Refresh()
		{
		}

		// Token: 0x060081DE RID: 33246 RVA: 0x002BC39F File Offset: 0x002BA59F
		public virtual void UnHide()
		{
		}

		// Token: 0x04006DF5 RID: 28149
		public int ModuleID = -1;
	}
}
