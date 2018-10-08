using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Other/Screen Overlay")]
	[RequireComponent(typeof(Camera))]
	public class ScreenOverlay : PostEffectsBase
	{
		public enum OverlayBlendMode
		{
			Additive,
			ScreenBlend,
			Multiply,
			Overlay,
			AlphaBlend
		}

		public OverlayBlendMode blendMode = OverlayBlendMode.Overlay;

		public float intensity = 1f;

		public Texture2D texture;

		public Shader overlayShader;

		private Material overlayMaterial;

		public override bool CheckResources()
		{
			CheckSupport(false);
			overlayMaterial = CheckShaderAndCreateMaterial(overlayShader, overlayMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				Vector4 val = default(Vector4);
				val._002Ector(1f, 0f, 0f, 1f);
				overlayMaterial.SetVector("_UV_Transform", val);
				overlayMaterial.SetFloat("_Intensity", intensity);
				overlayMaterial.SetTexture("_Overlay", texture);
				Graphics.Blit(source, destination, overlayMaterial, (int)blendMode);
			}
		}
	}
}