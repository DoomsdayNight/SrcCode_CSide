using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007BE RID: 1982
	public class NKCUIDiveReadySquadSlot : MonoBehaviour
	{
		// Token: 0x06004E8D RID: 20109 RVA: 0x0017AE25 File Offset: 0x00179025
		private void Awake()
		{
			this.m_NKM_UI_DIVE_INFO_SQUAD_SLOT.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_DIVE_INFO_SQUAD_SLOT.PointerClick.AddListener(new UnityAction(this.OnClicked));
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x0017AE53 File Offset: 0x00179053
		public void SetSelectedEvent(NKCUIDiveReadySquadSlot.OnSelectedDiveReadySquadSlot _OnSelectedDiveReadySquadSlot)
		{
			this.m_OnSelectedDiveReadySquadSlot = _OnSelectedDiveReadySquadSlot;
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x0017AE5C File Offset: 0x0017905C
		private void OnClicked()
		{
			if (this.m_OnSelectedDiveReadySquadSlot != null)
			{
				this.m_OnSelectedDiveReadySquadSlot();
			}
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x0017AE71 File Offset: 0x00179071
		public void SetUnSelected()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_SQUAD_SLOT_SELECTED, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_SQUAD_SLOT_UNSELECTED, true);
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x0017AE8C File Offset: 0x0017908C
		public void SetSelected(NKMDeckIndex sNKMDeckIndex)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_SQUAD_SLOT_SELECTED, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_SQUAD_SLOT_UNSELECTED, false);
			NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(sNKMDeckIndex);
			if (deckData != null)
			{
				NKMUnitData shipFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
				if (shipFromUID != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
					if (unitTempletBase != null)
					{
						this.m_NKM_UI_DIVE_INFO_SQUAD_SLOT_SHIP_IMAGE.sprite = NKCResourceUtility.GetOrLoadMinimapFaceIcon(unitTempletBase, false);
					}
				}
			}
			this.m_NKM_UI_DIVE_INFO_SQUAD_SLOT_DECKNUMBER_COUNT.text = ((int)(sNKMDeckIndex.m_iIndex + 1)).ToString();
		}

		// Token: 0x04003E34 RID: 15924
		public NKCUIComStateButton m_NKM_UI_DIVE_INFO_SQUAD_SLOT;

		// Token: 0x04003E35 RID: 15925
		public GameObject m_NKM_UI_DIVE_INFO_SQUAD_SLOT_SELECTED;

		// Token: 0x04003E36 RID: 15926
		public GameObject m_NKM_UI_DIVE_INFO_SQUAD_SLOT_UNSELECTED;

		// Token: 0x04003E37 RID: 15927
		public Image m_NKM_UI_DIVE_INFO_SQUAD_SLOT_SHIP_IMAGE;

		// Token: 0x04003E38 RID: 15928
		public Text m_NKM_UI_DIVE_INFO_SQUAD_SLOT_DECKNUMBER_COUNT;

		// Token: 0x04003E39 RID: 15929
		private NKCUIDiveReadySquadSlot.OnSelectedDiveReadySquadSlot m_OnSelectedDiveReadySquadSlot;

		// Token: 0x0200147D RID: 5245
		// (Invoke) Token: 0x0600A8F7 RID: 43255
		public delegate void OnSelectedDiveReadySquadSlot();
	}
}
