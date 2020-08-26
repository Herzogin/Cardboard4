using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Windows.Speech;

[RequireComponent(typeof(EventTrigger))]
public class GazeAndVoiceBehavior : MonoBehaviour
{
    bool gazedAt = false;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        // Add triggers.
        EventTrigger trigger = GetComponent<EventTrigger>();

        // EventTrigger entry for PointerEnter.
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

        // EventTrigger entry for PointerExit.
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

        // Add the entries to the Event Trigger.
        trigger.triggers.Add(enterEntry);
        trigger.triggers.Add(exitEntry);
        
        actions.Add("fall", Fall);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        Debug.Log("new keywordRecognizer");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gazedAt)
        {
            Debug.Log("gazedAt");
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
        }
    }

    private void OnPointerEnter(PointerEventData data)
    {
        gazedAt = true;
    }

    private void OnPointerExit(PointerEventData data)
    {
        gazedAt = false;
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Fall()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }
}
