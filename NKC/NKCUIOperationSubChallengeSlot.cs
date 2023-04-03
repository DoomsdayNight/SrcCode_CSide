using System;
using NKC.UI.Trim;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A05 RID: 2565
	public class NKCUIOperationSubChallengeSlot : MonoBehaviour
	{
		// Token: 0x06007004 RID: 28676 RVA: 0x002515A0 File Offset: 0x0024F7A0
		public void SetData(NKMEpisodeTempletV2 epTemplet, NKCUIOperationSubChallengeSlot.OnClickSlot onClickSlot)
		{
			this.m_dOnClickSlot = onClickSlot;
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
			this.m_Key = epTemplet.m_EpisodeID;
			NKCUtil.SetImageSprite(this.m_imgThumbnail, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", epTemplet.m_EPThumbnail, false), false);
			if (this.m_lbTitle != null && !string.IsNullOrEmpty(epTemplet.m_EpisodeName))
			{
				NKCUtil.SetLabelText(this.m_lbTitle, epTemplet.GetEpisodeName());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbTitle, "");
			}
			if (this.m_lbDesc != null && !string.IsNullOrEmpty(epTemplet.m_EpisodeDesc))
			{
				NKCUtil.SetLabelText(this.m_lbDesc, epTemplet.GetEpisodeDesc());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbDesc, "");
			}
			if (this.m_lbRewardDesc != null && !string.IsNullOrEmpty(epTemplet.m_EpisodeDescSub))
			{
				NKCUtil.SetLabelText(this.m_lbRewardDesc, epTemplet.GetEpisodeDescSub());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbRewardDesc, "");
			}
			NKCUtil.SetGameobjectActive(this.m_objLock, !NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), epTemplet));
			this.m_strLockedMessage = NKCUtilString.GetUnlockConditionRequireDesc(epTemplet.GetUnlockInfo(), false);
			NKMStageTempletV2 firstStage = epTemplet.GetFirstStage(1);
			if (firstStage != null)
			{
				if (firstStage.m_StageReqItemID > 0)
				{
					NKCUtil.SetGameobjectActive(this.m_objResource, true);
					NKCUtil.SetImageSprite(this.m_imgResourceIcon, NKCResourceUtility.GetOrLoadMiscItemIcon(firstStage.m_StageReqItemID), false);
					NKCUtil.SetLabelText(this.m_lbResourceCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(firstStage.m_StageReqItemID).ToString());
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objResource, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objResource, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckEpisodeHasEventDrop(epTemplet));
		}

		// Token: 0x06007005 RID: 28677 RVA: 0x0025177C File Offset: 0x0024F97C
		public void SetData(NKMEpisodeGroupTemplet groupTemplet, EPISODE_CATEGORY category, NKCUIOperationSubChallengeSlot.OnClickSlot onClickSlot)
		{
			this.m_dOnClickSlot = onClickSlot;
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
			if (groupTemplet == null)
			{
				return;
			}
			this.m_Key = groupTemplet.EpisodeGroupID;
			if (groupTemplet.lstEpisodeTemplet.Count > 0)
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = groupTemplet.lstEpisodeTemplet[0];
				if (nkmepisodeTempletV != null)
				{
					NKCUtil.SetImageSprite(this.m_imgThumbnail, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", nkmepisodeTempletV.m_EPThumbnail, false), false);
					NKCUtil.SetLabelText(this.m_lbTitle, nkmepisodeTempletV.GetEpisodeTitle());
					switch (category)
					{
					case EPISODE_CATEGORY.EC_TRIM:
						NKCUtil.SetGameobjectActive(this.m_objLock, !NKCContentManager.IsContentsUnlocked(ContentsType.DIMENSION_TRIM, 0, 0));
						this.m_strLockedMessage = NKCContentManager.GetLockedMessage(ContentsType.DIMENSION_TRIM, 0);
						NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKCUITrimUtility.HaveEventDrop());
						NKCUtil.SetGameobjectActive(this.m_objResource, false);
						goto IL_1F7;
					case EPISODE_CATEGORY.EC_SHADOW:
						NKCUtil.SetGameobjectActive(this.m_objLock, !NKCContentManager.IsContentsUnlocked(ContentsType.SHADOW_PALACE, 0, 0));
						this.m_strLockedMessage = NKCContentManager.GetLockedMessage(ContentsType.SHADOW_PALACE, 0);
						NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckEpisodeHasEventDrop(nkmepisodeTempletV));
						NKCUtil.SetGameobjectActive(this.m_objResource, true);
						NKCUtil.SetImageSprite(this.m_imgResourceIcon, NKCResourceUtility.GetOrLoadMiscItemIcon(19), false);
						NKCUtil.SetLabelText(this.m_lbResourceCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(19).ToString());
						goto IL_1F7;
					}
					NKCUtil.SetGameobjectActive(this.m_objLock, !NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), nkmepisodeTempletV));
					this.m_strLockedMessage = NKCUtilString.GetUnlockConditionRequireDesc(nkmepisodeTempletV.GetUnlockInfo(), false);
					NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckEpisodeHasEventDrop(nkmepisodeTempletV));
					NKCUtil.SetGameobjectActive(this.m_objResource, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckEpisodeHasEventDrop(nkmepisodeTempletV));
					NKCUtil.SetGameobjectActive(this.m_objResource, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objEventDrop, false);
				NKCUtil.SetGameobjectActive(this.m_objResource, false);
			}
			IL_1F7:
			NKCUtil.SetGameobjectActive(this.m_lbDesc, false);
			NKCUtil.SetGameobjectActive(this.m_lbRewardDesc, false);
		}

		// Token: 0x06007006 RID: 28678 RVA: 0x00251998 File Offset: 0x0024FB98
		private void OnClickBtn()
		{
			if (this.m_objLock.activeSelf)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(this.m_strLockedMessage, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCUIOperationSubChallengeSlot.OnClickSlot dOnClickSlot = this.m_dOnClickSlot;
			if (dOnClickSlot == null)
			{
				return;
			}
			dOnClickSlot(this.m_Key);
		}

		// Token: 0x04005B9B RID: 23451
		public NKCUIComStateButton m_btn;

		// Token: 0x04005B9C RID: 23452
		[Space]
		public Image m_imgThumbnail;

		// Token: 0x04005B9D RID: 23453
		public Text m_lbTitle;

		// Token: 0x04005B9E RID: 23454
		public Text m_lbDesc;

		// Token: 0x04005B9F RID: 23455
		public Text m_lbRewardDesc;

		// Token: 0x04005BA0 RID: 23456
		[Space]
		public GameObject m_objLock;

		// Token: 0x04005BA1 RID: 23457
		[Space]
		public GameObject m_objEventDrop;

		// Token: 0x04005BA2 RID: 23458
		[Header("입장재화")]
		public GameObject m_objResource;

		// Token: 0x04005BA3 RID: 23459
		public Image m_imgResourceIcon;

		// Token: 0x04005BA4 RID: 23460
		public Text m_lbResourceCount;

		// Token: 0x04005BA5 RID: 23461
		private NKCUIOperationSubChallengeSlot.OnClickSlot m_dOnClickSlot;

		// Token: 0x04005BA6 RID: 23462
		private int m_Key;

		// Token: 0x04005BA7 RID: 23463
		private string m_strLockedMessage = "";

		// Token: 0x02001745 RID: 5957
		// (Invoke) Token: 0x0600B2CA RID: 45770
		public delegate void OnClickSlot(int key);
	}
}
