using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.Office.Forge
{
	// Token: 0x0200083F RID: 2111
	public class NKCOfficeFacilityForge : NKCOfficeFacility
	{
		// Token: 0x06005425 RID: 21541 RVA: 0x0019AD98 File Offset: 0x00198F98
		public override void Init()
		{
			base.Init();
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCOfficeFunitureForgeSlot nkcofficeFunitureForgeSlot = this.m_lstSlot[i];
				if (!(nkcofficeFunitureForgeSlot == null))
				{
					nkcofficeFunitureForgeSlot.SetIndex(i + 1);
					NKMCraftData craftData = NKCScenManager.GetScenManager().GetMyUserData().m_CraftData;
					NKMCraftSlotData nkmcraftSlotData = (craftData != null) ? craftData.GetSlotData((byte)(i + 1)) : null;
					if (nkmcraftSlotData != null && nkmcraftSlotData.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED)
					{
						nkcofficeFunitureForgeSlot.UpdateData(true);
					}
					else
					{
						nkcofficeFunitureForgeSlot.UpdateData(false);
					}
				}
			}
			if (null != this.m_fnEquipEnhance)
			{
				NKCOfficeFacilityFuniture fnEquipEnhance = this.m_fnEquipEnhance;
				fnEquipEnhance.dOnClickFuniture = (NKCOfficeFuniture.OnClickFuniture)Delegate.Combine(fnEquipEnhance.dOnClickFuniture, new NKCOfficeFuniture.OnClickFuniture(this.OnClickEquipEnhance));
				this.m_fnEquipEnhance.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0));
			}
			if (this.m_fnEquipUpgrade != null)
			{
				if (NKMOpenTagManager.IsOpened("EQUIP_UPGRADE"))
				{
					bool flag = NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0) && NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_UPGRADE, 0, 0);
					NKCOfficeFacilityFuniture fnEquipUpgrade = this.m_fnEquipUpgrade;
					fnEquipUpgrade.dOnClickFuniture = (NKCOfficeFuniture.OnClickFuniture)Delegate.Combine(fnEquipUpgrade.dOnClickFuniture, new NKCOfficeFuniture.OnClickFuniture(this.OnClickEquipUpgrade));
					this.m_fnEquipUpgrade.SetLock(!flag);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_fnEquipUpgrade, false);
			}
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x0019AEEB File Offset: 0x001990EB
		private void OnClickEquipEnhance(int id, long uid)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_ENCHANT, 0);
				return;
			}
			NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT, 0L, null);
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x0019AF25 File Offset: 0x00199125
		private void OnClickEquipUpgrade(int id, long uid)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_UPGRADE, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_UPGRADE, 0);
				return;
			}
			NKCUIForgeUpgrade.Instance.Open();
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0019AF5B File Offset: 0x0019915B
		private NKCOfficeFunitureForgeSlot GetSlot(int forgeSlotIndex)
		{
			if (forgeSlotIndex < 1)
			{
				return null;
			}
			if (forgeSlotIndex > this.m_lstSlot.Count)
			{
				return null;
			}
			return this.m_lstSlot[forgeSlotIndex - 1];
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0019AF84 File Offset: 0x00199184
		public void UpdateSlot(int index)
		{
			NKCOfficeFunitureForgeSlot slot = this.GetSlot(index);
			if (slot != null)
			{
				slot.UpdateData(true);
			}
		}

		// Token: 0x0400432C RID: 17196
		[Header("공방 관련 데이터")]
		public List<NKCOfficeFunitureForgeSlot> m_lstSlot;

		// Token: 0x0400432D RID: 17197
		[Header("공방 정보")]
		public NKCOfficeFacilityFuniture m_fnEquipEnhance;

		// Token: 0x0400432E RID: 17198
		public NKCOfficeFacilityFuniture m_fnEquipUpgrade;
	}
}
