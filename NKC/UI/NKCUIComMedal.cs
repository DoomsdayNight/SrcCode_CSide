using System;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200093C RID: 2364
	public class NKCUIComMedal : MonoBehaviour
	{
		// Token: 0x06005E82 RID: 24194 RVA: 0x001D5064 File Offset: 0x001D3264
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
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (dungeonClearData != null)
			{
				flag = true;
				flag2 |= dungeonClearData.missionResult1;
				flag3 |= dungeonClearData.missionResult2;
			}
			if (this.m_MissionIcon1 != null)
			{
				if (flag)
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon1, this.m_sprMissionOn, false);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon1, this.m_sprMissionOff, false);
				}
			}
			if (this.m_MissionIcon2 != null)
			{
				if (flag2)
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon2, this.m_sprMissionOn, false);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon2, this.m_sprMissionOff, false);
				}
			}
			if (this.m_MissionIcon3 != null)
			{
				if (flag3)
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon3, this.m_sprMissionOn, false);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon3, this.m_sprMissionOff, false);
				}
			}
			if (this.m_MissionText1 != null)
			{
				if (flag)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText1, this.successTextColor);
				}
				else
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText1, this.failTextColor);
				}
			}
			if (this.m_MissionText2 != null)
			{
				if (flag2)
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
				if (flag3)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText3, this.successTextColor);
					return;
				}
				NKCUtil.SetLabelTextColor(this.m_MissionText3, this.failTextColor);
			}
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x001D5280 File Offset: 0x001D3480
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
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (phaseClearData != null)
			{
				flag = true;
				flag2 |= phaseClearData.missionResult1;
				flag3 |= phaseClearData.missionResult2;
			}
			if (this.m_MissionIcon1 != null)
			{
				if (flag)
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon1, this.m_sprMissionOn, false);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon1, this.m_sprMissionOff, false);
				}
			}
			if (this.m_MissionIcon2 != null)
			{
				if (flag2)
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon2, this.m_sprMissionOn, false);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon2, this.m_sprMissionOff, false);
				}
			}
			if (this.m_MissionIcon3 != null)
			{
				if (flag3)
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon3, this.m_sprMissionOn, false);
				}
				else
				{
					NKCUtil.SetImageSprite(this.m_MissionIcon3, this.m_sprMissionOff, false);
				}
			}
			if (this.m_MissionText1 != null)
			{
				if (flag)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText1, this.successTextColor);
				}
				else
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText1, this.failTextColor);
				}
			}
			if (this.m_MissionText2 != null)
			{
				if (flag2)
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
				if (flag3)
				{
					NKCUtil.SetLabelTextColor(this.m_MissionText3, this.successTextColor);
					return;
				}
				NKCUtil.SetLabelTextColor(this.m_MissionText3, this.failTextColor);
			}
		}

		// Token: 0x04004A99 RID: 19097
		[Header("메달 이미지")]
		public Sprite m_sprMissionOn;

		// Token: 0x04004A9A RID: 19098
		public Sprite m_sprMissionOff;

		// Token: 0x04004A9B RID: 19099
		[Header("미션 있을 때 켜지는 최상위 오브젝트")]
		public GameObject m_objMissionRoot;

		// Token: 0x04004A9C RID: 19100
		[Header("미션이 있을 경우 켜지는 오브젝트들")]
		public GameObject m_objMission1;

		// Token: 0x04004A9D RID: 19101
		public GameObject m_objMission2;

		// Token: 0x04004A9E RID: 19102
		public GameObject m_objMission3;

		// Token: 0x04004A9F RID: 19103
		[Header("미션 메달 아이콘")]
		public Image m_MissionIcon1;

		// Token: 0x04004AA0 RID: 19104
		public Image m_MissionIcon2;

		// Token: 0x04004AA1 RID: 19105
		public Image m_MissionIcon3;

		// Token: 0x04004AA2 RID: 19106
		[Header("미션 내용")]
		public TMP_Text m_MissionText1;

		// Token: 0x04004AA3 RID: 19107
		public TMP_Text m_MissionText2;

		// Token: 0x04004AA4 RID: 19108
		public TMP_Text m_MissionText3;

		// Token: 0x04004AA5 RID: 19109
		[Header("미션 완료/미완료 텍스트 컬러")]
		public Color successTextColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04004AA6 RID: 19110
		public Color failTextColor = new Color(0.4392157f, 0.48235294f, 0.5176471f, 1f);
	}
}
