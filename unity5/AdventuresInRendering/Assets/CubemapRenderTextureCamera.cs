using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CubemapRenderTextureCamera : MonoBehaviour 
{

	// Attach this script to an object that uses a Reflective shader.
	// Realtime reflective cubemaps!
		
	int cubemapSize = 128;
	bool oneFacePerFrame = false;
	
	Camera _renderTextureCamera;
	Camera renderTextureCamera
	{
		get
		{
			if (!_renderTextureCamera)
			{
				var go = new GameObject ();
				go.AddComponent <Camera> ();
				//go.hideFlags = HideFlags.HideAndDontSave;
				go.transform.position = transform.position;
				go.transform.rotation = Quaternion.identity;
				_renderTextureCamera = go.GetComponent<Camera> ();
				_renderTextureCamera.farClipPlane = 100; // don't render very far into cubemap
				_renderTextureCamera.enabled = false;
			}
			return _renderTextureCamera;
		}
		set
		{
			_renderTextureCamera = value;
		}
	}
	
	private RenderTexture _renderTexture;
	private RenderTexture renderTexture
	{
		get
		{
			if ((renderTextureCamera) && (!_renderTexture)) 
			{	
				_renderTexture = new RenderTexture (cubemapSize, cubemapSize, 16);
				_renderTexture.isCubemap = true;
				_renderTexture.hideFlags = HideFlags.HideAndDontSave;
				GetComponent<Renderer>().sharedMaterial.SetTexture ("_Cube", _renderTexture);
			}
			return _renderTexture;
		}
		set
		{
			_renderTexture = value;
		}
	}
	
	void Start ()
	{
		// render all six faces at startup
		UpdateCubemap( 63 );
	}
	
	void LateUpdate ()
	{
		if (oneFacePerFrame)
		{
			int faceToRender = Time.frameCount % 6;
			int faceMask = 1 << faceToRender;
			UpdateCubemap (faceMask);
		} 
		else
		{
			UpdateCubemap (63); // all six faces
		}
	}
	
	void UpdateCubemap (int faceMask)
	{		
		renderTextureCamera.transform.position = transform.position;
		renderTextureCamera.RenderToCubemap (renderTexture, faceMask);
	}
	
	void OnDisable () 
	{
		DestroyImmediate (renderTextureCamera);
		DestroyImmediate (renderTexture);
	}}
