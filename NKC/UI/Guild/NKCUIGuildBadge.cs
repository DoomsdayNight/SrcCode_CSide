using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B4B RID: 2891
	public class NKCUIGuildBadge : MonoBehaviour
	{
		// Token: 0x0600839A RID: 33690 RVA: 0x002C5882 File Offset: 0x002C3A82
		public void InitUI()
		{
			this.SetData(new GuildBadgeInfo(0L));
		}

		// Token: 0x0600839B RID: 33691 RVA: 0x002C5891 File Offset: 0x002C3A91
		public void SetData(long badgeId)
		{
			this.SetData(new GuildBadgeInfo(badgeId));
		}

		// Token: 0x0600839C RID: 33692 RVA: 0x002C589F File Offset: 0x002C3A9F
		public void SetData(long badgeId, bool bOpponent)
		{
			this.SetData(new GuildBadgeInfo(badgeId), bOpponent);
		}

		// Token: 0x0600839D RID: 33693 RVA: 0x002C58AE File Offset: 0x002C3AAE
		public void SetData(int frameId, int frameColorId, int markId, int markColorId)
		{
			this.SetData(new GuildBadgeInfo(frameId, frameColorId, markId, markColorId));
		}

		// Token: 0x0600839E RID: 33694 RVA: 0x002C58C0 File Offset: 0x002C3AC0
		public void SetData(GuildBadgeInfo guildBadgeInfo)
		{
			NKMGuildBadgeFrameTemplet nkmguildBadgeFrameTemplet = NKMGuildBadgeFrameTemplet.Find(guildBadgeInfo.FrameId);
			if (nkmguildBadgeFrameTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgFrame, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Mark", nkmguildBadgeFrameTemplet.BadgeFrameImg, false), false);
			}
			NKMGuildBadgeColorTemplet nkmguildBadgeColorTemplet = NKMGuildBadgeColorTemplet.Find(guildBadgeInfo.FrameColorId);
			if (nkmguildBadgeColorTemplet != null)
			{
				NKCUtil.SetImageColor(this.m_imgFrame, NKCUtil.GetColor(nkmguildBadgeColorTemplet.BadgeColorCode));
			}
			NKMGuildBadgeMarkTemplet nkmguildBadgeMarkTemplet = NKMGuildBadgeMarkTemplet.Find(guildBadgeInfo.MarkId);
			if (nkmguildBadgeMarkTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgMark, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Mark", nkmguildBadgeMarkTemplet.BadgeMarkImg, false), false);
			}
			nkmguildBadgeColorTemplet = NKMGuildBadgeColorTemplet.Find(guildBadgeInfo.MarkColorId);
			if (nkmguildBadgeColorTemplet != null)
			{
				NKCUtil.SetImageColor(this.m_imgMark, NKCUtil.GetColor(nkmguildBadgeColorTemplet.BadgeColorCode));
			}
		}

		// Token: 0x0600839F RID: 33695 RVA: 0x002C5970 File Offset: 0x002C3B70
		public void SetData(GuildBadgeInfo guildBadgeInfo, bool bOpponent)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				if (bOpponent)
				{
					if (gameOptionData.StreamingHideOpponentInfo)
					{
						this.SetDataHide();
						return;
					}
				}
				else if (gameOptionData.StreamingHideMyInfo)
				{
					this.SetDataHide();
					return;
				}
			}
			NKMGuildBadgeFrameTemplet nkmguildBadgeFrameTemplet = NKMGuildBadgeFrameTemplet.Find(guildBadgeInfo.FrameId);
			if (nkmguildBadgeFrameTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgFrame, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Mark", nkmguildBadgeFrameTemplet.BadgeFrameImg, false), false);
			}
			NKMGuildBadgeColorTemplet nkmguildBadgeColorTemplet = NKMGuildBadgeColorTemplet.Find(guildBadgeInfo.FrameColorId);
			if (nkmguildBadgeColorTemplet != null)
			{
				NKCUtil.SetImageColor(this.m_imgFrame, NKCUtil.GetColor(nkmguildBadgeColorTemplet.BadgeColorCode));
			}
			NKMGuildBadgeMarkTemplet nkmguildBadgeMarkTemplet = NKMGuildBadgeMarkTemplet.Find(guildBadgeInfo.MarkId);
			if (nkmguildBadgeMarkTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgMark, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Mark", nkmguildBadgeMarkTemplet.BadgeMarkImg, false), false);
			}
			nkmguildBadgeColorTemplet = NKMGuildBadgeColorTemplet.Find(guildBadgeInfo.MarkColorId);
			if (nkmguildBadgeColorTemplet != null)
			{
				NKCUtil.SetImageColor(this.m_imgMark, NKCUtil.GetColor(nkmguildBadgeColorTemplet.BadgeColorCode));
			}
		}

		// Token: 0x060083A0 RID: 33696 RVA: 0x002C5A50 File Offset: 0x002C3C50
		public void SetDataHide()
		{
			NKMGuildBadgeFrameTemplet nkmguildBadgeFrameTemplet = NKMGuildBadgeFrameTemplet.Find(0);
			if (nkmguildBadgeFrameTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgFrame, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Mark", nkmguildBadgeFrameTemplet.BadgeFrameImg, false), false);
			}
			NKMGuildBadgeColorTemplet nkmguildBadgeColorTemplet = NKMGuildBadgeColorTemplet.Find(0);
			if (nkmguildBadgeColorTemplet != null)
			{
				NKCUtil.SetImageColor(this.m_imgFrame, NKCUtil.GetColor(nkmguildBadgeColorTemplet.BadgeColorCode));
			}
			NKMGuildBadgeMarkTemplet nkmguildBadgeMarkTemplet = NKMGuildBadgeMarkTemplet.Find(0);
			if (nkmguildBadgeMarkTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgMark, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Mark", nkmguildBadgeMarkTemplet.BadgeMarkImg, false), false);
			}
			nkmguildBadgeColorTemplet = NKMGuildBadgeColorTemplet.Find(0);
			if (nkmguildBadgeColorTemplet != null)
			{
				NKCUtil.SetImageColor(this.m_imgMark, NKCUtil.GetColor(nkmguildBadgeColorTemplet.BadgeColorCode));
			}
		}

		// Token: 0x04006FC9 RID: 28617
		public Image m_imgFrame;

		// Token: 0x04006FCA RID: 28618
		public Image m_imgMark;
	}
}
