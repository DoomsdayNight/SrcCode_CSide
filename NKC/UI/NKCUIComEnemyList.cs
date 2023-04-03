using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000933 RID: 2355
	public class NKCUIComEnemyList : MonoBehaviour
	{
		// Token: 0x06005E13 RID: 24083 RVA: 0x001D1B9B File Offset: 0x001CFD9B
		public void InitUI()
		{
			this.m_fInitPosXOfEnemyContent = this.m_NKM_UI_OPERATION_POPUP_ENEMY_Content.transform.localPosition.x;
			this.m_bInitComplete = true;
		}

		// Token: 0x06005E14 RID: 24084 RVA: 0x001D1BBF File Offset: 0x001CFDBF
		public void SetData(int stageID)
		{
			this.SetData(NKMStageTempletV2.Find(stageID));
		}

		// Token: 0x06005E15 RID: 24085 RVA: 0x001D1BCD File Offset: 0x001CFDCD
		public void SetData(NKMStageTempletV2 stageTemplet)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			if (stageTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_StageBattleStrID = stageTemplet.m_StageBattleStrID;
			this.SetEnemyListUI(this.Get_dicEnemyUnitStrIDs());
			this.SetEnemyLevel();
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x001D1C0C File Offset: 0x001CFE0C
		public void SetData(NKMDungeonTempletBase cNKMDungeonTempletBase)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			if (cNKMDungeonTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_StageBattleStrID = cNKMDungeonTempletBase.m_DungeonStrID;
			Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(cNKMDungeonTempletBase);
			this.SetEnemyListUI(enemyUnits);
			this.SetEnemyLevel();
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x001D1C58 File Offset: 0x001CFE58
		private void SetEnemyLevel()
		{
			bool flag = true;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_StageBattleStrID);
			if (nkmwarfareTemplet != null)
			{
				int num = nkmwarfareTemplet.m_WarfareLevel;
				NKCUtil.SetLabelText(this.m_lbEnemyLevel, string.Format(NKCUtilString.GET_STRING_DUNGEON_LEVEL_ONE_PARAM, num));
				return;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_StageBattleStrID);
			if (dungeonTempletBase != null)
			{
				if (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
				{
					flag = false;
				}
				if (dungeonTempletBase.StageTemplet != null && (dungeonTempletBase.StageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE || dungeonTempletBase.StageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL))
				{
					flag = false;
				}
				int num = dungeonTempletBase.m_DungeonLevel;
				if (flag)
				{
					NKCUtil.SetLabelText(this.m_lbEnemyLevel, string.Format(NKCUtilString.GET_STRING_DUNGEON_LEVEL_ONE_PARAM, num));
					return;
				}
				NKCUtil.SetLabelText(this.m_lbEnemyLevel, "");
				return;
			}
			else
			{
				NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(this.m_StageBattleStrID);
				if (nkmphaseTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbEnemyLevel, string.Format(NKCUtilString.GET_STRING_DUNGEON_LEVEL_ONE_PARAM, nkmphaseTemplet.PhaseLevel));
					return;
				}
				return;
			}
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x001D1D48 File Offset: 0x001CFF48
		private Dictionary<string, NKCEnemyData> Get_dicEnemyUnitStrIDs()
		{
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_StageBattleStrID);
			Dictionary<string, NKCEnemyData> enemyUnits;
			if (nkmstageTempletV != null)
			{
				enemyUnits = NKMDungeonManager.GetEnemyUnits(nkmstageTempletV);
			}
			else
			{
				enemyUnits = NKMDungeonManager.GetEnemyUnits(NKMDungeonManager.GetDungeonTempletBase(this.m_StageBattleStrID));
			}
			return enemyUnits;
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x001D1D80 File Offset: 0x001CFF80
		private void SetEnemyListUI(Dictionary<string, NKCEnemyData> dicEnemyUnitStrIDs)
		{
			Vector3 localPosition = this.m_NKM_UI_OPERATION_POPUP_ENEMY_Content.transform.localPosition;
			this.m_NKM_UI_OPERATION_POPUP_ENEMY_Content.transform.localPosition = new Vector3(this.m_fInitPosXOfEnemyContent, localPosition.y, localPosition.z);
			if (dicEnemyUnitStrIDs == null)
			{
				return;
			}
			int i;
			if (dicEnemyUnitStrIDs.Count > this.m_lstNKCDeckViewEnemySlot.Count)
			{
				int count = this.m_lstNKCDeckViewEnemySlot.Count;
				for (i = 0; i < dicEnemyUnitStrIDs.Count - count; i++)
				{
					NKCDeckViewEnemySlot newInstance = NKCDeckViewEnemySlot.GetNewInstance(this.m_NKM_UI_OPERATION_POPUP_ENEMY_Content.transform);
					if (!(newInstance == null))
					{
						newInstance.Init(i + count);
						newInstance.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
						Vector3 localPosition2 = newInstance.gameObject.transform.localPosition;
						newInstance.gameObject.transform.localPosition = new Vector3(localPosition2.x, localPosition2.y, 0f);
						this.m_lstNKCDeckViewEnemySlot.Add(newInstance);
					}
				}
			}
			List<NKCEnemyData> list = new List<NKCEnemyData>(dicEnemyUnitStrIDs.Values);
			list.Sort(new NKCEnemyData.CompNED());
			for (i = 0; i < list.Count; i++)
			{
				NKCDeckViewEnemySlot nkcdeckViewEnemySlot = this.m_lstNKCDeckViewEnemySlot[i];
				nkcdeckViewEnemySlot.SetEnemyData(NKMUnitManager.GetUnitTempletBase(list[i].m_UnitStrID), list[i]);
				nkcdeckViewEnemySlot.m_NKCUIComButton.PointerClick.RemoveAllListeners();
				nkcdeckViewEnemySlot.m_NKCUIComButton.PointerClick.AddListener(new UnityAction(this.OnClickEnemySlot));
				NKCUtil.SetGameobjectActive(nkcdeckViewEnemySlot.gameObject, true);
			}
			while (i < this.m_lstNKCDeckViewEnemySlot.Count)
			{
				NKCUtil.SetGameobjectActive(this.m_lstNKCDeckViewEnemySlot[i].gameObject, false);
				i++;
			}
		}

		// Token: 0x06005E1A RID: 24090 RVA: 0x001D1F50 File Offset: 0x001D0150
		private void OnClickEnemySlot()
		{
			foreach (NKCDeckViewEnemySlot nkcdeckViewEnemySlot in this.m_lstNKCDeckViewEnemySlot)
			{
				nkcdeckViewEnemySlot.m_NKCUIComButton.Select(false, false, false);
			}
			NKCPopupEnemyList.Instance.Open(this.Get_dicEnemyUnitStrIDs().Values.ToList<NKCEnemyData>());
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x001D1FC4 File Offset: 0x001D01C4
		private void OnDestroy()
		{
			for (int i = 0; i < this.m_lstNKCDeckViewEnemySlot.Count; i++)
			{
				this.m_lstNKCDeckViewEnemySlot[i].CloseInstance();
			}
			this.m_lstNKCDeckViewEnemySlot.Clear();
		}

		// Token: 0x04004A41 RID: 19009
		public Text m_lbEnemyLevel;

		// Token: 0x04004A42 RID: 19010
		public GameObject m_NKM_UI_OPERATION_POPUP_ENEMY_Content;

		// Token: 0x04004A43 RID: 19011
		private List<NKCDeckViewEnemySlot> m_lstNKCDeckViewEnemySlot = new List<NKCDeckViewEnemySlot>();

		// Token: 0x04004A44 RID: 19012
		private float m_fInitPosXOfEnemyContent;

		// Token: 0x04004A45 RID: 19013
		private string m_StageBattleStrID;

		// Token: 0x04004A46 RID: 19014
		private bool m_bInitComplete;
	}
}
