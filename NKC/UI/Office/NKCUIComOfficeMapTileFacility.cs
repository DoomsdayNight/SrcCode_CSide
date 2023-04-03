using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AED RID: 2797
	public class NKCUIComOfficeMapTileFacility : MonoBehaviour
	{
		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06007D87 RID: 32135 RVA: 0x002A1210 File Offset: 0x0029F410
		public NKMOfficeRoomTemplet.RoomType RoomType
		{
			get
			{
				return this.m_eFacilityType;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06007D88 RID: 32136 RVA: 0x002A1218 File Offset: 0x0029F418
		public bool IsRedDotOn
		{
			get
			{
				return this.m_objRedDot.activeSelf;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06007D89 RID: 32137 RVA: 0x002A1225 File Offset: 0x0029F425
		public RectTransform RectTransformTileShape
		{
			get
			{
				return this.m_rtBgShape;
			}
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x002A1230 File Offset: 0x0029F430
		public void Init()
		{
			this.m_eContentsType = ContentsType.None;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnTileButton, new UnityAction(this.OnBtnTile));
			GameObject objBgRoot = this.m_objBgRoot;
			RectTransform rectTransform = (objBgRoot != null) ? objBgRoot.GetComponent<RectTransform>() : null;
			GameObject objLockRoot = this.m_objLockRoot;
			RectTransform rectTransform2 = (objLockRoot != null) ? objLockRoot.GetComponent<RectTransform>() : null;
			if (rectTransform != null && rectTransform2 != null)
			{
				rectTransform2.sizeDelta = rectTransform.sizeDelta;
			}
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x002A12A0 File Offset: 0x0029F4A0
		public void UpdateRoomState(Dictionary<string, NKCUIOfficeMinimapFacility.FacilityInfo> dicFacilityInfo, NKCUIOfficeMinimapFacility.FacilityInfo lockInfo)
		{
			this.m_eFacilityType = NKMOfficeRoomTemplet.RoomType.Dorm;
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.m_iRoomId);
			if (nkmofficeRoomTemplet != null)
			{
				this.m_eFacilityType = nkmofficeRoomTemplet.Type;
				this.m_eContentsType = NKCUIOfficeMapFront.GetFacilityContentType(this.m_eFacilityType);
			}
			this.m_bUnlocked = false;
			if (nkmofficeRoomTemplet != null)
			{
				this.m_bUnlocked = NKCContentManager.IsContentsUnlocked(this.m_eContentsType, 0, 0);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnTileButton, new UnityAction(this.OnBtnTile));
			NKCUtil.SetGameobjectActive(this.m_objOn, this.m_bUnlocked);
			NKCUtil.SetGameobjectActive(this.m_objLock, !this.m_bUnlocked);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			NKCUtil.SetGameobjectActive(this.m_objIconRoot, nkmofficeRoomTemplet != null);
			NKCUtil.SetGameobjectActive(this.m_objLockRoot, !this.m_bUnlocked);
			if (this.m_bUnlocked)
			{
				if (dicFacilityInfo.ContainsKey(this.roomInfoKey))
				{
					Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_office_sprite", dicFacilityInfo[this.roomInfoKey].m_strIcon, false);
					NKCUtil.SetGameobjectActive(this.m_objIconRoot, orLoadAssetResource != null);
					NKCUtil.SetImageSprite(this.m_imgIcon, orLoadAssetResource, false);
					this.ApplyTileColor(dicFacilityInfo[this.roomInfoKey]);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objIconRoot, false);
				}
			}
			else
			{
				this.ApplyTileColor(lockInfo);
			}
			this.UpdateCraftingState();
			this.UpdateRedDot();
		}

		// Token: 0x06007D8C RID: 32140 RVA: 0x002A13F0 File Offset: 0x0029F5F0
		public void UpdateRedDot()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null || !this.m_bUnlocked)
			{
				return;
			}
			switch (this.m_eFacilityType)
			{
			case NKMOfficeRoomTemplet.RoomType.Forge:
				NKCUtil.SetGameobjectActive(this.m_objRedDot, NKCAlarmManager.CheckFactoryNotify(nkmuserData));
				return;
			case NKMOfficeRoomTemplet.RoomType.Hangar:
				NKCUtil.SetGameobjectActive(this.m_objRedDot, NKCAlarmManager.CheckHangarNotify(nkmuserData));
				return;
			case NKMOfficeRoomTemplet.RoomType.CEO:
				NKCUtil.SetGameobjectActive(this.m_objRedDot, NKCAlarmManager.CheckScoutNotify(nkmuserData));
				return;
			default:
				return;
			}
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x002A1460 File Offset: 0x0029F660
		private void ApplyTileColor(NKCUIOfficeMinimapFacility.FacilityInfo facilityInfo)
		{
			if (!string.IsNullOrEmpty(facilityInfo.m_bgColor))
			{
				Color color;
				ColorUtility.TryParseHtmlString(facilityInfo.m_bgColor, out color);
				NKCUtil.SetImageColor(this.m_imgBg, color);
			}
			if (!string.IsNullOrEmpty(facilityInfo.m_glowColor))
			{
				Color color;
				ColorUtility.TryParseHtmlString(facilityInfo.m_glowColor, out color);
				NKCUtil.SetImageColor(this.m_imgGlow, color);
			}
			if (!string.IsNullOrEmpty(facilityInfo.m_strokeColor))
			{
				Color color;
				ColorUtility.TryParseHtmlString(facilityInfo.m_strokeColor, out color);
				NKCUtil.SetImageColor(this.m_imgStroke, color);
				NKCUtil.SetImageColor(this.m_imgFacilityColor, color);
			}
			if (!string.IsNullOrEmpty(facilityInfo.m_titleColor))
			{
				Color color;
				ColorUtility.TryParseHtmlString(facilityInfo.m_titleColor, out color);
				NKCUtil.SetLabelTextColor(this.m_lbTitle, color);
			}
			if (!string.IsNullOrEmpty(facilityInfo.m_npcColor))
			{
				Color color;
				ColorUtility.TryParseHtmlString(facilityInfo.m_npcColor, out color);
				NKCUtil.SetImageColor(this.m_imgNpc1, color);
				NKCUtil.SetImageColor(this.m_imgNpc2, color);
			}
		}

		// Token: 0x06007D8E RID: 32142 RVA: 0x002A1548 File Offset: 0x0029F748
		private void UpdateCraftingState()
		{
			if (this.m_eFacilityType != NKMOfficeRoomTemplet.RoomType.Forge || !this.m_bUnlocked)
			{
				NKCUtil.SetGameobjectActive(this.m_objWorkshopRoot, false);
				return;
			}
			NKMCraftData craftData = NKCScenManager.GetScenManager().GetMyUserData().m_CraftData;
			NKM_CRAFT_SLOT_STATE nkm_CRAFT_SLOT_STATE = NKM_CRAFT_SLOT_STATE.NECSS_EMPTY;
			int max_CRAFT_SLOT_DATA = NKMCraftData.MAX_CRAFT_SLOT_DATA;
			for (int i = 1; i <= max_CRAFT_SLOT_DATA; i++)
			{
				NKMCraftSlotData slotData = craftData.GetSlotData((byte)i);
				if (slotData != null)
				{
					if (slotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW)
					{
						nkm_CRAFT_SLOT_STATE = NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW;
					}
					else if (slotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED && nkm_CRAFT_SLOT_STATE != NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW)
					{
						nkm_CRAFT_SLOT_STATE = NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED;
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objWorkshopRoot, nkm_CRAFT_SLOT_STATE > NKM_CRAFT_SLOT_STATE.NECSS_EMPTY);
			NKCUtil.SetGameobjectActive(this.m_objProgress, nkm_CRAFT_SLOT_STATE == NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW);
			NKCUtil.SetGameobjectActive(this.m_objComplete, nkm_CRAFT_SLOT_STATE == NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED);
		}

		// Token: 0x06007D8F RID: 32143 RVA: 0x002A160C File Offset: 0x0029F80C
		private void OnBtnTile()
		{
			if (!this.m_bUnlocked)
			{
				if (this.m_eContentsType == ContentsType.None)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_COMING_SOON_SYSTEM, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCContentManager.ShowLockedMessagePopup(this.m_eContentsType, 0);
				return;
			}
			else
			{
				if (NKMOfficeRoomTemplet.Find(this.m_iRoomId) == null)
				{
					return;
				}
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance == null)
				{
					return;
				}
				instance.Open(this.m_iRoomId);
				return;
			}
		}

		// Token: 0x04006A76 RID: 27254
		public int m_iRoomId;

		// Token: 0x04006A77 RID: 27255
		public string roomInfoKey;

		// Token: 0x04006A78 RID: 27256
		[Space]
		public Text m_lbTitle;

		// Token: 0x04006A79 RID: 27257
		public Image m_imgBg;

		// Token: 0x04006A7A RID: 27258
		public Image m_imgGlow;

		// Token: 0x04006A7B RID: 27259
		public Image m_imgStroke;

		// Token: 0x04006A7C RID: 27260
		public Image m_imgFacilityColor;

		// Token: 0x04006A7D RID: 27261
		public Image m_imgIcon;

		// Token: 0x04006A7E RID: 27262
		public Image m_imgNpc1;

		// Token: 0x04006A7F RID: 27263
		public Image m_imgNpc2;

		// Token: 0x04006A80 RID: 27264
		[Header("시설 아이콘 Deco")]
		public GameObject m_objIconRoot;

		// Token: 0x04006A81 RID: 27265
		public GameObject m_objLock;

		// Token: 0x04006A82 RID: 27266
		public GameObject m_objOn;

		// Token: 0x04006A83 RID: 27267
		[Space]
		public GameObject m_objBgRoot;

		// Token: 0x04006A84 RID: 27268
		public GameObject m_objLockRoot;

		// Token: 0x04006A85 RID: 27269
		public GameObject m_objRedDot;

		// Token: 0x04006A86 RID: 27270
		public RectTransform m_rtBgShape;

		// Token: 0x04006A87 RID: 27271
		public NKCUIComStateButton m_csbtnTileButton;

		// Token: 0x04006A88 RID: 27272
		[Header("공방 제작 상태")]
		public GameObject m_objWorkshopRoot;

		// Token: 0x04006A89 RID: 27273
		public GameObject m_objProgress;

		// Token: 0x04006A8A RID: 27274
		public GameObject m_objComplete;

		// Token: 0x04006A8B RID: 27275
		private const string m_strSpriteBundleName = "ab_ui_office_sprite";

		// Token: 0x04006A8C RID: 27276
		private NKMOfficeRoomTemplet.RoomType m_eFacilityType;

		// Token: 0x04006A8D RID: 27277
		private ContentsType m_eContentsType;

		// Token: 0x04006A8E RID: 27278
		private bool m_bUnlocked;

		// Token: 0x02001861 RID: 6241
		public enum FacilityType
		{
			// Token: 0x0400A8B9 RID: 43193
			None,
			// Token: 0x0400A8BA RID: 43194
			Lab,
			// Token: 0x0400A8BB RID: 43195
			Factory,
			// Token: 0x0400A8BC RID: 43196
			Hangar,
			// Token: 0x0400A8BD RID: 43197
			Boss,
			// Token: 0x0400A8BE RID: 43198
			TeraBrain
		}
	}
}
