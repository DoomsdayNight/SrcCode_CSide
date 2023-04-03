using System;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007F1 RID: 2033
	public class NKCPopupCompanyBuffSlot : MonoBehaviour
	{
		// Token: 0x06005098 RID: 20632 RVA: 0x00185FA8 File Offset: 0x001841A8
		public static NKCPopupCompanyBuffSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_EVENTBUFF_SLOT", false, null);
			NKCPopupCompanyBuffSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCPopupCompanyBuffSlot>();
			if (component == null)
			{
				Debug.LogError("NKCPopupEventBuffSlot Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.m_InstanceData = nkcassetInstanceData;
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x00186046 File Offset: 0x00184246
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x00186068 File Offset: 0x00184268
		public void SetData(NKMCompanyBuffData buff, NKCPopupCompanyBuffSlot.OnExpire onExpire)
		{
			this.dOnExpire = onExpire;
			this.m_NKMCompanyBuffTemplet = NKMTempletContainer<NKMCompanyBuffTemplet>.Find(buff.Id);
			if (this.m_NKMCompanyBuffTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetLabelText(this.m_lbTitle, this.m_NKMCompanyBuffTemplet.GetBuffName());
			NKCUtil.SetLabelText(this.m_lbDesc, NKCUtil.GetCompanyBuffDesc(this.m_NKMCompanyBuffTemplet.m_CompanyBuffID));
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCUtil.GetCompanyBuffIconSprite(buff), false);
			this.m_expireTime = new DateTime(buff.ExpireTicks);
			this.SetTimeLabel();
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x00186108 File Offset: 0x00184308
		private void SetTimeLabel()
		{
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(this.m_expireTime);
			if (timeLeft.TotalSeconds < 0.0)
			{
				NKCPopupCompanyBuffSlot.OnExpire onExpire = this.dOnExpire;
				if (onExpire != null)
				{
					onExpire(false);
				}
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbDay, timeLeft.Days > 0);
			if (timeLeft.Days > 0)
			{
				this.m_lbDay.text = string.Format(NKCUtilString.GetTimeString(this.m_expireTime, true), timeLeft.Days);
			}
			if (timeLeft.TotalHours >= 1.0)
			{
				this.m_lbTime.text = NKCUtilString.GetTimeSpanString(timeLeft.Subtract(new TimeSpan(timeLeft.Days, 0, 0, 0)));
				NKCUtil.SetLabelTextColor(this.m_lbTime, Color.white);
				return;
			}
			this.m_lbTime.text = NKCUtilString.GetTimeSpanStringMS(timeLeft.Subtract(new TimeSpan(timeLeft.Days, 0, 0, 0)));
			NKCUtil.SetLabelTextColor(this.m_lbTime, Color.red);
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x00186218 File Offset: 0x00184418
		private void Update()
		{
			this.deltaTime += Time.deltaTime;
			if (this.deltaTime > this.updateInterval)
			{
				this.deltaTime -= this.updateInterval;
				this.SetTimeLabel();
			}
		}

		// Token: 0x0400408B RID: 16523
		public Image m_imgIcon;

		// Token: 0x0400408C RID: 16524
		public Text m_lbTitle;

		// Token: 0x0400408D RID: 16525
		public Text m_lbDesc;

		// Token: 0x0400408E RID: 16526
		public Text m_lbDay;

		// Token: 0x0400408F RID: 16527
		public Text m_lbTime;

		// Token: 0x04004090 RID: 16528
		private NKMCompanyBuffTemplet m_NKMCompanyBuffTemplet;

		// Token: 0x04004091 RID: 16529
		private DateTime m_expireTime;

		// Token: 0x04004092 RID: 16530
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04004093 RID: 16531
		private NKCPopupCompanyBuffSlot.OnExpire dOnExpire;

		// Token: 0x04004094 RID: 16532
		private float updateInterval = 1f;

		// Token: 0x04004095 RID: 16533
		private float deltaTime;

		// Token: 0x020014AE RID: 5294
		// (Invoke) Token: 0x0600A9A1 RID: 43425
		public delegate void OnExpire(bool bOpen);
	}
}
