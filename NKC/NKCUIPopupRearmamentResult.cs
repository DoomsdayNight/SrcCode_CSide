using System;
using System.Collections;
using System.Collections.Generic;
using NKC.UI.Component;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

namespace NKC.UI
{
	// Token: 0x02000A9B RID: 2715
	public class NKCUIPopupRearmamentResult : NKCUIBase
	{
		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06007850 RID: 30800 RVA: 0x0027EE80 File Offset: 0x0027D080
		public static NKCUIPopupRearmamentResult Instance
		{
			get
			{
				if (NKCUIPopupRearmamentResult.m_Instance == null)
				{
					NKCUIPopupRearmamentResult.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupRearmamentResult>("ab_ui_rearm", "AB_UI_REARM_RESULT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupRearmamentResult.CleanupInstance)).GetInstance<NKCUIPopupRearmamentResult>();
					NKCUIPopupRearmamentResult instance = NKCUIPopupRearmamentResult.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCUIPopupRearmamentResult.m_Instance;
			}
		}

		// Token: 0x06007851 RID: 30801 RVA: 0x0027EED5 File Offset: 0x0027D0D5
		private static void CleanupInstance()
		{
			NKCUIPopupRearmamentResult.m_Instance = null;
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06007852 RID: 30802 RVA: 0x0027EEDD File Offset: 0x0027D0DD
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupRearmamentResult.m_Instance != null && NKCUIPopupRearmamentResult.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007853 RID: 30803 RVA: 0x0027EEF8 File Offset: 0x0027D0F8
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupRearmamentResult.m_Instance != null && NKCUIPopupRearmamentResult.m_Instance.IsOpen)
			{
				NKCUIPopupRearmamentResult.m_Instance.Close();
			}
		}

		// Token: 0x06007854 RID: 30804 RVA: 0x0027EF1D File Offset: 0x0027D11D
		private void OnDestroy()
		{
			NKCUIPopupRearmamentResult.m_Instance = null;
		}

		// Token: 0x06007855 RID: 30805 RVA: 0x0027EF25 File Offset: 0x0027D125
		public override void CloseInternal()
		{
			this.m_VideoPlayer = null;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06007856 RID: 30806 RVA: 0x0027EF3A File Offset: 0x0027D13A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06007857 RID: 30807 RVA: 0x0027EF3D File Offset: 0x0027D13D
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_REARM_CONFIRM_POPUP_TITLE;
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06007858 RID: 30808 RVA: 0x0027EF44 File Offset: 0x0027D144
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06007859 RID: 30809 RVA: 0x0027EF47 File Offset: 0x0027D147
		public override void OnBackButton()
		{
		}

		// Token: 0x0600785A RID: 30810 RVA: 0x0027EF49 File Offset: 0x0027D149
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnOK, new UnityAction(this.CloseAfterOpenUnitInfo));
		}

