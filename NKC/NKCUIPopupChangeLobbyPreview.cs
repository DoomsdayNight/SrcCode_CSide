using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009CD RID: 2509
	public class NKCUIPopupChangeLobbyPreview : NKCUIBase
	{
		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06006B0B RID: 27403 RVA: 0x0022C4C8 File Offset: 0x0022A6C8
		public static NKCUIPopupChangeLobbyPreview Instance
		{
			get
			{
				if (NKCUIPopupChangeLobbyPreview.m_Instance == null)
				{
					NKCUIPopupChangeLobbyPreview.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupChangeLobbyPreview>("ab_ui_nkm_ui_user_info", "NKM_UI_USER_INFO_LOBBY_CHANGE_PREVIEW", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupChangeLobbyPreview.CleanupInstance)).GetInstance<NKCUIPopupChangeLobbyPreview>();
					NKCUIPopupChangeLobbyPreview.m_Instance.Init();
				}
				return NKCUIPopupChangeLobbyPreview.m_Instance;
			}
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x0022C517 File Offset: 0x0022A717
		private static void CleanupInstance()
		{
			NKCUIPopupChangeLobbyPreview.m_Instance = null;
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06006B0D RID: 27405 RVA: 0x0022C51F File Offset: 0x0022A71F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupChangeLobbyPreview.m_Instance != null && NKCUIPopupChangeLobbyPreview.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006B0E RID: 27406 RVA: 0x0022C53A File Offset: 0x0022A73A
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupChangeLobbyPreview.m_Instance != null && NKCUIPopupChangeLobbyPreview.m_Instance.IsOpen)
			{
				NKCUIPopupChangeLobbyPreview.m_Instance.Close();
			}
		}

		// Token: 0x06006B0F RID: 27407 RVA: 0x0022C55F File Offset: 0x0022A75F
		private void OnDestroy()
		{
			NKCUIPopupChangeLobbyPreview.m_Instance = null;
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06006B10 RID: 27408 RVA: 0x0022C567 File Offset: 0x0022A767
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06006B11 RID: 27409 RVA: 0x0022C56A File Offset: 0x0022A76A
		public override string MenuName
		{
			get
			{
				return "Preset preview";
			}
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x0022C571 File Offset: 0x0022A771
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006B13 RID: 27411 RVA: 0x0022C57F File Offset: 0x0022A77F
		private void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(this.OnOK));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(base.Close));
		}

		// Token: 0x06006B14 RID: 27412 RVA: 0x0022C5BD File Offset: 0x0022A7BD
		private void OnOK()
		{
			base.Close();
			NKCUIPopupChangeLobbyPreview.OnConfirm onConfirm = this.dOnConfirm;
			if (onConfirm == null)
			{
				return;
			}
			onConfirm(this.m_Index);
		}

		// Token: 0x06006B15 RID: 27413 RVA: 0x0022C5DC File Offset: 0x0022A7DC
		public void Open(int presetIndex, string title, string previewPath, string desc, NKCUIPopupChangeLobbyPreview.OnConfirm onConfirm)
		{
			this.m_Index = presetIndex;
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			NKCUtil.SetLabelText(this.m_lbDesc, desc);
			this.dOnConfirm = onConfirm;
			Sprite sprite = NKCResourceUtility.LoadNewSprite(previewPath, 100f);
			NKCUtil.SetGameobjectActive(this.m_rtPreviewBG, sprite != null);
			NKCUtil.SetGameobjectActive(this.m_imgPreview, sprite != null);
			NKCUtil.SetGameobjectActive(this.m_objPreviewNotExist, sprite == null);
			if (sprite != null)
			{
				NKCUtil.SetImageSprite(this.m_imgPreview, sprite, false);
				float num = this.m_rtPreviewLimit.GetWidth() - this.m_fPadding * 2f;
				float num2 = this.m_rtPreviewLimit.GetHeight() - this.m_fPadding * 2f;
				float num3 = num / num2;
				float num4 = (float)sprite.texture.width / (float)sprite.texture.height;
				float num5;
				float num6;
				if (num3 > num4)
				{
					num5 = num2;
					num6 = num4 * num5;
				}
				else
				{
					num6 = num;
					num5 = num6 / num4;
				}
				float x = num6 + this.m_fPadding * 2f;
				float y = num5 + this.m_fPadding * 2f;
				this.m_rtPreviewBG.SetSize(new Vector2(x, y));
			}
			base.UIOpened(true);
		}

		// Token: 0x040056AC RID: 22188
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_user_info";

		// Token: 0x040056AD RID: 22189
		private const string UI_ASSET_NAME = "NKM_UI_USER_INFO_LOBBY_CHANGE_PREVIEW";

		// Token: 0x040056AE RID: 22190
		private static NKCUIPopupChangeLobbyPreview m_Instance;

		// Token: 0x040056AF RID: 22191
		public Text m_lbTitle;

		// Token: 0x040056B0 RID: 22192
		public Text m_lbDesc;

		// Token: 0x040056B1 RID: 22193
		public RectTransform m_rtPreviewLimit;

		// Token: 0x040056B2 RID: 22194
		public RectTransform m_rtPreviewBG;

		// Token: 0x040056B3 RID: 22195
		public Image m_imgPreview;

		// Token: 0x040056B4 RID: 22196
		public GameObject m_objPreviewNotExist;

		// Token: 0x040056B5 RID: 22197
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040056B6 RID: 22198
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x040056B7 RID: 22199
		public float m_fPadding = 27.5f;

		// Token: 0x040056B8 RID: 22200
		private NKCUIPopupChangeLobbyPreview.OnConfirm dOnConfirm;

		// Token: 0x040056B9 RID: 22201
		private int m_Index;

		// Token: 0x020016D3 RID: 5843
		// (Invoke) Token: 0x0600B16F RID: 45423
		public delegate void OnConfirm(int index);
	}
}
