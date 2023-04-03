using System;
using ClientPacket.Contract;
using NKC.Templet;
using NKC.UI.Contract;
using NKM;
using NKM.Contract2;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Module
{
	// Token: 0x02000B24 RID: 2852
	public class NKCUIModuleSubUIContract : NKCUIModuleSubUIBase
	{
		// Token: 0x060081E8 RID: 33256 RVA: 0x002BC7A0 File Offset: 0x002BA9A0
		public override void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnShowList, new UnityAction(this.OnClickShowList));
			NKCUtil.SetLabelText(this.m_lbContractLeftTryCnt, string.Format(NKCUtilString.GET_STRING_CONTRACT_COUNT_ONE_PARAM, 1));
			NKCUtil.SetLabelText(this.m_lbContractRightTryCntOff, string.Format(NKCUtilString.GET_STRING_CONTRACT_COUNT_ONE_PARAM, 1));
		}

		// Token: 0x060081E9 RID: 33257 RVA: 0x002BC7FC File Offset: 0x002BA9FC
		public override void OnOpen(NKMEventCollectionIndexTemplet templet)
		{
			this.m_TargetContract = null;
			if (templet != null)
			{
				ContractTempletBase targetTemplet = NKCUIModuleSubUIContract.GetTargetTemplet(templet.EventContractId);
				if (targetTemplet == null)
				{
					Debug.Log("<color=red>�ش��ϴ� ä�� �����͸� ã�� �� �����ϴ�.</color>");
					return;
				}
				this.m_TargetContract = targetTemplet;
			}
			this.UpdateUI();
		}

		// Token: 0x060081EA RID: 33258 RVA: 0x002BC83A File Offset: 0x002BAA3A
		public override void Refresh()
		{
			this.UpdateUI();
		}

		// Token: 0x060081EB RID: 33259 RVA: 0x002BC844 File Offset: 0x002BAA44
		private void UpdateUI()
		{
			if (this.m_TargetContract == null)
			{
				return;
			}
			if (!(this.m_TargetContract is ContractTempletV2))
			{
				return;
			}
			ContractTempletV2 contractTempletV = this.m_TargetContract as ContractTempletV2;
			if (contractTempletV == null)
			{
				return;
			}
			for (int i = 0; i < contractTempletV.m_SingleTryRequireItems.Length; i++)
			{
				MiscItemUnit reqItem = contractTempletV.m_SingleTryRequireItems[i];
				if (reqItem != null)
				{
					NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
					ContractCostType _costType = (i == 0) ? ContractCostType.Ticket : ContractCostType.Money;
					int num = (int)inventoryData.GetCountMiscItem(reqItem.ItemId);
					NKCUtil.SetLabelText(this.m_lbMiscCnt, num.ToString());
					this.m_iCurMultiContractTryCnt = Math.Min(num / reqItem.Count32, 10);
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(reqItem.ItemId);
					if (itemMiscTempletByID != null)
					{
						Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
						NKCUtil.SetImageSprite(this.m_imgResourceIcon, orLoadMiscItemSmallIcon, false);
						NKCUtil.SetImageSprite(this.m_imgContractRightIcon, orLoadMiscItemSmallIcon, false);
					}
					this.m_btnContractLeft.PointerClick.RemoveAllListeners();
					this.m_btnContractLeft.SetData(reqItem.ItemId, reqItem.Count32);
					if ((long)num >= reqItem.Count)
					{
						this.m_btnContractLeft.PointerClick.AddListener(delegate()
						{
							NKCPacketSender.Send_NKMPacket_CONTRACT_REQ(this.m_TargetContract.Key, _costType, 1);
						});
					}
					else
					{
						this.m_btnContractLeft.PointerClick.AddListener(delegate()
						{
							NKCShopManager.OpenItemLackPopup(reqItem.ItemId, reqItem.Count32);
						});
					}
					int num2 = Math.Max(1, this.m_iCurMultiContractTryCnt);
					this.m_btnContractRight.PointerClick.RemoveAllListeners();
					this.m_btnContractRight.SetData(reqItem.ItemId, reqItem.Count32 * num2);
					NKCUtil.SetLabelText(this.m_lbContractRightTryCnt, string.Format(NKCUtilString.GET_STRING_CONTRACT_COUNT_ONE_PARAM, num2.ToString()));
					if (this.m_iCurMultiContractTryCnt > 0 && num >= reqItem.Count32 * this.m_iCurMultiContractTryCnt)
					{
						this.m_btnContractRight.PointerClick.AddListener(delegate()
						{
							NKCPacketSender.Send_NKMPacket_CONTRACT_REQ(this.m_TargetContract.Key, _costType, this.m_iCurMultiContractTryCnt);
						});
					}
					NKCUtil.SetGameobjectActive(this.m_objContractRightOn, this.m_iCurMultiContractTryCnt > 0);
					NKCUtil.SetGameobjectActive(this.m_objContractRightOff, this.m_iCurMultiContractTryCnt <= 0);
				}
			}
			Debug.Log(string.Format("<color=red>NKCUIModuleSubUIContract:UpdateUI[{0}] : {1} : {2}</color>", contractTempletV.Key, contractTempletV.GetContractName(), contractTempletV.GetContractDesc()));
		}

		// Token: 0x060081EC RID: 33260 RVA: 0x002BCAAC File Offset: 0x002BACAC
		public static ContractTempletBase GetTargetTemplet(int _contractID)
		{
			ContractTempletBase contractTempletBase = ContractTempletBase.FindBase(_contractID);
			if (contractTempletBase != null && contractTempletBase.EnableByTag)
			{
				NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
				if (nkccontractDataMgr != null && NKCSynchronizedTime.IsEventTime(contractTempletBase.EventIntervalTemplet) && nkccontractDataMgr.CheckOpenCond(contractTempletBase))
				{
					NKCContractCategoryTemplet nkccontractCategoryTemplet = NKCContractCategoryTemplet.Find(contractTempletBase.Category);
					if (nkccontractCategoryTemplet != null && nkccontractCategoryTemplet.m_Type == NKCContractCategoryTemplet.TabType.Hidden)
					{
						return contractTempletBase;
					}
				}
			}
			return null;
		}

		// Token: 0x060081ED RID: 33261 RVA: 0x002BCB0C File Offset: 0x002BAD0C
		private void OnClickShowList()
		{
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(this.m_TargetContract.Key);
			if (contractTempletV == null)
			{
				return;
			}
			NKCUIContractPopupRateV2.Instance.Open(contractTempletV);
		}

		// Token: 0x04006DFF RID: 28159
		[Header("UI")]
		public NKCUIComResourceButton m_btnContractLeft;

		// Token: 0x04006E00 RID: 28160
		public NKCUIComResourceButton m_btnContractRight;

		// Token: 0x04006E01 RID: 28161
		public Text m_lbContractRightTryCnt;

		// Token: 0x04006E02 RID: 28162
		public Text m_lbContractRightTryCntOff;

		// Token: 0x04006E03 RID: 28163
		public Text m_lbContractLeftTryCnt;

		// Token: 0x04006E04 RID: 28164
		public Image m_imgContractRightIcon;

		// Token: 0x04006E05 RID: 28165
		public GameObject m_objContractRightOn;

		// Token: 0x04006E06 RID: 28166
		public GameObject m_objContractRightOff;

		// Token: 0x04006E07 RID: 28167
		public NKCUIComStateButton m_csbtnShowList;

		// Token: 0x04006E08 RID: 28168
		public Image m_imgResourceIcon;

		// Token: 0x04006E09 RID: 28169
		public Text m_lbMiscCnt;

		// Token: 0x04006E0A RID: 28170
		private ContractTempletBase m_TargetContract;

		// Token: 0x04006E0B RID: 28171
		private int m_iCurMultiContractTryCnt;
	}
}
