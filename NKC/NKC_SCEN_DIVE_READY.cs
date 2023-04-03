using System;
using ClientPacket.Mode;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000706 RID: 1798
	public class NKC_SCEN_DIVE_READY : NKC_SCEN_BASIC
	{
		// Token: 0x06004688 RID: 18056 RVA: 0x001566CD File Offset: 0x001548CD
		public void SetResetTicketChargeDate(DateTime _ResetTicketChargeDate)
		{
			this.m_bGetResetTicketChargeDate = true;
			this.m_ResetTicketChargeDate = _ResetTicketChargeDate;
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x001566DD File Offset: 0x001548DD
		public NKC_SCEN_DIVE_READY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_DIVE_READY;
			this.m_NUF_DIVE_READY = GameObject.Find("NUF_DIVE_READY");
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x00156708 File Offset: 0x00154908
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NUF_DIVE_READY.SetActive(true);
			if (!this.m_bLoadedUI && this.m_NKC_SCEN_DIVE_READY_UI_DATA.m_NUF_LOGIN_PREFAB == null)
			{
				this.m_NKC_SCEN_DIVE_READY_UI_DATA.m_NUF_LOGIN_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NUF_DIVE_READY_PREFAB", true, null);
			}
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x00156758 File Offset: 0x00154958
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!this.m_bLoadedUI)
			{
				this.m_NKC_SCEN_DIVE_READY_UI_DATA.m_NUF_LOGIN_PREFAB.m_Instant.transform.SetParent(this.m_NUF_DIVE_READY.transform, false);
				this.m_NKCUIDeckView = NKCUIDeckViewer.Instance;
				this.m_NKCUIDiveReady = NKCUIDiveReady.InitUI();
			}
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x001567AF File Offset: 0x001549AF
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			this.m_NKCUIDeckView.LoadComplete();
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x001567C2 File Offset: 0x001549C2
		public override void ScenStart()
		{
			base.ScenStart();
			this.DoAfterGettingResetTicketChargeDate();
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x001567D0 File Offset: 0x001549D0
		public void Refresh()
		{
			if (this.m_NKCUIDiveReady != null && this.m_NKCUIDiveReady.IsOpen)
			{
				this.m_NKCUIDiveReady.Refresh();
			}
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x001567F8 File Offset: 0x001549F8
		public void DoAfterGettingResetTicketChargeDate()
		{
			if (this.m_NKCUIDiveReady != null)
			{
				this.m_NKCUIDiveReady.Open(this.m_reservedCityID, this.m_reservedEventDiveID, this.m_ResetTicketChargeDate);
			}
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x00156825 File Offset: 0x00154A25
		public void DoAfterLogout()
		{
			this.m_bGetResetTicketChargeDate = false;
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x0015682E File Offset: 0x00154A2E
		public void SetTargetEventID(int cityID, int eventDiveID)
		{
			this.m_reservedCityID = cityID;
			this.m_reservedEventDiveID = eventDiveID;
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x0015683E File Offset: 0x00154A3E
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUIDeckView.IsOpen)
			{
				this.m_NKCUIDeckView.Close();
			}
			this.m_NKCUIDiveReady.Close();
			this.m_NUF_DIVE_READY.SetActive(false);
			this.UnloadUI();
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x0015687B File Offset: 0x00154A7B
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCUIDeckView = null;
			this.m_NKCUIDiveReady = null;
			this.m_NKC_SCEN_DIVE_READY_UI_DATA.Init();
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x0015689C File Offset: 0x00154A9C
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			if (!NKCCamera.IsTrackingCameraPos())
			{
				NKCCamera.TrackingPos(10f, NKMRandom.Range(-50f, 50f), NKMRandom.Range(-50f, 50f), NKMRandom.Range(-1000f, -900f));
			}
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x001568ED File Offset: 0x00154AED
		public void OnRecv(NKMPacket_DIVE_GIVE_UP_ACK cNKMPacket_DIVE_GIVE_UP_ACK)
		{
			this.m_NKCUIDiveReady.OnRecv(cNKMPacket_DIVE_GIVE_UP_ACK);
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x001568FB File Offset: 0x00154AFB
		public void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			this.m_NKCUIDiveReady.OnRecv(cNKMPacket_DIVE_EXPIRE_NOT);
		}

		// Token: 0x04003786 RID: 14214
		private GameObject m_NUF_DIVE_READY;

		// Token: 0x04003787 RID: 14215
		private NKC_SCEN_DIVE_READY_UI_DATA m_NKC_SCEN_DIVE_READY_UI_DATA = new NKC_SCEN_DIVE_READY_UI_DATA();

		// Token: 0x04003788 RID: 14216
		private NKCUIDeckViewer m_NKCUIDeckView;

		// Token: 0x04003789 RID: 14217
		private NKCUIDiveReady m_NKCUIDiveReady;

		// Token: 0x0400378A RID: 14218
		private bool m_bGetResetTicketChargeDate;

		// Token: 0x0400378B RID: 14219
		private DateTime m_ResetTicketChargeDate;

		// Token: 0x0400378C RID: 14220
		private int m_reservedCityID;

		// Token: 0x0400378D RID: 14221
		private int m_reservedEventDiveID;
	}
}
