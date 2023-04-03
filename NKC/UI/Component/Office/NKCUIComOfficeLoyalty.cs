using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component.Office
{
	// Token: 0x02000C6B RID: 3179
	public class NKCUIComOfficeLoyalty : MonoBehaviour
	{
		// Token: 0x060093CB RID: 37835 RVA: 0x00327848 File Offset: 0x00325A48
		private void Awake()
		{
			NKCUtil.SetGameobjectActive(this.m_objFxTake, false);
		}

		// Token: 0x060093CC RID: 37836 RVA: 0x00327856 File Offset: 0x00325A56
		private void OnDisable()
		{
			NKCUtil.SetGameobjectActive(this.m_objFxTake, false);
		}

		// Token: 0x060093CD RID: 37837 RVA: 0x00327864 File Offset: 0x00325A64
		public void SetData(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				base.gameObject.SetActive(false);
				return;
			}
			this.m_unitData = unitData;
			this.UpdateData(this.m_unitData);
		}

		// Token: 0x060093CE RID: 37838 RVA: 0x00327889 File Offset: 0x00325A89
		public void PlayTakeHeartEffect()
		{
			NKCUtil.SetGameobjectActive(this.m_objFxTake, true);
		}

		// Token: 0x060093CF RID: 37839 RVA: 0x00327898 File Offset: 0x00325A98
		private void UpdateData(NKMUnitData unitData)
		{
			if (unitData.IsPermanentContract || unitData.loyalty >= 10000)
			{
				base.gameObject.SetActive(false);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase != null && unitTempletBase.IsTrophy)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (unitData.CheckOfficeRoomHeartFull())
			{
				NKCUtil.SetImageFillAmount(this.m_imgGuage, 1f);
				NKCUtil.SetGameobjectActive(this.m_objFxFull, true);
				if (this.m_CanvasGroup != null)
				{
					this.m_CanvasGroup.DOKill(false);
					this.m_CanvasGroup.DOFade(1f, this.m_fAlphaChangeTime);
				}
			}
			else
			{
				float officeRoomHeartGauge = unitData.GetOfficeRoomHeartGauge();
				if (officeRoomHeartGauge >= 0.667f)
				{
					NKCUtil.SetImageFillAmount(this.m_imgGuage, 0.667f);
				}
				else if (officeRoomHeartGauge >= 0.333f)
				{
					NKCUtil.SetImageFillAmount(this.m_imgGuage, 0.333f);
				}
				else
				{
					NKCUtil.SetImageFillAmount(this.m_imgGuage, 0f);
				}
				NKCUtil.SetGameobjectActive(this.m_objFxFull, false);
				if (this.m_CanvasGroup != null)
				{
					this.m_CanvasGroup.DOKill(false);
					this.m_CanvasGroup.DOFade(0f, this.m_fAlphaChangeTime).SetDelay(4f);
				}
			}
			this.m_fTimeToUpdate = 300f;
		}

		// Token: 0x060093D0 RID: 37840 RVA: 0x003279E4 File Offset: 0x00325BE4
		private void Update()
		{
			this.m_fTimeToUpdate -= Time.unscaledDeltaTime;
			if (this.m_fTimeToUpdate < 0f)
			{
				this.UpdateData(this.m_unitData);
			}
			if (base.transform.lossyScale.x < 0f)
			{
				base.transform.localScale = new Vector3(-base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z);
			}
		}

		// Token: 0x040080C4 RID: 32964
		public Image m_imgGuage;

		// Token: 0x040080C5 RID: 32965
		public GameObject m_objFxFull;

		// Token: 0x040080C6 RID: 32966
		public GameObject m_objFxTake;

		// Token: 0x040080C7 RID: 32967
		public CanvasGroup m_CanvasGroup;

		// Token: 0x040080C8 RID: 32968
		public float m_fAlphaChangeTime = 1f;

		// Token: 0x040080C9 RID: 32969
		private NKMUnitData m_unitData;

		// Token: 0x040080CA RID: 32970
		private const float UPDATE_GAP = 300f;

		// Token: 0x040080CB RID: 32971
		private float m_fTimeToUpdate;
	}
}
