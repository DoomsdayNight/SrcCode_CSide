using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guide
{
	// Token: 0x02000C3A RID: 3130
	public class NKCUIPopupGuideSubSlot : MonoBehaviour
	{
		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x0600915D RID: 37213 RVA: 0x00318DB3 File Offset: 0x00316FB3
		public string ARTICLE_ID
		{
			get
			{
				return this.m_articleID;
			}
		}

		// Token: 0x0600915E RID: 37214 RVA: 0x00318DBC File Offset: 0x00316FBC
		public void Init(string title, string articleID, NKCUIPopupGuideSubSlot.OnClicked clicked)
		{
			NKCUtil.SetLabelText(this.m_TEXT, title);
			if (this.m_LayoutElement != null)
			{
				this.m_fMaxHeight = this.m_LayoutElement.preferredHeight;
				this.m_LayoutElement.preferredHeight = 0f;
			}
			this.m_articleID = articleID;
			this.dOnClicked = clicked;
			NKCUtil.SetGameobjectActive(this.m_SELECTED, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetBindFunction(this.m_BUTTON_CONTENT, new UnityAction(this.OnBtnClick));
		}

		// Token: 0x0600915F RID: 37215 RVA: 0x00318E44 File Offset: 0x00317044
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

		// Token: 0x06009160 RID: 37216 RVA: 0x00318E97 File Offset: 0x00317097
		public void OnBtnClick()
		{
			if (this.dOnClicked != null)
			{
				this.dOnClicked(this.m_articleID, 0);
			}
		}

		// Token: 0x06009161 RID: 37217 RVA: 0x00318EB4 File Offset: 0x003170B4
		public bool OnSelected(string ARTICLE_ID)
		{
			bool flag = string.Equals(ARTICLE_ID, this.m_articleID);
			this.m_BUTTON_CONTENT.Select(flag, true, false);
			return flag;
		}

		// Token: 0x06009162 RID: 37218 RVA: 0x00318EDE File Offset: 0x003170DE
		public void OnSelectedObject(string ARTICLE_ID)
		{
			NKCUtil.SetGameobjectActive(this.m_SELECTED, string.Equals(ARTICLE_ID, this.m_articleID));
		}

		// Token: 0x06009163 RID: 37219 RVA: 0x00318EF7 File Offset: 0x003170F7
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

		// Token: 0x04007E89 RID: 32393
		public Text m_TEXT;

		// Token: 0x04007E8A RID: 32394
		public GameObject m_DOT;

		// Token: 0x04007E8B RID: 32395
		public GameObject m_SELECTED;

		// Token: 0x04007E8C RID: 32396
		public NKCUIComStateButton m_BUTTON_CONTENT;

		// Token: 0x04007E8D RID: 32397
		public LayoutElement m_LayoutElement;

		// Token: 0x04007E8E RID: 32398
		public float fActiveSpeed = 10f;

		// Token: 0x04007E8F RID: 32399
		private float m_fMaxHeight;

		// Token: 0x04007E90 RID: 32400
		private string m_articleID;

		// Token: 0x04007E91 RID: 32401
		private NKCUIPopupGuideSubSlot.OnClicked dOnClicked;

		// Token: 0x02001A05 RID: 6661
		// (Invoke) Token: 0x0600BACA RID: 47818
		public delegate void OnClicked(string ArticleID, int idx = 0);
	}
}
