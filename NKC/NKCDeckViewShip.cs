using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007A0 RID: 1952
	public class NKCDeckViewShip : MonoBehaviour
	{
		// Token: 0x06004CB6 RID: 19638 RVA: 0x0016FC06 File Offset: 0x0016DE06
		public void Open(NKMUnitData shipUnitData, bool bEnableShowBan = false)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.SetShipSlotData(shipUnitData, bEnableShowBan);
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x0016FC29 File Offset: 0x0016DE29
		public NKMUnitData GetUnitData()
		{
			return this.m_ShipUnitData;
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x0016FC31 File Offset: 0x0016DE31
		public void Init(NKCDeckViewShip.OnShipClicked onShipClicked)
		{
			this.dOnShipClicked = onShipClicked;
			this.m_cbtnShip.PointerClick.RemoveAllListeners();
			this.m_cbtnShip.PointerClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x0016FC66 File Offset: 0x0016DE66
		private void OnClick()
		{
			if (this.dOnShipClicked != null)
			{
				this.dOnShipClicked();
			}
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0016FC7B File Offset: 0x0016DE7B
		public void Close()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0016FC98 File Offset: 0x0016DE98
		public void SetShipSlotData(NKMUnitData shipUnitData, bool bEnableShowBan = false)
		{
			this.m_ShipUnitData = shipUnitData;
			if (this.m_ShipUnitData != null)
			{
				this.m_ShipUnitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_ShipUnitData.m_UnitID);
				if (this.m_ShipUnitTempletBase != null)
				{
					Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, this.m_ShipUnitTempletBase);
					if (sprite != null)
					{
						this.m_imgShip.sprite = sprite;
						if (bEnableShowBan && NKCBanManager.IsBanShip(this.m_ShipUnitTempletBase.m_ShipGroupID, NKCBanManager.BAN_DATA_TYPE.FINAL))
						{
							NKCUtil.SetGameobjectActive(this.m_objBan, true);
							int shipBanLevel = NKCBanManager.GetShipBanLevel(this.m_ShipUnitTempletBase.m_ShipGroupID, NKCBanManager.BAN_DATA_TYPE.FINAL);
							NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, shipBanLevel));
							int nerfPercentByShipBanLevel = NKMUnitStatManager.GetNerfPercentByShipBanLevel(shipBanLevel);
							NKCUtil.SetLabelText(this.m_lbBanApplyDesc, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_APPLY_DESC_ONE_PARAM, nerfPercentByShipBanLevel));
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_objBan, false);
						}
					}
					if (this.m_UIShipInfo != null)
					{
						this.m_UIShipInfo.SetShipData(shipUnitData, this.m_ShipUnitTempletBase, NKMDeckIndex.None, true);
					}
					if (this.m_UIShipInfo_Small != null)
					{
						this.m_UIShipInfo_Small.SetShipData(shipUnitData, this.m_ShipUnitTempletBase, NKMDeckIndex.None, true);
					}
					NKCUtil.SetGameobjectActive(this.m_objSeized, this.m_ShipUnitData.IsSeized);
					return;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			this.m_imgShip.sprite = this.m_spriteNoShipImage;
			NKCUtil.SetGameobjectActive(this.m_objSeized, false);
			if (this.m_UIShipInfo != null)
			{
				this.m_UIShipInfo.SetShipData(null, null, NKMDeckIndex.None, false);
			}
			if (this.m_UIShipInfo_Small != null)
			{
				this.m_UIShipInfo_Small.SetShipData(null, null, NKMDeckIndex.None, false);
			}
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0016FE4A File Offset: 0x0016E04A
		public void UpdateShipSlotData(NKMUnitData shipUnitData, bool bEnableShowBan = false)
		{
			if (this.m_ShipUnitData.m_UnitUID != shipUnitData.m_UnitUID)
			{
				return;
			}
			this.SetShipSlotData(shipUnitData, bEnableShowBan);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0016FE68 File Offset: 0x0016E068
		public void SetSelectEffect(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objEmptySelected, value);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0016FE78 File Offset: 0x0016E078
		public void Enable(bool bInfoActive = true)
		{
			this.m_cgShipInfo.DOKill(false);
			this.m_cgShipInfoSmall.DOKill(false);
			this.m_cgShipInfo.DOFade(0f, 0.4f).SetEase(Ease.OutCubic).OnComplete(delegate
			{
				NKCUtil.SetGameobjectActive(this.m_cgShipInfo.gameObject, false);
			});
			NKCUtil.SetGameobjectActive(this.m_cgShipInfoSmall.gameObject, bInfoActive);
			this.m_cgShipInfoSmall.DOFade(1f, 0.4f).SetEase(Ease.OutCubic);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0016FEFC File Offset: 0x0016E0FC
		public void Disable()
		{
			this.m_cgShipInfo.DOKill(false);
			this.m_cgShipInfoSmall.DOKill(false);
			NKCUtil.SetGameobjectActive(this.m_cgShipInfo.gameObject, true);
			this.m_cgShipInfo.DOFade(1f, 0.4f).SetEase(Ease.OutCubic);
			this.m_cgShipInfoSmall.DOFade(0f, 0.4f).SetEase(Ease.OutCubic).OnComplete(delegate
			{
				NKCUtil.SetGameobjectActive(this.m_cgShipInfoSmall.gameObject, false);
			});
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0016FF80 File Offset: 0x0016E180
		private Sprite GetSpriteMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string stringMoveType = this.GetStringMoveType(type);
			if (string.IsNullOrEmpty(stringMoveType))
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_DECK_VIEW_SPRITE", stringMoveType, false);
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0016FFAC File Offset: 0x0016E1AC
		private string GetStringMoveType(NKM_UNIT_STYLE_TYPE type)
		{
			string result = string.Empty;
			switch (type)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				result = "NKM_DECK_VIEW_SHIP_MOVETYPE_1";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				result = "NKM_DECK_VIEW_SHIP_MOVETYPE_4";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				result = "NKM_DECK_VIEW_SHIP_MOVETYPE_2";
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				result = "NKM_DECK_VIEW_SHIP_MOVETYPE_3";
				break;
			}
			return result;
		}

		// Token: 0x04003C71 RID: 15473
		private NKMUnitData m_ShipUnitData;

		// Token: 0x04003C72 RID: 15474
		private NKMUnitTempletBase m_ShipUnitTempletBase;

		// Token: 0x04003C73 RID: 15475
		public Image m_imgShip;

		// Token: 0x04003C74 RID: 15476
		public NKCUIComButton m_cbtnShip;

		// Token: 0x04003C75 RID: 15477
		public CanvasGroup m_cgShipInfo;

		// Token: 0x04003C76 RID: 15478
		public NKCUIShipInfoSummary m_UIShipInfo;

		// Token: 0x04003C77 RID: 15479
		public CanvasGroup m_cgShipInfoSmall;

		// Token: 0x04003C78 RID: 15480
		public NKCUIShipInfoSummary m_UIShipInfo_Small;

		// Token: 0x04003C79 RID: 15481
		public GameObject m_objEmptySelected;

		// Token: 0x04003C7A RID: 15482
		public GameObject m_objBan;

		// Token: 0x04003C7B RID: 15483
		public Text m_lbBanLevel;

		// Token: 0x04003C7C RID: 15484
		public Text m_lbBanApplyDesc;

		// Token: 0x04003C7D RID: 15485
		public GameObject m_objSeized;

		// Token: 0x04003C7E RID: 15486
		[Header("디폴트 이미지")]
		public Sprite m_spriteNoShipImage;

		// Token: 0x04003C7F RID: 15487
		private NKCDeckViewShip.OnShipClicked dOnShipClicked;

		// Token: 0x02001460 RID: 5216
		// (Invoke) Token: 0x0600A88A RID: 43146
		public delegate void OnShipClicked();
	}
}
