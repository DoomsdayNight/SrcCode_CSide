using System;
using ClientPacket.Mode;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000704 RID: 1796
	public class NKC_SCEN_DIVE : NKC_SCEN_BASIC
	{
		// Token: 0x06004670 RID: 18032 RVA: 0x0015640E File Offset: 0x0015460E
		public NKC_SCEN_DIVE()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_DIVE;
			this.m_NUM_DIVE = GameObject.Find("NUM_DIVE");
			this.m_NUF_DIVE = GameObject.Find("NUF_DIVE");
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x00156449 File Offset: 0x00154649
		public NKCDiveGame GetDiveGame()
		{
			return this.m_NKCDiveGame;
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x00156451 File Offset: 0x00154651
		public void OnLoginSuccess()
		{
			this.SetIntro(false);
			this.SetSectorAddEvent(false);
			this.SetSectorAddEventWhenStart(false);
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x00156468 File Offset: 0x00154668
		public void SetIntro(bool bSet = true)
		{
			NKCDiveGame.SetIntro(bSet);
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00156470 File Offset: 0x00154670
		public void SetSectorAddEvent(bool bSet = true)
		{
			NKCDiveGame.SetSectorAddEvent(bSet);
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x00156478 File Offset: 0x00154678
		public void SetSectorAddEventWhenStart(bool bSet = true)
		{
			NKCDiveGame.SetSectorAddEventWhenStart(bSet);
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x00156480 File Offset: 0x00154680
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NUM_DIVE.SetActive(true);
			this.m_NUF_DIVE.SetActive(true);
			if (!this.m_bLoadedUI)
			{
				if (this.m_NKC_SCEN_DIVE_UI_DATA.m_NUM_DIVE_PREFAB == null)
				{
					this.m_NKC_SCEN_DIVE_UI_DATA.m_NUM_DIVE_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NUM_DIVE_PREFAB", true, null);
				}
				if (this.m_NKC_SCEN_DIVE_UI_DATA.m_NUF_DIVE_PREFAB == null)
				{
					this.m_NKC_SCEN_DIVE_UI_DATA.m_NUF_DIVE_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NUF_DIVE_PREFAB", true, null);
				}
			}
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x00156508 File Offset: 0x00154708
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!this.m_bLoadedUI)
			{
				this.m_NKC_SCEN_DIVE_UI_DATA.m_NUM_DIVE_PREFAB.m_Instant.transform.SetParent(this.m_NUM_DIVE.transform, false);
				this.m_NKC_SCEN_DIVE_UI_DATA.m_NUF_DIVE_PREFAB.m_Instant.transform.SetParent(this.m_NUF_DIVE.transform, false);
				this.m_NKCDiveGame = NKCDiveGame.Init();
			}
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x0015657A File Offset: 0x0015477A
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x00156582 File Offset: 0x00154782
		public override void ScenStart()
		{
			base.ScenStart();
			this.m_NKCDiveGame.Open();
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x00156595 File Offset: 0x00154795
		public override void ScenEnd()
		{
			this.m_NKCDiveGame.Close();
			base.ScenEnd();
			this.m_NUM_DIVE.SetActive(false);
			this.m_NUF_DIVE.SetActive(false);
			this.UnloadUI();
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x001565C6 File Offset: 0x001547C6
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCDiveGame = null;
			this.m_NKC_SCEN_DIVE_UI_DATA.Init();
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x001565E0 File Offset: 0x001547E0
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x001565E8 File Offset: 0x001547E8
		public void OnRecv(NKMPacket_DIVE_SUICIDE_ACK cNKMPacket_DIVE_SUICIDE_ACK)
		{
			this.m_NKCDiveGame.OnRecv(cNKMPacket_DIVE_SUICIDE_ACK);
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x001565F6 File Offset: 0x001547F6
		public void OnRecv(NKMPacket_DIVE_SELECT_ARTIFACT_ACK cNKMPacket_DIVE_SELECT_ARTIFACT_ACK)
		{
			this.m_NKCDiveGame.OnRecv(cNKMPacket_DIVE_SELECT_ARTIFACT_ACK);
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x00156604 File Offset: 0x00154804
		public void OnRecv(NKMPacket_DIVE_MOVE_FORWARD_ACK cNKMPacket_DIVE_MOVE_FORWARD_ACK)
		{
			this.m_NKCDiveGame.OnRecv(cNKMPacket_DIVE_MOVE_FORWARD_ACK);
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x00156612 File Offset: 0x00154812
		public void OnRecv(NKMPacket_DIVE_GIVE_UP_ACK cNKMPacket_DIVE_GIVE_UP_ACK, bool bEventDive)
		{
			if (bEventDive)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReservedDiveReverseAni(true);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				return;
			}
			NKCUtil.SetDiveTargetEventID();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE_READY, true);
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00156647 File Offset: 0x00154847
		public void OnRecv(NKMPacket_DIVE_AUTO_ACK cNKMPacket_DIVE_AUTO_ACK)
		{
			this.m_NKCDiveGame.OnRecv(cNKMPacket_DIVE_AUTO_ACK);
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x00156655 File Offset: 0x00154855
		public void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			this.m_NKCDiveGame.OnRecv(cNKMPacket_DIVE_EXPIRE_NOT);
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x00156663 File Offset: 0x00154863
		public void TryGiveUp()
		{
			if (this.m_NKCDiveGame != null)
			{
				this.m_NKCDiveGame.GiveUp();
			}
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x0015667E File Offset: 0x0015487E
		public void TryTempLeave()
		{
			if (this.m_NKCDiveGame != null)
			{
				this.m_NKCDiveGame.TempLeave();
			}
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x00156699 File Offset: 0x00154899
		public void DoAfterLogout()
		{
			NKCDiveGame.SetReservedUnitDieShow(false, -1, NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_EXPLOSION);
		}

		// Token: 0x04003781 RID: 14209
		private GameObject m_NUM_DIVE;

		// Token: 0x04003782 RID: 14210
		private GameObject m_NUF_DIVE;

		// Token: 0x04003783 RID: 14211
		private NKC_SCEN_DIVE_UI_DATA m_NKC_SCEN_DIVE_UI_DATA = new NKC_SCEN_DIVE_UI_DATA();

		// Token: 0x04003784 RID: 14212
		private NKCDiveGame m_NKCDiveGame;
	}
}
