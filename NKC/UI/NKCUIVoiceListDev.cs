using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A00 RID: 2560
	public class NKCUIVoiceListDev : MonoBehaviour
	{
		// Token: 0x06006FAC RID: 28588 RVA: 0x0024EC6E File Offset: 0x0024CE6E
		public static NKCUIVoiceListDev Init(GameObject voiceListUI)
		{
			NKCUIVoiceListDev component = voiceListUI.GetComponent<NKCUIVoiceListDev>();
			component.slotPrefab.gameObject.SetActive(false);
			component.itemPrefab.SetActive(false);
			component.InitItem();
			component.InitUnitList();
			return component;
		}

		// Token: 0x06006FAD RID: 28589 RVA: 0x0024EC9F File Offset: 0x0024CE9F
		public void Open()
		{
			NKCSoundManager.StopMusic();
		}

		// Token: 0x06006FAE RID: 28590 RVA: 0x0024ECA6 File Offset: 0x0024CEA6
		public void Close()
		{
		}

		// Token: 0x06006FAF RID: 28591 RVA: 0x0024ECA8 File Offset: 0x0024CEA8
		private void InitUnitList()
		{
			List<NKMUnitTempletBase> listNKMUnitTempletBaseForUnit = NKMUnitTempletBase.Get_listNKMUnitTempletBaseForUnit();
			this.RemoveNoNeedUnitTypeForSingleMode(listNKMUnitTempletBaseForUnit);
			int count = listNKMUnitTempletBaseForUnit.Count;
			for (int i = 0; i < count; i++)
			{
				NKCDeckViewUnitSelectListSlot nkcdeckViewUnitSelectListSlot = UnityEngine.Object.Instantiate<NKCDeckViewUnitSelectListSlot>(this.slotPrefab);
				nkcdeckViewUnitSelectListSlot.Init(false);
				nkcdeckViewUnitSelectListSlot.gameObject.SetActive(true);
				nkcdeckViewUnitSelectListSlot.transform.localScale = Vector3.one;
				nkcdeckViewUnitSelectListSlot.transform.SetParent(this.trUnitContent);
				nkcdeckViewUnitSelectListSlot.SetData(listNKMUnitTempletBaseForUnit[i], 1, 0, false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectThisSlot));
				this.dicUnit.Add(listNKMUnitTempletBaseForUnit[i].m_UnitID, nkcdeckViewUnitSelectListSlot);
			}
		}

		// Token: 0x06006FB0 RID: 28592 RVA: 0x0024ED4C File Offset: 0x0024CF4C
		private void InitItem()
		{
			List<NKCVoiceTemplet> templets = NKCUIVoiceManager.GetTemplets();
			int count = templets.Count;
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemPrefab);
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.trVoiceContent);
				gameObject.transform.Find("Type/text").GetComponent<Text>().text = templets[i].Type.ToString().Substring(3);
				Text component = gameObject.transform.Find("FileName/text").GetComponent<Text>();
				component.text = "";
				NKCUIComStateButton componentInChildren = gameObject.GetComponentInChildren<NKCUIComStateButton>();
				NKCUIVoiceListDev.VoiceItem item = new NKCUIVoiceListDev.VoiceItem
				{
					Index = i,
					FileName = component,
					Button = componentInChildren
				};
				componentInChildren.PointerClick.AddListener(delegate()
				{
					this.OnClickPlay(item);
				});
				componentInChildren.gameObject.SetActive(false);
				this.listVoiceItem.Add(item);
			}
		}

		// Token: 0x06006FB1 RID: 28593 RVA: 0x0024EE69 File Offset: 0x0024D069
		private void OnSelectThisSlot(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			Debug.Log("unit : " + unitTempletBase.m_UnitStrID);
			this.currentUnitStrID = unitTempletBase.m_UnitStrID;
			this.SelectUnit(unitTempletBase.m_UnitID);
			this.SetVoiceList(this.currentUnitStrID);
		}

		// Token: 0x06006FB2 RID: 28594 RVA: 0x0024EEA4 File Offset: 0x0024D0A4
		private void SetVoiceList(string unitStrID)
		{
			List<NKCVoiceTemplet> templets = NKCUIVoiceManager.GetTemplets();
			for (int i = 0; i < this.listVoiceItem.Count; i++)
			{
				NKCUIVoiceListDev.VoiceItem voiceItem = this.listVoiceItem[i];
				NKCVoiceTemplet nkcvoiceTemplet = templets[voiceItem.Index];
				if (NKCUIVoiceManager.CheckAsset(unitStrID, 0, nkcvoiceTemplet.FileName, VOICE_BUNDLE.UNIT))
				{
					voiceItem.FileName.text = nkcvoiceTemplet.FileName;
					voiceItem.Button.gameObject.SetActive(true);
				}
				else
				{
					voiceItem.FileName.text = "없음";
					voiceItem.Button.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06006FB3 RID: 28595 RVA: 0x0024EF40 File Offset: 0x0024D140
		private void OnClickPlay(NKCUIVoiceListDev.VoiceItem item)
		{
			if (string.IsNullOrEmpty(this.currentUnitStrID))
			{
				return;
			}
			NKCVoiceTemplet nkcvoiceTemplet = NKCUIVoiceManager.GetTemplets()[item.Index];
			NKCUIVoiceManager.PlayOnUI(this.currentUnitStrID, 0, nkcvoiceTemplet.FileName, (float)nkcvoiceTemplet.Volume, VOICE_BUNDLE.UNIT, false);
		}

		// Token: 0x06006FB4 RID: 28596 RVA: 0x0024EF88 File Offset: 0x0024D188
		private void SelectUnit(int unitID)
		{
			foreach (KeyValuePair<int, NKCDeckViewUnitSelectListSlot> keyValuePair in this.dicUnit)
			{
				if (keyValuePair.Key != unitID)
				{
					keyValuePair.Value.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
				}
				else
				{
					keyValuePair.Value.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
				}
			}
		}

		// Token: 0x06006FB5 RID: 28597 RVA: 0x0024EFFC File Offset: 0x0024D1FC
		private void RemoveNoNeedUnitTypeForSingleMode(List<NKMUnitTempletBase> listNKMUnitTempletBase)
		{
			if (listNKMUnitTempletBase == null)
			{
				return;
			}
			for (int i = 0; i < listNKMUnitTempletBase.Count; i++)
			{
				if (listNKMUnitTempletBase[i].m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
				{
					listNKMUnitTempletBase.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x04005B52 RID: 23378
		[SerializeField]
		private Transform trUnitContent;

		// Token: 0x04005B53 RID: 23379
		[SerializeField]
		private NKCDeckViewUnitSelectListSlot slotPrefab;

		// Token: 0x04005B54 RID: 23380
		[SerializeField]
		private Transform trVoiceContent;

		// Token: 0x04005B55 RID: 23381
		[SerializeField]
		private GameObject itemPrefab;

		// Token: 0x04005B56 RID: 23382
		private Dictionary<int, NKCDeckViewUnitSelectListSlot> dicUnit = new Dictionary<int, NKCDeckViewUnitSelectListSlot>();

		// Token: 0x04005B57 RID: 23383
		private List<NKCUIVoiceListDev.VoiceItem> listVoiceItem = new List<NKCUIVoiceListDev.VoiceItem>();

		// Token: 0x04005B58 RID: 23384
		private string currentUnitStrID = string.Empty;

		// Token: 0x0200173C RID: 5948
		public struct VoiceItem
		{
			// Token: 0x0400A65E RID: 42590
			public int Index;

			// Token: 0x0400A65F RID: 42591
			public Text FileName;

			// Token: 0x0400A660 RID: 42592
			public NKCUIComStateButton Button;
		}
	}
}
