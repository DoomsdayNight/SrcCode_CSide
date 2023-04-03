using System;
using Cs.Logging;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A07 RID: 2567
	public class NKCUIOperationSubGrowthEPSlot : MonoBehaviour
	{
		// Token: 0x06007015 RID: 28693 RVA: 0x00251F55 File Offset: 0x00250155
		public void InitUI(NKCUIOperationSubGrowthEPSlot.OnClickEpSlot onClickEpSlot)
		{
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnBtn));
			this.m_dOnClickEpSlot = onClickEpSlot;
		}

		// Token: 0x06007016 RID: 28694 RVA: 0x00251F8C File Offset: 0x0025018C
		public bool SetData(NKMEpisodeTempletV2 epTemplet)
		{
			if (epTemplet == null)
			{
				return false;
			}
			this.m_EpisodeTemplet = epTemplet;
			this.m_EpisodeID = epTemplet.m_EpisodeID;
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			if (!epTemplet.IsOpen)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return false;
			}
			NKCUtil.SetImageSprite(this.m_img, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", epTemplet.m_EPThumbnail, false), false);
			NKCUtil.SetLabelText(this.m_lbTitle, epTemplet.GetEpisodeName());
			NKCUtil.SetLabelText(this.m_lbRewardDesc, epTemplet.GetEpisodeDesc());
			NKCUtil.SetLabelText(this.m_lbEpSubDesc, epTemplet.GetEpisodeDescSub());
			NKCUtil.SetGameobjectActive(this.m_objReddot, NKMEpisodeMgr.HasReddot(epTemplet.m_EpisodeID));
			NKMStageTempletV2 firstStage = this.m_EpisodeTemplet.GetFirstStage(1);
			if (firstStage != null)
			{
				bool flag = NKMEpisodeMgr.IsPossibleEpisode(cNKMUserData, this.m_EpisodeTemplet);
				bool flag2 = !NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, firstStage.m_UnlockInfo, false);
				bool flag3 = this.m_EpisodeTemplet.IsOpenedDayOfWeek();
				NKCUtil.SetGameobjectActive(this.m_objLock, !flag || flag2 || !flag3);
				NKCUtil.SetGameobjectActive(base.gameObject, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objEventBadge, this.m_EpisodeTemplet.HaveEventDrop);
			if (this.m_objLock.activeSelf)
			{
				UnlockInfo unlockInfo = this.m_EpisodeTemplet.GetUnlockInfo();
				if (!NKMContentUnlockManager.IsStarted(unlockInfo))
				{
					NKCUtil.SetGameobjectActive(this.m_lbRemainTime, true);
					string @string = NKCStringTable.GetString("SI_DP_TIME_TO_OPEN", new object[]
					{
						NKCUtilString.GetRemainTimeString(NKMContentUnlockManager.GetConditionStartTime(unlockInfo), 2)
					});
					NKCUtil.SetLabelText(this.m_lbRemainTime, @string);
				}
				else if (this.m_EpisodeTemplet.HasEventTimeLimit)
				{
					NKCUtil.SetGameobjectActive(this.m_lbRemainTime, true);
					NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GetRemainTimeStringEx(this.m_EpisodeTemplet.EpisodeDateEndUtc));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lbRemainTime, false);
				}
			}
			else if (this.m_EpisodeTemplet.HasEventTimeLimit)
			{
				NKCUtil.SetGameobjectActive(this.m_lbRemainTime, true);
				NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GetRemainTimeStringEx(this.m_EpisodeTemplet.EpisodeDateEndUtc));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbRemainTime, false);
			}
			this.m_deltaTime = 0f;
			return true;
		}

		// Token: 0x06007017 RID: 28695 RVA: 0x002521B4 File Offset: 0x002503B4
		private void Update()
		{
			this.m_deltaTime += Time.deltaTime;
			if (this.m_deltaTime > 1f)
			{
				this.m_deltaTime -= 1f;
				if (this.m_EpisodeTemplet == null)
				{
					return;
				}
				if (this.m_objLock.activeSelf)
				{
					if (NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), this.m_EpisodeTemplet) && this.m_EpisodeTemplet.IsOpenedDayOfWeek())
					{
						NKCUtil.SetGameobjectActive(this.m_objLock, false);
						return;
					}
					UnlockInfo unlockInfo = this.m_EpisodeTemplet.GetUnlockInfo();
					if (!NKMContentUnlockManager.IsStarted(unlockInfo))
					{
						string @string = NKCStringTable.GetString("SI_DP_TIME_TO_OPEN", new object[]
						{
							NKCUtilString.GetRemainTimeString(NKMContentUnlockManager.GetConditionStartTime(unlockInfo), 2)
						});
						NKCUtil.SetLabelText(this.m_lbRemainTime, @string);
						return;
					}
				}
				else
				{
					if (!this.m_EpisodeTemplet.IsOpenedDayOfWeek())
					{
						NKCUtil.SetGameobjectActive(this.m_objLock, true);
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						}, "");
						return;
					}
					if (!this.m_EpisodeTemplet.HasEventTimeLimit)
					{
						return;
					}
					if (!this.m_EpisodeTemplet.IsOpen)
					{
						Log.Warn(string.Format("{0}", this.m_EpisodeTemplet.m_EpisodeID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIOperationSubGrowthEPSlot.cs", 173);
						NKCUtil.SetGameobjectActive(this.m_objLock, true);
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						}, "");
						return;
					}
					NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GetRemainTimeStringEx(this.m_EpisodeTemplet.EpisodeDateEndUtc));
					if (!this.m_EpisodeTemplet.IsOpen)
					{
						NKCUtil.SetGameobjectActive(this.m_objLock, true);
					}
				}
			}
		}

		// Token: 0x06007018 RID: 28696 RVA: 0x00252380 File Offset: 0x00250580
		private void OnBtn()
		{
			if (this.m_objLock.gameObject.activeSelf)
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GetUnlockConditionRequireDesc(nkmepisodeTempletV.GetFirstStage(1).m_UnlockInfo, false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCUIOperationSubGrowthEPSlot.OnClickEpSlot dOnClickEpSlot = this.m_dOnClickEpSlot;
			if (dOnClickEpSlot == null)
			{
				return;
			}
			dOnClickEpSlot(this.m_EpisodeID);
		}

		// Token: 0x04005BAF RID: 23471
		public GameObject m_objLock;

		// Token: 0x04005BB0 RID: 23472
		public Image m_img;

		// Token: 0x04005BB1 RID: 23473
		public Text m_lbTitle;

		// Token: 0x04005BB2 RID: 23474
		public Text m_lbSubTitle;

		// Token: 0x04005BB3 RID: 23475
		public Text m_lbRewardDesc;

		// Token: 0x04005BB4 RID: 23476
		public Text m_lbEpSubDesc;

		// Token: 0x04005BB5 RID: 23477
		public Text m_lbRemainTime;

		// Token: 0x04005BB6 RID: 23478
		public GameObject m_objEventBadge;

		// Token: 0x04005BB7 RID: 23479
		public GameObject m_objReddot;

		// Token: 0x04005BB8 RID: 23480
		public NKCUIComStateButton m_btn;

		// Token: 0x04005BB9 RID: 23481
		private NKCUIOperationSubGrowthEPSlot.OnClickEpSlot m_dOnClickEpSlot;

		// Token: 0x04005BBA RID: 23482
		private NKMEpisodeTempletV2 m_EpisodeTemplet;

		// Token: 0x04005BBB RID: 23483
		private int m_EpisodeID;

		// Token: 0x04005BBC RID: 23484
		private float m_deltaTime;

		// Token: 0x02001746 RID: 5958
		// (Invoke) Token: 0x0600B2CE RID: 45774
		public delegate void OnClickEpSlot(int episodeID);
	}
}
