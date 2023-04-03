using System;
using UnityEngine;

namespace NKC.UI.Option
{
	// Token: 0x02000B8B RID: 2955
	public abstract class NKCUIGameOptionContentBase : MonoBehaviour
	{
		// Token: 0x06008873 RID: 34931
		public abstract void Init();

		// Token: 0x06008874 RID: 34932
		public abstract void SetContent();

		// Token: 0x06008875 RID: 34933 RVA: 0x002E2AB2 File Offset: 0x002E0CB2
		public virtual bool Processhotkey(HotkeyEventType eventType)
		{
			return false;
		}
	}
}
