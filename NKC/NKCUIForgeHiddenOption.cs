using System;
using System.Collections;
using System.Collections.Generic;
using NKC.FX;
using NKM;
using NKM.Contract2;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000996 RID: 2454
	public class NKCUIForgeHiddenOption : MonoBehaviour
	{
		// Token: 0x170011CB RID: 4555
		// (set) Token: 0x060065DF RID: 26079 RVA: 0x00206F22 File Offset: 0x00205122
		public bool UnlockingSocket
		{
			set
			{
				this.m_bUnlockingSocket = value;
			}
		}

		// Token: 0x060065E0 RID: 26080 RVA: 0x00206F2C File Offset: 0x0020512C
		public void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnUnlockOption, new UnityAction(this.OnClickUnlockOption));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOptionInfo, new UnityAction(this.OnClickOptionInfo));
			if (this.m_HiddenOptionSlotArray != null)
			{
				int num = this.m_HiddenOptionSlotArray.Length;
				for (int i = 0; i < num; i++)
				{
					this.m_HiddenOptionSlotArray[i].Init();
				}
			}
			this.m_bUnlockingSocket = false;
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x00206F98 File Offset: 0x00205198
		public void SetOut()
		{
			this.m_rectMove.Set("Out");
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.DeactivateAllFx();
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x00206FBC File Offset: 0x002051BC
		public void AnimateOutToIn()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_rectMove.Set("Out");
			this.m_rectMove.Transit("In", null);
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x00206FEB File Offset: 0x002051EB
		public void SetEnchantCard(GameObject enchantCard)
		{
			this.m_objEnchantCard = enchantCard;
		}

		// Token: 0x060065E4 RID: 26084 RVA: 0x00206FF4 File Offset: 0x002051F4
		public void SetLeftEquipUID(long uid)
		{
			this.m_LeftEquipUID = uid;
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x00206FFD File Offset: 0x002051FD
		public void SetUI()
		{
			this.EnableUI(this.m_LeftEquipUID != 0L);
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x00207010 File Offset: 0x00205210
		public void ActivateUnlockFx(int socketIndex, NKCUIForgeHiddenOption.OnExplosionFxActivated dOnEffectActivated = null)
		{
			if (this.m_objSocketUnlockFx == null || this.m_objSocketUnlockFx.Length <= socketIndex)
			{
				return;
			}
			if (this.m_explosionFx != null && this.m_objEnchantCard != null)
			{
				this.m_explosionFx.position = this.m_objEnchantCard.transform.position;
			}
			NKCUtil.SetGameobjectActive(this.m_objFxRoot, true);
			NKCUtil.SetGameobjectActive(this.m_objSocketUnlockFx[socketIndex], true);
			this.m_dOnExplosionFxActivated = dOnEffectActivated;
			this.StopFxCoroutine();
			this.m_fxExplosionCoroutine = base.StartCoroutine(this.IExplosionFxActivationCheck());
		}

		// Token: 0x060065E7 RID: 26087 RVA: 0x002070A4 File Offset: 0x002052A4
		public bool IsEffectStopped()
		{
			if (this.m_fxActiveCheck == null)
			{
				return true;
			}
			int num = this.m_fxActiveCheck.Length;
			for (int i = 0; i < num; i++)
			{
				if (!(this.m_fxActiveCheck[i] == null) && this.m_fxActiveCheck[i].gameObject.activeInHierarchy && !this.m_fxActiveCheck[i].IsStopped)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060065E8 RID: 26088 RVA: 0x00207106 File Offset: 0x00205306
		public void Close()
		{
			this.m_LeftEquipUID = 0L;
			this.m_objEnchantCard = null;
			this.DeactivateAllFx();
			this.StopFxCoroutine();
			this.m_dOnExplosionFxActivated = null;
			this.m_fxExplosionPlayer = null;
			this.m_bUnlockingSocket = false;
		}

		// Token: 0x060065E9 RID: 26089 RVA: 0x00207138 File Offset: 0x00205338
		private IEnumerator IExplosionFxActivationCheck()
		{
			bool skip = false;
			this.m_csbtnUnlockOption.SetLock(true, false);
			if (this.m_fxExplosionPlayer != null)
			{
				while (!this.m_fxExplosionPlayer.gameObject.activeInHierarchy || this.m_fxExplosionPlayer.IsStopped)
				{
					if (Input.GetMouseButtonDown(0))
					{
						if (this.m_fxActiveCheck != null)
						{
							int num = this.m_fxActiveCheck.Length;
							for (int i = 0; i < num; i++)
							{
								this.m_fxActiveCheck[i].Stop();
							}
						}
						skip = true;
						break;
					}
					yield return null;
				}
			}
			this.m_csbtnUnlockOption.SetLock(false, false);
			if (this.m_dOnExplosionFxActivated != null)
			{
				this.m_dOnExplosionFxActivated();
			}
			if (skip)
			{
				this.m_fxExplosionPlayer.Restart();
			}
			yield break;
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x00207148 File Offset: 0x00205348
		private void EnableUI(bool bActive)
		{
			this.m_iOpenedSocketCount = 0;
			Transform explosionFx = this.m_explosionFx;
			this.m_fxExplosionPlayer = ((explosionFx != null) ? explosionFx.GetComponent<NKC_FXM_PLAYER>() : null);
			NKCUtil.SetGameobjectActive(this.m_objOptionList, bActive);
			NKCUtil.SetGameobjectActive(this.m_objDisable, bActive);
			NKCUtil.SetGameobjectActive(this.m_objEmpty, !bActive);
			NKCUtil.SetGameobjectActive(this.m_csbtnOptionInfo, bActive);
			this.m_csbtnUnlockOption.SetLock(true, false);
			if (!this.m_bUnlockingSocket)
			{
				this.DeactivateAllFx();
			}
			this.m_bUnlockingSocket = false;
			this.StopFxCoroutine();
			if (!bActive)
			{
				if (this.m_itemCostSlotArray != null)
				{
					int num = this.m_itemCostSlotArray.Length;
					for (int i = 0; i < num; i++)
					{
						this.m_itemCostSlotArray[i].SetData(0, 0, 0L, true, true, false);
					}
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objOptionList, false);
			NKCUtil.SetGameobjectActive(this.m_objDisable, true);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMEquipItemData nkmequipItemData = (myUserData != null) ? myUserData.m_InventoryData.GetItemEquip(this.m_LeftEquipUID) : null;
			if (nkmequipItemData == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			bool flag = equipTemplet.IsRelic();
			NKCUtil.SetGameobjectActive(this.m_objOptionList, flag);
			NKCUtil.SetGameobjectActive(this.m_objDisable, !flag);
			NKCUtil.SetGameobjectActive(this.m_csbtnOptionInfo, flag);
			if (flag)
			{
				if (NKMTempletContainer<NKMPotentialOptionGroupTemplet>.Find(equipTemplet.GetPotentialOptionGroupID()) == null)
				{
					Debug.LogError(string.Format("Potential Option Group Id: {0} of this EquipID: {1} does not exist", equipTemplet.GetPotentialOptionGroupID(), equipTemplet.m_ItemEquipID));
					return;
				}
				if (this.m_HiddenOptionSlotArray == null)
				{
					return;
				}
				if (nkmequipItemData.potentialOption == null)
				{
					int num2 = this.m_HiddenOptionSlotArray.Length;
					for (int j = 0; j < num2; j++)
					{
						int num3 = (this.m_socketEnchantLv != null && this.m_socketEnchantLv.Length > j) ? this.m_socketEnchantLv[j] : 99;
						this.m_HiddenOptionSlotArray[j].Lock(num3, num3 <= nkmequipItemData.m_EnchantLevel);
					}
					NKCUtil.SetLabelText(this.m_lbResult, NKCUtilString.GET_STRING_EQUIP_POTENTIAL_OPEN_REQUIRED);
					this.m_csbtnUnlockOption.SetLock(false, false);
				}
				else
				{
					int num4 = Mathf.Min(nkmequipItemData.potentialOption.sockets.Length, this.m_HiddenOptionSlotArray.Length);
					for (int k = 0; k < num4; k++)
					{
						int num5 = (this.m_socketEnchantLv != null && this.m_socketEnchantLv.Length > k) ? this.m_socketEnchantLv[k] : 99;
						if (this.m_HiddenOptionSlotArray.Length <= k || nkmequipItemData.potentialOption.sockets[k] == null)
						{
							this.m_HiddenOptionSlotArray[k].Lock(num5, num5 <= nkmequipItemData.m_EnchantLevel);
						}
						else
						{
							this.m_HiddenOptionSlotArray[k].Unlocked(k, nkmequipItemData.potentialOption.statType, nkmequipItemData.potentialOption.sockets[k].statValue, nkmequipItemData.potentialOption.sockets[k].statFactor);
							this.m_iOpenedSocketCount++;
						}
					}
					if (this.m_iOpenedSocketCount > 0)
					{
						NKCUtil.SetLabelText(this.m_lbResult, NKCUtil.GetPotentialStatText(nkmequipItemData));
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbResult, NKCUtilString.GET_STRING_EQUIP_POTENTIAL_OPEN_REQUIRED);
					}
					this.m_csbtnUnlockOption.SetLock(this.m_iOpenedSocketCount >= nkmequipItemData.potentialOption.sockets.Length, false);
				}
				if (this.m_itemCostSlotArray != null)
				{
					List<MiscItemUnit> socketOpenResource = equipTemplet.GetSocketOpenResource(this.m_iOpenedSocketCount);
					int num6 = this.m_itemCostSlotArray.Length;
					for (int l = 0; l < num6; l++)
					{
						if (socketOpenResource.Count <= l)
						{
							this.m_itemCostSlotArray[l].SetData(0, 0, 0L, true, true, false);
						}
						else
						{
							long curCnt = 0L;
							if (myUserData != null)
							{
								curCnt = myUserData.m_InventoryData.GetCountMiscItem(socketOpenResource[l].ItemId);
							}
							this.m_itemCostSlotArray[l].SetData(socketOpenResource[l].ItemId, socketOpenResource[l].Count32, curCnt, true, true, false);
						}
					}
					return;
				}
			}
			else
			{
				if (this.m_itemCostSlotArray != null)
				{
					int num7 = this.m_itemCostSlotArray.Length;
					for (int m = 0; m < num7; m++)
					{
						this.m_itemCostSlotArray[m].SetData(0, 0, 0L, true, true, false);
					}
				}
				this.m_csbtnUnlockOption.SetLock(true, false);
			}
		}

		// Token: 0x060065EB RID: 26091 RVA: 0x0020756C File Offset: 0x0020576C
		private void OnClickUnlockOption()
		{
			if (!this.IsEffectStopped())
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			List<MiscItemUnit> socketOpenResource = equipTemplet.GetSocketOpenResource(this.m_iOpenedSocketCount);
			int count = socketOpenResource.Count;
			int i = 0;
			while (i < count)
			{
				long countMiscItem = myUserData.m_InventoryData.GetCountMiscItem(socketOpenResource[i].ItemId);
				if (socketOpenResource[i].Count > countMiscItem)
				{
					int itemId = socketOpenResource[i].ItemId;
					if (itemId == 1 || itemId == 101)
					{
						NKCShopManager.OpenItemLackPopup(socketOpenResource[i].ItemId, (int)socketOpenResource[i].Count);
						return;
					}
					NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM, null, "");
					return;
				}
				else
				{
					i++;
				}
			}
			int num = (this.m_socketEnchantLv != null && this.m_socketEnchantLv.Length > this.m_iOpenedSocketCount) ? this.m_socketEnchantLv[this.m_iOpenedSocketCount] : 0;
			if (num > itemEquip.m_EnchantLevel)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_EQUIP_POTENTIAL_CANNOT_OPEN, num), null, "");
				return;
			}
			NKCPacketSender.Send_NKMPacket_EQUIP_OPEN_SOCKET_REQ(itemEquip.m_ItemUid, this.m_iOpenedSocketCount);
		}

		// Token: 0x060065EC RID: 26092 RVA: 0x002076B8 File Offset: 0x002058B8
		private void OnClickOptionInfo()
		{
			if (!this.IsEffectStopped() || this.m_LeftEquipUID == 0L)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMEquipItemData nkmequipItemData = (myUserData != null) ? myUserData.m_InventoryData.GetItemEquip(this.m_LeftEquipUID) : null;
			if (nkmequipItemData == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			NKCPopupHiddenOptionPopup.Instance.Open(equipTemplet.GetPotentialOptionGroupID());
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x0020771C File Offset: 0x0020591C
		private void DeactivateAllFx()
		{
			if (this.m_objSocketUnlockFx != null)
			{
				int num = this.m_objSocketUnlockFx.Length;
				for (int i = 0; i < num; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_objSocketUnlockFx[i], false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objFxRoot, false);
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x00207760 File Offset: 0x00205960
		private void StopFxCoroutine()
		{
			if (this.m_fxExplosionCoroutine != null)
			{
				base.StopCoroutine(this.m_fxExplosionCoroutine);
				this.m_fxExplosionCoroutine = null;
			}
		}

		// Token: 0x04005166 RID: 20838
		public NKCUIRectMove m_rectMove;

		// Token: 0x04005167 RID: 20839
		public GameObject m_objOptionList;

		// Token: 0x04005168 RID: 20840
		public GameObject m_objDisable;

		// Token: 0x04005169 RID: 20841
		public GameObject m_objEmpty;

		// Token: 0x0400516A RID: 20842
		public GameObject m_objResource;

		// Token: 0x0400516B RID: 20843
		public NKCUIComStateButton m_csbtnUnlockOption;

		// Token: 0x0400516C RID: 20844
		public NKCUIComStateButton m_csbtnOptionInfo;

		// Token: 0x0400516D RID: 20845
		public Text m_lbResult;

		// Token: 0x0400516E RID: 20846
		[Header("개방 옵션 슬롯")]
		public NKCUIForgeHiddenOptionSlot[] m_HiddenOptionSlotArray;

		// Token: 0x0400516F RID: 20847
		public int[] m_socketEnchantLv;

		// Token: 0x04005170 RID: 20848
		[Header("개방 비용 슬롯")]
		public NKCUIItemCostSlot[] m_itemCostSlotArray;

		// Token: 0x04005171 RID: 20849
		[Header("개방 이펙트")]
		public GameObject m_objFxRoot;

		// Token: 0x04005172 RID: 20850
		public Transform m_explosionFx;

		// Token: 0x04005173 RID: 20851
		public GameObject[] m_objSocketUnlockFx;

		// Token: 0x04005174 RID: 20852
		[Header("이펙트 활성화 체크")]
		public NKC_FXM_PLAYER[] m_fxActiveCheck;

		// Token: 0x04005175 RID: 20853
		private long m_LeftEquipUID;

		// Token: 0x04005176 RID: 20854
		private int m_iOpenedSocketCount;

		// Token: 0x04005177 RID: 20855
		private GameObject m_objEnchantCard;

		// Token: 0x04005178 RID: 20856
		private NKC_FXM_PLAYER m_fxExplosionPlayer;

		// Token: 0x04005179 RID: 20857
		private Coroutine m_fxExplosionCoroutine;

		// Token: 0x0400517A RID: 20858
		private bool m_bUnlockingSocket;

		// Token: 0x0400517B RID: 20859
		private NKCUIForgeHiddenOption.OnExplosionFxActivated m_dOnExplosionFxActivated;

		// Token: 0x02001665 RID: 5733
		// (Invoke) Token: 0x0600B021 RID: 45089
		public delegate void OnExplosionFxActivated();
	}
}
