using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AEC RID: 2796
	public class NKCUIComMapSectionButton : MonoBehaviour
	{
		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06007D80 RID: 32128 RVA: 0x002A119B File Offset: 0x0029F39B
		// (set) Token: 0x06007D81 RID: 32129 RVA: 0x002A11A3 File Offset: 0x0029F3A3
		public int m_iSectionId { get; private set; }

		// Token: 0x06007D82 RID: 32130 RVA: 0x002A11AC File Offset: 0x0029F3AC
		public void SetLock(int sectionId, bool value)
		{
			this.m_iSectionId = sectionId;
			NKCUtil.SetGameobjectActive(this.m_objNormal, !value);
			NKCUtil.SetGameobjectActive(this.m_objLock, value);
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x002A11D0 File Offset: 0x0029F3D0
		public bool IsLocked()
		{
			return this.m_objLock.activeSelf;
		}

		// Token: 0x06007D84 RID: 32132 RVA: 0x002A11DD File Offset: 0x0029F3DD
		public void SetRedDot(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objRedDot, value);
		}

		// Token: 0x06007D85 RID: 32133 RVA: 0x002A11EB File Offset: 0x0029F3EB
		public bool IsRedDotOn()
		{
			return !(this.m_objRedDot == null) && this.m_objRedDot.activeSelf;
		}

		// Token: 0x04006A6F RID: 27247
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04006A70 RID: 27248
		public Text m_lbNormalText;

		// Token: 0x04006A71 RID: 27249
		public Text m_lbLockText;

		// Token: 0x04006A72 RID: 27250
		public GameObject m_objNormal;

		// Token: 0x04006A73 RID: 27251
		public GameObject m_objLock;

		// Token: 0x04006A74 RID: 27252
		public GameObject m_objRedDot;
	}
}
