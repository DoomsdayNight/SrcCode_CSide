using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007DC RID: 2012
	public class NKCWarfareGameBattleCondition
	{
		// Token: 0x06004F57 RID: 20311 RVA: 0x0017F698 File Offset: 0x0017D898
		public NKCWarfareGameBattleCondition(Transform trBattleConditionParent)
		{
			this._trBattleConditionParent = trBattleConditionParent;
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x0017F6EC File Offset: 0x0017D8EC
		public void Init()
		{
			this._dicWFBattleConditionByTileIndex.Clear();
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x0017F6FC File Offset: 0x0017D8FC
		public void Close()
		{
			foreach (KeyValuePair<int, NKCWarfareGameBattleCondition.WFBattleCondition> keyValuePair in this._dicWFBattleConditionByTileIndex)
			{
				NKCAssetResourceManager.CloseInstance(keyValuePair.Value.BCInstance);
			}
			this._dicWFBattleConditionByTileIndex.Clear();
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x0017F744 File Offset: 0x0017D944
		public void RemoveBattleCondition(int tileIndex)
		{
			if (!this._dicWFBattleConditionByTileIndex.ContainsKey(tileIndex))
			{
				return;
			}
			NKCAssetResourceManager.CloseInstance(this._dicWFBattleConditionByTileIndex[tileIndex].BCInstance);
			this._dicWFBattleConditionByTileIndex.Remove(tileIndex);
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x0017F778 File Offset: 0x0017D978
		public void SetBattleCondition(int tileIndex, int battleConditionID, Vector3 tilePos)
		{
			if (this._dicWFBattleConditionByTileIndex.ContainsKey(tileIndex))
			{
				if (this._dicWFBattleConditionByTileIndex[tileIndex].BCID == battleConditionID)
				{
					return;
				}
				this.RemoveBattleCondition(tileIndex);
			}
			NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(battleConditionID);
			if (templetByID == null)
			{
				return;
			}
			string battleCondWFIcon = templetByID.BattleCondWFIcon;
			NKCAssetInstanceData nkcassetInstanceData = this.CreateBattleCondition(battleCondWFIcon, tilePos);
			if (nkcassetInstanceData == null)
			{
				return;
			}
			NKCWarfareGameBattleCondition.WFBattleCondition value = new NKCWarfareGameBattleCondition.WFBattleCondition(battleConditionID, nkcassetInstanceData);
			this._dicWFBattleConditionByTileIndex.Add(tileIndex, value);
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x0017F7E4 File Offset: 0x0017D9E4
		private NKCAssetInstanceData CreateBattleCondition(string fileName, Vector3 tilePos)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return null;
			}
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", fileName, false, null);
			GameObject instant = nkcassetInstanceData.m_Instant;
			if (instant == null)
			{
				Debug.LogError(string.Format("전역 전투 환경 오브젝트를 찾을 수 없음 - {0}", fileName));
				return null;
			}
			if (this._trBattleConditionParent != null)
			{
				instant.transform.SetParent(this._trBattleConditionParent);
				instant.transform.localScale = this.BC_SCALE;
				instant.transform.localPosition = tilePos + this.BC_POS_ADD;
			}
			if (instant.GetComponent<NKCUICamFaceBillboard>() == null)
			{
				instant.AddComponent<NKCUICamFaceBillboard>();
			}
			return nkcassetInstanceData;
		}

		// Token: 0x04003F4C RID: 16204
		private Transform _trBattleConditionParent;

		// Token: 0x04003F4D RID: 16205
		private Dictionary<int, NKCWarfareGameBattleCondition.WFBattleCondition> _dicWFBattleConditionByTileIndex = new Dictionary<int, NKCWarfareGameBattleCondition.WFBattleCondition>();

		// Token: 0x04003F4E RID: 16206
		private readonly Vector3 BC_SCALE = new Vector3(2.2f, 2.2f, 2.2f);

		// Token: 0x04003F4F RID: 16207
		private readonly Vector3 BC_POS_ADD = Vector3.up * 18f;

		// Token: 0x04003F50 RID: 16208
		private const string BUNDLE_NAME = "AB_UI_NKM_UI_WARFARE";

		// Token: 0x02001491 RID: 5265
		public class WFBattleCondition
		{
			// Token: 0x0600A944 RID: 43332 RVA: 0x0034D3F4 File Offset: 0x0034B5F4
			public WFBattleCondition(int id, NKCAssetInstanceData instance)
			{
				this.BCID = id;
				this.BCInstance = instance;
			}

			// Token: 0x04009E6D RID: 40557
			public int BCID;

			// Token: 0x04009E6E RID: 40558
			public NKCAssetInstanceData BCInstance;
		}
	}
}
