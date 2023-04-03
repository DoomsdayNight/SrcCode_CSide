using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002EB RID: 747
	[ExecuteInEditMode]
	[RequireComponent(typeof(CanvasRenderer), typeof(ParticleSystem))]
	[AddComponentMenu("UI/Effects/Extensions/UIParticleSystem")]
	public class UIParticleSystem : MaskableGraphic
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00037BFC File Offset: 0x00035DFC
		public override Texture mainTexture
		{
			get
			{
				return this.currentTexture;
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00037C04 File Offset: 0x00035E04
		protected bool Initialize()
		{
			if (this._transform == null)
			{
				this._transform = base.transform;
			}
			if (this.pSystem == null)
			{
				this.pSystem = base.GetComponent<ParticleSystem>();
				if (this.pSystem == null)
				{
					return false;
				}
				this.mainModule = this.pSystem.main;
				if (this.pSystem.main.maxParticles > 14000)
				{
					this.mainModule.maxParticles = 14000;
				}
				this.pRenderer = this.pSystem.GetComponent<ParticleSystemRenderer>();
				if (this.pRenderer != null)
				{
					this.pRenderer.enabled = false;
				}
				if (this.material == null)
				{
					Shader shaderInstance = ShaderLibrary.GetShaderInstance("UI Extensions/Particles/Additive");
					if (shaderInstance)
					{
						this.material = new Material(shaderInstance);
					}
				}
				this.currentMaterial = this.material;
				if (this.currentMaterial && this.currentMaterial.HasProperty("_MainTex"))
				{
					this.currentTexture = this.currentMaterial.mainTexture;
					if (this.currentTexture == null)
					{
						this.currentTexture = Texture2D.whiteTexture;
					}
				}
				this.material = this.currentMaterial;
				this.mainModule.scalingMode = ParticleSystemScalingMode.Hierarchy;
				this.particles = null;
			}
			if (this.particles == null)
			{
				this.particles = new ParticleSystem.Particle[this.pSystem.main.maxParticles];
			}
			this.imageUV = new Vector4(0f, 0f, 1f, 1f);
			this.textureSheetAnimation = this.pSystem.textureSheetAnimation;
			this.textureSheetAnimationFrames = 0;
			this.textureSheetAnimationFrameSize = Vector2.zero;
			if (this.textureSheetAnimation.enabled)
			{
				this.textureSheetAnimationFrames = this.textureSheetAnimation.numTilesX * this.textureSheetAnimation.numTilesY;
				this.textureSheetAnimationFrameSize = new Vector2(1f / (float)this.textureSheetAnimation.numTilesX, 1f / (float)this.textureSheetAnimation.numTilesY);
			}
			return true;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00037E21 File Offset: 0x00036021
		protected override void Awake()
		{
			base.Awake();
			if (!this.Initialize())
			{
				base.enabled = false;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00037E38 File Offset: 0x00036038
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (!this.isInitialised && !this.pSystem.main.playOnAwake)
			{
				this.pSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
				this.isInitialised = true;
			}
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			Vector2 zero3 = Vector2.zero;
			int num = this.pSystem.GetParticles(this.particles);
			for (int i = 0; i < num; i++)
			{
				ParticleSystem.Particle particle = this.particles[i];
				Vector2 vector = (this.mainModule.simulationSpace == ParticleSystemSimulationSpace.Local) ? particle.position : this._transform.InverseTransformPoint(particle.position);
				float num2 = -particle.rotation * 0.017453292f;
				float f = num2 + 1.5707964f;
				Color32 currentColor = particle.GetCurrentColor(this.pSystem);
				float num3 = particle.GetCurrentSize(this.pSystem) * 0.5f;
				if (this.mainModule.scalingMode == ParticleSystemScalingMode.Shape)
				{
					vector /= base.canvas.scaleFactor;
				}
				Vector4 vector2 = this.imageUV;
				if (this.textureSheetAnimation.enabled)
				{
					float num4 = 1f - particle.remainingLifetime / particle.startLifetime;
					if (this.textureSheetAnimation.frameOverTime.curveMin != null)
					{
						num4 = this.textureSheetAnimation.frameOverTime.curveMin.Evaluate(1f - particle.remainingLifetime / particle.startLifetime);
					}
					else if (this.textureSheetAnimation.frameOverTime.curve != null)
					{
						num4 = this.textureSheetAnimation.frameOverTime.curve.Evaluate(1f - particle.remainingLifetime / particle.startLifetime);
					}
					else if (this.textureSheetAnimation.frameOverTime.constant > 0f)
					{
						num4 = this.textureSheetAnimation.frameOverTime.constant - particle.remainingLifetime / particle.startLifetime;
					}
					num4 = Mathf.Repeat(num4 * (float)this.textureSheetAnimation.cycleCount, 1f);
					int num5 = 0;
					ParticleSystemAnimationType animation = this.textureSheetAnimation.animation;
					if (animation != ParticleSystemAnimationType.WholeSheet)
					{
						if (animation == ParticleSystemAnimationType.SingleRow)
						{
							num5 = Mathf.FloorToInt(num4 * (float)this.textureSheetAnimation.numTilesX);
							int num6 = this.textureSheetAnimation.rowIndex;
							if (this.textureSheetAnimation.rowMode == ParticleSystemAnimationRowMode.Random)
							{
								num6 = Mathf.Abs((int)(particle.randomSeed % (uint)this.textureSheetAnimation.numTilesY));
							}
							num5 += num6 * this.textureSheetAnimation.numTilesX;
						}
					}
					else
					{
						num5 = Mathf.FloorToInt(num4 * (float)this.textureSheetAnimationFrames);
					}
					num5 %= this.textureSheetAnimationFrames;
					vector2.x = (float)(num5 % this.textureSheetAnimation.numTilesX) * this.textureSheetAnimationFrameSize.x;
					vector2.y = 1f - (float)(num5 / this.textureSheetAnimation.numTilesX + 1) * this.textureSheetAnimationFrameSize.y;
					vector2.z = vector2.x + this.textureSheetAnimationFrameSize.x;
					vector2.w = vector2.y + this.textureSheetAnimationFrameSize.y;
				}
				zero.x = vector2.x;
				zero.y = vector2.y;
				this._quad[0] = UIVertex.simpleVert;
				this._quad[0].color = currentColor;
				this._quad[0].uv0 = zero;
				zero.x = vector2.x;
				zero.y = vector2.w;
				this._quad[1] = UIVertex.simpleVert;
				this._quad[1].color = currentColor;
				this._quad[1].uv0 = zero;
				zero.x = vector2.z;
				zero.y = vector2.w;
				this._quad[2] = UIVertex.simpleVert;
				this._quad[2].color = currentColor;
				this._quad[2].uv0 = zero;
				zero.x = vector2.z;
				zero.y = vector2.y;
				this._quad[3] = UIVertex.simpleVert;
				this._quad[3].color = currentColor;
				this._quad[3].uv0 = zero;
				if (num2 == 0f)
				{
					zero2.x = vector.x - num3;
					zero2.y = vector.y - num3;
					zero3.x = vector.x + num3;
					zero3.y = vector.y + num3;
					zero.x = zero2.x;
					zero.y = zero2.y;
					this._quad[0].position = zero;
					zero.x = zero2.x;
					zero.y = zero3.y;
					this._quad[1].position = zero;
					zero.x = zero3.x;
					zero.y = zero3.y;
					this._quad[2].position = zero;
					zero.x = zero3.x;
					zero.y = zero2.y;
					this._quad[3].position = zero;
				}
				else if (this.use3dRotation)
				{
					Vector3 a = (this.mainModule.simulationSpace == ParticleSystemSimulationSpace.Local) ? particle.position : this._transform.InverseTransformPoint(particle.position);
					if (this.mainModule.scalingMode == ParticleSystemScalingMode.Shape)
					{
						vector /= base.canvas.scaleFactor;
					}
					Vector3[] array = new Vector3[]
					{
						new Vector3(-num3, -num3, 0f),
						new Vector3(-num3, num3, 0f),
						new Vector3(num3, num3, 0f),
						new Vector3(num3, -num3, 0f)
					};
					Quaternion rotation = Quaternion.Euler(particle.rotation3D);
					this._quad[0].position = a + rotation * array[0];
					this._quad[1].position = a + rotation * array[1];
					this._quad[2].position = a + rotation * array[2];
					this._quad[3].position = a + rotation * array[3];
				}
				else
				{
					Vector2 b = new Vector2(Mathf.Cos(num2), Mathf.Sin(num2)) * num3;
					Vector2 b2 = new Vector2(Mathf.Cos(f), Mathf.Sin(f)) * num3;
					this._quad[0].position = vector - b - b2;
					this._quad[1].position = vector - b + b2;
					this._quad[2].position = vector + b + b2;
					this._quad[3].position = vector + b - b2;
				}
				vh.AddUIVertexQuad(this._quad);
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00038650 File Offset: 0x00036850
		private void Update()
		{
			if (!this.fixedTime && Application.isPlaying)
			{
				this.pSystem.Simulate(Time.unscaledDeltaTime, false, false, true);
				this.SetAllDirty();
				if ((this.currentMaterial != null && this.currentTexture != this.currentMaterial.mainTexture) || (this.material != null && this.currentMaterial != null && this.material.shader != this.currentMaterial.shader))
				{
					this.pSystem = null;
					this.Initialize();
				}
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x000386F8 File Offset: 0x000368F8
		private void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				this.SetAllDirty();
			}
			else if (this.fixedTime)
			{
				this.pSystem.Simulate(Time.unscaledDeltaTime, false, false, true);
				this.SetAllDirty();
				if ((this.currentMaterial != null && this.currentTexture != this.currentMaterial.mainTexture) || (this.material != null && this.currentMaterial != null && this.material.shader != this.currentMaterial.shader))
				{
					this.pSystem = null;
					this.Initialize();
				}
			}
			if (this.material == this.currentMaterial)
			{
				return;
			}
			this.pSystem = null;
			this.Initialize();
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000387CA File Offset: 0x000369CA
		protected override void OnDestroy()
		{
			this.currentMaterial = null;
			this.currentTexture = null;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000387DA File Offset: 0x000369DA
		public void StartParticleEmission()
		{
			this.pSystem.time = 0f;
			this.pSystem.Play();
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000387F7 File Offset: 0x000369F7
		public void StopParticleEmission()
		{
			this.pSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00038806 File Offset: 0x00036A06
		public void PauseParticleEmission()
		{
			this.pSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
		}

		// Token: 0x04000B5A RID: 2906
		[Tooltip("Having this enabled run the system in LateUpdate rather than in Update making it faster but less precise (more clunky)")]
		public bool fixedTime = true;

		// Token: 0x04000B5B RID: 2907
		[Tooltip("Enables 3d rotation for the particles")]
		public bool use3dRotation;

		// Token: 0x04000B5C RID: 2908
		private Transform _transform;

		// Token: 0x04000B5D RID: 2909
		private ParticleSystem pSystem;

		// Token: 0x04000B5E RID: 2910
		private ParticleSystem.Particle[] particles;

		// Token: 0x04000B5F RID: 2911
		private UIVertex[] _quad = new UIVertex[4];

		// Token: 0x04000B60 RID: 2912
		private Vector4 imageUV = Vector4.zero;

		// Token: 0x04000B61 RID: 2913
		private ParticleSystem.TextureSheetAnimationModule textureSheetAnimation;

		// Token: 0x04000B62 RID: 2914
		private int textureSheetAnimationFrames;

		// Token: 0x04000B63 RID: 2915
		private Vector2 textureSheetAnimationFrameSize;

		// Token: 0x04000B64 RID: 2916
		private ParticleSystemRenderer pRenderer;

		// Token: 0x04000B65 RID: 2917
		private bool isInitialised;

		// Token: 0x04000B66 RID: 2918
		private Material currentMaterial;

		// Token: 0x04000B67 RID: 2919
		private Texture currentTexture;

		// Token: 0x04000B68 RID: 2920
		private ParticleSystem.MainModule mainModule;
	}
}
