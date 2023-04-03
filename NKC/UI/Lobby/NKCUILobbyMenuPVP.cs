using System;
using ClientPacket.Pvp;
using Cs.Core.Util;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C18 RID: 3096
	public class NKCUILobbyMenuPVP : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F23 RID: 36643 RVA: 0x0030ABEC File Offset: 0x00308DEC
		public void Init(ContentsType contentsType)
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
				this.m_ContentsType = contentsType;
				NKCUtil.SetLabelText(this.m_lbSeasonDesc, string.Empty);
				NKCUtil.SetLabelText(this.m_lbPVPAsyncTicketCount, string.Empty);
				NKCUtil.SetLabelText(this.m_lbRemainPVPPoint, string.Empty);
				NKCUtil.SetGameobjectActive(this.m_objPVPPointIcon, false);
			}
		}

		// Token: 0x06008F24 RID: 36644 RVA: 0x0030AC76 File Offset: 0x00308E76
		private void Update()
		{
			if (this.m_bLocked)
			{
				return;
			}
			if (this.m_fPrevUpdateTime + 1f < Time.time)
			{
				this.ContentsUpdate(NKCScenManager.GetScenManager().GetMyUserData());
				this.m_fPrevUpdateTime = Time.time;
			}
		}

		// Token: 0x06008F25 RID: 36645 RVA: 0x0030ACB0 File Offset: 0x00308EB0
		protected override void ContentsUpdate(NKMUserData userData)
		{
			bool flag = NKCAlarmManager.CheckPVPNotify(userData);
			NKCUtil.SetGameobjectActive(this.m_objNotify, flag);
			NKMPvpRankSeasonTemplet pvpRankSeasonTemplet = NKCPVPManager.GetPvpRankSeasonTemplet(NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0)));
			if (pvpRankSeasonTemplet != null)
			{
				if (!NKCSynchronizedTime.IsFinished(pvpRankSeasonTemplet.EndDate))
				{
					NKCUtil.SetLabelText(this.m_lbSeasonDesc, NKCUtilString.GetRemainTimeString(pvpRankSeasonTemplet.EndDate, 1));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbSeasonDesc, NKCUtilString.GET_STRING_TIME_CLOSING);
				}
			}
			if (userData != null && userData.m_AsyncData != null)
			{
				long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(13);
				NKCUtil.SetLabelText(this.m_lbPVPAsyncTicketCount, string.Format(string.Format("{0}/{1}", countMiscItem, NKMPvpCommonConst.Instance.AsyncTicketMaxCount), Array.Empty<object>()));
			}
			if (userData != null)
			{
				long countMiscItem2 = userData.m_InventoryData.GetCountMiscItem(6);
				int charge_POINT_MAX_COUNT = NKMPvpCommonConst.Instance.CHARGE_POINT_MAX_COUNT;
				NKCUtil.SetLabelText(this.m_lbRemainPVPPoint, string.Format("{0}/{1}", countMiscItem2, charge_POINT_MAX_COUNT));
			}
			if (NKCPVPManager.IsPvpLeagueUnlocked() && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE) && NKMPvpCommonConst.Instance.LeaguePvp.IsValidTime(ServiceTime.Recent))
			{
				NKCUtil.SetGameobjectActive(this.m_objLeagueOpenTag, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objLeagueOpenTag, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objPVPPointIcon, true);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().ProcessPVPPointCharge(6);
			this.SetNotify(flag);
		}

		// Token: 0x06008F26 RID: 36646 RVA: 0x0030AE14 File Offset: 0x00309014
		private void OnButton()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.PVP, 0);
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_INTRO, false);
		}

		// Token: 0x06008F27 RID: 36647 RVA: 0x0030AE34 File Offset: 0x00309034
		public void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			base.UpdateData(NKCScenManager.GetScenManager().GetMyUserData());
		}

		// Token: 0x04007C1E RID: 31774
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007C1F RID: 31775
		public GameObject m_objNotify;

		// Token: 0x04007C20 RID: 31776
		public Text m_lbSeasonDesc;

		// Token: 0x04007C21 RID: 31777
		public Text m_lbPVPAsyncTicketCount;

		// Token: 0x04007C22 RID: 31778
		public Text m_lbRemainPVPPoint;

		// Token: 0x04007C23 RID: 31779
		public GameObject m_objPVPPointIcon;

		// Token: 0x04007C24 RID: 31780
		public GameObject m_objLeagueOpenTag;

		// Token: 0x04007C25 RID: 31781
		private float m_fPrevUpdateTime;
	}
}
