using System;
using ClientPacket.Warfare;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000729 RID: 1833
	public class NKC_SCEN_SHADOW_BATTLE : NKC_SCEN_BASIC
	{
		// Token: 0x06004901 RID: 18689 RVA: 0x0015FF0B File Offset: 0x0015E10B
		public NKC_SCEN_SHADOW_BATTLE()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_SHADOW_BATTLE;
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x0015FF1B File Offset: 0x0015E11B
		public void SetShadowPalaceID(int palaceID)
		{
			this.m_palaceID = palaceID;
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x0015FF24 File Offset: 0x0015E124
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_loadUIData))
			{
				this.m_loadUIData = NKCUIShadowBattle.OpenNewInstanceAsync();
			}
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x0015FF44 File Offset: 0x0015E144
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!(this.m_shadowBattle == null))
			{
				return;
			}
			if (this.m_loadUIData != null && this.m_loadUIData.CheckLoadAndGetInstance<NKCUIShadowBattle>(out this.m_shadowBattle))
			{
				this.m_shadowBattle.Init();
				return;
			}
			Debug.LogError("NKC_SCEN_SHADOW_BATTLE.ScenLoadUIComplete - ui load fail");
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x0015FF97 File Offset: 0x0015E197
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIShadowBattle shadowBattle = this.m_shadowBattle;
			if (shadowBattle == null)
			{
				return;
			}
			shadowBattle.Open(this.m_palaceID);
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x0015FFB5 File Offset: 0x0015E1B5
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIShadowBattle shadowBattle = this.m_shadowBattle;
			if (shadowBattle != null)
			{
				shadowBattle.Close();
			}
			this.UnloadUI();
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x0015FFD4 File Offset: 0x0015E1D4
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_shadowBattle = null;
			NKCUIManager.LoadedUIData loadUIData = this.m_loadUIData;
			if (loadUIData != null)
			{
				loadUIData.CloseInstance();
			}
			this.m_loadUIData = null;
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x0015FFFB File Offset: 0x0015E1FB
		public void OnRecv(NKMPacket_WARFARE_GAME_GIVE_UP_ACK sPacket)
		{
			if (NKCUIShadowBattle.IsInstanceOpen)
			{
				this.m_shadowBattle.StartCurrentBattle();
			}
		}

		// Token: 0x04003876 RID: 14454
		private NKCUIShadowBattle m_shadowBattle;

		// Token: 0x04003877 RID: 14455
		private NKCUIManager.LoadedUIData m_loadUIData;

		// Token: 0x04003878 RID: 14456
		private int m_palaceID;
	}
}
