using System;
using NKC.Patcher;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000910 RID: 2320
	public class NKCUIPatcher : NKCUIBase
	{
		// Token: 0x06005CC6 RID: 23750 RVA: 0x001CAD35 File Offset: 0x001C8F35
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIPatcher.s_LoadedUIData))
			{
				NKCUIPatcher.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIPatcher>("ab_ui_patch", "NKM_UI_PATCH", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPatcher.CleanupInstance));
			}
			return NKCUIPatcher.s_LoadedUIData;
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06005CC7 RID: 23751 RVA: 0x001CAD69 File Offset: 0x001C8F69
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPatcher.s_LoadedUIData != null && NKCUIPatcher.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x001CAD7E File Offset: 0x001C8F7E
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIPatcher.s_LoadedUIData != null && NKCUIPatcher.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x001CAD93 File Offset: 0x001C8F93
		public static NKCUIPatcher GetInstance()
		{
			if (NKCUIPatcher.s_LoadedUIData != null && NKCUIPatcher.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIPatcher.s_LoadedUIData.GetInstance<NKCUIPatcher>();
			}
			return null;
		}

		// Token: 0x06005CCA RID: 23754 RVA: 0x001CADB4 File Offset: 0x001C8FB4
		public static void CleanupInstance()
		{
			NKCUIPatcher.s_LoadedUIData = null;
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x001CADBC File Offset: 0x001C8FBC
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06005CCC RID: 23756 RVA: 0x001CADCA File Offset: 0x001C8FCA
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06005CCD RID: 23757 RVA: 0x001CADCD File Offset: 0x001C8FCD
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06005CCE RID: 23758 RVA: 0x001CADD0 File Offset: 0x001C8FD0
		public override string MenuName
		{
			get
			{
				return "패치";
			}
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x001CADD8 File Offset: 0x001C8FD8
		public void Open()
		{
			base.gameObject.SetActive(true);
			this.m_lbAppVersion.text = string.Format("CounterSide {0}({1}/{2})", NKCUtilString.GetAppVersionText(), 845, NKMDataVersion.DataVersion);
			NKCUtil.SetGameobjectActive(this.m_objBackgroundDownloadNotice, NKCPatchDownloader.Instance.BackgroundDownloadAvailble);
			base.UIOpened(true);
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x001CAE3B File Offset: 0x001C903B
		public void Update()
		{
			this.SetDownloadPercent(NKCPatchDownloader.Instance.DownloadPercent);
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x001CAE4D File Offset: 0x001C904D
		private void SetDownloadPercent(float percent)
		{
			this.m_lbDownloadProgress.text = string.Format("{0}%", (int)(percent * 100f));
			this.m_slProgress.value = percent;
		}

		// Token: 0x040048F4 RID: 18676
		public const string ASSET_BUNDLE_NAME = "ab_ui_patch";

		// Token: 0x040048F5 RID: 18677
		public const string UI_ASSET_NAME = "NKM_UI_PATCH";

		// Token: 0x040048F6 RID: 18678
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x040048F7 RID: 18679
		[Header("왼쪽 위 텍스트")]
		public Text m_lbAppVersion;

		// Token: 0x040048F8 RID: 18680
		[Header("아래쪽")]
		public GameObject m_objBackgroundDownloadNotice;

		// Token: 0x040048F9 RID: 18681
		public Text m_lbDownloadProgress;

		// Token: 0x040048FA RID: 18682
		public Slider m_slProgress;
	}
}
