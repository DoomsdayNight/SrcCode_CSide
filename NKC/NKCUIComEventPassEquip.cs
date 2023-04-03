using System;
using NKM;
using NKM.EventPass;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000759 RID: 1881
	public class NKCUIComEventPassEquip : MonoBehaviour
	{
		// Token: 0x06004B23 RID: 19235 RVA: 0x00168174 File Offset: 0x00166374
		public static NKCUIComEventPassEquip GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_EVENT_PASS", "NKM_UI_EVENT_PASS_EQUIP", false, null);
			NKCUIComEventPassEquip component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIComEventPassEquip>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKM_UI_EVENT_PASS_EQUIP Prefab null!");
				return null;
			}
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x00168218 File Offset: 0x00166418
		public void SetData(NKMEventPassTemplet eventPassTemplet)
		{
			if (eventPassTemplet == null)
			{
				return;
			}
			switch (eventPassTemplet.EventPassMainRewardType)
			{
			case NKM_REWARD_TYPE.RT_MISC:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(eventPassTemplet.EventPassMainReward);
				NKCUtil.SetImageSprite(this.m_imgEquipIcon, NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByID), false);
				NKCUtil.SetImageSprite(this.m_imgEquipType, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_ICON", "NKM_UI_COMMON_UNIT_TYPE_ETC", false), false);
				return;
			}
			case NKM_REWARD_TYPE.RT_USER_EXP:
				break;
			case NKM_REWARD_TYPE.RT_EQUIP:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(eventPassTemplet.EventPassMainReward);
				NKCUtil.SetImageSprite(this.m_imgEquipIcon, NKCResourceUtility.GetOrLoadEquipIcon(equipTemplet), false);
				NKCUtil.SetImageSprite(this.m_imgEquipType, NKCResourceUtility.GetOrLoadUnitStyleIcon(equipTemplet.m_EquipUnitStyleType, false), false);
				return;
			}
			case NKM_REWARD_TYPE.RT_MOLD:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(eventPassTemplet.EventPassMainReward);
				NKCUtil.SetImageSprite(this.m_imgEquipIcon, NKCResourceUtility.GetOrLoadMoldIcon(itemMoldTempletByID), false);
				NKCUtil.SetImageSprite(this.m_imgEquipType, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_ICON", "NKM_UI_COMMON_UNIT_TYPE_ETC", false), false);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x001682F5 File Offset: 0x001664F5
		private void OnDestroy()
		{
			this.m_imgEquipIcon = null;
			this.m_imgEquipType = null;
			NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceData);
		}

		// Token: 0x040039CC RID: 14796
		public Image m_imgEquipIcon;

		// Token: 0x040039CD RID: 14797
		public Image m_imgEquipType;

		// Token: 0x040039CE RID: 14798
		private NKCAssetInstanceData m_NKCAssetInstanceData;
	}
}
