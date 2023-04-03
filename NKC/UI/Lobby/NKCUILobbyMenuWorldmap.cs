using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C1B RID: 3099
	public class NKCUILobbyMenuWorldmap : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F39 RID: 36665 RVA: 0x0030B3E4 File Offset: 0x003095E4
		private long GetCompleteTick(int slotIndex)
		{
			int num;
			if (this.m_iCompleteMissionCount > 0)
			{
				if (slotIndex == 0)
				{
					return -1L;
				}
				num = slotIndex - 1;
			}
			else
			{
				num = slotIndex;
			}
			if (num < this.m_lstCompleteTime.Count)
			{
				return this.m_lstCompleteTime[num];
			}
			return 0L;
		}

		// Token: 0x06008F3A RID: 36666 RVA: 0x0030B428 File Offset: 0x00309628
		public void Init(ContentsType contentsType)
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
				this.m_ContentsType = contentsType;
				this.m_iCompleteMissionCount = 0;
				NKCUtil.SetGameobjectActive(this.m_objEvent, false);
				NKCUtil.SetGameobjectActive(this.m_objNoOngoingMission, true);
				NKCUtil.SetGameobjectActive(this.m_objHasMissionComplete, false);
				NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
				NKCUtil.SetGameobjectActive(this.m_objDiveAlert, false);
				NKCUtil.SetGameobjectActive(this.m_objShadowAlert, false);
				NKCUtil.SetGameobjectActive(this.m_objFierceBattleAlert, false);
				NKCUtil.SetGameobjectActive(this.m_objIsOngoingMission, false);
				NKCUtil.SetGameobjectActive(this.m_objCompleteMission, false);
				NKCUtil.SetGameobjectActive(this.m_objTrimEnterCount, false);
				for (int i = 0; i < this.m_lstWorldMapMissionMoniter.Count; i++)
				{
					this.m_lstWorldMapMissionMoniter[i].SetActive(false);
				}
			}
		}

		// Token: 0x06008F3B RID: 36667 RVA: 0x0030B520 File Offset: 0x00309720
		private void Update()
		{
			if (this.m_bLocked)
			{
				return;
			}
			this.m_fUpdateTimer -= Time.deltaTime;
			if (this.m_fUpdateTimer <= 0f)
			{
				this.m_fUpdateTimer = 1f;
				while (this.m_lstCompleteTime.Count > 0 && NKCSynchronizedTime.IsFinished(this.m_lstCompleteTime[0]))
				{
					this.m_lstCompleteTime.RemoveAt(0);
					this.m_iCompleteMissionCount++;
					this.SetNotify(true);
					this.UpdateCompleteMissionUI();
				}
				this.UpdateFierceTimeUI();
			}
		}

		// Token: 0x06008F3C RID: 36668 RVA: 0x0030B5B0 File Offset: 0x003097B0
		protected override void SetNotify(bool value)
		{
			base.SetNotify(value);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, value);
		}

		// Token: 0x06008F3D RID: 36669 RVA: 0x0030B5C8 File Offset: 0x003097C8
		protected override void ContentsUpdate(NKMUserData userData)
		{
			this.m_lstCompleteTime.Clear();
			this.m_iCompleteMissionCount = 0;
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in userData.m_WorldmapData.worldMapCityDataMap)
			{
				NKMWorldMapCityData value = keyValuePair.Value;
				if (value != null && value.HasMission())
				{
					if (value.IsMissionFinished(NKCSynchronizedTime.GetServerUTCTime(0.0)))
					{
						this.m_iCompleteMissionCount++;
					}
					else
					{
						this.m_lstCompleteTime.Add(value.worldMapMission.completeTime);
					}
				}
			}
			this.m_lstCompleteTime.Sort();
			this.UpdateCompleteMissionUI();
			if (this.m_iCompleteMissionCount > 0)
			{
				this.SetNotify(true);
				return;
			}
			if (userData.m_DiveGameData != null)
			{
				this.SetNotify(true);
			}
			this.SetNotify(false);
		}

		// Token: 0x06008F3E RID: 36670 RVA: 0x0030B6B8 File Offset: 0x003098B8
		private void SetTimeUI()
		{
			if (this.m_lstWorldMapMissionMoniter == null)
			{
				return;
			}
			int num = 0;
			if (this.m_iCompleteMissionCount > 0)
			{
				num = 1;
				this.m_lstWorldMapMissionMoniter[0].SetActive(true);
				this.m_lstWorldMapMissionMoniter[0].SetColor(this.m_colCompletedBG, this.m_colCompletedText);
				this.m_lstWorldMapMissionMoniter[0].SetCompletedText(this.m_iCompleteMissionCount);
				NKCUtil.SetGameobjectActive(this.m_objHasMissionComplete, true);
				NKCUtil.SetGameobjectActive(this.m_objRedDot, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objHasMissionComplete, false);
				NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			}
			for (int i = num; i < this.m_lstWorldMapMissionMoniter.Count; i++)
			{
				NKCUILobbyMenuWorldmap.WorldmapMissionMoniter worldmapMissionMoniter = this.m_lstWorldMapMissionMoniter[i];
				long completeTick = this.GetCompleteTick(i);
				if (completeTick == 0L)
				{
					worldmapMissionMoniter.SetActive(false);
				}
				else if (completeTick >= 0L)
				{
					worldmapMissionMoniter.SetActive(true);
					worldmapMissionMoniter.SetColor(this.m_colProgressBG, this.m_colProgressText);
					worldmapMissionMoniter.UpdateTime(completeTick);
				}
			}
			this.m_fUpdateTimer = 1f;
		}

		// Token: 0x06008F3F RID: 36671 RVA: 0x0030B7BC File Offset: 0x003099BC
		private void UpdateCompleteMissionUI()
		{
			int count = this.m_lstCompleteTime.Count;
			if (this.m_iCompleteMissionCount > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoOngoingMission, false);
				NKCUtil.SetGameobjectActive(this.m_objIsOngoingMission, false);
				NKCUtil.SetGameobjectActive(this.m_objCompleteMission, true);
				NKCUtil.SetLabelText(this.m_txtNumCompleteMission, string.Format(NKCUtilString.GET_STRING_LOBBY_CITY_MISSION_COMPLETE, this.m_iCompleteMissionCount));
				NKCUtil.SetGameobjectActive(this.m_objRedDot, true);
				return;
			}
			if (count > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoOngoingMission, false);
				NKCUtil.SetGameobjectActive(this.m_objIsOngoingMission, true);
				NKCUtil.SetGameobjectActive(this.m_objCompleteMission, false);
				NKCUtil.SetLabelText(this.m_txtNumOnGoingMission, string.Format(NKCUtilString.GET_STRING_LOBBY_CITY_MISSION_ONGOING, count));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNoOngoingMission, true);
			NKCUtil.SetGameobjectActive(this.m_objIsOngoingMission, false);
			NKCUtil.SetGameobjectActive(this.m_objCompleteMission, false);
		}

		// Token: 0x06008F40 RID: 36672 RVA: 0x0030B898 File Offset: 0x00309A98
		private void UpdateTimeUI()
		{
			if (this.m_lstWorldMapMissionMoniter == null)
			{
				return;
			}
			for (int i = 0; i < this.m_lstWorldMapMissionMoniter.Count; i++)
			{
				NKCUILobbyMenuWorldmap.WorldmapMissionMoniter worldmapMissionMoniter = this.m_lstWorldMapMissionMoniter[i];
				long completeTick = this.GetCompleteTick(i);
				if (completeTick > 0L)
				{
					worldmapMissionMoniter.UpdateTime(completeTick);
				}
			}
		}

		// Token: 0x06008F41 RID: 36673 RVA: 0x0030B8E8 File Offset: 0x00309AE8
		private void UpdateFierceTimeUI()
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (this.m_objFierceBattleAlert != null && this.m_objFierceBattleAlert.activeSelf && nkcfierceBattleSupportDataMgr != null)
			{
				DateTime dateTime = NKMTime.LocalToUTC(nkcfierceBattleSupportDataMgr.FierceTemplet.FierceGameEnd, 0);
				NKCUtil.SetLabelText(this.m_txtFierceBattleTimeLeft, string.Format("{0}{1}", NKCSynchronizedTime.GetTimeLeftString(dateTime), NKCUtilString.GET_STRING_LOBBY_FIERCEBATTLE_TIME_REMAIN));
				if (NKCSynchronizedTime.IsFinished(dateTime))
				{
					NKCUtil.SetGameobjectActive(this.m_objFierceBattleAlert, false);
				}
			}
		}

		// Token: 0x06008F42 RID: 36674 RVA: 0x0030B964 File Offset: 0x00309B64
		private void OnButton()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.WORLDMAP, 0);
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetShowIntro();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, false);
		}

		// Token: 0x06008F43 RID: 36675 RVA: 0x0030B993 File Offset: 0x00309B93
		public override void CleanUp()
		{
			if (this.m_VideoTexture != null)
			{
				this.m_VideoTexture.CleanUp();
			}
		}

		// Token: 0x04007C41 RID: 31809
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007C42 RID: 31810
		public NKCUIComVideoTexture m_VideoTexture;

		// Token: 0x04007C43 RID: 31811
		public List<NKCUILobbyMenuWorldmap.WorldmapMissionMoniter> m_lstWorldMapMissionMoniter;

		// Token: 0x04007C44 RID: 31812
		public GameObject m_objNoOngoingMission;

		// Token: 0x04007C45 RID: 31813
		public GameObject m_objEvent;

		// Token: 0x04007C46 RID: 31814
		public GameObject m_objDiveAlert;

		// Token: 0x04007C47 RID: 31815
		public GameObject m_objShadowAlert;

		// Token: 0x04007C48 RID: 31816
		public GameObject m_objShadowProgress;

		// Token: 0x04007C49 RID: 31817
		public Text m_txtShadowLife;

		// Token: 0x04007C4A RID: 31818
		public GameObject m_objFierceBattleAlert;

		// Token: 0x04007C4B RID: 31819
		public Text m_txtFierceBattleTimeLeft;

		// Token: 0x04007C4C RID: 31820
		public GameObject m_objTrimEnterCount;

		// Token: 0x04007C4D RID: 31821
		public Text m_txtTrimEnterCount;

		// Token: 0x04007C4E RID: 31822
		public GameObject m_objIsOngoingMission;

		// Token: 0x04007C4F RID: 31823
		public Text m_txtNumOnGoingMission;

		// Token: 0x04007C50 RID: 31824
		public GameObject m_objCompleteMission;

		// Token: 0x04007C51 RID: 31825
		public Text m_txtNumCompleteMission;

		// Token: 0x04007C52 RID: 31826
		public GameObject m_objHasMissionComplete;

		// Token: 0x04007C53 RID: 31827
		public GameObject m_objRedDot;

		// Token: 0x04007C54 RID: 31828
		public Color m_colCompletedBG;

		// Token: 0x04007C55 RID: 31829
		public Color m_colCompletedText;

		// Token: 0x04007C56 RID: 31830
		public Color m_colProgressBG;

		// Token: 0x04007C57 RID: 31831
		public Color m_colProgressText;

		// Token: 0x04007C58 RID: 31832
		private int m_iCompleteMissionCount;

		// Token: 0x04007C59 RID: 31833
		private List<long> m_lstCompleteTime = new List<long>();

		// Token: 0x04007C5A RID: 31834
		private float m_fUpdateTimer = 1f;

		// Token: 0x020019D9 RID: 6617
		[Serializable]
		public class WorldmapMissionMoniter
		{
			// Token: 0x0600BA58 RID: 47704 RVA: 0x0036DEF8 File Offset: 0x0036C0F8
			public void SetColor(Color colBG, Color colText)
			{
				this.m_imgBG.color = colBG;
				this.m_lbText.color = colText;
			}

			// Token: 0x0600BA59 RID: 47705 RVA: 0x0036DF12 File Offset: 0x0036C112
			public void SetCompletedText(int completedCount)
			{
				NKCUtil.SetGameobjectActive(this.m_objRoot, true);
				this.m_lbText.text = NKCUtilString.GET_STRING_MISSION_COMPLETE + " " + completedCount.ToString();
			}

			// Token: 0x0600BA5A RID: 47706 RVA: 0x0036DF41 File Offset: 0x0036C141
			public void SetActive(bool value)
			{
				NKCUtil.SetGameobjectActive(this.m_objRoot, value);
			}

			// Token: 0x0600BA5B RID: 47707 RVA: 0x0036DF4F File Offset: 0x0036C14F
			public void UpdateTime(long finishTime)
			{
				this.m_lbText.text = NKCSynchronizedTime.GetTimeLeftString(finishTime);
			}

			// Token: 0x0400AD04 RID: 44292
			public GameObject m_objRoot;

			// Token: 0x0400AD05 RID: 44293
			public Image m_imgBG;

			// Token: 0x0400AD06 RID: 44294
			public Text m_lbText;

			// Token: 0x02001A97 RID: 6807
			public enum eMode
			{
				// Token: 0x0400AEA2 RID: 44706
				TimeCount,
				// Token: 0x0400AEA3 RID: 44707
				CompletedMission
			}
		}
	}
}
