using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.Patcher
{
	// Token: 0x02000891 RID: 2193
	public class NKCPatcherUI : MonoBehaviour
	{
		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06005749 RID: 22345 RVA: 0x001A37CA File Offset: 0x001A19CA
		// (set) Token: 0x0600574A RID: 22346 RVA: 0x001A37EB File Offset: 0x001A19EB
		private float ProgressBarValue
		{
			get
			{
				if (!(this.sdProgressBar != null))
				{
					return 0f;
				}
				return this.sdProgressBar.value;
			}
			set
			{
				if (this.sdProgressBar != null)
				{
					this.sdProgressBar.value = value;
				}
			}
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x001A3808 File Offset: 0x001A1A08
		private void Awake()
		{
			NKCUtil.SetLabelText(this.lbAppVersion, NKCUtilString.GetAppVersionText());
			NKCUtil.SetLabelText(this.lbProtocolVersion, NKCUtilString.GetProtocolVersionText());
			if (NKCPatcher.IsIntegrityCheckReserved())
			{
				NKCUtil.SetGameobjectActive(this.m_ctglIntegrityCheck, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_ctglIntegrityCheck, true);
				this.m_ctglIntegrityCheck.OnValueChanged.RemoveAllListeners();
				this.m_ctglIntegrityCheck.OnValueChanged.AddListener(delegate(bool v)
				{
					PlayerPrefsContainer.Set("PatchIntegrityCheck", v);
				});
			}
			if (this.evtBackground != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					this.m_bBGTouch = true;
				});
				this.evtBackground.triggers.Add(entry);
			}
			NKCUtil.SetGameobjectActive(this.evtBackground, false);
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x001A38E4 File Offset: 0x001A1AE4
		private void Start()
		{
			RectTransform componentInChildren = base.gameObject.GetComponentInChildren<RectTransform>();
			Vector3 localPosition = componentInChildren.localPosition;
			localPosition.Set(0f, 0f, 0f);
			componentInChildren.localPosition = localPosition;
			NKCUtil.SetLabelText(this.lbVersionCode, NKCConnectionInfo.s_ServerType);
			NKCUtil.SetLabelText(this.lbProgressText, "");
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x001A393F File Offset: 0x001A1B3F
		public void SetIntegrityCheckProgress()
		{
			NKCPatchDownloader.Instance.onIntegrityCheckProgress = new NKCPatchDownloader.OnIntegrityCheckProgress(this.<SetIntegrityCheckProgress>g__OnIntegrityCheckProgress|17_0);
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x001A3957 File Offset: 0x001A1B57
		public void SetActive(bool active)
		{
			NKCUtil.SetGameobjectActive(this, active);
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x001A3960 File Offset: 0x001A1B60
		public void SetProgressText(string str)
		{
			NKCUtil.SetLabelText(this.lbNoticeText, str);
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x001A396E File Offset: 0x001A1B6E
		public void Progress()
		{
			if (this.lbNoticeText == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.lbNoticeText, this.lbNoticeText.text + ".");
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x001A399F File Offset: 0x001A1B9F
		public void SetActiveBackGround(bool active)
		{
			NKCUtil.SetGameobjectActive(this.lbCanDownloadBackground, active);
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x001A39AD File Offset: 0x001A1BAD
		public void Set_lbCanDownloadBackground(string str)
		{
			NKCUtil.SetLabelText(this.lbCanDownloadBackground, str);
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x001A39BB File Offset: 0x001A1BBB
		public bool BackGroundTextIsNull()
		{
			return this.lbCanDownloadBackground == null;
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x001A39C9 File Offset: 0x001A1BC9
		public void SetForTouchWait()
		{
			NKCUtil.SetGameobjectActive(this.evtBackground, true);
			NKCUtil.SetGameobjectActive(this.sdProgressBar, false);
			NKCUtil.SetGameobjectActive(this.lbNoticeText, false);
			NKCUtil.SetGameobjectActive(this.lbProgressText, false);
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x001A39FB File Offset: 0x001A1BFB
		public IEnumerator WaitForTouch()
		{
			float fTime = 0f;
			while (!this.m_bBGTouch)
			{
				this.lbCanDownloadBackground.color = new Color(1f, 1f, 1f, (Mathf.Cos(fTime * 3f) + 1f) * 0.5f);
				fTime += Time.unscaledDeltaTime;
				yield return null;
			}
			yield break;
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x001A3A0C File Offset: 0x001A1C0C
		public void OnFileDownloadProgressTotal(long currentByte, long maxByte)
		{
			float progressBarValue = (float)currentByte / (float)maxByte;
			this.ProgressBarValue = progressBarValue;
			NKCUtil.SetLabelText(this.lbProgressText, string.Format("{0:0.00%}", this.ProgressBarValue));
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x001A3A46 File Offset: 0x001A1C46
		public void OnInvalidPatcherVideoPlayer(bool active)
		{
			NKCUtil.SetGameobjectActive(this.m_objFallbackBG, active);
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x001A3A68 File Offset: 0x001A1C68
		[CompilerGenerated]
		private void <SetIntegrityCheckProgress>g__OnIntegrityCheckProgress|17_0(int fileCount, int totalCount)
		{
			if (totalCount != 0)
			{
				float num = (float)fileCount / (float)totalCount;
				NKCUtil.SetLabelText(this.lbNoticeText, string.Format("{0} ({1:0.00%})", NKCStringTable.GetString("SI_DP_PATCHER_INTEGRITY_CHECK", false), num));
			}
		}

		// Token: 0x04004524 RID: 17700
		[SerializeField]
		public Slider sdProgressBar;

		// Token: 0x04004525 RID: 17701
		[SerializeField]
		private TMP_Text lbNoticeText;

		// Token: 0x04004526 RID: 17702
		[SerializeField]
		private TMP_Text lbProgressText;

		// Token: 0x04004527 RID: 17703
		[SerializeField]
		private TMP_Text lbVersionCode;

		// Token: 0x04004528 RID: 17704
		[SerializeField]
		private TMP_Text lbAppVersion;

		// Token: 0x04004529 RID: 17705
		[SerializeField]
		private TMP_Text lbProtocolVersion;

		// Token: 0x0400452A RID: 17706
		[SerializeField]
		private Text lbCanDownloadBackground;

		// Token: 0x0400452B RID: 17707
		[SerializeField]
		private GameObject m_touchToStart;

		// Token: 0x0400452C RID: 17708
		[Header("���� �÷��� �� �ɶ� ���� ���")]
		public GameObject m_objFallbackBG;

		// Token: 0x0400452D RID: 17709
		[Header("���� ���ἶ �˻� ��ư")]
		public NKCUIComToggle m_ctglIntegrityCheck;

		// Token: 0x0400452E RID: 17710
		[Header("��� ��ġ")]
		public EventTrigger evtBackground;

		// Token: 0x0400452F RID: 17711
		private bool m_bBGTouch;
	}
}