		// Token: 0x0600785B RID: 30811 RVA: 0x0027EF64 File Offset: 0x0027D164
		public void Open(NKMUnitData resultUnitData)
		{
			if (resultUnitData == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(resultUnitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			this.SetResultUI(false);
			int num = (NKCRearmamentUtil.GetRearmamentTemplet(resultUnitData.m_UnitID).RearmGrade + 1) * 5;
			int num2 = 0;
			List<NKMUnitSkillTemplet> unitAllSkillTemplets = NKMUnitSkillManager.GetUnitAllSkillTemplets(resultUnitData);
			for (int i = 0; i < unitAllSkillTemplets.Count; i++)
			{
				NKMUnitSkillTemplet nkmunitSkillTemplet = unitAllSkillTemplets[i];
				if (nkmunitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER)
				{
					NKCUtil.SetImageSprite(this.m_imgLeaderIcon, NKCUtil.GetSkillIconSprite(nkmunitSkillTemplet), false);
					NKCUtil.SetLabelText(this.m_lbLeaderSkillName, nkmunitSkillTemplet.GetSkillName());
					NKCUtil.SetLabelText(this.m_lbLeaderSkillLv, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, nkmunitSkillTemplet.m_Level));
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_lstSkillResult[num2].imgIcon, NKCUtil.GetSkillIconSprite(nkmunitSkillTemplet), false);
					NKCUtil.SetLabelText(this.m_lstSkillResult[num2].lbName, nkmunitSkillTemplet.GetSkillName());
					string msg = string.Format(NKCUtilString.GET_STRING_REARM_RESULT_POPUP_SKILL_LEVEL_BEFORE, nkmunitSkillTemplet.m_Level, num - 5);
					NKCUtil.SetLabelText(this.m_lstSkillResult[num2].lbBeforeLvInfo, msg);
					string msg2 = string.Format(NKCUtilString.GET_STRING_REARM_RESULT_POPUP_SKILL_LEVEL_AFTER, nkmunitSkillTemplet.m_Level, num);
					NKCUtil.SetLabelText(this.m_lstSkillResult[num2].lbAfterLvInfo, msg2);
					num2++;
				}
			}
			NKCUtil.SetLabelText(this.m_lbUnitName, unitTempletBase.GetUnitTitle());
			NKCUtil.SetLabelText(this.m_lbLeftUnitName, unitTempletBase.GetUnitName());
			NKCUtil.SetImageSprite(this.m_imgRank, NKCUtil.GetSpriteUnitGrade(unitTempletBase.m_NKM_UNIT_GRADE), false);
			this.m_CharView.CloseCharacterIllust();
			this.m_CharView.SetCharacterIllust(unitTempletBase, 0, false, true, 0);
			this.m_CharSummary.SetData(resultUnitData);
			this.m_ResultUnitData = resultUnitData;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.IntroMovie();
			base.UIOpened(true);
		}

		// Token: 0x0600785C RID: 30812 RVA: 0x0027F154 File Offset: 0x0027D354
		private void SetResultUI(bool bActive)
		{
			foreach (GameObject targetObj in this.m_lstHideObject)
			{
				NKCUtil.SetGameobjectActive(targetObj, bActive);
			}
		}

		// Token: 0x0600785D RID: 30813 RVA: 0x0027F1A8 File Offset: 0x0027D3A8
		private void IntroMovie()
		{
			this.m_VideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (this.m_VideoPlayer != null)
			{
				this.m_VideoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
				this.m_VideoPlayer.m_fMoviePlaySpeed = this.MOVIE_PLAY_SPEED;
				NKCSoundManager.StopMusic();
				this.m_VideoPlayer.Play(this.REARM_MOVIE, false, true, new NKCUIComVideoPlayer.VideoPlayMessageCallback(this.VideoPlayMessageCallback), true);
				if (!string.IsNullOrEmpty(this.REARM_INTRO_SOUND))
				{
					this.m_introSoundUID = NKCSoundManager.PlaySound(this.REARM_INTRO_SOUND, 1f, 0f, 0f, false, 0f, false, 0f);
				}
			}
		}

