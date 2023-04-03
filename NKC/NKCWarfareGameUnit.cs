using System;
using System.Collections;
using ClientPacket.Community;
using ClientPacket.Warfare;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007E6 RID: 2022
	public class NKCWarfareGameUnit : MonoBehaviour
	{
		// Token: 0x06004FEC RID: 20460 RVA: 0x0018265C File Offset: 0x0018085C
		public static NKCWarfareGameUnit GetNewInstance(Transform parent, NKCWarfareGameUnit.onClickUnit _OnClickUnit)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_UNIT", false, null);
			NKCWarfareGameUnit component = nkcassetInstanceData.m_Instant.GetComponent<NKCWarfareGameUnit>();
			if (component == null)
			{
				Debug.LogError("NKCWarfareGameUnit Prefab null!");
				return null;
			}
			component.m_Instance = nkcassetInstanceData;
			component.m_OnClickUnit = _OnClickUnit;
			component.m_goNUM_WARFARE_USER_UNIT_IMG_orgPos = component.m_goNUM_WARFARE_USER_UNIT_IMG.transform.localPosition;
			component.m_NUM_WARFARE_USER_UNIT_IMG.enabled = false;
			component.m_NUM_WARFARE_USER_UNIT_SHADOW.enabled = false;
			component.m_NKCUIComButton.PointerClick.RemoveAllListeners();
			component.m_NKCUIComButton.PointerClick.AddListener(new UnityAction(component.OnClickUnit));
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			component.m_animator = component.GetComponent<Animator>();
			return component;
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x00182745 File Offset: 0x00180945
		public bool IsMoving()
		{
			return !(this.m_NKCWarfareUnitMover == null) && this.m_NKCWarfareUnitMover.IsRunning();
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x00182764 File Offset: 0x00180964
		public void ShowUserUnitTileFX(WarfareUnitSyncData cNKMWarfareUnitSyncData)
		{
			if (this.m_NKMWarfareUnitData.unitType != WarfareUnitData.Type.User)
			{
				return;
			}
			if (this.m_NKMWarfareUnitData.hp > cNKMWarfareUnitSyncData.hp)
			{
				this.TriggerThunderFX();
				return;
			}
			if (this.m_NKMWarfareUnitData.supply < (int)cNKMWarfareUnitSyncData.supply)
			{
				this.TriggerSupplyFX();
			}
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x001827B2 File Offset: 0x001809B2
		public void TriggerRepairFX()
		{
			if (this.m_NUM_WARFARE_FX_TILE_REPAIR != null)
			{
				this.m_NUM_WARFARE_FX_TILE_REPAIR.SetActive(false);
				this.m_NUM_WARFARE_FX_TILE_REPAIR.SetActive(true);
			}
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x001827DA File Offset: 0x001809DA
		public void TriggerSupplyFX()
		{
			if (this.m_NUM_WARFARE_FX_TILE_SUPPLY != null)
			{
				this.m_NUM_WARFARE_FX_TILE_SUPPLY.SetActive(false);
				this.m_NUM_WARFARE_FX_TILE_SUPPLY.SetActive(true);
			}
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x00182802 File Offset: 0x00180A02
		public void TriggerThunderFX()
		{
			if (this.m_NUM_WARFARE_FX_TILE_THUNDER != null)
			{
				this.m_NUM_WARFARE_FX_TILE_THUNDER.SetActive(false);
				this.m_NUM_WARFARE_FX_TILE_THUNDER.SetActive(true);
			}
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0018282A File Offset: 0x00180A2A
		public void HideFX()
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_TILE_REPAIR, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_TILE_SUPPLY, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_TILE_THUNDER, false);
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x00182850 File Offset: 0x00180A50
		public void SetPause(bool bSet)
		{
			this.m_bPause = bSet;
			if (this.m_NKCWarfareUnitMover != null)
			{
				this.m_NKCWarfareUnitMover.SetPause(bSet);
			}
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x00182874 File Offset: 0x00180A74
		public void SetState(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE _NKC_WARFARE_GAME_UNIT_STATE)
		{
			this.m_NKC_WARFARE_GAME_UNIT_STATE = _NKC_WARFARE_GAME_UNIT_STATE;
			if (this.m_NKC_WARFARE_GAME_UNIT_STATE != NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE.NWGUS_IDLE && this.m_NKC_WARFARE_GAME_UNIT_STATE == NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE.NWGUS_MOVING && this.CheckPossibleBreathing())
			{
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DOKill(false);
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.localPosition = this.m_goNUM_WARFARE_USER_UNIT_IMG_orgPos;
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x001828ED File Offset: 0x00180AED
		public NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE GetState()
		{
			return this.m_NKC_WARFARE_GAME_UNIT_STATE;
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x001828F8 File Offset: 0x00180AF8
		public void Move(Vector3 _EndPos, float _fTrackingTime, NKCWarfareUnitMover.OnCompleteMove _OnCompleteMove = null, bool bOnlyJump = false)
		{
			if (this.CheckShipType() && !bOnlyJump)
			{
				this.m_NKCWarfareUnitMover.Move(_EndPos, _fTrackingTime, _OnCompleteMove);
			}
			else
			{
				if (this.m_JumpSeq != null && this.m_JumpSeq.IsActive())
				{
					this.m_JumpSeq.Kill(false);
				}
				this.m_JumpSeq = DOTween.Sequence();
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				float num = _fTrackingTime - 0.5f;
				this.m_JumpSeq.Append(this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DORotate(new Vector3(0f, 0f, -5f), num / 3f, RotateMode.Fast));
				this.m_JumpSeq.Append(this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DORotate(new Vector3(0f, 0f, 0f), num * 2f / 3f, RotateMode.Fast));
				this.m_NKCWarfareUnitMover.Jump(_EndPos, _fTrackingTime, _OnCompleteMove);
			}
			NKCSoundManager.PlaySound("FX_UI_WARFARE_SHIP_MOVE", 1f, 0f, 0f, false, 0f, false, 0f);
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x00182A26 File Offset: 0x00180C26
		public void OnClickUnit()
		{
			if (this.m_OnClickUnit != null)
			{
				this.m_OnClickUnit(this.m_NKMWarfareUnitData.warfareGameUnitUID);
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06004FF8 RID: 20472 RVA: 0x00182A46 File Offset: 0x00180C46
		public int TileIndex
		{
			get
			{
				return (int)this.m_NKMWarfareUnitData.tileIndex;
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06004FF9 RID: 20473 RVA: 0x00182A53 File Offset: 0x00180C53
		public bool IsSupporter
		{
			get
			{
				return this.m_NKMWarfareUnitData.friendCode != 0L;
			}
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x00182A64 File Offset: 0x00180C64
		private void ClearSeqs()
		{
			if (this.m_JumpSeq != null && this.m_JumpSeq.IsActive())
			{
				this.m_JumpSeq.Pause<Sequence>();
				this.m_JumpSeq.Kill(false);
			}
			this.m_JumpSeq = null;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x00182A9C File Offset: 0x00180C9C
		public void Close()
		{
			this.ClearSeqs();
			this.m_bPause = false;
			if (this.m_NKCWarfareUnitMover != null)
			{
				this.m_NKCWarfareUnitMover.Stop();
			}
			base.gameObject.transform.DOKill(false);
			if (base.GetComponent<CanvasGroup>() != null)
			{
				base.GetComponent<CanvasGroup>().DOKill(false);
			}
			if (this.m_ImgUnit != null)
			{
				this.m_ImgUnit.DOKill(false);
			}
			if (this.m_NUM_WARFARE_USER_UNIT_END_CG != null)
			{
				this.m_NUM_WARFARE_USER_UNIT_END_CG.DOKill(false);
			}
			if (this.m_goNUM_WARFARE_USER_UNIT_IMG != null)
			{
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DOKill(false);
			}
			if (this.m_Instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_Instance);
			}
			this.m_Instance = null;
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x00182B6C File Offset: 0x00180D6C
		public WarfareUnitData GetNKMWarfareUnitData()
		{
			return this.m_NKMWarfareUnitData;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x00182B74 File Offset: 0x00180D74
		private void Update()
		{
			if (this.m_NKC_WARFARE_GAME_UNIT_STATE == NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE.NWGUS_IDLE && this.m_fTimer > 0f)
			{
				this.m_fTimer -= Time.deltaTime;
				if (this.m_fTimer <= 0f && this.dOnTimeEndCallback != null)
				{
					this.dOnTimeEndCallback(this);
					this.dOnTimeEndCallback = null;
				}
			}
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x00182BD0 File Offset: 0x00180DD0
		public void SetNKMWarfareUnitData(WarfareUnitData cNKMWarfareUnitData)
		{
			this.m_NKMWarfareUnitData = cNKMWarfareUnitData;
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x00182BD9 File Offset: 0x00180DD9
		public void SetDungeonStrID(string dungeonStrID)
		{
			if (this.m_NKMWarfareUnitData != null)
			{
				this.m_NKMWarfareUnitData.dungeonID = NKMDungeonManager.GetDungeonID(dungeonStrID);
			}
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x00182BF4 File Offset: 0x00180DF4
		public void SetDeckIndex(NKMDeckIndex sNKMDeckIndex)
		{
			if (this.m_NKMWarfareUnitData != null)
			{
				this.m_NKMWarfareUnitData.deckIndex = sNKMDeckIndex;
			}
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x00182C0A File Offset: 0x00180E0A
		public void SetWarfareGameUnitUID(int uid)
		{
			if (this.m_NKMWarfareUnitData != null)
			{
				this.m_NKMWarfareUnitData.warfareGameUnitUID = uid;
			}
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x00182C20 File Offset: 0x00180E20
		private void ResetWinBuff()
		{
			this.m_NUM_WARFARE_FX_SPECIAL_POWER_UP != null;
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x00182C30 File Offset: 0x00180E30
		private bool CheckShipTypeEvenIfDungeon(int dungeonID)
		{
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_NKMWarfareUnitData.dungeonID);
			if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonIcon == "")
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(dungeonTempletBase.m_DungeonID);
				if (dungeonTemplet != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(dungeonTemplet.m_BossUnitStrID);
					if (unitTempletBase != null && unitTempletBase.m_UnitID > 50000 && unitTempletBase.m_UnitID < 60000)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x00182CA0 File Offset: 0x00180EA0
		public void PlayClickAni()
		{
			if (this.m_NKMWarfareUnitData != null && this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.Dungeon && this.m_goNUM_WARFARE_USER_UNIT_IMG != null && !this.CheckShipTypeEvenIfDungeon(this.m_NKMWarfareUnitData.dungeonID))
			{
				this.ClearSeqs();
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DOShakeScale(0.8f, new Vector3(0f, 0.07f, 0f), 5, 20f, true, ShakeRandomnessMode.Full);
			}
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x00182D44 File Offset: 0x00180F44
		public void PlayEnemySpawnAni()
		{
			if (this.m_NKMWarfareUnitData != null && this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.Dungeon && this.m_goNUM_WARFARE_USER_UNIT_IMG != null)
			{
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.localScale = new Vector3(1f, 0f, 1f);
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DOScaleY(1f, 0.4f).SetEase(Ease.OutBack);
			}
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x00182DBB File Offset: 0x00180FBB
		public bool CheckShipType()
		{
			return this.m_NKMWarfareUnitData != null && (this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.User || this.CheckShipTypeEvenIfDungeon(this.m_NKMWarfareUnitData.dungeonID));
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x00182DEC File Offset: 0x00180FEC
		public void PlayDieAni()
		{
			this.SetState(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE.NWGUS_DYING);
			if (this.m_goNUM_WARFARE_USER_UNIT_IMG != null && this.m_NKMWarfareUnitData != null)
			{
				if (this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.User)
				{
					this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DORotate(new Vector3(0f, 0f, 20f), 2f, RotateMode.Fast);
					return;
				}
				if (this.CheckShipTypeEvenIfDungeon(this.m_NKMWarfareUnitData.dungeonID))
				{
					this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DORotate(new Vector3(0f, 0f, -20f), 2f, RotateMode.Fast);
					return;
				}
				this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DOShakeRotation(2f, 90f, 10, 90f, true, ShakeRandomnessMode.Full);
			}
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x00182EB8 File Offset: 0x001810B8
		private void EnableAni()
		{
			this.m_NUM_WARFARE_USER_UNIT_IMG.enabled = true;
			this.m_NUM_WARFARE_USER_UNIT_SHADOW.enabled = true;
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x00182ED4 File Offset: 0x001810D4
		public void PlayAni(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION aniType, UnityAction completeAction = null)
		{
			string text = string.Empty;
			if (aniType != NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION.NWGUA_RUNAWAY)
			{
				if (aniType == NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_ANIMATION.NWGUA_ENTER)
				{
					text = "NUM_WARFARE_UNIT_ENTER";
				}
			}
			else
			{
				text = "NUM_WARFARE_UNIT_RUNAWAY";
			}
			if (text != string.Empty)
			{
				this.m_animator.Play(text);
				if (completeAction != null)
				{
					base.StartCoroutine(this.OnCompleteAni(completeAction, text));
				}
			}
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x00182F2A File Offset: 0x0018112A
		private IEnumerator OnCompleteAni(UnityAction completeCallback, string aniName)
		{
			if (!this.m_animator.GetCurrentAnimatorStateInfo(0).IsName(aniName))
			{
				yield return null;
			}
			while (this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			while (this.m_bPause)
			{
				yield return null;
			}
			if (completeCallback != null)
			{
				completeCallback();
			}
			yield break;
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x00182F48 File Offset: 0x00181148
		public void UpdateTurnUI()
		{
			if (this.m_NKMWarfareUnitData == null)
			{
				return;
			}
			if (this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.User)
			{
				this.ResetWinBuff();
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData != null)
			{
				bool flag = warfareGameData.CheckTeamA_By_GameUnitUID(this.m_NKMWarfareUnitData.warfareGameUnitUID);
				if (warfareGameData.isTurnA == flag && this.m_NKMWarfareUnitData.hp > 0f)
				{
					NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_USER_UNIT_END, this.m_NKMWarfareUnitData.isTurnEnd);
					NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_USER_UNIT_IMG_TURN_ON, flag && !this.m_NKMWarfareUnitData.isTurnEnd);
					if (this.m_NKMWarfareUnitData.isTurnEnd)
					{
						if (this.m_ImgUnit != null)
						{
							this.m_ImgUnit.DOColor(new Color(0.49411765f, 0.49411765f, 0.49411765f, 1f), 0.9f).SetEase(Ease.InCubic);
						}
						if (this.m_NUM_WARFARE_USER_UNIT_END_CG != null)
						{
							this.m_NUM_WARFARE_USER_UNIT_END_CG.DOFade(1f, 0.9f).SetEase(Ease.InCubic);
							return;
						}
					}
					else
					{
						if (this.m_ImgUnit != null)
						{
							this.m_ImgUnit.DOColor(new Color(1f, 1f, 1f, 1f), 0.9f).SetEase(Ease.OutCubic);
						}
						if (this.m_NUM_WARFARE_USER_UNIT_END_CG != null)
						{
							this.m_NUM_WARFARE_USER_UNIT_END_CG.alpha = 0f;
							return;
						}
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_USER_UNIT_IMG_TURN_ON, false);
					NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_USER_UNIT_END, false);
					if (this.m_NUM_WARFARE_USER_UNIT_END_CG != null)
					{
						this.m_NUM_WARFARE_USER_UNIT_END_CG.alpha = 0f;
					}
					if (this.m_ImgUnit != null)
					{
						this.m_ImgUnit.DOColor(new Color(1f, 1f, 1f, 1f), 0.9f).SetEase(Ease.OutCubic);
					}
				}
			}
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x00183139 File Offset: 0x00181339
		public void SetTurnEndTimer(NKCWarfareGameUnit.OnTimeEndCallback onTimeEndCallback)
		{
			this.dOnTimeEndCallback = onTimeEndCallback;
			this.m_fTimer = 0.9f;
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x00183150 File Offset: 0x00181350
		public bool CheckPossibleBreathing()
		{
			if (this.m_NKMWarfareUnitData != null && this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.Dungeon)
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_NKMWarfareUnitData.dungeonID);
				if (dungeonTempletBase != null && !this.CheckShipTypeEvenIfDungeon(dungeonTempletBase.m_DungeonID))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x00183198 File Offset: 0x00181398
		public void SetBreathingMotion()
		{
			this.m_goNUM_WARFARE_USER_UNIT_IMG.transform.DOScale(new Vector3(1.04f, 1.04f, 1.04f), 1.5f + UnityEngine.Random.value).SetLoops(-1, LoopType.Yoyo);
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x001831D4 File Offset: 0x001813D4
		public void OneTimeSetUnitUI()
		{
			if (this.m_NKMWarfareUnitData == null)
			{
				return;
			}
			if (this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.User)
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				this.m_NKCUIComButton.UpdateOrgSize();
				NKMUnitTempletBase nkmunitTempletBase = null;
				if (!this.IsSupporter)
				{
					NKMDeckIndex deckIndex = this.m_NKMWarfareUnitData.deckIndex;
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(deckIndex);
					if (deckData != null)
					{
						NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
						if (shipFromUID != null)
						{
							nkmunitTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
						}
					}
				}
				else
				{
					WarfareSupporterListData supportUnitData = NKCScenManager.GetScenManager().WarfareGameData.supportUnitData;
					if (supportUnitData != null && supportUnitData.commonProfile.friendCode == this.m_NKMWarfareUnitData.friendCode)
					{
						nkmunitTempletBase = NKMUnitManager.GetUnitTempletBase(supportUnitData.deckData.GetShipUnitId());
					}
				}
				if (nkmunitTempletBase != null)
				{
					Sprite orLoadMinimapFaceIcon = NKCResourceUtility.GetOrLoadMinimapFaceIcon(nkmunitTempletBase.m_MiniMapFaceName);
					if (orLoadMinimapFaceIcon == null)
					{
						NKCAssetResourceData assetResourceUnitInvenIconEmpty = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
						if (assetResourceUnitInvenIconEmpty != null)
						{
							this.m_ImgUnit.sprite = assetResourceUnitInvenIconEmpty.GetAsset<Sprite>();
						}
						else
						{
							this.m_ImgUnit.sprite = null;
						}
					}
					else
					{
						this.m_ImgUnit.sprite = orLoadMinimapFaceIcon;
					}
				}
				this.EnableAni();
			}
			else if (this.m_NKMWarfareUnitData.unitType == WarfareUnitData.Type.Dungeon)
			{
				bool flag = false;
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_NKMWarfareUnitData.dungeonID);
				if (dungeonTempletBase != null)
				{
					if (this.CheckShipTypeEvenIfDungeon(dungeonTempletBase.m_DungeonID))
					{
						this.EnableAni();
					}
					if (dungeonTempletBase.m_DungeonIcon == "")
					{
						NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(dungeonTempletBase.m_DungeonID);
						if (dungeonTemplet != null)
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(dungeonTemplet.m_BossUnitStrID);
							if (unitTempletBase != null)
							{
								Sprite orLoadMinimapFaceIcon2 = NKCResourceUtility.GetOrLoadMinimapFaceIcon(unitTempletBase.m_MiniMapFaceName);
								if (orLoadMinimapFaceIcon2 != null)
								{
									this.m_ImgUnit.sprite = orLoadMinimapFaceIcon2;
									flag = true;
								}
							}
						}
					}
					else
					{
						Sprite orLoadMinimapFaceIcon3 = NKCResourceUtility.GetOrLoadMinimapFaceIcon(dungeonTempletBase.m_DungeonIcon);
						if (orLoadMinimapFaceIcon3 != null)
						{
							this.m_ImgUnit.sprite = orLoadMinimapFaceIcon3;
							flag = true;
						}
					}
				}
				if (!flag)
				{
					NKCAssetResourceData assetResourceUnitInvenIconEmpty2 = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
					if (assetResourceUnitInvenIconEmpty2 != null)
					{
						this.m_ImgUnit.sprite = assetResourceUnitInvenIconEmpty2.GetAsset<Sprite>();
					}
					else
					{
						this.m_ImgUnit.sprite = null;
					}
				}
				if (this.m_NUM_WARFARE_USER_UNIT_END != null)
				{
					this.m_NUM_WARFARE_USER_UNIT_END.transform.localScale = new Vector3(-1f, 1f, 1f);
				}
				base.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
				this.m_NKCUIComButton.UpdateOrgSize();
			}
			this.UpdateTurnUI();
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x00183480 File Offset: 0x00181680
		public void SetAttackIcon(bool bActive, bool bGray = false)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_USER_UNIT_ATTACK, bActive && !bGray);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_USER_UNIT_ATTACK_GRAY, bActive && bGray);
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x001834A5 File Offset: 0x001816A5
		public void SetChangeIcon(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_USER_UNIT_CHANGE, bActive);
		}

		// Token: 0x04003FFC RID: 16380
		public GameObject m_goNUM_WARFARE_USER_UNIT_IMG;

		// Token: 0x04003FFD RID: 16381
		public Image m_ImgUnit;

		// Token: 0x04003FFE RID: 16382
		public NKCUIComButton m_NKCUIComButton;

		// Token: 0x04003FFF RID: 16383
		public NKCWarfareUnitMover m_NKCWarfareUnitMover;

		// Token: 0x04004000 RID: 16384
		public GameObject m_NUM_WARFARE_FX_SPECIAL_POWER_UP;

		// Token: 0x04004001 RID: 16385
		public GameObject m_NUM_WARFARE_FX_TILE_REPAIR;

		// Token: 0x04004002 RID: 16386
		public GameObject m_NUM_WARFARE_FX_TILE_SUPPLY;

		// Token: 0x04004003 RID: 16387
		public GameObject m_NUM_WARFARE_FX_TILE_THUNDER;

		// Token: 0x04004004 RID: 16388
		public DOTweenVisualManager m_NUM_WARFARE_USER_UNIT_SHADOW;

		// Token: 0x04004005 RID: 16389
		public DOTweenVisualManager m_NUM_WARFARE_USER_UNIT_IMG;

		// Token: 0x04004006 RID: 16390
		public GameObject m_NUM_WARFARE_USER_UNIT_IMG_TURN_ON;

		// Token: 0x04004007 RID: 16391
		public GameObject m_NUM_WARFARE_USER_UNIT_END;

		// Token: 0x04004008 RID: 16392
		public CanvasGroup m_NUM_WARFARE_USER_UNIT_END_CG;

		// Token: 0x04004009 RID: 16393
		public GameObject m_NUM_WARFARE_USER_UNIT_ATTACK;

		// Token: 0x0400400A RID: 16394
		public GameObject m_NUM_WARFARE_USER_UNIT_ATTACK_GRAY;

		// Token: 0x0400400B RID: 16395
		public GameObject m_NUM_WARFARE_USER_UNIT_CHANGE;

		// Token: 0x0400400C RID: 16396
		private WarfareUnitData m_NKMWarfareUnitData;

		// Token: 0x0400400D RID: 16397
		private NKCAssetInstanceData m_Instance;

		// Token: 0x0400400E RID: 16398
		private NKCWarfareGameUnit.onClickUnit m_OnClickUnit;

		// Token: 0x0400400F RID: 16399
		private Sequence m_JumpSeq;

		// Token: 0x04004010 RID: 16400
		private NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE m_NKC_WARFARE_GAME_UNIT_STATE;

		// Token: 0x04004011 RID: 16401
		private Vector3 m_goNUM_WARFARE_USER_UNIT_IMG_orgPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04004012 RID: 16402
		private float m_fTimer;

		// Token: 0x04004013 RID: 16403
		private NKCWarfareGameUnit.OnTimeEndCallback dOnTimeEndCallback;

		// Token: 0x04004014 RID: 16404
		private Animator m_animator;

		// Token: 0x04004015 RID: 16405
		private bool m_bPause;

		// Token: 0x0200149D RID: 5277
		public enum NKC_WARFARE_GAME_UNIT_STATE
		{
			// Token: 0x04009E99 RID: 40601
			NWGUS_IDLE,
			// Token: 0x04009E9A RID: 40602
			NWGUS_MOVING,
			// Token: 0x04009E9B RID: 40603
			NWGUS_DYING
		}

		// Token: 0x0200149E RID: 5278
		public enum NKC_WARFARE_GAME_UNIT_ANIMATION
		{
			// Token: 0x04009E9D RID: 40605
			NWGUA_IDLE,
			// Token: 0x04009E9E RID: 40606
			NWGUA_RUNAWAY,
			// Token: 0x04009E9F RID: 40607
			NWGUA_ENTER
		}

		// Token: 0x0200149F RID: 5279
		// (Invoke) Token: 0x0600A963 RID: 43363
		public delegate void onClickUnit(int gameUID);

		// Token: 0x020014A0 RID: 5280
		// (Invoke) Token: 0x0600A967 RID: 43367
		public delegate void OnTimeEndCallback(NKCWarfareGameUnit unit);
	}
}
