using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000993 RID: 2451
	public class NKCUIForgeCraftMoldSlot : MonoBehaviour
	{
		// Token: 0x060065AF RID: 26031 RVA: 0x00205490 File Offset: 0x00203690
		public static NKCUIForgeCraftMoldSlot GetNewInstance(Transform parent, NKCUIForgeCraftMoldSlot.OnClickMoldSlot dOnClickMoldSlot = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_FACTORY", "NKM_UI_FACTORY_CRAFT_MOLD_SLOT", false, null);
			if (nkcassetInstanceData == null || nkcassetInstanceData.m_Instant == null)
			{
				Debug.LogError("NKCUIForgeCraftMoldSlot Prefab null!");
				return null;
			}
			NKCUIForgeCraftMoldSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIForgeCraftMoldSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIForgeCraftMoldSlot null!");
				return null;
			}
			component.m_dOnClickMoldSlot = dOnClickMoldSlot;
			component.transform.SetParent(parent, false);
			component.m_InstanceData = nkcassetInstanceData;
			component.m_AB_ICON_SLOT.Init();
			component.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON.PointerClick.RemoveAllListeners();
			component.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON.PointerClick.AddListener(new UnityAction(component.OnClickCraftMoldSlot));
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x0020554C File Offset: 0x0020374C
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060065B1 RID: 26033 RVA: 0x0020556B File Offset: 0x0020376B
		public void OnClickCraftMoldSlot()
		{
			if (this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON_text.color == NKCUtil.GetColor("#222222"))
			{
				return;
			}
			if (this.m_dOnClickMoldSlot != null)
			{
				this.m_dOnClickMoldSlot(this.m_cNKMMoldItemData);
			}
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x002055A3 File Offset: 0x002037A3
		private void Update()
		{
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x002055A8 File Offset: 0x002037A8
		public void SetData(int index)
		{
			if (!NKCUIForgeCraftMold.HasInstance)
			{
				return;
			}
			NKMMoldItemData sortedMoldItemData = NKCUIForgeCraftMold.Instance.GetSortedMoldItemData(index);
			this.m_cNKMMoldItemData = sortedMoldItemData;
			bool flag = true;
			if (sortedMoldItemData == null)
			{
				return;
			}
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(sortedMoldItemData.m_MoldID);
			if (itemMoldTempletByID == null)
			{
				Debug.LogError(string.Format("Mold templet not found : id {0}", sortedMoldItemData.m_MoldID));
				return;
			}
			this.m_AB_ICON_SLOT.SetData(NKCUISlot.SlotData.MakeMoldItemData(sortedMoldItemData.m_MoldID, sortedMoldItemData.m_Count), false, false, true, null);
			if (itemMoldTempletByID.IsEquipMold)
			{
				this.m_AB_ICON_SLOT.SetOnClickAction(new NKCUISlot.SlotClickType[]
				{
					NKCUISlot.SlotClickType.MoldList
				});
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BLACK, sortedMoldItemData.m_Count <= 0L && !itemMoldTempletByID.m_bPermanent);
			}
			else
			{
				this.m_AB_ICON_SLOT.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BLACK, false);
			}
			if (sortedMoldItemData.m_Count == 0L && !itemMoldTempletByID.m_bPermanent)
			{
				flag = false;
			}
			this.m_AB_ICON_SLOT.SetHaveCount(sortedMoldItemData.m_Count, itemMoldTempletByID.m_bPermanent);
			this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_NAME.text = itemMoldTempletByID.GetItemName();
			TimeSpan timeSpan = new TimeSpan(itemMoldTempletByID.m_Time / 60, itemMoldTempletByID.m_Time % 60, 0);
			this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_TIME.text = NKCUtilString.GetTimeSpanString(timeSpan);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				for (int i = 0; i < this.m_lstCostSlot.Count; i++)
				{
					if (i >= itemMoldTempletByID.m_MaterialList.Count)
					{
						this.m_lstCostSlot[i].SetData(0, 0, 0L, true, true, false);
					}
					else
					{
						long materialCount = this.GetMaterialCount(nkmuserData, itemMoldTempletByID.m_MaterialList[i].m_MaterialType, itemMoldTempletByID.m_MaterialList[i].m_MaterialID);
						int materialValue = itemMoldTempletByID.m_MaterialList[i].m_MaterialValue;
						bool bShowEvent = itemMoldTempletByID.m_MaterialList[i].m_MaterialID == 1 && NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT);
						if (itemMoldTempletByID.m_MaterialList[i].m_MaterialID == 1)
						{
							NKCCompanyBuff.SetDiscountOfCreditInCraft(nkmuserData.m_companyBuffDataList, ref materialValue);
						}
						this.m_lstCostSlot[i].SetData(itemMoldTempletByID.m_MaterialList[i].m_MaterialID, materialValue, materialCount, true, true, bShowEvent);
						if (materialCount < (long)itemMoldTempletByID.m_MaterialList[i].m_MaterialValue)
						{
							flag = false;
						}
					}
				}
			}
			if (flag)
			{
				this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON_img.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_POPUP_BLUEBUTTON", false);
				this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON_text.color = NKCUtil.GetColor("#FFFFFF");
				return;
			}
			this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON_img.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_POPUP_BUTTON_02", false);
			this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON_text.color = NKCUtil.GetColor("#222222");
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x0020587F File Offset: 0x00203A7F
		public long GetMaterialCount(NKMUserData cNKMUserData, NKM_REWARD_TYPE type, int id)
		{
			if (cNKMUserData == null)
			{
				return 0L;
			}
			if (type == NKM_REWARD_TYPE.RT_MISC)
			{
				return cNKMUserData.m_InventoryData.GetCountMiscItem(id);
			}
			Debug.Log("not supported material");
			return 0L;
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x002058A4 File Offset: 0x00203AA4
		public int MoldID
		{
			get
			{
				NKMMoldItemData cNKMMoldItemData = this.m_cNKMMoldItemData;
				if (cNKMMoldItemData == null)
				{
					return 0;
				}
				return cNKMMoldItemData.m_MoldID;
			}
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x002058B7 File Offset: 0x00203AB7
		public RectTransform GetButtonRect()
		{
			return this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON.GetComponent<RectTransform>();
		}

		// Token: 0x04005122 RID: 20770
		public NKCUISlot m_AB_ICON_SLOT;

		// Token: 0x04005123 RID: 20771
		public Text m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_NAME;

		// Token: 0x04005124 RID: 20772
		public Text m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_COUNT;

		// Token: 0x04005125 RID: 20773
		public Text m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_TIME;

		// Token: 0x04005126 RID: 20774
		public List<NKCUIItemCostSlot> m_lstCostSlot;

		// Token: 0x04005127 RID: 20775
		public GameObject m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BLACK;

		// Token: 0x04005128 RID: 20776
		public NKCUIComButton m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON;

		// Token: 0x04005129 RID: 20777
		public Image m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON_img;

		// Token: 0x0400512A RID: 20778
		public Text m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_BUTTON_text;

		// Token: 0x0400512B RID: 20779
		private NKCUIForgeCraftMoldSlot.OnClickMoldSlot m_dOnClickMoldSlot;

		// Token: 0x0400512C RID: 20780
		private NKMMoldItemData m_cNKMMoldItemData;

		// Token: 0x0400512D RID: 20781
		private const string IMPOSSIBLE_TEXT_COLOR = "#222222";

		// Token: 0x0400512E RID: 20782
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x02001661 RID: 5729
		// (Invoke) Token: 0x0600B016 RID: 45078
		public delegate void OnClickMoldSlot(NKMMoldItemData cNKMMoldItemData);
	}
}
