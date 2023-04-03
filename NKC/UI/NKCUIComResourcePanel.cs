using System;
using ClientPacket.Office;
using NKC.UI.Tooltip;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000944 RID: 2372
	public class NKCUIComResourcePanel : MonoBehaviour, NKCUIManager.IInventoryChangeObserver
	{
		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x001D69B4 File Offset: 0x001D4BB4
		// (set) Token: 0x06005EB6 RID: 24246 RVA: 0x001D69BC File Offset: 0x001D4BBC
		public int Handle { get; set; }

		// Token: 0x06005EB7 RID: 24247 RVA: 0x001D69C5 File Offset: 0x001D4BC5
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			this.RefreshCount(NKCUIComResourcePanel.ResourceType.MISC, itemData.ItemID);
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x001D69D4 File Offset: 0x001D4BD4
		public void OnInteriorInventoryUpdate(NKMInteriorData interiorData, bool bAdded)
		{
			this.RefreshCount(NKCUIComResourcePanel.ResourceType.INTERIOR, interiorData.itemId);
		}

		// Token: 0x06005EB9 RID: 24249 RVA: 0x001D69E3 File Offset: 0x001D4BE3
		public void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipData)
		{
			this.RefreshCount(NKCUIComResourcePanel.ResourceType.EQUIP, equipData.m_ItemEquipID);
		}

		// Token: 0x06005EBA RID: 24250 RVA: 0x001D69F4 File Offset: 0x001D4BF4
		private void Awake()
		{
			if (this.resourceInfo == null)
			{
				return;
			}
			int num = this.resourceInfo.Length;
			for (int i = 0; i < num; i++)
			{
				switch (this.resourceInfo[i].resourceType)
				{
				case NKCUIComResourcePanel.ResourceType.MISC:
					this.resourceInfo[i].onSetSprite = new NKCUIComResourcePanel.ResourceInfo.OnSetSprite(this.SetSpriteMiscType);
					this.resourceInfo[i].onUpdateCount = new NKCUIComResourcePanel.ResourceInfo.OnUpdateCount(this.UpdateCountMiscType);
					break;
				case NKCUIComResourcePanel.ResourceType.INTERIOR:
					this.resourceInfo[i].onSetSprite = new NKCUIComResourcePanel.ResourceInfo.OnSetSprite(this.SetSpriteMiscType);
					this.resourceInfo[i].onUpdateCount = new NKCUIComResourcePanel.ResourceInfo.OnUpdateCount(this.UpdateCountInteriorType);
					break;
				case NKCUIComResourcePanel.ResourceType.EQUIP:
					this.resourceInfo[i].onSetSprite = new NKCUIComResourcePanel.ResourceInfo.OnSetSprite(this.SetSpriteEquipType);
					this.resourceInfo[i].onUpdateCount = new NKCUIComResourcePanel.ResourceInfo.OnUpdateCount(this.UpdateCountEquipType);
					break;
				}
				this.resourceInfo[i].SetSprite();
				this.resourceInfo[i].UpdateCount();
				NKCUIComStateButton csbtnButton = this.resourceInfo[i].csbtnButton;
				if (csbtnButton != null)
				{
					csbtnButton.PointerDown.RemoveAllListeners();
				}
				if (this.showToolTip)
				{
					int index = i;
					NKCUIComStateButton csbtnButton2 = this.resourceInfo[i].csbtnButton;
					if (csbtnButton2 != null)
					{
						csbtnButton2.PointerDown.AddListener(delegate(PointerEventData eventData)
						{
							this.OnSlotDown(this.resourceInfo[index].slotData, eventData);
						});
					}
				}
			}
		}

		// Token: 0x06005EBB RID: 24251 RVA: 0x001D6B8C File Offset: 0x001D4D8C
		private void SetSpriteMiscType(int itemId, ref Image targetUI)
		{
			Sprite orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(NKMItemMiscTemplet.Find(itemId));
			NKCUtil.SetImageSprite(targetUI, orLoadMiscItemIcon, false);
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x001D6BB0 File Offset: 0x001D4DB0
		private void SetSpriteEquipType(int itemId, ref Image targetUI)
		{
			Sprite orLoadEquipIcon = NKCResourceUtility.GetOrLoadEquipIcon(NKMItemManager.GetEquipTemplet(itemId));
			NKCUtil.SetImageSprite(targetUI, orLoadEquipIcon, false);
		}

		// Token: 0x06005EBD RID: 24253 RVA: 0x001D6BD4 File Offset: 0x001D4DD4
		private void UpdateCountMiscType(int itemId, ref Text targetUI, ref NKCUISlot.SlotData slotData)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(itemId);
			NKCUtil.SetLabelText(targetUI, countMiscItem.ToString());
			if (slotData == null)
			{
				slotData = NKCUISlot.SlotData.MakeMiscItemData(itemId, countMiscItem, 0);
			}
		}

		// Token: 0x06005EBE RID: 24254 RVA: 0x001D6C14 File Offset: 0x001D4E14
		private void UpdateCountInteriorType(int itemId, ref Text targetUI, ref NKCUISlot.SlotData slotData)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			long interiorCount = nkmuserData.OfficeData.GetInteriorCount(itemId);
			NKCUtil.SetLabelText(targetUI, interiorCount.ToString());
			if (slotData == null)
			{
				slotData = NKCUISlot.SlotData.MakeMiscItemData(itemId, interiorCount, 0);
			}
		}

		// Token: 0x06005EBF RID: 24255 RVA: 0x001D6C54 File Offset: 0x001D4E54
		private void UpdateCountEquipType(int itemId, ref Text targetUI, ref NKCUISlot.SlotData slotData)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			long num = (long)nkmuserData.m_InventoryData.GetSameKindEquipCount(itemId, 0);
			NKCUtil.SetLabelText(targetUI, num.ToString());
			if (slotData == null)
			{
				slotData = NKCUISlot.SlotData.MakeEquipData(itemId, 1, 0);
			}
		}

		// Token: 0x06005EC0 RID: 24256 RVA: 0x001D6C98 File Offset: 0x001D4E98
		private void RefreshCount(NKCUIComResourcePanel.ResourceType resourceType, int itemId)
		{
			if (this.resourceInfo == null)
			{
				return;
			}
			int num = this.resourceInfo.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.resourceInfo[i].resourceType == resourceType && this.resourceInfo[i].itemId == itemId)
				{
					this.resourceInfo[i].UpdateCount();
				}
			}
		}

		// Token: 0x06005EC1 RID: 24257 RVA: 0x001D6CFC File Offset: 0x001D4EFC
		private void RefreshTotalCount()
		{
			if (this.resourceInfo == null)
			{
				return;
			}
			int num = this.resourceInfo.Length;
			for (int i = 0; i < num; i++)
			{
				this.resourceInfo[i].UpdateCount();
			}
		}

		// Token: 0x06005EC2 RID: 24258 RVA: 0x001D6D38 File Offset: 0x001D4F38
		private void OnSlotDown(NKCUISlot.SlotData slotData, PointerEventData eventData)
		{
			NKCUITooltip.Instance.Open(slotData, new Vector2?(eventData.position));
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x001D6D50 File Offset: 0x001D4F50
		private void OnEnable()
		{
			NKCUIManager.RegisterInventoryObserver(this);
			this.RefreshTotalCount();
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x001D6D5E File Offset: 0x001D4F5E
		private void OnDisable()
		{
			NKCUIManager.UnregisterInventoryObserver(this);
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x001D6D68 File Offset: 0x001D4F68
		private void OnDestroy()
		{
			int num = this.resourceInfo.Length;
			for (int i = 0; i < num; i++)
			{
				this.resourceInfo[i].Release();
			}
		}

		// Token: 0x04004ADD RID: 19165
		public NKCUIComResourcePanel.ResourceInfo[] resourceInfo;

		// Token: 0x04004ADE RID: 19166
		public bool showToolTip;

		// Token: 0x020015CE RID: 5582
		public enum ResourceType
		{
			// Token: 0x0400A28C RID: 41612
			MISC,
			// Token: 0x0400A28D RID: 41613
			INTERIOR,
			// Token: 0x0400A28E RID: 41614
			EQUIP
		}

		// Token: 0x020015CF RID: 5583
		[Serializable]
		public struct ResourceInfo
		{
			// Token: 0x0600AE59 RID: 44633 RVA: 0x0035B074 File Offset: 0x00359274
			public void SetSprite()
			{
				if (this.onSetSprite != null)
				{
					this.onSetSprite(this.itemId, ref this.iconImageUI);
				}
			}

			// Token: 0x0600AE5A RID: 44634 RVA: 0x0035B095 File Offset: 0x00359295
			public void UpdateCount()
			{
				if (this.onUpdateCount != null)
				{
					this.onUpdateCount(this.itemId, ref this.countTextUI, ref this.slotData);
				}
			}

			// Token: 0x0600AE5B RID: 44635 RVA: 0x0035B0BC File Offset: 0x003592BC
			public void Release()
			{
				this.iconImageUI = null;
				this.countTextUI = null;
				this.csbtnButton = null;
				this.slotData = null;
				this.onSetSprite = null;
				this.onUpdateCount = null;
			}

			// Token: 0x0400A28F RID: 41615
			public NKCUIComResourcePanel.ResourceType resourceType;

			// Token: 0x0400A290 RID: 41616
			public int itemId;

			// Token: 0x0400A291 RID: 41617
			public Image iconImageUI;

			// Token: 0x0400A292 RID: 41618
			public Text countTextUI;

			// Token: 0x0400A293 RID: 41619
			public NKCUIComStateButton csbtnButton;

			// Token: 0x0400A294 RID: 41620
			public NKCUISlot.SlotData slotData;

			// Token: 0x0400A295 RID: 41621
			public NKCUIComResourcePanel.ResourceInfo.OnSetSprite onSetSprite;

			// Token: 0x0400A296 RID: 41622
			public NKCUIComResourcePanel.ResourceInfo.OnUpdateCount onUpdateCount;

			// Token: 0x02001A70 RID: 6768
			// (Invoke) Token: 0x0600BBED RID: 48109
			public delegate void OnSetSprite(int itemId, ref Image targetUI);

			// Token: 0x02001A71 RID: 6769
			// (Invoke) Token: 0x0600BBF1 RID: 48113
			public delegate void OnUpdateCount(int itemId, ref Text targetUI, ref NKCUISlot.SlotData slotData);
		}
	}
}
