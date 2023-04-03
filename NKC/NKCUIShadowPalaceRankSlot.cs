using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009DF RID: 2527
	public class NKCUIShadowPalaceRankSlot : MonoBehaviour
	{
		// Token: 0x06006C8F RID: 27791 RVA: 0x00237780 File Offset: 0x00235980
		public void Init()
		{
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x00237784 File Offset: 0x00235984
		public void SetData(LeaderBoardSlotData rankData, int rank, bool bMyRank)
		{
			NKCUtil.SetLabelText(this.m_txtRank, rank.ToString());
			NKCUtil.SetLabelText(this.m_txtLevel, NKCStringTable.GetString("SI_DP_LEVEL_ONE_PARAM", new object[]
			{
				rankData.level
			}));
			NKCUtil.SetLabelText(this.m_txtName, rankData.nickname);
			NKCUtil.SetLabelText(this.m_txtScore, rankData.score);
			this.m_unitSlot.SetProfiledata(rankData.Profile, null);
			Sprite sprite = null;
			switch (rank)
			{
			case 1:
				sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_SHADOW_SPRITE", "Rank_01", false);
				break;
			case 2:
				sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_SHADOW_SPRITE", "Rank_02", false);
				break;
			case 3:
				sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_SHADOW_SPRITE", "Rank_03", false);
				break;
			}
			this.m_imgRank.enabled = (sprite != null);
			if (sprite != null)
			{
				this.m_imgRank.sprite = sprite;
			}
			NKCUtil.SetGameobjectActive(this.m_objMyRank, bMyRank);
		}

		// Token: 0x0400584A RID: 22602
		public Text m_txtRank;

		// Token: 0x0400584B RID: 22603
		public Text m_txtLevel;

		// Token: 0x0400584C RID: 22604
		public Text m_txtName;

		// Token: 0x0400584D RID: 22605
		public Text m_txtScore;

		// Token: 0x0400584E RID: 22606
		public NKCUISlotProfile m_unitSlot;

		// Token: 0x0400584F RID: 22607
		public Image m_imgRank;

		// Token: 0x04005850 RID: 22608
		public GameObject m_objMyRank;
	}
}
