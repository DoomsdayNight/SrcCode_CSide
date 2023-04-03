using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000B9F RID: 2975
	public class NKCUIResultSubUIDungeon : NKCUIResultSubUIBase
	{
		// Token: 0x0600899F RID: 35231 RVA: 0x002EA88C File Offset: 0x002E8A8C
		private void SetMissionGray(NKCUIResultSubUIDungeon.MissionUI ui, bool bShowMedal)
		{
			NKCUtil.SetGameobjectActive(ui.m_objSuccess, false);
			NKCUtil.SetGameobjectActive(ui.m_objGray, bShowMedal);
			NKCUtil.SetGameobjectActive(ui.m_objCheckSuccess, false);
			NKCUtil.SetGameobjectActive(ui.m_objCheckFail, !bShowMedal);
			ui.m_lbMissionDesc.color = this.m_colMissionTextGray;
		}

		// Token: 0x060089A0 RID: 35232 RVA: 0x002EA8E0 File Offset: 0x002E8AE0
		private void SetMissionSuccess(NKCUIResultSubUIDungeon.MissionUI ui, bool bShowMedal)
		{
			NKCUtil.SetGameobjectActive(ui.m_objSuccess, bShowMedal);
			NKCUtil.SetGameobjectActive(ui.m_objGray, false);
			NKCUtil.SetGameobjectActive(ui.m_objCheckSuccess, !bShowMedal);
			NKCUtil.SetGameobjectActive(ui.m_objCheckFail, false);
			ui.m_lbMissionDesc.color = this.m_colMissionTextSuccess;
		}

		// Token: 0x060089A1 RID: 35233 RVA: 0x002EA931 File Offset: 0x002E8B31
		public void SetMissionData(NKCUIResultSubUIDungeon.MissionUI ui, NKCUIResultSubUIDungeon.MissionData data)
		{
			ui.m_lbMissionDesc.text = NKCUtilString.GetDGMissionText(data.eMissionType, data.iMissionValue);
		}

		// Token: 0x060089A2 RID: 35234 RVA: 0x002EA950 File Offset: 0x002E8B50
		public void SetData(List<NKCUIResultSubUIDungeon.MissionData> lstMissionData, bool bShowMedal = true, bool bIgnoreAutoClose = false, bool bShowClearPoint = false, float fClearPoint = 0f)
		{
			if (lstMissionData == null || lstMissionData.Count == 0)
			{
				Debug.Log("MissionData null!");
				base.ProcessRequired = false;
				return;
			}
			this.m_lstMissionData = lstMissionData;
			for (int i = 0; i < this.m_lstMissionUI.Count; i++)
			{
				NKCUIResultSubUIDungeon.MissionUI missionUI = this.m_lstMissionUI[i];
				if (i < lstMissionData.Count)
				{
					NKCUtil.SetGameobjectActive(missionUI.m_objRoot, lstMissionData[i].eMissionType > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE);
					this.SetMissionData(missionUI, lstMissionData[i]);
					if (this.m_lstMissionData[i].bSuccess)
					{
						this.SetMissionSuccess(missionUI, bShowMedal);
					}
					else
					{
						this.SetMissionGray(missionUI, bShowMedal);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(missionUI.m_objRoot, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objClearPoint, bShowClearPoint);
			if (this.m_objClearPoint.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbClearPoint, string.Format("{0}%", (fClearPoint * 100f).ToString("N0")));
				this.m_imgClearPoint.fillAmount = fClearPoint;
			}
			base.ProcessRequired = true;
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
		}

		// Token: 0x060089A3 RID: 35235 RVA: 0x002EAA6A File Offset: 0x002E8C6A
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089A4 RID: 35236 RVA: 0x002EAA72 File Offset: 0x002E8C72
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			yield return new WaitForSeconds(this.DelayAfterMission);
			this.m_bFinished = true;
			yield break;
		}

		// Token: 0x060089A5 RID: 35237 RVA: 0x002EAA81 File Offset: 0x002E8C81
		public override void FinishProcess()
		{
			this.m_bFinished = true;
			if (base.ProcessRequired)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
			}
		}

		// Token: 0x04007607 RID: 30215
		[Header("Mission")]
		public List<NKCUIResultSubUIDungeon.MissionUI> m_lstMissionUI;

		// Token: 0x04007608 RID: 30216
		public Color m_colMissionTextGray;

		// Token: 0x04007609 RID: 30217
		public Color m_colMissionTextSuccess;

		// Token: 0x0400760A RID: 30218
		[Header("길드 협력전 결과")]
		public GameObject m_objClearPoint;

		// Token: 0x0400760B RID: 30219
		public Image m_imgClearPoint;

		// Token: 0x0400760C RID: 30220
		public Text m_lbClearPoint;

		// Token: 0x0400760D RID: 30221
		private List<NKCUIResultSubUIDungeon.MissionData> m_lstMissionData;

		// Token: 0x0400760E RID: 30222
		private bool m_bFinished;

		// Token: 0x0400760F RID: 30223
		public float DelayAfterMission = 0.2f;

		// Token: 0x02001961 RID: 6497
		public class MissionData
		{
			// Token: 0x0400ABA0 RID: 43936
			public DUNGEON_GAME_MISSION_TYPE eMissionType;

			// Token: 0x0400ABA1 RID: 43937
			public int iMissionValue;

			// Token: 0x0400ABA2 RID: 43938
			public bool bSuccess;
		}

		// Token: 0x02001962 RID: 6498
		[Serializable]
		public class MissionUI
		{
			// Token: 0x0400ABA3 RID: 43939
			public GameObject m_objRoot;

			// Token: 0x0400ABA4 RID: 43940
			public GameObject m_objSuccess;

			// Token: 0x0400ABA5 RID: 43941
			public GameObject m_objGray;

			// Token: 0x0400ABA6 RID: 43942
			public GameObject m_objCheckSuccess;

			// Token: 0x0400ABA7 RID: 43943
			public GameObject m_objCheckFail;

			// Token: 0x0400ABA8 RID: 43944
			public Text m_lbMissionDesc;
		}
	}
}
