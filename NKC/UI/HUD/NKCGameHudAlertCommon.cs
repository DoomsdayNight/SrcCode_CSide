using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C3D RID: 3133
	public class NKCGameHudAlertCommon : MonoBehaviour, IGameHudAlert
	{
		// Token: 0x06009222 RID: 37410 RVA: 0x0031DFAC File Offset: 0x0031C1AC
		public void AddAlert(List<NKCGameHudAlertCommon.NKCGameHudAlertData> lstData)
		{
			foreach (NKCGameHudAlertCommon.NKCGameHudAlertData item in lstData)
			{
				this.m_qData.Enqueue(item);
			}
		}

		// Token: 0x06009223 RID: 37411 RVA: 0x0031E000 File Offset: 0x0031C200
		public void AddAlert(NKCGameHudAlertCommon.NKCGameHudAlertData data)
		{
			this.m_qData.Enqueue(data);
		}

		// Token: 0x06009224 RID: 37412 RVA: 0x0031E00E File Offset: 0x0031C20E
		public void AddAlert(string title, string desc, string iconName)
		{
			this.m_qData.Enqueue(new NKCGameHudAlertCommon.NKCGameHudAlertData(title, desc, iconName));
		}

		// Token: 0x06009225 RID: 37413 RVA: 0x0031E024 File Offset: 0x0031C224
		private void SetData(NKCGameHudAlertCommon.NKCGameHudAlertData data)
		{
			Sprite sp;
			if (!string.IsNullOrEmpty(data.iconName))
			{
				sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_BC", data.iconName, false);
			}
			else
			{
				sp = null;
			}
			NKCUtil.SetLabelText(this.m_Title, data.title);
			NKCUtil.SetLabelText(this.m_Desc, data.desc);
			NKCUtil.SetImageSprite(this.m_Image, sp, true);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_Animator.SetTrigger("PLAY");
			this.m_Animator.GetCurrentAnimatorStateInfo(0);
		}

		// Token: 0x06009226 RID: 37414 RVA: 0x0031E0AC File Offset: 0x0031C2AC
		public void SetActive(bool value)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, value);
		}

		// Token: 0x06009227 RID: 37415 RVA: 0x0031E0BC File Offset: 0x0031C2BC
		public void OnStart()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_qData.Count > 0)
			{
				NKCGameHudAlertCommon.NKCGameHudAlertData data = this.m_qData.Dequeue();
				this.SetData(data);
			}
		}

		// Token: 0x06009228 RID: 37416 RVA: 0x0031E0F8 File Offset: 0x0031C2F8
		public void OnUpdate()
		{
			if (this.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				return;
			}
			if (this.m_qData.Count > 0)
			{
				NKCGameHudAlertCommon.NKCGameHudAlertData data = this.m_qData.Dequeue();
				this.SetData(data);
			}
		}

		// Token: 0x06009229 RID: 37417 RVA: 0x0031E144 File Offset: 0x0031C344
		public bool IsFinished()
		{
			return this.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && this.m_qData.Count == 0;
		}

		// Token: 0x0600922A RID: 37418 RVA: 0x0031E17C File Offset: 0x0031C37C
		public void OnCleanup()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04007F16 RID: 32534
		public Animator m_Animator;

		// Token: 0x04007F17 RID: 32535
		public Image m_Image;

		// Token: 0x04007F18 RID: 32536
		public Text m_Title;

		// Token: 0x04007F19 RID: 32537
		public Text m_Desc;

		// Token: 0x04007F1A RID: 32538
		private Queue<NKCGameHudAlertCommon.NKCGameHudAlertData> m_qData = new Queue<NKCGameHudAlertCommon.NKCGameHudAlertData>();

		// Token: 0x02001A0A RID: 6666
		public class NKCGameHudAlertData
		{
			// Token: 0x0600BADB RID: 47835 RVA: 0x0036E612 File Offset: 0x0036C812
			public NKCGameHudAlertData(string _title, string _desc, string _iconName)
			{
				this.title = _title;
				this.desc = _desc;
				this.iconName = _iconName;
			}

			// Token: 0x0400AD93 RID: 44435
			public string title;

			// Token: 0x0400AD94 RID: 44436
			public string desc;

			// Token: 0x0400AD95 RID: 44437
			public string iconName;
		}
	}
}
