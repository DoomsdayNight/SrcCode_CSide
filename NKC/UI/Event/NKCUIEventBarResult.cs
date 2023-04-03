using System;
using System.Collections;
using NKM;
using NKM.Event;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Event
{
	// Token: 0x02000BD6 RID: 3030
	public class NKCUIEventBarResult : MonoBehaviour
	{
		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06008C8C RID: 35980 RVA: 0x002FCEBA File Offset: 0x002FB0BA
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIEventBarResult.Instance != null && NKCUIEventBarResult.Instance.gameObject.activeSelf;
			}
		}

		// Token: 0x06008C8D RID: 35981 RVA: 0x002FCEDA File Offset: 0x002FB0DA
		public void Init()
		{
			if (NKCUIEventBarResult.Instance == null)
			{
				NKCUIEventBarResult.Instance = this;
			}
			this.m_onClose = null;
		}

		// Token: 0x06008C8E RID: 35982 RVA: 0x002FCEF8 File Offset: 0x002FB0F8
		public void Open(int rewardItemID)
		{
			if (rewardItemID == 0)
			{
				return;
			}
			NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(rewardItemID);
			if (nkmeventBarTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objShake, nkmeventBarTemplet.Technique == ManufacturingTechnique.shake);
			NKCUtil.SetGameobjectActive(this.m_objStir, nkmeventBarTemplet.Technique == ManufacturingTechnique.stir);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_unitID);
			ManufacturingTechnique technique = nkmeventBarTemplet.Technique;
			if (technique != ManufacturingTechnique.stir)
			{
				if (technique == ManufacturingTechnique.shake)
				{
					if (this.m_NKCASUISpineIllust_Shake == null)
					{
						this.m_NKCASUISpineIllust_Shake = NKCResourceUtility.OpenSpineSD(unitTempletBase, false);
					}
					if (this.m_NKCASUISpineIllust_Shake != null)
					{
						this.m_NKCASUISpineIllust_Shake.SetParent(this.m_shakeSDRoot, false);
						RectTransform rectTransform = this.m_NKCASUISpineIllust_Shake.GetRectTransform();
						if (rectTransform != null)
						{
							rectTransform.localScale = new Vector3(this.m_scale, this.m_scale, 1f);
						}
						this.m_NKCASUISpineIllust_Shake.SetAnimation(this.shakeAniName, false, 0, true, 0f, true);
					}
				}
			}
			else
			{
				if (this.m_NKCASUISpineIllust_Stir == null)
				{
					this.m_NKCASUISpineIllust_Stir = NKCResourceUtility.OpenSpineSD(unitTempletBase, false);
				}
				if (this.m_NKCASUISpineIllust_Stir != null)
				{
					this.m_NKCASUISpineIllust_Stir.SetParent(this.m_stirSDRoot, false);
					RectTransform rectTransform2 = this.m_NKCASUISpineIllust_Stir.GetRectTransform();
					if (rectTransform2 != null)
					{
						rectTransform2.localScale = new Vector3(this.m_scale, this.m_scale, 1f);
					}
					this.m_NKCASUISpineIllust_Stir.SetAnimation(this.stirAniName, false, 0, true, 0f, true);
				}
			}
			base.gameObject.SetActive(true);
			NKCComSoundPlayer soundPlayer = this.m_soundPlayer;
			if (soundPlayer != null)
			{
				soundPlayer.Play();
			}
			if (this.m_coroutine != null)
			{
				base.StopCoroutine(this.m_coroutine);
				this.m_coroutine = null;
			}
			this.m_coroutine = base.StartCoroutine(this.IStartClose());
		}

		// Token: 0x06008C8F RID: 35983 RVA: 0x002FD0A3 File Offset: 0x002FB2A3
		private IEnumerator IStartClose()
		{
			float timer = this.m_fRewardPopupTimer;
			while (timer > 0f)
			{
				timer -= Time.deltaTime;
				yield return null;
			}
			this.Close();
			yield break;
		}

		// Token: 0x06008C90 RID: 35984 RVA: 0x002FD0B4 File Offset: 0x002FB2B4
		public void Close()
		{
			if (this.m_coroutine != null)
			{
				base.StopCoroutine(this.m_coroutine);
				this.m_coroutine = null;
			}
			if (this.m_onClose != null)
			{
				this.m_onClose();
				this.m_onClose = null;
			}
			if (this.m_NKCASUISpineIllust_Shake != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust_Shake);
				this.m_NKCASUISpineIllust_Shake = null;
			}
			if (this.m_NKCASUISpineIllust_Stir != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust_Stir);
				this.m_NKCASUISpineIllust_Stir = null;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008C91 RID: 35985 RVA: 0x002FD14C File Offset: 0x002FB34C
		private void OnDestroy()
		{
			NKCUIEventBarResult.Instance = null;
			this.m_onClose = null;
			if (this.m_NKCASUISpineIllust_Shake != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust_Shake);
				this.m_NKCASUISpineIllust_Shake = null;
			}
			if (this.m_NKCASUISpineIllust_Stir != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust_Stir);
				this.m_NKCASUISpineIllust_Stir = null;
			}
		}

		// Token: 0x0400796D RID: 31085
		public static NKCUIEventBarResult Instance;

		// Token: 0x0400796E RID: 31086
		public Animator m_animator;

		// Token: 0x0400796F RID: 31087
		public CanvasGroup m_canvasGroup;

		// Token: 0x04007970 RID: 31088
		public GameObject m_objShake;

		// Token: 0x04007971 RID: 31089
		public GameObject m_objStir;

		// Token: 0x04007972 RID: 31090
		public NKCComSoundPlayer m_soundPlayer;

		// Token: 0x04007973 RID: 31091
		public float m_fRewardPopupTimer;

		// Token: 0x04007974 RID: 31092
		[Header("캐릭터 SD")]
		public int m_unitID;

		// Token: 0x04007975 RID: 31093
		public float m_scale;

		// Token: 0x04007976 RID: 31094
		public RectTransform m_shakeSDRoot;

		// Token: 0x04007977 RID: 31095
		public RectTransform m_stirSDRoot;

		// Token: 0x04007978 RID: 31096
		public string shakeAniName;

		// Token: 0x04007979 RID: 31097
		public string stirAniName;

		// Token: 0x0400797A RID: 31098
		public NKCUIEventBarResult.OnClose m_onClose;

		// Token: 0x0400797B RID: 31099
		private Coroutine m_coroutine;

		// Token: 0x0400797C RID: 31100
		private NKCASUIUnitIllust m_NKCASUISpineIllust_Shake;

		// Token: 0x0400797D RID: 31101
		private NKCASUIUnitIllust m_NKCASUISpineIllust_Stir;

		// Token: 0x020019B1 RID: 6577
		// (Invoke) Token: 0x0600B9C4 RID: 47556
		public delegate void OnClose();
	}
}
