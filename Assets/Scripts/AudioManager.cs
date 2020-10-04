using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : Singleton<AudioManager> {
    
    [EventRef]
    public string jetPackNoise;
    FMOD.Studio.EventInstance jetPackInstance;

    [EventRef]
    public string extinguisherNoise;
    FMOD.Studio.EventInstance extinguisherInstance;


    public override void Awake(){
        base.Awake();
        InitializeSounds();
    }


    void InitializeSounds(){
        jetPackInstance = RuntimeManager.CreateInstance(jetPackNoise);
        jetPackInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));

        extinguisherInstance = RuntimeManager.CreateInstance(extinguisherNoise);
        extinguisherInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
    }

    void PlaySound(FMOD.Studio.EventInstance instance, bool start=true){
        if(start){
            FMOD.Studio.PLAYBACK_STATE playbackState;
            instance.getPlaybackState(out playbackState);
            if(playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
                instance.start();
            }
        }else{
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }


    public void PlayJetPack(bool start=true){
        PlaySound(jetPackInstance, start);
    }

    public void PlayExtinguisher(bool start=true){
        PlaySound(extinguisherInstance, start);
    }

}
