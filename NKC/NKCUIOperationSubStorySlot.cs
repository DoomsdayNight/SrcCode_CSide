using System;
using NKM;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A0D RID: 2573
	public class NKCUIOperationSubStorySlot : MonoBehaviour
	{
		// Token: 0x0600704F RID: 28751 RVA: 0x00253467 File Offset: 0x00251667
		public int GetEpisodeID()
		{
			return this.m_EpisodeID;
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x0025346F File Offset: 0x0025166F
		public void SetEpisodeID(int episodeID)
		{
			this.m_EpisodeID = episodeID;
		}

		// Token: 0x06007051 RID: 28753 RVA: 0x00253478 File Offset: 0x00251678
		public void InitUI(NKCUIOperationSubStorySlot.OnSelectSlot onSelectSlot = null)
		{
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
			this.m_dOnSelectSlot = onSelectSlot;
		}

		// Token: 0x06007052 RID: 28754 RVA: 0x002534B0 File Offset: 0x002516B0
		public void SetData(bool bSetImage = false)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV == null || !nkmepisodeTempletV.EnableByTag)
			{
				NKCUtil.SetGameobjectActive(this.m_objSubstream, false);
				NKCUtil.SetGameobjectActive(this.m_objSupplement, false);
				NKCUtil.SetGameobjectActive(this.m_objLock, false);
				NKCUtil.SetGameobjectActive(this.m_objBlind, true);
				NKCUtil.SetGameobjectActive(this.m_objReddot, false);
				NKCUtil.SetGameobjectActive(this.m_objEventDrop, false);
				NKCUtil.SetGameobjectActive(this.m_objFocusFX, false);
				NKCUtil.SetLabelText(this.m_lbLevel, "");
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTitlie, nkmepisodeTempletV.GetEpisodeTitle());
			NKCUtil.SetLabelText(this.m_lbSubTitle, nkmepisodeTempletV.GetEpisodeName());
			NKCUtil.SetGameobjectActive(this.m_objSubstream, !nkmepisodeTempletV.m_bIsSupplement);
			NKCUtil.SetGameobjectActive(this.m_objSupplement, nkmepisodeTempletV.m_bIsSupplement);
			int firstBattleStageLevel = nkmepisodeTempletV.GetFirstBattleStageLevel(1);
			if (firstBattleStageLevel > 0)
			{
				NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, firstBattleStageLevel));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbLevel, "");
			}
			if (bSetImage)
			{
				NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
				NKCUtil.SetImageSprite(this.m_imgIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Texture_Res", nkmepisodeTempletV.m_EPThumbnail_SUB_Node, false), false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
			}
			GameObject objLock = this.m_objLock;
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			UnlockInfo unlockInfo = nkmepisodeTempletV.GetUnlockInfo();
			NKCUtil.SetGameobjectActive(objLock, !NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false));
			NKCUtil.SetGameobjectActive(this.m_objBlind, !nkmepisodeTempletV.IsOpen);
			NKCUtil.SetGameobjectActive(this.m_objReddot, NKMEpisodeMgr.HasReddot(nkmepisodeTempletV.m_EpisodeID));
			NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckEpisodeHasEventDrop(nkmepisodeTempletV));
			NKCUtil.SetGameobjectActive(this.m_objFocusFX, false);
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x00253660 File Offset: 0x00251860
		public void Refresh()
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV == null || !nkmepisodeTempletV.EnableByTag)
			{
				return;
			}
			GameObject objLock = this.m_objLock;
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			UnlockInfo unlockInfo = nkmepisodeTempletV.GetUnlockInfo();
			NKCUtil.SetGameobjectActive(objLock, !NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false));
			NKCUtil.SetGameobjectActive(this.m_objBlind, !nkmepisodeTempletV.IsOpen);
			NKCUtil.SetGameobjectActive(this.m_objReddot, NKMEpisodeMgr.HasReddot(nkmepisodeTempletV.m_EpisodeID));
			NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKMEpisodeMgr.CheckEpisodeHasEventDrop(nkmepisodeTempletV));
			NKCUtil.SetGameobjectActive(this.m_objFocusFX, false);
		}

		// Token: 0x06007054 RID: 28756 RVA: 0x002536EF File Offset: 0x002518EF
		public void ShowFocusFx()
		{
			NKCUtil.SetGameobjectActive(this.m_objFocusFX, false);
			NKCUtil.SetGameobjectActive(this.m_objFocusFX, true);
		}

		// Token: 0x06007055 RID: 28757 RVA: 0x0025370C File Offset: 0x0025190C
		private void OnClickSlot()
		{
			if (this.m_objBlind.activeSelf)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_EPISODE_SUBSTREAM_DATA_EXPUNGED, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (this.m_objLock.activeSelf)
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
				NKCPopupMessage nkcpopupMessage = NKCUIManager.NKCPopupMessage;
				UnlockInfo unlockInfo = nkmepisodeTempletV.GetUnlockInfo();
				nkcpopupMessage.Open(new PopupMessage(NKCContentManager.MakeUnlockConditionString(unlockInfo, false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (this.m_dOnSelectSlot != null)
			{
				this.m_dOnSelectSlot(this.m_EpisodeID);
				return;
			}
			NKCUIOperationSubStoryPopup.Instance.Open(NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL));
		}

		// Token: 0x04005BFD RID: 23549
		[Header("------ 해당 에피소드 아이디 직접 입력해줘야함 ------")]
		public int m_EpisodeID;

		// Token: 0x04005BFE RID: 23550
		[Header("아래는 전부 링크 필요")]
		public NKCUIComStateButton m_btn;

		// Token: 0x04005BFF RID: 23551
		public TMP_Text m_lbTitlie;

		// Token: 0x04005C00 RID: 23552
		public TMP_Text m_lbSubTitle;

		// Token: 0x04005C01 RID: 23553
		public GameObject m_objSupplement;

		// Token: 0x04005C02 RID: 23554
		public GameObject m_objSubstream;

		// Token: 0x04005C03 RID: 23555
		public GameObject m_objReddot;

		// Token: 0x04005C04 RID: 23556
		[Space]
		public Image m_imgIcon;

		// Token: 0x04005C05 RID: 23557
		[Space]
		public GameObject m_objEventDrop;

		// Token: 0x04005C06 RID: 23558
		[Header("레벨")]
		public TMP_Text m_lbLevel;

		// Token: 0x04005C07 RID: 23559
		[Header("조건 걸려서 잠겼을 때")]
		public GameObject m_objLock;

		// Token: 0x04005C08 RID: 23560
		[Header("태그로 막혔을 때")]
		public GameObject m_objBlind;

		// Token: 0x04005C09 RID: 23561
		[Header("오브젝트 강조 이펙트")]
		public GameObject m_objFocusFX;

		// Token: 0x04005C0A RID: 23562
		private NKCUIOperationSubStorySlot.OnSelectSlot m_dOnSelectSlot;

		// Token: 0x0200174B RID: 5963
		// (Invoke) Token: 0x0600B2E0 RID: 45792
		public delegate void OnSelectSlot(int episodeID);
	}
}
