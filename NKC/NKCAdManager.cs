using System;
using System.Collections.Generic;
using ClientPacket.Event;
using NKC.Advertise;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000617 RID: 1559
	public static class NKCAdManager
	{
		// Token: 0x0600302C RID: 12332 RVA: 0x000ED2B0 File Offset: 0x000EB4B0
		public static NKMADItemRewardInfo GetItemRewardInfo(int itemId)
		{
			return NKCAdManager.itemRewardInfos.Find((NKMADItemRewardInfo e) => e.adItemId == itemId);
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000ED2E0 File Offset: 0x000EB4E0
		public static void SetItemRewardInfo(List<NKMADItemRewardInfo> _itemRewardInfos)
		{
			NKCAdManager.itemRewardInfos = _itemRewardInfos;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000ED2E8 File Offset: 0x000EB4E8
		public static void SetInventoryExpandRewardInfo(List<NKM_INVENTORY_EXPAND_TYPE> _inventoryExpandRewardInfos)
		{
			NKCAdManager.inventoryExpandRewardInfos = _inventoryExpandRewardInfos;
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000ED2F0 File Offset: 0x000EB4F0
		public static void UpdateItemRewardInfo(NKMADItemRewardInfo itemRewardInfo)
		{
			int num = NKCAdManager.itemRewardInfos.FindIndex((NKMADItemRewardInfo e) => e.adItemId == itemRewardInfo.adItemId);
			if (num < 0 || num >= NKCAdManager.itemRewardInfos.Count)
			{
				NKCAdManager.itemRewardInfos.Add(itemRewardInfo);
				return;
			}
			NKCAdManager.itemRewardInfos[num] = itemRewardInfo;
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000ED354 File Offset: 0x000EB554
		public static void UpdateInventoryRewardInfo(NKM_INVENTORY_EXPAND_TYPE inventoryType)
		{
			int num = NKCAdManager.inventoryExpandRewardInfos.FindIndex((NKM_INVENTORY_EXPAND_TYPE e) => e == inventoryType);
			if (num < 0 || num >= NKCAdManager.inventoryExpandRewardInfos.Count)
			{
				NKCAdManager.inventoryExpandRewardInfos.Add(inventoryType);
			}
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x000ED3A6 File Offset: 0x000EB5A6
		public static bool IsOpenTagEnabled()
		{
			return NKMADTemplet.EnableByTag;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000ED3B0 File Offset: 0x000EB5B0
		public static bool IsAdRewardItem(int itemId)
		{
			NKMADTemplet nkmadtemplet = NKMADTemplet.Find(itemId);
			return NKMADTemplet.EnableByTag && nkmadtemplet != null && NKMOpenTagManager.IsOpened(nkmadtemplet.OpenTag) && NKCAdBase.Instance.IsAdvertiseEnabled();
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000ED3E8 File Offset: 0x000EB5E8
		public static bool IsAdRewardInventory(NKM_INVENTORY_EXPAND_TYPE inventoryType)
		{
			bool flag = false;
			switch (inventoryType)
			{
			case NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP:
				flag = (NKMCommonConst.INVENTORY_EQUIP_EXPAND_COUNT > 0);
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT:
				flag = (NKMCommonConst.INVENTORY_UNIT_EXPAND_COUNT > 0);
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP:
				flag = (NKMCommonConst.INVENTORY_SHIP_EXPAND_COUNT > 0);
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR:
				flag = (NKMCommonConst.INVENTORY_OPERATOR_EXPAND_COUNT > 0);
				break;
			}
			return NKMADTemplet.EnableByTag && flag && NKCAdBase.Instance.IsAdvertiseEnabled();
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000ED450 File Offset: 0x000EB650
		public static int GetAdItemRewardRemainDailyCount(int itemId)
		{
			NKMADItemRewardInfo itemRewardInfo = NKCAdManager.GetItemRewardInfo(itemId);
			if (itemRewardInfo == null)
			{
				return NKCAdManager.GetAdItemRewardMaxDailyCount(itemId);
			}
			return itemRewardInfo.remainDailyLimit;
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000ED474 File Offset: 0x000EB674
		public static int GetAdItemRewardMaxDailyCount(int itemId)
		{
			NKMADTemplet nkmadtemplet = NKMADTemplet.Find(itemId);
			if (nkmadtemplet == null)
			{
				return 0;
			}
			return nkmadtemplet.DayLimit;
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000ED494 File Offset: 0x000EB694
		public static bool AdItemRewardCoolTimeFinished(int itemId)
		{
			NKMADItemRewardInfo itemRewardInfo = NKCAdManager.GetItemRewardInfo(itemId);
			if (itemRewardInfo == null)
			{
				return true;
			}
			if (itemRewardInfo.remainDailyLimit <= 0)
			{
				return false;
			}
			NKMADTemplet nkmadtemplet = NKMADTemplet.Find(itemId);
			return nkmadtemplet != null && NKCSynchronizedTime.IsFinished(NKCSynchronizedTime.ToUtcTime(itemRewardInfo.latestRewardTime.AddSeconds((double)(nkmadtemplet.WatchCoolTime + 1))));
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000ED4E4 File Offset: 0x000EB6E4
		public static TimeSpan GetAdItemRewardCoolTime(int itemId)
		{
			NKMADItemRewardInfo itemRewardInfo = NKCAdManager.GetItemRewardInfo(itemId);
			if (itemRewardInfo == null)
			{
				return new TimeSpan(0, 0, 0);
			}
			NKMADTemplet nkmadtemplet = NKMADTemplet.Find(itemId);
			if (nkmadtemplet == null)
			{
				return new TimeSpan(long.MaxValue);
			}
			return NKCSynchronizedTime.GetTimeLeft(NKCSynchronizedTime.ToUtcTime(itemRewardInfo.latestRewardTime.AddSeconds((double)(nkmadtemplet.WatchCoolTime + 1))));
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000ED53B File Offset: 0x000EB73B
		public static bool InventoryRewardReceived(NKM_INVENTORY_EXPAND_TYPE inventoryType)
		{
			return NKCAdManager.inventoryExpandRewardInfos.Contains(inventoryType);
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000ED548 File Offset: 0x000EB748
		public static void WatchItemRewardAd(int itemId)
		{
			int itemId2 = itemId;
			NKCAdBase.AD_TYPE adType;
			if (itemId2 != 1)
			{
				if (itemId2 != 2)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ERROR, null, "");
					return;
				}
				adType = NKCAdBase.AD_TYPE.ETERNIUM;
			}
			else
			{
				adType = NKCAdBase.AD_TYPE.CREDIT;
			}
			NKCAdBase.Instance.WatchRewardedAd(adType, delegate
			{
				NKCPacketSender.Send_NKMPacket_AD_ITEM_REWARD_REQ(itemId);
			}, new NKCAdBase.OnAdFailedToShowAd(NKCAdManager.OnAdFailedToShow));
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000ED5B8 File Offset: 0x000EB7B8
		public static void WatchInventoryRewardAd(NKM_INVENTORY_EXPAND_TYPE inventoryType)
		{
			NKCAdBase.AD_TYPE adType;
			switch (inventoryType)
			{
			case NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP:
				adType = NKCAdBase.AD_TYPE.EQUIP_INV;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT:
				adType = NKCAdBase.AD_TYPE.UNIT_INV;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP:
				adType = NKCAdBase.AD_TYPE.SHIP_INV;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR:
				adType = NKCAdBase.AD_TYPE.OPERATOR_INV;
				break;
			default:
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ERROR, null, "");
				return;
			}
			NKCAdBase.Instance.WatchRewardedAd(adType, delegate
			{
				NKCPacketSender.Send_NKMPacket_AD_INVENTORY_EXPAND_REQ(inventoryType);
			}, new NKCAdBase.OnAdFailedToShowAd(NKCAdManager.OnAdFailedToShow));
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000ED640 File Offset: 0x000EB840
		private static void OnAdFailedToShow(NKCAdBase.NKC_ADMOB_ERROR_CODE resultCode, string message)
		{
			if (resultCode != NKCAdBase.NKC_ADMOB_ERROR_CODE.NARC_FAILED_TO_LOAD)
			{
				if (resultCode != NKCAdBase.NKC_ADMOB_ERROR_CODE.NARC_FAILED_TO_SHOW)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR, null, "");
					return;
				}
				if (NKCDefineManager.DEFINE_USE_CHEAT())
				{
					string content = string.IsNullOrEmpty(message) ? NKCUtilString.GET_STRING_AD_FAILED_TO_SHOW : (NKCUtilString.GET_STRING_AD_FAILED_TO_SHOW + "\n" + message);
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, content, null, "");
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_AD_FAILED_TO_SHOW, null, "");
				return;
			}
			else
			{
				if (NKCDefineManager.DEFINE_USE_CHEAT())
				{
					string content2 = string.IsNullOrEmpty(message) ? NKCUtilString.GET_STRING_AD_FAILED_TO_LOAD : (NKCUtilString.GET_STRING_AD_FAILED_TO_LOAD + "\n" + message);
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, content2, null, "");
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_AD_FAILED_TO_LOAD, null, "");
				return;
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000ED70C File Offset: 0x000EB90C
		public static void SetItemRewardAdButtonState(NKCPopupItemBox.eMode mode, int itemId, NKCUIComStateButton btnAdOn, NKCUIComStateButton btnAdOff, Text lbAdRemainCount)
		{
			if (mode != NKCPopupItemBox.eMode.MoveToShop || !NKCAdManager.IsAdRewardItem(itemId))
			{
				NKCUtil.SetGameobjectActive(btnAdOn, false);
				NKCUtil.SetGameobjectActive(btnAdOff, false);
				return;
			}
			if (NKCAdManager.GetAdItemRewardRemainDailyCount(itemId) <= 0)
			{
				NKCUtil.SetGameobjectActive(btnAdOn, false);
				NKCUtil.SetGameobjectActive(btnAdOff, true);
				return;
			}
			NKCUtil.SetGameobjectActive(btnAdOn, true);
			NKCUtil.SetGameobjectActive(btnAdOff, false);
			NKCUtil.SetLabelText(lbAdRemainCount, string.Format("({0}/{1})", NKCAdManager.GetAdItemRewardRemainDailyCount(itemId), NKCAdManager.GetAdItemRewardMaxDailyCount(itemId)));
			bool flag = NKCAdManager.AdItemRewardCoolTimeFinished(itemId);
			if (btnAdOn != null)
			{
				btnAdOn.SetLock(!flag, false);
			}
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000ED798 File Offset: 0x000EB998
		public static void UpdateItemRewardAdCoolTime(int itemId, NKCUIComStateButton btnAdOn, NKCUIComStateButton btnAdOff, Text lbAdCoolTime, Text lbAdRemainCount)
		{
			if (!NKCAdBase.Instance.IsAdvertiseEnabled())
			{
				return;
			}
			bool flag = btnAdOn != null && btnAdOn.gameObject.activeSelf && btnAdOn.m_bLock;
			bool flag2 = btnAdOff != null && btnAdOff.gameObject.activeSelf;
			if (!flag && !flag2)
			{
				return;
			}
			TimeSpan adItemRewardCoolTime = NKCAdManager.GetAdItemRewardCoolTime(itemId);
			if (flag)
			{
				if (adItemRewardCoolTime.Ticks > 0L)
				{
					NKCUtil.SetLabelText(lbAdCoolTime, NKCSynchronizedTime.GetTimeSpanString(adItemRewardCoolTime) ?? "");
					return;
				}
				NKCUtil.SetLabelText(lbAdRemainCount, string.Format("({0}/{1})", NKCAdManager.GetAdItemRewardRemainDailyCount(itemId), NKCAdManager.GetAdItemRewardMaxDailyCount(itemId)));
				if (btnAdOn != null)
				{
					btnAdOn.SetLock(false, false);
					return;
				}
			}
			else if (flag2 && adItemRewardCoolTime.Ticks <= 0L)
			{
				NKCAdManager.SetItemRewardAdButtonState(NKCPopupItemBox.eMode.MoveToShop, itemId, btnAdOn, btnAdOff, lbAdRemainCount);
			}
		}

		// Token: 0x04002F9F RID: 12191
		private static List<NKMADItemRewardInfo> itemRewardInfos = new List<NKMADItemRewardInfo>();

		// Token: 0x04002FA0 RID: 12192
		private static List<NKM_INVENTORY_EXPAND_TYPE> inventoryExpandRewardInfos = new List<NKM_INVENTORY_EXPAND_TYPE>();
	}
}
