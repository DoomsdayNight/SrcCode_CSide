using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007A1 RID: 1953
	public class NKCDeckViewShipListSlot : MonoBehaviour
	{
		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06004CC5 RID: 19653 RVA: 0x00170026 File Offset: 0x0016E226
		public NKMUnitData ShipUnitData
		{
			get
			{
				return this.m_ShipUnitData;
			}
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0017002E File Offset: 0x0016E22E
		public NKCUIComButton GetNKCUIComButton()
		{
			return this.m_cbtnSlot;
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x00170038 File Offset: 0x0016E238
		public static NKCDeckViewShipListSlot GetNewInstance(int index, Transform parent, NKCDeckViewShipListSlot.OnShipChange delegateOnShipChange)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_DECK_VIEW_SHIP_LIST_SLOT", "NKM_UI_DECK_VIEW_SHIP_LIST_SLOT", false, null);
			if (nkcassetInstanceData == null || nkcassetInstanceData.m_Instant == null)
			{
				Debug.LogError("NKCDeckViewShipListSlot Prefab null!");
				return null;
			}
			NKCDeckViewShipListSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCDeckViewShipListSlot>();
			if (component == null)
			{
				Debug.LogError("NKCDeckViewShipListSlot Prefab null!");
				return null;
			}
			component.m_Index = index;
			component.transform.SetParent(parent);
			component.m_RectTransform = component.GetComponent<RectTransform>();
			component.m_RectTransform.anchoredPosition = new Vector2(0f, (float)(-(float)index * 230));
			component.m_RectTransform.localScale = Vector3.one;
			component.m_cbtnChange.PointerClick.RemoveAllListeners();
			component.m_cbtnChange.PointerClick.AddListener(new UnityAction(component.ShipChangeButtonClicked));
			component.dOnShipChange = delegateOnShipChange;
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x00170122 File Offset: 0x0016E322
		private void ShipChangeButtonClicked()
		{
			if (this.dOnShipChange != null)
			{
				this.dOnShipChange(this.m_ShipUnitData.m_UnitUID);
			}
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x00170142 File Offset: 0x0016E342
		public bool Update()
		{
			if (!base.gameObject.activeSelf)
			{
				return false;
			}
			this.m_PosX.Update(Time.deltaTime);
			this.UpdatePos();
			return true;
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x0017016C File Offset: 0x0016E36C
		public void UpdatePos()
		{
			Vector2 anchoredPosition = this.m_RectTransform.anchoredPosition;
			anchoredPosition.x = this.m_PosX.GetNowValue();
			this.m_RectTransform.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x001701A4 File Offset: 0x0016E3A4
		public void SetData(NKMUnitData cShipUnitData, NKM_DECK_TYPE eCurrentDeckType, NKMDeckIndex deckIndex, bool bAnimate)
		{
			this.m_ShipUnitData = cShipUnitData;
			if (this.m_ShipUnitData != null)
			{
				if (!base.gameObject.activeSelf)
				{
					base.gameObject.SetActive(true);
				}
				this.m_lbLevel.text = this.m_ShipUnitData.m_UnitLevel.ToString();
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_ShipUnitData.m_UnitID);
				if (unitTempletBase != null)
				{
					NKCAssetResourceData unitResource = NKCResourceUtility.GetUnitResource(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
					if (unitResource != null)
					{
						this.m_imgShip.sprite = unitResource.GetAsset<Sprite>();
					}
					this.m_lbName.text = unitTempletBase.GetUnitName();
				}
				else
				{
					this.m_lbName.text = "";
				}
				if (deckIndex.m_eDeckType == eCurrentDeckType)
				{
					this.m_objRootHasDeck.SetActive(true);
					this.m_objRootNoDeck.SetActive(false);
					this.m_lbDeckNumber.text = NKCUtilString.GetDeckNumberString(deckIndex);
				}
				else
				{
					this.m_objRootHasDeck.SetActive(false);
					this.m_objRootNoDeck.SetActive(true);
				}
				if (bAnimate)
				{
					this.FadeInMove();
					return;
				}
			}
			else if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x001702BC File Offset: 0x0016E4BC
		public bool FadeInMove()
		{
			if (!base.gameObject.activeSelf)
			{
				return false;
			}
			this.m_PosX.SetNowValue(900f);
			this.m_PosX.SetTracking(0f, 0.2f * (float)(this.m_Index + 1), TRACKING_DATA_TYPE.TDT_SLOWER);
			this.UpdatePos();
			return true;
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x0017030F File Offset: 0x0016E50F
		public void Select()
		{
			this.m_Animator.Play("START_ON", -1, 0f);
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x00170327 File Offset: 0x0016E527
		public void DeSelect()
		{
			this.m_Animator.Play("START_OFF", -1, 0f);
		}

		// Token: 0x04003C80 RID: 15488
		private int m_Index;

		// Token: 0x04003C81 RID: 15489
		private NKMUnitData m_ShipUnitData;

		// Token: 0x04003C82 RID: 15490
		private RectTransform m_RectTransform;

		// Token: 0x04003C83 RID: 15491
		public Animator m_Animator;

		// Token: 0x04003C84 RID: 15492
		public NKCUIComButton m_cbtnSlot;

		// Token: 0x04003C85 RID: 15493
		public Image m_imgShip;

		// Token: 0x04003C86 RID: 15494
		public Text m_lbLevel;

		// Token: 0x04003C87 RID: 15495
		public Text m_lbName;

		// Token: 0x04003C88 RID: 15496
		public GameObject m_objRootHasDeck;

		// Token: 0x04003C89 RID: 15497
		public GameObject m_objRootNoDeck;

		// Token: 0x04003C8A RID: 15498
		public Text m_lbDeckNumber;

		// Token: 0x04003C8B RID: 15499
		public NKCUIComButton m_cbtnDetail;

		// Token: 0x04003C8C RID: 15500
		public NKCUIComButton m_cbtnChange;

		// Token: 0x04003C8D RID: 15501
		private NKMTrackingFloat m_PosX = new NKMTrackingFloat();

		// Token: 0x04003C8E RID: 15502
		private NKCDeckViewShipListSlot.OnShipChange dOnShipChange;

		// Token: 0x02001461 RID: 5217
		// (Invoke) Token: 0x0600A88E RID: 43150
		public delegate void OnShipChange(long shipUID);
	}
}
