using System;
using System.IO;
using System.Runtime.CompilerServices;
using AssetBundles;
using UnityEngine;

namespace NKC.InfraTool
{
	// Token: 0x02000894 RID: 2196
	public class PatchToolGUI : MonoBehaviour
	{
		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x001A495B File Offset: 0x001A2B5B
		private IConfigChecker ConfigCheckController
		{
			get
			{
				return this._patchCheckController;
			}
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x001A4963 File Offset: 0x001A2B63
		private void Awake()
		{
			this.ConfigCheckController.Init();
			base.StartCoroutine(this.ConfigCheckController.UpdateServerInfo());
			base.StartCoroutine(this.ConfigCheckController.RunAll());
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x001A4994 File Offset: 0x001A2B94
		private void Update()
		{
			this.m_editorW = Screen.width;
			this.m_editorH = Screen.height;
			this.ConfigCheckController.Update();
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x001A49B7 File Offset: 0x001A2BB7
		private void OnGUI()
		{
			GUIFormat.BeginArea(new Rect(0f, 0f, (float)(this.m_editorW - this.Padding), (float)this.m_editorH), new Action(this.<OnGUI>g__Area|9_0));
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x001A4A0C File Offset: 0x001A2C0C
		[CompilerGenerated]
		private void <OnGUI>g__Area|9_0()
		{
			GUIFormat.Horizontal(new Action(this.<OnGUI>g__InputField|9_1), true);
			GUIFormat.Horizontal(new Action(this.<OnGUI>g__Buttons|9_3), true);
			GUIFormat.Horizontal(new Action(PatchToolGUI.<OnGUI>g__Button1|9_4), true);
			GUIFormat.Horizontal(new Action(this.<OnGUI>g__Buttons1|9_5), true);
			GUIFormat.Vertical(new Action(this.<OnGUI>g__InfoLabel|9_6), true);
			GUIFormat.Horizontal(new Action(PatchToolGUI.<OnGUI>g__Toggles|9_7), true);
			GUIFormat.ScrollView(new Action(this.<OnGUI>g__ShowLog|9_8), (float)this.m_editorW, (float)this.m_editorH, true);
			GUIFormat.Vertical(new Action(this.<OnGUI>g__Solution|9_9), true);
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x001A4AB7 File Offset: 0x001A2CB7
		[CompilerGenerated]
		private void <OnGUI>g__InputField|9_1()
		{
			GUIFormat.Label("ConfigAddress : ", null, 30);
			this.ConfigCheckController.BaseFileServerAddress = GUIFormat.TextField(this.ConfigCheckController.BaseFileServerAddress, GUI.skin.textField, 30);
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x001A4AF0 File Offset: 0x001A2CF0
		[CompilerGenerated]
		private void <OnGUI>g__RunButtons|9_2()
		{
			if (GUIFormat.Button("1. [ Config Update ]", TextAnchor.MiddleCenter, 30, 50))
			{
				base.StartCoroutine(this.ConfigCheckController.ConfigRequest());
			}
			if (GUIFormat.Button("2. [ Start DownLoad ]", TextAnchor.MiddleCenter, 30, 50))
			{
				base.StartCoroutine(this.ConfigCheckController.StartDownLoad());
			}
			if (GUIFormat.Button("3. [ Login Request ]", TextAnchor.MiddleCenter, 30, 50))
			{
				this.ConfigCheckController.RequestLoginConnection();
			}
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x001A4B60 File Offset: 0x001A2D60
		[CompilerGenerated]
		private void <OnGUI>g__Buttons|9_3()
		{
			if (GUIFormat.Button("[ Run ] ", TextAnchor.MiddleCenter, 30, 50))
			{
				base.StartCoroutine(this.ConfigCheckController.RunAll());
			}
			if (GUIFormat.Button("[ Run ] Extra Asset ", TextAnchor.MiddleCenter, 30, 50))
			{
				base.StartCoroutine(this.ConfigCheckController.ExtraAssetRunAll());
			}
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x001A4BB4 File Offset: 0x001A2DB4
		[CompilerGenerated]
		internal static void <OnGUI>g__Button1|9_4()
		{
			string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			if (Directory.Exists(localDownloadPath) && GUIFormat.Button("[Clear AssetBundle Cache]", TextAnchor.MiddleCenter, 30, 50))
			{
				Directory.Delete(localDownloadPath, true);
			}
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x001A4BE8 File Offset: 0x001A2DE8
		[CompilerGenerated]
		private void <OnGUI>g__Buttons1|9_5()
		{
			if (GUIFormat.Button("[ Save Log ]", TextAnchor.MiddleCenter, 30, 50))
			{
				this.ConfigCheckController.SaveLogToText();
			}
			if (this.ConfigCheckController.IsSaveToTag && GUIFormat.Button("[ Save Tag ]", TextAnchor.MiddleCenter, 30, 50))
			{
				this.ConfigCheckController.SaveTagListToText();
			}
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x001A4C3A File Offset: 0x001A2E3A
		[CompilerGenerated]
		private void <OnGUI>g__InfoLabel|9_6()
		{
			GUIFormat.Label("[ Status ]", null, 30);
			GUIFormat.Label(this.ConfigCheckController.VersionInfoStr ?? "", GUI.skin.box, 30);
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x001A4C6E File Offset: 0x001A2E6E
		[CompilerGenerated]
		internal static void <OnGUI>g__Toggles|9_7()
		{
			GUIFormat.Label("[ Log ]", null, 30);
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x001A4C7D File Offset: 0x001A2E7D
		[CompilerGenerated]
		private void <OnGUI>g__ShowLog|9_8()
		{
			GUIFormat.Label(this.ConfigCheckController.logStr ?? "", null, 30);
		}

		// Token: 0x060057A2 RID: 22434 RVA: 0x001A4C9B File Offset: 0x001A2E9B
		[CompilerGenerated]
		private void <OnGUI>g__Solution|9_9()
		{
			GUIFormat.Label("[ Solution ]", null, 30);
			GUIFormat.Box(this.ConfigCheckController.ErrorSolutionStr ?? "", this.m_editorH, this.boxGab);
		}

		// Token: 0x04004542 RID: 17730
		public int Padding;

		// Token: 0x04004543 RID: 17731
		private int m_editorW;

		// Token: 0x04004544 RID: 17732
		private int m_editorH;

		// Token: 0x04004545 RID: 17733
		public int boxGab = 55;

		// Token: 0x04004546 RID: 17734
		private PatchCheckController _patchCheckController = new PatchCheckController();
	}
}
