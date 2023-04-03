using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007A7 RID: 1959
	public class NKCDiveGameSectorSet : MonoBehaviour
	{
		// Token: 0x06004D25 RID: 19749 RVA: 0x001730C9 File Offset: 0x001712C9
		public int GetDistance()
		{
			return this.m_Distance;
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x001730D1 File Offset: 0x001712D1
		public bool IsAnimating()
		{
			return this.m_coAni != null;
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x001730DC File Offset: 0x001712DC
		public void StopAni()
		{
			if (this.m_coAni != null)
			{
				base.StopCoroutine(this.m_coAni);
				this.m_coAni = null;
			}
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x001730FC File Offset: 0x001712FC
		public static NKCDiveGameSectorSet GetNewInstance(int _Distance, Transform parent, NKCDiveGameSector.OnClickSector dOnClickSector = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NKM_UI_DIVE_PROCESS_SECTOR_SET", false, null);
			NKCDiveGameSectorSet component = nkcassetInstanceData.m_Instant.GetComponent<NKCDiveGameSectorSet>();
			if (component == null)
			{
				Debug.LogError("NKCDiveGameSectorSet Prefab null!");
				return null;
			}
			component.m_Distance = _Distance;
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			for (int i = 0; i < component.m_lstNKCDiveGameSectorEven.Count; i++)
			{
				component.m_lstNKCDiveGameSectorEven[i].Init(component, dOnClickSector);
			}
			for (int i = 0; i < component.m_lstNKCDiveGameSectorOdd.Count; i++)
			{
				component.m_lstNKCDiveGameSectorOdd[i].Init(component, dOnClickSector);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x001731C0 File Offset: 0x001713C0
		private void OnDestroy()
		{
			if (this.m_InstanceData != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			}
			this.m_InstanceData = null;
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x001731DC File Offset: 0x001713DC
		public void SetActive(bool bSet)
		{
			this.m_bRealActive = bSet;
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x001731E5 File Offset: 0x001713E5
		public bool GetActive()
		{
			return this.m_bRealActive;
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x001731ED File Offset: 0x001713ED
		public int GetRealSetSize()
		{
			return this.m_RealSetSize;
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x001731F5 File Offset: 0x001713F5
		private void SetEven(bool bEven)
		{
			this.m_bEven = bEven;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SECTOR_SET_EVEN, bEven);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_SECTOR_SET_ODD, !bEven);
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0017321C File Offset: 0x0017141C
		private int GetStartIndex()
		{
			int num;
			if (this.m_bEven)
			{
				num = this.m_lstNKCDiveGameSectorEven.Count / 2 - 1;
				num -= this.m_RealSetSize / 2 - 1;
			}
			else
			{
				num = this.m_lstNKCDiveGameSectorOdd.Count / 2;
				num -= this.m_RealSetSize / 2;
			}
			return num;
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0017326C File Offset: 0x0017146C
		public void SetBoss()
		{
			this.SetEven(false);
			for (int i = 0; i < this.m_lstNKCDiveGameSectorOdd.Count; i++)
			{
				if (i == this.m_lstNKCDiveGameSectorOdd.Count / 2)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorOdd[i], true);
					this.m_lstNKCDiveGameSectorOdd[i].SetUI(new NKMDiveSlot(NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_BOSS, NKM_DIVE_EVENT_TYPE.NDET_DUNGEON_BOSS, 0, 0, 0));
					this.m_lstNKCDiveGameSectorOdd[i].SetSelected(false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorOdd[i], false);
				}
			}
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x001732FC File Offset: 0x001714FC
		public void SetGrey()
		{
			this.InvalidGrey();
			List<NKCDiveGameSector> list;
			if (this.m_bEven)
			{
				list = this.m_lstNKCDiveGameSectorEven;
			}
			else
			{
				list = this.m_lstNKCDiveGameSectorOdd;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			int num = 0;
			if (myUserData.m_DiveGameData != null)
			{
				num = myUserData.m_DiveGameData.Player.PlayerBase.SlotIndex;
			}
			int num2 = 0;
			for (int i = 0; i < list.Count; i++)
			{
				if (i >= this.GetStartIndex() && i < this.GetStartIndex() + this.m_RealSetSize)
				{
					if (num != num2 && num - 1 != num2 && num + 1 != num2)
					{
						list[i].SetGrey();
					}
					num2++;
				}
			}
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x001733A8 File Offset: 0x001715A8
		public void InvalidGrey()
		{
			for (int i = 0; i < this.m_lstNKCDiveGameSectorEven.Count; i++)
			{
				this.m_lstNKCDiveGameSectorEven[i].InvaldGrey();
			}
			for (int i = 0; i < this.m_lstNKCDiveGameSectorOdd.Count; i++)
			{
				this.m_lstNKCDiveGameSectorOdd[i].InvaldGrey();
			}
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x00173405 File Offset: 0x00171605
		private void SetDiveGameSectorData(NKCDiveGameSector cNKCDiveGameSector, NKMDiveSlot cNKMDiveSlot, int index, int realIndex)
		{
			if (cNKCDiveGameSector == null || cNKMDiveSlot == null)
			{
				return;
			}
			cNKCDiveGameSector.SetUI(cNKMDiveSlot);
			cNKCDiveGameSector.SetSlotIndex(realIndex);
			cNKCDiveGameSector.SetUISlotIndex(index);
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0017342A File Offset: 0x0017162A
		private IEnumerator ProcessGameSectorSpawnAni(NKMDiveSlotSet cNKMDiveSlotSet, List<NKCDiveGameSector> lstNKCDiveGameSector, bool playSpawnSound)
		{
			int realIndex = 0;
			int num;
			for (int i = 0; i < lstNKCDiveGameSector.Count; i = num + 1)
			{
				if (i >= this.GetStartIndex() && i < this.GetStartIndex() + this.m_RealSetSize)
				{
					NKMDiveSlot cNKMDiveSlot = cNKMDiveSlotSet.Slots[realIndex];
					this.SetDiveGameSectorData(lstNKCDiveGameSector[i], cNKMDiveSlot, i, realIndex);
					lstNKCDiveGameSector[i].PlayOpenAni(playSpawnSound);
					yield return new WaitForSeconds(0.1f);
					num = realIndex;
					realIndex = num + 1;
				}
				else
				{
					NKCUtil.SetGameobjectActive(lstNKCDiveGameSector[i], false);
				}
				num = i;
			}
			this.m_coAni = null;
			yield break;
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x00173450 File Offset: 0x00171650
		public void SetUIByData(NKMDiveSlotSet cNKMDiveSlotSet, bool bSpawnAni = false, bool bPlaySpawnSound = true)
		{
			this.SetRealSize(cNKMDiveSlotSet.Slots.Count);
			List<NKCDiveGameSector> list;
			if (this.m_bEven)
			{
				list = this.m_lstNKCDiveGameSectorEven;
			}
			else
			{
				list = this.m_lstNKCDiveGameSectorOdd;
			}
			if (this.m_coAni != null)
			{
				base.StopCoroutine(this.m_coAni);
				this.m_coAni = null;
			}
			if (bSpawnAni && base.gameObject.activeSelf)
			{
				this.m_coAni = base.StartCoroutine(this.ProcessGameSectorSpawnAni(cNKMDiveSlotSet, list, bPlaySpawnSound));
				return;
			}
			int num = 0;
			for (int i = 0; i < list.Count; i++)
			{
				if (i >= this.GetStartIndex() && i < this.GetStartIndex() + this.m_RealSetSize)
				{
					NKMDiveSlot cNKMDiveSlot = cNKMDiveSlotSet.Slots[num];
					this.SetDiveGameSectorData(list[i], cNKMDiveSlot, i, num);
					num++;
				}
				else
				{
					NKCUtil.SetGameobjectActive(list[i], false);
				}
			}
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x00173528 File Offset: 0x00171728
		public void SetAllEmpty()
		{
			List<NKCDiveGameSector> currSectorList = this.GetCurrSectorList();
			for (int i = 0; i < currSectorList.Count; i++)
			{
				if (i >= this.GetStartIndex() && i < this.GetStartIndex() + this.m_RealSetSize)
				{
					currSectorList[i].SetUI(new NKMDiveSlot(NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_NONE, NKM_DIVE_EVENT_TYPE.NDET_NONE, 0, 0, 0));
					currSectorList[i].SetSlotIndex(i - this.GetStartIndex());
					currSectorList[i].SetUISlotIndex(i);
				}
			}
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x001735A0 File Offset: 0x001717A0
		public NKCDiveGameSector GetBossSector()
		{
			List<NKCDiveGameSector> currSectorList = this.GetCurrSectorList();
			for (int i = 0; i < currSectorList.Count; i++)
			{
				if (currSectorList[i] != null && currSectorList[i].gameObject.activeSelf && currSectorList[i].GetNKMDiveSlot().EventType == NKM_DIVE_EVENT_TYPE.NDET_DUNGEON_BOSS)
				{
					return currSectorList[i];
				}
			}
			return null;
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x00173608 File Offset: 0x00171808
		public NKCDiveGameSector GetSector(NKMDiveSlot cNKMDiveSlot)
		{
			if (cNKMDiveSlot == null)
			{
				return null;
			}
			List<NKCDiveGameSector> currSectorList = this.GetCurrSectorList();
			for (int i = 0; i < currSectorList.Count; i++)
			{
				if (currSectorList[i].GetNKMDiveSlot() == cNKMDiveSlot)
				{
					return currSectorList[i];
				}
			}
			return null;
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x0017364C File Offset: 0x0017184C
		public NKCDiveGameSector GetSector(int index)
		{
			List<NKCDiveGameSector> currSectorList = this.GetCurrSectorList();
			int num = 0;
			for (int i = 0; i < currSectorList.Count; i++)
			{
				if (i >= this.GetStartIndex() && i < this.GetStartIndex() + this.m_RealSetSize)
				{
					if (num == index)
					{
						return currSectorList[i];
					}
					num++;
				}
			}
			return null;
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x001736A0 File Offset: 0x001718A0
		private List<NKCDiveGameSector> GetCurrSectorList()
		{
			List<NKCDiveGameSector> result;
			if (this.m_bEven)
			{
				result = this.m_lstNKCDiveGameSectorEven;
			}
			else
			{
				result = this.m_lstNKCDiveGameSectorOdd;
			}
			return result;
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x001736C8 File Offset: 0x001718C8
		public void SetRealSize(int realSize)
		{
			this.m_RealSetSize = realSize;
			this.SetEven(this.m_RealSetSize % 2 == 0);
			List<NKCDiveGameSector> currSectorList = this.GetCurrSectorList();
			for (int i = 0; i < currSectorList.Count; i++)
			{
				if (i >= this.GetStartIndex() && i < this.GetStartIndex() + this.m_RealSetSize)
				{
					NKCUtil.SetGameobjectActive(currSectorList[i], true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(currSectorList[i], false);
				}
			}
		}

		// Token: 0x04003D18 RID: 15640
		private bool m_bEven = true;

		// Token: 0x04003D19 RID: 15641
		private int m_RealSetSize;

		// Token: 0x04003D1A RID: 15642
		public GameObject m_NKM_UI_DIVE_PROCESS_SECTOR_SET_EVEN;

		// Token: 0x04003D1B RID: 15643
		public GameObject m_NKM_UI_DIVE_PROCESS_SECTOR_SET_ODD;

		// Token: 0x04003D1C RID: 15644
		public List<NKCDiveGameSector> m_lstNKCDiveGameSectorEven = new List<NKCDiveGameSector>();

		// Token: 0x04003D1D RID: 15645
		public List<NKCDiveGameSector> m_lstNKCDiveGameSectorOdd = new List<NKCDiveGameSector>();

		// Token: 0x04003D1E RID: 15646
		private int m_Distance;

		// Token: 0x04003D1F RID: 15647
		private bool m_bRealActive;

		// Token: 0x04003D20 RID: 15648
		private Coroutine m_coAni;

		// Token: 0x04003D21 RID: 15649
		private NKCAssetInstanceData m_InstanceData;
	}
}
