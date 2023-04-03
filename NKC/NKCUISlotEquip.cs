using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009EB RID: 2539
	public class NKCUISlotEquip : NKCUISlot
	{
		// Token: 0x06006DAE RID: 28078 RVA: 0x0023F9A4 File Offset: 0x0023DBA4
		public static NKCUISlotEquip GetNewInstance(Transform parent, NKCUISlotEquip.OnSelectedEquipSlot selectedSlot = null)
		{
			NKCUISlotEquip component = NKCAssetResourceManager.OpenInstance<GameObject>("AB_INVEN_ICON", "AB_ICON_SLOT_EQUIP", false, null).m_Instant.GetComponent<NKCUISlotEquip>();
			if (component == null)
			{
				Debug.LogError("NKCUISlotEquip Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06006DAF RID: 28079 RVA: 0x0023FA04 File Offset: 0x0023DC04
		public static NKCUISlotEquip GetNewInstanceForEmpty(Transform parent, NKCUISlotEquip.OnSelectedEquipSlot selectedSlot = null)
		{
			NKCUISlotEquip component = NKCAssetResourceManager.OpenInstance<GameObject>("AB_INVEN_ICON", "AB_ICON_SLOT_EQUIP", false, null).m_Instant.GetComponent<NKCUISlotEquip>();
			if (component == null)
			{
				Debug.LogError("NKCUISlotEquip Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06006DB0 RID: 28080 RVA: 0x0023FA64 File Offset: 0x0023DC64
		public void SetData(NKMEquipItemData cNKMEquipItemData, NKCUISlotEquip.OnSelectedEquipSlot selectedSlot = null, bool lockMaxItem = false, bool bSkipEquipBox = false, bool bShowFierceInfo = false, bool bPresetContained = false)
		{
			if (cNKMEquipItemData == null)
			{
				return;
			}
			this.m_cNKMEquipItemData = cNKMEquipItemData;
			NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeEquipData(cNKMEquipItemData);
			base.SetData(data2, false, true, true, delegate(NKCUISlot.SlotData data, bool bLocked)
			{
				selectedSlot(this, this.m_cNKMEquipItemData);
			});
			this.SetOnSelectedEquipSlot(selectedSlot, lockMaxItem);
			NKCUtil.SetGameobjectActive(this.m_objLock, cNKMEquipItemData.m_bLock);
			bool flag = false;
			if (cNKMEquipItemData.m_OwnerUnitUID > 0L)
			{
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(cNKMEquipItemData.m_OwnerUnitUID);
				if (unitFromUID != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
					if (unitTempletBase != null)
					{
						NKCUtil.SetGameobjectActive(this.m_objUsedUnit, true);
						flag = true;
						NKCUtil.SetImageSprite(this.m_imgUsedUnit, NKCResourceUtility.GetOrLoadMinimapFaceIcon(unitTempletBase, false), false);
					}
				}
			}
			if (!flag)
			{
				NKCUtil.SetGameobjectActive(this.m_objUsedUnit, false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_ITEM_EQUIP_FIERCE_BATTLE, false);
			NKCUtil.SetGameobjectActive(this.m_objPresetTag, bPresetContained);
		}

		// Token: 0x06006DB1 RID: 28081 RVA: 0x0023FB50 File Offset: 0x0023DD50
		public override void TurnOffExtraUI()
		{
			base.TurnOffExtraUI();
			NKCUtil.SetGameobjectActive(this.m_objEquipHaveCount, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_ITEM_EQUIP_FIERCE_BATTLE, false);
			NKCUtil.SetGameobjectActive(this.m_objUsedUnit, false);
			NKCUtil.SetGameobjectActive(this.m_objEquipHaveCount, false);
			NKCUtil.SetGameobjectActive(this.m_objSelectDelete, false);
			NKCUtil.SetGameobjectActive(this.m_objPresetTag, false);
			NKCUtil.SetGameobjectActive(this.m_objLock, false);
			NKCUtil.SetGameobjectActive(this.m_objEquipEmpty, false);
			NKCUtil.SetGameobjectActive(this.m_objMessage, false);
		}

		// Token: 0x06006DB2 RID: 28082 RVA: 0x0023FBD0 File Offset: 0x0023DDD0
		public void SetEmptyMaterial(NKCUISlotEquip.OnSelectedEquipSlot selectedSlot = null)
		{
			this.SetOnSelectedEquipSlot(selectedSlot, false);
			this.TurnOffExtraUI();
			NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
			NKCUtil.SetGameobjectActive(this.m_lbName, false);
			base.SetActiveCount(false);
			NKCUtil.SetGameobjectActive(this.m_lbItemAddCount, false);
			NKCUtil.SetGameobjectActive(this.m_objStarRoot, false);
			if (this.m_sp_MatAdd != null)
			{
				this.m_imgBG.sprite = this.m_sp_MatAdd;
				return;
			}
			this.m_imgBG.sprite = this.m_spEmpty;
		}

		// Token: 0x06006DB3 RID: 28083 RVA: 0x0023FC53 File Offset: 0x0023DE53
		public void SetOnSelectedEquipSlot(NKCUISlotEquip.OnSelectedEquipSlot selectedSlot, bool lockMaxItem = false)
		{
			if (selectedSlot != null)
			{
				this.m_cbtnButton.PointerClick.RemoveAllListeners();
				this.m_OnSelectedSlot = selectedSlot;
				this.m_cbtnButton.PointerClick.AddListener(new UnityAction(this.OnSelectedItemSlotImpl));
				this.m_bLockMaxItem = lockMaxItem;
			}
		}

		// Token: 0x06006DB4 RID: 28084 RVA: 0x0023FC92 File Offset: 0x0023DE92
		public void SetSlotMessage(bool bValue, string message, Color messageColor)
		{
			NKCUtil.SetGameobjectActive(this.m_objMessage, bValue);
			if (bValue)
			{
				NKCUtil.SetLabelText(this.m_lbMessage, message);
				NKCUtil.SetLabelTextColor(this.m_lbMessage, messageColor);
			}
		}

		// Token: 0x06006DB5 RID: 28085 RVA: 0x0023FCBC File Offset: 0x0023DEBC
		private void OnSelectedItemSlotImpl()
		{
			if (this.m_OnSelectedSlot != null)
			{
				if (this.m_cNKMEquipItemData != null)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(this.m_cNKMEquipItemData.m_ItemEquipID);
					if (this.m_bLockMaxItem && equipTemplet != null && this.m_cNKMEquipItemData.m_EnchantLevel >= NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER))
					{
						NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("NEC_FAIL_EQUIP_ITEM_ENCHANT_MAX", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
				}
				this.m_OnSelectedSlot(this, this.m_cNKMEquipItemData);
			}
		}

		// Token: 0x06006DB6 RID: 28086 RVA: 0x0023FD39 File Offset: 0x0023DF39
		public void SetSlotState(NKCUIInvenEquipSlot.EQUIP_SLOT_STATE eEQUIP_SLOT_STATE)
		{
			this.m_EQUIP_SLOT_STATE = eEQUIP_SLOT_STATE;
			NKCUtil.SetGameobjectActive(this.m_objSelected, this.m_EQUIP_SLOT_STATE == NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED);
			NKCUtil.SetGameobjectActive(this.m_objSelectDelete, this.m_EQUIP_SLOT_STATE == NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_DELETE);
		}

		// Token: 0x06006DB7 RID: 28087 RVA: 0x0023FD6A File Offset: 0x0023DF6A
		public void SetLock(bool bLock, bool bBig = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objLock, bLock);
			NKCUtil.SetGameobjectActive(this.m_objSelectLock, bLock && bBig);
		}

		// Token: 0x06006DB8 RID: 28088 RVA: 0x0023FD88 File Offset: 0x0023DF88
		public void SetEmpty(NKCUISlotEquip.OnSelectedEquipSlot selectedSlot = null, NKMEquipItemData cNKMEquipItemData = null)
		{
			this.TurnOffExtraUI();
			NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeEquipData(cNKMEquipItemData);
			base.SetData(data2, false, true, true, delegate(NKCUISlot.SlotData data, bool bLocked)
			{
				selectedSlot(this, cNKMEquipItemData);
			});
			this.SetOnSelectedEquipSlot(selectedSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objEquipEmpty, true);
			this.m_cNKMEquipItemData = cNKMEquipItemData;
		}

		// Token: 0x06006DB9 RID: 28089 RVA: 0x0023FDFD File Offset: 0x0023DFFD
		public void SetUpgradeSlotState(NKC_EQUIP_UPGRADE_STATE state)
		{
			NKCUtil.SetGameobjectActive(this.m_objMessage, state != NKC_EQUIP_UPGRADE_STATE.UPGRADABLE);
			NKCUtil.SetLabelText(this.m_lbMessage, NKCUtilString.GetEquipUpgradeStateString(state));
			NKCUtil.SetLabelTextColor(this.m_lbMessage, (state == NKC_EQUIP_UPGRADE_STATE.UPGRADABLE) ? Color.white : Color.red);
		}

		// Token: 0x06006DBA RID: 28090 RVA: 0x0023FE3D File Offset: 0x0023E03D
		public NKMEquipItemData GetNKMEquipItemData()
		{
			return this.m_cNKMEquipItemData;
		}

		// Token: 0x06006DBB RID: 28091 RVA: 0x0023FE45 File Offset: 0x0023E045
		public NKMEquipTemplet GetNKMEquipTemplet()
		{
			if (this.m_cNKMEquipItemData == null)
			{
				return null;
			}
			return NKMItemManager.GetEquipTemplet(this.m_cNKMEquipItemData.m_ItemEquipID);
		}

		// Token: 0x06006DBC RID: 28092 RVA: 0x0023FE61 File Offset: 0x0023E061
		public NKCUIInvenEquipSlot.EQUIP_SLOT_STATE Get_EQUIP_SLOT_STATE()
		{
			return this.m_EQUIP_SLOT_STATE;
		}

		// Token: 0x06006DBD RID: 28093 RVA: 0x0023FE69 File Offset: 0x0023E069
		public long GetEquipItemUID()
		{
			if (this.m_cNKMEquipItemData != null)
			{
				return this.m_cNKMEquipItemData.m_ItemUid;
			}
			return 0L;
		}

		// Token: 0x04005946 RID: 22854
		[Header("프리셋 태그")]
		public GameObject m_objPresetTag;

		// Token: 0x04005947 RID: 22855
		[Header("장비 잠김 표시")]
		public GameObject m_objLock;

		// Token: 0x04005948 RID: 22856
		[Header("장비 잠금 선택")]
		public GameObject m_objSelectLock;

		// Token: 0x04005949 RID: 22857
		[Header("장비 분해 선택")]
		public GameObject m_objSelectDelete;

		// Token: 0x0400594A RID: 22858
		[Header("장비 빈칸")]
		public GameObject m_objEquipEmpty;

		// Token: 0x0400594B RID: 22859
		[Header("장비 보유 수")]
		public GameObject m_objEquipHaveCount;

		// Token: 0x0400594C RID: 22860
		public Text m_lbEquipHaveCount;

		// Token: 0x0400594D RID: 22861
		[Header("장착중 유닛")]
		public GameObject m_objUsedUnit;

		// Token: 0x0400594E RID: 22862
		public Image m_imgUsedUnit;

		// Token: 0x0400594F RID: 22863
		[Header("격전지원")]
		public GameObject m_NKM_UI_ITEM_EQUIP_FIERCE_BATTLE;

		// Token: 0x04005950 RID: 22864
		public Text m_NKM_UI_UNIT_SELECT_LIST_FIERCE_BATTLE_TEXT;

		// Token: 0x04005951 RID: 22865
		[Header("메세지")]
		public GameObject m_objMessage;

		// Token: 0x04005952 RID: 22866
		public Text m_lbMessage;

		// Token: 0x04005953 RID: 22867
		private NKCUISlotEquip.OnSelectedEquipSlot m_OnSelectedSlot;

		// Token: 0x04005954 RID: 22868
		private NKMEquipItemData m_cNKMEquipItemData;

		// Token: 0x04005955 RID: 22869
		private bool m_bLockMaxItem;

		// Token: 0x04005956 RID: 22870
		private NKCUIInvenEquipSlot.EQUIP_SLOT_STATE m_EQUIP_SLOT_STATE;

		// Token: 0x020016FC RID: 5884
		// (Invoke) Token: 0x0600B1F4 RID: 45556
		public delegate void OnSelectedEquipSlot(NKCUISlotEquip slot, NKMEquipItemData data);
	}
}
