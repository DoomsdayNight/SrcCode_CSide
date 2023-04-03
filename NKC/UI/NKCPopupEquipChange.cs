using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A44 RID: 2628
	public class NKCPopupEquipChange : NKCUIBase
	{
		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x0600735C RID: 29532 RVA: 0x00265C14 File Offset: 0x00263E14
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x0600735D RID: 29533 RVA: 0x00265C17 File Offset: 0x00263E17
		public override string MenuName
		{
			get
			{
				return "장비 변경 확인";
			}
		}

		// Token: 0x0600735E RID: 29534 RVA: 0x00265C20 File Offset: 0x00263E20
		public void InitUI()
		{
			this.m_cbtnOK.PointerClick.AddListener(new UnityAction(this.OnOkButton));
			NKCUtil.SetHotkey(this.m_cbtnOK, HotkeyEventType.Confirm);
			this.m_cbtnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_lstStatSlots = new List<NKCPopupEquipChangeStatSlot>();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600735F RID: 29535 RVA: 0x00265C9C File Offset: 0x00263E9C
		public void Open(NKMEquipItemData beforeNKMEquipItemData, NKMEquipItemData afterNKMEquipItemData, NKCPopupEquipChange.OnEquipChangePopupOK onOK, bool bShowFierceInfo = false)
		{
			if (beforeNKMEquipItemData == null || afterNKMEquipItemData == null)
			{
				return;
			}
			this.m_slotBefore.SetData(beforeNKMEquipItemData, bShowFierceInfo, false);
			this.m_slotAfter.SetData(afterNKMEquipItemData, bShowFierceInfo, false);
			this.SetChangedStat(beforeNKMEquipItemData, afterNKMEquipItemData);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.dOnOK = onOK;
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06007360 RID: 29536 RVA: 0x00265CFC File Offset: 0x00263EFC
		private void SetChangedStat(NKMEquipItemData beforeNKMEquipItemData, NKMEquipItemData afterNKMEquipItemData)
		{
			Dictionary<NKM_STAT_TYPE, NKCPopupEquipChange.ItemStatCompare> dictionary = new Dictionary<NKM_STAT_TYPE, NKCPopupEquipChange.ItemStatCompare>();
			if (afterNKMEquipItemData.potentialOption != null && afterNKMEquipItemData.potentialOption.sockets != null && afterNKMEquipItemData.potentialOption.sockets.Length != 0)
			{
				NKMPotentialOption.SocketData[] sockets = afterNKMEquipItemData.potentialOption.sockets;
				for (int i = 0; i < sockets.Length; i++)
				{
					if (sockets.Length > i && sockets[i] != null)
					{
						this.SetStatCompare(ref dictionary, afterNKMEquipItemData.potentialOption.statType, sockets[i].statFactor, sockets[i].statValue, true);
					}
				}
			}
			foreach (EQUIP_ITEM_STAT equip_ITEM_STAT in afterNKMEquipItemData.m_Stat)
			{
				float fValue = equip_ITEM_STAT.stat_value + (float)afterNKMEquipItemData.m_EnchantLevel * equip_ITEM_STAT.stat_level_value;
				this.SetStatCompare(ref dictionary, equip_ITEM_STAT.type, equip_ITEM_STAT.stat_factor, fValue, true);
			}
			if (beforeNKMEquipItemData.potentialOption != null && beforeNKMEquipItemData.potentialOption.sockets != null && beforeNKMEquipItemData.potentialOption.sockets.Length != 0)
			{
				NKMPotentialOption.SocketData[] sockets2 = beforeNKMEquipItemData.potentialOption.sockets;
				for (int j = 0; j < sockets2.Length; j++)
				{
					if (sockets2.Length > j && sockets2[j] != null)
					{
						this.SetStatCompare(ref dictionary, beforeNKMEquipItemData.potentialOption.statType, sockets2[j].statFactor * -1f, sockets2[j].statValue * -1f, false);
					}
				}
			}
			foreach (EQUIP_ITEM_STAT equip_ITEM_STAT2 in beforeNKMEquipItemData.m_Stat)
			{
				float num = equip_ITEM_STAT2.stat_value + (float)beforeNKMEquipItemData.m_EnchantLevel * equip_ITEM_STAT2.stat_level_value;
				this.SetStatCompare(ref dictionary, equip_ITEM_STAT2.type, equip_ITEM_STAT2.stat_factor * -1f, num * -1f, false);
			}
			foreach (KeyValuePair<NKM_STAT_TYPE, NKCPopupEquipChange.ItemStatCompare> keyValuePair in dictionary)
			{
				string statShortName = NKCUtilString.GetStatShortName(keyValuePair.Key, keyValuePair.Value.statValue);
				string text;
				if (NKMUnitStatManager.IsPercentStat(keyValuePair.Key))
				{
					text = NKCUtilString.GetStatShortString("{1:P1}", keyValuePair.Key, keyValuePair.Value.statValue);
				}
				else
				{
					text = NKCUtilString.GetStatShortString("{1:0}", keyValuePair.Key, keyValuePair.Value.statValue);
				}
				float num2 = (NKCUtilString.IsNameReversedIfNegative(keyValuePair.Key) && keyValuePair.Value.statValue < 0f) ? (-keyValuePair.Value.changedStatValue) : keyValuePair.Value.changedStatValue;
				string changedValueColor = "";
				string text2;
				if (NKMUnitStatManager.IsPercentStat(keyValuePair.Key))
				{
					text2 = string.Format("{0:+0.#%;-0.#%;''}", Math.Round(new decimal(num2 * 1000f)) / 1000m);
				}
				else
				{
					text2 = string.Format("{0:+#;-#;''}", num2);
				}
				if (num2 > 0f)
				{
					text2 = string.Format("<size=20>{0}</size>{1}", "▲", text2);
					changedValueColor = "#A3FF66";
				}
				else if (num2 < 0f)
				{
					text2 = string.Format("<size=20>{0}</size>{1}", "▼", text2);
					changedValueColor = "#FF3D40";
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.AddChangedStatSlot(statShortName, text, text2, changedValueColor);
				}
				if (keyValuePair.Value.statFactor != 0f || keyValuePair.Value.changedStatFactor != 0f)
				{
					bool flag = NKCUtilString.IsNameReversedIfNegative(keyValuePair.Key) && keyValuePair.Value.statFactor < 0f;
					float num3 = flag ? (-keyValuePair.Value.statFactor) : keyValuePair.Value.statFactor;
					float num4 = flag ? (-keyValuePair.Value.changedStatFactor) : keyValuePair.Value.changedStatFactor;
					decimal d = new decimal((num3 != 0f) ? num3 : num4);
					text = string.Format("{0:P1}", Math.Round(d * 1000m) / 1000m);
					text2 = string.Format("{0:+0.#%;-0.#%;''}", text);
					if (d < 0m)
					{
						text = string.Format("{0:P1}", Math.Round(0.0) / 1000.0);
					}
					if (num4 > 0f)
					{
						text2 = string.Format("<size=20>{0}</size>{1}", "▲", text2);
						changedValueColor = "#A3FF66";
					}
					else if (num4 < 0f)
					{
						text2 = string.Format("<size=20>{0}</size>{1}", "▼", text2);
						changedValueColor = "#FF3D40";
					}
					if (!string.IsNullOrEmpty(text2))
					{
						this.AddChangedStatSlot(statShortName, text, text2, changedValueColor);
					}
				}
			}
		}

		// Token: 0x06007361 RID: 29537 RVA: 0x00266238 File Offset: 0x00264438
		private void SetStatCompare(ref Dictionary<NKM_STAT_TYPE, NKCPopupEquipChange.ItemStatCompare> dicItemStatCompare, NKM_STAT_TYPE type, float fFactor, float fValue, bool bBeforeItem = false)
		{
			bool flag = fFactor != 0f;
			if (!dicItemStatCompare.ContainsKey(type))
			{
				NKCPopupEquipChange.ItemStatCompare itemStatCompare = new NKCPopupEquipChange.ItemStatCompare();
				if (flag)
				{
					itemStatCompare.statFactor = (bBeforeItem ? fFactor : 0f);
					itemStatCompare.changedStatFactor = fFactor;
				}
				else
				{
					itemStatCompare.statValue = (bBeforeItem ? fValue : 0f);
					itemStatCompare.changedStatValue = fValue;
				}
				dicItemStatCompare.Add(type, itemStatCompare);
				return;
			}
			if (flag)
			{
				if (bBeforeItem)
				{
					dicItemStatCompare[type].statFactor += fFactor;
				}
				dicItemStatCompare[type].changedStatFactor += fFactor;
				return;
			}
			if (bBeforeItem)
			{
				dicItemStatCompare[type].statValue += fValue;
			}
			dicItemStatCompare[type].changedStatValue += fValue;
		}

		// Token: 0x06007362 RID: 29538 RVA: 0x00266308 File Offset: 0x00264508
		private void AddChangedStatSlot(string statShortName, string statValue, string changedValueStr, string changedValueColor)
		{
			NKCPopupEquipChangeStatSlot nkcpopupEquipChangeStatSlot = UnityEngine.Object.Instantiate<NKCPopupEquipChangeStatSlot>(this.m_pfbStatSlot, this.m_ScrollRect.content);
			if (null != nkcpopupEquipChangeStatSlot)
			{
				nkcpopupEquipChangeStatSlot.SetData(statShortName, statValue, changedValueStr, changedValueColor);
				NKCUtil.SetGameobjectActive(nkcpopupEquipChangeStatSlot, true);
				this.m_lstStatSlots.Add(nkcpopupEquipChangeStatSlot);
			}
		}

		// Token: 0x06007363 RID: 29539 RVA: 0x00266353 File Offset: 0x00264553
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007364 RID: 29540 RVA: 0x00266368 File Offset: 0x00264568
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			foreach (NKCPopupEquipChangeStatSlot nkcpopupEquipChangeStatSlot in this.m_lstStatSlots)
			{
				UnityEngine.Object.Destroy(nkcpopupEquipChangeStatSlot.gameObject);
			}
			this.m_lstStatSlots.Clear();
		}

		// Token: 0x06007365 RID: 29541 RVA: 0x002663D4 File Offset: 0x002645D4
		private void OnOkButton()
		{
			if (this.dOnOK != null)
			{
				this.dOnOK();
			}
		}

		// Token: 0x04005F56 RID: 24406
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_UNIT_CHANGE_POPUP";

		// Token: 0x04005F57 RID: 24407
		public const string UI_ASSET_NAME = "NKM_UI_EQUIP_CHANGE_POPUP";

		// Token: 0x04005F58 RID: 24408
		public NKCUIComButton m_cbtnOK;

		// Token: 0x04005F59 RID: 24409
		public NKCUIComButton m_cbtnCancel;

		// Token: 0x04005F5A RID: 24410
		public NKCUIInvenEquipSlot m_slotBefore;

		// Token: 0x04005F5B RID: 24411
		public NKCUIInvenEquipSlot m_slotAfter;

		// Token: 0x04005F5C RID: 24412
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005F5D RID: 24413
		private NKCPopupEquipChange.OnEquipChangePopupOK dOnOK;

		// Token: 0x04005F5E RID: 24414
		public ScrollRect m_ScrollRect;

		// Token: 0x04005F5F RID: 24415
		public NKCPopupEquipChangeStatSlot m_pfbStatSlot;

		// Token: 0x04005F60 RID: 24416
		private List<NKCPopupEquipChangeStatSlot> m_lstStatSlots;

		// Token: 0x0200178E RID: 6030
		// (Invoke) Token: 0x0600B39F RID: 45983
		public delegate void OnEquipChangePopupOK();

		// Token: 0x0200178F RID: 6031
		public class ItemStatCompare
		{
			// Token: 0x0400A710 RID: 42768
			public float statValue;

			// Token: 0x0400A711 RID: 42769
			public float changedStatValue;

			// Token: 0x0400A712 RID: 42770
			public float statFactor;

			// Token: 0x0400A713 RID: 42771
			public float changedStatFactor;
		}
	}
}
