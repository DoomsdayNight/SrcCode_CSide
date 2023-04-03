using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007BA RID: 1978
	public class NKCUICutScenUnitMgr : MonoBehaviour
	{
		// Token: 0x06004E59 RID: 20057 RVA: 0x0017A06F File Offset: 0x0017826F
		public static NKCUICutScenUnitMgr GetCutScenUnitMgr()
		{
			return NKCUICutScenUnitMgr.m_scNKCUICutScenUnitMgr;
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x0017A076 File Offset: 0x00178276
		public void SetPause(bool bPause)
		{
			this.m_bPause = bPause;
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x0017A080 File Offset: 0x00178280
		public static void InitUI(GameObject goNKM_UI_CUTSCEN_PLAYER)
		{
			if (NKCUICutScenUnitMgr.m_scNKCUICutScenUnitMgr != null)
			{
				return;
			}
			NKCUICutScenUnitMgr.m_scNKCUICutScenUnitMgr = goNKM_UI_CUTSCEN_PLAYER.transform.Find("NKM_UI_CUTSCEN_PLAYER_UNIT_MGR").gameObject.GetComponent<NKCUICutScenUnitMgr>();
			NKCUICutScenUnitMgr.m_scNKCUICutScenUnitMgr.InitCutUnit();
			NKCUICutScenUnitMgr.m_scNKCUICutScenUnitMgr.Close();
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x0017A0D0 File Offset: 0x001782D0
		private void InitCutUnit()
		{
			this.m_arrCutUnit[0] = this.m_nkcCutUnitLeft;
			this.m_arrCutUnit[1] = this.m_nkcCutUnitRight;
			this.m_arrCutUnit[2] = this.m_nkcCutUnitCenter;
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				this.m_arrCutUnit[i].InitCutUnit();
			}
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x0017A127 File Offset: 0x00178327
		public void Reset()
		{
			this.SetPause(false);
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x0017A130 File Offset: 0x00178330
		public bool IsFinished()
		{
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				if (!this.m_arrCutUnit[i].IsFinished())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x0017A164 File Offset: 0x00178364
		public bool IsExistUnitInScen()
		{
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				if (this.m_arrCutUnit[i].GoUnit.activeSelf)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x0017A19C File Offset: 0x0017839C
		private void Update()
		{
			if (this.m_bPause)
			{
				return;
			}
			if (this.m_currentPos != CutUnitPosType.NONE)
			{
				this.m_arrCutUnit[(int)this.m_currentPos].UpdateUnitPos();
			}
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				this.m_arrCutUnit[i].ManualUpdate();
			}
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x0017A1F0 File Offset: 0x001783F0
		public void Finish()
		{
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				this.m_arrCutUnit[i].FinishUnit();
			}
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x0017A220 File Offset: 0x00178420
		public void StopCrash()
		{
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				this.m_arrCutUnit[i].StopUnitCrash();
			}
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x0017A24D File Offset: 0x0017844D
		public RectTransform GetUnitRectTransform(CutUnitPosType posType)
		{
			return this.m_arrCutUnit[(int)posType].RectTransform;
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x0017A25C File Offset: 0x0017845C
		public NKCUICharacterView GetUnitCharacterView(CutUnitPosType posType)
		{
			return this.m_arrCutUnit[(int)posType].CharacterView;
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x0017A26B File Offset: 0x0017846B
		public NKCCutTemplet GetUnitCutTemplet(CutUnitPosType posType)
		{
			return this.m_arrCutUnit[(int)posType].NKCCutTemplet;
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x0017A27C File Offset: 0x0017847C
		public void DarkenOtherUnitColor(CutUnitPosType posType)
		{
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				if (this.m_arrCutUnit[i].NKCCutTemplet != null && posType != (CutUnitPosType)i)
				{
					this.m_arrCutUnit[i].DarkenUnit();
				}
			}
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x0017A2BC File Offset: 0x001784BC
		public void ClearUnitByPos(NKCCutTemplet cNKCCutTemplet, CutUnitPosType posType)
		{
			if (this.m_arrCutUnit[(int)posType] == null)
			{
				return;
			}
			this.m_arrCutUnit[(int)posType].ClearUnit(cNKCCutTemplet, true, false);
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x0017A2DF File Offset: 0x001784DF
		public void SetUnit(NKCCutScenCharTemplet cNKCCutScenCharTemplet, NKCCutTemplet cNKCCutTemplet)
		{
			this.m_currentPos = cNKCCutTemplet.CutUnitPos;
			if (this.m_currentPos == CutUnitPosType.NONE)
			{
				return;
			}
			this.m_arrCutUnit[(int)this.m_currentPos].SetUnit(cNKCCutScenCharTemplet, cNKCCutTemplet);
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x0017A30C File Offset: 0x0017850C
		public void ClearUnit(NKCCutTemplet cNKCCutTemplet)
		{
			this.m_currentPos = cNKCCutTemplet.CutUnitPos;
			bool bNone = false;
			if (this.m_currentPos == CutUnitPosType.NONE)
			{
				bNone = true;
			}
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				this.m_arrCutUnit[i].ClearUnit(cNKCCutTemplet, bNone, this.m_currentPos == (CutUnitPosType)i);
			}
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x0017A35D File Offset: 0x0017855D
		public void Open()
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x0017A378 File Offset: 0x00178578
		public void Close()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			for (int i = 0; i < this.m_arrCutUnit.Length; i++)
			{
				this.m_arrCutUnit[i].Close();
			}
			this.m_currentPos = CutUnitPosType.NONE;
		}

		// Token: 0x04003DFA RID: 15866
		public NKCCutUnit m_nkcCutUnitLeft;

		// Token: 0x04003DFB RID: 15867
		public NKCCutUnit m_nkcCutUnitRight;

		// Token: 0x04003DFC RID: 15868
		public NKCCutUnit m_nkcCutUnitCenter;

		// Token: 0x04003DFD RID: 15869
		private NKCCutUnit[] m_arrCutUnit = new NKCCutUnit[3];

		// Token: 0x04003DFE RID: 15870
		private CutUnitPosType m_currentPos;

		// Token: 0x04003DFF RID: 15871
		private static NKCUICutScenUnitMgr m_scNKCUICutScenUnitMgr;

		// Token: 0x04003E00 RID: 15872
		private bool m_bPause;
	}
}
