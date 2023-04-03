using System;
using NKC.Patcher;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200090F RID: 2319
	public class NKCUIOverlayPatchProgress : NKCUIBase
	{
		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06005CB7 RID: 23735 RVA: 0x001CAB43 File Offset: 0x001C8D43
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06005CB8 RID: 23736 RVA: 0x001CAB48 File Offset: 0x001C8D48
		public static NKCUIOverlayPatchProgress Instance
		{
			get
			{
				if (NKCUIOverlayPatchProgress.m_Instance == null)
				{
					NKCUIOverlayPatchProgress.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOverlayPatchProgress>("ab_ui_nkm_ui_hud", "AB_UI_GAME_HUD_PATCH_DOWNLOAD", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOverlayPatchProgress.CleanupInstance)).GetInstance<NKCUIOverlayPatchProgress>();
					NKCUIOverlayPatchProgress instance = NKCUIOverlayPatchProgress.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCUIOverlayPatchProgress.m_Instance;
			}
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x001CAB9D File Offset: 0x001C8D9D
		private static void CleanupInstance()
		{
			NKCUIOverlayPatchProgress.m_Instance = null;
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06005CBA RID: 23738 RVA: 0x001CABA5 File Offset: 0x001C8DA5
		public static bool HasInstance
		{
			get
			{
				return NKCUIOverlayPatchProgress.m_Instance != null;
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06005CBB RID: 23739 RVA: 0x001CABB2 File Offset: 0x001C8DB2
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOverlayPatchProgress.m_Instance != null && NKCUIOverlayPatchProgress.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x001CABCD File Offset: 0x001C8DCD
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOverlayPatchProgress.m_Instance != null && NKCUIOverlayPatchProgress.m_Instance.IsOpen)
			{
				NKCUIOverlayPatchProgress.m_Instance.Close();
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06005CBD RID: 23741 RVA: 0x001CABF2 File Offset: 0x001C8DF2
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06005CBE RID: 23742 RVA: 0x001CABF5 File Offset: 0x001C8DF5
		public override string MenuName
		{
			get
			{
				return "Patch Progress Overlay";
			}
		}

		// Token: 0x06005CBF RID: 23743 RVA: 0x001CABFC File Offset: 0x001C8DFC
		public override void CloseInternal()
		{
			NKCUIBase.SetGameObjectActive(base.gameObject, false);
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x001CAC0A File Offset: 0x001C8E0A
		public void Init()
		{
			NKCUIBase.SetGameObjectActive(base.gameObject, false);
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x001CAC18 File Offset: 0x001C8E18
		public static void OpenWhenDownloading()
		{
			if (NKCPatchDownloader.Instance == null)
			{
				return;
			}
			if (!NKCPatchDownloader.Instance.IsBackGroundDownload())
			{
				NKCUIOverlayPatchProgress instance = NKCUIOverlayPatchProgress.m_Instance;
				if (instance == null)
				{
					return;
				}
				instance.Close();
				return;
			}
			else if ((int)(NKCPatchDownloader.Instance.DownloadPercent * 100f) >= 100)
			{
				NKCUIOverlayPatchProgress instance2 = NKCUIOverlayPatchProgress.m_Instance;
				if (instance2 == null)
				{
					return;
				}
				instance2.Close();
				return;
			}
			else
			{
				if (NKCUIOverlayPatchProgress.Instance == null)
				{
					return;
				}
				if (!NKCUIOverlayPatchProgress.m_Instance.IsOpen)
				{
					NKCUIOverlayPatchProgress.m_Instance.Open();
				}
				return;
			}
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x001CAC98 File Offset: 0x001C8E98
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x001CACB0 File Offset: 0x001C8EB0
		private void Update()
		{
			if (NKCPatchDownloader.Instance == null)
			{
				return;
			}
			if (NKCUIOverlayPatchProgress.m_Instance == null)
			{
				return;
			}
			if (!NKCUIOverlayPatchProgress.m_Instance.IsOpen)
			{
				return;
			}
			float downloadPercent = NKCPatchDownloader.Instance.DownloadPercent;
			this.m_lbCurrentPercent.text = string.Format("{0}%", (int)(downloadPercent * 100f));
			if ((int)(downloadPercent * 100f) >= 100)
			{
				base.Close();
			}
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x001CAD25 File Offset: 0x001C8F25
		private void OnDestroy()
		{
			NKCUIOverlayPatchProgress.m_Instance = null;
		}

		// Token: 0x040048F0 RID: 18672
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_hud";

		// Token: 0x040048F1 RID: 18673
		public const string UI_ASSET_NAME = "AB_UI_GAME_HUD_PATCH_DOWNLOAD";

		// Token: 0x040048F2 RID: 18674
		private static NKCUIOverlayPatchProgress m_Instance;

		// Token: 0x040048F3 RID: 18675
		public Text m_lbCurrentPercent;
	}
}
