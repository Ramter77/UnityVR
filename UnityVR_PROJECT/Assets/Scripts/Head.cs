using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Head : MonoBehaviour {

	[Range(-1, 1)]
	public float mThreshold = 2.5f;
	public ParticleSystem mParticleSystem;

    //public float mAudioStream = 3;
    public AudioSource audioSource;
    public AudioClip mAudioStream = null;
	private Coroutine mCurrentSmoke = null;

    public bool isInput;


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);

        //Debug.Log(audioSource.clip);
        audioSource.loop = true;

        foreach (var device in Microphone.devices)
        {
            Debug.Log(device);
            //Debug.Log(Microphone.IsRecording(device));
        }

        //audioSource.Play();

        //if (Microphone.GetPosition(null) > 0) { }
        //Debug.Log("start playing... position is " + Microphone.GetPosition(null));
        //audioSource.Play();

        
    }

    private void Update()
    {
        //Debug.Log("start playing... position is " + Microphone.GetPosition(null));


        //TestForAudioInput();
        
    }

    private void OnTriggerEnter(Collider other) {
		//Debug.Log("Entered head collider: " + other);

		var cigarette = other.gameObject.GetComponent<Cigarette>();

		if (cigarette) {
			mCurrentSmoke = StartCoroutine(cigarette.Drag(this));

			mParticleSystem.Stop();
		}
	}

    private void OnTriggerStay(Collider other)
    {
        var cigarette = other.gameObject.GetComponent<Cigarette>();

        if (cigarette)
        {
            TestForAudioInput();
        }        
    }

    private void OnTriggerExit(Collider other) {
		//Debug.Log("Exited head collider: " + other);
		if (other.gameObject.tag == "Cigarette") {
			if (mCurrentSmoke != null) {
				StopCoroutine(mCurrentSmoke);

                if (isInput)
                {
                    mParticleSystem.Play();
                }
			}
		}
	}

    public float Sum(params float[] array)
    {
        float result = 0;

        for (int i = 0; i < array.Length; i++)
        {
            result += array[i];
        }

        return result;
    }

    public float Average(params float[] customerssalary)
    {
        float sum = Sum(customerssalary);
        float result = (float)sum / customerssalary.Length;
        return result;
    }

    public bool TestForAudioInput() {
		//Set the max amount of samples: 44100
		int length = audioSource.clip.samples * audioSource.clip.channels;
		float[] samples = new float[length];

        
        //Get data
        audioSource.clip.GetData(samples, 0);

        //Debug.Log(audioSource.clip.GetData(samples, 0) );
        //Average

        //Debug.Log(samples[0] * 10000);
        //float averageSample = samples.Average() * 10000;

        //float sum = Sum(samples);
        //float averageSample = (float)(sum / samples.Length) * 10000;

        Debug.Log(samples);

        float averageSample = Average(samples) * 10000;

        Debug.Log("samples: " + averageSample);



        //If within threshold
        isInput = averageSample < mThreshold ? true: false;
        //bool isInput = audioSource < mThreshold ? true: false;

        Debug.Log("isInput: " + isInput);
		return isInput;
	}

    /* 	private void TestAudio() {
            while (!(Microphone.GetPosition("Vive") > 0)) {
                //Wait
            }

            AudioSource audioSource = GetComponent<AudioSource>();
            //audioSource.clip = audioSource;
            //audioSource.Play();
        }
    */
}
