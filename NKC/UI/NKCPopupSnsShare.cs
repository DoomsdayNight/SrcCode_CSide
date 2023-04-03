using System;
using System.Collections;
using System.IO;
using NKC.Publisher;
using NKC.UI.Component;
using NKC.Util;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A88 RID: 2696
	public class NKCPopupSnsShare : NKCUIBase
	{
		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06007743 RID: 30531 RVA: 0x0027AE17 File Offset: 0x00279017
		public static NKCPopupSnsShare Instance
		{
			get
			{
				if (NKCPopupSnsShare.m_Instance == null)
				{
					NKCPopupSnsShare.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupSnsShare>("ab_ui_nkm_ui_result", "NKM_UI_RESULT_GET_SHARE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupSnsShare.CleanupInstance)).GetInstance<NKCPopupSnsShare>();
				}
				return NKCPopupSnsShare.m_Instance;
			}
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x0027AE51 File Offset: 0x00279051
		private static void CleanupInstance()
		{
			NKCPopupSnsShare.m_Instance = null;
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06007745 RID: 30533 RVA: 0x0027AE59 File Offset: 0x00279059
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06007746 RID: 30534 RVA: 0x0027AE5C File Offset: 0x0027905C
		public static bool HasInstance
		{
			get
			{
				return NKCPopupSnsShare.m_Instance != null;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06007747 RID: 30535 RVA: 0x0027AE69 File Offset: 0x00279069
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupSnsShare.m_Instance != null && NKCPopupSnsShare.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x0027AE84 File Offset: 0x00279084
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupSnsShare.m_Instance != null && NKCPopupSnsShare.m_Instance.IsOpen)
			{
				NKCPopupSnsShare.m_Instance.Close();
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06007749 RID: 30537 RVA: 0x0027AEA9 File Offset: 0x002790A9
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x0600774A RID: 30538 RVA: 0x0027AEAC File Offset: 0x002790AC
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x0600774B RID: 30539 RVA: 0x0027AEAF File Offset: 0x002790AF
		public override string MenuName
		{
			get
			{
				return "Share";
			}
		}

		// Token: 0x0600774C RID: 30540 RVA: 0x0027AEB6 File Offset: 0x002790B6
		public override void CloseInternal()
		{
			NKCUICharacterView charview = this.m_charview;
			if (charview != null)
			{
				charview.CleanUp();
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600774D RID: 30541 RVA: 0x0027AED5 File Offset: 0x002790D5
		public override void OnBackButton()
		{
		}

		// Token: 0x0600774E RID: 30542 RVA: 0x0027AED8 File Offset: 0x002790D8
		public void Open(NKMUserData userData, NKMUnitData targetUnit, NKCPublisherModule.SNS_SHARE_TYPE eSST)
		{
			this.m_SNS_SHARE_TYPE = eSST;
			NKCUICharInfoSummary charSummary = this.m_charSummary;
			if (charSummary != null)
			{
				charSummary.SetData(targetUnit);
			}
			if (this.m_charview != null)
			{
				this.m_charview.SetCharacterIllust(targetUnit, false, true, true, 0);
				this.m_charview.SetAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE, true);
			}
			NKCUtil.SetLabelText(this.m_lbLevel, NKCStringTable.GetString("SI_DP_LEVEL_ONE_PARAM", false), new object[]
			{
				userData.m_UserLevel
			});
			NKCUtil.SetLabelText(this.m_lbUserName, userData.m_UserNickName);
			NKCUtil.SetLabelText(this.m_lbUserUID, NKCUtilString.GetFriendCode(userData.m_FriendCode));
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetUnit);
			NKCUtil.SetGameobjectActive(this.m_objBGN, unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_N);
			NKCUtil.SetGameobjectActive(this.m_objBGR, unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_R);
			NKCUtil.SetGameobjectActive(this.m_objBGSR, unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR);
			NKCUtil.SetGameobjectActive(this.m_objBGSSR, unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR);
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.SNS_SHARE_QR) && NKCPublisherModule.Marketing.SnsQRImageEnabled())
			{
				NKCUtil.SetGameobjectActive(this.m_qrImage, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_qrImage, false);
			}
			base.UIOpened(true);
			base.StartCoroutine(this.Process());
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x0600774F RID: 30543 RVA: 0x0027B014 File Offset: 0x00279214
		private string CapturePath
		{
			get
			{
				return Path.Combine(Application.persistentDataPath, "ScreenCapture.png");
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06007750 RID: 30544 RVA: 0x0027B025 File Offset: 0x00279225
		private string ThumbnailPath
		{
			get
			{
				return Path.Combine(Application.persistentDataPath, "Thumbnail.png");
			}
		}

		// Token: 0x06007751 RID: 30545 RVA: 0x0027B036 File Offset: 0x00279236
		private IEnumerator Process()
		{
			yield return null;
			NKCScenManager.GetScenManager().GetEffectManager().DeleteAllEffect();
			NKCUIManager.CloseAllOverlay();
			yield return new WaitForEndOfFrame();
			if (!NKCScreenCaptureUtility.CaptureScreenWithThumbnail(this.CapturePath, this.ThumbnailPath))
			{
				this.OnShareFinished(NKC_PUBLISHER_RESULT_CODE.NPRC_MARKETING_SNS_SHARE_FAIL, null);
				yield break;
			}
			yield return null;
			NKCPublisherModule.Marketing.TrySnsShare(this.m_SNS_SHARE_TYPE, this.CapturePath, this.ThumbnailPath, new NKCPublisherModule.OnComplete(this.OnShareFinished));
			yield break;
		}

		// Token: 0x06007752 RID: 30546 RVA: 0x0027B045 File Offset: 0x00279245
		private void OnShareFinished(NKC_PUBLISHER_RESULT_CODE result, string additionalError)
		{
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.CHN))
			{
				base.Close();
				return;
			}
			if (!NKCPublisherModule.CheckError(result, additionalError, true, new NKCPopupOKCancel.OnButton(base.Close), false))
			{
				return;
			}
			base.Close();
		}

		// Token: 0x040063CE RID: 25550
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_result";

		// Token: 0x040063CF RID: 25551
		private const string UI_ASSET_NAME = "NKM_UI_RESULT_GET_SHARE";

		// Token: 0x040063D0 RID: 25552
		private static NKCPopupSnsShare m_Instance;

		// Token: 0x040063D1 RID: 25553
		public NKCUICharacterView m_charview;

		// Token: 0x040063D2 RID: 25554
		public NKCUICharInfoSummary m_charSummary;

		// Token: 0x040063D3 RID: 25555
		public Text m_lbLevel;

		// Token: 0x040063D4 RID: 25556
		public Text m_lbUserName;

		// Token: 0x040063D5 RID: 25557
		public Text m_lbUserUID;

		// Token: 0x040063D6 RID: 25558
		public GameObject m_objBGN;

		// Token: 0x040063D7 RID: 25559
		public GameObject m_objBGR;

		// Token: 0x040063D8 RID: 25560
		public GameObject m_objBGSR;

		// Token: 0x040063D9 RID: 25561
		public GameObject m_objBGSSR;

		// Token: 0x040063DA RID: 25562
		public GameObject m_qrImage;

		// Token: 0x040063DB RID: 25563
		private NKCPublisherModule.SNS_SHARE_TYPE m_SNS_SHARE_TYPE = NKCPublisherModule.SNS_SHARE_TYPE.SST_NONE;

		// Token: 0x040063DC RID: 25564
		private const string CAPTURE_FILE_NAME = "ScreenCapture.png";

		// Token: 0x040063DD RID: 25565
		private const string THUMBNAIL_FILE_NAME = "Thumbnail.png";
	}
}
