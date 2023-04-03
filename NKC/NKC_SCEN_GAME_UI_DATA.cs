using System;
using NKC.UI.HUD;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200070F RID: 1807
	public class NKC_SCEN_GAME_UI_DATA
	{
		// Token: 0x06004723 RID: 18211 RVA: 0x00158C86 File Offset: 0x00156E86
		public NKC_SCEN_GAME_UI_DATA()
		{
			this.Init();
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x00158C94 File Offset: 0x00156E94
		public void Init()
		{
			this.m_NUF_GAME_PREFAB = null;
			this.m_GAME_BATTLE_MAP = null;
			this.m_GAME_BATTLE_UNIT = null;
			this.m_GAME_BATTLE_UNIT_SHADOW = null;
			this.m_GAME_BATTLE_UNIT_MOTION_BLUR = null;
			this.m_GAME_BATTLE_UNIT_VIEWER = null;
			this.m_NUF_GAME_HUD_MINI_MAP = null;
			this.m_NUM_GAME_BATTLE_EFFECT = null;
			this.m_NUFGameObjects = null;
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x00158CE0 File Offset: 0x00156EE0
		public GameObject Get_GAME_BATTLE_MAP()
		{
			return this.m_GAME_BATTLE_MAP;
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x00158CE8 File Offset: 0x00156EE8
		public GameObject Get_GAME_BATTLE_UNIT()
		{
			return this.m_GAME_BATTLE_UNIT;
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x00158CF0 File Offset: 0x00156EF0
		public GameObject Get_GAME_BATTLE_UNIT_SHADOW()
		{
			return this.m_GAME_BATTLE_UNIT_SHADOW;
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x00158CF8 File Offset: 0x00156EF8
		public GameObject Get_GAME_BATTLE_UNIT_MOTION_BLUR()
		{
			return this.m_GAME_BATTLE_UNIT_MOTION_BLUR;
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x00158D00 File Offset: 0x00156F00
		public GameObject Get_GAME_BATTLE_UNIT_VIEWER()
		{
			return this.m_GAME_BATTLE_UNIT_VIEWER;
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x00158D08 File Offset: 0x00156F08
		public GameObject Get_NUF_GAME_HUD_MINI_MAP()
		{
			return this.m_NUF_GAME_HUD_MINI_MAP;
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x00158D10 File Offset: 0x00156F10
		public GameObject Get_NUM_GAME_BATTLE_EFFECT()
		{
			return this.m_NUM_GAME_BATTLE_EFFECT;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x00158D18 File Offset: 0x00156F18
		public NKCGameHudObjects GetHudObjects()
		{
			return this.m_NUFGameObjects;
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x00158D20 File Offset: 0x00156F20
		public GameObject Get_NUF_BEFORE_HUD_EFFECT()
		{
			if (!(this.m_NUFGameObjects != null))
			{
				return null;
			}
			return this.m_NUFGameObjects.m_NUF_BEFORE_HUD_EFFECT;
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00158D3D File Offset: 0x00156F3D
		public GameObject Get_NUF_BEFORE_HUD_CONTROL_EFFECT_ANCHOR()
		{
			if (!(this.m_NUFGameObjects != null))
			{
				return null;
			}
			return this.m_NUFGameObjects.m_NUF_BEFORE_HUD_CONTROL_EFFECT_ANCHOR;
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x00158D5A File Offset: 0x00156F5A
		public GameObject Get_NUF_BEFORE_HUD_CONTROL_EFFECT()
		{
			if (!(this.m_NUFGameObjects != null))
			{
				return null;
			}
			return this.m_NUFGameObjects.m_NUF_BEFORE_HUD_CONTROL_EFFECT;
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x00158D77 File Offset: 0x00156F77
		public GameObject Get_NUF_AFTER_HUD_EFFECT()
		{
			if (!(this.m_NUFGameObjects != null))
			{
				return null;
			}
			return this.m_NUFGameObjects.m_NUF_AFTER_HUD_EFFECT;
		}

		// Token: 0x040037C8 RID: 14280
		public NKCAssetInstanceData m_NUF_GAME_PREFAB;

		// Token: 0x040037C9 RID: 14281
		public GameObject m_GAME_BATTLE_MAP;

		// Token: 0x040037CA RID: 14282
		public GameObject m_GAME_BATTLE_UNIT;

		// Token: 0x040037CB RID: 14283
		public GameObject m_GAME_BATTLE_UNIT_SHADOW;

		// Token: 0x040037CC RID: 14284
		public GameObject m_GAME_BATTLE_UNIT_MOTION_BLUR;

		// Token: 0x040037CD RID: 14285
		public GameObject m_GAME_BATTLE_UNIT_VIEWER;

		// Token: 0x040037CE RID: 14286
		public GameObject m_NUF_GAME_HUD_MINI_MAP;

		// Token: 0x040037CF RID: 14287
		public GameObject m_NUM_GAME_BATTLE_EFFECT;

		// Token: 0x040037D0 RID: 14288
		public NKCGameHudObjects m_NUFGameObjects;
	}
}
