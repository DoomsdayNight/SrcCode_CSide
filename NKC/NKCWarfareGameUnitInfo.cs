using System;
using ClientPacket.Community;
using ClientPacket.Guild;
using ClientPacket.Warfare;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007E7 RID: 2023
	public class NKCWarfareGameUnitInfo : MonoBehaviour
	{
		// Token: 0x06005013 RID: 20499 RVA: 0x001834D5 File Offset: 0x001816D5
		public void SetFlag(bool bSet)
		{
			this.m_bFlag = bSet;
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x001834DE File Offset: 0x001816DE
		public bool GetFlag()
		{
			return this.m_bFlag;
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x001834E6 File Offset: 0x001816E6
		public void SetTartget(bool bSet)
		{
			this.m_bTarget = bSet;
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x001834EF File Offset: 0x001816EF
		public bool GetTarget()
		{
			return this.m_bTarget;
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x001834F8 File Offset: 0x001816F8
		public static NKCWarfareGameUnitInfo GetNewInstance(Transform parent, Transform unitTrans)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_UNIT_INFO", false, null);
			NKCWarfareGameUnitInfo component = nkcassetInstanceData.m_Instant.GetComponent<NKCWarfareGameUnitInfo>();
			if (component == null)
			{
				Debug.LogError("NKCWarfareGameUnitInfo Prefab null!");
				return null;
			}
			component.m_Instance = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			component.m_UnitTransform = unitTrans;
			component.m_animator = component.GetComponent<Animator>();
			return component;
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x00183588 File Offset: 0x00181788
		private NKMWarfareMapTemplet GetNKMWarfareMapTemplet()
		{
			if (this.m_NKMWarfareMapTemplet == null)
			{
				WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData != null)
				{
					NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
					if (nkmwarfareTemplet != null)
					{
						this.m_NKMWarfareMapTemplet = nkmwarfareTemplet.MapTemplet;
					}
				}
			}
			return this.m_NKMWarfareMapTemplet;
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x001835CC File Offset: 0x001817CC
		public void Close()
		{
			if (this.m_flagSequence != null)
			{
				this.m_flagSequence.Kill(false);
				this.m_flagSequence = null;
			}
			if (this.m_NUM_WARFARE_UNIT_FLAG != null)
			{
				this.m_NUM_WARFARE_UNIT_FLAG.transform.DOKill(false);
			}
			if (this.m_Instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_Instance);
			}
			this.m_Instance = null;
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x0600501A RID: 20506 RVA: 0x0018362E File Offset: 0x0018182E
		public int TileIndex
		{
			get
			{
				return (int)this.m_NKMWarfareUnitData.tileIndex;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x0600501B RID: 20507 RVA: 0x0018363B File Offset: 0x0018183B
		public bool IsSupporter
		{
			get
			{
				return this.m_NKMWarfareUnitData.friendCode != 0L;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x0018364C File Offset: 0x0018184C
		public long FriendCode
		{
			get
			{
				return this.m_NKMWarfareUnitData.friendCode;
			}
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0018365C File Offset: 0x0018185C
		public void PlayFlagAni()
		{
			if (this.m_NUM_WARFARE_UNIT_FLAG != null && this.m_NUM_WARFARE_UNIT_FLAG.activeSelf)
			{
				this.m_NUM_WARFARE_UNIT_FLAG.transform.localScale = new Vector3(1f, 1f, 1f);
				if (this.m_flagSequence != null)
				{
					this.m_flagSequence.Kill(false);
					this.m_flagSequence = null;
				}
				this.m_flagSequence = DOTween.Sequence();
				this.m_flagSequence.Append(this.m_NUM_WARFARE_UNIT_FLAG.transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 0.7f).From<TweenerCore<Vector3, Vector3, VectorOptions>>().SetEase(Ease.OutQuad));
				this.m_flagSequence.Join(this.m_NUM_WARFARE_UNIT_FLAG.transform.DOMoveY(20f, 0.7f, false).From(true).SetEase(Ease.OutQuad));
			}
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x00183748 File Offset: 0x00181948
		public void PlayAni(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION aniType)
		{
			string text = string.Empty;
			if (aniType != NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION.NWGUA_RUNAWAY)
			{
				if (aniType == NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION.NWGUA_ENTER)
				{
					text = "NUM_WARFARE_UNIT_INFO_ENTER";
				}
			}
			else
			{
				text = "NUM_WARFARE_UNIT_INFO_RUNAWAY";
			}
			if (text != string.Empty)
			{
				this.m_animator.Play(text);
			}
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x0018378C File Offset: 0x0018198C
		public void SetUnitTransform(Transform trans)
		{
			this.m_UnitTransform = trans;
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x00183795 File Offset: 0x00181995
		public void SetNKMWarfareUnitData(WarfareUnitData cNKMWarfareUnitData)
		{
			this.m_NKMWarfareUnitData = cNKMWarfareUnitData;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0018379E File Offset: 0x0018199E
		public WarfareUnitData GetNKMWarfareUnitData()
		{
			return this.m_NKMWarfareUnitData;
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x001837A8 File Offset: 0x001819A8
		private void SetSuuplyCountUI(int count)
		{
			if (count <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLE_POINT_ACTIVE_1, false);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLE_POINT_ACTIVE_2, false);
			}
			if (count == 1)
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLE_POINT_ACTIVE_1, true);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLE_POINT_ACTIVE_2, false);
			}
			if (count >= 2)
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLE_POINT_ACTIVE_1, true);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLE_POINT_ACTIVE_2, true);
			}
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x0018380C File Offset: 0x00181A0C
		public void SetUnitInfoUI()
		{
			if (this.m_NKMWarfareUnitData == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_FLAG, this.m_bFlag);
			if (this.m_NUM_WARFARE_UNIT_FLAG != null)
			{
				this.m_NUM_WARFARE_UNIT_FLAG.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_TARGET, this.m_bTarget);
			if (this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.User)
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLE_POINT, true);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_INCR, false);
				this.SetMovableIcon(false, 1);
				this.SetActionTypeIcon(false, NKM_WARFARE_ENEMY_ACTION_TYPE.NWEAT_NONE);
				bool flag = NKCGuildManager.HasGuild() && NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.friendCode == this.m_NKMWarfareUnitData.friendCode) != null;
				bool flag2 = NKCWarfareManager.IsGeustSupporter(this.m_NKMWarfareUnitData.friendCode);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_DECKNUMBER, !this.IsSupporter);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_DECKNUMBER_SUPPORT, this.IsSupporter && !flag && !flag2);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_DECKNUMBER_FRIEND_GUEST, this.IsSupporter && !flag && flag2);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_DECKNUMBER_GUILD_GUEST, this.IsSupporter && flag);
				bool flag3 = NKCWarfareManager.CheckOnTileType(this.GetNKMWarfareMapTemplet(), this.TileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWMTT_REPAIR);
				bool flag4 = NKCWarfareManager.CheckOnTileType(this.GetNKMWarfareMapTemplet(), this.TileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWMTT_RESUPPLY);
				bool flag5 = NKCWarfareManager.CheckOnTileType(this.GetNKMWarfareMapTemplet(), this.TileIndex, NKM_WARFARE_MAP_TILE_TYPE.NWNTT_SERVICE);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_SPECIALTILE_REPAIR, flag3 || flag4 || flag5);
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				string hexRGB;
				if (!this.IsSupporter)
				{
					hexRGB = "#5AFB55";
					NKMDeckIndex deckIndex = this.m_NKMWarfareUnitData.deckIndex;
					this.m_NUM_WARFARE_UNIT_DECKNUMBER_COUNT.text = ((int)(deckIndex.m_iIndex + 1)).ToString();
					NKMUnitData deckLeaderUnitData = myUserData.m_ArmyData.GetDeckLeaderUnitData(deckIndex);
					if (deckLeaderUnitData != null)
					{
						this.m_NUM_WARFARE_UNIT_LV_TEXT.text = deckLeaderUnitData.m_UnitLevel.ToString();
					}
				}
				else
				{
					hexRGB = "#FFC501";
					WarfareSupporterListData supportUnitData = NKCScenManager.GetScenManager().WarfareGameData.supportUnitData;
					if (supportUnitData != null && supportUnitData.commonProfile.friendCode == this.m_NKMWarfareUnitData.friendCode)
					{
						NKMDummyUnitData nkmdummyUnitData = supportUnitData.deckData.List[(int)supportUnitData.deckData.LeaderIndex];
						this.m_NUM_WARFARE_UNIT_LV_TEXT.text = nkmdummyUnitData.UnitLevel.ToString();
					}
				}
				this.SetSuuplyCountUI(this.m_NKMWarfareUnitData.supply);
				this.m_NUM_WARFARE_UNIT_HP_1_Img.color = NKCUtil.GetColor(hexRGB);
				this.m_NUM_WARFARE_UNIT_HP_2_Img.color = this.m_NUM_WARFARE_UNIT_HP_1_Img.color;
				this.m_NUM_WARFARE_UNIT_HP_3_Img.color = this.m_NUM_WARFARE_UNIT_HP_1_Img.color;
				this.UpdateHPBarUI();
				return;
			}
			if (this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.Dungeon)
			{
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLE_POINT, false);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_INCR, this.m_NKMWarfareUnitData.isSummonee);
				this.SetMovableIcon(this.IsMovableActionType(), 1);
				this.SetActionTypeIcon(true, this.m_NKMWarfareUnitData.warfareEnemyActionType);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_DECKNUMBER, false);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_SPECIALTILE_REPAIR, false);
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_NKMWarfareUnitData.dungeonID);
				if (dungeonTempletBase != null)
				{
					NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_HP, dungeonTempletBase.m_DungeonType != NKM_DUNGEON_TYPE.NDT_WAVE);
					NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_ENEMY_WAVE, dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE);
					this.m_NUM_WARFARE_UNIT_LV_TEXT.text = dungeonTempletBase.m_DungeonLevel.ToString();
					if (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
					{
						NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(dungeonTempletBase.m_DungeonID);
						if (dungeonTemplet != null)
						{
							this.m_NUM_WARFARE_UNIT_ENEMY_WAVE_TEXT.text = string.Format(NKCUtilString.GET_STRING_WARFARE_WAVE_ONE_PARAM, dungeonTemplet.m_listDungeonWave.Count);
						}
					}
				}
				this.m_NUM_WARFARE_UNIT_HP_1_Img.color = new Color(1f, 0f, 0.30588236f);
				this.m_NUM_WARFARE_UNIT_HP_2_Img.color = this.m_NUM_WARFARE_UNIT_HP_1_Img.color;
				this.m_NUM_WARFARE_UNIT_HP_3_Img.color = this.m_NUM_WARFARE_UNIT_HP_1_Img.color;
				this.UpdateHPBarUI();
			}
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x00183C1F File Offset: 0x00181E1F
		private float GetProperRatioValue(float fRatio)
		{
			if (fRatio < 0f)
			{
				fRatio = 0f;
			}
			if (fRatio > 1f)
			{
				fRatio = 1f;
			}
			return fRatio;
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x00183C40 File Offset: 0x00181E40
		public void OnPlayServiceSound(NKM_WARFARE_SERVICE_TYPE serviceType)
		{
			if (this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.User && serviceType == NKM_WARFARE_SERVICE_TYPE.NWST_RESUPPLY)
			{
				NKCOperatorUtil.PlayVoice(this.m_NKMWarfareUnitData.deckIndex, VOICE_TYPE.VT_BULLET_FILL, true);
			}
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x00183C68 File Offset: 0x00181E68
		private void UpdateHPBarUI()
		{
			if (this.m_NKMWarfareUnitData != null)
			{
				if (this.m_NKMWarfareUnitData.hpMax == 0f)
				{
					this.m_NUM_WARFARE_UNIT_HP_1_Img.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NUM_WARFARE_UNIT_HP_2_Img.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NUM_WARFARE_UNIT_HP_3_Img.transform.localScale = new Vector3(1f, 1f, 1f);
					return;
				}
				float num = this.m_NKMWarfareUnitData.hp / this.m_NKMWarfareUnitData.hpMax;
				num = this.GetProperRatioValue(num);
				if (num > 0.6f)
				{
					this.m_NUM_WARFARE_UNIT_HP_1_Img.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NUM_WARFARE_UNIT_HP_2_Img.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NUM_WARFARE_UNIT_HP_3_Img.transform.localScale = new Vector3(this.GetProperRatioValue((num - 0.6f) / 0.4f), 1f, 1f);
					return;
				}
				if (num > 0.3f)
				{
					this.m_NUM_WARFARE_UNIT_HP_1_Img.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_NUM_WARFARE_UNIT_HP_2_Img.transform.localScale = new Vector3(this.GetProperRatioValue((num - 0.3f) / 0.3f), 1f, 1f);
					this.m_NUM_WARFARE_UNIT_HP_3_Img.transform.localScale = new Vector3(0f, 1f, 1f);
					return;
				}
				this.m_NUM_WARFARE_UNIT_HP_1_Img.transform.localScale = new Vector3(this.GetProperRatioValue(num / 0.3f), 1f, 1f);
				this.m_NUM_WARFARE_UNIT_HP_2_Img.transform.localScale = new Vector3(0f, 1f, 1f);
				this.m_NUM_WARFARE_UNIT_HP_3_Img.transform.localScale = new Vector3(0f, 1f, 1f);
			}
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x00183E99 File Offset: 0x00182099
		private void SetMovableIcon(bool active, int movable = 1)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_ENEMY_MOVE, active);
			if (active)
			{
				this.m_NUM_WARFARE_UNIT_ENEMY_MOVE2.text = movable.ToString();
			}
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x00183EBC File Offset: 0x001820BC
		private void SetActionTypeIcon(bool active, NKM_WARFARE_ENEMY_ACTION_TYPE actionType = NKM_WARFARE_ENEMY_ACTION_TYPE.NWEAT_NONE)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_ENEMY_MOVE_TOLEADER, active && actionType == NKM_WARFARE_ENEMY_ACTION_TYPE.NWEAT_ONLY_FLAG_SHIP_ATK);
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x00183ED3 File Offset: 0x001820D3
		private void Update()
		{
			if (this.m_UnitTransform != null)
			{
				base.gameObject.transform.localPosition = this.m_UnitTransform.localPosition;
			}
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x00183F00 File Offset: 0x00182100
		public bool IsMovableActionType()
		{
			return this.m_NKMWarfareUnitData != null && (this.m_NKMWarfareUnitData.warfareEnemyActionType == NKM_WARFARE_ENEMY_ACTION_TYPE.NWEAT_NEAREST_ATK || this.m_NKMWarfareUnitData.warfareEnemyActionType == NKM_WARFARE_ENEMY_ACTION_TYPE.NWEAT_ONLY_FLAG_SHIP_ATK || this.m_NKMWarfareUnitData.warfareEnemyActionType == NKM_WARFARE_ENEMY_ACTION_TYPE.NWEAT_FIND_WIN_TILE || this.m_NKMWarfareUnitData.warfareEnemyActionType == NKM_WARFARE_ENEMY_ACTION_TYPE.NWEAT_FIND_LOSE_TILE);
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x00183F51 File Offset: 0x00182151
		public void SetBattleAssistIcon(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_UNIT_BATTLEASSIST, bActive);
		}

		// Token: 0x04004016 RID: 16406
		public GameObject m_NUM_WARFARE_UNIT_FLAG;

		// Token: 0x04004017 RID: 16407
		public GameObject m_NUM_WARFARE_UNIT_TARGET;

		// Token: 0x04004018 RID: 16408
		public GameObject m_NUM_WARFARE_UNIT_INCR;

		// Token: 0x04004019 RID: 16409
		public Text m_NUM_WARFARE_UNIT_LV_TEXT;

		// Token: 0x0400401A RID: 16410
		public GameObject m_NUM_WARFARE_UNIT_HP;

		// Token: 0x0400401B RID: 16411
		public GameObject m_NUM_WARFARE_UNIT_HP_1;

		// Token: 0x0400401C RID: 16412
		public Image m_NUM_WARFARE_UNIT_HP_1_Img;

		// Token: 0x0400401D RID: 16413
		public GameObject m_NUM_WARFARE_UNIT_HP_2;

		// Token: 0x0400401E RID: 16414
		public Image m_NUM_WARFARE_UNIT_HP_2_Img;

		// Token: 0x0400401F RID: 16415
		public GameObject m_NUM_WARFARE_UNIT_HP_3;

		// Token: 0x04004020 RID: 16416
		public Image m_NUM_WARFARE_UNIT_HP_3_Img;

		// Token: 0x04004021 RID: 16417
		public GameObject m_NUM_WARFARE_UNIT_BATTLE_POINT;

		// Token: 0x04004022 RID: 16418
		public GameObject m_NUM_WARFARE_UNIT_BATTLE_POINT_ACTIVE_1;

		// Token: 0x04004023 RID: 16419
		public GameObject m_NUM_WARFARE_UNIT_BATTLE_POINT_ACTIVE_2;

		// Token: 0x04004024 RID: 16420
		public GameObject m_NUM_WARFARE_UNIT_ENEMY_WAVE;

		// Token: 0x04004025 RID: 16421
		public Text m_NUM_WARFARE_UNIT_ENEMY_WAVE_TEXT;

		// Token: 0x04004026 RID: 16422
		public GameObject m_NUM_WARFARE_UNIT_ENEMY_MOVE;

		// Token: 0x04004027 RID: 16423
		public Text m_NUM_WARFARE_UNIT_ENEMY_MOVE2;

		// Token: 0x04004028 RID: 16424
		public GameObject m_NUM_WARFARE_UNIT_ENEMY_MOVE_TOLEADER;

		// Token: 0x04004029 RID: 16425
		public GameObject m_NUM_WARFARE_UNIT_DECKNUMBER;

		// Token: 0x0400402A RID: 16426
		public Text m_NUM_WARFARE_UNIT_DECKNUMBER_COUNT;

		// Token: 0x0400402B RID: 16427
		public GameObject m_NUM_WARFARE_UNIT_DECKNUMBER_SUPPORT;

		// Token: 0x0400402C RID: 16428
		public GameObject m_NUM_WARFARE_UNIT_DECKNUMBER_FRIEND_GUEST;

		// Token: 0x0400402D RID: 16429
		public GameObject m_NUM_WARFARE_UNIT_DECKNUMBER_GUILD_GUEST;

		// Token: 0x0400402E RID: 16430
		public GameObject m_NUM_WARFARE_UNIT_SPECIALTILE_REPAIR;

		// Token: 0x0400402F RID: 16431
		public GameObject m_NUM_WARFARE_UNIT_BATTLEASSIST;

		// Token: 0x04004030 RID: 16432
		private Transform m_UnitTransform;

		// Token: 0x04004031 RID: 16433
		private WarfareUnitData m_NKMWarfareUnitData;

		// Token: 0x04004032 RID: 16434
		private bool m_bFlag;

		// Token: 0x04004033 RID: 16435
		private bool m_bTarget;

		// Token: 0x04004034 RID: 16436
		private NKCAssetInstanceData m_Instance;

		// Token: 0x04004035 RID: 16437
		private Animator m_animator;

		// Token: 0x04004036 RID: 16438
		private NKMWarfareMapTemplet m_NKMWarfareMapTemplet;

		// Token: 0x04004037 RID: 16439
		private Sequence m_flagSequence;
	}
}
