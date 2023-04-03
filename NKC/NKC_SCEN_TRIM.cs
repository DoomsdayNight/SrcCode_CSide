using System;
using ClientPacket.Mode;
using NKC.UI;
using NKC.UI.Trim;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200072E RID: 1838
	public class NKC_SCEN_TRIM : NKC_SCEN_BASIC
	{
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x00160703 File Offset: 0x0015E903
		public int TrimIntervalId
		{
			get
			{
				return this.m_currentTrimIntervalId;
			}
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x0016070B File Offset: 0x0015E90B
		public NKC_SCEN_TRIM()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_TRIM;
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x0016071B File Offset: 0x0015E91B
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			NKCUITrimUtility.TrimIntervalJoin();
			if (!NKCUIManager.IsValid(this.m_loadUIDataTrimMain))
			{
				this.m_loadUIDataTrimMain = NKCUITrimMain.OpenNewInstanceAsync();
			}
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x00160740 File Offset: 0x0015E940
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_uiTrimMain == null)
			{
				if (this.m_loadUIDataTrimMain != null && this.m_loadUIDataTrimMain.CheckLoadAndGetInstance<NKCUITrimMain>(out this.m_uiTrimMain))
				{
					this.m_uiTrimMain.Init();
					NKCUtil.SetGameobjectActive(this.m_uiTrimMain.gameObject, false);
				}
				else
				{
					Debug.LogError("NKC_SCEN_TRIM.ScenLoadUIComplete - ui load AB_UI_TRIM failed");
				}
			}
			NKCUIDeckViewer instance = NKCUIDeckViewer.Instance;
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x001607AC File Offset: 0x0015E9AC
		public override void ScenStart()
		{
			base.ScenStart();
			if (this.m_uiTrimMain == null)
			{
				Debug.LogError("TrimMain ui not found");
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GAME_LOAD_FAILED, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			NKMTrimIntervalTemplet nkmtrimIntervalTemplet = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
			this.m_currentTrimIntervalId = 0;
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.DIMENSION_TRIM, 0, 0))
			{
				string lockedMessage = NKCContentManager.GetLockedMessage(ContentsType.DIMENSION_TRIM, 0);
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, lockedMessage, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			if (nkmtrimIntervalTemplet != null && NKCUITrimUtility.OpenTagEnabled)
			{
				this.m_currentTrimIntervalId = nkmtrimIntervalTemplet.TrimIntervalID;
				this.m_uiTrimMain.Open(nkmtrimIntervalTemplet, this.m_reservedTrimId);
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRIM_NOT_INTERVAL_TIME, delegate()
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
			}, "");
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x001608B5 File Offset: 0x0015EAB5
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.m_reservedTrimId = 0;
			NKCUITrimMain uiTrimMain = this.m_uiTrimMain;
			if (uiTrimMain != null)
			{
				uiTrimMain.Close();
			}
			this.UnloadUI();
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x001608DB File Offset: 0x0015EADB
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_uiTrimMain = null;
			NKCUIManager.LoadedUIData loadUIDataTrimMain = this.m_loadUIDataTrimMain;
			if (loadUIDataTrimMain != null)
			{
				loadUIDataTrimMain.CloseInstance();
			}
			this.m_loadUIDataTrimMain = null;
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x00160902 File Offset: 0x0015EB02
		public void SetReservedTrim(TrimModeState TrimModeState)
		{
			if (TrimModeState == null)
			{
				return;
			}
			this.m_reservedTrimId = TrimModeState.trimId;
		}

		// Token: 0x04003887 RID: 14471
		private NKCUITrimMain m_uiTrimMain;

		// Token: 0x04003888 RID: 14472
		private NKCUIManager.LoadedUIData m_loadUIDataTrimMain;

		// Token: 0x04003889 RID: 14473
		private int m_currentTrimIntervalId;

		// Token: 0x0400388A RID: 14474
		private int m_reservedTrimId;
	}
}
