using System;
using System.Collections;
using NKC.Publisher;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x02000871 RID: 2161
	public class NKCLogo : MonoBehaviour
	{
		// Token: 0x060055D5 RID: 21973 RVA: 0x0019F95D File Offset: 0x0019DB5D
		public void Init()
		{
			if (this.isInit)
			{
				return;
			}
			this.InitUI();
			this.isInit = true;
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x0019F975 File Offset: 0x0019DB75
		private void InitUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NPUF_BSIDE_LOGO, false);
			NKCUtil.SetGameobjectActive(this.m_NPUF_GAMEBEANS_LOGO, false);
			NKCUtil.SetGameobjectActive(this.m_NPUF_ZLONG_LOGO, false);
			NKCUtil.SetGameobjectActive(this.m_NPUF_ZLONG_LOGO3, false);
			NKCUtil.SetGameobjectActive(this.m_NPUF_BSIDE_PC_GAME_GRADE, false);
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x0019F9B3 File Offset: 0x0019DBB3
		public IEnumerator DisplayLogo()
		{
			if (NKCLogo.s_bLogoPlayed)
			{
				yield break;
			}
			if (NKCDefineManager.DEFINE_IOS())
			{
				yield return new WaitForSeconds(1f);
			}
			NKCPublisherModule.ePublisherType publisherType = NKCPublisherModule.PublisherType;
			switch (publisherType)
			{
			case NKCPublisherModule.ePublisherType.None:
			case NKCPublisherModule.ePublisherType.NexonToy:
			case NKCPublisherModule.ePublisherType.NexonPC:
				goto IL_148;
			case NKCPublisherModule.ePublisherType.Zlong:
				if (NKCDefineManager.DEFINE_ZLONG_SEA())
				{
					yield return this.ProcessLogo(this.m_NPUF_ZLONG_LOGO, -1f);
					goto IL_16F;
				}
				if (NKCDefineManager.DEFINE_ZLONG_CHN())
				{
					yield return this.ProcessLogo(this.m_NPUF_ZLONG_LOGO3, -1f);
					goto IL_16F;
				}
				yield return this.ProcessLogo(this.m_NPUF_GAMEBEANS_LOGO, -1f);
				goto IL_16F;
			case NKCPublisherModule.ePublisherType.SB_Gamebase:
				break;
			default:
				if (publisherType != NKCPublisherModule.ePublisherType.STEAM)
				{
					goto IL_148;
				}
				break;
			}
			yield return this.ProcessLogo(this.m_NPUF_BSIDE_LOGO, -1f);
			goto IL_16F;
			IL_148:
			yield return this.ProcessLogo(this.m_NPUF_BSIDE_LOGO, -1f);
			IL_16F:
			NKCLogo.s_bLogoPlayed = true;
			yield break;
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x0019F9C2 File Offset: 0x0019DBC2
		public IEnumerator DisplayGameGrade()
		{
			yield return this.ProcessLogo(this.m_NPUF_BSIDE_PC_GAME_GRADE, 3f);
			yield break;
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x0019F9D1 File Offset: 0x0019DBD1
		private IEnumerator ProcessLogo(GameObject obj, float fSpecialLogoTime = -1f)
		{
			if (obj == null)
			{
				yield break;
			}
			obj.SetActive(true);
			obj.transform.localPosition = Vector3.zero;
			if (fSpecialLogoTime != -1f)
			{
				yield return new WaitForSeconds(fSpecialLogoTime);
			}
			else
			{
				yield return new WaitForSeconds(this.m_fLogoTime);
			}
			obj.SetActive(false);
			if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong)
			{
				UnityEngine.Object.Destroy(obj);
			}
			yield break;
		}

		// Token: 0x04004483 RID: 17539
		public GameObject m_NPUF_BSIDE_LOGO;

		// Token: 0x04004484 RID: 17540
		public GameObject m_NPUF_GAMEBEANS_LOGO;

		// Token: 0x04004485 RID: 17541
		public GameObject m_NPUF_ZLONG_LOGO;

		// Token: 0x04004486 RID: 17542
		public GameObject m_NPUF_ZLONG_LOGO3;

		// Token: 0x04004487 RID: 17543
		public GameObject m_NPUF_BSIDE_PC_GAME_GRADE;

		// Token: 0x04004488 RID: 17544
		private float m_fLogoTime = 1.5f;

		// Token: 0x04004489 RID: 17545
		public static bool s_bLogoPlayed;

		// Token: 0x0400448A RID: 17546
		private bool isInit;
	}
}
