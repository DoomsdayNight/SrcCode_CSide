using System;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x020007DA RID: 2010
	public class NKCUIUnitSelect : MonoBehaviour
	{
		// Token: 0x06004F4C RID: 20300 RVA: 0x0017F538 File Offset: 0x0017D738
		public void Init(UnityAction addListener = null)
		{
			this.m_Anim = base.GetComponent<Animator>();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_UNIT_add = base.GetComponentInChildren<NKCUIComStateButton>();
			if (addListener != null)
			{
				this.m_UNIT_add.PointerClick.RemoveAllListeners();
				this.m_UNIT_add.PointerClick.AddListener(addListener);
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0017F58D File Offset: 0x0017D78D
		public void Prepare()
		{
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x0017F58F File Offset: 0x0017D78F
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x0017F59D File Offset: 0x0017D79D
		public void SetListener(UnityAction addListener)
		{
			this.m_UNIT_add.PointerClick.RemoveAllListeners();
			this.m_UNIT_add.PointerClick.AddListener(addListener);
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0017F5C0 File Offset: 0x0017D7C0
		public void Outro()
		{
			this.m_Anim.SetTrigger("Exit");
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x0017F5D2 File Offset: 0x0017D7D2
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04003F47 RID: 16199
		private Animator m_Anim;

		// Token: 0x04003F48 RID: 16200
		private NKCUIComStateButton m_UNIT_add;
	}
}