		// Token: 0x0600785E RID: 30814 RVA: 0x0027F24C File Offset: 0x0027D44C
		private void VideoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage message)
		{
			switch (message)
			{
			case NKCUIComVideoPlayer.eVideoMessage.PlayFailed:
			case NKCUIComVideoPlayer.eVideoMessage.PlayComplete:
			{
				NKCSoundManager.StopSound(this.m_introSoundUID);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OFFICE)
				{
					NKCSoundManager.PlayScenMusic(NKM_SCEN_ID.NSI_OFFICE, false);
				}
				else
				{
					NKCSoundManager.PlayScenMusic(NKM_SCEN_ID.NSI_HOME, false);
				}
				NKCUIComVideoCamera videoPlayer = this.m_VideoPlayer;
				if (videoPlayer != null)
				{
					videoPlayer.Stop();
				}
				this.m_introSoundUID = 0;
				this.SetResultUI(true);
				this.PlayAni();
				break;
			}
			case NKCUIComVideoPlayer.eVideoMessage.PlayBegin:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600785F RID: 30815 RVA: 0x0027F2BB File Offset: 0x0027D4BB
		private void PlayAni()
		{
			this.m_Ani.SetTrigger("INTRO");
			base.StartCoroutine(this.OpenUnitInfo(this.fAniDisplayedTime));
		}

		// Token: 0x06007860 RID: 30816 RVA: 0x0027F2E0 File Offset: 0x0027D4E0
		private IEnumerator OpenUnitInfo(float fDelay)
		{
			yield return new WaitForSeconds(fDelay);
			this.CloseAfterOpenUnitInfo();
			yield break;
		}

		// Token: 0x06007861 RID: 30817 RVA: 0x0027F2F8 File Offset: 0x0027D4F8
		public void CloseAfterOpenUnitInfo()
		{
			base.Close();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_UNIT_LIST && NKCUIRearmament.IsInstanceOpen)
			{
				NKCUIRearmament.Instance.Close();
			}
			else if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OFFICE && NKCUIRearmament.IsInstanceOpen)
			{
				NKCUIRearmament.Instance.Open(NKCUIRearmament.REARM_TYPE.RT_LIST);
			}
			NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(new List<long>
			{
				this.m_ResultUnitData.m_UnitUID
			}, 0);
			NKCUIUnitInfo.Instance.Open(this.m_ResultUnitData, null, openOption, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
		}

		// Token: 0x040064D3 RID: 25811
		private const string ASSET_BUNDLE_NAME = "ab_ui_rearm";

		// Token: 0x040064D4 RID: 25812
		private const string UI_ASSET_NAME = "AB_UI_REARM_RESULT";

		// Token: 0x040064D5 RID: 25813
		private static NKCUIPopupRearmamentResult m_Instance;

		// Token: 0x040064D6 RID: 25814
		public NKCUICharInfoSummary m_CharSummary;

		// Token: 0x040064D7 RID: 25815
		public NKCUICharacterView m_CharView;

		// Token: 0x040064D8 RID: 25816
		public Animator m_Ani;

		// Token: 0x040064D9 RID: 25817
		public Image m_imgRank;

		// Token: 0x040064DA RID: 25818
		public Text m_lbUnitName;

		// Token: 0x040064DB RID: 25819
		public Text m_lbLeftUnitName;

		// Token: 0x040064DC RID: 25820
		public List<GameObject> m_lstHideObject = new List<GameObject>();

		// Token: 0x040064DD RID: 25821
		[Header("리더스킬")]
		public Image m_imgLeaderIcon;

		// Token: 0x040064DE RID: 25822
		public Text m_lbLeaderSkillLv;

		// Token: 0x040064DF RID: 25823
		public Text m_lbLeaderSkillName;

		// Token: 0x040064E0 RID: 25824
		[Header("스킬")]
		public List<strResultSkillInfo> m_lstSkillResult;

		// Token: 0x040064E1 RID: 25825
		[Header("재무장 확인")]
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040064E2 RID: 25826
		private NKMUnitData m_ResultUnitData;

		// Token: 0x040064E3 RID: 25827
		private NKCUIComVideoCamera m_VideoPlayer;

		// Token: 0x040064E4 RID: 25828
		[Header("연출 영상 재생 속도")]
		public float MOVIE_PLAY_SPEED = 1.5f;

		// Token: 0x040064E5 RID: 25829
		[Header("재무장 연출 동영상")]
		public string REARM_MOVIE = "Rearmament_Intro.mp4";

		// Token: 0x040064E6 RID: 25830
		[Header("재무장 연출 음악")]
		public string REARM_INTRO_SOUND = "";

		// Token: 0x040064E7 RID: 25831
		private int m_introSoundUID;

		// Token: 0x040064E8 RID: 25832
		public float fAniDisplayedTime = 10f;
	}
}
