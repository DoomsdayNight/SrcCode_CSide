using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007B0 RID: 1968
	public class NKCSideMenuSlotChild : MonoBehaviour
	{
		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x0017750F File Offset: 0x0017570F
		public string KEY
		{
			get
			{
				return this.m_strKey;
			}
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x00177518 File Offset: 0x00175718
		public void Init(string title, string key, RectTransform rtParent, NKCSideMenuSlotChild.OnClicked clicked)
		{
			base.gameObject.GetComponent<RectTransform>().SetParent(rtParent);
			NKCUtil.SetLabelText(this.m_TEXT, title);
			if (this.m_LayoutElement != null)
			{
				this.m_fMaxHeight = this.m_LayoutElement.preferredHeight;
				this.m_LayoutElement.preferredHeight = 0f;
			}
			this.m_strKey = key;
			this.dOnClicked = clicked;
			NKCUtil.SetGameobjectActive(this.m_SELECTED, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_Toggle != null)
			{
				this.m_Toggle.OnValueChanged.RemoveAllListeners();
				this.m_Toggle.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValuechange));
			}
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x001775D2 File Offset: 0x001757D2
		private void OnValuechange(bool bActive)
		{
			if (bActive && this.dOnClicked != null)
			{
				this.dOnClicked(this.m_strKey);
			}
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x001775F0 File Offset: 0x001757F0
		public void OnActive(bool Active)
		{
			Color color = NKCUtil.GetColor("#FFFFFF");
			if (!Active)
			{
				color.a = 0.6f;
			}
			NKCUtil.SetLabelTextColor(this.m_TEXT, color);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.StopAllCoroutines();
			base.StartCoroutine(this.ActiveButton(Active));
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x00177643 File Offset: 0x00175843
		public void OnBtnClick()
		{
			if (this.dOnClicked != null)
			{
				this.dOnClicked(this.m_strKey);
			}
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x0017765E File Offset: 0x0017585E
		public bool OnSelected(string ARTICLE_ID)
		{
			NKCUtil.SetGameobjectActive(this.m_SELECTED, string.Equals(ARTICLE_ID, this.m_strKey));
			return string.Equals(ARTICLE_ID, this.m_strKey);
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x00177683 File Offset: 0x00175883
		private IEnumerator ActiveButton(bool bActive)
		{
			if (bActive)
			{
				while (this.m_LayoutElement.preferredHeight < this.m_fMaxHeight)
				{
					this.m_LayoutElement.preferredHeight += this.fActiveSpeed;
					yield return new WaitForEndOfFrame();
				}
			}
			else
			{
				while (this.m_LayoutElement.preferredHeight > 0f)
				{
					this.m_LayoutElement.preferredHeight -= this.fActiveSpeed;
					yield return new WaitForEndOfFrame();
				}
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			this.m_LayoutElement.preferredHeight = Mathf.Clamp(this.m_LayoutElement.preferredHeight, 0f, this.m_fMaxHeight);
			yield return null;
			yield break;
		}

		// Token: 0x04003D79 RID: 15737
		public Text m_TEXT;

		// Token: 0x04003D7A RID: 15738
		public GameObject m_SELECTED;

		// Token: 0x04003D7B RID: 15739
		public GameObject m_LOCK;

		// Token: 0x04003D7C RID: 15740
		public NKCUIComStateButton m_BUTTON_CONTENT;

		// Token: 0x04003D7D RID: 15741
		public NKCUIComToggle m_Toggle;

		// Token: 0x04003D7E RID: 15742
		public LayoutElement m_LayoutElement;

		// Token: 0x04003D7F RID: 15743
		public float fActiveSpeed = 10f;

		// Token: 0x04003D80 RID: 15744
		private float m_fMaxHeight;

		// Token: 0x04003D81 RID: 15745
		private string m_strKey;

		// Token: 0x04003D82 RID: 15746
		private NKCSideMenuSlotChild.OnClicked dOnClicked;

		// Token: 0x02001472 RID: 5234
		// (Invoke) Token: 0x0600A8CA RID: 43210
		public delegate void OnClicked(string key);
	}
}
