using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Module
{
	// Token: 0x02000B1C RID: 2844
	public class NKCUIModuleCollectionGroup : MonoBehaviour
	{
		// Token: 0x0600818A RID: 33162 RVA: 0x002BAA18 File Offset: 0x002B8C18
		private void Init()
		{
			if (this.m_slotRoot != null)
			{
				NKCUIModuleCollectionSlot[] componentsInChildren = this.m_slotRoot.GetComponentsInChildren<NKCUIModuleCollectionSlot>();
				int num = (componentsInChildren != null) ? componentsInChildren.Length : 0;
				for (int i = 0; i < num; i++)
				{
					componentsInChildren[i].Init();
					this.m_slotList.Add(componentsInChildren[i]);
				}
			}
		}

		// Token: 0x0600818B RID: 33163 RVA: 0x002BAA6C File Offset: 0x002B8C6C
		public void SetData(int collectionGroupId, List<ValueTuple<int, bool>> unitList)
		{
			if (this.m_slotRoot == null || unitList == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbGrade, this.GetGradeString((NKCUIModuleCollectionGroup.COLLECTION_GRADE)collectionGroupId));
			int count = this.m_slotList.Count;
			int count2 = unitList.Count;
			int num = count2 - count;
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_objSlotPrefab, this.m_slotRoot);
				if (!(gameObject == null))
				{
					NKCUIModuleCollectionSlot component = gameObject.GetComponent<NKCUIModuleCollectionSlot>();
					if (component != null)
					{
						component.Init();
						this.m_slotList.Add(component);
					}
				}
			}
			int num2 = 0;
			count = this.m_slotList.Count;
			for (int j = 0; j < count; j++)
			{
				if (j >= count2)
				{
					NKCUtil.SetGameobjectActive(this.m_slotList[j].gameObject, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_slotList[j].gameObject, true);
					NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeUnitData(unitList[j].Item1, 1, 0, 0);
					this.m_slotList[j].SetData(slotData, collectionGroupId == 100105);
					bool item = unitList[j].Item2;
					if (item)
					{
						num2++;
					}
					this.m_slotList[j].SetOwnState(item);
				}
			}
			NKCUtil.SetLabelText(this.m_lbCount, string.Format("{0}/{1}", num2, count2));
		}

		// Token: 0x0600818C RID: 33164 RVA: 0x002BABE8 File Offset: 0x002B8DE8
		public static NKCUIModuleCollectionGroup GetNewInstance(Transform parent, string bundleName, string assetName)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assetName, false, null);
			NKCUIModuleCollectionGroup nkcuimoduleCollectionGroup = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIModuleCollectionGroup>() : null;
			if (nkcuimoduleCollectionGroup == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIModuleCollectionGroup Prefab null!");
				return null;
			}
			nkcuimoduleCollectionGroup.m_InstanceData = nkcassetInstanceData;
			nkcuimoduleCollectionGroup.Init();
			if (parent != null)
			{
				nkcuimoduleCollectionGroup.transform.SetParent(parent);
			}
			nkcuimoduleCollectionGroup.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuimoduleCollectionGroup.gameObject.SetActive(false);
			return nkcuimoduleCollectionGroup;
		}

		// Token: 0x0600818D RID: 33165 RVA: 0x002BAC7A File Offset: 0x002B8E7A
		public void DestoryInstance()
		{
			List<NKCUIModuleCollectionSlot> slotList = this.m_slotList;
			if (slotList != null)
			{
				slotList.Clear();
			}
			this.m_slotList = null;
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600818E RID: 33166 RVA: 0x002BACB4 File Offset: 0x002B8EB4
		private string GetGradeString(NKCUIModuleCollectionGroup.COLLECTION_GRADE grade)
		{
			switch (grade)
			{
			case NKCUIModuleCollectionGroup.COLLECTION_GRADE.CG_N:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_N_FOR_EVENTDECK", false);
			case NKCUIModuleCollectionGroup.COLLECTION_GRADE.CG_R:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_R_FOR_EVENTDECK", false);
			case NKCUIModuleCollectionGroup.COLLECTION_GRADE.CG_SR:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_SR_FOR_EVENTDECK", false);
			case NKCUIModuleCollectionGroup.COLLECTION_GRADE.CG_SSR:
				return NKCStringTable.GetString("SI_DP_UNIT_GRADE_STRING_NUG_SSR_FOR_EVENTDECK", false);
			case NKCUIModuleCollectionGroup.COLLECTION_GRADE.CG_AWAKEN:
				return NKCStringTable.GetString("SI_PF_FILTER_UNIT_TYPE_AWAKEN", false);
			default:
				return "";
			}
		}

		// Token: 0x0600818F RID: 33167 RVA: 0x002BAD24 File Offset: 0x002B8F24
		private void OnClickUnitSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
		}

		// Token: 0x04006DBA RID: 28090
		public Transform m_slotRoot;

		// Token: 0x04006DBB RID: 28091
		public Text m_lbGrade;

		// Token: 0x04006DBC RID: 28092
		public Text m_lbCount;

		// Token: 0x04006DBD RID: 28093
		public GameObject m_objSlotPrefab;

		// Token: 0x04006DBE RID: 28094
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04006DBF RID: 28095
		private List<NKCUIModuleCollectionSlot> m_slotList = new List<NKCUIModuleCollectionSlot>();

		// Token: 0x020018B7 RID: 6327
		public enum COLLECTION_GRADE
		{
			// Token: 0x0400A9A8 RID: 43432
			CG_N = 100101,
			// Token: 0x0400A9A9 RID: 43433
			CG_R,
			// Token: 0x0400A9AA RID: 43434
			CG_SR,
			// Token: 0x0400A9AB RID: 43435
			CG_SSR,
			// Token: 0x0400A9AC RID: 43436
			CG_AWAKEN
		}
	}
}
