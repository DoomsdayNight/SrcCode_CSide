using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007AB RID: 1963
	public class NKCDiveGameUnit : MonoBehaviour
	{
		// Token: 0x06004D53 RID: 19795 RVA: 0x00174928 File Offset: 0x00172B28
		public static NKCDiveGameUnit GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NKM_UI_DIVE_PROCESS_UNIT", false, null);
			NKCDiveGameUnit component = nkcassetInstanceData.m_Instant.GetComponent<NKCDiveGameUnit>();
			if (component == null)
			{
				Debug.LogError("NKM_UI_DIVE_PROCESS_UNIT Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x00174991 File Offset: 0x00172B91
		private void OnDestroy()
		{
			if (this.m_InstanceData != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			}
			this.m_InstanceData = null;
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x001749B0 File Offset: 0x00172BB0
		public void PlaySpawnAni(NKCDiveGameUnit.OnAniComplete _OnSpawnComplete = null)
		{
			this.m_OnSpawnComplete = _OnSpawnComplete;
			Color color = this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.color;
			this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.color = new Color(color.r, color.g, color.b, 1f);
			base.gameObject.transform.DOMove(new Vector3(-500f, 1000f, -1000f), 1.5f, false).SetEase(Ease.OutCubic).From(true).OnComplete(new TweenCallback(this.OnSpawnComplete));
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x00174A40 File Offset: 0x00172C40
		public void ResetRotation()
		{
			this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x00174A68 File Offset: 0x00172C68
		public void PlayDieAniExplosion(NKCDiveGameUnit.OnAniComplete _AfterUnitDie = null)
		{
			this.m_AfterUnitDie = _AfterUnitDie;
			this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.transform.DORotate(new Vector3(0f, 0f, 20f), 2f, RotateMode.Fast);
			if (this.m_UnitDieSequence != null)
			{
				this.m_UnitDieSequence.Kill(false);
			}
			this.m_UnitDieSequence = DOTween.Sequence();
			this.m_UnitDieSequence.Append(base.gameObject.transform.DOShakePosition(2f, 30f, 30, 90f, false, true, ShakeRandomnessMode.Full));
			Color color = this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.color;
			this.m_UnitDieSequence.Append(this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.DOColor(new Color(color.r, color.g, color.b, 0f), 0.6f));
			this.m_UnitDieSequence.AppendCallback(new TweenCallback(this.AfterUnitDieImpl));
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x00174B54 File Offset: 0x00172D54
		public void PlayDieAniWarp(NKCDiveGameUnit.OnAniComplete _AfterUnitDie = null)
		{
			this.m_AfterUnitDie = _AfterUnitDie;
			if (this.m_UnitDieSequence != null)
			{
				this.m_UnitDieSequence.Kill(false);
			}
			this.m_UnitDieSequence = DOTween.Sequence();
			this.m_UnitDieSequence.AppendInterval(2f);
			Color color = this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.color;
			this.m_UnitDieSequence.Append(this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.DOColor(new Color(color.r, color.g, color.b, 0f), 0.6f));
			this.m_UnitDieSequence.AppendCallback(new TweenCallback(this.AfterUnitDieImpl));
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x00174BF4 File Offset: 0x00172DF4
		private void AfterUnitDieImpl()
		{
			if (this.m_AfterUnitDie != null)
			{
				this.m_AfterUnitDie();
				this.m_AfterUnitDie = null;
			}
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x00174C10 File Offset: 0x00172E10
		private void OnSpawnComplete()
		{
			if (this.m_OnSpawnComplete != null)
			{
				this.m_OnSpawnComplete();
				this.m_OnSpawnComplete = null;
			}
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x00174C2C File Offset: 0x00172E2C
		public void Move(Vector3 _EndPos, float _fTrackingTime, NKCDiveGameUnitMover.OnCompleteMove _OnCompleteMove = null)
		{
			this.m_NKCDiveGameUnitMover.Move(_EndPos, _fTrackingTime, _OnCompleteMove);
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x00174C3C File Offset: 0x00172E3C
		public bool IsMoving()
		{
			return !(this.m_NKCDiveGameUnitMover == null) && this.m_NKCDiveGameUnitMover.IsRunning();
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x00174C59 File Offset: 0x00172E59
		public void SetPause(bool bSet)
		{
			if (this.m_NKCDiveGameUnitMover != null)
			{
				this.m_NKCDiveGameUnitMover.SetPause(bSet);
			}
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x00174C78 File Offset: 0x00172E78
		public void Clear()
		{
			if (this.m_NKCDiveGameUnitMover != null)
			{
				this.m_NKCDiveGameUnitMover.Stop();
			}
			base.gameObject.transform.DOKill(false);
			if (this.m_UnitDieSequence != null)
			{
				this.m_UnitDieSequence.Kill(false);
				this.m_UnitDieSequence = null;
			}
			this.m_AfterUnitDie = null;
			this.m_OnSpawnComplete = null;
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x00174CD9 File Offset: 0x00172ED9
		public void PlaySearch()
		{
			if (this.m_NKM_UI_DIVE_PROCESS_UNIT_SEARCH_FX != null)
			{
				this.m_NKM_UI_DIVE_PROCESS_UNIT_SEARCH_FX.Play("NKM_UI_DIVE_PROCESS_UNIT_SEARCH_FX_BASE");
			}
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x00174CFC File Offset: 0x00172EFC
		public void SetUI(int unitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase != null)
			{
				Sprite orLoadMinimapFaceIcon = NKCResourceUtility.GetOrLoadMinimapFaceIcon(unitTempletBase.m_MiniMapFaceName);
				if (orLoadMinimapFaceIcon == null)
				{
					NKCAssetResourceData assetResourceUnitInvenIconEmpty = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
					if (assetResourceUnitInvenIconEmpty != null)
					{
						this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.sprite = assetResourceUnitInvenIconEmpty.GetAsset<Sprite>();
					}
					else
					{
						this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.sprite = null;
					}
				}
				else
				{
					this.m_NKM_UI_DIVE_PROCESS_UNIT_IMG.sprite = orLoadMinimapFaceIcon;
				}
				this.m_NKM_UI_DIVE_PROCESS_UNIT.Play("NKM_UI_DIVE_PROCESS_UNIT_CRUISER");
			}
		}

		// Token: 0x04003D3C RID: 15676
		public Image m_NKM_UI_DIVE_PROCESS_UNIT_IMG;

		// Token: 0x04003D3D RID: 15677
		public Animator m_NKM_UI_DIVE_PROCESS_UNIT_SEARCH_FX;

		// Token: 0x04003D3E RID: 15678
		public NKCDiveGameUnitMover m_NKCDiveGameUnitMover;

		// Token: 0x04003D3F RID: 15679
		public Animator m_NKM_UI_DIVE_PROCESS_UNIT;

		// Token: 0x04003D40 RID: 15680
		private Sequence m_UnitDieSequence;

		// Token: 0x04003D41 RID: 15681
		private NKCDiveGameUnit.OnAniComplete m_AfterUnitDie;

		// Token: 0x04003D42 RID: 15682
		private NKCDiveGameUnit.OnAniComplete m_OnSpawnComplete;

		// Token: 0x04003D43 RID: 15683
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04003D44 RID: 15684
		private const float UNIT_SHAKE_TIME = 2f;

		// Token: 0x04003D45 RID: 15685
		private const float UNIT_FADE_OUT_TIME = 0.6f;

		// Token: 0x02001467 RID: 5223
		// (Invoke) Token: 0x0600A8A6 RID: 43174
		public delegate void OnAniComplete();
	}
}
