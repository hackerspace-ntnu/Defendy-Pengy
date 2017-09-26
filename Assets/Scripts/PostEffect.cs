using UnityEngine;
using System.Collections;

public class PostEffect : MonoBehaviour 
{
	Camera AttachedCamera;
	public Shader Post_Outline;
	public Shader DrawSimple;
	Camera TempCam;
	Material Post_Mat;
	// public RenderTexture TempRT;


	void Start () 
	{
		AttachedCamera = GetComponent<Camera>();
		TempCam = new GameObject().AddComponent<Camera>();
		TempCam.enabled = false;
		Post_Mat = new Material(Post_Outline);
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		//set up a temporary camera
		TempCam.CopyFrom(AttachedCamera);
		TempCam.clearFlags = CameraClearFlags.Color;
		TempCam.backgroundColor = Color.black;

		//cull any layer that isn't the outline
		TempCam.cullingMask = 1 << LayerMask.NameToLayer("Outline");

		//make the temporary rendertexture
		RenderTexture TempRT = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);

		//put it to video memory
		TempRT.Create();

		//set the camera's target texture when rendering
		TempCam.targetTexture = TempRT;

		//render all objects this camera can render, but with our custom shader.
		TempCam.RenderWithShader(DrawSimple,"");

		//copy the temporary RT to the final image
		Graphics.Blit(TempRT, destination,Post_Mat);

		//release the temporary RT
		TempRT.Release();
	}

}