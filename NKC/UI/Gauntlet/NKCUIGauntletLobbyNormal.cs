using System;
using Cs.Logging;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B77 RID: 2935
	public class NKCUIGauntletLobbyNormal : MonoBehaviour
	{
		// Token: 0x06008710 RID: 34576 RVA: 0x002DB30C File Offset: 0x002D950C
		public void Init()
		{
			this.m_csbtnBattleRecord.PointerClick.RemoveAllListeners();
			this.m_csbtnBattleRecord.PointerClick.AddListener(new UnityAction(this.OnClickBattleHistory));
			this.m_csbtnBattleReady.PointerClick.RemoveAllListeners();
			this.m_csbtnBattleReady.PointerClick.AddListener(new UnityAction(this.OnClickBattleReady));
		}

		// Token: 0x06008711 RID: 34577 RVA: 0x002DB374 File Offset: 0x002D9574
		public void SetUI()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				PvpState pvpData = nkmuserData.m_PvpData;
			}
		}

		// Token: 0x06008712 RID: 34578 RVA: 0x002DB394 File Offset: 0x002D9594
		private void OnClickBattleHistory()
		{
			NKC_SCEN_GAUNTLET_LOBBY nkc_SCEN_GAUNTLET_LOBBY = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY();
			if (nkc_SCEN_GAUNTLET_LOBBY != null)
			{
				nkc_SCEN_GAUNTLET_LOBBY.OpenBattleRecord(NKM_GAME_TYPE.NGT_INVALID);
			}
		}

		// Token: 0x06008713 RID: 34579 RVA: 0x002DB3B6 File Offset: 0x002D95B6
		private void OnClickBattleReady()
		{
			Log.Error("일반전 삭제됨", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLobbyNormal.cs", 53);
		}

		// Token: 0x04007383 RID: 29571
		public NKCUIComStateButton m_csbtnBattleRecord;

		// Token: 0x04007384 RID: 29572
		public NKCUIComStateButton m_csbtnBattleReady;

		// Token: 0x04007385 RID: 29573
		public Text m_lbAccumWin;

		// Token: 0x04007386 RID: 29574
		public Text m_lbMaxStreakWin;
	}
}
