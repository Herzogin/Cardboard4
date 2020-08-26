using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceMovementCube : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("up", Up);
        actions.Add("down", Down);
        actions.Add("back", Back);
        actions.Add("bigger", Bigger);
        actions.Add("smaller", Smaller);
        actions.Add("rotate right", RotateRight);
        actions.Add("rotate left", RotateLeft);
        //actions.Add("fall", Fall);
        actions.Add("stop", Stop);


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        //keywordRecognizer.Stop();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Forward()
    {
        transform.Translate(0, 0, 1);
    }

    private void Back()
    {
        transform.Translate(0, 0, -1);
    }

    private void Up()
    {
        transform.Translate(0, 1, 0);
        //transform.Translate(Vector3.up * Time.deltaTime);
        //transform.position += transform.up * Time.deltaTime;

    }

    private void Down()
    {
        transform.Translate(0, -1, 0);
    }

    private void Bigger()
    {
        transform.localScale = new Vector3(2, 2, 2);
    }

    private void Smaller()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void RotateRight()
    {
        transform.Rotate(0.0f, 45.0f, 0.0f);
    }

    private void RotateLeft()
    {
        transform.Rotate(0.0f, -45.0f, 0.0f);
    }

    private void Fall()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

    private void Stop()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }
}
