using System;
using System.Collections.Generic;
using System.Text;
using NKC.Templet;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200093A RID: 2362
	public class NKCUIComItemDropInfoSlot : MonoBehaviour
	{
		// Token: 0x06005E66 RID: 24166 RVA: 0x001D3E3C File Offset: 0x001D203C
		private void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMove, new UnityAction(this.OnClickMove));
			this.m_OnMove = null;
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x001D3E5C File Offset: 0x001D205C
		public void SetData(ItemDropInfo itemDropInfo)
		{
			if (itemDropInfo == null)
			{
				this.ResetUI();
				return;
			}
			this.m_LockMsg = "";
			switch (itemDropInfo.ContentType)
			{
			case DropContent.Stage:
			{
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(itemDropInfo.ContentID);
				if (nkmstageTempletV == null)
				{
					this.ResetUI();
					return;
				}
				if (nkmstageTempletV.EpisodeTemplet != null)
				{
					ContentsType contentsType = NKCContentManager.GetContentsType(nkmstageTempletV.EpisodeTemplet.m_EPCategory);
					if (!NKCContentManager.IsContentsUnlocked(contentsType, 0, 0))
					{
						this.m_LockMsg = NKCContentManager.GetLockedMessage(contentsType, 0);
					}
					else if (contentsType == ContentsType.DAILY)
					{
						switch (nkmstageTempletV.EpisodeId)
						{
						case 101:
							this.m_LockMsg = NKCContentManager.GetLockedMessage(ContentsType.DAILY_ATTACK, 0);
							break;
						case 102:
							this.m_LockMsg = NKCContentManager.GetLockedMessage(ContentsType.DAILY_SEARCH, 0);
							break;
						case 103:
							this.m_LockMsg = NKCContentManager.GetLockedMessage(ContentsType.DAILY_DEFENCE, 0);
							break;
						}
					}
				}
				if (!NKMEpisodeMgr.CheckEpisodeMission(NKCScenManager.CurrentUserData(), nkmstageTempletV))
				{
					this.m_LockMsg = NKCUtilString.GetUnlockConditionRequireDesc(nkmstageTempletV, false);
				}
				if (string.IsNullOrEmpty(this.m_LockMsg) && !nkmstageTempletV.IsOpenedDayOfWeek())
				{
					this.m_LockMsg = NKCUtilString.GET_STRING_DAILY_CHECK_DAY;
				}
				this.SetUI(nkmstageTempletV, itemDropInfo.Summary);
				return;
			}
			case DropContent.Shop:
			{
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(itemDropInfo.ContentID);
				if (shopItemTemplet == null)
				{
					this.ResetUI();
					return;
				}
				this.SetUI(shopItemTemplet, itemDropInfo.Summary);
				return;
			}
			case DropContent.WorldMapMission:
			{
				NKMWorldMapMissionTemplet nkmworldMapMissionTemplet = NKMTempletContainer<NKMWorldMapMissionTemplet>.Find(itemDropInfo.ContentID);
				if (nkmworldMapMissionTemplet == null)
				{
					this.ResetUI();
					return;
				}
				this.SetUI(nkmworldMapMissionTemplet, itemDropInfo.Summary);
				return;
			}
			case DropContent.Raid:
			{
				NKMRaidTemplet nkmraidTemplet = NKMTempletContainer<NKMRaidTemplet>.Find(itemDropInfo.ContentID);
				if (nkmraidTemplet == null)
				{
					this.ResetUI();
					return;
				}
				this.SetUI(nkmraidTemplet, itemDropInfo.Summary);
				return;
			}
			case DropContent.Shadow:
			{
				NKMShadowPalaceTemplet nkmshadowPalaceTemplet = NKMTempletContainer<NKMShadowPalaceTemplet>.Find(itemDropInfo.ContentID);
				if (nkmshadowPalaceTemplet == null)
				{
					this.ResetUI();
					return;
				}
				this.SetUI(nkmshadowPalaceTemplet, itemDropInfo.Summary);
				return;
			}
			case DropContent.Dive:
			{
				NKMDiveTemplet nkmdiveTemplet = NKMTempletContainer<NKMDiveTemplet>.Find(itemDropInfo.ContentID);
				if (nkmdiveTemplet == null)
				{
					this.ResetUI();
					return;
				}
				this.SetUI(nkmdiveTemplet, itemDropInfo.Summary);
				return;
			}
			case DropContent.Fierce:
			{
				NKMFiercePointRewardTemplet nkmfiercePointRewardTemplet = NKMTempletContainer<NKMFiercePointRewardTemplet>.Find(itemDropInfo.ContentID);
				if (nkmfiercePointRewardTemplet == null)
				{
					this.ResetUI();
					return;
				}
				this.SetUI(nkmfiercePointRewardTemplet, itemDropInfo.Summary);
				return;
			}
			case DropContent.RandomMoldBox:
				this.SetRandomMoldBoxUI(itemDropInfo.ItemID);
				return;
			case DropContent.UnitDismiss:
				this.SetUnitDismissUI();
				return;
			case DropContent.UnitExtract:
				this.SetUnitExtractUI();
				return;
			case DropContent.Trim:
				this.SetTrimRewardUI(itemDropInfo.ContentID);
				return;
			case DropContent.SubStreamShop:
				this.SetStageShopUI(itemDropInfo);
				return;
			default:
				this.ResetUI();
				return;
			}
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x001D40C4 File Offset: 0x001D22C4
		public static NKCUIComItemDropInfoSlot GetNewInstance(Transform parent, bool bMentoringSlot = false)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_ITEM_DROP_SLOT", false, null);
			NKCUIComItemDropInfoSlot nkcuicomItemDropInfoSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIComItemDropInfoSlot>() : null;
			if (nkcuicomItemDropInfoSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKM_UI_POPUP_COLLECTION_ACHIEVEMENT_SLOT Prefab null!");
				return null;
			}
			nkcuicomItemDropInfoSlot.m_InstanceData = nkcassetInstanceData;
			nkcuicomItemDropInfoSlot.Init();
			if (parent != null)
			{
				nkcuicomItemDropInfoSlot.transform.SetParent(parent);
			}
			nkcuicomItemDropInfoSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuicomItemDropInfoSlot.gameObject.SetActive(false);
			return nkcuicomItemDropInfoSlot;
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x001D415E File Offset: 0x001D235E
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			this.m_OnMove = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x001D4184 File Offset: 0x001D2384
		private void SetPurchaseTextState(bool isPurchaseButton)
		{
			NKCUtil.SetGameobjectActive(this.m_objMoveText, !isPurchaseButton);
			NKCUtil.SetGameobjectActive(this.m_objPurchaseText, isPurchaseButton);
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x001D41A4 File Offset: 0x001D23A4
		private void SetUI(NKMStageTempletV2 stageTemplet, bool summary)
		{
			if (!summary)
			{
				if (stageTemplet.EpisodeTemplet != null)
				{
					string episodeCategoryEx = NKCUtilString.GetEpisodeCategoryEx1(stageTemplet.EpisodeTemplet.m_EPCategory);
					switch (stageTemplet.EpisodeTemplet.m_EPCategory)
					{
					case EPISODE_CATEGORY.EC_MAINSTREAM:
					{
						string text = (stageTemplet.m_Difficulty == EPISODE_DIFFICULTY.NORMAL) ? "" : ("[" + NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_ADDON_HARD", false) + "]");
						string msg = string.Format("{0} {1}-{2}-{3} {4}", new object[]
						{
							episodeCategoryEx,
							stageTemplet.EpisodeTemplet.GetEpisodeTitle(),
							stageTemplet.ActId,
							stageTemplet.m_StageUINum,
							text
						});
						NKCUtil.SetLabelText(this.m_lbDropStage, msg);
						goto IL_17A;
					}
					case EPISODE_CATEGORY.EC_EVENT:
					{
						string text2 = (stageTemplet.m_Difficulty == EPISODE_DIFFICULTY.NORMAL) ? "" : ("[" + NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_ADDON_HARD", false) + "]");
						string msg2 = string.Format("{0} {1}-{2}-{3} {4}", new object[]
						{
							episodeCategoryEx,
							stageTemplet.EpisodeTemplet.GetEpisodeTitle(),
							stageTemplet.ActId,
							stageTemplet.m_StageUINum,
							text2
						});
						NKCUtil.SetLabelText(this.m_lbDropStage, msg2);
						goto IL_17A;
					}
					}
					NKCUtil.SetLabelText(this.m_lbDropStage, episodeCategoryEx);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbDropStage, NKCStringTable.GetString("SI_DP_EPISODE_CATEGORY_EC_DEFAULT", false));
				}
				IL_17A:
				this.SetStageTempletShortCut(stageTemplet, true);
			}
			else
			{
				if (stageTemplet.EpisodeTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GetEpisodeCategoryEx1(stageTemplet.EpisodeCategory));
					this.SetStageTempletShortCut(stageTemplet, false);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_MENU_NAME_OPERATION_VIEWER);
					this.m_OnMove = delegate()
					{
						NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION, "", false);
					};
				}
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCStringTable.GetString("SI_ITEM_DROP_INFO_ALL_DROP", false));
			}
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x001D43B0 File Offset: 0x001D25B0
		private void SetUI(ShopItemTemplet shopItemTemplet, bool summary)
		{
			if (!summary)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append((shopItemTemplet.TabTemplet != null) ? shopItemTemplet.TabTemplet.GetTabName() : " - ");
				if (shopItemTemplet != null)
				{
					int buyCountLeft = NKCShopManager.GetBuyCountLeft(shopItemTemplet.m_ProductID);
					string value;
					if (shopItemTemplet.TabTemplet.IsCountResetType)
					{
						value = string.Format(NKCUtilString.GET_STRING_SHOP_PURCHASE_COUNT_TWO_PARAM, buyCountLeft, shopItemTemplet.m_QuantityLimit);
					}
					else
					{
						value = NKCShopManager.GetBuyCountString(shopItemTemplet.resetType, buyCountLeft, shopItemTemplet.m_QuantityLimit, false);
					}
					stringBuilder.Append(" ");
					stringBuilder.Append(value);
				}
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, stringBuilder.ToString());
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_SHOP);
				bool isSupplyItem = shopItemTemplet.TabTemplet != null && shopItemTemplet.TabTemplet.m_TabName == "TAB_SUPPLY";
				this.m_OnMove = delegate()
				{
					NKCShopManager.OnBtnProductBuy(shopItemTemplet.m_ProductID, isSupplyItem);
				};
				this.SetPurchaseTextState(true);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCUtilString.GET_STRING_SHOP);
			NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_SHOP);
			this.m_OnMove = delegate()
			{
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_SHOP, "", false);
			};
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x001D4540 File Offset: 0x001D2740
		private void SetUI(NKMWorldMapMissionTemplet wmmTemplet, bool summary)
		{
			if (!summary)
			{
				string msg = (wmmTemplet != null) ? wmmTemplet.GetMissionName() : " - ";
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, msg);
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_WORLDMAP);
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_WORLDMAP_MISSION, "", false);
				};
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCStringTable.GetString("SI_DP_WORLDMAP_MISSION", false));
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_WORLDMAP);
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_WORLDMAP_MISSION, "", false);
				};
			}
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x001D45F8 File Offset: 0x001D27F8
		private void SetUI(NKMRaidTemplet raidTemplet, bool summary)
		{
			if (!summary)
			{
				string msg = (raidTemplet != null && raidTemplet.DungeonTempletBase != null) ? raidTemplet.DungeonTempletBase.GetDungeonName() : " - ";
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, msg);
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_RAID);
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_WORLDMAP_MISSION, "", false);
				};
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCUtilString.GET_STRING_RAID);
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_WORLDMAP);
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_WORLDMAP_MISSION, "", false);
				};
			}
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x001D46B8 File Offset: 0x001D28B8
		private void SetUI(NKMShadowPalaceTemplet shadowPalaceTemplet, bool summary)
		{
			if (!summary)
			{
				string msg = (shadowPalaceTemplet != null) ? shadowPalaceTemplet.PalaceName : " - ";
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, msg);
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_SHADOW_PALACE);
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_SHADOW_PALACE, "", false);
				};
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, " - ");
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_SHADOW_PALACE);
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_SHADOW_PALACE, "", false);
				};
			}
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x001D4768 File Offset: 0x001D2968
		private void SetUI(NKMDiveTemplet diveTemplet, bool summary)
		{
			if (!summary)
			{
				string msg = (diveTemplet != null) ? diveTemplet.Get_STAGE_NAME() : " - ";
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, msg);
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_DIVE);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCUtilString.GET_STRING_DIVE);
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_WORLDMAP);
			}
			this.m_OnMove = delegate()
			{
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.DIVE, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.DIVE, 0);
					return;
				}
				UnlockInfo unlockInfo = new UnlockInfo(diveTemplet.StageUnlockReqType, diveTemplet.StageUnlockReqValue);
				if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), unlockInfo, false))
				{
					string unlockConditionRequireDesc = NKCUtilString.GetUnlockConditionRequireDesc(STAGE_UNLOCK_REQ_TYPE.SURT_DIVE_HISTORY_CLEARED, diveTemplet.StageUnlockReqValue, string.Empty, DateTime.MinValue, false);
					if (!string.IsNullOrEmpty(unlockConditionRequireDesc))
					{
						NKCPopupMessageManager.AddPopupMessage(unlockConditionRequireDesc, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					}
					return;
				}
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_DIVE, "", false);
			};
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x001D47F8 File Offset: 0x001D29F8
		private void SetUI(NKMFiercePointRewardTemplet fprTemplet, bool summary)
		{
			if (!summary)
			{
				string msg = (fprTemplet != null) ? NKCStringTable.GetString("SI_SHORTCUT_TITLE_FIERCE", false) : " - ";
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, msg);
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_FIERCE);
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_FIERCE, "", false);
				};
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCStringTable.GetString("SI_PF_FIERCE_BATTLE_BUTTON_SCORE_REWARD_TEXT", false));
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_FIERCE);
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_FIERCE, "", false);
				};
			}
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x001D48B4 File Offset: 0x001D2AB4
		private void SetRandomMoldBoxUI(int itemID)
		{
			NKCUtil.SetLabelText(this.m_lbDropStage, NKCStringTable.GetString("SI_OFFICE_ROOM_NAME_TECH_FACTORY", false));
			NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCStringTable.GetString("SI_DP_FORGE_CRAFT_MOLD", false));
			NKM_CRAFT_TAB_TYPE destTab = NKM_CRAFT_TAB_TYPE.MT_EQUIP;
			string[] names = Enum.GetNames(typeof(NKM_CRAFT_TAB_TYPE));
			int num = names.Length;
			for (int i = 0; i < num; i++)
			{
				NKM_CRAFT_TAB_TYPE nkm_CRAFT_TAB_TYPE = NKM_CRAFT_TAB_TYPE.MT_EQUIP;
				if (Enum.TryParse<NKM_CRAFT_TAB_TYPE>(names[i], out nkm_CRAFT_TAB_TYPE) && this.CheckCraftTabContainItem(nkm_CRAFT_TAB_TYPE, itemID))
				{
					destTab = nkm_CRAFT_TAB_TYPE;
					break;
				}
			}
			this.m_OnMove = delegate()
			{
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_EQUIP_MAKE, destTab.ToString(), false);
			};
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E73 RID: 24179 RVA: 0x001D4958 File Offset: 0x001D2B58
		private void SetUnitDismissUI()
		{
			NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_MANAGEMENT);
			NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCUtilString.GET_STRING_REMOVE_UNIT);
			this.m_OnMove = delegate()
			{
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_DISMISS, "", false);
			};
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x001D49B4 File Offset: 0x001D2BB4
		private void SetUnitExtractUI()
		{
			NKCUtil.SetLabelText(this.m_lbDropStage, NKCStringTable.GetString("SI_OFFICE_ROOM_NAME_TECH_LAB", false));
			NKCUtil.SetLabelText(this.m_lbDropInfoDesc, NKCUtilString.GET_STRING_REARM_EXTRACT_TITLE);
			this.m_OnMove = delegate()
			{
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, "EXTRACT", false);
			};
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x001D4A14 File Offset: 0x001D2C14
		private void SetTrimRewardUI(int trimId)
		{
			NKCUtil.SetLabelText(this.m_lbDropStage, NKCStringTable.GetString("SI_GUIDE_CATEGORY_TITLE_MANUAL_TRIM", false));
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimId);
			string msg = (nkmtrimTemplet != null) ? NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false) : " - ";
			NKCUtil.SetLabelText(this.m_lbDropInfoDesc, msg);
			this.m_OnMove = delegate()
			{
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_TRIM, "", false);
			};
			this.SetPurchaseTextState(false);
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x001D4A90 File Offset: 0x001D2C90
		private void SetStageShopUI(ItemDropInfo itemDropInfo)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(itemDropInfo.ContentID);
			if (shopItemTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbDropStage, NKCUtilString.GET_STRING_SHOP);
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, shopItemTemplet.TabTemplet.GetTabName());
				this.m_OnMove = delegate()
				{
					NKCShopManager.OnBtnProductBuy(shopItemTemplet.m_ProductID, false);
				};
			}
			this.SetPurchaseTextState(true);
		}

		// Token: 0x06005E77 RID: 24183 RVA: 0x001D4B00 File Offset: 0x001D2D00
		private void SetStageTempletShortCut(NKMStageTempletV2 stageTemplet, bool setLabelDesc)
		{
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				NKMWarfareTemplet warfareTemplet = NKMWarfareTemplet.Find(stageTemplet.m_StageBattleStrID);
				if (warfareTemplet != null)
				{
					if (setLabelDesc)
					{
						NKCUtil.SetLabelText(this.m_lbDropInfoDesc, warfareTemplet.GetWarfareName());
					}
					this.m_OnMove = delegate()
					{
						NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_MAINSTREAM, warfareTemplet.m_WarfareID.ToString(), false);
					};
					return;
				}
				if (setLabelDesc)
				{
					NKCUtil.SetLabelText(this.m_lbDropInfoDesc, " - ");
				}
				if (stageTemplet.EpisodeTemplet != null)
				{
					this.m_OnMove = delegate()
					{
						NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION, stageTemplet.EpisodeCategory.ToString(), false);
					};
					return;
				}
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_MAINSTREAM, "", false);
				};
				return;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(stageTemplet.m_StageBattleStrID);
				if (dungeonTempletBase != null)
				{
					if (setLabelDesc)
					{
						NKCUtil.SetLabelText(this.m_lbDropInfoDesc, dungeonTempletBase.GetDungeonName());
					}
					this.m_OnMove = delegate()
					{
						NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_DUNGEON, stageTemplet.Key.ToString(), false);
					};
					return;
				}
				if (setLabelDesc)
				{
					NKCUtil.SetLabelText(this.m_lbDropInfoDesc, " - ");
				}
				if (stageTemplet.EpisodeTemplet != null)
				{
					this.m_OnMove = delegate()
					{
						NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION, stageTemplet.EpisodeCategory.ToString(), false);
					};
					return;
				}
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_DUNGEON, "", false);
				};
				return;
			}
			case STAGE_TYPE.ST_PHASE:
			{
				NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(stageTemplet.m_StageBattleStrID);
				string paramException = (stageTemplet.EpisodeTemplet != null) ? stageTemplet.EpisodeCategory.ToString() : "";
				NKM_SHORTCUT_TYPE shortCutTypeException = (stageTemplet.EpisodeTemplet != null) ? NKM_SHORTCUT_TYPE.SHORTCUT_OPERATION : NKM_SHORTCUT_TYPE.SHORTCUT_DUNGEON;
				if (nkmphaseTemplet == null)
				{
					if (setLabelDesc)
					{
						NKCUtil.SetLabelText(this.m_lbDropInfoDesc, " - ");
					}
					this.m_OnMove = delegate()
					{
						NKCContentManager.MoveToShortCut(shortCutTypeException, paramException, false);
					};
					return;
				}
				if (setLabelDesc)
				{
					NKCUtil.SetLabelText(this.m_lbDropInfoDesc, nkmphaseTemplet.GetName());
				}
				if (nkmphaseTemplet.PhaseList == null || nkmphaseTemplet.PhaseList.List.Count <= 0)
				{
					this.m_OnMove = delegate()
					{
						NKCContentManager.MoveToShortCut(shortCutTypeException, paramException, false);
					};
					return;
				}
				if (nkmphaseTemplet.PhaseList.List[0].Dungeon != null)
				{
					this.m_OnMove = delegate()
					{
						NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_DUNGEON, stageTemplet.Key.ToString(), false);
					};
					return;
				}
				this.m_OnMove = delegate()
				{
					NKCContentManager.MoveToShortCut(shortCutTypeException, paramException, false);
				};
				return;
			}
			default:
				NKCUtil.SetLabelText(this.m_lbDropInfoDesc, "");
				return;
			}
		}

		// Token: 0x06005E78 RID: 24184 RVA: 0x001D4DA8 File Offset: 0x001D2FA8
		private bool CheckCraftTabContainItem(NKM_CRAFT_TAB_TYPE craftTabType, int itemID)
		{
			List<NKMMoldItemData> moldItemData = NKMItemManager.GetMoldItemData(craftTabType);
			int count = moldItemData.Count;
			for (int i = 0; i < count; i++)
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(moldItemData[i].m_MoldID);
				List<int> list;
				if (itemMoldTempletByID != null && NKMItemManager.m_dicRandomMoldBox.TryGetValue(itemMoldTempletByID.m_RewardGroupID, out list) && list.Contains(itemID))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005E79 RID: 24185 RVA: 0x001D4E05 File Offset: 0x001D3005
		private void ResetUI()
		{
			NKCUtil.SetLabelText(this.m_lbDropStage, "");
			NKCUtil.SetLabelText(this.m_lbDropInfoDesc, "");
			this.SetPurchaseTextState(false);
			this.m_OnMove = null;
		}

		// Token: 0x06005E7A RID: 24186 RVA: 0x001D4E35 File Offset: 0x001D3035
		private void OnClickMove()
		{
			if (!string.IsNullOrEmpty(this.m_LockMsg))
			{
				NKCPopupMessageManager.AddPopupMessage(this.m_LockMsg, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (this.m_OnMove != null)
			{
				this.m_OnMove();
			}
		}

		// Token: 0x04004A8A RID: 19082
		public Text m_lbDropStage;

		// Token: 0x04004A8B RID: 19083
		public Text m_lbDropInfoDesc;

		// Token: 0x04004A8C RID: 19084
		public NKCUIComStateButton m_csbtnMove;

		// Token: 0x04004A8D RID: 19085
		public GameObject m_objMoveText;

		// Token: 0x04004A8E RID: 19086
		public GameObject m_objPurchaseText;

		// Token: 0x04004A8F RID: 19087
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04004A90 RID: 19088
		private string m_LockMsg;

		// Token: 0x04004A91 RID: 19089
		private NKCUIComItemDropInfoSlot.OnMove m_OnMove;

		// Token: 0x020015C0 RID: 5568
		// (Invoke) Token: 0x0600AE26 RID: 44582
		public delegate void OnMove();
	}
}
