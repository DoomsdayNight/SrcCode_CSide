using System;
using System.Text;
using ClientPacket.Warfare;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C17 RID: 3095
	public class NKCUILobbyMenuOperation : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F1B RID: 36635 RVA: 0x0030A910 File Offset: 0x00308B10
		public void Init()
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
			if (this.m_csbtnMoveToOngoingMission != null)
			{
				this.m_csbtnMoveToOngoingMission.PointerClick.RemoveAllListeners();
				this.m_csbtnMoveToOngoingMission.PointerClick.AddListener(new UnityAction(this.OnMoveToOngoingMission));
			}
		}

		// Token: 0x06008F1C RID: 36636 RVA: 0x0030A994 File Offset: 0x00308B94
		private void UpdateWFOnGoingUI(NKMUserData userData)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			bool flag = false;
			if (warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
				if (nkmwarfareTemplet != null)
				{
					flag = true;
					NKCUtil.SetLabelText(this.m_lbMissionOnProgress, this.MakeWFString(nkmwarfareTemplet));
				}
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnMoveToOngoingMission, flag);
			NKCUtil.SetGameobjectActive(this.m_objNoOngoingMission, !flag);
			this.SetNotify(flag);
		}

		// Token: 0x06008F1D RID: 36637 RVA: 0x0030AA00 File Offset: 0x00308C00
		protected override void ContentsUpdate(NKMUserData userData)
		{
			this.m_UserData = userData;
			NKCUtil.SetGameobjectActive(this.m_objEvent, false);
			if (this.m_VideoTexture != null && !this.m_VideoTexture.IsPlaying)
			{
				this.m_VideoTexture.Play(true, false, null, false);
			}
			this.UpdateWFOnGoingUI(userData);
		}

		// Token: 0x06008F1E RID: 36638 RVA: 0x0030AA54 File Offset: 0x00308C54
		private string MakeWFString(NKMWarfareTemplet cNKMWarfareTemplet)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string wfepisodeNumber = NKCUtilString.GetWFEpisodeNumber(cNKMWarfareTemplet);
			if (!string.IsNullOrEmpty(wfepisodeNumber))
			{
				stringBuilder.Append("<color=#888888>");
				stringBuilder.Append(wfepisodeNumber);
				stringBuilder.Append("</color> ");
			}
			if (cNKMWarfareTemplet.GetWarfareName().Length > 9)
			{
				stringBuilder.Append(cNKMWarfareTemplet.GetWarfareName().Substring(0, 9));
				stringBuilder.Append("..");
			}
			else
			{
				stringBuilder.Append(cNKMWarfareTemplet.GetWarfareName());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008F1F RID: 36639 RVA: 0x0030AADB File Offset: 0x00308CDB
		private void OnButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, false);
		}

		// Token: 0x06008F20 RID: 36640 RVA: 0x0030AAEC File Offset: 0x00308CEC
		private void OnMoveToOngoingMission()
		{
			if (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				int warfareTempletID = NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID;
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareTempletID);
				if (nkmwarfareTemplet == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_WARFARE_TEMPLET), null, "");
					Debug.LogError("can't find warfare templet, warfare templet ID : " + warfareTempletID.ToString());
					return;
				}
				if (nkmwarfareTemplet.MapTemplet == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_WARFARE_MAP_TEMPLET), null, "");
					Debug.LogError("can't find warfare map templet, warfare templet ID : " + warfareTempletID.ToString());
					return;
				}
				NKC_SCEN_WARFARE_GAME nkc_SCEN_WARFARE_GAME = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME();
				if (nkc_SCEN_WARFARE_GAME != null)
				{
					int warfareTempletID2 = NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID;
					nkc_SCEN_WARFARE_GAME.SetWarfareStrID(NKCWarfareManager.GetWarfareStrID(warfareTempletID2));
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WARFARE_GAME, true);
				}
			}
		}

		// Token: 0x06008F21 RID: 36641 RVA: 0x0030ABC8 File Offset: 0x00308DC8
		public override void CleanUp()
		{
			if (this.m_VideoTexture != null)
			{
				this.m_VideoTexture.CleanUp();
			}
		}

		// Token: 0x04007C16 RID: 31766
		private const int MAX_MISSIONNAME_COUNT = 9;

		// Token: 0x04007C17 RID: 31767
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007C18 RID: 31768
		public NKCUIComStateButton m_csbtnMoveToOngoingMission;

		// Token: 0x04007C19 RID: 31769
		public GameObject m_objNoOngoingMission;

		// Token: 0x04007C1A RID: 31770
		public Text m_lbMissionOnProgress;

		// Token: 0x04007C1B RID: 31771
		public GameObject m_objEvent;

		// Token: 0x04007C1C RID: 31772
		public NKCUIComVideoTexture m_VideoTexture;

		// Token: 0x04007C1D RID: 31773
		private NKMUserData m_UserData;
	}
}
