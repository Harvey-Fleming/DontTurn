using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Runtime.CompilerServices;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float AmbienceVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;
    [Range(0, 1)]

    private Bus masterBus;
    private Bus ambienceBus;
    private Bus sfxBus;

    private List<EventInstance> eventInstances;
    
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance ambienceEventInstance;
    private EventInstance whispersEventInstance;
    public static AudioManager instance { get; private set; }

    public GameObject Player;
    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        ambienceBus.setVolume(AmbienceVolume);
        sfxBus.setVolume(SFXVolume);
    }

    private void Start()
    {
        InitialiseAmbience(FMODEvents.instance.levelAmbience);
        InitialiseWhispers(FMODEvents.instance.curseWhispers);
    }

    public void InitialiseAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateEventInstance(ambienceEventReference);
        ambienceEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Player.transform.position));
        ambienceEventInstance.start();

    }

    public void InitialiseWhispers(EventReference whispersEventReference)
    {
        whispersEventInstance = CreateEventInstance(whispersEventReference);
        whispersEventInstance.start();
    }

    public void SetWhispersParameter(float parameterValue)
    {
        whispersEventInstance.setParameterByName("WhisperVolume", parameterValue);
    }

    public void SetAmbienceArea(AmbienceArea area)
    {
        ambienceEventInstance.setParameterByName("area", (float) area);
        ambienceEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Player.transform.position));
    }

    public void SetFootstepsSurface(FootstepTypes area)
    {
        Player.GetComponent<PlayerMovement>().playerFootsteps.setParameterByName("Surface", (float)area);
    }


    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void CleanUp()
    {
        //stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}

