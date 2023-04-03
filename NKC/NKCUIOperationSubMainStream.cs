using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A08 RID: 2568
	public class NKCUIOperationSubMainStream : MonoBehaviour
	{
		// Token: 0x0600701A RID: 28698 RVA: 0x002523F8 File Offset: 0x002505F8
		public void InitUI()
		{
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			this.m_btnStart.PointerClick.RemoveAllListeners();
			this.m_btnStart.PointerClick.AddListener(new UnityAction(this.OnClickStart));
			this.m_btnStart.m_bGetCallbackWhileLocked = true;
		}

		// Token: 0x0600701B RID: 28699 RVA: 0x00252490 File Offset: 0x00250690
		private RectTransform GetObject(int idx)
		{
			NKCUIOperationSubMainStreamEPSlot nkcuioperationSubMainStreamEPSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuioperationSubMainStreamEPSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuioperationSubMainStreamEPSlot = UnityEngine.Object.Instantiate<NKCUIOperationSubMainStreamEPSlot>(this.m_pfbSlot);
			}
			nkcuioperationSubMainStreamEPSlot.InitUI(new NKCUIOperationSubMainStreamEPSlot.OnEPSlotSelect(this.OnSelectEPSlot), this.m_tglGroup);
			return nkcuioperationSubMainStreamEPSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600701C RID: 28700 RVA: 0x002524E8 File Offset: 0x002506E8
		private void ReturnObject(Transform tr)
		{
			NKCUIOperationSubMainStreamEPSlot component = tr.GetComponent<NKCUIOperationSubMainStreamEPSlot>();
			if (component == null)
			{
				return;
			}
			if (component.GetEpisodeID() > 0 && this.m_dicSlot.ContainsKey(component.GetEpisodeID()))
			{
				this.m_dicSlot.Remove(component.GetEpisodeID());
			}
			this.m_stkSlot.Push(component);
			tr.SetParent(this.m_trObjPool);
		}

		// Token: 0x0600701D RID: 28701 RVA: 0x0025254C File Offset: 0x0025074C
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIOperationSubMainStreamEPSlot component = tr.GetComponent<NKCUIOperationSubMainStreamEPSlot>();
			if (component == null)
			{
				return;
			}
			this.m_dicSlot.Remove(component.GetEpisodeID());
			tr.SetParent(this.m_loop.content);
			component.SetData(this.m_lstEpisodeTemplet[idx].m_EpisodeID, this.m_lstEpisodeTemplet[idx].GetEpisodeTitle(), idx);
			component.SetSelected(this.m_lstEpisodeTemplet[idx].m_EpisodeID == this.m_EpisodeTemplet.m_EpisodeID);
			component.RefreshRedDot();
			this.m_dicSlot.Add(this.m_lstEpisodeTemplet[idx].m_EpisodeID, component);
		}

		// Token: 0x0600701E RID: 28702 RVA: 0x00252600 File Offset: 0x00250800
		public void Open()
		{
			this.m_dicSlot.Clear();
			this.m_lstEpisodeTemplet = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(EPISODE_CATEGORY.EC_MAINSTREAM, true, EPISODE_DIFFICULTY.NORMAL);
			NKMEpisodeTempletV2 reservedEpisodeTemplet = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeTemplet();
			if (reservedEpisodeTemplet != null)
			{
				this.m_EpisodeTemplet = reservedEpisodeTemplet;
			}
			else if (NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetLastPlayedMainStream() > 0)
			{
				this.m_EpisodeTemplet = NKMEpisodeTempletV2.Find(NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetLastPlayedMainStream(), EPISODE_DIFFICULTY.NORMAL);
			}
			else
			{
				this.m_EpisodeTemplet = NKMEpisodeTempletV2.Find(this.GetLatestEpisodeTemplet().m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			}
			if (this.m_EpisodeTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_rtLeftFade, false);
			NKCUtil.SetGameobjectActive(this.m_rtRightFade, false);
			NKCUtil.SetGameobjectActive(this.m_imgFullFade, false);
			this.ResetFade();
			this.m_loop.TotalCount = this.m_lstEpisodeTemplet.Count;
			this.m_loop.RefreshCells(false);
			Canvas.ForceUpdateCanvases();
			this.m_loop.ScrollToCell(this.m_lstEpisodeTemplet.FindIndex((NKMEpisodeTempletV2 x) => x.m_EpisodeID == this.m_EpisodeTemplet.m_EpisodeID), 0.1f, LoopScrollRect.ScrollTarget.Center, null);
			this.SetData();
			this.TutorialCheck();
		}

		// Token: 0x0600701F RID: 28703 RVA: 0x00252718 File Offset: 0x00250918
		private void ResetFade()
		{
			this.m_rtLeftFade.anchoredPosition = this.m_vLeftStartPos;
			this.m_rtRightFade.anchoredPosition = this.m_vRightStartPos;
			this.m_imgFullFade.color = this.m_cFadeStartColor;
			NKCUtil.SetLabelTextColor(this.m_lbEpisodeTitle, this.m_cTextEndColor);
			NKCUtil.SetLabelTextColor(this.m_lbEpisodeNum, this.m_cTextEndColor);
			NKCUtil.SetLabelTextColor(this.m_lbEpisodeDesc, this.m_cTextEndColor);
			this.m_rtLeftFade.DOKill(false);
			this.m_rtRightFade.DOKill(false);
			this.m_imgFullFade.DOKill(false);
			this.m_lbEpisodeTitle.DOKill(false);
			this.m_lbEpisodeNum.DOKill(false);
			this.m_lbEpisodeDesc.DOKill(false);
		}

		// Token: 0x06007020 RID: 28704 RVA: 0x002527DC File Offset: 0x002509DC
		private void StartFade()
		{
			this.m_rtLeftFade.DOAnchorPos(this.m_vLeftEndPos, this.m_fFadeDuration, false).SetEase(this.m_SideFadeEase);
			this.m_rtRightFade.DOAnchorPos(this.m_vRightEndPos, this.m_fFadeDuration, false).SetEase(this.m_SideFadeEase);
			this.m_imgFullFade.DOColor(this.m_cFadeEndColor, this.m_fFadeDuration2).SetEase(this.m_FullFadeEase);
			NKCUtil.SetLabelTextColor(this.m_lbEpisodeTitle, this.m_cTextStartColor);
			NKCUtil.SetLabelTextColor(this.m_lbEpisodeNum, this.m_cTextStartColor);
			NKCUtil.SetLabelTextColor(this.m_lbEpisodeDesc, this.m_cTextStartColor);
			this.m_lbEpisodeTitle.DOColor(this.m_cTextEndColor, this.m_fTextFadeDuration).SetEase(this.m_TextFadeEase);
			this.m_lbEpisodeNum.DOColor(this.m_cTextEndColor, this.m_fTextFadeDuration).SetEase(this.m_TextFadeEase);
			this.m_lbEpisodeDesc.DOColor(this.m_cTextEndColor, this.m_fTextFadeDuration).SetEase(this.m_TextFadeEase);
		}

		// Token: 0x06007021 RID: 28705 RVA: 0x002528F0 File Offset: 0x00250AF0
		private void SetData()
		{
			NKCUtil.SetGameobjectActive(this.m_rtLeftFade, true);
			NKCUtil.SetGameobjectActive(this.m_rtRightFade, true);
			NKCUtil.SetGameobjectActive(this.m_imgFullFade, true);
			this.ResetFade();
			this.StartFade();
			bool flag = !NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), this.m_EpisodeTemplet.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (flag)
			{
				this.m_btnStart.Lock(false);
			}
			else
			{
				this.m_btnStart.UnLock(false);
			}
			NKCUtil.SetLabelText(this.m_lbEpisodeNum, this.m_EpisodeTemplet.GetEpisodeName());
			NKCUtil.SetLabelText(this.m_lbEpisodeTitle, this.m_EpisodeTemplet.GetEpisodeTitle());
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_lbEpisodeDesc, "");
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbEpisodeDesc, this.m_EpisodeTemplet.GetEpisodeDesc());
			}
			NKCUtil.SetImageSprite(this.m_imgBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Bg", this.m_EpisodeTemplet.m_EPThumbnail, false), false);
			NKMEpisodeTempletV2 reservedEpisodeTemplet = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeTemplet();
			if (reservedEpisodeTemplet != null)
			{
				NKCUIOperationNodeViewer.Instance.Open(reservedEpisodeTemplet);
			}
		}

		// Token: 0x06007022 RID: 28706 RVA: 0x002529FC File Offset: 0x00250BFC
		public void OnClickStart()
		{
			if (this.m_btnStart.m_bLock)
			{
				NKMStageTempletV2 firstStage = this.m_EpisodeTemplet.GetFirstStage(1);
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GetUnlockConditionRequireDesc(firstStage, false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetLastPlayedMainStream(this.m_EpisodeTemplet.m_EpisodeID);
			NKCUIFadeInOut.FadeOut(this.m_FadeTime, delegate
			{
				NKCUIOperationNodeViewer.Instance.Open(this.m_EpisodeTemplet);
			}, false, -1f);
		}

		// Token: 0x06007023 RID: 28707 RVA: 0x00252A7C File Offset: 0x00250C7C
		public void OnSelectEPSlot(int episodeID)
		{
			this.m_EpisodeTemplet = NKMEpisodeTempletV2.Find(episodeID, EPISODE_DIFFICULTY.NORMAL);
			if (this.m_EpisodeTemplet == null)
			{
				return;
			}
			foreach (KeyValuePair<int, NKCUIOperationSubMainStreamEPSlot> keyValuePair in this.m_dicSlot)
			{
				keyValuePair.Value.ChangeSelected(keyValuePair.Key == episodeID);
			}
			this.SetData();
		}

		// Token: 0x06007024 RID: 28708 RVA: 0x00252AFC File Offset: 0x00250CFC
		private NKMEpisodeTempletV2 GetLatestEpisodeTemplet()
		{
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(EPISODE_CATEGORY.EC_MAINSTREAM, true, EPISODE_DIFFICULTY.HARD);
			if (listNKMEpisodeTempletByCategory.Count > 0)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				for (int i = listNKMEpisodeTempletByCategory.Count - 1; i >= 0; i--)
				{
					NKMEpisodeTempletV2 nkmepisodeTempletV = listNKMEpisodeTempletByCategory[i];
					NKMStageTempletV2 firstStage = nkmepisodeTempletV.GetFirstStage(1);
					if (NKMEpisodeMgr.CheckEpisodeMission(myUserData, firstStage))
					{
						return nkmepisodeTempletV;
					}
				}
			}
			return null;
		}

		// Token: 0x06007025 RID: 28709 RVA: 0x00252B58 File Offset: 0x00250D58
		public void SetTutorialMainstreamGuide(NKCGameEventManager.NKCGameEventTemplet eventTemplet, UnityAction Complete)
		{
			NKCUIOperationSubMainStreamEPSlot nkcuioperationSubMainStreamEPSlot = this.m_lstEpSlot.Find((NKCUIOperationSubMainStreamEPSlot x) => x.GetEpisodeID() == eventTemplet.Value);
			if (!(nkcuioperationSubMainStreamEPSlot == null))
			{
				this.m_loop.SetIndexPosition(nkcuioperationSubMainStreamEPSlot.GetUIIndex());
				NKCGameEventManager.OpenTutorialGuideBySettedFace(nkcuioperationSubMainStreamEPSlot.GetComponent<RectTransform>(), NKCUIOverlayTutorialGuide.ClickGuideType.Touch, eventTemplet, null, false);
				NKCUIOverlayTutorialGuide.Instance.SetStealInput(delegate(BaseEventData x)
				{
					NKCUIOverlayTutorialGuide.CheckInstanceAndClose();
					UnityAction complete2 = Complete;
					if (complete2 != null)
					{
						complete2();
					}
					NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(eventTemplet.Value, EPISODE_DIFFICULTY.NORMAL);
					NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedStage(nkmepisodeTempletV.GetFirstStage(1));
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
				});
				return;
			}
			UnityAction complete = Complete;
			if (complete == null)
			{
				return;
			}
			complete();
		}

		// Token: 0x06007026 RID: 28710 RVA: 0x00252BE6 File Offset: 0x00250DE6
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_MainStream, true);
		}

		// Token: 0x04005BBD RID: 23485
		[Header("에피소드 선택")]
		public LoopVerticalScrollRect m_loop;

		// Token: 0x04005BBE RID: 23486
		public Transform m_trObjPool;

		// Token: 0x04005BBF RID: 23487
		public NKCUIOperationSubMainStreamEPSlot m_pfbSlot;

		// Token: 0x04005BC0 RID: 23488
		public NKCUIComToggleGroup m_tglGroup;

		// Token: 0x04005BC1 RID: 23489
		[Header("메인")]
		public TextMeshProUGUI m_lbEpisodeNum;

		// Token: 0x04005BC2 RID: 23490
		public TextMeshProUGUI m_lbEpisodeTitle;

		// Token: 0x04005BC3 RID: 23491
		public TextMeshProUGUI m_lbEpisodeDesc;

		// Token: 0x04005BC4 RID: 23492
		public Image m_imgBG;

		// Token: 0x04005BC5 RID: 23493
		public NKCUIComStateButton m_btnStart;

		// Token: 0x04005BC6 RID: 23494
		[Header("페이드 연출 관련")]
		public float m_fTextFadeDuration;

		// Token: 0x04005BC7 RID: 23495
		public Ease m_TextFadeEase;

		// Token: 0x04005BC8 RID: 23496
		public Color m_cTextStartColor;

		// Token: 0x04005BC9 RID: 23497
		public Color m_cTextEndColor;

		// Token: 0x04005BCA RID: 23498
		[Space]
		public float m_fFadeDuration;

		// Token: 0x04005BCB RID: 23499
		public Ease m_SideFadeEase;

		// Token: 0x04005BCC RID: 23500
		public RectTransform m_rtLeftFade;

		// Token: 0x04005BCD RID: 23501
		public Vector2 m_vLeftStartPos;

		// Token: 0x04005BCE RID: 23502
		public Vector2 m_vLeftEndPos;

		// Token: 0x04005BCF RID: 23503
		public RectTransform m_rtRightFade;

		// Token: 0x04005BD0 RID: 23504
		public Vector2 m_vRightStartPos;

		// Token: 0x04005BD1 RID: 23505
		public Vector2 m_vRightEndPos;

		// Token: 0x04005BD2 RID: 23506
		[Space]
		public float m_fFadeDuration2;

		// Token: 0x04005BD3 RID: 23507
		public Ease m_FullFadeEase;

		// Token: 0x04005BD4 RID: 23508
		public Image m_imgFullFade;

		// Token: 0x04005BD5 RID: 23509
		public Color m_cFadeStartColor;

		// Token: 0x04005BD6 RID: 23510
		public Color m_cFadeEndColor;

		// Token: 0x04005BD7 RID: 23511
		public float m_FadeTime = 0.3f;

		// Token: 0x04005BD8 RID: 23512
		private Dictionary<int, NKCUIOperationSubMainStreamEPSlot> m_dicSlot = new Dictionary<int, NKCUIOperationSubMainStreamEPSlot>();

		// Token: 0x04005BD9 RID: 23513
		private List<NKCUIOperationSubMainStreamEPSlot> m_lstEpSlot = new List<NKCUIOperationSubMainStreamEPSlot>();

		// Token: 0x04005BDA RID: 23514
		private Stack<NKCUIOperationSubMainStreamEPSlot> m_stkSlot = new Stack<NKCUIOperationSubMainStreamEPSlot>();

		// Token: 0x04005BDB RID: 23515
		private List<NKMEpisodeTempletV2> m_lstEpisodeTemplet = new List<NKMEpisodeTempletV2>();

		// Token: 0x04005BDC RID: 23516
		private NKMEpisodeTempletV2 m_EpisodeTemplet;
	}
}
