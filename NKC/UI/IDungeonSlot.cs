using System;

namespace NKC.UI
{
	// Token: 0x02000983 RID: 2435
	public interface IDungeonSlot
	{
		// Token: 0x060063DA RID: 25562
		void SetSelectNode(bool bValue);

		// Token: 0x02001634 RID: 5684
		// (Invoke) Token: 0x0600AF7B RID: 44923
		public delegate void OnSelectedItemSlot(int dunIndex, string dunStrID, bool isPlaying);
	}
}
