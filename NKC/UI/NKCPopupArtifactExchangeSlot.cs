using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A33 RID: 2611
	public class NKCPopupArtifactExchangeSlot : MonoBehaviour
	{
		// Token: 0x0600725B RID: 29275 RVA: 0x002601E8 File Offset: 0x0025E3E8
		public static NKCPopupArtifactExchangeSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NKM_UI_DIVE_ARTIFACT_EXCHANGE_POPUP_SLOT", false, null);
			NKCPopupArtifactExchangeSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCPopupArtifactExchangeSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCPopupArtifactExchangeSlot Prefab null!");
				return null;
			}
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			if (component.m_NKCUISlot != null)
			{
				component.m_NKCUISlot.Init();
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x0600725C RID: 29276 RVA: 0x002602A8 File Offset: 0x0025E4A8
		public void SetData(int artifactID)
		{
			this.m_NKCUISlot.SetDiveArtifactData(NKCUISlot.SlotData.MakeDiveArtifactData(artifactID, 1), false, false, true, null);
			this.m_NKCUISlot.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
			this.m_imgGetMiscItemIcon.sprite = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(NKMCommonConst.DiveArtifactReturnItemId);
			NKMDiveArtifactTemplet.Find(artifactID);
		}

		// Token: 0x0600725D RID: 29277 RVA: 0x002602F8 File Offset: 0x0025E4F8
		private void OnDestroy()
		{
			if (this.m_NKCAssetInstanceData != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceData);
			}
		}

		// Token: 0x04005E42 RID: 24130
		public NKCUISlot m_NKCUISlot;

		// Token: 0x04005E43 RID: 24131
		public Image m_imgGetMiscItemIcon;

		// Token: 0x04005E44 RID: 24132
		public Text m_lbGetMiscItemCount;

		// Token: 0x04005E45 RID: 24133
		private NKCAssetInstanceData m_NKCAssetInstanceData;
	}
}
