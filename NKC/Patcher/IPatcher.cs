using System;
using System.Collections;

namespace NKC.Patcher
{
	// Token: 0x02000885 RID: 2181
	public interface IPatcher
	{
		// Token: 0x060056DB RID: 22235
		void SetActive(bool active);

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060056DC RID: 22236
		bool PatchSuccess { get; }

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060056DD RID: 22237
		string ReasonOfFailure { get; }

		// Token: 0x060056DE RID: 22238
		IEnumerator ProcessPatch();
	}
}
