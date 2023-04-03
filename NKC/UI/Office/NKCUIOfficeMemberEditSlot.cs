using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AF2 RID: 2802
	public class NKCUIOfficeMemberEditSlot : MonoBehaviour
	{
		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06007E53 RID: 32339 RVA: 0x002A5FF5 File Offset: 0x002A41F5
		public int UnitId
		{
			get
			{
				return this.m_iUnitId;
			}
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x002A5FFD File Offset: 0x002A41FD
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSelectSlot, new UnityAction(this.OnBtnSelectSlot));
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x002A6018 File Offset: 0x002A4218
		public static NKCUIOfficeMemberEditSlot GetNewInstance(Transform parent, bool bMentoringSlot = false)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_office", "AB_UI_POPUP_OFFICE_MEMBER_EDIT_SLOT", false, null);
			NKCUIOfficeMemberEditSlot nkcuiofficeMemberEditSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIOfficeMemberEditSlot>() : null;
			if (nkcuiofficeMemberEditSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIOfficeMemberEditSlot Prefab null!");
				return null;
			}
			nkcuiofficeMemberEditSlot.m_InstanceData = nkcassetInstanceData;
			nkcuiofficeMemberEditSlot.Init();
			if (parent != null)
			{
				nkcuiofficeMemberEditSlot.transform.SetParent(parent);
			}
			nkcuiofficeMemberEditSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuiofficeMemberEditSlot.gameObject.SetActive(false);
			return nkcuiofficeMemberEditSlot;
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x002A60B2 File Offset: 0x002A42B2
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x002A60D4 File Offset: 0x002A42D4
		public void SetData(List<long> unitAssignedList, NKMUnitData unitData, int roomId, NKCUIOfficeMemberEditSlot.SelectSlot onSelectSlot)
		{
			this.m_iRoomId = roomId;
			if (unitData == null)
			{
				return;
			}
			this.m_iUnitId = unitData.m_UnitID;
			this.m_lUnitUId = unitData.m_UnitUID;
			NKCUtil.SetLabelText(this.m_lbUnitId, NKCCollectionManager.GetEmployeeNumber(unitData.m_UnitID));
			NKCUtil.SetImageFillAmount(this.m_imgRoyaltyGauge, (float)unitData.loyalty / 10000f);
			NKCUtil.SetGameobjectActive(this.m_objRoyaltyMax, unitData.loyalty >= 10000);
			NKCUtil.SetGameobjectActive(this.m_objLifeTimeContract, unitData.IsPermanentContract);
			int num = -1;
			if (unitAssignedList != null)
			{
				num = unitAssignedList.FindIndex((long e) => e == unitData.m_UnitUID);
			}
			NKCUtil.SetGameobjectActive(this.m_objSelected, num >= 0);
			if (num >= 0)
			{
				NKCUtil.SetLabelText(this.m_lbSelectedNumber, (num + 1).ToString("D2"));
			}
			NKCUtil.SetGameobjectActive(this.m_objMovedIn, unitData.OfficeRoomId > 0 && unitData.OfficeRoomId != roomId);
			int unitID = unitData.m_UnitID;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			if (unitTempletBase != null)
			{
				unitID = unitTempletBase.m_BaseUnitID;
				NKCUtil.SetLabelText(this.m_lbUnitName, NKCStringTable.GetString(unitTempletBase.Name, false));
				switch (unitTempletBase.m_NKM_UNIT_GRADE)
				{
				case NKM_UNIT_GRADE.NUG_N:
					NKCUtil.SetImageSprite(this.m_imgUnitGradeBg, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_office_sprite", "AB_UI_OFFICE_UNIT_CARD_BG_N", false), false);
					break;
				case NKM_UNIT_GRADE.NUG_R:
					NKCUtil.SetImageSprite(this.m_imgUnitGradeBg, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_office_sprite", "AB_UI_OFFICE_UNIT_CARD_BG_R", false), false);
					break;
				case NKM_UNIT_GRADE.NUG_SR:
					NKCUtil.SetImageSprite(this.m_imgUnitGradeBg, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_office_sprite", "AB_UI_OFFICE_UNIT_CARD_BG_SR", false), false);
					break;
				case NKM_UNIT_GRADE.NUG_SSR:
					NKCUtil.SetImageSprite(this.m_imgUnitGradeBg, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_office_sprite", "AB_UI_OFFICE_UNIT_CARD_BG_SSR", false), false);
					break;
				}
				NKCUtil.SetImageSprite(this.m_imgUnitFace, NKCResourceUtility.GetOrLoadMinimapFaceIcon(unitTempletBase, false), false);
				NKCUtil.SetGameobjectActive(this.m_objRoyalty, unitTempletBase.IsUnitStyleType());
			}
			if (unitData.m_SkinID > 0)
			{
				Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitID, unitData.m_SkinID);
				NKCUtil.SetImageSprite(this.m_imgSkin, sprite, false);
				NKCUtil.SetGameobjectActive(this.m_objSkin, sprite != null);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objSkin, false);
			}
			this.m_dOnSelectSlot = onSelectSlot;
		}

		// Token: 0x06007E58 RID: 32344 RVA: 0x002A635A File Offset: 0x002A455A
		private void OnBtnSelectSlot()
		{
			if (this.m_dOnSelectSlot != null)
			{
				this.m_dOnSelectSlot(this.m_lUnitUId);
			}
		}

		// Token: 0x04006B15 RID: 27413
		public Text m_lbUnitId;

		// Token: 0x04006B16 RID: 27414
		public Text m_lbUnitName;

		// Token: 0x04006B17 RID: 27415
		public Image m_imgUnitGradeBg;

		// Token: 0x04006B18 RID: 27416
		public Image m_imgUnitFace;

		// Token: 0x04006B19 RID: 27417
		public Image m_imgRoyaltyGauge;

		// Token: 0x04006B1A RID: 27418
		public GameObject m_objRoyalty;

		// Token: 0x04006B1B RID: 27419
		public GameObject m_objRoyaltyMax;

		// Token: 0x04006B1C RID: 27420
		public GameObject m_objLifeTimeContract;

		// Token: 0x04006B1D RID: 27421
		public GameObject m_objMovedIn;

		// Token: 0x04006B1E RID: 27422
		[Header("스킨 아이콘")]
		public GameObject m_objSkin;

		// Token: 0x04006B1F RID: 27423
		public Image m_imgSkin;

		// Token: 0x04006B20 RID: 27424
		[Space]
		public GameObject m_objSelected;

		// Token: 0x04006B21 RID: 27425
		public Text m_lbSelectedNumber;

		// Token: 0x04006B22 RID: 27426
		public NKCUIComStateButton m_csbtnSelectSlot;

		// Token: 0x04006B23 RID: 27427
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04006B24 RID: 27428
		private int m_iRoomId;

		// Token: 0x04006B25 RID: 27429
		private int m_iUnitId;

		// Token: 0x04006B26 RID: 27430
		private long m_lUnitUId;

		// Token: 0x04006B27 RID: 27431
		private NKCUIOfficeMemberEditSlot.SelectSlot m_dOnSelectSlot;

		// Token: 0x02001871 RID: 6257
		// (Invoke) Token: 0x0600B5EB RID: 46571
		public delegate void SelectSlot(long unitUId);
	}
}
