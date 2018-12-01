using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Head : MonoBehaviour {

	[Range(0, 5)]
	public float mThreshold = 2.5f;
	public ParticleSystem mParticleSystem;

	public float mAudioStream = 3;
	//private AudioClip mAudioStream = null;
	private Coroutine mCurrentSmoke = null;

	void Awake() {
		//Devices

		//Frequencies
	}
	// Use this for initialization
	void Start () {
		//mAudioStream = Microphone.Start("Vive", true, 3, 44100);	//44.1khz
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Entered head collider: " + other);

		var cigarette = other.gameObject.GetComponent<Cigarette>();

		if (cigarette) {
			mCurrentSmoke = StartCoroutine(cigarette.Drag(this));

			mParticleSystem.Stop();
		}
	}

	private void OnTriggerExit(Collider other) {
		Debug.Log("Exited head collider: " + other);
		if (other.gameObject.tag == "Cigarette") {
			//if (mCurrentSmoke != null) {
				//StopCoroutine(mCurrentSmoke);

				mParticleSystem.Play();
			//}
		}
	}

	public bool TestForAudioInput() {
		//Set the max amount of samples: 44100
		//int length = mAudioStream.samples * mAudioStream.channels;
		//float[] samples = new float[length];

		//Get data
		//mAudioStream.GetData(samples, 0);

		//Average
		//float averageSample = samples.Average() * 10000;

		//If within threshold
		//bool isInput = averageSample < mThreshold ? true: false;
		bool isInput = mAudioStream < mThreshold ? true: false;

		Debug.Log("isInput: " + isInput);
		return isInput;
	}

/* 	private void TestAudio() {
		while (!(Microphone.GetPosition("Vive") > 0)) {
			//Wait
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		//audioSource.clip = mAudioStream;
		//audioSource.Play();
	}
*/
}
