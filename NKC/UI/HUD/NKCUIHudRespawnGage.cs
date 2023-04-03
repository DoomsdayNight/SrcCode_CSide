using System;
using System.Collections.Generic;
using System.Text;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C49 RID: 3145
	public class NKCUIHudRespawnGage : MonoBehaviour
	{
		// Token: 0x06009293 RID: 37523 RVA: 0x003207B8 File Offset: 0x0031E9B8
		public void SetData()
		{
			this.m_RespawnCostGage.SetNowValue(0f);
			this.m_RespawnCostGageAssist.SetNowValue(0f);
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_StringBuilder.AppendFormat("{0}", this.m_BeforeRespawnCost);
			this.m_lbGageCost.text = this.m_StringBuilder.ToString();
		}

		// Token: 0x06009294 RID: 37524 RVA: 0x0032082F File Offset: 0x0031EA2F
		public void SetRespawnCostNowValue(float fValue)
		{
			this.m_RespawnCostGage.Init();
			this.m_RespawnCostGage.SetNowValue(fValue);
			this.m_RespawnCostGageAssist.SetNowValue(fValue);
		}

		// Token: 0x06009295 RID: 37525 RVA: 0x00320854 File Offset: 0x0031EA54
		public void SetSupply(int supplyCount)
		{
			if (supplyCount == 0)
			{
				Color color = this.m_imgCostBgCharge.color;
				color.r = 0.7f;
				color.g = 0f;
				color.b = 0.7f;
				this.m_imgCostBgCharge.color = color;
				color = this.m_lbGageCost.color;
				color.r = 0.7f;
				color.g = 0.7f;
				color.b = 0.7f;
				this.m_lbGageCost.color = color;
				return;
			}
			Color color2 = this.m_imgCostBgCharge.color;
			color2.r = 1f;
			color2.g = 0f;
			color2.b = 1f;
			this.m_imgCostBgCharge.color = color2;
			color2 = this.m_lbGageCost.color;
			color2.r = 1f;
			color2.g = 1f;
			color2.b = 1f;
			this.m_lbGageCost.color = color2;
		}

		// Token: 0x06009296 RID: 37526 RVA: 0x00320958 File Offset: 0x0031EB58
		public void UpdateGage(float fDeltaTime)
		{
			this.m_RespawnCostGage.Update(fDeltaTime);
			this.m_RespawnCostGageAssist.Update(fDeltaTime);
			float nowValue = this.m_RespawnCostGage.GetNowValue();
			int num = Mathf.FloorToInt(nowValue);
			if (this.m_BeforeRespawnCost < num && this.m_AnimatorGageCost.gameObject.activeInHierarchy)
			{
				this.m_AnimatorGageCost.Play("FULL", -1, 0f);
			}
			if (this.m_BeforeRespawnCost != num)
			{
				this.m_BeforeRespawnCost = num;
				this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
				int num2 = num;
				if (num2 < 0)
				{
					num2 = 0;
				}
				this.m_StringBuilder.AppendFormat("{0}", num2);
				this.m_lbGageCost.text = this.m_StringBuilder.ToString();
			}
			NKCUtil.SetImageFillAmount(this.m_imgCostBgCharge, nowValue - (float)num);
		}

		// Token: 0x06009297 RID: 37527 RVA: 0x00320A30 File Offset: 0x0031EC30
		public void SetRespawnCost(float fRespawnCost)
		{
			if (fRespawnCost < this.m_RespawnCostGage.GetNowValue())
			{
				this.m_RespawnCostGage.SetTracking(fRespawnCost, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
				return;
			}
			if (this.m_RespawnCostGage.GetTargetValue() != fRespawnCost || !this.m_RespawnCostGage.IsTracking())
			{
				this.m_RespawnCostGage.SetTracking(fRespawnCost, 1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
		}

		// Token: 0x06009298 RID: 37528 RVA: 0x00320A8B File Offset: 0x0031EC8B
		public float GetRespawnCostGage()
		{
			return this.m_RespawnCostGage.GetNowValue();
		}

		// Token: 0x06009299 RID: 37529 RVA: 0x00320A98 File Offset: 0x0031EC98
		public void SetRespawnCostAssist(float fRespawnCost)
		{
			if (fRespawnCost < this.m_RespawnCostGageAssist.GetNowValue())
			{
				this.m_RespawnCostGageAssist.SetTracking(fRespawnCost, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
				return;
			}
			if (this.m_RespawnCostGageAssist.GetTargetValue() != fRespawnCost || !this.m_RespawnCostGageAssist.IsTracking())
			{
				this.m_RespawnCostGageAssist.SetTracking(fRespawnCost, 1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
		}

		// Token: 0x0600929A RID: 37530 RVA: 0x00320AF3 File Offset: 0x0031ECF3
		public float GetRespawnCostGageAssist()
		{
			return this.m_RespawnCostGageAssist.GetNowValue();
		}

		// Token: 0x0600929B RID: 37531 RVA: 0x00320B00 File Offset: 0x0031ED00
		public void PlayRespawnAddEvent(float value)
		{
			this.GetAddEvent().Play(value);
		}

		// Token: 0x0600929C RID: 37532 RVA: 0x00320B10 File Offset: 0x0031ED10
		private NKCUIHudRespawnCostAddEvent GetAddEvent()
		{
			foreach (NKCUIHudRespawnCostAddEvent nkcuihudRespawnCostAddEvent in this.m_lstCostAdd)
			{
				if (nkcuihudRespawnCostAddEvent != null && nkcuihudRespawnCostAddEvent.Idle)
				{
					return nkcuihudRespawnCostAddEvent;
				}
			}
			return this.MakeAddEvent();
		}

		// Token: 0x0600929D RID: 37533 RVA: 0x00320B7C File Offset: 0x0031ED7C
		private NKCUIHudRespawnCostAddEvent MakeAddEvent()
		{
			NKCUIHudRespawnCostAddEvent nkcuihudRespawnCostAddEvent = UnityEngine.Object.Instantiate<NKCUIHudRespawnCostAddEvent>(this.m_pfbCostAdd, base.transform);
			if (nkcuihudRespawnCostAddEvent == null)
			{
				return null;
			}
			nkcuihudRespawnCostAddEvent.transform.localRotation = Quaternion.identity;
			nkcuihudRespawnCostAddEvent.transform.localScale = Vector3.one;
			nkcuihudRespawnCostAddEvent.transform.SetAsLastSibling();
			this.m_lstCostAdd.Add(nkcuihudRespawnCostAddEvent);
			NKCUtil.SetGameobjectActive(nkcuihudRespawnCostAddEvent, false);
			return nkcuihudRespawnCostAddEvent;
		}

		// Token: 0x04007F90 RID: 32656
		public Animator m_AnimatorGageCost;

		// Token: 0x04007F91 RID: 32657
		public Image m_imgCostBgCharge;

		// Token: 0x04007F92 RID: 32658
		public Text m_lbGageCost;

		// Token: 0x04007F93 RID: 32659
		public NKCUIHudRespawnCostAddEvent m_pfbCostAdd;

		// Token: 0x04007F94 RID: 32660
		private List<NKCUIHudRespawnCostAddEvent> m_lstCostAdd = new List<NKCUIHudRespawnCostAddEvent>();

		// Token: 0x04007F95 RID: 32661
		private int m_BeforeRespawnCost;

		// Token: 0x04007F96 RID: 32662
		private NKMTrackingFloat m_RespawnCostGage = new NKMTrackingFloat();

		// Token: 0x04007F97 RID: 32663
		private NKMTrackingFloat m_RespawnCostGageAssist = new NKMTrackingFloat();

		// Token: 0x04007F98 RID: 32664
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04007F99 RID: 32665
		[Header("�Ⱦ��°� �ϴ� ���ܵ�")]
		public Image m_imgRespawnMovePoint;
	}
}
