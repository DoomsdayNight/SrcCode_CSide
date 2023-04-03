using System;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000929 RID: 2345
	public class NKCUIComDungeonMission : MonoBehaviour
	{
		// Token: 0x06005DEA RID: 24042 RVA: 0x001CFC6C File Offset: 0x001CDE6C
		public void SetData(NKMWarfareTemplet cNKMWarfareTemplet, bool bTextOnly = false)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (cNKMWarfareTemplet == null)
			{
				return;
			}
			WARFARE_GAME_MISSION_TYPE wfmissionType_ = cNKMWarfareTemplet.m_WFMissionType_1;
			WARFARE_GAME_MISSION_TYPE wfmissionType_2 = cNKMWarfareTemplet.m_WFMissionType_2;
			int wfmissionValue_ = cNKMWarfareTemplet.m_WFMissionValue_1;
			int wfmissionValue_2 = cNKMWarfareTemplet.m_WFMissionValue_2;
			NKCUtil.SetGameobjectActive(this.m_objMission1, true);
			NKCUtil.SetGameobjectActive(this.m_objMission2, wfmissionType_ > WARFARE_GAME_MISSION_TYPE.WFMT_NONE);
			NKCUtil.SetGameobjectActive(this.m_objMission3, wfmissionType_ > WARFARE_GAME_MISSION_TYPE.WFMT_NONE);
			NKCUtil.SetLabelText(this.m_MissionText1, NKCUtilString.GetWFMissionText(WARFARE_GAME_MISSION_TYPE.WFMT_CLEAR, 0));
			NKCUtil.SetLabelText(this.m_MissionText2, NKCUtilString.GetWFMissionText(wfmissionType_, wfmissionValue_));
			NKCUtil.SetLabelText(this.m_MissionText3, NKCUtilString.GetWFMissionText(wfmissionType_2, wfmissionValue_2));
			NKMWarfareClearData warfareClearData = myUserData.GetWarfareClearData(cNKMWarfareTemplet.m_WarfareID);
			if (warfareClearData != null)
			{
				if (this.m_MissionText1 != null)
				{
					this.m_MissionText1.color = this.successTextColor;
				}
				if (this.m_MissionText2 != null)
				{
					if (warfareClearData.m_mission_result_1)
					{
						this.m_MissionText2.color = this.successTextColor;
					}
					else
					{
						this.m_MissionText2.color = this.failTextColor;
					}
				}
				if (this.m_MissionText3 != null)
				{
					if (warfareClearData.m_mission_result_2)
					{
						this.m_MissionText3.color = this.successTextColor;
					}
					else
					{
						this.m_MissionText3.color = this.failTextColor;
					}
				}
				NKCUtil.SetGameobjectActive(this.m_MissionIcon1_BG, false);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon2_BG, !warfareClearData.m_mission_result_1);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon3_BG, !warfareClearData.m_mission_result_2);
				if (bTextOnly)
				{
					this.m_MissionIcon1.SetActive(false);
					this.m_MissionIcon2.SetActive(false);
					this.m_MissionIcon3.SetActive(false);
					return;
				}
				if (this.m_MissionIcon1 != null && !this.m_MissionIcon1.activeSelf)
				{
					this.m_MissionIcon1.SetActive(true);
				}
				if (this.m_MissionIcon2 != null && this.m_MissionIcon2.activeSelf == !warfareClearData.m_mission_result_1)
				{
					this.m_MissionIcon2.SetActive(warfareClearData.m_mission_result_1);
				}
				if (this.m_MissionIcon3 != null && this.m_MissionIcon3.activeSelf == !warfareClearData.m_mission_result_2)
				{
					this.m_MissionIcon3.SetActive(warfareClearData.m_mission_result_2);
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_MissionIcon1_BG, true);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon2_BG, true);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon3_BG, true);
				if (this.m_MissionIcon1 != null && this.m_MissionIcon1.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon1, false);
				}
				if (this.m_MissionIcon2 != null && this.m_MissionIcon2.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon2, false);
				}
				if (this.m_MissionIcon3 != null && this.m_MissionIcon3.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon3, false);
				}
				if (this.m_MissionText1 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText1, this.failTextColor);
				}
				if (this.m_MissionText2 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText2, this.failTextColor);
				}
				if (this.m_MissionText3 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText3, this.failTextColor);
				}
			}
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x001CFFA8 File Offset: 0x001CE1A8
		public void SetData(NKMDungeonTempletBase cNKMDungeonTempletBase, bool bTextOnly = false)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (cNKMDungeonTempletBase == null)
			{
				return;
			}
			NKMDungeonClearData dungeonClearData = myUserData.GetDungeonClearData(cNKMDungeonTempletBase.m_DungeonID);
			DUNGEON_GAME_MISSION_TYPE dgmissionType_ = cNKMDungeonTempletBase.m_DGMissionType_1;
			DUNGEON_GAME_MISSION_TYPE dgmissionType_2 = cNKMDungeonTempletBase.m_DGMissionType_2;
			int dgmissionValue_ = cNKMDungeonTempletBase.m_DGMissionValue_1;
			int dgmissionValue_2 = cNKMDungeonTempletBase.m_DGMissionValue_2;
			NKCUtil.SetGameobjectActive(this.m_objMission1, true);
			NKCUtil.SetGameobjectActive(this.m_objMission2, dgmissionType_ > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE);
			NKCUtil.SetGameobjectActive(this.m_objMission3, dgmissionType_ > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE);
			NKCUtil.SetLabelText(this.m_MissionText1, NKCUtilString.GetDGMissionText(DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR, 0));
			NKCUtil.SetLabelText(this.m_MissionText2, NKCUtilString.GetDGMissionText(dgmissionType_, dgmissionValue_));
			NKCUtil.SetLabelText(this.m_MissionText3, NKCUtilString.GetDGMissionText(dgmissionType_2, dgmissionValue_2));
			if (dungeonClearData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_MissionIcon1_BG, false);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon2_BG, !dungeonClearData.missionResult1);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon3_BG, !dungeonClearData.missionResult2);
				if (!bTextOnly)
				{
					if (this.m_MissionIcon1 != null && !this.m_MissionIcon1.activeSelf)
					{
						this.m_MissionIcon1.SetActive(true);
					}
					if (this.m_MissionIcon2 != null && this.m_MissionIcon2.activeSelf == !dungeonClearData.missionResult1)
					{
						this.m_MissionIcon2.SetActive(dungeonClearData.missionResult1);
					}
					if (this.m_MissionIcon3 != null && this.m_MissionIcon3.activeSelf == !dungeonClearData.missionResult2)
					{
						this.m_MissionIcon3.SetActive(dungeonClearData.missionResult2);
					}
				}
				else
				{
					this.m_MissionIcon1.SetActive(false);
					this.m_MissionIcon2.SetActive(false);
					this.m_MissionIcon3.SetActive(false);
				}
				if (this.m_MissionText1 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText1, this.successTextColor);
				}
				if (this.m_MissionText2 != null)
				{
					if (dungeonClearData.missionResult1)
					{
						NKCUtil.SetLabelTextColor(this.m_MissionText2, this.successTextColor);
					}
					else
					{
						NKCUtil.SetLabelTextColor(this.m_MissionText2, this.failTextColor);
					}
				}
				if (this.m_MissionText3 != null)
				{
					if (dungeonClearData.missionResult2)
					{
						NKCUtil.SetLabelTextColor(this.m_MissionText3, this.successTextColor);
						return;
					}
					NKCUtil.SetLabelTextColor(this.m_MissionText3, this.failTextColor);
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_MissionIcon1_BG, true);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon2_BG, true);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon3_BG, true);
				if (this.m_MissionIcon1 != null && this.m_MissionIcon1.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon1, false);
				}
				if (this.m_MissionIcon2 != null && this.m_MissionIcon2.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon2, false);
				}
				if (this.m_MissionIcon3 != null && this.m_MissionIcon3.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon3, false);
				}
				if (this.m_MissionText1 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText1, this.failTextColor);
				}
				if (this.m_MissionText2 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText2, this.failTextColor);
				}
				if (this.m_MissionText3 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText3, this.failTextColor);
				}
			}
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x001D02D8 File Offset: 0x001CE4D8
		public void SetData(NKMPhaseTemplet phaseTemplet, bool bTextOnly = false)
		{
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			if (phaseTemplet == null)
			{
				return;
			}
			DUNGEON_GAME_MISSION_TYPE dgmissionType_ = phaseTemplet.m_DGMissionType_1;
			DUNGEON_GAME_MISSION_TYPE dgmissionType_2 = phaseTemplet.m_DGMissionType_2;
			int dgmissionValue_ = phaseTemplet.m_DGMissionValue_1;
			int dgmissionValue_2 = phaseTemplet.m_DGMissionValue_2;
			NKCUtil.SetGameobjectActive(this.m_objMission1, true);
			NKCUtil.SetGameobjectActive(this.m_objMission2, dgmissionType_ > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE);
			NKCUtil.SetGameobjectActive(this.m_objMission3, dgmissionType_ > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE);
			NKCUtil.SetLabelText(this.m_MissionText1, NKCUtilString.GetDGMissionText(DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR, 0));
			NKCUtil.SetLabelText(this.m_MissionText2, NKCUtilString.GetDGMissionText(dgmissionType_, dgmissionValue_));
			NKCUtil.SetLabelText(this.m_MissionText3, NKCUtilString.GetDGMissionText(dgmissionType_2, dgmissionValue_2));
			NKMPhaseClearData phaseClearData = NKCPhaseManager.GetPhaseClearData(phaseTemplet);
			if (phaseClearData != null)
			{
				if (this.m_MissionText1 != null)
				{
					this.m_MissionText1.color = this.successTextColor;
				}
				if (this.m_MissionText2 != null)
				{
					if (phaseClearData.missionResult1)
					{
						this.m_MissionText2.color = this.successTextColor;
					}
					else
					{
						this.m_MissionText2.color = this.failTextColor;
					}
				}
				if (this.m_MissionText3 != null)
				{
					if (phaseClearData.missionResult2)
					{
						this.m_MissionText3.color = this.successTextColor;
					}
					else
					{
						this.m_MissionText3.color = this.failTextColor;
					}
				}
				NKCUtil.SetGameobjectActive(this.m_MissionIcon1_BG, false);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon2_BG, !phaseClearData.missionResult1);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon3_BG, !phaseClearData.missionResult2);
				if (bTextOnly)
				{
					this.m_MissionIcon1.SetActive(false);
					this.m_MissionIcon2.SetActive(false);
					this.m_MissionIcon3.SetActive(false);
					return;
				}
				if (this.m_MissionIcon1 != null && !this.m_MissionIcon1.activeSelf)
				{
					this.m_MissionIcon1.SetActive(true);
				}
				if (this.m_MissionIcon2 != null && this.m_MissionIcon2.activeSelf == !phaseClearData.missionResult1)
				{
					this.m_MissionIcon2.SetActive(phaseClearData.missionResult1);
				}
				if (this.m_MissionIcon3 != null && this.m_MissionIcon3.activeSelf == !phaseClearData.missionResult2)
				{
					this.m_MissionIcon3.SetActive(phaseClearData.missionResult2);
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_MissionIcon1_BG, true);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon2_BG, true);
				NKCUtil.SetGameobjectActive(this.m_MissionIcon3_BG, true);
				if (this.m_MissionIcon1 != null && this.m_MissionIcon1.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon1, false);
				}
				if (this.m_MissionIcon2 != null && this.m_MissionIcon2.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon2, false);
				}
				if (this.m_MissionIcon3 != null && this.m_MissionIcon3.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_MissionIcon3, false);
				}
				if (this.m_MissionText1 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText1, this.failTextColor);
				}
				if (this.m_MissionText2 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText2, this.failTextColor);
				}
				if (this.m_MissionText3 != null)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText3, this.failTextColor);
				}
			}
		}

		// Token: 0x04004A22 RID: 18978
		public GameObject m_objMissionRoot;

		// Token: 0x04004A23 RID: 18979
		public GameObject m_objMission1;

		// Token: 0x04004A24 RID: 18980
		public GameObject m_objMission2;

		// Token: 0x04004A25 RID: 18981
		public GameObject m_objMission3;

		// Token: 0x04004A26 RID: 18982
		public GameObject m_MissionIcon1_BG;

		// Token: 0x04004A27 RID: 18983
		public GameObject m_MissionIcon2_BG;

		// Token: 0x04004A28 RID: 18984
		public GameObject m_MissionIcon3_BG;

		// Token: 0x04004A29 RID: 18985
		public GameObject m_MissionIcon1;

		// Token: 0x04004A2A RID: 18986
		public GameObject m_MissionIcon2;

		// Token: 0x04004A2B RID: 18987
		public GameObject m_MissionIcon3;

		// Token: 0x04004A2C RID: 18988
		public Text m_MissionText1;

		// Token: 0x04004A2D RID: 18989
		public Text m_MissionText2;

		// Token: 0x04004A2E RID: 18990
		public Text m_MissionText3;

		// Token: 0x04004A2F RID: 18991
		private Color successTextColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04004A30 RID: 18992
		private Color failTextColor = new Color(0.4392157f, 0.48235294f, 0.5176471f, 1f);
	}
}
