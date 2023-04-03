using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007A9 RID: 1961
	public class NKCDiveGameSquadSlot : MonoBehaviour
	{
		// Token: 0x06004D4A RID: 19786 RVA: 0x00174474 File Offset: 0x00172674
		public int GetDeckIndex()
		{
			if (this.m_NKMDiveSquad != null)
			{
				return this.m_NKMDiveSquad.DeckIndex;
			}
			return -1;
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x0017448C File Offset: 0x0017268C
		public static NKCDiveGameSquadSlot GetNewInstance(Transform parent, NKCDiveGameSquadSlot.OnClickSquadSlot dOnClickSquadSlot = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT", false, null);
			NKCDiveGameSquadSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCDiveGameSquadSlot>();
			if (component == null)
			{
				Debug.LogError("NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			component.m_dOnClickSquadSlot = dOnClickSquadSlot;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT.PointerClick.RemoveAllListeners();
			component.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT.PointerClick.AddListener(new UnityAction(component.OnClick));
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x00174547 File Offset: 0x00172747
		public void SetSelected(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_SELECT, bSet);
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x00174555 File Offset: 0x00172755
		private void OnDestroy()
		{
			if (this.m_InstanceData != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			}
			this.m_InstanceData = null;
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x00174571 File Offset: 0x00172771
		private void OnClick()
		{
			if (this.m_dOnClickSquadSlot != null)
			{
				this.m_dOnClickSquadSlot(this.m_NKMDiveSquad);
			}
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0017458C File Offset: 0x0017278C
		private float GetProperRatioValue(float fRatio)
		{
			if (fRatio < 0f)
			{
				fRatio = 0f;
			}
			if (fRatio > 1f)
			{
				fRatio = 1f;
			}
			return fRatio;
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x001745B0 File Offset: 0x001727B0
		private void SetSuuplyCountUI(int count)
		{
			if (count <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_BATTLEPOINT_1, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_BATTLEPOINT_2, false);
			}
			if (count == 1)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_BATTLEPOINT_1, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_BATTLEPOINT_2, false);
			}
			if (count >= 2)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_BATTLEPOINT_1, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_BATTLEPOINT_2, true);
			}
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x00174614 File Offset: 0x00172814
		public void SetUI(NKMDiveSquad cNKMDiveSquad)
		{
			if (cNKMDiveSquad == null)
			{
				return;
			}
			this.m_NKMDiveSquad = cNKMDiveSquad;
			this.SetSuuplyCountUI(cNKMDiveSquad.Supply);
			float num = 0f;
			if (cNKMDiveSquad.MaxHp > 0f)
			{
				num = cNKMDiveSquad.CurHp / cNKMDiveSquad.MaxHp;
			}
			num = this.GetProperRatioValue(num);
			if (num > 0.6f)
			{
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_1.transform.localScale = new Vector3(1f, 1f, 1f);
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_2.transform.localScale = new Vector3(1f, 1f, 1f);
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_3.transform.localScale = new Vector3(this.GetProperRatioValue((num - 0.6f) / 0.4f), 1f, 1f);
			}
			else if (num > 0.3f)
			{
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_1.transform.localScale = new Vector3(1f, 1f, 1f);
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_2.transform.localScale = new Vector3(this.GetProperRatioValue((num - 0.3f) / 0.3f), 1f, 1f);
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_3.transform.localScale = new Vector3(0f, 1f, 1f);
			}
			else
			{
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_1.transform.localScale = new Vector3(this.GetProperRatioValue(num / 0.3f), 1f, 1f);
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_2.transform.localScale = new Vector3(0f, 1f, 1f);
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_3.transform.localScale = new Vector3(0f, 1f, 1f);
			}
			NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, cNKMDiveSquad.DeckIndex);
			this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_DECKNUMBER_COUNT.text = ((int)(nkmdeckIndex.m_iIndex + 1)).ToString();
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitData nkmunitData = myUserData.m_ArmyData.GetDeckLeaderUnitData(nkmdeckIndex);
			if (nkmunitData != null)
			{
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_LV_TEXT.text = nkmunitData.m_UnitLevel.ToString();
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_DISABLE, cNKMDiveSquad.CurHp <= 0f || cNKMDiveSquad.Supply <= 0);
			if (cNKMDiveSquad.CurHp <= 0f)
			{
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_DISABLE_TEXT.text = NKCUtilString.GET_STRING_DIVE_SQUAD_NO_EXIST_HP;
			}
			else if (cNKMDiveSquad.Supply <= 0)
			{
				this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_DISABLE_TEXT.text = NKCUtilString.GET_STRING_DIVE_SQUAD_NO_EXIST_SUPPLY;
			}
			NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(nkmdeckIndex);
			if (deckData != null)
			{
				nkmunitData = myUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
				if (nkmunitData != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID);
					if (unitTempletBase != null)
					{
						Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
						if (sprite == null)
						{
							NKCAssetResourceData assetResourceUnitInvenIconEmpty = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
							if (assetResourceUnitInvenIconEmpty != null)
							{
								this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_SHIP.sprite = assetResourceUnitInvenIconEmpty.GetAsset<Sprite>();
								return;
							}
							this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_SHIP.sprite = null;
							return;
						}
						else
						{
							this.m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_SHIP.sprite = sprite;
						}
					}
				}
			}
		}

		// Token: 0x04003D2A RID: 15658
		public NKCUIComStateButton m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT;

		// Token: 0x04003D2B RID: 15659
		public GameObject m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_BATTLEPOINT_1;

		// Token: 0x04003D2C RID: 15660
		public GameObject m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_BATTLEPOINT_2;

		// Token: 0x04003D2D RID: 15661
		public Image m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_1;

		// Token: 0x04003D2E RID: 15662
		public Image m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_2;

		// Token: 0x04003D2F RID: 15663
		public Image m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_HP_3;

		// Token: 0x04003D30 RID: 15664
		public Text m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_LV_TEXT;

		// Token: 0x04003D31 RID: 15665
		public Text m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_DECKNUMBER_COUNT;

		// Token: 0x04003D32 RID: 15666
		public Image m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_SHIP;

		// Token: 0x04003D33 RID: 15667
		public GameObject m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_DISABLE;

		// Token: 0x04003D34 RID: 15668
		public Text m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_DISABLE_TEXT;

		// Token: 0x04003D35 RID: 15669
		public GameObject m_NKM_UI_DIVE_PROCESS_SQUAD_LIST_SLOT_SELECT;

		// Token: 0x04003D36 RID: 15670
		private NKMDiveSquad m_NKMDiveSquad;

		// Token: 0x04003D37 RID: 15671
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04003D38 RID: 15672
		private NKCDiveGameSquadSlot.OnClickSquadSlot m_dOnClickSquadSlot;

		// Token: 0x02001466 RID: 5222
		// (Invoke) Token: 0x0600A8A2 RID: 43170
		public delegate void OnClickSquadSlot(NKMDiveSquad cNKMDiveSquad);
	}
}
