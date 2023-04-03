using System;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007D1 RID: 2001
	public class NKCUIOPSupplyMissionSlot : MonoBehaviour
	{
		// Token: 0x06004EFA RID: 20218 RVA: 0x0017D68C File Offset: 0x0017B88C
		public void Init()
		{
			NKCUIComStateButton btn = this.m_btn;
			if (btn != null)
			{
				btn.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btn2 = this.m_btn;
			if (btn2 == null)
			{
				return;
			}
			btn2.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x0017D6C8 File Offset: 0x0017B8C8
		public void SetData(EPISODE_CATEGORY category, int supplyEpisodeId, NKCUIOPSupplyMissionSlot.OnClickSM onClickSM)
		{
			this.m_dOnClickSM = onClickSM;
			this.m_supplyEpisodeId = supplyEpisodeId;
			NKCUtil.SetGameobjectActive(this.m_objChallengeOnly, category == EPISODE_CATEGORY.EC_CHALLENGE);
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(supplyEpisodeId, EPISODE_DIFFICULTY.NORMAL);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (nkmepisodeTempletV == null || myUserData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgThumbnail, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_EP_THUMBNAIL", string.Format("EP_THUMBNAIL_{0}", nkmepisodeTempletV.m_EPThumbnail), false), false);
			NKCUtil.SetLabelText(this.m_lbName, nkmepisodeTempletV.GetEpisodeName());
			NKCUtil.SetLabelText(this.m_lbDesc, nkmepisodeTempletV.GetEpisodeDesc());
			NKCUtil.SetLabelText(this.m_lbDescSub, nkmepisodeTempletV.GetEpisodeDescSub());
			NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(NKCContentManager.GetFirstStageID(nkmepisodeTempletV, 1, EPISODE_DIFFICULTY.NORMAL));
			if (nkmstageTempletV != null)
			{
				bool flag = NKMEpisodeMgr.IsPossibleEpisode(myUserData, nkmepisodeTempletV);
				bool flag2 = !NKMContentUnlockManager.IsContentUnlocked(myUserData, nkmstageTempletV.m_UnlockInfo, false);
				bool flag3 = nkmepisodeTempletV.IsOpenedDayOfWeek();
				NKCUtil.SetGameobjectActive(this.m_objLock, !flag || flag2 || !flag3);
				NKCUtil.SetGameobjectActive(this.m_lbLock, flag2);
				NKCUtil.SetLabelText(this.m_lbLock, NKCUtilString.GetUnlockConditionRequireDesc(nkmstageTempletV, true));
				NKCUtil.SetGameobjectActive(base.gameObject, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objEventBadge, nkmepisodeTempletV.HaveEventDrop);
			if (this.m_objLock.activeSelf)
			{
				UnlockInfo unlockInfo = nkmepisodeTempletV.GetUnlockInfo();
				if (!NKMContentUnlockManager.IsStarted(unlockInfo))
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, true);
					string @string = NKCStringTable.GetString("SI_DP_TIME_TO_OPEN", new object[]
					{
						NKCUtilString.GetRemainTimeString(NKMContentUnlockManager.GetConditionStartTime(unlockInfo), 2)
					});
					NKCUtil.SetLabelText(this.m_TIME_TEXT, @string);
				}
				else if (nkmepisodeTempletV.HasEventTimeLimit)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, true);
					NKCUtil.SetLabelText(this.m_TIME_TEXT, NKCUtilString.GetRemainTimeStringEx(nkmepisodeTempletV.EpisodeDateEndUtc));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, false);
				}
			}
			else if (nkmepisodeTempletV.HasEventTimeLimit)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, true);
				NKCUtil.SetLabelText(this.m_TIME_TEXT, NKCUtilString.GetRemainTimeStringEx(nkmepisodeTempletV.EpisodeDateEndUtc));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_TIME, false);
			}
			this.m_deltaTime = 0f;
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x0017D8E8 File Offset: 0x0017BAE8
		public void OnClickBtn()
		{
			if (this.m_objLock.activeSelf)
			{
				return;
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_supplyEpisodeId, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV != null && !nkmepisodeTempletV.IsOpenedDayOfWeek())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			NKCUIOPSupplyMissionSlot.OnClickSM dOnClickSM = this.m_dOnClickSM;
			if (dOnClickSM == null)
			{
				return;
			}
			dOnClickSM(this.m_supplyEpisodeId);
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x0017D968 File Offset: 0x0017BB68
		private void Update()
		{
			this.m_deltaTime += Time.deltaTime;
			if (this.m_deltaTime > 1f)
			{
				this.m_deltaTime -= 1f;
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_supplyEpisodeId, EPISODE_DIFFICULTY.NORMAL);
				if (nkmepisodeTempletV == null)
				{
					return;
				}
				if (this.m_objLock.activeSelf)
				{
					if (NKMEpisodeMgr.IsPossibleEpisode(NKCScenManager.CurrentUserData(), nkmepisodeTempletV) && nkmepisodeTempletV.IsOpenedDayOfWeek())
					{
						NKCUtil.SetGameobjectActive(this.m_objLock, false);
						return;
					}
					UnlockInfo unlockInfo = nkmepisodeTempletV.GetUnlockInfo();
					if (!NKMContentUnlockManager.IsStarted(unlockInfo))
					{
						string @string = NKCStringTable.GetString("SI_DP_TIME_TO_OPEN", new object[]
						{
							NKCUtilString.GetRemainTimeString(NKMContentUnlockManager.GetConditionStartTime(unlockInfo), 2)
						});
						NKCUtil.SetLabelText(this.m_TIME_TEXT, @string);
						return;
					}
				}
				else
				{
					if (!nkmepisodeTempletV.IsOpenedDayOfWeek())
					{
						NKCUtil.SetGameobjectActive(this.m_objLock, true);
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						}, "");
						return;
					}
					if (!nkmepisodeTempletV.HasEventTimeLimit)
					{
						return;
					}
					if (!nkmepisodeTempletV.IsOpen)
					{
						NKCUtil.SetGameobjectActive(this.m_objLock, true);
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						}, "");
						return;
					}
					NKCUtil.SetLabelText(this.m_TIME_TEXT, NKCUtilString.GetRemainTimeStringEx(nkmepisodeTempletV.EpisodeDateEndUtc));
					if (!nkmepisodeTempletV.IsOpen)
					{
						NKCUtil.SetGameobjectActive(this.m_objLock, true);
					}
				}
			}
		}

		// Token: 0x04003EDE RID: 16094
		public Image m_imgThumbnail;

		// Token: 0x04003EDF RID: 16095
		public Text m_lbName;

		// Token: 0x04003EE0 RID: 16096
		public Text m_lbDesc;

		// Token: 0x04003EE1 RID: 16097
		public Text m_lbDescSub;

		// Token: 0x04003EE2 RID: 16098
		public GameObject m_objLock;

		// Token: 0x04003EE3 RID: 16099
		public GameObject m_objEventBadge;

		// Token: 0x04003EE4 RID: 16100
		public Text m_lbLock;

		// Token: 0x04003EE5 RID: 16101
		public NKCUIComStateButton m_btn;

		// Token: 0x04003EE6 RID: 16102
		public GameObject m_objChallengeOnly;

		// Token: 0x04003EE7 RID: 16103
		public GameObject m_NKM_UI_OPERATION_EPISODE_TIME;

		// Token: 0x04003EE8 RID: 16104
		public Text m_TIME_TEXT;

		// Token: 0x04003EE9 RID: 16105
		private NKCUIOPSupplyMissionSlot.OnClickSM m_dOnClickSM;

		// Token: 0x04003EEA RID: 16106
		private int m_supplyEpisodeId;

		// Token: 0x04003EEB RID: 16107
		private float m_deltaTime;

		// Token: 0x0200148C RID: 5260
		// (Invoke) Token: 0x0600A92E RID: 43310
		public delegate void OnClickSM(int episodeId);
	}
}
