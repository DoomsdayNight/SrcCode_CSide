using System;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A0C RID: 2572
	public class NKCUIOperationSubStoryPopup : NKCUIBase
	{
		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06007043 RID: 28739 RVA: 0x0025324C File Offset: 0x0025144C
		public static NKCUIOperationSubStoryPopup Instance
		{
			get
			{
				if (NKCUIOperationSubStoryPopup.m_Instance == null)
				{
					NKCUIOperationSubStoryPopup.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOperationSubStoryPopup>("AB_UI_OPERATION", "AB_UI_OPERATION_UI_SUB_02", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperationSubStoryPopup.CleanupInstance)).GetInstance<NKCUIOperationSubStoryPopup>();
					NKCUIOperationSubStoryPopup.m_Instance.InitUI();
				}
				return NKCUIOperationSubStoryPopup.m_Instance;
			}
		}

		// Token: 0x06007044 RID: 28740 RVA: 0x0025329B File Offset: 0x0025149B
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOperationSubStoryPopup.m_Instance != null && NKCUIOperationSubStoryPopup.m_Instance.IsOpen)
			{
				NKCUIOperationSubStoryPopup.m_Instance.Close();
			}
		}

		// Token: 0x06007045 RID: 28741 RVA: 0x002532C0 File Offset: 0x002514C0
		private static void CleanupInstance()
		{
			NKCUIOperationSubStoryPopup.m_Instance = null;
		}

		// Token: 0x06007046 RID: 28742 RVA: 0x002532C8 File Offset: 0x002514C8
		public static bool isOpen()
		{
			return NKCUIOperationSubStoryPopup.m_Instance != null && NKCUIOperationSubStoryPopup.m_Instance.IsOpen;
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06007047 RID: 28743 RVA: 0x002532E3 File Offset: 0x002514E3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06007048 RID: 28744 RVA: 0x002532E6 File Offset: 0x002514E6
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x002532ED File Offset: 0x002514ED
		private void InitUI()
		{
			this.m_btnStart.PointerClick.RemoveAllListeners();
			this.m_btnStart.PointerClick.AddListener(new UnityAction(this.OnClickStart));
		}

		// Token: 0x0600704A RID: 28746 RVA: 0x0025331B File Offset: 0x0025151B
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600704B RID: 28747 RVA: 0x0025332C File Offset: 0x0025152C
		public void Open(NKMEpisodeTempletV2 epTemplet)
		{
			if (epTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_EpisodeTemplet = epTemplet;
			NKCUtil.SetLabelText(this.m_lbTitle, this.m_EpisodeTemplet.GetEpisodeTitle());
			NKCUtil.SetLabelText(this.m_lbSubTitle, this.m_EpisodeTemplet.GetEpisodeName());
			NKCUtil.SetLabelText(this.m_lbDesc, this.m_EpisodeTemplet.GetEpisodeDesc());
			NKCUtil.SetImageSprite(this.m_imgBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Bg", this.m_EpisodeTemplet.m_EPThumbnail, false), false);
			NKCBGMInfoTemplet nkcbgminfoTemplet = NKCBGMInfoTemplet.Find(this.m_EpisodeTemplet.m_BG_Music);
			if (nkcbgminfoTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objBGM, true);
				NKCUtil.SetLabelText(this.m_lbBGM, NKCStringTable.GetString(nkcbgminfoTemplet.m_BgmNameStringID, false));
				NKCSoundManager.PlayMusic(nkcbgminfoTemplet.m_BgmAssetID, true, 1f, false, 0f, 0f);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objBGM, false);
			}
			base.UIOpened(true);
			this.TutorialCheck();
		}

		// Token: 0x0600704C RID: 28748 RVA: 0x00253422 File Offset: 0x00251622
		private void OnClickStart()
		{
			base.Close();
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetLastPlayedSubStream(this.m_EpisodeTemplet.m_EpisodeID);
			NKCUIOperationNodeViewer.Instance.Open(this.m_EpisodeTemplet);
		}

		// Token: 0x0600704D RID: 28749 RVA: 0x00253454 File Offset: 0x00251654
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_SubStream_Popup, true);
		}

		// Token: 0x04005BF2 RID: 23538
		private const string ASSET_BUNDLE_NAME = "AB_UI_OPERATION";

		// Token: 0x04005BF3 RID: 23539
		private const string UI_ASSET_NAME = "AB_UI_OPERATION_UI_SUB_02";

		// Token: 0x04005BF4 RID: 23540
		private static NKCUIOperationSubStoryPopup m_Instance;

		// Token: 0x04005BF5 RID: 23541
		public Image m_imgBG;

		// Token: 0x04005BF6 RID: 23542
		public TMP_Text m_lbTitle;

		// Token: 0x04005BF7 RID: 23543
		public TMP_Text m_lbSubTitle;

		// Token: 0x04005BF8 RID: 23544
		public TMP_Text m_lbDesc;

		// Token: 0x04005BF9 RID: 23545
		public GameObject m_objBGM;

		// Token: 0x04005BFA RID: 23546
		public TMP_Text m_lbBGM;

		// Token: 0x04005BFB RID: 23547
		public NKCUIComStateButton m_btnStart;

		// Token: 0x04005BFC RID: 23548
		private NKMEpisodeTempletV2 m_EpisodeTemplet;
	}
}
