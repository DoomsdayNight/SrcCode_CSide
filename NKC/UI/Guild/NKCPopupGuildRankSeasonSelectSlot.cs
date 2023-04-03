using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B47 RID: 2887
	public class NKCPopupGuildRankSeasonSelectSlot : MonoBehaviour
	{
		// Token: 0x0600837F RID: 33663 RVA: 0x002C5029 File Offset: 0x002C3229
		public void InitUI()
		{
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
		}

		// Token: 0x06008380 RID: 33664 RVA: 0x002C5057 File Offset: 0x002C3257
		public void SetData(int seasonId, string seasonName, bool bSelected, NKCPopupGuildRankSeasonSelectSlot.OnClick dOnClick)
		{
			this.m_seasonId = seasonId;
			this.m_dOnClick = dOnClick;
			this.m_btn.Select(bSelected, true, true);
			NKCUtil.SetLabelText(this.m_lbSeasonNameOn, seasonName);
			NKCUtil.SetLabelText(this.m_lbSeasonNameOff, seasonName);
		}

		// Token: 0x06008381 RID: 33665 RVA: 0x002C508F File Offset: 0x002C328F
		public void OnClickSlot()
		{
			NKCPopupGuildRankSeasonSelectSlot.OnClick dOnClick = this.m_dOnClick;
			if (dOnClick == null)
			{
				return;
			}
			dOnClick(this.m_seasonId);
		}

		// Token: 0x04006FA4 RID: 28580
		public NKCUIComStateButton m_btn;

		// Token: 0x04006FA5 RID: 28581
		public Text m_lbSeasonNameOn;

		// Token: 0x04006FA6 RID: 28582
		public Text m_lbSeasonNameOff;

		// Token: 0x04006FA7 RID: 28583
		private NKCPopupGuildRankSeasonSelectSlot.OnClick m_dOnClick;

		// Token: 0x04006FA8 RID: 28584
		private int m_seasonId;

		// Token: 0x020018DE RID: 6366
		// (Invoke) Token: 0x0600B6FD RID: 46845
		public delegate void OnClick(int seasonId);
	}
}
