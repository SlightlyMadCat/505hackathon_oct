using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicManager : MonoBehaviour
{
    [Serializable]
    public class SongSample
    {
        public AudioClip clip;
    }

    [SerializeField] private List<SongSample> songSamples = new List<SongSample>();
    [SerializeField] private AudioSource radioSource;
    private int lastPlayedId = 1;
    
    private void OnMouseDown()
    {
        PlayRandomTrack();
    }

    private void PlayRandomTrack()
    {
        bool _generated = false;
        int _newTrack = 0;
        
        while (!_generated)
        {
            _newTrack = Random.Range(0, songSamples.Count);
            if (_newTrack != lastPlayedId) _generated = true;
        }

        lastPlayedId = _newTrack;
        radioSource.clip = songSamples[_newTrack].clip;
        radioSource.Play();
    }
}
