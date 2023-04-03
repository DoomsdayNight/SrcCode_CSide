using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009A2 RID: 2466
	public class NKCUIHangarBuildSlot : MonoBehaviour
	{
		// Token: 0x060066CE RID: 26318 RVA: 0x0020E737 File Offset: 0x0020C937
		public void InitUI(UnityAction openAction, UnityAction closeAction, NKCUIHangarBuildSlot.OnShipInfo ShowInfo)
		{
			NKCUtil.SetBindFunction(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_BUTTON, new UnityAction(this.OpenConfirmPopup));
			this.m_openAction = openAction;
			this.m_closeAction = closeAction;
			this.dOnShipInfo = ShowInfo;
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x0020E768 File Offset: 0x0020C968
		private void OpenConfirmPopup()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (nkmuserData.m_ArmyData.GetCurrentShipCount() < nkmuserData.m_ArmyData.m_MaxShipCount)
			{
				if (this.m_BUTTON_MATERIAL_LACK.activeSelf)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_HANGAR_CONFIRM_FAIL, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
				if (this.m_BUTTON_CRAFT.activeSelf)
				{
					NKCUIPopupHangarBuildConfirm.Instance.Open(this.m_ShipID, new NKCUIPopupHangarBuildConfirm.OnTryBuildShip(this.OnTryBuildShip), this.m_closeAction);
					if (this.m_openAction != null)
					{
						this.m_openAction();
					}
				}
				return;
			}
			int count = 1;
			int num;
			bool flag = !NKCAdManager.IsAdRewardInventory(NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP) || !NKMInventoryManager.CanExpandInventoryByAd(NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP, nkmuserData, count, out num);
			if (!NKMInventoryManager.CanExpandInventory(NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP, nkmuserData, count, out num) && flag)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_CANNOT_EXPAND_INVENTORY), null, "");
				return;
			}
			string expandDesc = NKCUtilString.GetExpandDesc(NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP, true);
			NKCPopupInventoryAdd.SliderInfo sliderInfo = default(NKCPopupInventoryAdd.SliderInfo);
			sliderInfo.increaseCount = 1;
			sliderInfo.maxCount = 60;
			sliderInfo.currentCount = nkmuserData.m_ArmyData.m_MaxShipCount;
			sliderInfo.inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP;
			NKCPopupInventoryAdd.Instance.Open(NKCUtilString.GET_STRING_INVENTORY_SHIP, expandDesc, sliderInfo, 100, 101, delegate(int value)
			{
				NKCPacketSender.Send_NKMPacket_INVENTORY_EXPAND_REQ(NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP, value);
			}, true);
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x0020E8B8 File Offset: 0x0020CAB8
		public void SetData(NKMShipBuildTemplet data, bool IsNew = false)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			bool flag = true;
			for (int i = 0; i < this.m_lstItemSlot.Length; i++)
			{
				if (i + 1 > data.BuildMaterialList.Count)
				{
					this.m_lstItemSlot[i].SetData(0, 0, 0L, true, true, false);
				}
				else
				{
					BuildMaterial buildMaterial = data.BuildMaterialList[i];
					this.m_lstItemSlot[i].SetData(buildMaterial.m_ShipBuildMaterialID, buildMaterial.m_ShipBuildMaterialCount, nkmuserData.m_InventoryData.GetCountMiscItem(buildMaterial.m_ShipBuildMaterialID), true, true, false);
					if (flag)
					{
						flag = ((long)buildMaterial.m_ShipBuildMaterialCount <= nkmuserData.m_InventoryData.GetCountMiscItem(buildMaterial.m_ShipBuildMaterialID));
					}
				}
			}
			if (!NKMShipManager.CanUnlockShip(nkmuserData, data))
			{
				NKCUtil.SetGameobjectActive(this.m_BUTTON_MATERIAL_LACK, false);
				NKCUtil.SetGameobjectActive(this.m_BUTTON_CRAFT, false);
				NKCUtil.SetGameobjectActive(this.m_BUTTON_BUILD_LOCK, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_HANGAR_BUILD_SLOT_SHIP_NOTGET, true);
				NKCUtil.SetGameobjectActive(this.m_BUILD_LOCK_NOTICE, true);
				NKCUtil.SetLabelText(this.m_BUILD_LOCK_NOTICE_TEXT, this.GetUnlockConditionText(data));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_BUTTON_BUILD_LOCK, false);
				NKCUtil.SetGameobjectActive(this.m_BUTTON_MATERIAL_LACK, !flag);
				NKCUtil.SetGameobjectActive(this.m_BUTTON_CRAFT, flag);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_HANGAR_BUILD_SLOT_SHIP_NOTGET, false);
				NKCUtil.SetGameobjectActive(this.m_BUILD_LOCK_NOTICE, false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_BADGE, IsNew);
			bool bValue = false;
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyShip)
			{
				if (NKMShipManager.IsSameKindShip(keyValuePair.Value.m_UnitID, data.ShipID))
				{
					bValue = true;
					break;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_TAG_GET, bValue);
			this.m_ShipID = data.ShipID;
			NKMUnitTempletBase templetBase = NKMUnitManager.GetUnitTempletBase(data.ShipID);
			if (templetBase != null)
			{
				Sprite sp = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, templetBase);
				NKCUtil.SetImageSprite(this.m_SHIP_CARD_IMG, sp, false);
				NKCUtil.SetLabelText(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPNAME, templetBase.GetUnitName());
				NKCUtil.SetLabelText(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPINFO_CLASS_TEXT, templetBase.GetUnitTitle());
				Sprite shipGradeSprite = NKCUtil.GetShipGradeSprite(templetBase.m_NKM_UNIT_GRADE);
				NKCUtil.SetImageSprite(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPINFO_GRADE, shipGradeSprite, true);
				Sprite orLoadUnitStyleIcon = NKCResourceUtility.GetOrLoadUnitStyleIcon(templetBase.m_NKM_UNIT_STYLE_TYPE, true);
				NKCUtil.SetImageSprite(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPINFO_CLASS_ICON, orLoadUnitStyleIcon, true);
				for (int j = 0; j < this.m_lstSkillSlot.Length; j++)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(templetBase, j);
					if (shipSkillTempletByIndex != null)
					{
						NKCUtil.SetImageSprite(this.m_lstSkillSlot[j], NKCUtil.GetSkillIconSprite(shipSkillTempletByIndex), false);
						if (shipSkillTempletByIndex.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_PASSIVE)
						{
							NKCUtil.SetLabelText(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_BOTTOM_PASSIVE_TEXT, shipSkillTempletByIndex.GetDesc());
						}
						NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[j], true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[j], false);
					}
				}
				UnityAction <>9__1;
				foreach (NKCUIComStateButton btn in this.m_lstSkillBtn)
				{
					UnityAction bindFunc;
					if ((bindFunc = <>9__1) == null)
					{
						bindFunc = (<>9__1 = delegate()
						{
							NKCPopupSkillFullInfo.ShipInstance.OpenForShip(data.ShipID, 0L);
						});
					}
					NKCUtil.SetBindFunction(btn, bindFunc);
				}
				NKCUtil.SetBindFunction(this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPINFO_SHORTCUT_BUTTON, delegate()
				{
					this.OnMoveShipInfo(templetBase.m_UnitID);
				});
			}
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x0020EC54 File Offset: 0x0020CE54
		private string GetUnlockConditionText(NKMShipBuildTemplet templet)
		{
			if (templet == null)
			{
				return "";
			}
			switch (templet.ShipBuildUnlockType)
			{
			case NKMShipBuildTemplet.BuildUnlockType.BUT_PLAYER_LEVEL:
				return string.Format(NKCUtilString.GET_STRING_SHIP_BUILD_CONDITION_FAIL_PLAYER_LEVEL, templet.UnlockValue);
			case NKMShipBuildTemplet.BuildUnlockType.BUT_DUNGEON_CLEAR:
				return this.GetUnlockConditionDiscription(templet.UnlockValue, true);
			case NKMShipBuildTemplet.BuildUnlockType.BUT_WARFARE_CLEAR:
				return this.GetUnlockConditionDiscription(templet.UnlockValue, false);
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHIP_GET:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(templet.UnlockValue);
				return string.Format(NKCUtilString.GET_STRING_SHIP_BUILD_CONDITION_FAIL_SHIP_COLLECT, unitTempletBase.GetUnitName());
			}
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHIP_LV100:
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(templet.UnlockValue);
				return string.Format(NKCUtilString.GET_STRING_SHIP_BUILD_CONDITION_FAIL_SHIP_LEVEL, unitTempletBase2.GetUnitName(), 100);
			}
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHADOW_CLEAR:
			{
				NKMShadowPalaceTemplet nkmshadowPalaceTemplet = NKMTempletContainer<NKMShadowPalaceTemplet>.Find(templet.UnlockValue);
				if (nkmshadowPalaceTemplet != null)
				{
					return string.Format(NKCUtilString.GET_STRING_SHIP_BUILD_CONDITION_FAIL_SHADOW_CLEAR, nkmshadowPalaceTemplet.PalaceName);
				}
				break;
			}
			}
			return "";
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x0020ED34 File Offset: 0x0020CF34
		private string GetUnlockConditionDiscription(int key, bool bDungeon = true)
		{
			string result = "";
			if (bDungeon)
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(key);
				if (dungeonTempletBase != null)
				{
					result = string.Format(NKCUtilString.GET_STRING_SHIP_BUILD_CONDITION_FAIL_DUNGEON_CLEAR, dungeonTempletBase.GetDungeonName());
				}
			}
			else
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(key);
				if (nkmwarfareTemplet != null)
				{
					NKMStageTempletV2 stageTemplet = nkmwarfareTemplet.StageTemplet;
					if (stageTemplet != null)
					{
						result = string.Format(NKCUtilString.GET_STRING_SHIP_BUILD_CONDITION_FAIL_WARFARE_CLEAR, stageTemplet.EpisodeId - 1, stageTemplet.ActId, stageTemplet.m_StageUINum);
					}
				}
			}
			return result;
		}

		// Token: 0x060066D3 RID: 26323 RVA: 0x0020EDAA File Offset: 0x0020CFAA
		private void OnMoveShipInfo(int shipID)
		{
			NKCUIHangarBuildSlot.OnShipInfo onShipInfo = this.dOnShipInfo;
			if (onShipInfo == null)
			{
				return;
			}
			onShipInfo(shipID);
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x0020EDBD File Offset: 0x0020CFBD
		private void OnTryBuildShip(int shipID)
		{
			NKCUIPopupHangarBuildConfirm.Instance.Close();
			NKCPacketSender.Send_NKMPacket_SHIP_BUILD_REQ(shipID);
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x0020EDD0 File Offset: 0x0020CFD0
		public RectTransform GetRect(string strValue)
		{
			string text = strValue.ToUpper();
			if (text != null)
			{
				if (text == "SHIP")
				{
					return this.m_rectTop;
				}
				if (text == "SKILL")
				{
					return this.m_rectSkill;
				}
				if (text == "COST")
				{
					return this.m_rectCost;
				}
				if (text == "BUTTON")
				{
					return this.m_NKM_UI_HANGAR_BUILD_SLOT_LIST_BUTTON.GetComponent<RectTransform>();
				}
			}
			return null;
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x0020EE3F File Offset: 0x0020D03F
		public int ShipID
		{
			get
			{
				return this.m_ShipID;
			}
		}

		// Token: 0x04005294 RID: 21140
		[Header("함선 이미지")]
		public Image m_SHIP_CARD_IMG;

		// Token: 0x04005295 RID: 21141
		public GameObject m_NKM_UI_HANGAR_BUILD_SLOT_LIST_BADGE;

		// Token: 0x04005296 RID: 21142
		public NKCUIComStateButton m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPINFO_SHORTCUT_BUTTON;

		// Token: 0x04005297 RID: 21143
		public GameObject m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_TAG_GET;

		// Token: 0x04005298 RID: 21144
		public Text m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPNAME;

		// Token: 0x04005299 RID: 21145
		public Image m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPINFO_GRADE;

		// Token: 0x0400529A RID: 21146
		public Image m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPINFO_CLASS_ICON;

		// Token: 0x0400529B RID: 21147
		public Text m_NKM_UI_HANGAR_BUILD_SLOT_LIST_TOP_SHIPINFO_CLASS_TEXT;

		// Token: 0x0400529C RID: 21148
		public Text m_NKM_UI_HANGAR_BUILD_SLOT_LIST_BOTTOM_PASSIVE_TEXT;

		// Token: 0x0400529D RID: 21149
		public NKCUIComStateButton[] m_lstSkillBtn;

		// Token: 0x0400529E RID: 21150
		public Image[] m_lstSkillSlot;

		// Token: 0x0400529F RID: 21151
		public NKCUIItemCostSlot[] m_lstItemSlot;

		// Token: 0x040052A0 RID: 21152
		public NKCUIComStateButton m_NKM_UI_HANGAR_BUILD_SLOT_LIST_BUTTON;

		// Token: 0x040052A1 RID: 21153
		[Header("잠금")]
		public GameObject m_BUILD_LOCK_BG;

		// Token: 0x040052A2 RID: 21154
		public GameObject m_NKM_UI_HANGAR_BUILD_SLOT_SHIP_NOTGET;

		// Token: 0x040052A3 RID: 21155
		public GameObject m_BUILD_LOCK_NOTICE;

		// Token: 0x040052A4 RID: 21156
		public Text m_BUILD_LOCK_NOTICE_TEXT;

		// Token: 0x040052A5 RID: 21157
		[Header("버튼들")]
		public GameObject m_BUTTON_BUILD_LOCK;

		// Token: 0x040052A6 RID: 21158
		public GameObject m_BUTTON_MATERIAL_LACK;

		// Token: 0x040052A7 RID: 21159
		public GameObject m_BUTTON_CRAFT;

		// Token: 0x040052A8 RID: 21160
		[Header("튜토리얼용")]
		public RectTransform m_rectTop;

		// Token: 0x040052A9 RID: 21161
		public RectTransform m_rectSkill;

		// Token: 0x040052AA RID: 21162
		public RectTransform m_rectCost;

		// Token: 0x040052AB RID: 21163
		private int m_ShipID;

		// Token: 0x040052AC RID: 21164
		private UnityAction m_openAction;

		// Token: 0x040052AD RID: 21165
		private UnityAction m_closeAction;

		// Token: 0x040052AE RID: 21166
		private NKCUIHangarBuildSlot.OnShipInfo dOnShipInfo;

		// Token: 0x0200167D RID: 5757
		// (Invoke) Token: 0x0600B067 RID: 45159
		public delegate void OnShipInfo(int shipID);
	}
}
