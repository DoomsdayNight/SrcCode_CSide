using System;
using ClientPacket.Warfare;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x02000725 RID: 1829
	public class NKC_SCEN_OPERATION : NKC_SCEN_BASIC
	{
		// Token: 0x060048A8 RID: 18600 RVA: 0x0015EDE5 File Offset: 0x0015CFE5
		public bool IsFirstOpen()
		{
			return this.m_bFirstOpen;
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0015EDED File Offset: 0x0015CFED
		public void SetFirstOpen(bool bValue)
		{
			this.m_bFirstOpen = bValue;
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x0015EDF6 File Offset: 0x0015CFF6
		public EPISODE_CATEGORY GetLatest_EP_CATEGORY()
		{
			return this.m_Latest_EPISODE_CATEGORY;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x0015EDFE File Offset: 0x0015CFFE
		public void SetLatest_EP_CATEGORY(EPISODE_CATEGORY epCate)
		{
			this.m_Latest_EPISODE_CATEGORY = epCate;
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x0015EE07 File Offset: 0x0015D007
		public int GetLatest_EPISODE_ID()
		{
			return this.m_Latest_EPISODE_ID;
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x0015EE0F File Offset: 0x0015D00F
		public void SetLatest_EPISODE_ID(int episodeID)
		{
			this.m_Latest_EPISODE_ID = episodeID;
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x0015EE18 File Offset: 0x0015D018
		public NKC_SCEN_OPERATION()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_OPERATION;
			this.m_NUM_OPERATION = GameObject.Find("NUM_OPERATION");
			this.m_NUF_OPERATION = GameObject.Find("NUF_OPERATION");
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x0015EE70 File Offset: 0x0015D070
		public void UpdateEPRewardStatus()
		{
			this.m_NKCUIOperationViewer.UpdateEPRewardStatus();
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0015EE7D File Offset: 0x0015D07D
		public void UpdateEPCategory()
		{
			this.m_NKCUIOperationViewer.Open(false, this.m_Latest_EPISODE_ID);
			this.m_Latest_EPISODE_ID = 0;
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x0015EE98 File Offset: 0x0015D098
		public GameObject GetNUM_OPERATION_BG()
		{
			return this.m_NUM_OPERATION;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0015EEA0 File Offset: 0x0015D0A0
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NUM_OPERATION.SetActive(true);
			this.m_NUF_OPERATION.SetActive(true);
			if (!this.m_bLoadedUI)
			{
				if (this.m_NKC_SCEN_OPERATION_UI_DATA.m_NUM_OPERATION_PREFAB == null)
				{
					this.m_NKC_SCEN_OPERATION_UI_DATA.m_NUM_OPERATION_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_OPERATION", "NUM_OPERATION_PREFAB", true, null);
				}
				if (this.m_NKC_SCEN_OPERATION_UI_DATA.m_NUF_OPERATION_PREFAB == null)
				{
					this.m_NKC_SCEN_OPERATION_UI_DATA.m_NUF_OPERATION_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_OPERATION", "NUF_OPERATION_PREFAB", true, null);
				}
			}
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x0015EF28 File Offset: 0x0015D128
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!this.m_bLoadedUI)
			{
				this.m_NKC_SCEN_OPERATION_UI_DATA.m_NUM_OPERATION_PREFAB.m_Instant.transform.SetParent(this.m_NUM_OPERATION.transform, false);
				this.m_NKC_SCEN_OPERATION_UI_DATA.m_NUF_OPERATION_PREFAB.m_Instant.transform.SetParent(this.m_NUF_OPERATION.transform, false);
				this.m_NKCUIOperationViewer = NKCUIOperationViewer.InitUI();
			}
			if (this.m_bFirstOpen)
			{
				NKCUIOperationViewer nkcuioperationViewer = this.m_NKCUIOperationViewer;
				if (nkcuioperationViewer != null)
				{
					nkcuioperationViewer.SetFirstOpen();
				}
			}
			if (this.m_NKCUIOperationViewer != null)
			{
				this.m_NKCUIOperationViewer.PreLoad();
			}
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x0015EFCC File Offset: 0x0015D1CC
		public override void ScenStart()
		{
			base.ScenStart();
			this.m_NKCUIOperationViewer.Open(this.m_bFirstOpen, this.m_Latest_EPISODE_ID);
			this.m_Latest_EPISODE_ID = 0;
			if (!this.m_NUM_OPERATION.activeSelf)
			{
				this.m_NUM_OPERATION.SetActive(true);
			}
			NKCCamera.EnableBloom(false);
			NKCCamera.GetCamera().orthographic = false;
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, -1000f);
			if (this.m_objNUM_OPERATION_BG == null)
			{
				this.m_objNUM_OPERATION_BG = NKCUIManager.OpenUI("NUM_OPERATION_BG");
				this.m_objNUM_OPERATION_BG.transform.position = new Vector3(0f, 0f, 1000f);
			}
			if (this.m_objNUM_OPERATION_BG != null)
			{
				NKCCamera.RescaleRectToCameraFrustrum(this.m_objNUM_OPERATION_BG.GetComponent<RectTransform>(), NKCCamera.GetCamera(), new Vector2(200f, 200f), -1000f, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
			}
			this.m_bFirstOpen = false;
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x0015F0C4 File Offset: 0x0015D2C4
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.m_NKCUIOperationViewer.Close();
			if (this.m_NUM_OPERATION.activeSelf)
			{
				this.m_NUM_OPERATION.SetActive(false);
			}
			this.m_NUM_OPERATION.SetActive(false);
			this.m_NUF_OPERATION.SetActive(false);
			this.UnloadUI();
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0015F119 File Offset: 0x0015D319
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCUIOperationViewer = null;
			this.m_NKC_SCEN_OPERATION_UI_DATA.Init();
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x0015F134 File Offset: 0x0015D334
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			if (!NKCCamera.IsTrackingCameraPos())
			{
				NKCCamera.TrackingPos(10f, NKMRandom.Range(-50f, 50f), NKMRandom.Range(-50f, 50f), NKMRandom.Range(-1000f, -900f));
			}
			this.m_BloomIntensity.Update(Time.deltaTime);
			if (!this.m_BloomIntensity.IsTracking())
			{
				this.m_BloomIntensity.SetTracking(NKMRandom.Range(1f, 2f), 4f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			NKCCamera.SetBloomIntensity(this.m_BloomIntensity.GetNowValue());
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x0015F1D2 File Offset: 0x0015D3D2
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x0015F1D5 File Offset: 0x0015D3D5
		public void OnRecv(NKMPacket_WARFARE_EXPIRED_NOT cNKMPacket_WARFARE_EXPIRED_NOT)
		{
			if (this.m_NKCUIOperationViewer != null)
			{
				this.m_NKCUIOperationViewer.UpdateINGWarfareDirectGoUI();
			}
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x0015F1F0 File Offset: 0x0015D3F0
		public void SetTutorialMainstreamGuide(NKCGameEventManager.NKCGameEventTemplet eventTemplet, UnityAction Complete)
		{
			if (this.m_NKCUIOperationViewer != null)
			{
				this.m_NKCUIOperationViewer.SetTutorialMainstreamGuide(eventTemplet, Complete);
				return;
			}
			if (Complete != null)
			{
				Complete();
			}
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0015F217 File Offset: 0x0015D417
		public RectTransform GetDailyRect()
		{
			if (this.m_NKCUIOperationViewer != null)
			{
				return this.m_NKCUIOperationViewer.GetDailyRect();
			}
			return null;
		}

		// Token: 0x04003856 RID: 14422
		private GameObject m_NUM_OPERATION;

		// Token: 0x04003857 RID: 14423
		private GameObject m_NUF_OPERATION;

		// Token: 0x04003858 RID: 14424
		private NKC_SCEN_OPERATION_UI_DATA m_NKC_SCEN_OPERATION_UI_DATA = new NKC_SCEN_OPERATION_UI_DATA();

		// Token: 0x04003859 RID: 14425
		private NKCUIOperationViewer m_NKCUIOperationViewer;

		// Token: 0x0400385A RID: 14426
		private GameObject m_objNUM_OPERATION_BG;

		// Token: 0x0400385B RID: 14427
		private NKMTrackingFloat m_BloomIntensity = new NKMTrackingFloat();

		// Token: 0x0400385C RID: 14428
		private bool m_bFirstOpen = true;

		// Token: 0x0400385D RID: 14429
		private EPISODE_CATEGORY m_Latest_EPISODE_CATEGORY;

		// Token: 0x0400385E RID: 14430
		private int m_Latest_EPISODE_ID;
	}
}
