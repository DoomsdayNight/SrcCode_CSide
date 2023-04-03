using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Shop;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000938 RID: 2360
	public class NKCUIComItemCount : MonoBehaviour
	{
		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x001D2EEC File Offset: 0x001D10EC
		// (set) Token: 0x06005E44 RID: 24132 RVA: 0x001D2EF4 File Offset: 0x001D10F4
		public int CurrentItemID { get; private set; }

		// Token: 0x06005E45 RID: 24133 RVA: 0x001D2EFD File Offset: 0x001D10FD
		public void SetEndDate(DateTime endDate)
		{
			this.m_EndDate = endDate;
			this.m_bEndDateEvent = true;
		}

		// Token: 0x06005E46 RID: 24134 RVA: 0x001D2F0D File Offset: 0x001D110D
		public void SetEndDateEvent(NKCUIComItemCount.OnEndDateEvent dOnEndDateEvent)
		{
			this.m_dOnEndDateEvent = dOnEndDateEvent;
		}

		// Token: 0x06005E47 RID: 24135 RVA: 0x001D2F16 File Offset: 0x001D1116
		public void SetOnClickPlusBtn(NKCUIComItemCount.OnClickPlusBtn dOnClickPlusBtn)
		{
			this.m_dOnClickPlusBtn = dOnClickPlusBtn;
		}

		// Token: 0x06005E48 RID: 24136 RVA: 0x001D2F1F File Offset: 0x001D111F
		public void SetMaxCount(long max)
		{
			this.m_MaxCount = max;
		}

		// Token: 0x06005E49 RID: 24137 RVA: 0x001D2F28 File Offset: 0x001D1128
		public void SetTimeLabel(string text)
		{
			this.m_strTimeText = text;
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x001D2F31 File Offset: 0x001D1131
		private void Start()
		{
			if (this.m_csbtnPlus != null)
			{
				this.m_csbtnPlus.PointerClick.RemoveAllListeners();
				this.m_csbtnPlus.PointerClick.AddListener(new UnityAction(this.OnClickPlusBtnImpl));
			}
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x001D2F6D File Offset: 0x001D116D
		private void OnClickPlusBtnImpl()
		{
			if (this.m_dOnClickPlusBtn != null)
			{
				this.m_dOnClickPlusBtn();
			}
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x001D2F82 File Offset: 0x001D1182
		private void OnEnable()
		{
			this.UpdateRemainTimeForAddEvent();
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x001D2F8C File Offset: 0x001D118C
		private void UpdateRemainTimeForAddEvent()
		{
			if (this.m_lbRemainTimeForAddEvent != null)
			{
				TimeSpan timeSpan = this.m_EndDate - NKCSynchronizedTime.GetServerUTCTime(0.0);
				if (timeSpan.TotalHours > 0.0)
				{
					this.m_lbRemainTimeForAddEvent.text = NKCUtilString.GetTimeSpanString(timeSpan);
				}
				else if (timeSpan.TotalSeconds > 0.0)
				{
					this.m_lbRemainTimeForAddEvent.text = NKCUtilString.GetTimeSpanStringMS(timeSpan);
				}
				else if (string.IsNullOrEmpty(this.m_strTimeText))
				{
					this.m_lbRemainTimeForAddEvent.text = NKCUtilString.GetTimeSpanStringMS(timeSpan);
				}
				else
				{
					this.m_lbRemainTimeForAddEvent.text = this.m_strTimeText;
				}
				if (this.m_bEndDateEvent && NKCSynchronizedTime.IsFinished(this.m_EndDate))
				{
					if (this.m_dOnEndDateEvent != null)
					{
						this.m_bEndDateEvent = !this.m_dOnEndDateEvent();
						return;
					}
					this.m_bEndDateEvent = false;
				}
			}
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x001D3078 File Offset: 0x001D1278
		private void Update()
		{
			if (this.m_lbRemainTimeForAddEvent != null && this.m_fPrevUpdateTime + 1f < Time.time)
			{
				this.m_fPrevUpdateTime = Time.time;
				this.UpdateRemainTimeForAddEvent();
			}
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x001D30AC File Offset: 0x001D12AC
		public void SetData(NKMUserData userData, int itemID)
		{
			if (userData == null)
			{
				return;
			}
			NKMItemMiscData itemMisc = userData.m_InventoryData.GetItemMisc(itemID);
			this.UpdateData(itemMisc, itemID);
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x001D30D2 File Offset: 0x001D12D2
		public void UpdateData(NKMUserData userData)
		{
			if (this.CurrentItemID == 0)
			{
				return;
			}
			this.SetData(userData, this.CurrentItemID);
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x001D30EC File Offset: 0x001D12EC
		public void UpdateData(NKMItemMiscData itemData, int itemID = 0)
		{
			if (this.m_imgIcon != null && ((itemData != null && this.CurrentItemID != itemData.ItemID) || itemID != 0))
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
				if (itemMiscTempletByID != null)
				{
					Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
					this.m_imgIcon.sprite = orLoadMiscItemSmallIcon;
				}
			}
			if (this.m_lstImgIcon.Count > 0 && itemData != null)
			{
				for (int i = 0; i < this.m_lstImgIcon.Count; i++)
				{
					if ((long)i < itemData.TotalCount)
					{
						NKCUtil.SetGameobjectActive(this.m_lstImgIcon[i], true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstImgIcon[i], false);
					}
				}
			}
			long num = 0L;
			if (itemData != null)
			{
				if (this.m_lbPlusCount == null)
				{
					NKCUtil.SetLabelText(this.m_lbCount, this.GetItemCountString(itemData.CountFree, itemData.CountPaid));
				}
				else
				{
					NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
					if (inventoryData.IsFirstUpdate(itemData.ItemID, itemData.TotalCount))
					{
						if (base.gameObject.activeInHierarchy)
						{
							base.StopCoroutine(this.ChangeItemCount(null, 0L));
							base.StartCoroutine(this.ChangeItemCount(itemData, inventoryData.GetPreviousItemCount(itemData.ItemID, itemData.TotalCount)));
						}
						else
						{
							NKCUtil.SetLabelText(this.m_lbCount, this.GetItemCountString(itemData.CountFree, itemData.CountPaid));
						}
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbCount, this.GetItemCountString(itemData.CountFree, itemData.CountPaid));
					}
				}
				num = itemData.TotalCount;
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbCount, this.GetItemCountString(0L, 0L));
			}
			bool bValue = this.m_MaxCount > 0L && num < this.m_MaxCount;
			NKCUtil.SetGameobjectActive(this.m_objRemainTime, bValue);
			this.CurrentItemID = ((itemData != null) ? itemData.ItemID : itemID);
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x001D32BE File Offset: 0x001D14BE
		public void CleanUp()
		{
			this.CurrentItemID = 0;
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x001D32C8 File Offset: 0x001D14C8
		private string GetItemCountString(long freeCount, long cashCount)
		{
			return (freeCount + cashCount).ToString("N0");
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x001D32E5 File Offset: 0x001D14E5
		private IEnumerator ChangeItemCount(NKMItemMiscData miscItem, long oldVal)
		{
			if (oldVal == 0L)
			{
				NKCUtil.SetLabelText(this.m_lbCount, this.GetItemCountString(miscItem.CountFree, miscItem.CountPaid));
			}
			else if (oldVal != miscItem.TotalCount)
			{
				bool flag = oldVal < miscItem.TotalCount;
				long num = miscItem.TotalCount - oldVal;
				string msg = flag ? string.Format("(+{0})", num) : string.Format("({0})", num);
				NKCUtil.SetGameobjectActive(this.m_lbPlusCount.gameObject, true);
				NKCUtil.SetLabelText(this.m_lbPlusCount, msg);
				Color col = flag ? NKCUtil.GetColor("#FFCF3B") : NKCUtil.GetColor("#E92322");
				NKCUtil.SetLabelTextColor(this.m_lbPlusCount, col);
				NKCUtil.SetGameobjectActive(this.m_TempHideObject, false);
				this.m_lbPlusCount.DOFade(0f, 1f).SetDelay(1f).OnComplete(delegate
				{
					NKCUtil.SetGameobjectActive(this.m_lbPlusCount.gameObject, false);
					NKCUtil.SetGameobjectActive(this.m_TempHideObject, true);
				});
				if (num <= 0L)
				{
					NKCUtil.SetLabelText(this.m_lbCount, this.GetItemCountString(miscItem.CountFree, miscItem.CountPaid));
					yield return null;
				}
				else
				{
					float fUpdateTime = 0f;
					long increseVal = 1L;
					if (num > (long)this.m_iMaxGap)
					{
						fUpdateTime = this.m_fChangeTime / (float)this.m_iMaxGap;
						increseVal = num / (long)this.m_iMaxGap;
					}
					else
					{
						fUpdateTime = this.m_fChangeTime / (float)num;
					}
					while (oldVal < miscItem.TotalCount)
					{
						oldVal += increseVal;
						NKCUtil.SetLabelText(this.m_lbCount, ((int)oldVal).ToString());
						yield return new WaitForSeconds(fUpdateTime);
					}
					NKCUtil.SetLabelText(this.m_lbCount, this.GetItemCountString(miscItem.CountFree, miscItem.CountPaid));
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x001D3304 File Offset: 0x001D1504
		public void OpenMoveToShopPopup()
		{
			if (NKCShopManager.IsMoveToShopDefined(this.CurrentItemID))
			{
				NKCPopupItemBox nkcpopupItemBox = NKCPopupItemBox.OpenNewInstance();
				if (nkcpopupItemBox != null)
				{
					nkcpopupItemBox.OpenItemBox(this.CurrentItemID, NKCPopupItemBox.eMode.MoveToShop, new NKCPopupItemBox.OnButton(this.OnMoveToShop));
					return;
				}
			}
			else
			{
				NKCPopupItemBox nkcpopupItemBox2 = NKCPopupItemBox.OpenNewInstance();
				if (nkcpopupItemBox2 != null)
				{
					nkcpopupItemBox2.OpenItemBox(this.CurrentItemID, NKCPopupItemBox.eMode.Normal, null);
				}
			}
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x001D3364 File Offset: 0x001D1564
		private void OnMoveToShop()
		{
			if (NKCShopManager.CanUsePopupShopBuy(this.CurrentItemID))
			{
				NKCPopupShopBuyShortcut.Open(this.CurrentItemID);
				return;
			}
			if (NKCUIForge.IsInstanceOpen && NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					this.MoveToShop();
				}, null, false);
				return;
			}
			if (NKCUIForge.IsInstanceOpen && NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_SET_OPTION_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					this.MoveToShop();
				}, null, false);
				return;
			}
			this.MoveToShop();
		}

		// Token: 0x06005E57 RID: 24151 RVA: 0x001D33FC File Offset: 0x001D15FC
		private void MoveToShop()
		{
			TabId shopMoveTab = NKCShopManager.GetShopMoveTab(this.CurrentItemID);
			if (shopMoveTab.Type == "TAB_NONE")
			{
				Debug.LogWarning(string.Format("상점 바로가기가 정의되지 않은 타입 - {0}", this.CurrentItemID));
				return;
			}
			NKCUIShop.ShopShortcut(shopMoveTab.Type, shopMoveTab.SubIndex, 0);
		}

		// Token: 0x04004A75 RID: 19061
		public Image m_imgIcon;

		// Token: 0x04004A76 RID: 19062
		public List<Image> m_lstImgIcon;

		// Token: 0x04004A77 RID: 19063
		public Text m_lbCount;

		// Token: 0x04004A78 RID: 19064
		public Text m_lbPlusCount;

		// Token: 0x04004A79 RID: 19065
		public Text m_lbRemainTimeForAddEvent;

		// Token: 0x04004A7A RID: 19066
		public GameObject m_objRemainTime;

		// Token: 0x04004A7B RID: 19067
		public NKCUIComStateButton m_csbtnPlus;

		// Token: 0x04004A7D RID: 19069
		private DateTime m_EndDate;

		// Token: 0x04004A7E RID: 19070
		private float m_fPrevUpdateTime;

		// Token: 0x04004A7F RID: 19071
		private NKCUIComItemCount.OnEndDateEvent m_dOnEndDateEvent;

		// Token: 0x04004A80 RID: 19072
		private bool m_bEndDateEvent;

		// Token: 0x04004A81 RID: 19073
		private NKCUIComItemCount.OnClickPlusBtn m_dOnClickPlusBtn;

		// Token: 0x04004A82 RID: 19074
		public GameObject m_TempHideObject;

		// Token: 0x04004A83 RID: 19075
		private long m_MaxCount;

		// Token: 0x04004A84 RID: 19076
		private string m_strTimeText;

		// Token: 0x04004A85 RID: 19077
		private float m_fChangeTime = 0.33f;

		// Token: 0x04004A86 RID: 19078
		private int m_iMaxGap = 100;

		// Token: 0x020015BB RID: 5563
		// (Invoke) Token: 0x0600AE12 RID: 44562
		public delegate bool OnEndDateEvent();

		// Token: 0x020015BC RID: 5564
		// (Invoke) Token: 0x0600AE16 RID: 44566
		public delegate void OnClickPlusBtn();
	}
}
