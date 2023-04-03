using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000928 RID: 2344
	public class NKCUIComBattleCondition : MonoBehaviour
	{
		// Token: 0x06005DE2 RID: 24034 RVA: 0x001CFAD4 File Offset: 0x001CDCD4
		public static NKCUIComBattleCondition GetNewInstance(string bundleName, string assetName, Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assetName, false, null);
			NKCUIComBattleCondition nkcuicomBattleCondition = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIComBattleCondition>() : null;
			if (nkcuicomBattleCondition == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIComBattleCondition Prefab null!");
				return null;
			}
			nkcuicomBattleCondition.m_InstanceData = nkcassetInstanceData;
			nkcuicomBattleCondition.Init(null);
			if (parent != null)
			{
				nkcuicomBattleCondition.transform.SetParent(parent);
			}
			nkcuicomBattleCondition.GetComponent<RectTransform>().localScale = Vector3.one;
			nkcuicomBattleCondition.gameObject.SetActive(false);
			return nkcuicomBattleCondition;
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x001CFB58 File Offset: 0x001CDD58
		public void Init(NKCUIComBattleCondition.OnDownButton onDownButton = null)
		{
			this.m_battleConditionStrId = null;
			this.m_dOnDownButton = onDownButton;
			if (this.m_csbtnButton != null)
			{
				this.m_csbtnButton.PointerDown.RemoveAllListeners();
				this.m_csbtnButton.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnButtonDown));
			}
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x001CFBAD File Offset: 0x001CDDAD
		public void SetButtonDownFunc(NKCUIComBattleCondition.OnDownButton onDownButton)
		{
			this.m_dOnDownButton = onDownButton;
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x001CFBB8 File Offset: 0x001CDDB8
		public void SetData(string battleConditionId)
		{
			this.m_battleConditionStrId = battleConditionId;
			NKMBattleConditionTemplet templetByStrID = NKMBattleConditionManager.GetTempletByStrID(battleConditionId);
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCUtil.GetSpriteBattleConditionICon(templetByStrID), true);
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x001CFBE8 File Offset: 0x001CDDE8
		public void SetData(int battleConditionId)
		{
			NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(battleConditionId);
			this.m_battleConditionStrId = templetByID.BattleCondStrID;
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCUtil.GetSpriteBattleConditionICon(templetByID), true);
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x001CFC1A File Offset: 0x001CDE1A
		private void OnButtonDown(PointerEventData pointEventData)
		{
			if (this.m_dOnDownButton != null)
			{
				this.m_dOnDownButton(this.m_battleConditionStrId, pointEventData.position);
			}
		}

		// Token: 0x06005DE8 RID: 24040 RVA: 0x001CFC40 File Offset: 0x001CDE40
		private void OnDestroy()
		{
			this.m_battleConditionStrId = null;
			this.m_dOnDownButton = null;
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
		}

		// Token: 0x04004A1D RID: 18973
		public Image m_imgIcon;

		// Token: 0x04004A1E RID: 18974
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04004A1F RID: 18975
		private string m_battleConditionStrId;

		// Token: 0x04004A20 RID: 18976
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04004A21 RID: 18977
		private NKCUIComBattleCondition.OnDownButton m_dOnDownButton;

		// Token: 0x020015B8 RID: 5560
		// (Invoke) Token: 0x0600AE06 RID: 44550
		public delegate void OnDownButton(string battleCondId, Vector3 position);
	}
}
