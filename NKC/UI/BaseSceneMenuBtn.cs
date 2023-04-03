using System;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000973 RID: 2419
	[Serializable]
	public class BaseSceneMenuBtn
	{
		// Token: 0x04004DBE RID: 19902
		[Header("서브 메뉴별 설정")]
		public NKCUIBaseSceneMenu.BaseSceneMenuType Type;

		// Token: 0x04004DBF RID: 19903
		public GameObject obj;

		// Token: 0x04004DC0 RID: 19904
		public Animator animator;

		// Token: 0x04004DC1 RID: 19905
		public Sprite spBackground;

		// Token: 0x04004DC2 RID: 19906
		public BaseSceneMenuBtn.BaseSceneMenuSubBtn[] subBtn;

		// Token: 0x02001613 RID: 5651
		[Serializable]
		public class BaseSceneMenuSubBtn
		{
			// Token: 0x0400A318 RID: 41752
			public NKC_SCEN_BASE.eUIOpenReserve Type;

			// Token: 0x0400A319 RID: 41753
			public NKCUIComStateButton Btn;

			// Token: 0x0400A31A RID: 41754
			public NKCUIComStateButton LockedBtn;

			// Token: 0x0400A31B RID: 41755
			public GameObject m_objEvent;
		}
	}
}
