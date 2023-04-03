using System;
using System.Collections;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace NKC.UI
{
	// Token: 0x020009C3 RID: 2499
	public class NKCUIOperationIntro : NKCUIBase
	{
		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06006A62 RID: 27234 RVA: 0x00228F00 File Offset: 0x00227100
		public static NKCUIOperationIntro Instance
		{
			get
			{
				if (NKCUIOperationIntro.m_Instance == null)
				{
					NKCUIOperationIntro.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOperationIntro>("ab_ui_nkm_ui_operation", "NKM_UI_OPERATION_INTRO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperationIntro.CleanupInstance)).GetInstance<NKCUIOperationIntro>();
					NKCUIOperationIntro.m_Instance.InitUI();
				}
				return NKCUIOperationIntro.m_Instance;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06006A63 RID: 27235 RVA: 0x00228F4F File Offset: 0x0022714F
		public static bool HasInstance
		{
			get
			{
				return NKCUIOperationIntro.m_Instance != null;
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06006A64 RID: 27236 RVA: 0x00228F5C File Offset: 0x0022715C
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOperationIntro.m_Instance != null && NKCUIOperationIntro.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x00228F77 File Offset: 0x00227177
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOperationIntro.m_Instance != null && NKCUIOperationIntro.m_Instance.IsOpen)
			{
				NKCUIOperationIntro.m_Instance.Close();
			}
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x00228F9C File Offset: 0x0022719C
		private static void CleanupInstance()
		{
			NKCUIOperationIntro.m_Instance = null;
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06006A67 RID: 27239 RVA: 0x00228FA4 File Offset: 0x002271A4
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_OPERATION_INTRO;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06006A68 RID: 27240 RVA: 0x00228FAB File Offset: 0x002271AB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06006A69 RID: 27241 RVA: 0x00228FAE File Offset: 0x002271AE
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x00228FB1 File Offset: 0x002271B1
		private void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x00228FBF File Offset: 0x002271BF
		private IEnumerator OperationIntroUIOpenProcess()
		{
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
				subUICameraVideoPlayer.m_fMoviePlaySpeed = 1f;
				this.m_bWaitingMovie = true;
				subUICameraVideoPlayer.Play("MainStream_Transition.mp4", false, true, new NKCUIComVideoPlayer.VideoPlayMessageCallback(this.VideoPlayMessageCallback), false);
				while (this.m_bWaitingMovie)
				{
					yield return null;
					if (Input.anyKeyDown && PlayerPrefs.GetInt("OPERATION_INTRO_SKIP", 0) == 1)
					{
						break;
					}
				}
				if (PlayerPrefs.GetInt("OPERATION_INTRO_SKIP", 0) == 0)
				{
					PlayerPrefs.SetInt("OPERATION_INTRO_SKIP", 1);
				}
			}
			if (this.m_NKCUIOICallBack != null)
			{
				this.m_NKCUIOICallBack();
			}
			this.m_NKCUIOICallBack = null;
			this.m_bWaitingMovie = false;
			this.m_coIntro = null;
			yield break;
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x00228FD0 File Offset: 0x002271D0
		public void Open(NKMStageTempletV2 stageTemplet, NKCUIOperationIntro.NKCUIOICallBack _NKCUIOICallBack)
		{
			if (stageTemplet == null)
			{
				return;
			}
			this.m_NKCUIOICallBack = _NKCUIOICallBack;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_bWaitingMovie = false;
			NKMEpisodeTempletV2 episodeTemplet = stageTemplet.EpisodeTemplet;
			if (episodeTemplet != null)
			{
				string text;
				if (stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE)
				{
					text = episodeTemplet.GetEpisodeTitle() + " " + string.Format(NKCUtilString.GET_STRING_EP_TRAINING_NUMBER, stageTemplet.m_StageUINum);
				}
				else
				{
					bool flag = false;
					if (stageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_DUNGEON)
					{
						NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(stageTemplet.m_StageBattleStrID);
						if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
						{
							flag = true;
						}
					}
					if (flag)
					{
						text = episodeTemplet.GetEpisodeTitle() + " " + string.Format(NKCUtilString.GET_STRING_EP_CUTSCEN_NUMBER, stageTemplet.m_StageUINum);
					}
					else
					{
						text = string.Concat(new string[]
						{
							episodeTemplet.GetEpisodeTitle(),
							" ",
							stageTemplet.ActId.ToString(),
							"-",
							stageTemplet.m_StageUINum.ToString()
						});
					}
				}
				text = text.Replace("EP", "EPISODE");
				this.m_NKM_UI_OPERATION_INTRO_EPISODE_COUNT.text = text;
				switch (stageTemplet.m_STAGE_TYPE)
				{
				case STAGE_TYPE.ST_WARFARE:
				{
					NKMWarfareTemplet warfareTemplet = stageTemplet.WarfareTemplet;
					if (warfareTemplet != null)
					{
						this.m_NKM_UI_OPERATION_INTRO_EPISODE_TITLE.text = warfareTemplet.GetWarfareName();
						goto IL_1EC;
					}
					this.m_NKM_UI_OPERATION_INTRO_EPISODE_TITLE.text = "";
					goto IL_1EC;
				}
				case STAGE_TYPE.ST_PHASE:
				{
					NKMPhaseTemplet phaseTemplet = stageTemplet.PhaseTemplet;
					if (phaseTemplet != null)
					{
						this.m_NKM_UI_OPERATION_INTRO_EPISODE_TITLE.text = phaseTemplet.GetName();
						goto IL_1EC;
					}
					this.m_NKM_UI_OPERATION_INTRO_EPISODE_TITLE.text = "";
					goto IL_1EC;
				}
				}
				NKMDungeonTempletBase dungeonTempletBase2 = stageTemplet.DungeonTempletBase;
				if (dungeonTempletBase2 != null)
				{
					this.m_NKM_UI_OPERATION_INTRO_EPISODE_TITLE.text = dungeonTempletBase2.GetDungeonName();
				}
				else
				{
					this.m_NKM_UI_OPERATION_INTRO_EPISODE_TITLE.text = "";
				}
			}
			else
			{
				this.m_NKM_UI_OPERATION_INTRO_EPISODE_COUNT.text = "";
				this.m_NKM_UI_OPERATION_INTRO_EPISODE_TITLE.text = "";
			}
			IL_1EC:
			base.UIOpened(true);
			this.m_coIntro = base.StartCoroutine(this.OperationIntroUIOpenProcess());
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x002291E2 File Offset: 0x002273E2
		private void VideoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage message)
		{
			switch (message)
			{
			case NKCUIComVideoPlayer.eVideoMessage.PlayFailed:
			case NKCUIComVideoPlayer.eVideoMessage.PlayComplete:
				this.m_bWaitingMovie = false;
				break;
			case NKCUIComVideoPlayer.eVideoMessage.PlayBegin:
				break;
			default:
				return;
			}
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x00229200 File Offset: 0x00227400
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			if (this.m_coIntro != null)
			{
				base.StopCoroutine(this.m_coIntro);
			}
			this.m_coIntro = null;
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
			this.m_NKCUIOICallBack = null;
		}

		// Token: 0x06006A6F RID: 27247 RVA: 0x0022925D File Offset: 0x0022745D
		public override void OnBackButton()
		{
			if (this.m_NKCUIOICallBack != null)
			{
				this.m_NKCUIOICallBack();
			}
			this.m_NKCUIOICallBack = null;
		}

		// Token: 0x0400562E RID: 22062
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operation";

		// Token: 0x0400562F RID: 22063
		private const string UI_ASSET_NAME = "NKM_UI_OPERATION_INTRO";

		// Token: 0x04005630 RID: 22064
		private static NKCUIOperationIntro m_Instance;

		// Token: 0x04005631 RID: 22065
		private NKCUIOperationIntro.NKCUIOICallBack m_NKCUIOICallBack;

		// Token: 0x04005632 RID: 22066
		public Text m_NKM_UI_OPERATION_INTRO_EPISODE_COUNT;

		// Token: 0x04005633 RID: 22067
		public Text m_NKM_UI_OPERATION_INTRO_EPISODE_TITLE;

		// Token: 0x04005634 RID: 22068
		private Coroutine m_coIntro;

		// Token: 0x04005635 RID: 22069
		private bool m_bWaitingMovie;

		// Token: 0x020016C7 RID: 5831
		// (Invoke) Token: 0x0600B144 RID: 45380
		public delegate void NKCUIOICallBack();
	}
}
