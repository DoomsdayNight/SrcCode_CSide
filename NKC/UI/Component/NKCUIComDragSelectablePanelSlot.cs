using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C54 RID: 3156
	public class NKCUIComDragSelectablePanelSlot : MonoBehaviour
	{
		// Token: 0x0600931D RID: 37661 RVA: 0x00323480 File Offset: 0x00321680
		public void SetPrefabData(NKMAssetName assetName, int data, NKCUIComDragSelectablePanelSlot.OnClick onClick)
		{
			this.CleanUp();
			NKCUtil.SetButtonClickDelegate(this.m_Button, new UnityAction(this.OnButton));
			this.m_Data = data;
			this.dOnClick = onClick;
			this.m_prefabInstance = NKCAssetResourceManager.OpenInstance<GameObject>(assetName, false, null);
			if (this.m_prefabInstance != null && this.m_prefabInstance.m_Instant != null)
			{
				this.m_prefabInstance.m_Instant.transform.SetParent(this.m_rtPrefabRoot, false);
			}
			else
			{
				Debug.Log(string.Format("SetPrefabData Fail, file : {0}", assetName));
			}
			NKCUtil.SetGameobjectActive(this.m_Image, false);
			NKCUtil.SetGameobjectActive(this.m_rtPrefabRoot, true);
		}

		// Token: 0x0600931E RID: 37662 RVA: 0x00323528 File Offset: 0x00321728
		public void SetImageData(NKMAssetName assetName, int data, NKCUIComDragSelectablePanelSlot.OnClick onClick)
		{
			this.CleanUp();
			NKCUtil.SetButtonClickDelegate(this.m_Button, new UnityAction(this.OnButton));
			this.m_Data = data;
			this.dOnClick = onClick;
			NKCUtil.SetImageSprite(this.m_Image, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(assetName), false);
			NKCUtil.SetGameobjectActive(this.m_Image, true);
			NKCUtil.SetGameobjectActive(this.m_rtPrefabRoot, false);
		}

		// Token: 0x0600931F RID: 37663 RVA: 0x0032358A File Offset: 0x0032178A
		private void CleanUp()
		{
			if (this.m_prefabInstance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_prefabInstance);
				this.m_prefabInstance = null;
			}
			this.dOnClick = null;
		}

		// Token: 0x06009320 RID: 37664 RVA: 0x003235AD File Offset: 0x003217AD
		private void OnButton()
		{
			NKCUIComDragSelectablePanelSlot.OnClick onClick = this.dOnClick;
			if (onClick == null)
			{
				return;
			}
			onClick(this.m_Data);
		}

		// Token: 0x06009321 RID: 37665 RVA: 0x003235C5 File Offset: 0x003217C5
		private void OnDestroy()
		{
			this.CleanUp();
		}

		// Token: 0x04008013 RID: 32787
		public NKCUIComStateButton m_Button;

		// Token: 0x04008014 RID: 32788
		public Image m_Image;

		// Token: 0x04008015 RID: 32789
		public RectTransform m_rtPrefabRoot;

		// Token: 0x04008016 RID: 32790
		private NKCAssetInstanceData m_prefabInstance;

		// Token: 0x04008017 RID: 32791
		private int m_Data;

		// Token: 0x04008018 RID: 32792
		private NKCUIComDragSelectablePanelSlot.OnClick dOnClick;

		// Token: 0x02001A14 RID: 6676
		// (Invoke) Token: 0x0600BAFF RID: 47871
		public delegate void OnClick(int data);
	}
}
