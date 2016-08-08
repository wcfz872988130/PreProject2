using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MobileMovieTexture))]
public class TestMobileTexture : MonoBehaviour 
{
    private MobileMovieTexture m_movieTexture;

    void Awake()
    {
        m_movieTexture = GetComponent<MobileMovieTexture>();

        m_movieTexture.onFinished += OnFinished;
    }

    void OnFinished(MobileMovieTexture sender)
    {
        Debug.Log(sender.Path + " has finished ");
    }

    void OnGUI()
    {
//		Debug.Log("aaaaaaaaaaaaa");

        if (GUI.Button(new Rect(0, 0, 50, 50), m_movieTexture.isPlaying ? "Pause" : "Play" ))
        {
            if (m_movieTexture.isPlaying)
            {
                m_movieTexture.pause = true;
            }
            else
            {
                if (m_movieTexture.pause)
                {
                    m_movieTexture.pause = false;
                }
                else
                {
                    m_movieTexture.Play();
                }
            }
        }
//		GUI.Label (new Rect (0, 50, 100, 100), m_movieTexture.GetYandUVInfo ());
//
//		GUI.Label (new Rect (200, 50, 150, 100), m_movieTexture.GetPicInfo ());
//
//		GUI.Label (new Rect (0, 150,200,400), m_movieTexture.GetElapsedTime ());
//		GUI.Label (new Rect (0, 200, 250, 200), m_movieTexture.GetChannelTextureInfo ());
//		GUI.Label (new Rect (0, 250, 250, 200), m_movieTexture.GetPixels ());

//		GUI.Label (new Rect (0, 300, 300, 100), m_movieTexture.GetYandUVInfo ());
	}
}
