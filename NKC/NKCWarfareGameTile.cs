using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x020007E4 RID: 2020
	public class NKCWarfareGameTile : MonoBehaviour
	{
		// Token: 0x06004FD5 RID: 20437 RVA: 0x0018211C File Offset: 0x0018031C
		public static NKCWarfareGameTile GetNewInstance(int tileIndex, Transform parent, NKCWarfareGameTile.onClickPossibleArrivalTile _onClickPossibleArrivalTile)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_TILE", false, null);
			NKCWarfareGameTile component = nkcassetInstanceData.m_Instant.GetComponent<NKCWarfareGameTile>();
			if (component == null)
			{
				Debug.LogError("NKCWarfareGameTile Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			component.m_OnClickPossibleArrivalTile = _onClickPossibleArrivalTile;
			component.m_TileIndex = tileIndex;
			component.m_btn_M_WARFARE_TILE_B_READY_GO.PointerClick.RemoveAllListeners();
			component.m_btn_M_WARFARE_TILE_B_READY_GO.PointerClick.AddListener(new UnityAction(component.OnClickPossibleArrivalTile));
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			return component;
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x001821D2 File Offset: 0x001803D2
		public void Close()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
			this.m_instance = null;
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x001821E6 File Offset: 0x001803E6
		public void OnClickPossibleArrivalTile()
		{
			if (this.m_OnClickPossibleArrivalTile != null)
			{
				this.m_OnClickPossibleArrivalTile(this.m_TileIndex);
			}
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x00182204 File Offset: 0x00180404
		public void SetTileLayer0Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE eWARFARE_TILE_LAYER_0_TYPE)
		{
			if (eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_NONE)
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_DUMMY, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_DUMMY, true);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_A_NORMAL, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_A_DIVE_POINT, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_DIVE_POINT);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_A_ASSULT_POINT, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_ASSULT_POINT);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_A_RECOVERY_POINT, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_RECOVERY_POINT);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_A_ENEMY, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_ENEMY);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_B_NORMAL, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_NORMAL);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_B_ENEMY, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_ENEMY);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_B_READY, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_MOVABLE_USER_UNIT);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_B_READY_SELECT, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_MOVABLE_USER_UNIT_SELECTED);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_B_BATTLE, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_BATTLE);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_B_DONE, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_TURN_FINISHED);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_B_DONE_SELECT, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_TURN_FINISHED_SELECTED);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_B_READY_GO, eWARFARE_TILE_LAYER_0_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_POSSIBLE_ARRIVAL);
			}
			this.m_WARFARE_TILE_LAYER_0_TYPE = eWARFARE_TILE_LAYER_0_TYPE;
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x00182300 File Offset: 0x00180500
		public GameObject GetActiveGameObject()
		{
			switch (this.m_WARFARE_TILE_LAYER_0_TYPE)
			{
			default:
				return this.m_NUM_WARFARE_TILE_A_NORMAL;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_DIVE_POINT:
				return this.m_NUM_WARFARE_TILE_A_DIVE_POINT;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_ASSULT_POINT:
				return this.m_NUM_WARFARE_TILE_A_ASSULT_POINT;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_ENEMY:
				return this.m_NUM_WARFARE_TILE_A_ENEMY;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_NORMAL:
				return this.m_NUM_WARFARE_TILE_B_NORMAL;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_ENEMY:
				return this.m_NUM_WARFARE_TILE_B_ENEMY;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_MOVABLE_USER_UNIT:
				return this.m_NUM_WARFARE_TILE_B_READY;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_MOVABLE_USER_UNIT_SELECTED:
				return this.m_NUM_WARFARE_TILE_B_READY_SELECT;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_BATTLE:
				return this.m_NUM_WARFARE_TILE_B_BATTLE;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_TURN_FINISHED:
				return this.m_NUM_WARFARE_TILE_B_DONE;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_TURN_FINISHED_SELECTED:
				return this.m_NUM_WARFARE_TILE_B_DONE_SELECT;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_UNIT_POSSIBLE_ARRIVAL:
				return this.m_NUM_WARFARE_TILE_B_READY_GO;
			case NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_PLAY_USER_RECOVERY_POINT:
				return this.m_NUM_WARFARE_TILE_A_RECOVERY_POINT;
			}
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x001823AA File Offset: 0x001805AA
		public NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE Get_WARFARE_TILE_LAYER_0_TYPE()
		{
			return this.m_WARFARE_TILE_LAYER_0_TYPE;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x001823B2 File Offset: 0x001805B2
		public void SetTileLayer1Type(NKM_WARFARE_MAP_TILE_TYPE _NKM_WARFARE_MAP_TILE_TYPE)
		{
			if (_NKM_WARFARE_MAP_TILE_TYPE == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_REPAIR)
			{
				this.SetTileLayer1Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_EFFECT_REPAIR);
				return;
			}
			if (_NKM_WARFARE_MAP_TILE_TYPE == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_RESUPPLY)
			{
				this.SetTileLayer1Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_EFFECT_SUPPLY);
				return;
			}
			if (_NKM_WARFARE_MAP_TILE_TYPE == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_INCR)
			{
				this.SetTileLayer1Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_EFFECT_INCR);
				return;
			}
			if (_NKM_WARFARE_MAP_TILE_TYPE == NKM_WARFARE_MAP_TILE_TYPE.NWNTT_SERVICE)
			{
				this.SetTileLayer1Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_EFFECT_SERVICE);
				return;
			}
			this.SetTileLayer1Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_NONE);
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x001823EC File Offset: 0x001805EC
		public void SetTileLayer1Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE eWARFARE_TILE_LAYER_1_TYPE)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_EFFECT_REPAIR, eWARFARE_TILE_LAYER_1_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_EFFECT_REPAIR);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_EFFECT_SUPPLY, eWARFARE_TILE_LAYER_1_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_EFFECT_SUPPLY);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_EFFECT_SERVICE, eWARFARE_TILE_LAYER_1_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_EFFECT_SERVICE);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_EFFECT_INCR, eWARFARE_TILE_LAYER_1_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE.WTL1T_EFFECT_INCR);
			this.m_WARFARE_TILE_LAYER_1_TYPE = eWARFARE_TILE_LAYER_1_TYPE;
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0018243C File Offset: 0x0018063C
		public NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE Get_WARFARE_TILE_LAYER_1_TYPE()
		{
			return this.m_WARFARE_TILE_LAYER_1_TYPE;
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x00182444 File Offset: 0x00180644
		public void SetTileLayer2Type(WARFARE_GAME_CONDITION _NKM_WARFARE_MAP_TILE_WIN_TYPE, WARFARE_GAME_CONDITION _NKM_WARFARE_MAP_TILE_LOSE_TYPE)
		{
			if (_NKM_WARFARE_MAP_TILE_WIN_TYPE == WARFARE_GAME_CONDITION.WFC_TILE_ENTER)
			{
				this.SetTileLayer2Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE.WTL2T_WIN_ENTER);
				return;
			}
			if (_NKM_WARFARE_MAP_TILE_WIN_TYPE == WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD)
			{
				this.SetTileLayer2Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE.WTL2T_WIN_HOLD);
				return;
			}
			if (_NKM_WARFARE_MAP_TILE_LOSE_TYPE == WARFARE_GAME_CONDITION.WFC_TILE_ENTER)
			{
				this.SetTileLayer2Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE.WTL2T_LOSE_DEFENSE);
				return;
			}
			this.SetTileLayer2Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE.WTL2T_NONE);
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x00182471 File Offset: 0x00180671
		public void SetTileLayer2Type(NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE eWARFARE_TILE_LAYER_2_TYPE)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_A_WC_ENTER, eWARFARE_TILE_LAYER_2_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE.WTL2T_WIN_ENTER);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_A_WC_HOLD, eWARFARE_TILE_LAYER_2_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE.WTL2T_WIN_HOLD);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_TILE_A_WC_DEFENSE, eWARFARE_TILE_LAYER_2_TYPE == NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE.WTL2T_LOSE_DEFENSE);
			this.m_WARFARE_TILE_LAYER_2_TYPE = eWARFARE_TILE_LAYER_2_TYPE;
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x001824A7 File Offset: 0x001806A7
		public NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE Get_WARFARE_TILE_LAYER_2_TYPE()
		{
			return this.m_WARFARE_TILE_LAYER_2_TYPE;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x001824AF File Offset: 0x001806AF
		private void Update()
		{
			bool activeSelf = this.m_NUM_WARFARE_DUMMY.activeSelf;
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x001824BD File Offset: 0x001806BD
		public void SetDummyActive(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_DUMMY, bSet);
		}

		// Token: 0x04003FDD RID: 16349
		public GameObject m_NUM_WARFARE_DUMMY;

		// Token: 0x04003FDE RID: 16350
		public GameObject m_NUM_WARFARE_TILE_A_NORMAL;

		// Token: 0x04003FDF RID: 16351
		public GameObject m_NUM_WARFARE_TILE_A_DIVE_POINT;

		// Token: 0x04003FE0 RID: 16352
		public GameObject m_NUM_WARFARE_TILE_A_ASSULT_POINT;

		// Token: 0x04003FE1 RID: 16353
		public GameObject m_NUM_WARFARE_TILE_A_RECOVERY_POINT;

		// Token: 0x04003FE2 RID: 16354
		public GameObject m_NUM_WARFARE_TILE_A_ENEMY;

		// Token: 0x04003FE3 RID: 16355
		public GameObject m_NUM_WARFARE_TILE_A_WC_ENTER;

		// Token: 0x04003FE4 RID: 16356
		public GameObject m_NUM_WARFARE_TILE_A_WC_HOLD;

		// Token: 0x04003FE5 RID: 16357
		public GameObject m_NUM_WARFARE_TILE_A_WC_DEFENSE;

		// Token: 0x04003FE6 RID: 16358
		public GameObject m_NUM_WARFARE_TILE_B_NORMAL;

		// Token: 0x04003FE7 RID: 16359
		public GameObject m_NUM_WARFARE_TILE_B_ENEMY;

		// Token: 0x04003FE8 RID: 16360
		public GameObject m_NUM_WARFARE_TILE_B_READY;

		// Token: 0x04003FE9 RID: 16361
		public GameObject m_NUM_WARFARE_TILE_B_READY_SELECT;

		// Token: 0x04003FEA RID: 16362
		public GameObject m_NUM_WARFARE_TILE_B_BATTLE;

		// Token: 0x04003FEB RID: 16363
		public GameObject m_NUM_WARFARE_TILE_B_DONE;

		// Token: 0x04003FEC RID: 16364
		public GameObject m_NUM_WARFARE_TILE_B_DONE_SELECT;

		// Token: 0x04003FED RID: 16365
		public GameObject m_NUM_WARFARE_TILE_B_READY_GO;

		// Token: 0x04003FEE RID: 16366
		public GameObject m_NUM_WARFARE_TILE_EFFECT_REPAIR;

		// Token: 0x04003FEF RID: 16367
		public GameObject m_NUM_WARFARE_TILE_EFFECT_SUPPLY;

		// Token: 0x04003FF0 RID: 16368
		public GameObject m_NUM_WARFARE_TILE_EFFECT_SERVICE;

		// Token: 0x04003FF1 RID: 16369
		public GameObject m_NUM_WARFARE_TILE_EFFECT_INCR;

		// Token: 0x04003FF2 RID: 16370
		public NKCUIComButton m_btn_M_WARFARE_TILE_B_READY_GO;

		// Token: 0x04003FF3 RID: 16371
		private NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE m_WARFARE_TILE_LAYER_0_TYPE = NKCWarfareGameTile.WARFARE_TILE_LAYER_0_TYPE.WTL0T_READY_NORMAL;

		// Token: 0x04003FF4 RID: 16372
		private NKCWarfareGameTile.WARFARE_TILE_LAYER_1_TYPE m_WARFARE_TILE_LAYER_1_TYPE;

		// Token: 0x04003FF5 RID: 16373
		private NKCWarfareGameTile.WARFARE_TILE_LAYER_2_TYPE m_WARFARE_TILE_LAYER_2_TYPE;

		// Token: 0x04003FF6 RID: 16374
		private NKCWarfareGameTile.onClickPossibleArrivalTile m_OnClickPossibleArrivalTile;

		// Token: 0x04003FF7 RID: 16375
		private int m_TileIndex = -1;

		// Token: 0x04003FF8 RID: 16376
		private NKCAssetInstanceData m_instance;

		// Token: 0x02001499 RID: 5273
		public enum WARFARE_TILE_LAYER_0_TYPE
		{
			// Token: 0x04009E7F RID: 40575
			WTL0T_NONE,
			// Token: 0x04009E80 RID: 40576
			WTL0T_READY_NORMAL,
			// Token: 0x04009E81 RID: 40577
			WTL0T_READY_DIVE_POINT,
			// Token: 0x04009E82 RID: 40578
			WTL0T_READY_ASSULT_POINT,
			// Token: 0x04009E83 RID: 40579
			WTL0T_READY_ENEMY,
			// Token: 0x04009E84 RID: 40580
			WTL0T_PLAY_NORMAL,
			// Token: 0x04009E85 RID: 40581
			WTL0T_PLAY_ENEMY,
			// Token: 0x04009E86 RID: 40582
			WTL0T_PLAY_MOVABLE_USER_UNIT,
			// Token: 0x04009E87 RID: 40583
			WTL0T_PLAY_MOVABLE_USER_UNIT_SELECTED,
			// Token: 0x04009E88 RID: 40584
			WTL0T_PLAY_BATTLE,
			// Token: 0x04009E89 RID: 40585
			WTL0T_PLAY_USER_UNIT_TURN_FINISHED,
			// Token: 0x04009E8A RID: 40586
			WTL0T_PLAY_USER_UNIT_TURN_FINISHED_SELECTED,
			// Token: 0x04009E8B RID: 40587
			WTL0T_PLAY_USER_UNIT_POSSIBLE_ARRIVAL,
			// Token: 0x04009E8C RID: 40588
			WTL0T_PLAY_USER_RECOVERY_POINT
		}

		// Token: 0x0200149A RID: 5274
		public enum WARFARE_TILE_LAYER_1_TYPE
		{
			// Token: 0x04009E8E RID: 40590
			WTL1T_NONE,
			// Token: 0x04009E8F RID: 40591
			WTL1T_EFFECT_REPAIR,
			// Token: 0x04009E90 RID: 40592
			WTL1T_EFFECT_SUPPLY,
			// Token: 0x04009E91 RID: 40593
			WTL1T_EFFECT_SERVICE,
			// Token: 0x04009E92 RID: 40594
			WTL1T_EFFECT_INCR
		}

		// Token: 0x0200149B RID: 5275
		public enum WARFARE_TILE_LAYER_2_TYPE
		{
			// Token: 0x04009E94 RID: 40596
			WTL2T_NONE,
			// Token: 0x04009E95 RID: 40597
			WTL2T_WIN_ENTER,
			// Token: 0x04009E96 RID: 40598
			WTL2T_WIN_HOLD,
			// Token: 0x04009E97 RID: 40599
			WTL2T_LOSE_DEFENSE
		}

		// Token: 0x0200149C RID: 5276
		// (Invoke) Token: 0x0600A95F RID: 43359
		public delegate void onClickPossibleArrivalTile(int tileIndex);
	}
}
