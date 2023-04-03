using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007A8 RID: 1960
	public class NKCDiveGameSectorSetMgr
	{
		// Token: 0x06004D3C RID: 19772 RVA: 0x00173761 File Offset: 0x00171961
		public void Init(Transform parentOfSets)
		{
			this.m_trParentOfSets = parentOfSets;
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0017376C File Offset: 0x0017196C
		public bool IsAnimating()
		{
			if (this.m_bOpenAniForSecondLine)
			{
				return true;
			}
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (this.m_lstNKCDiveGameSectorSet[i].IsAnimating())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x001737B0 File Offset: 0x001719B0
		public void StopAni()
		{
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (this.m_lstNKCDiveGameSectorSet[i] != null)
				{
					this.m_lstNKCDiveGameSectorSet[i].StopAni();
				}
			}
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x001737F8 File Offset: 0x001719F8
		public NKCDiveGameSector GetBossSector()
		{
			for (int i = this.m_lstNKCDiveGameSectorSet.Count - 1; i >= 0; i--)
			{
				NKCDiveGameSectorSet nkcdiveGameSectorSet = this.m_lstNKCDiveGameSectorSet[i];
				if (nkcdiveGameSectorSet != null)
				{
					NKCDiveGameSector bossSector = nkcdiveGameSectorSet.GetBossSector();
					if (bossSector != null)
					{
						return bossSector;
					}
				}
			}
			return null;
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00173848 File Offset: 0x00171A48
		public void Update()
		{
			if (this.m_bOpenAniForSecondLine)
			{
				float num = Time.deltaTime;
				if (num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
				{
					num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
				}
				this.m_fElapsedTimeToOpenAniForSecondLine += num;
				if (this.m_fElapsedTimeToOpenAniForSecondLine >= 0.5f)
				{
					NKMDiveGameData diveGameData = NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
					if (diveGameData != null)
					{
						this.m_lstNKCDiveGameSectorSet[this.m_UISectorSetIndexToOpenAniFor2ndLine].SetUIByData(diveGameData.Floor.SlotSets[this.m_SectorSetIndexToOpenAniFor2ndLine], true, true);
					}
					this.m_bOpenAniForSecondLine = false;
				}
			}
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x001738F0 File Offset: 0x00171AF0
		public bool CheckExistEuclidInNextSectors()
		{
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (this.m_lstNKCDiveGameSectorSet[i].GetActive())
				{
					for (int j = 0; j < this.m_lstNKCDiveGameSectorSet[i].GetRealSetSize(); j++)
					{
						NKCDiveGameSector sector = this.m_lstNKCDiveGameSectorSet[i].GetSector(j);
						if (!(sector == null) && sector.GetNKMDiveSlot() != null && sector.CheckSelectable() && NKCDiveManager.IsEuclidSectorType(sector.GetNKMDiveSlot().SectorType))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x00173988 File Offset: 0x00171B88
		public NKCDiveGameSector GetNextDiveGameSectorByAuto(bool bStartPos)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (this.m_lstNKCDiveGameSectorSet[i].GetActive())
				{
					list.Clear();
					for (int j = 0; j < this.m_lstNKCDiveGameSectorSet[i].GetRealSetSize(); j++)
					{
						NKCDiveGameSector sector = this.m_lstNKCDiveGameSectorSet[i].GetSector(j);
						if (!(sector == null) && sector.GetNKMDiveSlot() != null && sector.CheckSelectable() && NKCDiveManager.IsEuclidSectorType(sector.GetNKMDiveSlot().SectorType))
						{
							list.Add(j);
						}
					}
					if (list.Count > 0)
					{
						int index = list[NKMRandom.Range(0, list.Count)];
						return this.m_lstNKCDiveGameSectorSet[i].GetSector(index);
					}
				}
			}
			int num = 0;
			int num2 = 1;
			if (bStartPos)
			{
				num2 = 0;
			}
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (this.m_lstNKCDiveGameSectorSet[i].GetActive())
				{
					if (num2 == num)
					{
						if (this.m_lstNKCDiveGameSectorSet[i].GetRealSetSize() == 1)
						{
							return this.m_lstNKCDiveGameSectorSet[i].GetSector(0);
						}
						list.Clear();
						for (int k = 0; k < this.m_lstNKCDiveGameSectorSet[i].GetRealSetSize(); k++)
						{
							NKCDiveGameSector sector2 = this.m_lstNKCDiveGameSectorSet[i].GetSector(k);
							if (!(sector2 == null) && sector2.GetNKMDiveSlot() != null && sector2.CheckSelectable())
							{
								list.Add(k);
							}
						}
						if (list.Count > 0)
						{
							int index2 = list[NKMRandom.Range(0, list.Count)];
							return this.m_lstNKCDiveGameSectorSet[i].GetSector(index2);
						}
					}
					num++;
				}
			}
			return null;
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00173B70 File Offset: 0x00171D70
		public NKCDiveGameSector GetActiveDiveGameSector(int slotSetIndex, int slotIndex)
		{
			if (slotSetIndex < 0 || slotSetIndex >= this.m_lstNKCDiveGameSectorSet.Count)
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (this.m_lstNKCDiveGameSectorSet[i].GetActive())
				{
					if (slotSetIndex == num)
					{
						return this.m_lstNKCDiveGameSectorSet[i].GetSector(slotIndex);
					}
					num++;
				}
			}
			return null;
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x00173BDC File Offset: 0x00171DDC
		public NKCDiveGameSector GetSector(NKMDiveSlot cNKMDiveSlot)
		{
			if (cNKMDiveSlot == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (this.m_lstNKCDiveGameSectorSet[i].GetActive())
				{
					NKCDiveGameSector sector = this.m_lstNKCDiveGameSectorSet[i].GetSector(cNKMDiveSlot);
					if (sector != null)
					{
						return sector;
					}
				}
			}
			return null;
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x00173C38 File Offset: 0x00171E38
		public void SetUIWhenStartBeforeScan(NKMDiveGameData cNKMDiveGameData)
		{
			if (cNKMDiveGameData == null)
			{
				return;
			}
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (i < cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], true);
					if (NKCDiveManager.IsDiveJump() || i + 1 == cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
					{
						this.m_lstNKCDiveGameSectorSet[i].SetBoss();
						this.m_lstNKCDiveGameSectorSet[i].SetActive(false);
					}
					else
					{
						this.m_lstNKCDiveGameSectorSet[i].SetRealSize(this.m_RealSetSize);
						this.m_lstNKCDiveGameSectorSet[i].SetAllEmpty();
						this.m_lstNKCDiveGameSectorSet[i].SetActive(false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], false);
				}
			}
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x00173D24 File Offset: 0x00171F24
		public void SetUIWhenAddSectorBeforeScan(NKMDiveGameData cNKMDiveGameData)
		{
			if (cNKMDiveGameData == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (i < cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], true);
					if (i >= cNKMDiveGameData.Player.PlayerBase.Distance - 1 && i < cNKMDiveGameData.Player.PlayerBase.Distance - 1 + cNKMDiveGameData.Floor.SlotSets.Count - 1)
					{
						if (i == cNKMDiveGameData.Player.PlayerBase.Distance - 1 && i + 1 != cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
						{
							this.m_lstNKCDiveGameSectorSet[i].SetRealSize(this.m_RealSetSize);
							this.m_lstNKCDiveGameSectorSet[i].SetAllEmpty();
							int index;
							if (cNKMDiveGameData.Player.PlayerBase.State == NKMDivePlayerState.Exploring || cNKMDiveGameData.Player.PlayerBase.State == NKMDivePlayerState.SelectArtifact)
							{
								index = cNKMDiveGameData.Player.PlayerBase.SlotIndex;
							}
							else
							{
								index = cNKMDiveGameData.Player.PlayerBase.PrevSlotIndex;
							}
							NKCDiveGameSector sector = this.m_lstNKCDiveGameSectorSet[i].GetSector(index);
							if (sector != null)
							{
								sector.SetUI(cNKMDiveGameData.Floor.SlotSets[num].Slots[0]);
							}
						}
						else
						{
							this.m_lstNKCDiveGameSectorSet[i].SetUIByData(cNKMDiveGameData.Floor.SlotSets[num], false, true);
						}
						this.m_lstNKCDiveGameSectorSet[i].SetActive(true);
						num++;
					}
					else if (NKCDiveManager.IsDiveJump() || i + 1 == cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
					{
						this.m_lstNKCDiveGameSectorSet[i].SetBoss();
						this.m_lstNKCDiveGameSectorSet[i].SetActive(false);
					}
					else
					{
						this.m_lstNKCDiveGameSectorSet[i].SetRealSize(this.m_RealSetSize);
						this.m_lstNKCDiveGameSectorSet[i].SetAllEmpty();
						this.m_lstNKCDiveGameSectorSet[i].SetActive(false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], false);
				}
			}
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x00173F78 File Offset: 0x00172178
		public void SetUI(NKMDiveGameData cNKMDiveGameData, bool bShowSpawnAni = false)
		{
			if (cNKMDiveGameData == null)
			{
				return;
			}
			int num = 0;
			if (cNKMDiveGameData.Player.PlayerBase.Distance == 0)
			{
				for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
				{
					if (i < cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
					{
						NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], true);
						if (i >= cNKMDiveGameData.Player.PlayerBase.Distance && i < cNKMDiveGameData.Player.PlayerBase.Distance + cNKMDiveGameData.Floor.SlotSets.Count)
						{
							this.m_lstNKCDiveGameSectorSet[i].SetActive(true);
							this.m_lstNKCDiveGameSectorSet[i].SetUIByData(cNKMDiveGameData.Floor.SlotSets[num], bShowSpawnAni, i == 0);
							num++;
						}
						else if (NKCDiveManager.IsDiveJump() || i + 1 == cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
						{
							this.m_lstNKCDiveGameSectorSet[i].SetBoss();
							this.m_lstNKCDiveGameSectorSet[i].SetActive(false);
						}
						else
						{
							this.m_lstNKCDiveGameSectorSet[i].SetRealSize(this.m_RealSetSize);
							this.m_lstNKCDiveGameSectorSet[i].SetAllEmpty();
							this.m_lstNKCDiveGameSectorSet[i].SetActive(false);
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], false);
					}
				}
				return;
			}
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (i < cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], true);
					if (i >= cNKMDiveGameData.Player.PlayerBase.Distance - 1 && i < cNKMDiveGameData.Player.PlayerBase.Distance - 1 + cNKMDiveGameData.Floor.SlotSets.Count)
					{
						if (i == cNKMDiveGameData.Player.PlayerBase.Distance - 1 && i + 1 != cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
						{
							this.m_lstNKCDiveGameSectorSet[i].SetRealSize(this.m_RealSetSize);
							this.m_lstNKCDiveGameSectorSet[i].SetAllEmpty();
							int index;
							if (cNKMDiveGameData.Player.PlayerBase.State == NKMDivePlayerState.Exploring || cNKMDiveGameData.Player.PlayerBase.State == NKMDivePlayerState.SelectArtifact)
							{
								index = cNKMDiveGameData.Player.PlayerBase.SlotIndex;
							}
							else
							{
								index = cNKMDiveGameData.Player.PlayerBase.PrevSlotIndex;
							}
							NKCDiveGameSector sector = this.m_lstNKCDiveGameSectorSet[i].GetSector(index);
							if (sector != null)
							{
								sector.SetUI(cNKMDiveGameData.Floor.SlotSets[num].Slots[0]);
							}
						}
						else
						{
							this.m_lstNKCDiveGameSectorSet[i].SetUIByData(cNKMDiveGameData.Floor.SlotSets[num], bShowSpawnAni && num == cNKMDiveGameData.Floor.SlotSets.Count - 1, true);
						}
						if (i == cNKMDiveGameData.Player.PlayerBase.Distance && i + 1 != cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
						{
							this.m_lstNKCDiveGameSectorSet[i].SetGrey();
						}
						else
						{
							this.m_lstNKCDiveGameSectorSet[i].InvalidGrey();
						}
						this.m_lstNKCDiveGameSectorSet[i].SetActive(true);
						num++;
					}
					else if (i + 1 == cNKMDiveGameData.Floor.Templet.RandomSetCount + 1)
					{
						this.m_lstNKCDiveGameSectorSet[i].SetBoss();
						this.m_lstNKCDiveGameSectorSet[i].SetActive(false);
					}
					else
					{
						this.m_lstNKCDiveGameSectorSet[i].SetRealSize(this.m_RealSetSize);
						this.m_lstNKCDiveGameSectorSet[i].SetAllEmpty();
						this.m_lstNKCDiveGameSectorSet[i].SetActive(false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], false);
				}
			}
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x00174390 File Offset: 0x00172590
		public void Reset(int depth, int setSize, NKCDiveGameSector.OnClickSector dOnClickSector = null)
		{
			this.m_bOpenAniForSecondLine = false;
			this.m_RealDepth = (NKCDiveManager.IsDiveJump() ? depth : (depth + 1));
			this.m_RealSetSize = setSize;
			if (this.m_lstNKCDiveGameSectorSet.Count < this.m_RealDepth)
			{
				int num = this.m_RealDepth - this.m_lstNKCDiveGameSectorSet.Count;
				int count = this.m_lstNKCDiveGameSectorSet.Count;
				for (int i = 0; i < num; i++)
				{
					NKCDiveGameSectorSet newInstance = NKCDiveGameSectorSet.GetNewInstance(count + i + 1, this.m_trParentOfSets, dOnClickSector);
					this.m_lstNKCDiveGameSectorSet.Add(newInstance);
				}
			}
			for (int i = 0; i < this.m_lstNKCDiveGameSectorSet.Count; i++)
			{
				if (i < this.m_RealDepth)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCDiveGameSectorSet[i], false);
				}
			}
		}

		// Token: 0x04003D22 RID: 15650
		private List<NKCDiveGameSectorSet> m_lstNKCDiveGameSectorSet = new List<NKCDiveGameSectorSet>();

		// Token: 0x04003D23 RID: 15651
		private Transform m_trParentOfSets;

		// Token: 0x04003D24 RID: 15652
		private int m_RealDepth;

		// Token: 0x04003D25 RID: 15653
		private int m_RealSetSize;

		// Token: 0x04003D26 RID: 15654
		private bool m_bOpenAniForSecondLine;

		// Token: 0x04003D27 RID: 15655
		private float m_fElapsedTimeToOpenAniForSecondLine;

		// Token: 0x04003D28 RID: 15656
		private int m_UISectorSetIndexToOpenAniFor2ndLine;

		// Token: 0x04003D29 RID: 15657
		private int m_SectorSetIndexToOpenAniFor2ndLine;
	}
}
