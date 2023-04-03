using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A2B RID: 2603
	public class NKCUIOverlayCaption : NKCUIBase
	{
		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x060071F7 RID: 29175 RVA: 0x0025E601 File Offset: 0x0025C801
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x060071F8 RID: 29176 RVA: 0x0025E604 File Offset: 0x0025C804
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x0025E60B File Offset: 0x0025C80B
		public override void CloseInternal()
		{
			this.CloseAllCaption();
			this.StopDelayWaiting();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060071FA RID: 29178 RVA: 0x0025E628 File Offset: 0x0025C828
		public void OpenCaption(string caption, int soundUID, float delay = 0f)
		{
			this.StopDelayWaiting();
			if (this.m_lstCaptions.Count == 0)
			{
				return;
			}
			if (!NKCUtil.CheckFinalCaptionEnabled())
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(caption) || soundUID < 0)
			{
				return;
			}
			if (this.m_lstCaptionSound.Count >= this.m_lstCaptions.Count)
			{
				bool flag = false;
				for (int i = 0; i < this.m_lstCaptions.Count; i++)
				{
					if (!this.m_lstCaptions[i].IsActive)
					{
						this.m_lstCaptionSound.RemoveAt(i);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.m_lstCaptionSound.RemoveAt(0);
				}
			}
			NKCUIComCaption.CaptionData item = new NKCUIComCaption.CaptionData(caption, soundUID);
			this.m_lstCaptionSound.Add(item);
			NKCUtil.SetGameobjectActive(base.gameObject, this.m_lstCaptionSound.Count > 0);
			if (base.gameObject.activeSelf && delay > 0f)
			{
				this.CloseAllCaption();
				this.StartDelayWaiting(delay);
				return;
			}
			this.RefreshData();
		}

		// Token: 0x060071FB RID: 29179 RVA: 0x0025E718 File Offset: 0x0025C918
		public void OpenCaption(List<NKCUIComCaption.CaptionDataTime> lstCaption)
		{
			this.StopDelayWaiting();
			if (lstCaption.Count == 0)
			{
				return;
			}
			foreach (NKCUIComCaption.CaptionDataTime captionDataTime in lstCaption)
			{
				if (string.IsNullOrWhiteSpace(captionDataTime.caption) || captionDataTime.startTime < 0L || captionDataTime.endTime < 0L)
				{
					return;
				}
			}
			this.m_lstCaptionSound.Clear();
			this.m_lstCaptionTime.Clear();
			this.m_lstCaptionTime = lstCaption;
			this.m_iTimeCaptionKey = 0;
			foreach (NKCUIComCaption.CaptionDataTime captionDataTime2 in this.m_lstCaptionTime)
			{
				captionDataTime2.ConvertTimeToTick();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, this.m_lstCaptionTime.Count > 0);
		}

		// Token: 0x060071FC RID: 29180 RVA: 0x0025E810 File Offset: 0x0025CA10
		private void StopDelayWaiting()
		{
			if (base.gameObject.activeSelf && this.m_delayCoroutine != null)
			{
				base.StopCoroutine(this.m_delayCoroutine);
				this.m_delayCoroutine = null;
			}
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x0025E83A File Offset: 0x0025CA3A
		private void StartDelayWaiting(float delay)
		{
			this.m_delayCoroutine = base.StartCoroutine(this.DelayWaiting(delay));
		}

		// Token: 0x060071FE RID: 29182 RVA: 0x0025E84F File Offset: 0x0025CA4F
		private IEnumerator DelayWaiting(float delay)
		{
			float timer = 0f;
			while (timer < delay)
			{
				timer += Time.deltaTime;
				yield return null;
			}
			this.RefreshData();
			yield break;
		}

		// Token: 0x060071FF RID: 29183 RVA: 0x0025E868 File Offset: 0x0025CA68
		private void RefreshData()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, this.m_lstCaptionSound.Count > 0);
			for (int i = 0; i < this.m_lstCaptions.Count; i++)
			{
				if (i < this.m_lstCaptionSound.Count)
				{
					this.m_lstCaptions[i].SetData(this.m_lstCaptionSound[i].caption, this.m_lstCaptionSound[i].key);
				}
				else
				{
					this.m_lstCaptions[i].CloseCaption();
				}
				this.m_lstCaptions[i].transform.SetAsLastSibling();
			}
		}

		// Token: 0x06007200 RID: 29184 RVA: 0x0025E910 File Offset: 0x0025CB10
		private void RefreshData(int iKey)
		{
			if (this.m_lstCaptionTime.Count < iKey || (iKey > 0 && this.m_iTimeCaptionKey == iKey))
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, this.m_lstCaptionTime.Count > 0);
			for (int i = 0; i < this.m_lstCaptions.Count; i++)
			{
				if (this.m_lstCaptionTime.Count > 0)
				{
					if (i < this.m_lstCaptionTime.Count)
					{
						this.m_lstCaptions[i].SetData(this.m_lstCaptionTime[iKey]);
						this.m_iTimeCaptionKey = iKey;
					}
					else
					{
						this.m_lstCaptions[i].CloseCaption();
					}
				}
				this.m_lstCaptions[i].transform.SetAsLastSibling();
			}
		}

		// Token: 0x06007201 RID: 29185 RVA: 0x0025E9D4 File Offset: 0x0025CBD4
		public void CloseCaption(int key)
		{
			for (int i = 0; i < this.m_lstCaptions.Count; i++)
			{
				if (this.m_lstCaptions[i].GetCaptionData() != null && this.m_lstCaptions[i].GetCaptionData().key == key)
				{
					this.m_lstCaptions[i].CloseCaption();
					break;
				}
			}
			this.RefreshData();
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x0025EA3C File Offset: 0x0025CC3C
		public void CloseAllCaption()
		{
			for (int i = 0; i < this.m_lstCaptions.Count; i++)
			{
				this.m_lstCaptions[i].CloseCaption();
			}
			this.m_lstCaptionTime.Clear();
		}

		// Token: 0x06007203 RID: 29187 RVA: 0x0025EA7C File Offset: 0x0025CC7C
		private void Update()
		{
			if (this.m_lstCaptionSound.Count > 0 && this.m_fNextCheckTime <= Time.time)
			{
				this.m_fNextCheckTime = Time.time + 1f;
				for (int i = 0; i < this.m_lstCaptions.Count; i++)
				{
					if (i < this.m_lstCaptionSound.Count)
					{
						if (!NKCSoundManager.IsPlayingVoice(this.m_lstCaptionSound[i].key))
						{
							this.m_lstCaptions[i].CloseCaption();
							this.m_lstCaptionSound.RemoveAt(i);
							this.RefreshData();
							break;
						}
					}
					else if (this.m_lstCaptions[i].IsActive)
					{
						this.m_lstCaptions[i].CloseCaption();
					}
				}
			}
			if (this.m_lstCaptionTime.Count > 0 && this.m_fNextCheckTime <= Time.time)
			{
				this.m_fNextCheckTime = Time.time + 0.1f;
				this.m_fCaptionTimer = DateTime.Now.Ticks;
				for (int j = 0; j < this.m_lstCaptions.Count; j++)
				{
					if (this.m_iTimeCaptionKey < this.m_lstCaptionTime.Count)
					{
						if (this.m_lstCaptionTime[this.m_iTimeCaptionKey].startTime < this.m_fCaptionTimer)
						{
							this.RefreshData(this.m_iTimeCaptionKey);
						}
						if (this.m_lstCaptionTime[this.m_iTimeCaptionKey].endTime < this.m_fCaptionTimer)
						{
							this.m_lstCaptions[j].CloseCaption();
						}
						if (this.m_lstCaptionTime.Count > this.m_iTimeCaptionKey + 1 && this.m_lstCaptionTime[this.m_iTimeCaptionKey + 1].startTime < this.m_fCaptionTimer)
						{
							this.RefreshData(this.m_iTimeCaptionKey + 1);
						}
					}
					else if (this.m_lstCaptions[j].IsActive)
					{
						this.m_lstCaptions[j].CloseCaption();
					}
				}
			}
		}

		// Token: 0x04005DEE RID: 24046
		public List<NKCUIComCaption> m_lstCaptions = new List<NKCUIComCaption>();

		// Token: 0x04005DEF RID: 24047
		private List<NKCUIComCaption.CaptionData> m_lstCaptionSound = new List<NKCUIComCaption.CaptionData>();

		// Token: 0x04005DF0 RID: 24048
		private List<NKCUIComCaption.CaptionDataTime> m_lstCaptionTime = new List<NKCUIComCaption.CaptionDataTime>();

		// Token: 0x04005DF1 RID: 24049
		private Coroutine m_delayCoroutine;

		// Token: 0x04005DF2 RID: 24050
		private float m_fNextCheckTime;

		// Token: 0x04005DF3 RID: 24051
		private long m_fCaptionTimer;

		// Token: 0x04005DF4 RID: 24052
		private int m_iTimeCaptionKey;
	}
}
