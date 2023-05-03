using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Runtime.CompilerServices;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance ambienceEventInstance;
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
    }

    private void Start()
    {
        InitialiseAmbience(FMODEvents.instance.cityAmbience);
        //InitialiseAmbience(FMODEvents.instance.sewerAmbience);
        //SetAmbienceParameter("Sewer_ambience_intensity", 1);
        SetAmbienceParameter("City_ambience_intensity", 0);
    }

    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void InitialiseAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateEventInstance(ambienceEventReference);
        ambienceEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Player.transform.position));
        ambienceEventInstance.start();

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

