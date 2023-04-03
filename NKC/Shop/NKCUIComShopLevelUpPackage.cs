using System;
using System.Collections.Generic;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD3 RID: 2771
	public class NKCUIComShopLevelUpPackage : MonoBehaviour, IShopPrefab
	{
		// Token: 0x06007C34 RID: 31796 RVA: 0x00297AB2 File Offset: 0x00295CB2
		public bool IsHideLockObject()
		{
			return this.m_bHideLockObject;
		}

		// Token: 0x06007C35 RID: 31797 RVA: 0x00297ABC File Offset: 0x00295CBC
		void IShopPrefab.SetData(ShopItemTemplet productTemplet)
		{
			if (productTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (productTemplet.m_PurchaseEventType != PURCHASE_EVENT_REWARD_TYPE.LEVELUP_PACKAGE)
			{
				Debug.LogError("Levelup Package가 아닌 아이템이 사용됨");
			}
			ShopLevelUpPackageGroupTemplet levelUpPackageGroupTemplet = NKCShopManager.GetLevelUpPackageGroupTemplet(productTemplet.m_PurchaseEventValue);
			if (levelUpPackageGroupTemplet == null)
			{
				Debug.LogError(string.Format("레벨업 패키지 정보 찾지 못함. id : {0}", productTemplet.m_PurchaseEventValue));
			}
			IEnumerable<ShopLevelUpPackageGroupData> groupDatas = levelUpPackageGroupTemplet.GetGroupDatas(0, levelUpPackageGroupTemplet.MaxLevelRequire);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			int userLevel = nkmuserData.m_UserLevel;
			int num = 0;
			bool flag = nkmuserData.m_ShopData.GetPurchasedCount(productTemplet) > 0;
			using (IEnumerator<ShopLevelUpPackageGroupData> enumerator = groupDatas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ShopLevelUpPackageGroupData shopLevelUpPackageGroupData = enumerator.Current;
					foreach (NKMRewardInfo nkmrewardInfo in shopLevelUpPackageGroupData.RewardInfos)
					{
						if (nkmrewardInfo.rewardType != NKM_REWARD_TYPE.RT_NONE)
						{
							if (num >= this.m_lstLevelUpReward.Count)
							{
								Debug.LogError("SlotInjector가 리워드 전체를 표기하기에 부족함!");
								break;
							}
							NKCUIComSlotDataInjector nkcuicomSlotDataInjector = this.m_lstLevelUpReward[num];
							nkcuicomSlotDataInjector.SetData(nkmrewardInfo);
							NKCUtil.SetGameobjectActive(nkcuicomSlotDataInjector, true);
							num++;
							if (string.IsNullOrEmpty(this.m_strkeyLevel))
							{
								NKCUtil.SetLabelText(nkcuicomSlotDataInjector.m_lbDesc, shopLevelUpPackageGroupData.LevelRequire.ToString());
							}
							else
							{
								NKCUtil.SetLabelText(nkcuicomSlotDataInjector.m_lbDesc, NKCStringTable.GetString(this.m_strkeyLevel, new object[]
								{
									shopLevelUpPackageGroupData.LevelRequire
								}));
							}
							if (flag && userLevel >= shopLevelUpPackageGroupData.LevelRequire)
							{
								NKCUISlot itemSlot = nkcuicomSlotDataInjector.m_itemSlot;
								if (itemSlot != null)
								{
									itemSlot.SetCompleteMark(true);
								}
							}
						}
					}
				}
				goto IL_1C4;
			}
			IL_1AE:
			NKCUtil.SetGameobjectActive(this.m_lstLevelUpReward[num], false);
			num++;
			IL_1C4:
			if (num >= this.m_lstLevelUpReward.Count)
			{
				return;
			}
			goto IL_1AE;
		}

		// Token: 0x04006901 RID: 26881
		[Header("레벨업 보상들이 들어갈 SlotDataInjector. Desc에 목표 레벨이 출력됨")]
		public List<NKCUIComSlotDataInjector> m_lstLevelUpReward;

		// Token: 0x04006902 RID: 26882
		[Header("목표 레벨 표기에 사용할 스트링 키. 없으면 그냥 숫자만")]
		public string m_strkeyLevel;

		// Token: 0x04006903 RID: 26883
		public bool m_bHideLockObject;
	}
}
